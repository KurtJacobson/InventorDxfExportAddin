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
            this.cbOuterProfileLineColor.SelectedColor = settings.OuterProfileLayerColor;
            this.cbOuterProfileLineType.SelectedLineType = settings.OuterProfileLineType;

            this.textBox2.Text = settings.InteriorProfilesLayer;
            this.colorComboBox1.SelectedColor = settings.InteriorProfilesLayerColor;
            this.lineTypeComboBox2.SelectedLineType = settings.InteriorProfilesLineType;

            this.textBox3.Text = settings.BendUpLayer;
            this.colorComboBox2.SelectedColor = settings.BendUpLayerColor;
            this.lineTypeComboBox3.SelectedLineType = settings.BendUpLineType;

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
            settings.OuterProfileLayerColor = this.cbOuterProfileLineColor.SelectedColor;
            settings.OuterProfileLineType = this.cbOuterProfileLineType.SelectedLineType;

            settings.InteriorProfilesLayer = this.textBox2.Text;
            settings.InteriorProfilesLayerColor = this.colorComboBox1.SelectedColor;
            settings.InteriorProfilesLineType = this.lineTypeComboBox2.SelectedLineType;

            settings.BendUpLayer = this.textBox3.Text;
            settings.BendUpLayerColor = this.colorComboBox2.SelectedColor;
            settings.BendUpLineType = this.lineTypeComboBox3.SelectedLineType;


            // Save settings
            settings.Save();
            
            // Close the settings window
            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            // Close window without saving anything
            this.Close();
        }
    }
}
