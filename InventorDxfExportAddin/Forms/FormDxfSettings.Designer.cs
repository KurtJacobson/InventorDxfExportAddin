namespace InventorDxfExportAddin
{
    partial class FormDxfSettings : Form
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormDxfSettings));
            groupBox1 = new GroupBox();
            tableLayoutPanel1 = new TableLayoutPanel();
            cbOuterProfileLineType = new InventorDxfExportAddin.Custom_Controls.LineTypeComboBox();
            label3 = new Label();
            label4 = new Label();
            label1 = new Label();
            cbOuterProfileLineColor = new InventorDxfExportAddin.Custom_Controls.ColorComboBox();
            tbOuterProfileLayer = new TextBox();
            groupBox2 = new GroupBox();
            tableLayoutPanel2 = new TableLayoutPanel();
            lineTypeComboBox2 = new InventorDxfExportAddin.Custom_Controls.LineTypeComboBox();
            label2 = new Label();
            label5 = new Label();
            label6 = new Label();
            colorComboBox1 = new InventorDxfExportAddin.Custom_Controls.ColorComboBox();
            textBox2 = new TextBox();
            groupBox3 = new GroupBox();
            tableLayoutPanel3 = new TableLayoutPanel();
            lineTypeComboBox3 = new InventorDxfExportAddin.Custom_Controls.LineTypeComboBox();
            label7 = new Label();
            label8 = new Label();
            label9 = new Label();
            colorComboBox2 = new InventorDxfExportAddin.Custom_Controls.ColorComboBox();
            textBox3 = new TextBox();
            btnSave = new Button();
            btnCancel = new Button();
            groupBox1.SuspendLayout();
            tableLayoutPanel1.SuspendLayout();
            groupBox2.SuspendLayout();
            tableLayoutPanel2.SuspendLayout();
            groupBox3.SuspendLayout();
            tableLayoutPanel3.SuspendLayout();
            SuspendLayout();
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(tableLayoutPanel1);
            groupBox1.Location = new Point(14, 14);
            groupBox1.Margin = new Padding(4, 3, 4, 3);
            groupBox1.Name = "groupBox1";
            groupBox1.Padding = new Padding(4, 3, 4, 3);
            groupBox1.Size = new Size(354, 123);
            groupBox1.TabIndex = 0;
            groupBox1.TabStop = false;
            groupBox1.Text = "Outer Profile";
            // 
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            tableLayoutPanel1.ColumnCount = 2;
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle());
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle());
            tableLayoutPanel1.Controls.Add(cbOuterProfileLineType, 1, 2);
            tableLayoutPanel1.Controls.Add(label3, 0, 0);
            tableLayoutPanel1.Controls.Add(label4, 0, 1);
            tableLayoutPanel1.Controls.Add(label1, 0, 2);
            tableLayoutPanel1.Controls.Add(cbOuterProfileLineColor, 1, 1);
            tableLayoutPanel1.Controls.Add(tbOuterProfileLayer, 1, 0);
            tableLayoutPanel1.Location = new Point(7, 22);
            tableLayoutPanel1.Margin = new Padding(4, 3, 4, 3);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 3;
            tableLayoutPanel1.RowStyles.Add(new RowStyle());
            tableLayoutPanel1.RowStyles.Add(new RowStyle());
            tableLayoutPanel1.RowStyles.Add(new RowStyle());
            tableLayoutPanel1.RowStyles.Add(new RowStyle());
            tableLayoutPanel1.RowStyles.Add(new RowStyle());
            tableLayoutPanel1.RowStyles.Add(new RowStyle());
            tableLayoutPanel1.RowStyles.Add(new RowStyle());
            tableLayoutPanel1.RowStyles.Add(new RowStyle());
            tableLayoutPanel1.RowStyles.Add(new RowStyle());
            tableLayoutPanel1.RowStyles.Add(new RowStyle());
            tableLayoutPanel1.Size = new Size(340, 95);
            tableLayoutPanel1.TabIndex = 0;
            // 
            // cbOuterProfileLineType
            // 
            cbOuterProfileLineType.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            cbOuterProfileLineType.DrawMode = DrawMode.OwnerDrawFixed;
            cbOuterProfileLineType.DropDownStyle = ComboBoxStyle.DropDownList;
            cbOuterProfileLineType.FormattingEnabled = true;
            cbOuterProfileLineType.Location = new Point(82, 65);
            cbOuterProfileLineType.Margin = new Padding(4, 3, 4, 3);
            cbOuterProfileLineType.Name = "cbOuterProfileLineType";
            cbOuterProfileLineType.SelectedItem = null;
            cbOuterProfileLineType.SelectedValue = "Continuous";
            cbOuterProfileLineType.Size = new Size(254, 24);
            cbOuterProfileLineType.TabIndex = 3;
            // 
            // label3
            // 
            label3.Anchor = AnchorStyles.Right;
            label3.AutoSize = true;
            label3.Location = new Point(4, 7);
            label3.Margin = new Padding(4, 0, 4, 0);
            label3.Name = "label3";
            label3.Size = new Size(70, 15);
            label3.TabIndex = 2;
            label3.Text = "Layer Name";
            label3.TextAlign = ContentAlignment.MiddleRight;
            // 
            // label4
            // 
            label4.Anchor = AnchorStyles.Right;
            label4.AutoSize = true;
            label4.Location = new Point(7, 36);
            label4.Margin = new Padding(4, 0, 4, 0);
            label4.Name = "label4";
            label4.Size = new Size(67, 15);
            label4.TabIndex = 3;
            label4.Text = "Layer Color";
            label4.TextAlign = ContentAlignment.MiddleRight;
            // 
            // label1
            // 
            label1.Anchor = AnchorStyles.Right;
            label1.AutoSize = true;
            label1.Location = new Point(18, 69);
            label1.Margin = new Padding(4, 0, 4, 0);
            label1.Name = "label1";
            label1.Size = new Size(56, 15);
            label1.TabIndex = 4;
            label1.Text = "Line Type";
            label1.TextAlign = ContentAlignment.MiddleRight;
            // 
            // cbOuterProfileLineColor
            // 
            cbOuterProfileLineColor.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            cbOuterProfileLineColor.DrawMode = DrawMode.OwnerDrawFixed;
            cbOuterProfileLineColor.DropDownStyle = ComboBoxStyle.DropDownList;
            cbOuterProfileLineColor.FormattingEnabled = true;
            cbOuterProfileLineColor.Location = new Point(82, 32);
            cbOuterProfileLineColor.Margin = new Padding(4, 3, 4, 3);
            cbOuterProfileLineColor.Name = "cbOuterProfileLineColor";
            cbOuterProfileLineColor.SelectedColor = Color.White;
            cbOuterProfileLineColor.SelectedValue = Color.White;
            cbOuterProfileLineColor.Size = new Size(254, 24);
            cbOuterProfileLineColor.TabIndex = 10;
            // 
            // tbOuterProfileLayer
            // 
            tbOuterProfileLayer.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            tbOuterProfileLayer.Location = new Point(82, 3);
            tbOuterProfileLayer.Margin = new Padding(4, 3, 4, 3);
            tbOuterProfileLayer.Name = "tbOuterProfileLayer";
            tbOuterProfileLayer.Size = new Size(254, 23);
            tbOuterProfileLayer.TabIndex = 7;
            // 
            // groupBox2
            // 
            groupBox2.Controls.Add(tableLayoutPanel2);
            groupBox2.Location = new Point(14, 144);
            groupBox2.Margin = new Padding(4, 3, 4, 3);
            groupBox2.Name = "groupBox2";
            groupBox2.Padding = new Padding(4, 3, 4, 3);
            groupBox2.Size = new Size(354, 123);
            groupBox2.TabIndex = 1;
            groupBox2.TabStop = false;
            groupBox2.Text = "Inner Profiles";
            // 
            // tableLayoutPanel2
            // 
            tableLayoutPanel2.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            tableLayoutPanel2.ColumnCount = 2;
            tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle());
            tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle());
            tableLayoutPanel2.Controls.Add(lineTypeComboBox2, 1, 2);
            tableLayoutPanel2.Controls.Add(label2, 0, 0);
            tableLayoutPanel2.Controls.Add(label5, 0, 1);
            tableLayoutPanel2.Controls.Add(label6, 0, 2);
            tableLayoutPanel2.Controls.Add(colorComboBox1, 1, 1);
            tableLayoutPanel2.Controls.Add(textBox2, 1, 0);
            tableLayoutPanel2.Location = new Point(7, 22);
            tableLayoutPanel2.Margin = new Padding(4, 3, 4, 3);
            tableLayoutPanel2.Name = "tableLayoutPanel2";
            tableLayoutPanel2.RowCount = 3;
            tableLayoutPanel2.RowStyles.Add(new RowStyle());
            tableLayoutPanel2.RowStyles.Add(new RowStyle());
            tableLayoutPanel2.RowStyles.Add(new RowStyle());
            tableLayoutPanel2.RowStyles.Add(new RowStyle());
            tableLayoutPanel2.RowStyles.Add(new RowStyle());
            tableLayoutPanel2.RowStyles.Add(new RowStyle());
            tableLayoutPanel2.RowStyles.Add(new RowStyle());
            tableLayoutPanel2.RowStyles.Add(new RowStyle());
            tableLayoutPanel2.RowStyles.Add(new RowStyle());
            tableLayoutPanel2.RowStyles.Add(new RowStyle());
            tableLayoutPanel2.Size = new Size(340, 95);
            tableLayoutPanel2.TabIndex = 0;
            // 
            // lineTypeComboBox2
            // 
            lineTypeComboBox2.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            lineTypeComboBox2.DrawMode = DrawMode.OwnerDrawFixed;
            lineTypeComboBox2.DropDownStyle = ComboBoxStyle.DropDownList;
            lineTypeComboBox2.FormattingEnabled = true;
            lineTypeComboBox2.Location = new Point(82, 65);
            lineTypeComboBox2.Margin = new Padding(4, 3, 4, 3);
            lineTypeComboBox2.Name = "lineTypeComboBox2";
            lineTypeComboBox2.SelectedItem = null;
            lineTypeComboBox2.SelectedValue = "Continuous";
            lineTypeComboBox2.Size = new Size(254, 24);
            lineTypeComboBox2.TabIndex = 3;
            // 
            // label2
            // 
            label2.Anchor = AnchorStyles.Right;
            label2.AutoSize = true;
            label2.Location = new Point(4, 7);
            label2.Margin = new Padding(4, 0, 4, 0);
            label2.Name = "label2";
            label2.Size = new Size(70, 15);
            label2.TabIndex = 2;
            label2.Text = "Layer Name";
            label2.TextAlign = ContentAlignment.MiddleRight;
            // 
            // label5
            // 
            label5.Anchor = AnchorStyles.Right;
            label5.AutoSize = true;
            label5.Location = new Point(7, 36);
            label5.Margin = new Padding(4, 0, 4, 0);
            label5.Name = "label5";
            label5.Size = new Size(67, 15);
            label5.TabIndex = 3;
            label5.Text = "Layer Color";
            label5.TextAlign = ContentAlignment.MiddleRight;
            // 
            // label6
            // 
            label6.Anchor = AnchorStyles.Right;
            label6.AutoSize = true;
            label6.Location = new Point(18, 69);
            label6.Margin = new Padding(4, 0, 4, 0);
            label6.Name = "label6";
            label6.Size = new Size(56, 15);
            label6.TabIndex = 4;
            label6.Text = "Line Type";
            label6.TextAlign = ContentAlignment.MiddleRight;
            // 
            // colorComboBox1
            // 
            colorComboBox1.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            colorComboBox1.DrawMode = DrawMode.OwnerDrawFixed;
            colorComboBox1.DropDownStyle = ComboBoxStyle.DropDownList;
            colorComboBox1.FormattingEnabled = true;
            colorComboBox1.Location = new Point(82, 32);
            colorComboBox1.Margin = new Padding(4, 3, 4, 3);
            colorComboBox1.Name = "colorComboBox1";
            colorComboBox1.SelectedColor = Color.White;
            colorComboBox1.SelectedValue = Color.White;
            colorComboBox1.Size = new Size(254, 24);
            colorComboBox1.TabIndex = 10;
            // 
            // textBox2
            // 
            textBox2.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            textBox2.Location = new Point(82, 3);
            textBox2.Margin = new Padding(4, 3, 4, 3);
            textBox2.Name = "textBox2";
            textBox2.Size = new Size(254, 23);
            textBox2.TabIndex = 7;
            // 
            // groupBox3
            // 
            groupBox3.Controls.Add(tableLayoutPanel3);
            groupBox3.Location = new Point(14, 275);
            groupBox3.Margin = new Padding(4, 3, 4, 3);
            groupBox3.Name = "groupBox3";
            groupBox3.Padding = new Padding(4, 3, 4, 3);
            groupBox3.Size = new Size(354, 123);
            groupBox3.TabIndex = 2;
            groupBox3.TabStop = false;
            groupBox3.Text = "Bend Lines";
            // 
            // tableLayoutPanel3
            // 
            tableLayoutPanel3.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            tableLayoutPanel3.ColumnCount = 2;
            tableLayoutPanel3.ColumnStyles.Add(new ColumnStyle());
            tableLayoutPanel3.ColumnStyles.Add(new ColumnStyle());
            tableLayoutPanel3.Controls.Add(lineTypeComboBox3, 1, 2);
            tableLayoutPanel3.Controls.Add(label7, 0, 0);
            tableLayoutPanel3.Controls.Add(label8, 0, 1);
            tableLayoutPanel3.Controls.Add(label9, 0, 2);
            tableLayoutPanel3.Controls.Add(colorComboBox2, 1, 1);
            tableLayoutPanel3.Controls.Add(textBox3, 1, 0);
            tableLayoutPanel3.Location = new Point(7, 22);
            tableLayoutPanel3.Margin = new Padding(4, 3, 4, 3);
            tableLayoutPanel3.Name = "tableLayoutPanel3";
            tableLayoutPanel3.RowCount = 3;
            tableLayoutPanel3.RowStyles.Add(new RowStyle());
            tableLayoutPanel3.RowStyles.Add(new RowStyle());
            tableLayoutPanel3.RowStyles.Add(new RowStyle());
            tableLayoutPanel3.RowStyles.Add(new RowStyle());
            tableLayoutPanel3.RowStyles.Add(new RowStyle());
            tableLayoutPanel3.RowStyles.Add(new RowStyle());
            tableLayoutPanel3.RowStyles.Add(new RowStyle());
            tableLayoutPanel3.RowStyles.Add(new RowStyle());
            tableLayoutPanel3.RowStyles.Add(new RowStyle());
            tableLayoutPanel3.RowStyles.Add(new RowStyle());
            tableLayoutPanel3.Size = new Size(340, 95);
            tableLayoutPanel3.TabIndex = 0;
            // 
            // lineTypeComboBox3
            // 
            lineTypeComboBox3.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            lineTypeComboBox3.DrawMode = DrawMode.OwnerDrawFixed;
            lineTypeComboBox3.DropDownStyle = ComboBoxStyle.DropDownList;
            lineTypeComboBox3.FormattingEnabled = true;
            lineTypeComboBox3.Location = new Point(82, 65);
            lineTypeComboBox3.Margin = new Padding(4, 3, 4, 3);
            lineTypeComboBox3.Name = "lineTypeComboBox3";
            lineTypeComboBox3.SelectedItem = null;
            lineTypeComboBox3.SelectedValue = "Continuous";
            lineTypeComboBox3.Size = new Size(254, 24);
            lineTypeComboBox3.TabIndex = 3;
            // 
            // label7
            // 
            label7.Anchor = AnchorStyles.Right;
            label7.AutoSize = true;
            label7.Location = new Point(4, 7);
            label7.Margin = new Padding(4, 0, 4, 0);
            label7.Name = "label7";
            label7.Size = new Size(70, 15);
            label7.TabIndex = 2;
            label7.Text = "Layer Name";
            label7.TextAlign = ContentAlignment.MiddleRight;
            // 
            // label8
            // 
            label8.Anchor = AnchorStyles.Right;
            label8.AutoSize = true;
            label8.Location = new Point(7, 36);
            label8.Margin = new Padding(4, 0, 4, 0);
            label8.Name = "label8";
            label8.Size = new Size(67, 15);
            label8.TabIndex = 3;
            label8.Text = "Layer Color";
            label8.TextAlign = ContentAlignment.MiddleRight;
            // 
            // label9
            // 
            label9.Anchor = AnchorStyles.Right;
            label9.AutoSize = true;
            label9.Location = new Point(18, 69);
            label9.Margin = new Padding(4, 0, 4, 0);
            label9.Name = "label9";
            label9.Size = new Size(56, 15);
            label9.TabIndex = 4;
            label9.Text = "Line Type";
            label9.TextAlign = ContentAlignment.MiddleRight;
            // 
            // colorComboBox2
            // 
            colorComboBox2.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            colorComboBox2.DrawMode = DrawMode.OwnerDrawFixed;
            colorComboBox2.DropDownStyle = ComboBoxStyle.DropDownList;
            colorComboBox2.FormattingEnabled = true;
            colorComboBox2.Location = new Point(82, 32);
            colorComboBox2.Margin = new Padding(4, 3, 4, 3);
            colorComboBox2.Name = "colorComboBox2";
            colorComboBox2.SelectedColor = Color.White;
            colorComboBox2.SelectedValue = Color.White;
            colorComboBox2.Size = new Size(254, 24);
            colorComboBox2.TabIndex = 10;
            // 
            // textBox3
            // 
            textBox3.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            textBox3.Location = new Point(82, 3);
            textBox3.Margin = new Padding(4, 3, 4, 3);
            textBox3.Name = "textBox3";
            textBox3.Size = new Size(254, 23);
            textBox3.TabIndex = 7;
            // 
            // btnSave
            // 
            btnSave.Location = new Point(186, 405);
            btnSave.Margin = new Padding(4, 3, 4, 3);
            btnSave.Name = "btnSave";
            btnSave.Size = new Size(88, 27);
            btnSave.TabIndex = 3;
            btnSave.Text = "Save";
            btnSave.UseVisualStyleBackColor = true;
            btnSave.Click += btnSave_Click;
            // 
            // btnCancel
            // 
            btnCancel.Location = new Point(280, 405);
            btnCancel.Margin = new Padding(4, 3, 4, 3);
            btnCancel.Name = "btnCancel";
            btnCancel.Size = new Size(88, 27);
            btnCancel.TabIndex = 4;
            btnCancel.Text = "Cancel";
            btnCancel.UseVisualStyleBackColor = true;
            btnCancel.Click += btnCancel_Click;
            // 
            // FormDxfSettings
            // 
            AcceptButton = btnSave;
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(379, 442);
            ControlBox = false;
            Controls.Add(btnCancel);
            Controls.Add(btnSave);
            Controls.Add(groupBox3);
            Controls.Add(groupBox2);
            Controls.Add(groupBox1);
            Icon = (Icon)resources.GetObject("$this.Icon");
            Margin = new Padding(4, 3, 4, 3);
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "FormDxfSettings";
            SizeGripStyle = SizeGripStyle.Hide;
            StartPosition = FormStartPosition.CenterParent;
            Text = "DXF Export Settings";
            groupBox1.ResumeLayout(false);
            tableLayoutPanel1.ResumeLayout(false);
            tableLayoutPanel1.PerformLayout();
            groupBox2.ResumeLayout(false);
            tableLayoutPanel2.ResumeLayout(false);
            tableLayoutPanel2.PerformLayout();
            groupBox3.ResumeLayout(false);
            tableLayoutPanel3.ResumeLayout(false);
            tableLayoutPanel3.PerformLayout();
            ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox tbOuterProfileLayer;
        private Custom_Controls.ColorComboBox cbOuterProfileLineColor;
        private Custom_Controls.LineTypeComboBox cbOuterProfileLineType;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private Custom_Controls.LineTypeComboBox lineTypeComboBox2;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private Custom_Controls.ColorComboBox colorComboBox1;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel3;
        private Custom_Controls.LineTypeComboBox lineTypeComboBox3;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private Custom_Controls.ColorComboBox colorComboBox2;
        private System.Windows.Forms.TextBox textBox3;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnCancel;
    }
}