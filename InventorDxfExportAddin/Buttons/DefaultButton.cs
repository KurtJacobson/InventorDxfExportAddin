using Inventor;
using Inventor.InternalNames.Ribbon;
using InventorDxfExportAddin.DxfExport;
using InventorDxfExportAddin.Forms;
using InventorDxfExportAddin.Properties;
using IxMilia.Dxf;
using IxMilia.Dxf.Entities;
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
                    bool success = export.ExportFlatDXF(partDoc);
                    if (success)
                        MessageBox.Show($"DXF successfully exported to:\n{export.ExportFullPath}",
                            "Export Complete", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
                new FormExportOptions().ShowDialog(InventorWindow.Instance);
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

}