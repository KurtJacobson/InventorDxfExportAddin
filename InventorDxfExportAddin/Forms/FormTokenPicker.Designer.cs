namespace InventorDxfExportAddin.Forms
{
    partial class FormTokenPicker
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.lblInstruction = new System.Windows.Forms.Label();
            this.lvTokens = new System.Windows.Forms.ListView();
            this.colToken = new System.Windows.Forms.ColumnHeader();
            this.colSource = new System.Windows.Forms.ColumnHeader();
            this.colValue = new System.Windows.Forms.ColumnHeader();
            this.btnInsert = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.SuspendLayout();
            //
            // lblInstruction
            //
            this.lblInstruction.AutoSize = true;
            this.lblInstruction.Location = new System.Drawing.Point(12, 12);
            this.lblInstruction.Name = "lblInstruction";
            this.lblInstruction.TabIndex = 0;
            this.lblInstruction.Text = "Double-click or select a token and click Insert:";
            //
            // lvTokens
            //
            this.lvTokens.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right));
            this.lvTokens.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
                this.colToken, this.colSource, this.colValue });
            this.lvTokens.FullRowSelect = true;
            this.lvTokens.GridLines = true;
            this.lvTokens.HideSelection = false;
            this.lvTokens.Location = new System.Drawing.Point(12, 32);
            this.lvTokens.MultiSelect = false;
            this.lvTokens.Name = "lvTokens";
            this.lvTokens.Size = new System.Drawing.Size(560, 340);
            this.lvTokens.TabIndex = 1;
            this.lvTokens.UseCompatibleStateImageBehavior = false;
            this.lvTokens.View = System.Windows.Forms.View.Details;
            this.lvTokens.DoubleClick += new System.EventHandler(this.lvTokens_DoubleClick);
            this.lvTokens.SelectedIndexChanged += new System.EventHandler(this.lvTokens_SelectedIndexChanged);
            //
            // colToken
            //
            this.colToken.Text = "Token";
            this.colToken.Width = 190;
            //
            // colSource
            //
            this.colSource.Text = "Source";
            this.colSource.Width = 160;
            //
            // colValue
            //
            this.colValue.Text = "Current Value";
            this.colValue.Width = 200;
            //
            // btnInsert
            //
            this.btnInsert.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnInsert.Enabled = false;
            this.btnInsert.Location = new System.Drawing.Point(416, 386);
            this.btnInsert.Name = "btnInsert";
            this.btnInsert.Size = new System.Drawing.Size(75, 23);
            this.btnInsert.TabIndex = 2;
            this.btnInsert.Text = "Insert";
            this.btnInsert.UseVisualStyleBackColor = true;
            this.btnInsert.Click += new System.EventHandler(this.btnInsert_Click);
            //
            // btnCancel
            //
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(497, 386);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 3;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            //
            // FormTokenPicker
            //
            this.AcceptButton = this.btnInsert;
            this.CancelButton = this.btnCancel;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(584, 421);
            this.Controls.Add(this.lblInstruction);
            this.Controls.Add(this.lvTokens);
            this.Controls.Add(this.btnInsert);
            this.Controls.Add(this.btnCancel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Sizable;
            this.MinimumSize = new System.Drawing.Size(500, 380);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormTokenPicker";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Insert Token";
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private System.Windows.Forms.Label lblInstruction;
        private System.Windows.Forms.ListView lvTokens;
        private System.Windows.Forms.ColumnHeader colToken;
        private System.Windows.Forms.ColumnHeader colSource;
        private System.Windows.Forms.ColumnHeader colValue;
        private System.Windows.Forms.Button btnInsert;
        private System.Windows.Forms.Button btnCancel;
    }
}
