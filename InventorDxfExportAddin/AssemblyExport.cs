using Inventor;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using Path = System.IO.Path;

namespace InventorDxfExportAddin
{
    /// <summary>
    /// A unique sheet metal part found in an assembly, with its total occurrence count.
    /// </summary>
    public class SheetMetalPart
    {
        public string       FullFileName { get; internal set; }
        public PartDocument Document     { get; internal set; }
        public int          Quantity     { get; internal set; }
        public string       PartNumber   { get; internal set; }
        public string       Material     { get; internal set; }
        public string       Thickness    { get; internal set; }
        /// <summary>Non-null when the part file could not be inspected (not loaded, access error, etc.).</summary>
        public string       LoadError    { get; internal set; }

        public bool HasError => LoadError != null;
    }

    internal static class AssemblyTraversal
    {
        private const string SheetMetalSubType = "{9C464203-9BAE-11D3-8BAD-0060B0CE6BB4}";

        /// <summary>
        /// Walks the full assembly tree and returns one <see cref="SheetMetalPart"/> per unique
        /// sheet metal part file. Suppressed occurrences are skipped. Non-sheet-metal parts are
        /// ignored. Quantity reflects the total count of non-suppressed occurrences at all levels.
        /// Results are sorted by PartNumber (falling back to filename).
        /// </summary>
        public static List<SheetMetalPart> FindSheetMetalParts(AssemblyDocument asmDoc)
        {
            var seen = new Dictionary<string, SheetMetalPart>(StringComparer.OrdinalIgnoreCase);

            Traverse(asmDoc.ComponentDefinition.Occurrences, seen);

            var result = new List<SheetMetalPart>(seen.Values);
            result.Sort((a, b) =>
            {
                string ka = !string.IsNullOrEmpty(a.PartNumber) ? a.PartNumber : Path.GetFileName(a.FullFileName);
                string kb = !string.IsNullOrEmpty(b.PartNumber) ? b.PartNumber : Path.GetFileName(b.FullFileName);
                return string.Compare(ka, kb, StringComparison.OrdinalIgnoreCase);
            });
            return result;
        }

        // Accepts both ComponentOccurrences (top-level) and ComponentOccurrencesEnumerator (sub-assemblies).
        private static void Traverse(IEnumerable occurrences,
                                     Dictionary<string, SheetMetalPart> seen)
        {
            foreach (ComponentOccurrence occ in occurrences)
            {
                if (occ.Suppressed) continue;

                if (occ.DefinitionDocumentType == DocumentTypeEnum.kAssemblyDocumentObject)
                {
                    Traverse(occ.SubOccurrences, seen);
                    continue;
                }

                if (occ.DefinitionDocumentType != DocumentTypeEnum.kPartDocumentObject)
                    continue;

                string path;
                try   { path = occ.ReferencedDocumentDescriptor.FullDocumentName; }
                catch { continue; }

                if (string.IsNullOrEmpty(path)) continue;

                // Already seen — just increment quantity.
                if (seen.TryGetValue(path, out var existing))
                {
                    existing.Quantity++;
                    continue;
                }

                var part = new SheetMetalPart { FullFileName = path, Quantity = 1 };

                try
                {
                    var doc = occ.ReferencedDocumentDescriptor.ReferencedDocument as PartDocument;

                    if (doc == null)
                    {
                        part.LoadError = "Document could not be opened or is not a Part.";
                        seen[path] = part;
                        LogManager.Log.Warning($"Assembly traversal: could not access '{path}'.");
                        continue;
                    }

                    // Skip non-sheet-metal parts entirely — they won't appear in the export list.
                    if (doc.SubType != SheetMetalSubType)
                        continue;

                    part.Document = doc;
                    ResolveProperties(part, doc);
                }
                catch (Exception ex)
                {
                    part.LoadError = ex.Message;
                    LogManager.Log.Warning($"Assembly traversal: error inspecting '{path}': {ex.Message}");
                    seen[path] = part;
                    continue;
                }

                seen[path] = part;
            }
        }

        private static void ResolveProperties(SheetMetalPart part, PartDocument doc)
        {
            var tokens = TemplateHelper.ResolveTokens((Document)doc);

            tokens.TryGetValue("PartNumber", out string pn);
            tokens.TryGetValue("Material",   out string mat);
            part.PartNumber = pn  ?? "";
            part.Material   = mat ?? "";

            try
            {
                var smDef = (SheetMetalComponentDefinition)doc.ComponentDefinition;
                double thickCm = smDef.Thickness.ModelValue;
                part.Thickness = TemplateHelper.FormatThickness(
                    thickCm, doc.UnitsOfMeasure, unit: "mm", precision: null);
            }
            catch
            {
                part.Thickness = "";
            }
        }
    }
}
