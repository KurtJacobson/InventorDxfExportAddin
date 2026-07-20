using System;
using System.Drawing;

namespace InventorDxfExportAddin
{
    /// <summary>
    /// ACI (AutoCAD Color Index) standard colors used by the DXF layer settings UI.
    /// Single source of truth shared by ColorComboBox and SettingsManager.
    /// </summary>
    internal static class DxfColors
    {
        public static readonly Color Red       = Color.FromArgb(255,   0,   0);  // ACI 1
        public static readonly Color Yellow    = Color.FromArgb(255, 255,   0);  // ACI 2
        public static readonly Color Green     = Color.FromArgb(  0, 255,   0);  // ACI 3
        public static readonly Color Cyan      = Color.FromArgb(  0, 255, 255);  // ACI 4
        public static readonly Color Blue      = Color.FromArgb(  0,   0, 255);  // ACI 5
        public static readonly Color Magenta   = Color.FromArgb(255,   0, 255);  // ACI 6
        public static readonly Color White     = Color.FromArgb(255, 255, 255);  // ACI 7
        public static readonly Color DarkGray  = Color.FromArgb(128, 128, 128);  // ACI 8
        public static readonly Color LightGray = Color.FromArgb(192, 192, 192);  // ACI 9

        private static readonly (string Name, Color Color)[] All =
        {
            ("Red",       Red),
            ("Yellow",    Yellow),
            ("Green",     Green),
            ("Cyan",      Cyan),
            ("Blue",      Blue),
            ("Magenta",   Magenta),
            ("White",     White),
            ("DarkGray",  DarkGray),
            ("LightGray", LightGray),
        };

        /// <summary>
        /// Resolves an ACI color name (e.g. "Green") to its Color.
        /// Returns true and sets <paramref name="color"/> on match.
        /// </summary>
        public static bool TryParse(string name, out Color color)
        {
            foreach (var (n, c) in All)
            {
                if (string.Equals(n, name?.Trim(), StringComparison.OrdinalIgnoreCase))
                {
                    color = c;
                    return true;
                }
            }
            color = default(Color);
            return false;
        }

        /// <summary>
        /// Returns the ACI name for a color if it matches one exactly, otherwise null.
        /// </summary>
        public static string NameOf(Color c)
        {
            int argb = c.ToArgb();
            foreach (var (n, col) in All)
                if (col.ToArgb() == argb) return n;
            return null;
        }

        /// <summary>Formats a color as "R, G, B".</summary>
        public static string ToRgbString(Color c) => $"{c.R}, {c.G}, {c.B}";
    }
}
