using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using InventorDxfExportAddin.Properties;
using InventorDxfExportAddin;


namespace InventorDxfExportAddin
{
    public partial class FormDxfSettings : Form
    {
        public FormDxfSettings()
        {
            InitializeComponent();

            // Make form window fixed size
            this.FormBorderStyle = FormBorderStyle.FixedDialog;

            var settings = DxfSettings.Default;

            // Initialize values
            this.tbOuterProfileLayer.Text = settings.OuterProfileLayer;
            this.cbOuterProfileLayerColor.SelectedColor = settings.OuterProfileLayerColor;
            this.cbOuterProfileLineType.SelectedLineType = settings.OuterProfileLineType;

            this.tbInnerProfilesLayer.Text = settings.InteriorProfilesLayer;
            this.cbInnerProfilesLayerColor.SelectedColor = settings.InteriorProfilesLayerColor;
            this.cbInnerProfilesLineType.SelectedLineType = settings.InteriorProfilesLineType;

            this.tbBendLineLayer.Text = settings.BendUpLayer;
            this.cbBendLinesLayerColor.SelectedColor = settings.BendUpLayerColor;
            this.cbBendLineType.SelectedLineType = settings.BendUpLineType;

            this.cbEnableBendLinesDown.Checked = settings.BendDownEnabled;
            ManageCheckGroupBox(cbEnableBendLinesDown, groupBox4);
            this.cbEnableBendLinesDown.CheckedChanged += (s, e) => ManageCheckGroupBox(cbEnableBendLinesDown, groupBox4);
            this.tbBendLinesDownLayer.Text = settings.BendDownLayer;
            this.cbBendLinesDownLayerColor.SelectedColor = settings.BendDownLayerColor;
            this.cbBendDownLineType.SelectedLineType = settings.BendDownLineType;
        }

        public object GetDxfSetting(string name)
        {
            // Get DXF settings
            return DxfSettings.Default[name];
        }

        public void SetDxfSetting(string name, object value)
        {
            // Set DXF settings
            Properties.Settings.Default[name] = value;
        }


        private void btnSave_Click(object sender, EventArgs e)
        {
            // Commit changes to the settings
            var settings = DxfSettings.Default;

            settings.OuterProfileLayer = this.tbOuterProfileLayer.Text;
            settings.OuterProfileLayerColor = this.cbOuterProfileLayerColor.SelectedColor;
            settings.OuterProfileLineType = this.cbOuterProfileLineType.SelectedLineType;

            settings.InteriorProfilesLayer = this.tbInnerProfilesLayer.Text;
            settings.InteriorProfilesLayerColor = this.cbInnerProfilesLayerColor.SelectedColor;
            settings.InteriorProfilesLineType = this.cbInnerProfilesLineType.SelectedLineType;

            settings.BendUpLayer = this.tbBendLineLayer.Text;
            settings.BendUpLayerColor = this.cbBendLinesLayerColor.SelectedColor;
            settings.BendUpLineType = this.cbBendLineType.SelectedLineType;

            settings.BendDownEnabled = this.cbEnableBendLinesDown.Checked;
            settings.BendDownLayer = this.tbBendLinesDownLayer.Text;
            settings.BendDownLayerColor = this.cbBendLinesDownLayerColor.SelectedColor;
            settings.BendDownLineType = this.cbBendDownLineType.SelectedLineType;

            // Save settings
            settings.Save();

            // Close the settings window
            this.Close();
        }

        private void ManageCheckGroupBox(CheckBox chk, GroupBox grp)
        {
            if (chk.Parent == grp)
            {
                grp.Parent.Controls.Add(chk);
                chk.Location = new Point(chk.Left + grp.Left, chk.Top + grp.Top);
                chk.BringToFront();
            }
            grp.Enabled = chk.Checked;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            // Close window without saving anything
            this.Close();
        }
    }
}
