using Inventor;
using System.Windows.Forms;

namespace InventorDxfExportAddin.Forms
{
    public partial class FormExportOptions : Form
    {
        private readonly Document _doc;

        public FormExportOptions(Document doc = null)
        {
            _doc = doc;
            InitializeComponent();
            LoadSettings();
        }

        private void LoadSettings()
        {
            var s = Properties.DxfSettings.Default;

            rbNextToSource.Checked  = s.ExportMode == "NextToSourceFile";
            rbCustomDir.Checked     = s.ExportMode == "CustomDirectory";
            rbTemplatePath.Checked  = s.ExportMode == "TemplatePath";

            // If none matched (e.g. fresh install), default to NextToSource
            if (!rbNextToSource.Checked && !rbCustomDir.Checked && !rbTemplatePath.Checked)
                rbNextToSource.Checked = true;

            tbExportDir.Text          = s.ExportDirectory ?? "";
            tbTemplateBaseDir.Text    = s.TemplateBaseDirectory ?? "";
            tbSubfolderTemplate.Text  = s.SubfolderTemplate ?? "";
            tbFilenameTemplate.Text   = s.FilenameTemplate ?? "";
            cbPromptOverwrite.Checked = s.PromptBeforeOverwrite;

            tbTemplateBaseDir.TextChanged   += (_, __) => UpdatePreview();
            tbSubfolderTemplate.TextChanged += (_, __) => UpdatePreview();
            tbFilenameTemplate.TextChanged  += (_, __) => UpdatePreview();

            UpdateControls();
        }

        private void UpdateControls()
        {
            bool fixedDir   = rbCustomDir.Checked;
            bool templateOn = rbTemplatePath.Checked;

            tbExportDir.Enabled         = fixedDir;
            btnBrowse.Enabled           = fixedDir;

            lblBaseDir.Enabled          = templateOn;
            tbTemplateBaseDir.Enabled   = templateOn;
            btnBrowseTemplate.Enabled   = templateOn;
            lblSubfolder.Enabled        = templateOn;
            tbSubfolderTemplate.Enabled = templateOn;
            btnPickSubfolder.Enabled    = templateOn;
            lblFilename.Enabled         = templateOn;
            tbFilenameTemplate.Enabled  = templateOn;
            btnPickFilename.Enabled     = templateOn;
            lblTokens.Enabled           = templateOn;
            lblPreview.Visible          = templateOn;
            tbPreview.Visible           = templateOn;

            UpdatePreview();
        }

        private void rbMode_CheckedChanged(object sender, System.EventArgs e) => UpdateControls();

        private void btnBrowse_Click(object sender, System.EventArgs e)
        {
            using var dlg = new FolderBrowserDialog
            {
                Description = "Select DXF export directory",
                SelectedPath = tbExportDir.Text
            };
            if (dlg.ShowDialog(this) == DialogResult.OK)
                tbExportDir.Text = dlg.SelectedPath;
        }

        private void btnBrowseTemplate_Click(object sender, System.EventArgs e)
        {
            using var dlg = new FolderBrowserDialog
            {
                Description = "Select base directory for template output",
                SelectedPath = tbTemplateBaseDir.Text
            };
            if (dlg.ShowDialog(this) == DialogResult.OK)
                tbTemplateBaseDir.Text = dlg.SelectedPath;
        }

        private void btnSave_Click(object sender, System.EventArgs e)
        {
            if (rbCustomDir.Checked && string.IsNullOrWhiteSpace(tbExportDir.Text))
            {
                MessageBox.Show(this, "Please choose an export directory, or select a different output mode.",
                    "Export Options", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var s = Properties.DxfSettings.Default;

            if (rbTemplatePath.Checked)
                s.ExportMode = "TemplatePath";
            else if (rbCustomDir.Checked)
                s.ExportMode = "CustomDirectory";
            else
                s.ExportMode = "NextToSourceFile";

            s.ExportDirectory       = tbExportDir.Text.Trim();
            s.TemplateBaseDirectory = tbTemplateBaseDir.Text.Trim();
            s.SubfolderTemplate     = tbSubfolderTemplate.Text.Trim();
            s.FilenameTemplate      = tbFilenameTemplate.Text.Trim();
            s.PromptBeforeOverwrite = cbPromptOverwrite.Checked;
            s.Save();

            LogManager.Log.Information(
                $"Export options saved: mode={s.ExportMode}, templateBase={s.TemplateBaseDirectory}, " +
                $"subfolderTemplate={s.SubfolderTemplate}, filenameTemplate={s.FilenameTemplate}");

            this.Close();
        }

        private void btnCancel_Click(object sender, System.EventArgs e) => this.Close();

        private void UpdatePreview()
        {
            if (_doc == null || !rbTemplatePath.Checked)
            {
                tbPreview.Text = "";
                return;
            }

            try
            {
                string baseDir = tbTemplateBaseDir.Text.Trim();
                if (string.IsNullOrEmpty(baseDir))
                    baseDir = System.IO.Path.GetDirectoryName(_doc.FullFileName) ?? "";

                string subfolder = TemplateHelper.Expand(tbSubfolderTemplate.Text, _doc, sanitize: true);
                string filename  = TemplateHelper.Expand(tbFilenameTemplate.Text,  _doc, sanitize: true);

                if (string.IsNullOrEmpty(filename))
                    filename = System.IO.Path.GetFileNameWithoutExtension(_doc.FullFileName);

                string preview = string.IsNullOrEmpty(subfolder)
                    ? System.IO.Path.Combine(baseDir, filename + ".dxf")
                    : System.IO.Path.Combine(baseDir, subfolder, filename + ".dxf");

                tbPreview.Text = preview;
            }
            catch
            {
                tbPreview.Text = "";
            }
        }

        private void btnPickSubfolder_Click(object sender, System.EventArgs e)
            => InsertToken(tbSubfolderTemplate);

        private void btnPickFilename_Click(object sender, System.EventArgs e)
            => InsertToken(tbFilenameTemplate);

        private void InsertToken(System.Windows.Forms.TextBox target)
        {
            using var picker = new FormTokenPicker(_doc);
            if (picker.ShowDialog(this) != System.Windows.Forms.DialogResult.OK) return;
            int pos = target.SelectionStart;
            target.Text = target.Text.Remove(pos, target.SelectionLength).Insert(pos, picker.SelectedToken);
            target.SelectionStart = pos + picker.SelectedToken.Length;
            target.Focus();
        }
    }
}
