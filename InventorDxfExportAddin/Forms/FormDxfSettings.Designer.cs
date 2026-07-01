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
            cbOuterProfileLayerColor = new InventorDxfExportAddin.Custom_Controls.ColorComboBox();
            tbOuterProfileLayer = new TextBox();
            groupBox2 = new GroupBox();
            tableLayoutPanel2 = new TableLayoutPanel();
            cbInnerProfilesLineType = new InventorDxfExportAddin.Custom_Controls.LineTypeComboBox();
            label2 = new Label();
            label5 = new Label();
            label6 = new Label();
            cbInnerProfilesLayerColor = new InventorDxfExportAddin.Custom_Controls.ColorComboBox();
            tbInnerProfilesLayer = new TextBox();
            groupBox3 = new GroupBox();
            tableLayoutPanel3 = new TableLayoutPanel();
            cbBendLineType = new InventorDxfExportAddin.Custom_Controls.LineTypeComboBox();
            label7 = new Label();
            label8 = new Label();
            label9 = new Label();
            cbBendLinesLayerColor = new InventorDxfExportAddin.Custom_Controls.ColorComboBox();
            tbBendLineLayer = new TextBox();
            btnSave = new Button();
            btnCancel = new Button();
            groupBox4 = new GroupBox();
            cbEnableBendLinesDown = new CheckBox();
            tableLayoutPanel4 = new TableLayoutPanel();
            cbBendDownLineType = new InventorDxfExportAddin.Custom_Controls.LineTypeComboBox();
            label10 = new Label();
            label11 = new Label();
            label12 = new Label();
            cbBendLinesDownLayerColor = new InventorDxfExportAddin.Custom_Controls.ColorComboBox();
            tbBendLinesDownLayer = new TextBox();
            groupBox1.SuspendLayout();
            tableLayoutPanel1.SuspendLayout();
            groupBox2.SuspendLayout();
            tableLayoutPanel2.SuspendLayout();
            groupBox3.SuspendLayout();
            tableLayoutPanel3.SuspendLayout();
            groupBox4.SuspendLayout();
            tableLayoutPanel4.SuspendLayout();
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
            tableLayoutPanel1.Controls.Add(cbOuterProfileLayerColor, 1, 1);
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
            cbOuterProfileLineType.SelectedLineType = Inventor.LineTypeEnum.kContinuousLineType;
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
            // cbOuterProfileLayerColor
            // 
            cbOuterProfileLayerColor.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            cbOuterProfileLayerColor.DrawMode = DrawMode.OwnerDrawFixed;
            cbOuterProfileLayerColor.DropDownStyle = ComboBoxStyle.DropDownList;
            cbOuterProfileLayerColor.FormattingEnabled = true;
            cbOuterProfileLayerColor.Location = new Point(82, 32);
            cbOuterProfileLayerColor.Margin = new Padding(4, 3, 4, 3);
            cbOuterProfileLayerColor.Name = "cbOuterProfileLayerColor";
            cbOuterProfileLayerColor.SelectedColor = Color.FromArgb(255, 255, 255);
            cbOuterProfileLayerColor.SelectedValue = Color.FromArgb(255, 255, 255);
            cbOuterProfileLayerColor.Size = new Size(254, 24);
            cbOuterProfileLayerColor.TabIndex = 10;
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
            groupBox2.Location = new Point(14, 143);
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
            tableLayoutPanel2.Controls.Add(cbInnerProfilesLineType, 1, 2);
            tableLayoutPanel2.Controls.Add(label2, 0, 0);
            tableLayoutPanel2.Controls.Add(label5, 0, 1);
            tableLayoutPanel2.Controls.Add(label6, 0, 2);
            tableLayoutPanel2.Controls.Add(cbInnerProfilesLayerColor, 1, 1);
            tableLayoutPanel2.Controls.Add(tbInnerProfilesLayer, 1, 0);
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
            // cbInnerProfilesLineType
            // 
            cbInnerProfilesLineType.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            cbInnerProfilesLineType.DrawMode = DrawMode.OwnerDrawFixed;
            cbInnerProfilesLineType.DropDownStyle = ComboBoxStyle.DropDownList;
            cbInnerProfilesLineType.FormattingEnabled = true;
            cbInnerProfilesLineType.Location = new Point(82, 65);
            cbInnerProfilesLineType.Margin = new Padding(4, 3, 4, 3);
            cbInnerProfilesLineType.Name = "cbInnerProfilesLineType";
            cbInnerProfilesLineType.SelectedLineType = Inventor.LineTypeEnum.kContinuousLineType;
            cbInnerProfilesLineType.Size = new Size(254, 24);
            cbInnerProfilesLineType.TabIndex = 3;
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
            // cbInnerProfilesLayerColor
            // 
            cbInnerProfilesLayerColor.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            cbInnerProfilesLayerColor.DrawMode = DrawMode.OwnerDrawFixed;
            cbInnerProfilesLayerColor.DropDownStyle = ComboBoxStyle.DropDownList;
            cbInnerProfilesLayerColor.FormattingEnabled = true;
            cbInnerProfilesLayerColor.Location = new Point(82, 32);
            cbInnerProfilesLayerColor.Margin = new Padding(4, 3, 4, 3);
            cbInnerProfilesLayerColor.Name = "cbInnerProfilesLayerColor";
            cbInnerProfilesLayerColor.SelectedColor = Color.FromArgb(255, 255, 255);
            cbInnerProfilesLayerColor.SelectedValue = Color.FromArgb(255, 255, 255);
            cbInnerProfilesLayerColor.Size = new Size(254, 24);
            cbInnerProfilesLayerColor.TabIndex = 10;
            // 
            // tbInnerProfilesLayer
            // 
            tbInnerProfilesLayer.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            tbInnerProfilesLayer.Location = new Point(82, 3);
            tbInnerProfilesLayer.Margin = new Padding(4, 3, 4, 3);
            tbInnerProfilesLayer.Name = "tbInnerProfilesLayer";
            tbInnerProfilesLayer.Size = new Size(254, 23);
            tbInnerProfilesLayer.TabIndex = 7;
            // 
            // groupBox3
            // 
            groupBox3.Controls.Add(tableLayoutPanel3);
            groupBox3.Location = new Point(14, 272);
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
            tableLayoutPanel3.Controls.Add(cbBendLineType, 1, 2);
            tableLayoutPanel3.Controls.Add(label7, 0, 0);
            tableLayoutPanel3.Controls.Add(label8, 0, 1);
            tableLayoutPanel3.Controls.Add(label9, 0, 2);
            tableLayoutPanel3.Controls.Add(cbBendLinesLayerColor, 1, 1);
            tableLayoutPanel3.Controls.Add(tbBendLineLayer, 1, 0);
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
            // cbBendLineType
            // 
            cbBendLineType.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            cbBendLineType.DrawMode = DrawMode.OwnerDrawFixed;
            cbBendLineType.DropDownStyle = ComboBoxStyle.DropDownList;
            cbBendLineType.FormattingEnabled = true;
            cbBendLineType.Location = new Point(82, 65);
            cbBendLineType.Margin = new Padding(4, 3, 4, 3);
            cbBendLineType.Name = "cbBendLineType";
            cbBendLineType.SelectedLineType = Inventor.LineTypeEnum.kContinuousLineType;
            cbBendLineType.Size = new Size(254, 24);
            cbBendLineType.TabIndex = 3;
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
            // cbBendLinesLayerColor
            // 
            cbBendLinesLayerColor.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            cbBendLinesLayerColor.DrawMode = DrawMode.OwnerDrawFixed;
            cbBendLinesLayerColor.DropDownStyle = ComboBoxStyle.DropDownList;
            cbBendLinesLayerColor.FormattingEnabled = true;
            cbBendLinesLayerColor.Location = new Point(82, 32);
            cbBendLinesLayerColor.Margin = new Padding(4, 3, 4, 3);
            cbBendLinesLayerColor.Name = "cbBendLinesLayerColor";
            cbBendLinesLayerColor.SelectedColor = Color.FromArgb(255, 255, 255);
            cbBendLinesLayerColor.SelectedValue = Color.FromArgb(255, 255, 255);
            cbBendLinesLayerColor.Size = new Size(254, 24);
            cbBendLinesLayerColor.TabIndex = 10;
            // 
            // tbBendLineLayer
            // 
            tbBendLineLayer.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            tbBendLineLayer.Location = new Point(82, 3);
            tbBendLineLayer.Margin = new Padding(4, 3, 4, 3);
            tbBendLineLayer.Name = "tbBendLineLayer";
            tbBendLineLayer.Size = new Size(254, 23);
            tbBendLineLayer.TabIndex = 7;
            // 
            // btnSave
            // 
            btnSave.Location = new Point(184, 530);
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
            btnCancel.Location = new Point(280, 530);
            btnCancel.Margin = new Padding(4, 3, 4, 3);
            btnCancel.Name = "btnCancel";
            btnCancel.Size = new Size(88, 27);
            btnCancel.TabIndex = 4;
            btnCancel.Text = "Cancel";
            btnCancel.UseVisualStyleBackColor = true;
            btnCancel.Click += btnCancel_Click;
            // 
            // groupBox4
            // 
            groupBox4.Controls.Add(cbEnableBendLinesDown);
            groupBox4.Controls.Add(tableLayoutPanel4);
            groupBox4.Location = new Point(14, 401);
            groupBox4.Margin = new Padding(4, 3, 4, 3);
            groupBox4.Name = "groupBox4";
            groupBox4.Padding = new Padding(4, 3, 4, 3);
            groupBox4.Size = new Size(354, 123);
            groupBox4.TabIndex = 3;
            groupBox4.TabStop = false;
            // 
            // cbEnableBendLinesDown
            // 
            cbEnableBendLinesDown.AutoSize = true;
            cbEnableBendLinesDown.Location = new Point(7, 0);
            cbEnableBendLinesDown.Name = "cbEnableBendLinesDown";
            cbEnableBendLinesDown.Size = new Size(189, 19);
            cbEnableBendLinesDown.TabIndex = 1;
            cbEnableBendLinesDown.Text = "Seperate Layer for Down Bends";
            cbEnableBendLinesDown.UseVisualStyleBackColor = true;
            // 
            // tableLayoutPanel4
            // 
            tableLayoutPanel4.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            tableLayoutPanel4.ColumnCount = 2;
            tableLayoutPanel4.ColumnStyles.Add(new ColumnStyle());
            tableLayoutPanel4.ColumnStyles.Add(new ColumnStyle());
            tableLayoutPanel4.Controls.Add(cbBendDownLineType, 1, 2);
            tableLayoutPanel4.Controls.Add(label10, 0, 0);
            tableLayoutPanel4.Controls.Add(label11, 0, 1);
            tableLayoutPanel4.Controls.Add(label12, 0, 2);
            tableLayoutPanel4.Controls.Add(cbBendLinesDownLayerColor, 1, 1);
            tableLayoutPanel4.Controls.Add(tbBendLinesDownLayer, 1, 0);
            tableLayoutPanel4.Location = new Point(8, 22);
            tableLayoutPanel4.Margin = new Padding(4, 3, 4, 3);
            tableLayoutPanel4.Name = "tableLayoutPanel4";
            tableLayoutPanel4.RowCount = 3;
            tableLayoutPanel4.RowStyles.Add(new RowStyle());
            tableLayoutPanel4.RowStyles.Add(new RowStyle());
            tableLayoutPanel4.RowStyles.Add(new RowStyle());
            tableLayoutPanel4.RowStyles.Add(new RowStyle());
            tableLayoutPanel4.RowStyles.Add(new RowStyle());
            tableLayoutPanel4.RowStyles.Add(new RowStyle());
            tableLayoutPanel4.RowStyles.Add(new RowStyle());
            tableLayoutPanel4.RowStyles.Add(new RowStyle());
            tableLayoutPanel4.RowStyles.Add(new RowStyle());
            tableLayoutPanel4.RowStyles.Add(new RowStyle());
            tableLayoutPanel4.Size = new Size(339, 95);
            tableLayoutPanel4.TabIndex = 0;
            // 
            // cbBendDownLineType
            // 
            cbBendDownLineType.Anchor = AnchorStyles.Left;
            cbBendDownLineType.DrawMode = DrawMode.OwnerDrawFixed;
            cbBendDownLineType.DropDownStyle = ComboBoxStyle.DropDownList;
            cbBendDownLineType.FormattingEnabled = true;
            cbBendDownLineType.Location = new Point(82, 65);
            cbBendDownLineType.Margin = new Padding(4, 3, 4, 3);
            cbBendDownLineType.Name = "cbBendDownLineType";
            cbBendDownLineType.SelectedLineType = Inventor.LineTypeEnum.kContinuousLineType;
            cbBendDownLineType.Size = new Size(253, 24);
            cbBendDownLineType.TabIndex = 3;
            // 
            // label10
            // 
            label10.Anchor = AnchorStyles.Right;
            label10.AutoSize = true;
            label10.Location = new Point(4, 7);
            label10.Margin = new Padding(4, 0, 4, 0);
            label10.Name = "label10";
            label10.Size = new Size(70, 15);
            label10.TabIndex = 2;
            label10.Text = "Layer Name";
            label10.TextAlign = ContentAlignment.MiddleRight;
            // 
            // label11
            // 
            label11.Anchor = AnchorStyles.Right;
            label11.AutoSize = true;
            label11.Location = new Point(7, 36);
            label11.Margin = new Padding(4, 0, 4, 0);
            label11.Name = "label11";
            label11.Size = new Size(67, 15);
            label11.TabIndex = 3;
            label11.Text = "Layer Color";
            label11.TextAlign = ContentAlignment.MiddleRight;
            // 
            // label12
            // 
            label12.Anchor = AnchorStyles.Right;
            label12.AutoSize = true;
            label12.Location = new Point(18, 69);
            label12.Margin = new Padding(4, 0, 4, 0);
            label12.Name = "label12";
            label12.Size = new Size(56, 15);
            label12.TabIndex = 4;
            label12.Text = "Line Type";
            label12.TextAlign = ContentAlignment.MiddleRight;
            // 
            // cbBendLinesDownLayerColor
            // 
            cbBendLinesDownLayerColor.Anchor = AnchorStyles.Left;
            cbBendLinesDownLayerColor.DrawMode = DrawMode.OwnerDrawFixed;
            cbBendLinesDownLayerColor.DropDownStyle = ComboBoxStyle.DropDownList;
            cbBendLinesDownLayerColor.FormattingEnabled = true;
            cbBendLinesDownLayerColor.Location = new Point(82, 32);
            cbBendLinesDownLayerColor.Margin = new Padding(4, 3, 4, 3);
            cbBendLinesDownLayerColor.Name = "cbBendLinesDownLayerColor";
            cbBendLinesDownLayerColor.SelectedColor = Color.FromArgb(255, 255, 255);
            cbBendLinesDownLayerColor.SelectedValue = Color.FromArgb(255, 255, 255);
            cbBendLinesDownLayerColor.Size = new Size(253, 24);
            cbBendLinesDownLayerColor.TabIndex = 10;
            // 
            // tbBendLinesDownLayer
            // 
            tbBendLinesDownLayer.Anchor = AnchorStyles.Left;
            tbBendLinesDownLayer.Location = new Point(82, 3);
            tbBendLinesDownLayer.Margin = new Padding(4, 3, 4, 3);
            tbBendLinesDownLayer.Name = "tbBendLinesDownLayer";
            tbBendLinesDownLayer.Size = new Size(253, 23);
            tbBendLinesDownLayer.TabIndex = 7;
            // 
            // FormDxfSettings
            // 
            AcceptButton = btnSave;
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(379, 565);
            ControlBox = false;
            Controls.Add(groupBox4);
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
            Text = "DXF Layer Settings";
            groupBox1.ResumeLayout(false);
            tableLayoutPanel1.ResumeLayout(false);
            tableLayoutPanel1.PerformLayout();
            groupBox2.ResumeLayout(false);
            tableLayoutPanel2.ResumeLayout(false);
            tableLayoutPanel2.PerformLayout();
            groupBox3.ResumeLayout(false);
            tableLayoutPanel3.ResumeLayout(false);
            tableLayoutPanel3.PerformLayout();
            groupBox4.ResumeLayout(false);
            groupBox4.PerformLayout();
            tableLayoutPanel4.ResumeLayout(false);
            tableLayoutPanel4.PerformLayout();
            ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox tbOuterProfileLayer;
        private InventorDxfExportAddin.Custom_Controls.ColorComboBox cbOuterProfileLayerColor;
        private InventorDxfExportAddin.Custom_Controls.LineTypeComboBox cbOuterProfileLineType;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private InventorDxfExportAddin.Custom_Controls.LineTypeComboBox cbInnerProfilesLineType;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private InventorDxfExportAddin.Custom_Controls.ColorComboBox cbInnerProfilesLayerColor;
        private System.Windows.Forms.TextBox tbInnerProfilesLayer;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel3;
        private InventorDxfExportAddin.Custom_Controls.LineTypeComboBox cbBendLineType;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private InventorDxfExportAddin.Custom_Controls.ColorComboBox cbBendLinesLayerColor;
        private System.Windows.Forms.TextBox tbBendLineLayer;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel4;
        private InventorDxfExportAddin.Custom_Controls.LineTypeComboBox cbBendDownLineType;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label12;
        private InventorDxfExportAddin.Custom_Controls.ColorComboBox cbBendLinesDownLayerColor;
        private System.Windows.Forms.TextBox tbBendLinesDownLayer;
        private System.Windows.Forms.CheckBox cbEnableBendLinesDown;
    }
}