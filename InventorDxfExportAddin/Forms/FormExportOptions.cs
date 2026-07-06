using System.Windows.Forms;

namespace InventorDxfExportAddin.Forms
{
    public partial class FormExportOptions : Form
    {
        public FormExportOptions()
        {
            InitializeComponent();
            LoadSettings();
        }

        private void LoadSettings()
        {
            var settings = Properties.DxfSettings.Default;

            rbNextToSource.Checked = settings.ExportMode == "NextToSourceFile";
            rbCustomDir.Checked    = settings.ExportMode == "CustomDirectory";
            tbExportDir.Text       = settings.ExportDirectory ?? "";
            cbPromptOverwrite.Checked = settings.PromptBeforeOverwrite;

            UpdateDirControls();
        }

        private void UpdateDirControls()
        {
            tbExportDir.Enabled  = rbCustomDir.Checked;
            btnBrowse.Enabled    = rbCustomDir.Checked;
        }

        private void rbNextToSource_CheckedChanged(object sender, System.EventArgs e) => UpdateDirControls();
        private void rbCustomDir_CheckedChanged(object sender, System.EventArgs e)    => UpdateDirControls();

        private void btnBrowse_Click(object sender, System.EventArgs e)
        {
            using var dlg = new FolderBrowserDialog
            {
                Description = "Select DXF export directory",
                SelectedPath = tbExportDir.Text
            };
            if (dlg.ShowDialog() == DialogResult.OK)
                tbExportDir.Text = dlg.SelectedPath;
        }

        private void btnSave_Click(object sender, System.EventArgs e)
        {
            if (rbCustomDir.Checked && string.IsNullOrWhiteSpace(tbExportDir.Text))
            {
                MessageBox.Show("Please choose an export directory, or select \"Save next to source file\".",
                    "Export Options", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var settings = Properties.DxfSettings.Default;
            settings.ExportMode           = rbCustomDir.Checked ? "CustomDirectory" : "NextToSourceFile";
            settings.ExportDirectory      = tbExportDir.Text.Trim();
            settings.PromptBeforeOverwrite = cbPromptOverwrite.Checked;
            settings.Save();

            LogManager.Log.Information(
                $"Export options saved: mode={settings.ExportMode}, dir={settings.ExportDirectory}, promptOverwrite={settings.PromptBeforeOverwrite}");

            this.Close();
        }

        private void btnCancel_Click(object sender, System.EventArgs e) => this.Close();
    }
}
