namespace InventorDxfExportAddin.Forms
{
    partial class FormAbout
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            btnClose = new Button();
            btnShowLog = new Button();
            tableLayoutPanel1 = new TableLayoutPanel();
            lblAddinPath = new Label();
            label5 = new Label();
            label2 = new Label();
            lblInventorVersion = new Label();
            label1 = new Label();
            label3 = new Label();
            lblLogFilePath = new LinkLabel();
            label4 = new Label();
            lblAddinVersion = new Label();
            tableLayoutPanel1.SuspendLayout();
            SuspendLayout();
            // 
            // btnClose
            // 
            btnClose.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            btnClose.Location = new Point(539, 241);
            btnClose.Name = "btnClose";
            btnClose.Size = new Size(75, 23);
            btnClose.TabIndex = 0;
            btnClose.Text = "Close";
            btnClose.UseVisualStyleBackColor = true;
            btnClose.Click += btnClose_Click;
            // 
            // btnShowLog
            // 
            btnShowLog.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            btnShowLog.Location = new Point(3, 241);
            btnShowLog.Name = "btnShowLog";
            btnShowLog.Size = new Size(113, 23);
            btnShowLog.TabIndex = 1;
            btnShowLog.Text = "Open Error Log";
            btnShowLog.UseVisualStyleBackColor = true;
            btnShowLog.Click += btnShowLog_Click;
            // 
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            tableLayoutPanel1.ColumnCount = 2;
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 20.42139F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 79.5786057F));
            tableLayoutPanel1.Controls.Add(lblInventorVersion, 1, 1);
            tableLayoutPanel1.Controls.Add(label1, 0, 1);
            tableLayoutPanel1.Controls.Add(label3, 0, 0);
            tableLayoutPanel1.Controls.Add(btnShowLog, 0, 5);
            tableLayoutPanel1.Controls.Add(btnClose, 1, 5);
            tableLayoutPanel1.Controls.Add(label2, 0, 4);
            tableLayoutPanel1.Controls.Add(label5, 0, 3);
            tableLayoutPanel1.Controls.Add(lblLogFilePath, 1, 4);
            tableLayoutPanel1.Controls.Add(lblAddinPath, 1, 3);
            tableLayoutPanel1.Controls.Add(label4, 0, 2);
            tableLayoutPanel1.Controls.Add(lblAddinVersion, 1, 2);
            tableLayoutPanel1.Location = new Point(12, 12);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 6;
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 30F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle());
            tableLayoutPanel1.Size = new Size(617, 267);
            tableLayoutPanel1.TabIndex = 2;
            // 
            // lblAddinPath
            // 
            lblAddinPath.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            lblAddinPath.AutoEllipsis = true;
            lblAddinPath.AutoSize = true;
            lblAddinPath.Location = new Point(128, 70);
            lblAddinPath.Name = "lblAddinPath";
            lblAddinPath.Size = new Size(486, 20);
            lblAddinPath.TabIndex = 5;
            lblAddinPath.Text = "--";
            lblAddinPath.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // label5
            // 
            label5.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Right;
            label5.AutoSize = true;
            label5.Location = new Point(31, 70);
            label5.Name = "label5";
            label5.Size = new Size(91, 20);
            label5.TabIndex = 4;
            label5.Text = "Addin Location:";
            label5.TextAlign = ContentAlignment.MiddleRight;
            // 
            // label2
            // 
            label2.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Right;
            label2.AutoSize = true;
            label2.Location = new Point(22, 90);
            label2.Name = "label2";
            label2.Size = new Size(100, 20);
            label2.TabIndex = 2;
            label2.Text = "Log File Location:";
            label2.TextAlign = ContentAlignment.MiddleRight;
            // 
            // lblInventorVersion
            // 
            lblInventorVersion.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            lblInventorVersion.AutoSize = true;
            lblInventorVersion.Location = new Point(128, 30);
            lblInventorVersion.Name = "lblInventorVersion";
            lblInventorVersion.Size = new Size(486, 20);
            lblInventorVersion.TabIndex = 1;
            lblInventorVersion.Text = "--";
            lblInventorVersion.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // label1
            // 
            label1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Right;
            label1.AutoSize = true;
            label1.Location = new Point(27, 30);
            label1.Name = "label1";
            label1.Size = new Size(95, 20);
            label1.TabIndex = 0;
            label1.Text = "Inventor Version:";
            label1.TextAlign = ContentAlignment.MiddleRight;
            // 
            // label3
            // 
            label3.Anchor = AnchorStyles.None;
            label3.AutoSize = true;
            tableLayoutPanel1.SetColumnSpan(label3, 2);
            label3.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            label3.Location = new Point(136, 5);
            label3.Name = "label3";
            label3.Size = new Size(344, 19);
            label3.TabIndex = 3;
            label3.Text = "Schroder DXF Export Addin for AutoDesk Inventor\r\n";
            label3.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // lblLogFilePath
            // 
            lblLogFilePath.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            lblLogFilePath.AutoEllipsis = true;
            lblLogFilePath.AutoSize = true;
            lblLogFilePath.Location = new Point(128, 90);
            lblLogFilePath.Name = "lblLogFilePath";
            lblLogFilePath.Size = new Size(486, 20);
            lblLogFilePath.TabIndex = 6;
            lblLogFilePath.TabStop = true;
            lblLogFilePath.Text = "c:\\log\\file\\path";
            lblLogFilePath.TextAlign = ContentAlignment.MiddleLeft;
            lblLogFilePath.LinkClicked += lblLogFilePath_LinkClicked;
            // 
            // label4
            // 
            label4.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Right;
            label4.AutoSize = true;
            label4.Location = new Point(42, 50);
            label4.Name = "label4";
            label4.Size = new Size(80, 20);
            label4.TabIndex = 7;
            label4.Text = "AddIn Version";
            label4.TextAlign = ContentAlignment.MiddleRight;
            // 
            // lblAddinVersion
            // 
            lblAddinVersion.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left;
            lblAddinVersion.AutoSize = true;
            lblAddinVersion.Location = new Point(128, 50);
            lblAddinVersion.Name = "lblAddinVersion";
            lblAddinVersion.Size = new Size(17, 20);
            lblAddinVersion.TabIndex = 8;
            lblAddinVersion.Text = "--";
            lblAddinVersion.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // FormAbout
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(641, 291);
            Controls.Add(tableLayoutPanel1);
            Name = "FormAbout";
            Text = "FormAbout";
            tableLayoutPanel1.ResumeLayout(false);
            tableLayoutPanel1.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private Button btnClose;
        private Button btnShowLog;
        private TableLayoutPanel tableLayoutPanel1;
        private Label lblInventorVersion;
        private Label label1;
        private Label label2;
        private Label label3;
        private Label label5;
        private Label lblAddinPath;
        private LinkLabel lblLogFilePath;
        private Label label4;
        private Label lblAddinVersion;
    }
}