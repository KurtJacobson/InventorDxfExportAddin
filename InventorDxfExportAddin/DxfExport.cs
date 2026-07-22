using Inventor;
using InventorDxfExportAddin;
using Color = System.Drawing.Color;
using IxMilia;
using IxMilia.Dxf;
using IxMilia.Dxf.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace InventorDxfExportAddin.DxfExport
{
    public class DxfExportEngine
    {
        private PartDocument partDoc = null;
        private string exportFileName = null;
        private string exportDirectory = null;

        public DxfExportEngine(PartDocument doc) 
        {
            this.partDoc = doc;
        }

        public string ExportFilename
        {
            get 
            {
                if (this.exportFileName == null)
                {
                   return System.IO.Path.GetFileNameWithoutExtension(partDoc.DisplayName) + ".dxf";
                }                
                return exportFileName; 
            }
            set { exportFileName = value; }
        }

        public string ExportDirectory
        {
            get
            {
                if (this.exportDirectory != null)
                    return this.exportDirectory;

                var settings = Properties.DxfSettings.Default;

                if (settings.ExportMode == "CustomDirectory" && !string.IsNullOrWhiteSpace(settings.ExportDirectory))
                {
                    System.IO.Directory.CreateDirectory(settings.ExportDirectory);
                    return settings.ExportDirectory;
                }

                if (settings.ExportMode == "TemplatePath")
                {
                    string baseDir = string.IsNullOrWhiteSpace(settings.TemplateBaseDirectory)
                        ? System.IO.Path.GetDirectoryName(partDoc.FullFileName) ?? ""
                        : settings.TemplateBaseDirectory;

                    string subfolders = ExpandTemplate(settings.SubfolderTemplate ?? "", partDoc);
                    string dir = string.IsNullOrWhiteSpace(subfolders)
                        ? baseDir
                        : System.IO.Path.Combine(baseDir, subfolders);
                    System.IO.Directory.CreateDirectory(dir);

                    if (!string.IsNullOrWhiteSpace(settings.FilenameTemplate))
                        this.exportFileName = ExpandTemplate(settings.FilenameTemplate, partDoc) + ".dxf";

                    return dir;
                }

                // Default: save next to the source file (IPT or STP)
                var fullFileName = partDoc.FullFileName;
                if (!string.IsNullOrEmpty(fullFileName))
                {
                    var dir = System.IO.Path.GetDirectoryName(fullFileName);
                    System.IO.Directory.CreateDirectory(dir);
                    return dir;
                }

                // Document hasn't been saved as an IPT — prompt user for output location,
                // defaulting to the source STP file's directory if we can find it.
                using var dialog = new SaveFileDialog
                {
                    Title = "Save DXF As",
                    Filter = "DXF Files (*.dxf)|*.dxf",
                    FileName = ExportFilename,
                    DefaultExt = "dxf",
                    InitialDirectory = GetSourceStpDirectory()
                };

                if (dialog.ShowDialog() != DialogResult.OK)
                    return null;

                this.exportFileName  = System.IO.Path.GetFileName(dialog.FileName);
                this.exportDirectory = System.IO.Path.GetDirectoryName(dialog.FileName);
                return this.exportDirectory;
            }

            set { this.exportDirectory = value; }
        }

        /// <summary>
        /// Searches document property sets for a string value that points to a .stp/.step file
        /// on disk. Returns the full path, or null if not found.
        /// </summary>
        private string GetSourceStpPath()
        {
            try
            {
                foreach (PropertySet propSet in partDoc.PropertySets)
                {
                    foreach (Property prop in propSet)
                    {
                        if (prop.Value is string val &&
                            (val.EndsWith(".stp", StringComparison.OrdinalIgnoreCase) ||
                             val.EndsWith(".step", StringComparison.OrdinalIgnoreCase)) &&
                            System.IO.File.Exists(val))
                        {
                            LogManager.Log.Information($"Found source STP path in property '{prop.Name}': {val}");
                            return val;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                LogManager.Log.Warning($"Could not read document properties for STP path: {ex.Message}");
            }

            return null;
        }

        private string GetSourceStpDirectory()
        {
            var stpPath = GetSourceStpPath();
            return stpPath != null
                ? System.IO.Path.GetDirectoryName(stpPath)
                : System.Environment.GetFolderPath(System.Environment.SpecialFolder.MyDocuments);
        }

        public string ExportFullPath
        {
            get {
                return System.IO.Path.Combine(ExportDirectory, ExportFilename); }
        }

        /// <summary>
        /// Expands {Token} placeholders using the part document's iProperties and sheet metal data.
        /// Each token value is sanitized so it is safe to use as a path segment.
        ///
        /// Simple tokens: {Material}  {PartNumber}  {Description}  {RevisionNumber}  {FileName}
        ///
        /// Thickness token supports optional unit and precision:
        ///   {Thickness}           — document units, auto precision (trailing zeros trimmed)
        ///   {Thickness:mm}        — force millimetres, auto precision
        ///   {Thickness:in:3}      — force inches, 3 decimal places
        ///   {Thickness::2}        — document units, 2 decimal places
        /// Supported units: mm, cm, m, in, ft
        /// </summary>
        private string ExpandTemplate(string template, PartDocument doc)
            => TemplateHelper.Expand(template, (Document)doc, sanitize: true);


        /// <summary>
        /// Checks that all entities on the outline layer form a single closed loop.
        /// DxfSpline entities are included in the connectivity check using their
        /// evaluated start/end points, but their endpoints cannot be snapped
        /// (spline geometry repair would require moving control points).
        /// Endpoints of DxfLine entities that fall within snapTolerance of an unmatched
        /// endpoint from another entity are snapped together. Arc endpoints are checked
        /// but not modified (arc geometry repair would require re-parameterisation).
        /// </summary>
        private static bool ValidateAndRepairOutline(DxfFile file, string layerName,
            double connectedTol = 1e-6, double snapTol = 1e-3)
        {
            var lines   = file.Entities.OfType<DxfLine>  ().Where(e => e.Layer == layerName).ToList();
            var arcs    = file.Entities.OfType<DxfArc>   ().Where(e => e.Layer == layerName).ToList();
            var splines = file.Entities.OfType<DxfSpline>().Where(e => e.Layer == layerName).ToList();

            int total = lines.Count + arcs.Count + splines.Count;
            if (total == 0)
            {
                LogManager.Log.Warning($"Outline layer '{layerName}' has no line, arc, or spline entities.");
                MessageBox.Show($"The exported DXF has no geometry on the outline layer '{layerName}'.\n\nThe export has been aborted.",
                    "Open Contour", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (splines.Count > 0)
                LogManager.Log.Information($"Outline '{layerName}': {splines.Count} spline(s) will be tessellated.");

            // Build a flat list of endpoints: point value, optional setter, owning entity index.
            // Lines are mutable; arc and spline endpoints are read-only.
            var pts = new List<(DxfPoint pt, Action<DxfPoint> set, int entityIdx)>();
            int idx = 0;
            foreach (var l in lines)
            {
                var line = l;
                pts.Add((l.P1, p => line.P1 = p, idx));
                pts.Add((l.P2, p => line.P2 = p, idx));
                idx++;
            }
            foreach (var a in arcs)
            {
                pts.Add((OutlineHelper.ArcStart(a), null, idx));
                pts.Add((OutlineHelper.ArcEnd(a),   null, idx));
                idx++;
            }
            foreach (var s in splines)
            {
                pts.Add((OutlineHelper.EvaluateBSpline(s, 0.0), null, idx));
                pts.Add((OutlineHelper.EvaluateBSpline(s, 1.0), null, idx));
                idx++;
            }

            // Mark endpoints that already share a point with another entity.
            bool[] connected = new bool[pts.Count];
            for (int i = 0; i < pts.Count; i++)
                for (int j = 0; j < pts.Count; j++)
                    if (pts[i].entityIdx != pts[j].entityIdx &&
                        OutlineHelper.Dist(pts[i].pt, pts[j].pt) <= connectedTol)
                        connected[i] = connected[j] = true;

            var openIdx = Enumerable.Range(0, pts.Count).Where(i => !connected[i]).ToList();

            if (openIdx.Count == 0)
            {
                LogManager.Log.Information(
                    $"Outline '{layerName}': closed loop confirmed ({total} segments).");
                return true;
            }

            LogManager.Log.Warning(
                $"Outline '{layerName}': {openIdx.Count} open endpoint(s) across {total} segments — attempting repair.");

            // Snap open endpoints that are within snapTol of each other.
            var processed = new HashSet<int>();
            int repaired = 0;
            foreach (int i in openIdx)
            {
                if (processed.Contains(i)) continue;
                foreach (int j in openIdx)
                {
                    if (j == i || pts[j].entityIdx == pts[i].entityIdx || processed.Contains(j)) continue;
                    double d = OutlineHelper.Dist(pts[i].pt, pts[j].pt);
                    if (d > snapTol) continue;

                    var mid = OutlineHelper.Midpt(pts[i].pt, pts[j].pt);
                    pts[i].set?.Invoke(mid);
                    pts[j].set?.Invoke(mid);
                    processed.Add(i);
                    processed.Add(j);
                    repaired++;
                    LogManager.Log.Information($"  Snapped gap of {d:G4} units between entity {pts[i].entityIdx} and {pts[j].entityIdx}.");
                    break;
                }
            }

            int remaining = openIdx.Count - processed.Count;
            if (remaining == 0)
            {
                LogManager.Log.Information($"Outline repair complete: {repaired} gap(s) closed.");
                return true;
            }

            LogManager.Log.Warning(
                $"Outline repair: {repaired} gap(s) closed, {remaining} open endpoint(s) remain.");
            MessageBox.Show(
                $"The flat pattern outline on layer '{layerName}' is not a closed contour.\n\n" +
                $"{remaining} open endpoint(s) could not be automatically joined " +
                $"(largest gap may exceed the snap tolerance of {snapTol} units).\n\n" +
                "Please check the model geometry and try again.",
                "Open Contour", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return false;
        }

        /// <summary>
        /// Chains all DxfLine and DxfArc entities on the outline layer into a single closed
        /// DxfLwPolyline, removing the original entities. Arc segments are encoded as bulge
        /// values (bulge = tan(included_angle / 4), positive = CCW, negative = CW traversal).
        /// Called after ValidateAndRepairOutline so endpoints are already coincident.
        /// </summary>
        private static bool MergeOutlineToPolyline(DxfFile file, string layerName,
            double chainTol = 1e-4, int splineSamples = 64)
        {
            var lines   = file.Entities.OfType<DxfLine>  ().Where(e => e.Layer == layerName).ToList();
            var arcs    = file.Entities.OfType<DxfArc>   ().Where(e => e.Layer == layerName).ToList();
            var splines = file.Entities.OfType<DxfSpline>().Where(e => e.Layer == layerName).ToList();

            if (lines.Count == 0 && arcs.Count == 0 && splines.Count == 0) return true;

            // Build a flat list of segments: start point, end point, forward bulge, source entity.
            // Arc bulge = tan(included_angle / 4), positive because DXF arcs are CCW.
            // Splines are represented as a chain of straight micro-segments (bulge = 0).
            var segments = new List<OutlineHelper.Seg>();
            foreach (var l in lines)
                segments.Add(new OutlineHelper.Seg(l.P1, l.P2, 0.0, l));
            foreach (var a in arcs)
            {
                double included = ((a.EndAngle - a.StartAngle) % 360 + 360) % 360;
                double bulge = Math.Tan(included * Math.PI / 180.0 / 4.0);
                segments.Add(new OutlineHelper.Seg(OutlineHelper.ArcStart(a), OutlineHelper.ArcEnd(a), bulge, a));
            }
            foreach (var s in splines)
            {
                // Tessellate: emit splineSamples straight segments from t=0 to t=1.
                var pts = Enumerable.Range(0, splineSamples + 1)
                    .Select(i => OutlineHelper.EvaluateBSpline(s, (double)i / splineSamples))
                    .ToList();
                // All micro-segments share the same source entity so the whole spline is removed together.
                for (int i = 0; i < pts.Count - 1; i++)
                    segments.Add(new OutlineHelper.Seg(pts[i], pts[i + 1], 0.0, s));
            }

            // Walk segments into an ordered chain starting from the first segment.
            var chain   = new List<(DxfPoint pt, double bulge)>();
            var remaining = new List<OutlineHelper.Seg>(segments);

            var first = remaining[0];
            remaining.RemoveAt(0);
            chain.Add((first.Start, first.Bulge));
            var tip = first.End;
            var chainStart = first.Start;

            while (remaining.Count > 0)
            {
                bool found = false;
                for (int i = 0; i < remaining.Count; i++)
                {
                    var s = remaining[i];
                    if (OutlineHelper.Dist(tip, s.Start) <= chainTol)
                    {
                        chain.Add((s.Start, s.Bulge));
                        tip = s.End;
                        remaining.RemoveAt(i);
                        found = true;
                        break;
                    }
                    if (OutlineHelper.Dist(tip, s.End) <= chainTol)
                    {
                        // Traversed end→start: negate bulge (CW instead of CCW).
                        chain.Add((s.End, -s.Bulge));
                        tip = s.Start;
                        remaining.RemoveAt(i);
                        found = true;
                        break;
                    }
                }
                if (!found)
                {
                    LogManager.Log.Warning($"Outline '{layerName}': could not build a continuous chain.");
                    MessageBox.Show(
                        $"The outline on layer '{layerName}' could not be converted to a polyline.\n\n" +
                        "The segments do not form a single continuous chain. " +
                        "Please check the model geometry and try again.",
                        "Polyline Conversion Failed", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return false;
                }
            }

            if (OutlineHelper.Dist(tip, chainStart) > chainTol)
            {
                double gap = OutlineHelper.Dist(tip, chainStart);
                LogManager.Log.Warning($"Outline '{layerName}': chain does not close (gap = {gap:G4}).");
                MessageBox.Show(
                    $"The outline on layer '{layerName}' could not be converted to a polyline.\n\n" +
                    $"The chain does not close (gap = {gap:G4} units). " +
                    "Please check the model geometry and try again.",
                    "Polyline Conversion Failed", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            // Build the closed LwPolyline.
            var vertices = chain.Select(c => new DxfLwPolylineVertex { X = c.pt.X, Y = c.pt.Y, Bulge = c.bulge });
            var poly = new DxfLwPolyline(vertices) { Layer = layerName, IsClosed = true };

            // Replace original entities with the polyline (insert first so it precedes bend lines).
            // Deduplicate sources: a tessellated spline produces many segments sharing one entity.
            foreach (var source in segments.Select(s => s.Source).Distinct())
                file.Entities.Remove(source);
            file.Entities.Insert(0, poly);

            LogManager.Log.Information(
                $"Outline '{layerName}' merged into a closed LwPolyline ({chain.Count} vertices, " +
                $"{lines.Count} lines + {arcs.Count} arcs).");
            return true;
        }

        private static string colorToRgb(System.Drawing.Color color)
        {
            return String.Format("{0};{1};{2}", color.R, color.G, color.B);
        }

        public bool ExportFlatDXF(PartDocument oDoc)
        {
            LogManager.Log.Information("------ Begin DXF export ------");
            LogManager.Log.Information($"CAD Model Name: {oDoc.DisplayName}");

            // ExportDirectory may prompt user; if they cancel, abort silently
            if (ExportDirectory == null) return false;

            // Overwrite check
            if (Properties.DxfSettings.Default.PromptBeforeOverwrite && System.IO.File.Exists(ExportFullPath))
            {
                var answer = MessageBox.Show(
                    $"A DXF file already exists at:\n{ExportFullPath}\n\nOverwrite it?",
                    "File Already Exists",
                    MessageBoxButtons.YesNoCancel,
                    MessageBoxIcon.Warning);

                if (answer == DialogResult.Cancel)
                    return false;

                if (answer == DialogResult.No)
                {
                    using var saveAs = new SaveFileDialog
                    {
                        Title = "Save DXF As",
                        Filter = "DXF Files (*.dxf)|*.dxf",
                        FileName = ExportFilename,
                        DefaultExt = "dxf",
                        InitialDirectory = ExportDirectory
                    };
                    if (saveAs.ShowDialog() != DialogResult.OK)
                        return false;

                    this.exportFileName  = System.IO.Path.GetFileName(saveAs.FileName);
                    this.exportDirectory = System.IO.Path.GetDirectoryName(saveAs.FileName);
                }
            }

            LogManager.Log.Information($"Export full path: {ExportFullPath}");

            SheetMetalComponentDefinition smCompDef;

            if (oDoc == null)
            {
                MessageBox.Show("Part document is null.", "Export error!");
                return false;
            }

            if (oDoc.DocumentType == DocumentTypeEnum.kPartDocumentObject)
            {
                Console.WriteLine("Is Inventor Part document");

                if (oDoc.SubType == "{9C464203-9BAE-11D3-8BAD-0060B0CE6BB4}")
                {
                    Console.WriteLine("Is sheet metal component!");

                    // we MUST class as a sheet metal comp def
                    smCompDef = (SheetMetalComponentDefinition)oDoc.ComponentDefinition;
                }
                else
                {
                    MessageBox.Show("The selected item is not a valid Sheetmetal component.", "Export Error!");
                    return false;
                }

                // Unfold if we don't already have a flat pattern
                if (!smCompDef.HasFlatPattern)
                {
                    try
                    {
                        smCompDef.Unfold();
                    }
                    catch
                    {
                        MessageBox.Show("Inventor encountered an error while unfolding the part.\n"
                                      + "Please correct the issue with the model and try again.", "Unfold error!");
                        return false;
                    }
                }

                // set export length units
                UnitsOfMeasure originalUnits = oDoc.UnitsOfMeasure;
                oDoc.UnitsOfMeasure.LengthUnits = UnitsTypeEnum.kInchLengthUnits;

                FlatPattern fPatt = smCompDef.FlatPattern;

                string filePath = System.IO.Path.Combine(LogManager.addinPath, "config/export.txt");

                ExportConfigManager exportConfigManager = new();

                foreach (LineTypeEnum lineType in Enum.GetValues(typeof(LineTypeEnum)))
                {
                    try
                    {
                        string key = String.Format("LineTypeEnum.{0}", Enum.GetName(typeof(LineTypeEnum), lineType));
                        int value = (int)lineType;

                        LogManager.Log.Debug($"Key: {key}, Value: {value}");
                        exportConfigManager.AddValue(key, value.ToString());
                    }
                    catch { 
                    
                    }
                }

                exportConfigManager.LoadConfigFile(filePath);


                string sOut = "FLAT PATTERN DXF?AcadVersion=2007"
                            // Outer Profile Layer
                            + "&OuterProfileLayer=" + Properties.DxfSettings.Default.OuterProfileLayer
                            + "&OuterProfileLayerColor=" + colorToRgb(Properties.DxfSettings.Default.OuterProfileLayerColor)
                            + "&OuterProfileLineType=" + ((decimal)Properties.DxfSettings.Default.OuterProfileLineType)

                            // Interior Profile Layer
                            + "&InteriorProfilesLayer=" + Properties.DxfSettings.Default.InteriorProfilesLayer
                            + "&InteriorProfilesLayerColor=" + colorToRgb(Properties.DxfSettings.Default.InteriorProfilesLayerColor)
                            + "&InteriorProfilesLineType=" + ((decimal)Properties.DxfSettings.Default.InteriorProfilesLineType)

                            // Bend Up Layer
                            //+ "&BendUpLayer=not_used"
                            //+ "&BendUpLayerColor=0;0;255"
                            //+ "&BendUpLineType=" + ((decimal)LineTypeEnum.kDashedLineType)

                            // Bend Down Layer
                            //+ "&BendDownLayer=not_used"
                            //+ "&BendDownLayerColor=255;0;191"
                            //+ "&BendDownLineType=" + ((decimal)LineTypeEnum.kDashedLineType)

                            // Layers to hide
                            + "&InvisibleLayers=IV_TANGENT;IV_ARC_CENTERS;IV_BEND;IV_BEND_DOWN;IV_UNCONSUMED_SKETCHES;not_used"

                            // Other Export Settings
                            //+ "&MergeProfilesIntoPolyline=True"
                            //+ "&SimplifySplines=True"
                            //+ "&AdvancedLegacyExport=True"
                            //+ "&SplineTolerance=0.01"
                            ;

                DataIO oDataIO = oDoc.ComponentDefinition.DataIO;
                oDataIO.WriteDataToFile(sOut, ExportFullPath);

                LogManager.Log.Information($"Inventor export complete, adding bend lines");

                UnitsOfMeasure docUnits = oDoc.UnitsOfMeasure;
                string uString = docUnits.GetStringFromType(docUnits.LengthUnits);


                // read DXF and add bend lines
                var file = DxfFile.Load(ExportFullPath);
                file.Header.Version = DxfAcadVersion.R2007;
                if (!ValidateAndRepairOutline(file, Properties.DxfSettings.Default.OuterProfileLayer)) return false;
                if (!MergeOutlineToPolyline(file, Properties.DxfSettings.Default.OuterProfileLayer)) return false;

                // Snapshot interior entities now — before XData / bend layers are added — for bend line trimming.
                var _outerPolyline = file.Entities
                    .OfType<DxfLwPolyline>()
                    .FirstOrDefault(p => p.Layer == Properties.DxfSettings.Default.OuterProfileLayer);
                var _interiorEntities = (IReadOnlyList<DxfEntity>)file.Entities
                    .Where(e => e.Layer == Properties.DxfSettings.Default.InteriorProfilesLayer)
                    .ToList();
                file.ApplicationIds.Add(new DxfAppId("POS3000_V3_PRODUCT"));
                file.ApplicationIds.Add(new DxfAppId("POS3000_V3_BENDINGLINE"));

                // convert from internal units (CM) to document units
                double toDocUnits(double value)
                {
                    return docUnits.ConvertUnits(value, UnitsTypeEnum.kCentimeterLengthUnits, docUnits.LengthUnits);
                }

                // get sheet thickness from model
                var sheetThickness = toDocUnits((double)smCompDef.Thickness.Value);
                var materialName = smCompDef.Material.Name;

                // ToDo: Find a better way to get the Outline layer
                foreach (DxfLayer layer in file.Layers)
                {

                    if (layer.Name == Properties.DxfSettings.Default.OuterProfileLayer)
                    {
                        // add XData with product info
                        layer.XData["POS3000_V3_PRODUCT"] = new DxfXDataApplicationItemCollection
                        (
                            new DxfXDataString(String.Format("Thickness={0:F4}", sheetThickness)),
                            new DxfXDataString(String.Format("MaterialId=({0})", materialName))
                        );
                    }
                }

                bool bendDownEnabled = Properties.DxfSettings.Default.BendDownEnabled;
                var bUpLayer = Properties.DxfSettings.Default.BendUpLayer;
                var bDownLayer = bendDownEnabled
                    ? Properties.DxfSettings.Default.BendDownLayer
                    : bUpLayer;

                string lineTypeToDxfName(LineTypeEnum lt) =>
                    Custom_Controls.LineTypeComboBox.Styles
                        .FirstOrDefault(s => s.LineType == lt)?.DxfName ?? "CONTINUOUS";

                var bUpLineType = lineTypeToDxfName(Properties.DxfSettings.Default.BendUpLineType);
                var bDownLineType = bendDownEnabled
                    ? lineTypeToDxfName(Properties.DxfSettings.Default.BendDownLineType)
                    : bUpLineType;

                // Ensure a line type is defined in the DXF file, adding it if missing
                void ensureLineType(string dxfName)
                {
                    if (file.LineTypes.Any(lt => lt.Name == dxfName)) return;
                    var style = Custom_Controls.LineTypeComboBox.Styles.FirstOrDefault(s => s.DxfName == dxfName);
                    if (style == null) return;
                    var lt2 = new DxfLineType(dxfName);
                    foreach (var len in style.DxfElements)
                        lt2.Elements.Add(new DxfLineTypeElement { DashDotSpaceLength = len });
                    file.LineTypes.Add(lt2);
                }

                // layerCustomColors tracks layers that use a custom (non-ACI) color,
                // so entities on those layers get Color24Bit set explicitly.
                var layerCustomColors = new Dictionary<string, System.Drawing.Color>();

                void ensureLayer(string layerName, string lineTypeName, System.Drawing.Color color)
                {
                    ensureLineType(lineTypeName);
                    var layer = file.Layers.FirstOrDefault(l => l.Name == layerName);
                    if (layer == null)
                    {
                        layer = new DxfLayer(layerName);
                        file.Layers.Add(layer);
                    }
                    layer.LineTypeName = lineTypeName;
                    var aci = Custom_Controls.ColorComboBox.GetAciIndex(color);
                    if (aci.HasValue)
                        layer.Color = DxfColor.FromIndex(aci.Value);
                    else
                        layerCustomColors[layerName] = color; // entity-level 24-bit color applied per entity
                }

                var upColor = Properties.DxfSettings.Default.BendUpLayerColor;
                var downColor = bendDownEnabled
                    ? Properties.DxfSettings.Default.BendDownLayerColor
                    : upColor;

                ensureLayer(bUpLayer, bUpLineType, upColor);
                ensureLayer(bDownLayer, bDownLineType, downColor);
                ensureLineType("DIVIDE"); // always available for hems

                foreach (FlatBendResult oBend in fPatt.FlatBendResults)
                {
                    // we only want bends on the top face, so continue if on bottom face
                    if (oBend.IsOnBottomFace) continue;

                    var bAngle = oBend.Angle * 180 / Math.PI;
                    var bRadius = toDocUnits(oBend.InnerRadius);

                    var bLayer = bUpLayer;

                    // down bends have a negative bend angle and optionally a separate layer
                    if (oBend.IsDirectionUp)
                    {
                        bAngle *= -1.0;
                        bLayer = bDownLayer;
                    }

                    Inventor.Point startPoint = oBend.Edge.StartVertex.Point;
                    Inventor.Point stopPoint = oBend.Edge.StopVertex.Point;

                    // bend start and end points
                    var x1 = toDocUnits(startPoint.X);
                    var y1 = toDocUnits(startPoint.Y);
                    var x2 = toDocUnits(stopPoint.X);
                    var y2 = toDocUnits(stopPoint.Y);

                    var bendXData = new DxfXDataApplicationItemCollection(
                        new DxfXDataString(String.Format("BendAngleDeg={0:F3}", bAngle)),
                        new DxfXDataString(String.Format("InnerRadius={0:F4}", bRadius)),
                        new DxfXDataString(String.Format("K_Factor={0:F3}", oBend.kFactor))
                    );

                    // Trim bend line against the outer profile and any cutouts.
                    // Setback is applied at every boundary crossing (outer and interior).
                    var setback = Properties.DxfSettings.Default.BendLineSetback;
                    var segments = _outerPolyline != null
                        ? BendLineTrimmer.Trim(
                            new DxfPoint(x1, y1, 0), new DxfPoint(x2, y2, 0),
                            _outerPolyline, _interiorEntities, setback)
                        : new System.Collections.Generic.List<(DxfPoint, DxfPoint)>
                          { (new DxfPoint(x1, y1, 0), new DxfPoint(x2, y2, 0)) };

                    foreach (var (segStart, segEnd) in segments)
                    {
                        var bLine = new DxfLine(segStart, segEnd);
                        bLine.Layer = bLayer;
                        // custom colors can't be set on the layer (DxfLayer only supports ACI), so set per-entity
                        if (layerCustomColors.TryGetValue(bLayer, out var customColor))
                            bLine.Color24Bit = customColor.ToArgb() & 0xFFFFFF;
                        // hems override the layer line type with DIVIDE
                        if (Math.Abs(bAngle) > 179)
                            bLine.LineTypeName = "DIVIDE";
                        bLine.XData["POS3000_V3_BENDINGLINE"] = bendXData;
                        file.Entities.Add(bLine);
                    }
                }

                file.Save(ExportFullPath);

                // restore original units of measure
                oDoc.UnitsOfMeasure.LengthUnits = originalUnits.LengthUnits;

                LogManager.Log.Information($"Bend lines added, export complete.");

                return true;
            }

            return false;
        }
    }

    internal static class OutlineHelper
    {
        internal struct Seg
        {
            internal DxfPoint Start, End;
            internal double   Bulge;
            internal DxfEntity Source;
            internal Seg(DxfPoint start, DxfPoint end, double bulge, DxfEntity source)
            { Start = start; End = end; Bulge = bulge; Source = source; }
        }

        internal static double Dist(DxfPoint a, DxfPoint b)
        {
            double dx = a.X - b.X, dy = a.Y - b.Y, dz = a.Z - b.Z;
            return Math.Sqrt(dx * dx + dy * dy + dz * dz);
        }

        internal static DxfPoint Midpt(DxfPoint a, DxfPoint b)
            => new DxfPoint((a.X + b.X) / 2, (a.Y + b.Y) / 2, (a.Z + b.Z) / 2);

        internal static DxfPoint ArcStart(DxfArc arc)
        {
            double rad = arc.StartAngle * Math.PI / 180;
            return new DxfPoint(
                arc.Center.X + arc.Radius * Math.Cos(rad),
                arc.Center.Y + arc.Radius * Math.Sin(rad),
                arc.Center.Z);
        }

        internal static DxfPoint ArcEnd(DxfArc arc)
        {
            double rad = arc.EndAngle * Math.PI / 180;
            return new DxfPoint(
                arc.Center.X + arc.Radius * Math.Cos(rad),
                arc.Center.Y + arc.Radius * Math.Sin(rad),
                arc.Center.Z);
        }

        /// <summary>
        /// Evaluates a B-spline at normalised parameter t ∈ [0, 1] using the de Boor algorithm.
        /// Supports any degree and both clamped and unclamped knot vectors.
        /// </summary>
        internal static DxfPoint EvaluateBSpline(DxfSpline spline, double t)
        {
            var knots = spline.KnotValues.ToList();
            var ctrl  = spline.ControlPoints.Select(cp => cp.Point).ToList();
            int p = spline.DegreeOfCurve;
            int n = ctrl.Count - 1;

            // Map t ∈ [0,1] onto the active knot range [u_min, u_max].
            double uMin = knots[p];
            double uMax = knots[n + 1];
            double u = uMin + t * (uMax - uMin);
            u = Math.Max(uMin, Math.Min(uMax - 1e-10, u));

            // Find the knot span index k such that knots[k] ≤ u < knots[k+1].
            int k = p;
            for (int i = p; i <= n; i++)
                if (u >= knots[i] && u < knots[i + 1]) { k = i; break; }

            // Initialise de Boor points d[0..p] = P[k-p .. k].
            var d = Enumerable.Range(0, p + 1)
                .Select(j => ctrl[k - p + j])
                .ToList();

            // De Boor recursion.
            for (int r = 1; r <= p; r++)
                for (int j = p; j >= r; j--)
                {
                    int   ki    = k - p + j;
                    double denom = knots[ki + p - r + 1] - knots[ki];
                    double alpha = denom < 1e-14 ? 0.0 : (u - knots[ki]) / denom;
                    d[j] = new DxfPoint(
                        (1 - alpha) * d[j - 1].X + alpha * d[j].X,
                        (1 - alpha) * d[j - 1].Y + alpha * d[j].Y,
                        (1 - alpha) * d[j - 1].Z + alpha * d[j].Z);
                }

            return d[p];
        }
    }

    public class ExportConfigManager
    {
        public string Template { get; set; }
        public Dictionary<string, string> Values { get; private set; } = new();

        public ExportConfigManager AddValue(string key, string value)
        {
            Values[key] = value;
            return this;
        }


        public string LoadConfigFile(string configFile)
        {
            try
            {
                //var lines = System.IO.File.ReadAllLines(configFile)
                //                .Where(line => !string.IsNullOrWhiteSpace(line) && !line.TrimStart().StartsWith("#"));


                //var lines = System.IO.File.ReadAllLines(configFile)
                //    .Select(line => line.Trim()) // Remove leading/trailing whitespace including newlines
                //    .Where(line => !string.IsNullOrWhiteSpace(line) && !line.StartsWith("#"))
                //    .ToList();

                var lines = System.IO.File.ReadAllLines(configFile);


                StringBuilder result = new StringBuilder();
                for (int i = 0; i < lines.Count(); i++)
                {
                    result.AppendLine($"{i + 1}: {lines[i]}");
                }
                LogManager.Log.Information($"\nRaw Config file:\n********\n{result}\n********");


                for (int i = 0; i < lines.Length; i++)
                {
                    string line = lines[i];
                    int lineNumber = i + 1;

                    if (line.Trim().StartsWith("#")) continue;  // Ignore comment lines
                    if (line.Trim() is "") continue;            // Ignore blank lines

                    try
                    {
                        LogManager.Log.Information($"Config Line {i+1}: {line}");

                        string renderedLine =
                            Regex.Replace(line, @"{{\s*(\w+)\s*}}", match =>
                            {
                                var key = match.Groups[1].Value;
                                if (Values != null && Values.TryGetValue(key, out var value)) return value;
                                else
                                {
                                    LogManager.Log.Error($"No value found for key '{key}' in config file line {i + 1}");
                                    return "<MISSING KEY>";
                                }
                            });

                        lines[i] = renderedLine;

                    }
                    catch (Exception ex)
                    {
                        {
                            LogManager.Log.Error($"Error in config file on line {i+1}: {ex.Message}\n{ex.StackTrace}");
                        }
                    }

                    //string inventorString = Render(string.Join("&", lines));
                    string inventorString = string.Join("&", lines);

                    LogManager.Log.Information($"Rendered inventor command:\n{inventorString}");
                    return inventorString;
                }
                return "";

            }
            catch (Exception ex)
            {
                LogManager.Log.Error($"Error processing config file: {ex.Message}\n{ex.StackTrace}");
                MessageBox.Show($"Error processing config file: {ex.Message}", "Config Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return "";
            }
        } 


        public string Render(string template)
        {
            return Regex.Replace(template, @"{{\s*(\w+)\s*}}", match =>
            {

                var key = match.Groups[1].Value;

                LogManager.Log.Error($"Key: {key}");

                if (Values != null && Values.TryGetValue(key, out var value))
                {
                    return value;
                }
                else
                {
                    return "<MISSING KEY>";
                }

            });
        }
    }
}
