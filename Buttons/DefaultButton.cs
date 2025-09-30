using System.Windows.Forms;
using Inventor;
using Inventor.InternalNames.Ribbon;

namespace InventorDxfExportAddin.Buttons
{
    public class DefaultButton : InventorButton
    {
        protected override void Execute(NameValueMap context, Inventor.Application inventor)
        {
            MessageBox.Show($"Current document name: {inventor.ActiveDocument.DisplayName}");
        }

        protected override string RibbonName => InventorRibbons.Part;

        protected override string RibbonTabName => PartRibbonTabs.SheetMetal;

        protected override string RibbonPanelName => "Schroeder DXF Export";

        protected override string Label => "Export\nDXF";

        protected override string Description => "Default Button Description";

        protected override string Tooltip => "Click the Default Button";

        protected override string LargeIconResourceName => "InventorDxfExportAddin.Buttons.Assets.Default-Light.png";

        protected override string DarkThemeLargeIconResourceName => "InventorDxfExportAddin.Buttons.Assets.Default-Dark.png";

        protected override string SmallIconResourceName => LargeIconResourceName;

        protected override string DarkThemeSmallIconResourceName => DarkThemeLargeIconResourceName;
    }
}