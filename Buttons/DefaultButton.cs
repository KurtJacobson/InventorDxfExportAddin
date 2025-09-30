using Inventor;
using Inventor.InternalNames.Ribbon;
using System.Windows.Forms;
using static System.Windows.Forms.DataFormats;

namespace InventorDxfExportAddin.Buttons
{
    public class ExportDxfButton : InventorButton
    {
        protected override void Execute(NameValueMap context, Inventor.Application inventor)
        {
            MessageBox.Show($"Current document name: {inventor.ActiveDocument.DisplayName}");
        }

        protected override string RibbonName => InventorRibbons.Part;

        protected override string RibbonTabName => PartRibbonTabs.SheetMetal;

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
            //MessageBox.Show($"Current document name: {inventor.ActiveDocument.DisplayName}");
            Form DxfSettings = new FormDxfSettings();
            DxfSettings.ShowDialog();

        }

        protected override string RibbonName => InventorRibbons.Part;

        protected override string RibbonTabName => PartRibbonTabs.SheetMetal;

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
            MessageBox.Show($"Current document name: {inventor.ActiveDocument.DisplayName}");
        }

        protected override string RibbonName => InventorRibbons.Part;

        protected override string RibbonTabName => PartRibbonTabs.SheetMetal;

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

}