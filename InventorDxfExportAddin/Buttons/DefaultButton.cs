using Inventor;
using Inventor.InternalNames.Ribbon;
using InventorDxfExportAddin.DxfExport;
using InventorDxfExportAddin.Forms;
using InventorDxfExportAddin.Properties;
using IxMilia.Dxf;
using IxMilia.Dxf.Entities;
using System.Collections.Generic;
using System.Diagnostics;
using System.Windows.Forms;
using static InventorDxfExportAddin.Custom_Controls.LineTypeComboBox;
using static System.Windows.Forms.DataFormats;

namespace InventorDxfExportAddin.Buttons
{

    public class ExportDxfButton : InventorButton
    {
        protected override void Execute(NameValueMap context, Inventor.Application inventor)
        {

            Document actDoc = inventor.ActiveDocument;
            if (actDoc == null) return;


            // check if active document is an inventor part
            if (actDoc.DocumentType == DocumentTypeEnum.kPartDocumentObject)
            {
                PartDocument partDoc = (PartDocument)actDoc;
                var export = new DxfExportEngine(partDoc);

                try
                {
                    ExportResult result = export.ExportFlatDXF(partDoc);
                    if (result.Success)
                        MessageBox.Show($"DXF successfully exported to:\n{result.OutputPath}",
                            "Export Complete", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    else if (!result.Skipped && result.ErrorMessage != null)
                        MessageBox.Show(result.ErrorMessage,
                            "Export Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                catch (Exception ex)
                {
                    Exception current = ex;
                    do
                    {
                        LogManager.Log.Error("{0}\n{1}", current.Message, current.StackTrace);
                    }
                    while ((current = current.InnerException) != null);

                    MessageBox.Show($"An error occurred while exporting the DXF.\nSee log for details.\n\n{ex.Message}",
                        "Export Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        protected override string RibbonName => InventorRibbons.Part;

        protected override string RibbonTabName => PartRibbonTabs.FlatPattern;

        protected override string RibbonPanelName => "Schröder DXF Export";

        protected override string Label => "Export\nDXF";

        protected override string Description => "Default Button Description";

        protected override string Tooltip => "Click the Default Button";

        protected override string LargeIconResourceName => "InventorDxfExportAddin.Buttons.Assets.Export-Light.png";

        protected override string DarkThemeLargeIconResourceName => "InventorDxfExportAddin.Buttons.Assets.Export-Light.png";

        protected override string SmallIconResourceName => LargeIconResourceName;

        protected override string DarkThemeSmallIconResourceName => DarkThemeLargeIconResourceName;
    }

    public class DxfSettingsButton : InventorButton
    {
        protected override void Execute(NameValueMap context, Inventor.Application inventor)
        {

            try
            {
                new FormDxfSettings().ShowDialog(InventorWindow.Instance);
            }
            catch (Exception ex)
            {
                LogManager.Log.Error("Exception opening DXF Settings");
                do
                {
                    LogManager.Log.Error("{0}\n{1}", ex.Message, ex.StackTrace);
                }
                while ((ex = ex.InnerException) != null);

                MessageBox.Show(ex.Message,
                    "Plugin Error - Exception opening DXF Settings!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        protected override string RibbonName => InventorRibbons.Part;

        protected override string RibbonTabName => PartRibbonTabs.FlatPattern;

        protected override string RibbonPanelName => "Schröder DXF Export";

        protected override string Label => "DXF Settings";

        protected override bool UseLargeIcon => false;

        protected override string Description => "Default Button Description";

        protected override string Tooltip => "Click the Default Button";

        protected override string LargeIconResourceName => "InventorDxfExportAddin.Buttons.Assets.Settings-Dark.png";

        protected override string DarkThemeLargeIconResourceName => "InventorDxfExportAddin.Buttons.Assets.Settings-Dark.png";

        protected override string SmallIconResourceName => LargeIconResourceName;

        protected override string DarkThemeSmallIconResourceName => DarkThemeLargeIconResourceName;
    }

    public class ExportOptionsButton : InventorButton
    {
        protected override void Execute(NameValueMap context, Inventor.Application inventor)
        {
            try
            {
                new FormExportOptions(inventor.ActiveDocument).ShowDialog(InventorWindow.Instance);
            }
            catch (Exception ex)
            {
                LogManager.Log.Error("Exception opening Export Options");
                do { LogManager.Log.Error("{0}\n{1}", ex.Message, ex.StackTrace); }
                while ((ex = ex.InnerException) != null);
                MessageBox.Show(ex.Message, "Export Options Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        protected override string RibbonName => InventorRibbons.Part;

        protected override string RibbonTabName => PartRibbonTabs.FlatPattern;

        protected override string RibbonPanelName => "Schröder DXF Export";

        protected override string Label => "Export Options";

        protected override bool UseLargeIcon => false;


        protected override string Description => "Default Button Description";

        protected override string Tooltip => "Click the Default Button";

        protected override string LargeIconResourceName => "InventorDxfExportAddin.Buttons.Assets.Settings-Dark.png";

        protected override string DarkThemeLargeIconResourceName => "InventorDxfExportAddin.Buttons.Assets.Settings-Dark.png";

        protected override string SmallIconResourceName => LargeIconResourceName;

        protected override string DarkThemeSmallIconResourceName => DarkThemeLargeIconResourceName;
    }

    public class AboutButton : InventorButton
    {
        protected override void Execute(NameValueMap context, Inventor.Application inventor)
        {
            try
            {
                new FormAbout().ShowDialog(InventorWindow.Instance);
            }
            catch (Exception ex)
            {
                LogManager.Log.Error("Exception opening About Dialog");
                do
                {
                    LogManager.Log.Error("{0}\n{1}", ex.Message, ex.StackTrace);
                }
                while ((ex = ex.InnerException) != null);

                MessageBox.Show(ex.Message,
                    "Plugin Error - Exception opening About Dialog!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        protected override string RibbonName => InventorRibbons.Part;

        protected override string RibbonTabName => PartRibbonTabs.FlatPattern;

        protected override string RibbonPanelName => "Schröder DXF Export";

        protected override string Label => "About Dialog";

        protected override bool UseLargeIcon => false;


        protected override string Description => "Default Button Description";

        protected override string Tooltip => "Click the Default Button";

        protected override string LargeIconResourceName => "InventorDxfExportAddin.Buttons.Assets.Settings-Dark.png";

        protected override string DarkThemeLargeIconResourceName => "InventorDxfExportAddin.Buttons.Assets.Settings-Dark.png";

        protected override string SmallIconResourceName => LargeIconResourceName;

        protected override string DarkThemeSmallIconResourceName => DarkThemeLargeIconResourceName;
    }

    public class ExportAssemblyDxfButton : InventorButton
    {
        protected override void Execute(NameValueMap context, Inventor.Application inventor)
        {
            var asmDoc = inventor.ActiveDocument as AssemblyDocument;
            if (asmDoc == null) return;

            // 1. Traverse the assembly to find all sheet metal parts.
            List<SheetMetalPart> parts;
            try
            {
                parts = AssemblyTraversal.FindSheetMetalParts(asmDoc);
            }
            catch (Exception ex)
            {
                LogManager.Log.Error($"Assembly traversal failed: {ex.Message}\n{ex.StackTrace}");
                MessageBox.Show($"Could not read the assembly structure.\n\n{ex.Message}",
                    "Assembly Export Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (parts.Count == 0)
            {
                MessageBox.Show("No sheet metal parts were found in the assembly.",
                    "Export Assembly DXFs", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            // 2. Ask once how to handle existing DXF files (if prompt is enabled).
            var policy = OverwritePolicy.OverwriteAll;
            if (SettingsManager.PromptBeforeOverwrite)
            {
                using (var dlg = new FormAssemblyExportOptions(parts.Count))
                {
                    if (dlg.ShowDialog(InventorWindow.Instance) != System.Windows.Forms.DialogResult.OK)
                        return;
                    policy = dlg.SelectedPolicy;
                }
            }

            // 3. Run the batch export and show the results report.
            AssemblyBatchExporter.Run(asmDoc, parts, policy);
        }

        protected override string RibbonName => InventorRibbons.Assembly;

        // Place on the Assemble tab — change to any other AssemblyRibbonTabs value if preferred.
        protected override string RibbonTabName => AssemblyRibbonTabs.Assemble;

        protected override string RibbonPanelName => "Schröder DXF Export";

        protected override string Label => "Export\nDXFs";

        protected override string Description => "Export flat-pattern DXFs for all sheet metal parts in the assembly.";

        protected override string Tooltip => "Export flat-pattern DXFs for all sheet metal parts in the assembly.";

        protected override string LargeIconResourceName => "InventorDxfExportAddin.Buttons.Assets.Export-Light.png";

        protected override string DarkThemeLargeIconResourceName => "InventorDxfExportAddin.Buttons.Assets.Export-Light.png";

        protected override string SmallIconResourceName => LargeIconResourceName;

        protected override string DarkThemeSmallIconResourceName => DarkThemeLargeIconResourceName;
    }

}