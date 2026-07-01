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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.cbOuterProfileLineType = new InventorDxfExportAddin.Custom_Controls.LineTypeComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.cbOuterProfileLayerColor = new InventorDxfExportAddin.Custom_Controls.ColorComboBox();
            this.tbOuterProfileLayer = new System.Windows.Forms.TextBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.cbInnerProfilesLineType = new InventorDxfExportAddin.Custom_Controls.LineTypeComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.cbInnerProfilesLayerColor = new InventorDxfExportAddin.Custom_Controls.ColorComboBox();
            this.tbInnerProfilesLayer = new System.Windows.Forms.TextBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
            this.cbBendLineType = new InventorDxfExportAddin.Custom_Controls.LineTypeComboBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.cbBendLinesLayerColor = new InventorDxfExportAddin.Custom_Controls.ColorComboBox();
            this.tbBendLineLayer = new System.Windows.Forms.TextBox();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.cbEnableBendLinesDown = new System.Windows.Forms.CheckBox();
            this.tableLayoutPanel4 = new System.Windows.Forms.TableLayoutPanel();
            this.cbBendDownLineType = new InventorDxfExportAddin.Custom_Controls.LineTypeComboBox();
            this.label10 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.cbBendLinesDownLayerColor = new InventorDxfExportAddin.Custom_Controls.ColorComboBox();
            this.tbBendLinesDownLayer = new System.Windows.Forms.TextBox();
            this.groupBox1.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.tableLayoutPanel3.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.tableLayoutPanel4.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.tableLayoutPanel1);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(303, 107);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Outer Profile";
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.cbOuterProfileLineType, 1, 2);
            this.tableLayoutPanel1.Controls.Add(this.label3, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.label4, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.label1, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.cbOuterProfileLayerColor, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.tbOuterProfileLayer, 1, 0);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(6, 19);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 3;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.Size = new System.Drawing.Size(291, 82);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // cbOuterProfileLineType
            // 
            this.cbOuterProfileLineType.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.cbOuterProfileLineType.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cbOuterProfileLineType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbOuterProfileLineType.FormattingEnabled = true;
            this.cbOuterProfileLineType.Location = new System.Drawing.Point(73, 57);
            this.cbOuterProfileLineType.Name = "cbOuterProfileLineType";
            this.cbOuterProfileLineType.SelectedLineType = Inventor.LineTypeEnum.kContinuousLineType;
            this.cbOuterProfileLineType.Size = new System.Drawing.Size(223, 21);
            this.cbOuterProfileLineType.TabIndex = 3;
            // 
            // label3
            // 
            this.label3.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(3, 6);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(64, 13);
            this.label3.TabIndex = 2;
            this.label3.Text = "Layer Name";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label4
            // 
            this.label4.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(7, 33);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(60, 13);
            this.label4.TabIndex = 3;
            this.label4.Text = "Layer Color";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label1
            // 
            this.label1.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 61);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(54, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "Line Type";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // cbOuterProfileLayerColor
            // 
            this.cbOuterProfileLayerColor.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.cbOuterProfileLayerColor.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cbOuterProfileLayerColor.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbOuterProfileLayerColor.FormattingEnabled = true;
            this.cbOuterProfileLayerColor.Location = new System.Drawing.Point(73, 29);
            this.cbOuterProfileLayerColor.Name = "cbOuterProfileLayerColor";
            this.cbOuterProfileLayerColor.SelectedColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.cbOuterProfileLayerColor.SelectedValue = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.cbOuterProfileLayerColor.Size = new System.Drawing.Size(223, 21);
            this.cbOuterProfileLayerColor.TabIndex = 10;
            // 
            // tbOuterProfileLayer
            // 
            this.tbOuterProfileLayer.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.tbOuterProfileLayer.Location = new System.Drawing.Point(73, 3);
            this.tbOuterProfileLayer.Name = "tbOuterProfileLayer";
            this.tbOuterProfileLayer.Size = new System.Drawing.Size(223, 20);
            this.tbOuterProfileLayer.TabIndex = 7;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.tableLayoutPanel2);
            this.groupBox2.Location = new System.Drawing.Point(12, 124);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(303, 107);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Inner Profiles";
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel2.ColumnCount = 2;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.Controls.Add(this.cbInnerProfilesLineType, 1, 2);
            this.tableLayoutPanel2.Controls.Add(this.label2, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.label5, 0, 1);
            this.tableLayoutPanel2.Controls.Add(this.label6, 0, 2);
            this.tableLayoutPanel2.Controls.Add(this.cbInnerProfilesLayerColor, 1, 1);
            this.tableLayoutPanel2.Controls.Add(this.tbInnerProfilesLayer, 1, 0);
            this.tableLayoutPanel2.Location = new System.Drawing.Point(6, 19);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 3;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel2.Size = new System.Drawing.Size(291, 82);
            this.tableLayoutPanel2.TabIndex = 0;
            // 
            // cbInnerProfilesLineType
            // 
            this.cbInnerProfilesLineType.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.cbInnerProfilesLineType.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cbInnerProfilesLineType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbInnerProfilesLineType.FormattingEnabled = true;
            this.cbInnerProfilesLineType.Location = new System.Drawing.Point(73, 57);
            this.cbInnerProfilesLineType.Name = "cbInnerProfilesLineType";
            this.cbInnerProfilesLineType.SelectedLineType = Inventor.LineTypeEnum.kContinuousLineType;
            this.cbInnerProfilesLineType.Size = new System.Drawing.Size(223, 21);
            this.cbInnerProfilesLineType.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(3, 6);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(64, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Layer Name";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label5
            // 
            this.label5.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(7, 33);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(60, 13);
            this.label5.TabIndex = 3;
            this.label5.Text = "Layer Color";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label6
            // 
            this.label6.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(13, 61);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(54, 13);
            this.label6.TabIndex = 4;
            this.label6.Text = "Line Type";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // cbInnerProfilesLayerColor
            // 
            this.cbInnerProfilesLayerColor.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.cbInnerProfilesLayerColor.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cbInnerProfilesLayerColor.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbInnerProfilesLayerColor.FormattingEnabled = true;
            this.cbInnerProfilesLayerColor.Location = new System.Drawing.Point(73, 29);
            this.cbInnerProfilesLayerColor.Name = "cbInnerProfilesLayerColor";
            this.cbInnerProfilesLayerColor.SelectedColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.cbInnerProfilesLayerColor.SelectedValue = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.cbInnerProfilesLayerColor.Size = new System.Drawing.Size(223, 21);
            this.cbInnerProfilesLayerColor.TabIndex = 10;
            // 
            // tbInnerProfilesLayer
            // 
            this.tbInnerProfilesLayer.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.tbInnerProfilesLayer.Location = new System.Drawing.Point(73, 3);
            this.tbInnerProfilesLayer.Name = "tbInnerProfilesLayer";
            this.tbInnerProfilesLayer.Size = new System.Drawing.Size(223, 20);
            this.tbInnerProfilesLayer.TabIndex = 7;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.tableLayoutPanel3);
            this.groupBox3.Location = new System.Drawing.Point(12, 236);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(303, 107);
            this.groupBox3.TabIndex = 2;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Bend Lines";
            // 
            // tableLayoutPanel3
            // 
            this.tableLayoutPanel3.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel3.ColumnCount = 2;
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel3.Controls.Add(this.cbBendLineType, 1, 2);
            this.tableLayoutPanel3.Controls.Add(this.label7, 0, 0);
            this.tableLayoutPanel3.Controls.Add(this.label8, 0, 1);
            this.tableLayoutPanel3.Controls.Add(this.label9, 0, 2);
            this.tableLayoutPanel3.Controls.Add(this.cbBendLinesLayerColor, 1, 1);
            this.tableLayoutPanel3.Controls.Add(this.tbBendLineLayer, 1, 0);
            this.tableLayoutPanel3.Location = new System.Drawing.Point(6, 19);
            this.tableLayoutPanel3.Name = "tableLayoutPanel3";
            this.tableLayoutPanel3.RowCount = 3;
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel3.Size = new System.Drawing.Size(291, 82);
            this.tableLayoutPanel3.TabIndex = 0;
            // 
            // cbBendLineType
            // 
            this.cbBendLineType.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.cbBendLineType.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cbBendLineType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbBendLineType.FormattingEnabled = true;
            this.cbBendLineType.Location = new System.Drawing.Point(73, 57);
            this.cbBendLineType.Name = "cbBendLineType";
            this.cbBendLineType.SelectedLineType = Inventor.LineTypeEnum.kContinuousLineType;
            this.cbBendLineType.Size = new System.Drawing.Size(223, 21);
            this.cbBendLineType.TabIndex = 3;
            // 
            // label7
            // 
            this.label7.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(3, 6);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(64, 13);
            this.label7.TabIndex = 2;
            this.label7.Text = "Layer Name";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label8
            // 
            this.label8.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(7, 33);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(60, 13);
            this.label8.TabIndex = 3;
            this.label8.Text = "Layer Color";
            this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label9
            // 
            this.label9.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(13, 61);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(54, 13);
            this.label9.TabIndex = 4;
            this.label9.Text = "Line Type";
            this.label9.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // cbBendLinesLayerColor
            // 
            this.cbBendLinesLayerColor.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.cbBendLinesLayerColor.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cbBendLinesLayerColor.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbBendLinesLayerColor.FormattingEnabled = true;
            this.cbBendLinesLayerColor.Location = new System.Drawing.Point(73, 29);
            this.cbBendLinesLayerColor.Name = "cbBendLinesLayerColor";
            this.cbBendLinesLayerColor.SelectedColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.cbBendLinesLayerColor.SelectedValue = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.cbBendLinesLayerColor.Size = new System.Drawing.Size(223, 21);
            this.cbBendLinesLayerColor.TabIndex = 10;
            // 
            // tbBendLineLayer
            // 
            this.tbBendLineLayer.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.tbBendLineLayer.Location = new System.Drawing.Point(73, 3);
            this.tbBendLineLayer.Name = "tbBendLineLayer";
            this.tbBendLineLayer.Size = new System.Drawing.Size(218, 20);
            this.tbBendLineLayer.TabIndex = 7;
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(158, 459);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 23);
            this.btnSave.TabIndex = 3;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            //
            // btnCancel
            //
            this.btnCancel.Location = new System.Drawing.Point(240, 459);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 4;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.cbEnableBendLinesDown);
            this.groupBox4.Controls.Add(this.tableLayoutPanel4);
            this.groupBox4.Location = new System.Drawing.Point(12, 348);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(303, 107);
            this.groupBox4.TabIndex = 3;
            this.groupBox4.TabStop = false;
            // 
            // cbEnableBendLinesDown
            // 
            this.cbEnableBendLinesDown.AutoSize = true;
            this.cbEnableBendLinesDown.Location = new System.Drawing.Point(6, 0);
            this.cbEnableBendLinesDown.Name = "cbEnableBendLinesDown";
            this.cbEnableBendLinesDown.Size = new System.Drawing.Size(177, 17);
            this.cbEnableBendLinesDown.TabIndex = 1;
            this.cbEnableBendLinesDown.Text = "Seperate Layer for Down Bends";
            this.cbEnableBendLinesDown.UseVisualStyleBackColor = true;
            // 
            // tableLayoutPanel4
            // 
            this.tableLayoutPanel4.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel4.ColumnCount = 2;
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel4.Controls.Add(this.cbBendDownLineType, 1, 2);
            this.tableLayoutPanel4.Controls.Add(this.label10, 0, 0);
            this.tableLayoutPanel4.Controls.Add(this.label11, 0, 1);
            this.tableLayoutPanel4.Controls.Add(this.label12, 0, 2);
            this.tableLayoutPanel4.Controls.Add(this.cbBendLinesDownLayerColor, 1, 1);
            this.tableLayoutPanel4.Controls.Add(this.tbBendLinesDownLayer, 1, 0);
            this.tableLayoutPanel4.Location = new System.Drawing.Point(7, 19);
            this.tableLayoutPanel4.Name = "tableLayoutPanel4";
            this.tableLayoutPanel4.RowCount = 3;
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel4.Size = new System.Drawing.Size(291, 82);
            this.tableLayoutPanel4.TabIndex = 0;
            // 
            // cbBendDownLineType
            // 
            this.cbBendDownLineType.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.cbBendDownLineType.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cbBendDownLineType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbBendDownLineType.FormattingEnabled = true;
            this.cbBendDownLineType.Location = new System.Drawing.Point(73, 57);
            this.cbBendDownLineType.Name = "cbBendDownLineType";
            this.cbBendDownLineType.SelectedLineType = Inventor.LineTypeEnum.kContinuousLineType;
            this.cbBendDownLineType.Size = new System.Drawing.Size(217, 21);
            this.cbBendDownLineType.TabIndex = 3;
            // 
            // label10
            // 
            this.label10.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(3, 6);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(64, 13);
            this.label10.TabIndex = 2;
            this.label10.Text = "Layer Name";
            this.label10.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label11
            // 
            this.label11.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(7, 33);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(60, 13);
            this.label11.TabIndex = 3;
            this.label11.Text = "Layer Color";
            this.label11.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label12
            // 
            this.label12.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(13, 61);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(54, 13);
            this.label12.TabIndex = 4;
            this.label12.Text = "Line Type";
            this.label12.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // cbBendLinesDownLayerColor
            // 
            this.cbBendLinesDownLayerColor.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.cbBendLinesDownLayerColor.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cbBendLinesDownLayerColor.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbBendLinesDownLayerColor.FormattingEnabled = true;
            this.cbBendLinesDownLayerColor.Location = new System.Drawing.Point(73, 29);
            this.cbBendLinesDownLayerColor.Name = "cbBendLinesDownLayerColor";
            this.cbBendLinesDownLayerColor.SelectedColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.cbBendLinesDownLayerColor.SelectedValue = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.cbBendLinesDownLayerColor.Size = new System.Drawing.Size(217, 21);
            this.cbBendLinesDownLayerColor.TabIndex = 10;
            // 
            // tbBendLinesDownLayer
            // 
            this.tbBendLinesDownLayer.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.tbBendLinesDownLayer.Location = new System.Drawing.Point(73, 3);
            this.tbBendLinesDownLayer.Name = "tbBendLinesDownLayer";
            this.tbBendLinesDownLayer.Size = new System.Drawing.Size(217, 20);
            this.tbBendLinesDownLayer.TabIndex = 7;
            // 
            // FormDxfSettings
            // 
            this.AcceptButton = this.btnSave;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(325, 490);
            this.ControlBox = false;
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormDxfSettings";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "DXF Layer Settings";
            this.groupBox1.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.tableLayoutPanel3.ResumeLayout(false);
            this.tableLayoutPanel3.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.tableLayoutPanel4.ResumeLayout(false);
            this.tableLayoutPanel4.PerformLayout();
            this.ResumeLayout(false);

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