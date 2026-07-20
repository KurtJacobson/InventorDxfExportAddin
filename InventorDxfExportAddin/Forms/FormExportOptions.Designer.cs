namespace InventorDxfExportAddin.Forms
{
    partial class FormExportOptions : System.Windows.Forms.Form
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.grpLocation = new System.Windows.Forms.GroupBox();
            this.rbNextToSource = new System.Windows.Forms.RadioButton();
            this.rbCustomDir = new System.Windows.Forms.RadioButton();
            this.tbExportDir = new System.Windows.Forms.TextBox();
            this.btnBrowse = new System.Windows.Forms.Button();
            this.rbTemplatePath = new System.Windows.Forms.RadioButton();
            this.lblBaseDir = new System.Windows.Forms.Label();
            this.tbTemplateBaseDir = new System.Windows.Forms.TextBox();
            this.btnBrowseTemplate = new System.Windows.Forms.Button();
            this.lblSubfolder = new System.Windows.Forms.Label();
            this.tbSubfolderTemplate = new System.Windows.Forms.TextBox();
            this.btnPickSubfolder = new System.Windows.Forms.Button();
            this.lblFilename = new System.Windows.Forms.Label();
            this.tbFilenameTemplate = new System.Windows.Forms.TextBox();
            this.btnPickFilename = new System.Windows.Forms.Button();
            this.lblTokens = new System.Windows.Forms.Label();
            this.lblPreview = new System.Windows.Forms.Label();
            this.tbPreview = new System.Windows.Forms.TextBox();
            this.grpOverwrite = new System.Windows.Forms.GroupBox();
            this.cbPromptOverwrite = new System.Windows.Forms.CheckBox();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnResetToGlobal = new System.Windows.Forms.Button();
            this.grpLocation.SuspendLayout();
            this.grpOverwrite.SuspendLayout();
            this.SuspendLayout();
            //
            // grpLocation
            //
            this.grpLocation.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right)));
            this.grpLocation.Controls.Add(this.rbNextToSource);
            this.grpLocation.Controls.Add(this.rbCustomDir);
            this.grpLocation.Controls.Add(this.tbExportDir);
            this.grpLocation.Controls.Add(this.btnBrowse);
            this.grpLocation.Controls.Add(this.rbTemplatePath);
            this.grpLocation.Controls.Add(this.lblBaseDir);
            this.grpLocation.Controls.Add(this.tbTemplateBaseDir);
            this.grpLocation.Controls.Add(this.btnBrowseTemplate);
            this.grpLocation.Controls.Add(this.lblSubfolder);
            this.grpLocation.Controls.Add(this.tbSubfolderTemplate);
            this.grpLocation.Controls.Add(this.btnPickSubfolder);
            this.grpLocation.Controls.Add(this.lblFilename);
            this.grpLocation.Controls.Add(this.tbFilenameTemplate);
            this.grpLocation.Controls.Add(this.btnPickFilename);
            this.grpLocation.Controls.Add(this.lblTokens);
            this.grpLocation.Controls.Add(this.lblPreview);
            this.grpLocation.Controls.Add(this.tbPreview);
            this.grpLocation.Location = new System.Drawing.Point(12, 12);
            this.grpLocation.Name = "grpLocation";
            this.grpLocation.Size = new System.Drawing.Size(460, 338);
            this.grpLocation.TabIndex = 0;
            this.grpLocation.TabStop = false;
            this.grpLocation.Text = "Output Location";
            //
            // rbNextToSource
            //
            this.rbNextToSource.AutoSize = true;
            this.rbNextToSource.Location = new System.Drawing.Point(12, 24);
            this.rbNextToSource.Name = "rbNextToSource";
            this.rbNextToSource.TabIndex = 0;
            this.rbNextToSource.Text = "Save next to source file (.STP / .IPT)";
            this.rbNextToSource.UseVisualStyleBackColor = true;
            this.rbNextToSource.CheckedChanged += new System.EventHandler(this.rbMode_CheckedChanged);
            //
            // rbCustomDir
            //
            this.rbCustomDir.AutoSize = true;
            this.rbCustomDir.Location = new System.Drawing.Point(12, 50);
            this.rbCustomDir.Name = "rbCustomDir";
            this.rbCustomDir.TabIndex = 1;
            this.rbCustomDir.Text = "Save to a fixed directory:";
            this.rbCustomDir.UseVisualStyleBackColor = true;
            this.rbCustomDir.CheckedChanged += new System.EventHandler(this.rbMode_CheckedChanged);
            //
            // tbExportDir
            //
            this.tbExportDir.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right)));
            this.tbExportDir.Location = new System.Drawing.Point(28, 72);
            this.tbExportDir.Name = "tbExportDir";
            this.tbExportDir.Size = new System.Drawing.Size(346, 20);
            this.tbExportDir.TabIndex = 2;
            //
            // btnBrowse
            //
            this.btnBrowse.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnBrowse.Location = new System.Drawing.Point(380, 70);
            this.btnBrowse.Name = "btnBrowse";
            this.btnBrowse.Size = new System.Drawing.Size(68, 23);
            this.btnBrowse.TabIndex = 3;
            this.btnBrowse.Text = "Browse...";
            this.btnBrowse.UseVisualStyleBackColor = true;
            this.btnBrowse.Click += new System.EventHandler(this.btnBrowse_Click);
            //
            // rbTemplatePath
            //
            this.rbTemplatePath.AutoSize = true;
            this.rbTemplatePath.Location = new System.Drawing.Point(12, 104);
            this.rbTemplatePath.Name = "rbTemplatePath";
            this.rbTemplatePath.TabIndex = 4;
            this.rbTemplatePath.Text = "Build path from iProperties (template):";
            this.rbTemplatePath.UseVisualStyleBackColor = true;
            this.rbTemplatePath.CheckedChanged += new System.EventHandler(this.rbMode_CheckedChanged);
            //
            // lblBaseDir
            //
            this.lblBaseDir.AutoSize = true;
            this.lblBaseDir.Location = new System.Drawing.Point(28, 130);
            this.lblBaseDir.Name = "lblBaseDir";
            this.lblBaseDir.TabIndex = 5;
            this.lblBaseDir.Text = "Base directory (blank = same folder as part):";
            //
            // tbTemplateBaseDir
            //
            this.tbTemplateBaseDir.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right)));
            this.tbTemplateBaseDir.Location = new System.Drawing.Point(28, 147);
            this.tbTemplateBaseDir.Name = "tbTemplateBaseDir";
            this.tbTemplateBaseDir.Size = new System.Drawing.Size(346, 20);
            this.tbTemplateBaseDir.TabIndex = 6;
            //
            // btnBrowseTemplate
            //
            this.btnBrowseTemplate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnBrowseTemplate.Location = new System.Drawing.Point(380, 145);
            this.btnBrowseTemplate.Name = "btnBrowseTemplate";
            this.btnBrowseTemplate.Size = new System.Drawing.Size(68, 23);
            this.btnBrowseTemplate.TabIndex = 7;
            this.btnBrowseTemplate.Text = "Browse...";
            this.btnBrowseTemplate.UseVisualStyleBackColor = true;
            this.btnBrowseTemplate.Click += new System.EventHandler(this.btnBrowseTemplate_Click);
            //
            // lblSubfolder
            //
            this.lblSubfolder.AutoSize = true;
            this.lblSubfolder.Location = new System.Drawing.Point(28, 176);
            this.lblSubfolder.Name = "lblSubfolder";
            this.lblSubfolder.TabIndex = 8;
            this.lblSubfolder.Text = "Subfolder pattern (creates nested directories):";
            //
            // tbSubfolderTemplate
            //
            this.tbSubfolderTemplate.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right)));
            this.tbSubfolderTemplate.Location = new System.Drawing.Point(28, 193);
            this.tbSubfolderTemplate.Name = "tbSubfolderTemplate";
            this.tbSubfolderTemplate.Size = new System.Drawing.Size(392, 20);
            this.tbSubfolderTemplate.TabIndex = 9;
            //
            // btnPickSubfolder
            //
            this.btnPickSubfolder.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnPickSubfolder.Location = new System.Drawing.Point(424, 191);
            this.btnPickSubfolder.Name = "btnPickSubfolder";
            this.btnPickSubfolder.Size = new System.Drawing.Size(24, 23);
            this.btnPickSubfolder.TabIndex = 13;
            this.btnPickSubfolder.Text = "…";
            this.btnPickSubfolder.UseVisualStyleBackColor = true;
            this.btnPickSubfolder.Click += new System.EventHandler(this.btnPickSubfolder_Click);
            //
            // lblFilename
            //
            this.lblFilename.AutoSize = true;
            this.lblFilename.Location = new System.Drawing.Point(28, 221);
            this.lblFilename.Name = "lblFilename";
            this.lblFilename.TabIndex = 10;
            this.lblFilename.Text = "Filename pattern (no extension; blank = source filename):";
            //
            // tbFilenameTemplate
            //
            this.tbFilenameTemplate.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right)));
            this.tbFilenameTemplate.Location = new System.Drawing.Point(28, 238);
            this.tbFilenameTemplate.Name = "tbFilenameTemplate";
            this.tbFilenameTemplate.Size = new System.Drawing.Size(392, 20);
            this.tbFilenameTemplate.TabIndex = 11;
            //
            // btnPickFilename
            //
            this.btnPickFilename.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnPickFilename.Location = new System.Drawing.Point(424, 236);
            this.btnPickFilename.Name = "btnPickFilename";
            this.btnPickFilename.Size = new System.Drawing.Size(24, 23);
            this.btnPickFilename.TabIndex = 14;
            this.btnPickFilename.Text = "…";
            this.btnPickFilename.UseVisualStyleBackColor = true;
            this.btnPickFilename.Click += new System.EventHandler(this.btnPickFilename_Click);
            //
            // lblTokens
            //
            this.lblTokens.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right)));
            this.lblTokens.ForeColor = System.Drawing.SystemColors.GrayText;
            this.lblTokens.Location = new System.Drawing.Point(28, 262);
            this.lblTokens.Name = "lblTokens";
            this.lblTokens.Size = new System.Drawing.Size(420, 26);
            this.lblTokens.TabIndex = 12;
            this.lblTokens.Text = "Use the … buttons to insert iProperty tokens into the subfolder and filename patterns.";
            //
            // lblPreview
            //
            this.lblPreview.AutoSize = true;
            this.lblPreview.ForeColor = System.Drawing.SystemColors.GrayText;
            this.lblPreview.Location = new System.Drawing.Point(28, 294);
            this.lblPreview.Name = "lblPreview";
            this.lblPreview.TabIndex = 15;
            this.lblPreview.Text = "Preview:";
            //
            // tbPreview
            //
            this.tbPreview.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right)));
            this.tbPreview.BackColor = System.Drawing.SystemColors.Control;
            this.tbPreview.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.tbPreview.ForeColor = System.Drawing.SystemColors.GrayText;
            this.tbPreview.Location = new System.Drawing.Point(28, 312);
            this.tbPreview.Name = "tbPreview";
            this.tbPreview.ReadOnly = true;
            this.tbPreview.Size = new System.Drawing.Size(420, 13);
            this.tbPreview.TabIndex = 16;
            this.tbPreview.TabStop = false;
            //
            // grpOverwrite
            //
            this.grpOverwrite.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right)));
            this.grpOverwrite.Controls.Add(this.cbPromptOverwrite);
            this.grpOverwrite.Location = new System.Drawing.Point(12, 360);
            this.grpOverwrite.Name = "grpOverwrite";
            this.grpOverwrite.Size = new System.Drawing.Size(460, 48);
            this.grpOverwrite.TabIndex = 1;
            this.grpOverwrite.TabStop = false;
            this.grpOverwrite.Text = "Overwrite Behavior";
            //
            // cbPromptOverwrite
            //
            this.cbPromptOverwrite.AutoSize = true;
            this.cbPromptOverwrite.Location = new System.Drawing.Point(12, 22);
            this.cbPromptOverwrite.Name = "cbPromptOverwrite";
            this.cbPromptOverwrite.TabIndex = 0;
            this.cbPromptOverwrite.Text = "Prompt before overwriting an existing DXF (offers Save As)";
            this.cbPromptOverwrite.UseVisualStyleBackColor = true;
            //
            // btnResetToGlobal
            //
            this.btnResetToGlobal.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnResetToGlobal.Location = new System.Drawing.Point(12, 422);
            this.btnResetToGlobal.Name = "btnResetToGlobal";
            this.btnResetToGlobal.Size = new System.Drawing.Size(150, 23);
            this.btnResetToGlobal.TabIndex = 4;
            this.btnResetToGlobal.Text = "↺ Reset to org defaults";
            this.btnResetToGlobal.UseVisualStyleBackColor = true;
            this.btnResetToGlobal.Visible = false;
            this.btnResetToGlobal.Click += new System.EventHandler(this.btnResetToGlobal_Click);
            //
            // btnSave
            //
            this.btnSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSave.Location = new System.Drawing.Point(316, 422);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 23);
            this.btnSave.TabIndex = 2;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            //
            // btnCancel
            //
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.Location = new System.Drawing.Point(397, 422);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 3;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            //
            // FormExportOptions
            //
            this.AcceptButton = this.btnSave;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(484, 457);
            this.Controls.Add(this.grpLocation);
            this.Controls.Add(this.grpOverwrite);
            this.Controls.Add(this.btnResetToGlobal);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.btnCancel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormExportOptions";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Export Options";
            this.grpLocation.ResumeLayout(false);
            this.grpLocation.PerformLayout();
            this.grpOverwrite.ResumeLayout(false);
            this.grpOverwrite.PerformLayout();
            this.ResumeLayout(false);
        }

        private System.Windows.Forms.GroupBox grpLocation;
        private System.Windows.Forms.RadioButton rbNextToSource;
        private System.Windows.Forms.RadioButton rbCustomDir;
        private System.Windows.Forms.TextBox tbExportDir;
        private System.Windows.Forms.Button btnBrowse;
        private System.Windows.Forms.RadioButton rbTemplatePath;
        private System.Windows.Forms.Label lblBaseDir;
        private System.Windows.Forms.TextBox tbTemplateBaseDir;
        private System.Windows.Forms.Button btnBrowseTemplate;
        private System.Windows.Forms.Label lblSubfolder;
        private System.Windows.Forms.TextBox tbSubfolderTemplate;
        private System.Windows.Forms.Button btnPickSubfolder;
        private System.Windows.Forms.Label lblFilename;
        private System.Windows.Forms.TextBox tbFilenameTemplate;
        private System.Windows.Forms.Button btnPickFilename;
        private System.Windows.Forms.Label lblTokens;
        private System.Windows.Forms.Label lblPreview;
        private System.Windows.Forms.TextBox tbPreview;
        private System.Windows.Forms.GroupBox grpOverwrite;
        private System.Windows.Forms.CheckBox cbPromptOverwrite;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnResetToGlobal;
    }
}
