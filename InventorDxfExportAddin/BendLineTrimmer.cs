using IxMilia.Dxf;
using IxMilia.Dxf.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace InventorDxfExportAddin.DxfExport
{
    /// <summary>
    /// Trims bend lines so they do not pass through interior cutouts.
    /// Strategy:
    ///   1. Parametrize the bend line as P(t) = A + t*(B-A) for t ∈ [0,1].
    ///   2. Collect all t-values where the line crosses the outer profile boundary
    ///      or any interior profile entity.
    ///   3. For each sub-interval, sample the midpoint and keep it only if the
    ///      midpoint is inside the outer profile AND outside all interior regions.
    /// </summary>
    internal static class BendLineTrimmer
    {
        private const double Eps = 1e-9;

        /// <summary>
        /// Returns the sub-segments of the bend line (A → B) that lie inside
        /// <paramref name="outer"/> and outside the closed regions formed by
        /// <paramref name="interiorEntities"/>, each pulled back by
        /// <paramref name="setback"/> at every boundary crossing.
        /// </summary>
        public static List<(DxfPoint Start, DxfPoint End)> Trim(
            DxfPoint a, DxfPoint b,
            DxfLwPolyline outer,
            IReadOnlyList<DxfEntity> interiorEntities,
            double setback = 0.0)
        {
            double ax = a.X, ay = a.Y;
            double dx = b.X - ax, dy = b.Y - ay;
            double len = Math.Sqrt(dx * dx + dy * dy);
            if (len < Eps)
                return new List<(DxfPoint, DxfPoint)>();

            // Setback expressed as a fraction of the full line length
            double sb = setback / len;

            // Gather all boundary-crossing t-values
            var tVals = new List<double> { 0.0, 1.0 };
            LwPolylineCrossings(ax, ay, dx, dy, outer, tVals);
            foreach (var ent in interiorEntities)
                EntityCrossings(ax, ay, dx, dy, ent, tVals);

            // Sort and deduplicate within [0,1]
            var ts = tVals
                .Select(t => Math.Max(0.0, Math.Min(1.0, t)))
                .Distinct(new DoubleComparer(Eps))
                .OrderBy(t => t)
                .ToList();

            var result = new List<(DxfPoint, DxfPoint)>();
            for (int i = 0; i < ts.Count - 1; i++)
            {
                double t0 = ts[i], t1 = ts[i + 1];
                if (t1 - t0 < Eps) continue;

                // Test the raw midpoint to decide whether this interval is material
                double tm = (t0 + t1) * 0.5;
                double mx = ax + tm * dx, my = ay + tm * dy;

                if (!PointInLwPolyline(mx, my, outer)) continue;
                if (PointInInteriors(mx, my, interiorEntities)) continue;

                // Apply setback inward from both ends of this segment
                double s0 = t0 + sb, s1 = t1 - sb;
                if (s1 - s0 < Eps) continue;  // segment too short after setback

                result.Add((
                    new DxfPoint(ax + s0 * dx, ay + s0 * dy, 0),
                    new DxfPoint(ax + s1 * dx, ay + s1 * dy, 0)));
            }
            return result;
        }

        // ── Boundary crossing t-value collection ─────────────────────────────

        private static void LwPolylineCrossings(
            double ax, double ay, double dx, double dy,
            DxfLwPolyline poly, List<double> tVals)
        {
            var verts = poly.Vertices.ToList();
            int n = verts.Count;
            for (int i = 0; i < n; i++)
            {
                var v0 = verts[i];
                var v1 = verts[(i + 1) % n];
                if (Math.Abs(v0.Bulge) < Eps)
                    SegmentCrossings(ax, ay, dx, dy, v0.X, v0.Y, v1.X, v1.Y, tVals);
                else
                    BulgeArcCrossings(ax, ay, dx, dy, v0, v1, tVals);
            }
        }

        private static void EntityCrossings(
            double ax, double ay, double dx, double dy,
            DxfEntity ent, List<double> tVals)
        {
            if (ent is DxfLine line)
                SegmentCrossings(ax, ay, dx, dy, line.P1.X, line.P1.Y, line.P2.X, line.P2.Y, tVals);
            else if (ent is DxfArc arc)
                ArcEntityCrossings(ax, ay, dx, dy, arc, tVals);
            else if (ent is DxfCircle circ)
                CircleCrossings(ax, ay, dx, dy, circ.Center.X, circ.Center.Y, circ.Radius, tVals);
            else if (ent is DxfLwPolyline lwp)
                LwPolylineCrossings(ax, ay, dx, dy, lwp, tVals);
        }

        /// <summary>Adds the t at which P(t) = A+t*D crosses the finite segment (cx,cy)-(ex,ey).</summary>
        private static void SegmentCrossings(
            double ax, double ay, double dx, double dy,
            double cx, double cy, double ex, double ey,
            List<double> tVals)
        {
            double sdx = ex - cx, sdy = ey - cy;
            double denom = dx * sdy - dy * sdx;
            if (Math.Abs(denom) < Eps) return;
            double t = ((cx - ax) * sdy - (cy - ay) * sdx) / denom;
            double s = ((cx - ax) * dy  - (cy - ay) * dx)  / denom;
            if (s >= -Eps && s <= 1.0 + Eps)
                tVals.Add(t);
        }

        private static void CircleCrossings(
            double ax, double ay, double dx, double dy,
            double cx, double cy, double r, List<double> tVals)
        {
            double wx = ax - cx, wy = ay - cy;
            double a = dx * dx + dy * dy;
            double b = 2 * (wx * dx + wy * dy);
            double c = wx * wx + wy * wy - r * r;
            double disc = b * b - 4 * a * c;
            if (disc < 0) return;
            double sq = Math.Sqrt(disc);
            tVals.Add((-b - sq) / (2 * a));
            tVals.Add((-b + sq) / (2 * a));
        }

        private static void ArcEntityCrossings(
            double ax, double ay, double dx, double dy,
            DxfArc arc, List<double> tVals)
        {
            double cx = arc.Center.X, cy = arc.Center.Y, r = arc.Radius;
            double startA = arc.StartAngle * Math.PI / 180;
            double endA   = arc.EndAngle   * Math.PI / 180;

            double wx = ax - cx, wy = ay - cy;
            double a  = dx * dx + dy * dy;
            double b  = 2 * (wx * dx + wy * dy);
            double c  = wx * wx + wy * wy - r * r;
            double disc = b * b - 4 * a * c;
            if (disc < 0) return;
            double sq = Math.Sqrt(disc);
            foreach (double t in new[] { (-b - sq) / (2 * a), (-b + sq) / (2 * a) })
            {
                double px = ax + t * dx, py = ay + t * dy;
                double angle = Math.Atan2(py - cy, px - cx);
                if (AngleOnArc(angle, startA, endA, ccw: true))  // DxfArc is always CCW
                    tVals.Add(t);
            }
        }

        private static void BulgeArcCrossings(
            double ax, double ay, double dx, double dy,
            DxfLwPolylineVertex v0, DxfLwPolylineVertex v1,
            List<double> tVals)
        {
            BulgeToArc(v0, v1, out double cx, out double cy, out double r, out double startA, out double endA);
            bool ccw = v0.Bulge > 0;

            double wx = ax - cx, wy = ay - cy;
            double a  = dx * dx + dy * dy;
            double b  = 2 * (wx * dx + wy * dy);
            double c  = wx * wx + wy * wy - r * r;
            double disc = b * b - 4 * a * c;
            if (disc < 0) return;
            double sq = Math.Sqrt(disc);
            foreach (double t in new[] { (-b - sq) / (2 * a), (-b + sq) / (2 * a) })
            {
                double px = ax + t * dx, py = ay + t * dy;
                double angle = Math.Atan2(py - cy, px - cx);
                if (AngleOnArc(angle, startA, endA, ccw))
                    tVals.Add(t);
            }
        }

        // ── Point-in-region tests ─────────────────────────────────────────────

        /// <summary>Ray-cast rightward from (px,py); returns true if inside the closed LwPolyline.</summary>
        private static bool PointInLwPolyline(double px, double py, DxfLwPolyline poly)
        {
            int crossings = 0;
            var verts = poly.Vertices.ToList();
            int n = verts.Count;
            for (int i = 0; i < n; i++)
            {
                var v0 = verts[i];
                var v1 = verts[(i + 1) % n];
                if (Math.Abs(v0.Bulge) < Eps)
                    crossings += RayVsSegment(px, py, v0.X, v0.Y, v1.X, v1.Y);
                else
                    crossings += RayVsBulgeArc(px, py, v0, v1);
            }
            return (crossings & 1) == 1;
        }

        /// <summary>
        /// Returns true if (px,py) is inside any closed region formed by the interior entities.
        /// Uses ray-casting against all entities collectively — correct because interior
        /// profiles form closed loops and crossing parity holds over the whole set.
        /// </summary>
        private static bool PointInInteriors(double px, double py, IReadOnlyList<DxfEntity> entities)
        {
            int crossings = 0;
            foreach (var ent in entities)
            {
                if (ent is DxfLine line)
                    crossings += RayVsSegment(px, py, line.P1.X, line.P1.Y, line.P2.X, line.P2.Y);
                else if (ent is DxfArc arc)
                    crossings += RayVsArcEntity(px, py, arc);
                else if (ent is DxfCircle circ)
                    crossings += RayVsCircle(px, py, circ);
                else if (ent is DxfLwPolyline lwp)
                {
                    var verts = lwp.Vertices.ToList();
                    int n = verts.Count;
                    for (int i = 0; i < n; i++)
                    {
                        var v0 = verts[i];
                        var v1 = verts[(i + 1) % n];
                        if (Math.Abs(v0.Bulge) < Eps)
                            crossings += RayVsSegment(px, py, v0.X, v0.Y, v1.X, v1.Y);
                        else
                            crossings += RayVsBulgeArc(px, py, v0, v1);
                    }
                }
            }
            return (crossings & 1) == 1;
        }

        // ── Ray crossing primitives (rightward ray from (px,py)) ─────────────

        private static int RayVsSegment(
            double px, double py,
            double x1, double y1, double x2, double y2)
        {
            // Count crossing only when segment straddles py, using the "lower endpoint
            // belongs to edge" convention to handle shared vertices correctly.
            if ((y1 < py) == (y2 < py)) return 0;
            double xCross = x1 + (py - y1) * (x2 - x1) / (y2 - y1);
            return xCross > px ? 1 : 0;
        }

        private static int RayVsCircle(double px, double py, DxfCircle circ)
        {
            double dy  = py - circ.Center.Y;
            double disc = circ.Radius * circ.Radius - dy * dy;
            if (disc < 0) return 0;
            double sq = Math.Sqrt(disc);
            int count = 0;
            // Left intersection (enters circle going right)
            if (circ.Center.X - sq > px) count++;
            // Right intersection (exits circle going right)
            if (circ.Center.X + sq > px) count++;
            return count;
        }

        private static int RayVsArcEntity(double px, double py, DxfArc arc)
        {
            double cx = arc.Center.X, cy = arc.Center.Y, r = arc.Radius;
            double startA = arc.StartAngle * Math.PI / 180;
            double endA   = arc.EndAngle   * Math.PI / 180;

            double dy = py - cy;
            double disc = r * r - dy * dy;
            if (disc < 0) return 0;
            double sq = Math.Sqrt(disc);
            int count = 0;
            foreach (double xCross in new[] { cx - sq, cx + sq })
            {
                if (xCross <= px) continue;
                double angle = Math.Atan2(dy, xCross - cx);
                if (AngleOnArc(angle, startA, endA, ccw: true))
                    count++;
            }
            return count;
        }

        private static int RayVsBulgeArc(double px, double py, DxfLwPolylineVertex v0, DxfLwPolylineVertex v1)
        {
            BulgeToArc(v0, v1, out double cx, out double cy, out double r, out double startA, out double endA);
            bool ccw = v0.Bulge > 0;

            double dy = py - cy;
            double disc = r * r - dy * dy;
            if (disc < 0) return 0;
            double sq = Math.Sqrt(disc);
            int count = 0;
            foreach (double xCross in new[] { cx - sq, cx + sq })
            {
                if (xCross <= px) continue;
                double angle = Math.Atan2(dy, xCross - cx);
                if (AngleOnArc(angle, startA, endA, ccw))
                    count++;
            }
            return count;
        }

        // ── Geometry utilities ────────────────────────────────────────────────

        /// <summary>Converts DXF LwPolyline bulge encoding to arc (center, radius, start/end angles).</summary>
        private static void BulgeToArc(
            DxfLwPolylineVertex v0, DxfLwPolylineVertex v1,
            out double cx, out double cy, out double r,
            out double startAngle, out double endAngle)
        {
            double b      = v0.Bulge;
            double chX    = v1.X - v0.X, chY = v1.Y - v0.Y;
            double d      = Math.Sqrt(chX * chX + chY * chY);
            double absB   = Math.Abs(b);

            r = d * (1.0 + b * b) / (4.0 * absB);

            // Perpendicular to chord in CCW direction (left-hand normal of chord v0→v1)
            double perpX = -chY / d, perpY = chX / d;
            double alpha = 2.0 * Math.Atan(absB);       // half arc-angle
            double cDist = r * Math.Cos(alpha);          // center distance from chord midpoint

            // Positive bulge → CCW arc → center to the left (+perp).
            // Negative bulge → CW arc → center to the right (-perp).
            double sign = Math.Sign(b);
            double midX = (v0.X + v1.X) * 0.5, midY = (v0.Y + v1.Y) * 0.5;
            cx = midX + sign * cDist * perpX;
            cy = midY + sign * cDist * perpY;

            startAngle = Math.Atan2(v0.Y - cy, v0.X - cx);
            endAngle   = Math.Atan2(v1.Y - cy, v1.X - cx);
        }

        /// <summary>Returns true if angle <paramref name="a"/> lies on the arc from
        /// <paramref name="startA"/> to <paramref name="endA"/> in the given direction.</summary>
        private static bool AngleOnArc(double a, double startA, double endA, bool ccw)
        {
            double sweep = ccw
                ? Wrap2Pi(endA   - startA)
                : Wrap2Pi(startA - endA);
            double delta = ccw
                ? Wrap2Pi(a - startA)
                : Wrap2Pi(startA - a);
            return delta <= sweep + Eps;
        }

        private static double Wrap2Pi(double a)
        {
            a %= 2 * Math.PI;
            return a < 0 ? a + 2 * Math.PI : a;
        }

        // ── Utilities ─────────────────────────────────────────────────────────

        private class DoubleComparer : IEqualityComparer<double>
        {
            private readonly double _tol;
            public DoubleComparer(double tol) { _tol = tol; }
            public bool Equals(double x, double y) => Math.Abs(x - y) <= _tol;
            public int GetHashCode(double obj) => 0;  // bucket everything together; Equals does the real work
        }
    }
}
