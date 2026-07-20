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
            var s = SettingsManager.Effective;

            rbNextToSource.Checked  = s.ExportMode == "NextToSourceFile";
            rbCustomDir.Checked     = s.ExportMode == "CustomDirectory";
            rbTemplatePath.Checked  = s.ExportMode == "TemplatePath";

            if (!rbNextToSource.Checked && !rbCustomDir.Checked && !rbTemplatePath.Checked)
                rbNextToSource.Checked = true;

            tbExportDir.Text          = s.ExportDirectory ?? "";
            tbTemplateBaseDir.Text    = s.TemplateBaseDirectory ?? "";
            tbSubfolderTemplate.Text  = s.SubfolderTemplate ?? "";
            tbFilenameTemplate.Text   = s.FilenameTemplate ?? "";
            cbPromptOverwrite.Checked = s.PromptBeforeOverwrite ?? true;

            tbTemplateBaseDir.TextChanged   += (_, __) => UpdatePreview();
            tbSubfolderTemplate.TextChanged += (_, __) => UpdatePreview();
            tbFilenameTemplate.TextChanged  += (_, __) => UpdatePreview();

            btnResetToGlobal.Visible = SettingsManager.HasGlobalConfig;

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
                Description  = "Select DXF export directory",
                SelectedPath = tbExportDir.Text
            };
            if (dlg.ShowDialog(this) == DialogResult.OK)
                tbExportDir.Text = dlg.SelectedPath;
        }

        private void btnBrowseTemplate_Click(object sender, System.EventArgs e)
        {
            using var dlg = new FolderBrowserDialog
            {
                Description  = "Select base directory for template output",
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

            string mode = rbTemplatePath.Checked ? "TemplatePath"
                        : rbCustomDir.Checked    ? "CustomDirectory"
                                                 : "NextToSourceFile";

            var form = new OrgSettings
            {
                ExportMode            = mode,
                ExportDirectory       = tbExportDir.Text.Trim(),
                TemplateBaseDirectory = tbTemplateBaseDir.Text.Trim(),
                SubfolderTemplate     = tbSubfolderTemplate.Text.Trim(),
                FilenameTemplate      = tbFilenameTemplate.Text.Trim(),
                PromptBeforeOverwrite = cbPromptOverwrite.Checked,
            };

            SettingsManager.SaveExportOptions(form);

            LogManager.Log.Information(
                $"Export options saved: mode={mode}, templateBase={form.TemplateBaseDirectory}, " +
                $"subfolderTemplate={form.SubfolderTemplate}, filenameTemplate={form.FilenameTemplate}");

            this.Close();
        }

        private void btnCancel_Click(object sender, System.EventArgs e) => this.Close();

        private void btnResetToGlobal_Click(object sender, System.EventArgs e)
        {
            var g = SettingsManager.Global;
            if (g == null) return;

            var mode = g.ExportMode ?? SettingsManager.Effective.ExportMode;
            rbNextToSource.Checked = mode == "NextToSourceFile";
            rbCustomDir.Checked    = mode == "CustomDirectory";
            rbTemplatePath.Checked = mode == "TemplatePath";
            if (!rbNextToSource.Checked && !rbCustomDir.Checked && !rbTemplatePath.Checked)
                rbNextToSource.Checked = true;

            tbExportDir.Text          = g.ExportDirectory          ?? "";
            tbTemplateBaseDir.Text    = g.TemplateBaseDirectory    ?? "";
            tbSubfolderTemplate.Text  = g.SubfolderTemplate        ?? "";
            tbFilenameTemplate.Text   = g.FilenameTemplate         ?? "";
            cbPromptOverwrite.Checked = g.PromptBeforeOverwrite    ?? true;
        }

        private void UpdatePreview()
        {
            if (_doc == null || !rbTemplatePath.Checked) { tbPreview.Text = ""; return; }

            try
            {
                string baseDir = tbTemplateBaseDir.Text.Trim();
                if (string.IsNullOrEmpty(baseDir))
                    baseDir = System.IO.Path.GetDirectoryName(_doc.FullFileName) ?? "";

                string subfolder = TemplateHelper.Expand(tbSubfolderTemplate.Text, _doc, sanitize: true);
                string filename  = TemplateHelper.Expand(tbFilenameTemplate.Text,  _doc, sanitize: true);

                if (string.IsNullOrEmpty(filename))
                    filename = System.IO.Path.GetFileNameWithoutExtension(_doc.FullFileName);

                tbPreview.Text = string.IsNullOrEmpty(subfolder)
                    ? System.IO.Path.Combine(baseDir, filename + ".dxf")
                    : System.IO.Path.Combine(baseDir, subfolder, filename + ".dxf");
            }
            catch { tbPreview.Text = ""; }
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
