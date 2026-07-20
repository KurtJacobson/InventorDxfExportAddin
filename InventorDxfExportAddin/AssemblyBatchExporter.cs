using Inventor;
using InventorDxfExportAddin.DxfExport;
using System.Collections.Generic;

namespace InventorDxfExportAddin
{
    /// <summary>
    /// Runs the batch export loop over a list of sheet metal parts found by
    /// <see cref="AssemblyTraversal"/> and produces a results report.
    /// </summary>
    internal static class AssemblyBatchExporter
    {
        public static void Run(AssemblyDocument asmDoc,
                               List<SheetMetalPart> parts,
                               OverwritePolicy policy)
        {
            // TODO: implement export loop, progress dialog, and HTML report.
        }
    }
}
