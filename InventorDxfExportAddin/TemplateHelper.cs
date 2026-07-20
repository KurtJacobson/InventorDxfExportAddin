using Inventor;
using System.Text.RegularExpressions;

namespace InventorDxfExportAddin
{
    /// <summary>
    /// Shared logic for expanding {Token} path templates from Inventor iProperties.
    /// Used by both the export engine and the Export Options preview/picker UI.
    /// </summary>
    internal static class TemplateHelper
    {
        /// <summary>
        /// Returns a dictionary of all simple token values (unsanitized) from the document.
        /// Keys match the token names without braces, e.g. "FileName", "PartNumber".
        /// Thickness variants are also included keyed by their full token string, e.g. "{Thickness:mm:2}".
        /// </summary>
        public static Dictionary<string, string> ResolveTokens(Document doc)
        {
            var tokens = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);

            try { tokens["FileName"] = System.IO.Path.GetFileNameWithoutExtension(doc.FullFileName); }
            catch { tokens["FileName"] = ""; }

            void TryProp(string setName, string propName, string key)
            {
                try { tokens[key] = doc.PropertySets[setName][propName].Value?.ToString() ?? ""; }
                catch { tokens[key] = ""; }
            }

            TryProp("Design Tracking Properties",   "Part Number",     "PartNumber");
            TryProp("Design Tracking Properties",   "Description",     "Description");
            TryProp("Design Tracking Properties",   "Material",        "Material");
            TryProp("Inventor Summary Information", "Revision Number", "RevisionNumber");

            // Thickness variants — only available for sheet metal parts
            try
            {
                var smDef = (SheetMetalComponentDefinition)((PartDocument)doc).ComponentDefinition;
                double thickCm = smDef.Thickness.ModelValue;
                UnitsOfMeasure uom = doc.UnitsOfMeasure;

                tokens["{Thickness}"]      = FormatThickness(thickCm, uom, null, null);
                tokens["{Thickness:mm:2}"] = FormatThickness(thickCm, uom, "mm", 2);
                tokens["{Thickness:in:3}"] = FormatThickness(thickCm, uom, "in", 3);
                tokens["{Thickness:in:4}"] = FormatThickness(thickCm, uom, "in", 4);
            }
            catch { }

            return tokens;
        }

        /// <summary>
        /// Expands a template string, substituting all recognised tokens with values from the document.
        /// When <paramref name="sanitize"/> is true, each value is cleaned so it is safe as a path segment.
        /// </summary>
        public static string Expand(string template, Document doc, bool sanitize = true)
        {
            if (string.IsNullOrWhiteSpace(template)) return "";

            var tokens = ResolveTokens(doc);

            // Thickness — regex handles the optional :unit:precision arguments
            double thicknessCm = double.NaN;
            try
            {
                var smDef = (SheetMetalComponentDefinition)((PartDocument)doc).ComponentDefinition;
                thicknessCm = smDef.Thickness.ModelValue;
            }
            catch { }

            string result = Regex.Replace(template,
                @"\{Thickness(?::([a-zA-Z]*))?(?::(\d+))?\}",
                m =>
                {
                    if (double.IsNaN(thicknessCm)) return "";
                    string unitArg      = m.Groups[1].Success ? m.Groups[1].Value : null;
                    int?   precisionArg = m.Groups[2].Success ? (int?)int.Parse(m.Groups[2].Value) : null;
                    string val          = FormatThickness(thicknessCm, doc.UnitsOfMeasure, unitArg, precisionArg);
                    return sanitize ? SanitizeSegment(val) : val;
                },
                RegexOptions.IgnoreCase);

            // Simple token substitution (everything except Thickness)
            foreach (var kvp in tokens)
            {
                if (kvp.Key.StartsWith("{")) continue; // skip the pre-keyed thickness variants
                string val = sanitize ? SanitizeSegment(kvp.Value) : kvp.Value;
                result = Regex.Replace(result,
                    Regex.Escape("{" + kvp.Key + "}"),
                    val.Replace("$", "$$"),
                    RegexOptions.IgnoreCase);
            }

            // Strip any tokens we couldn't resolve
            result = Regex.Replace(result, @"\{[^}]+\}", "");

            // Collapse doubled path separators left by empty tokens
            result = Regex.Replace(result, @"[\\/]{2,}", System.IO.Path.DirectorySeparatorChar.ToString());

            return result.Trim(System.IO.Path.DirectorySeparatorChar, ' ');
        }

        public static string FormatThickness(double thicknessCm, UnitsOfMeasure uom,
            string unit = null, int? precision = null)
        {
            try
            {
                UnitsTypeEnum targetUnit;
                string suffix;

                if (!string.IsNullOrEmpty(unit))
                {
                    switch (unit.ToLowerInvariant())
                    {
                        case "mm": targetUnit = UnitsTypeEnum.kMillimeterLengthUnits; suffix = "mm"; break;
                        case "cm": targetUnit = UnitsTypeEnum.kCentimeterLengthUnits; suffix = "cm"; break;
                        case "m":  targetUnit = UnitsTypeEnum.kMeterLengthUnits;      suffix = "m";  break;
                        case "in": targetUnit = UnitsTypeEnum.kInchLengthUnits;       suffix = "in"; break;
                        case "ft": targetUnit = UnitsTypeEnum.kFootLengthUnits;       suffix = "ft"; break;
                        default:   targetUnit = uom.LengthUnits; suffix = UnitSuffix(uom.LengthUnits); break;
                    }
                }
                else
                {
                    targetUnit = uom.LengthUnits;
                    suffix     = UnitSuffix(uom.LengthUnits);
                }

                double converted = uom.ConvertUnits(thicknessCm,
                    UnitsTypeEnum.kCentimeterLengthUnits, targetUnit);

                string num = precision.HasValue
                    ? converted.ToString("F" + precision.Value)
                    : converted.ToString("G6").TrimEnd('0').TrimEnd('.');

                return num + suffix;
            }
            catch
            {
                return thicknessCm.ToString("G4") + "cm";
            }
        }

        public static string UnitSuffix(UnitsTypeEnum unit)
        {
            switch (unit)
            {
                case UnitsTypeEnum.kMillimeterLengthUnits: return "mm";
                case UnitsTypeEnum.kCentimeterLengthUnits: return "cm";
                case UnitsTypeEnum.kMeterLengthUnits:      return "m";
                case UnitsTypeEnum.kInchLengthUnits:       return "in";
                case UnitsTypeEnum.kFootLengthUnits:       return "ft";
                default:                                   return "";
            }
        }

        public static string SanitizeSegment(string value)
        {
            if (string.IsNullOrEmpty(value)) return "";
            var invalid = System.IO.Path.GetInvalidFileNameChars();
            return new string(value.Select(c => invalid.Contains(c) ? '_' : c).ToArray()).Trim();
        }
    }
}
