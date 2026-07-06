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
            this.grpOverwrite = new System.Windows.Forms.GroupBox();
            this.cbPromptOverwrite = new System.Windows.Forms.CheckBox();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
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
            this.grpLocation.Location = new System.Drawing.Point(12, 12);
            this.grpLocation.Name = "grpLocation";
            this.grpLocation.Size = new System.Drawing.Size(460, 105);
            this.grpLocation.TabIndex = 0;
            this.grpLocation.TabStop = false;
            this.grpLocation.Text = "Output Location";
            //
            // rbNextToSource
            //
            this.rbNextToSource.AutoSize = true;
            this.rbNextToSource.Location = new System.Drawing.Point(12, 24);
            this.rbNextToSource.Name = "rbNextToSource";
            this.rbNextToSource.Size = new System.Drawing.Size(200, 17);
            this.rbNextToSource.TabIndex = 0;
            this.rbNextToSource.Text = "Save next to source file (.STP / .IPT)";
            this.rbNextToSource.UseVisualStyleBackColor = true;
            this.rbNextToSource.CheckedChanged += new System.EventHandler(this.rbNextToSource_CheckedChanged);
            //
            // rbCustomDir
            //
            this.rbCustomDir.AutoSize = true;
            this.rbCustomDir.Location = new System.Drawing.Point(12, 50);
            this.rbCustomDir.Name = "rbCustomDir";
            this.rbCustomDir.Size = new System.Drawing.Size(140, 17);
            this.rbCustomDir.TabIndex = 1;
            this.rbCustomDir.Text = "Save to a fixed directory:";
            this.rbCustomDir.UseVisualStyleBackColor = true;
            this.rbCustomDir.CheckedChanged += new System.EventHandler(this.rbCustomDir_CheckedChanged);
            //
            // tbExportDir
            //
            this.tbExportDir.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right)));
            this.tbExportDir.Location = new System.Drawing.Point(12, 74);
            this.tbExportDir.Name = "tbExportDir";
            this.tbExportDir.Size = new System.Drawing.Size(360, 20);
            this.tbExportDir.TabIndex = 2;
            //
            // btnBrowse
            //
            this.btnBrowse.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnBrowse.Location = new System.Drawing.Point(378, 72);
            this.btnBrowse.Name = "btnBrowse";
            this.btnBrowse.Size = new System.Drawing.Size(75, 23);
            this.btnBrowse.TabIndex = 3;
            this.btnBrowse.Text = "Browse...";
            this.btnBrowse.UseVisualStyleBackColor = true;
            this.btnBrowse.Click += new System.EventHandler(this.btnBrowse_Click);
            //
            // grpOverwrite
            //
            this.grpOverwrite.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right)));
            this.grpOverwrite.Controls.Add(this.cbPromptOverwrite);
            this.grpOverwrite.Location = new System.Drawing.Point(12, 128);
            this.grpOverwrite.Name = "grpOverwrite";
            this.grpOverwrite.Size = new System.Drawing.Size(460, 54);
            this.grpOverwrite.TabIndex = 1;
            this.grpOverwrite.TabStop = false;
            this.grpOverwrite.Text = "Overwrite Behavior";
            //
            // cbPromptOverwrite
            //
            this.cbPromptOverwrite.AutoSize = true;
            this.cbPromptOverwrite.Location = new System.Drawing.Point(12, 22);
            this.cbPromptOverwrite.Name = "cbPromptOverwrite";
            this.cbPromptOverwrite.Size = new System.Drawing.Size(300, 17);
            this.cbPromptOverwrite.TabIndex = 0;
            this.cbPromptOverwrite.Text = "Prompt before overwriting an existing DXF (offers Save As)";
            this.cbPromptOverwrite.UseVisualStyleBackColor = true;
            //
            // btnSave
            //
            this.btnSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSave.Location = new System.Drawing.Point(316, 200);
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
            this.btnCancel.Location = new System.Drawing.Point(397, 200);
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
            this.ClientSize = new System.Drawing.Size(484, 235);
            this.Controls.Add(this.grpLocation);
            this.Controls.Add(this.grpOverwrite);
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
        private System.Windows.Forms.GroupBox grpOverwrite;
        private System.Windows.Forms.CheckBox cbPromptOverwrite;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnCancel;
    }
}
