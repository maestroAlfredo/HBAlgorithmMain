namespace VoltageDropCalculatorApplication
{
    partial class nodeFeederForm
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(nodeFeederForm));
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.label1 = new System.Windows.Forms.Label();
            this.projectNameTextBox = new System.Windows.Forms.TextBox();
            this.createNewProjectButton = new System.Windows.Forms.Button();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.continueButton = new System.Windows.Forms.Button();
            this.label9 = new System.Windows.Forms.Label();
            this.passiveRadio = new System.Windows.Forms.RadioButton();
            this.activeRadio = new System.Windows.Forms.RadioButton();
            this.lengthNumericUpDown = new System.Windows.Forms.NumericUpDown();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.nodeNumCombo = new System.Windows.Forms.ComboBox();
            this.label7 = new System.Windows.Forms.Label();
            this.cableSelectCombo = new System.Windows.Forms.ComboBox();
            this.label8 = new System.Windows.Forms.Label();
            this.nodeDataGridView = new System.Windows.Forms.DataGridView();
            this.tableLayoutPanel5 = new System.Windows.Forms.TableLayoutPanel();
            this.cancelButton = new System.Windows.Forms.Button();
            this.proceedToVCalcButton = new System.Windows.Forms.Button();
            this.selectEndNodeCombo = new System.Windows.Forms.ComboBox();
            this.label18 = new System.Windows.Forms.Label();
            this.endNodeCombo = new System.Windows.Forms.ComboBox();
            this.tableLayoutPanel4 = new System.Windows.Forms.TableLayoutPanel();
            this.nodeNameTextBox = new System.Windows.Forms.TextBox();
            this.label20 = new System.Windows.Forms.Label();
            this.deleteNodeButton = new System.Windows.Forms.Button();
            this.label16 = new System.Windows.Forms.Label();
            this.label22 = new System.Windows.Forms.Label();
            this.nodeNameCombo = new System.Windows.Forms.ComboBox();
            this.addNodeButton = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.label17 = new System.Windows.Forms.Label();
            this.nodeCount = new System.Windows.Forms.Label();
            this.drawingPanel = new System.Windows.Forms.Panel();
            this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
            this.label19 = new System.Windows.Forms.Label();
            this.detailsCheckBox = new System.Windows.Forms.CheckBox();
            this.searchTextBox = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.sourceVoltageTextBox = new System.Windows.Forms.TextBox();
            this.operatingTemperatureText = new System.Windows.Forms.TextBox();
            this.tableLayoutPanel1.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.lengthNumericUpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nodeDataGridView)).BeginInit();
            this.tableLayoutPanel5.SuspendLayout();
            this.tableLayoutPanel4.SuspendLayout();
            this.tableLayoutPanel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 3;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 83F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 586F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.label1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.projectNameTextBox, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.createNewProjectButton, 2, 0);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(3, 34);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(896, 44);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(9, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(71, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Project Name";
            this.label1.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // projectNameTextBox
            // 
            this.projectNameTextBox.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.projectNameTextBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.projectNameTextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.projectNameTextBox.ForeColor = System.Drawing.Color.SteelBlue;
            this.projectNameTextBox.Location = new System.Drawing.Point(86, 11);
            this.projectNameTextBox.Name = "projectNameTextBox";
            this.projectNameTextBox.Size = new System.Drawing.Size(102, 22);
            this.projectNameTextBox.TabIndex = 1;
            this.projectNameTextBox.Text = "myProj";
            this.projectNameTextBox.TextChanged += new System.EventHandler(this.projectNameTextBox_TextChanged);
            // 
            // createNewProjectButton
            // 
            this.createNewProjectButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.createNewProjectButton.ForeColor = System.Drawing.Color.SteelBlue;
            this.createNewProjectButton.Location = new System.Drawing.Point(672, 3);
            this.createNewProjectButton.Name = "createNewProjectButton";
            this.createNewProjectButton.Size = new System.Drawing.Size(219, 38);
            this.createNewProjectButton.TabIndex = 0;
            this.createNewProjectButton.Text = "Create Project";
            this.createNewProjectButton.UseVisualStyleBackColor = true;
            this.createNewProjectButton.Click += new System.EventHandler(this.createNewProjectButton_Click);
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 8;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 83F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 104F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 93F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 88F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 122F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 94F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 86F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 8F));
            this.tableLayoutPanel2.Controls.Add(this.label9, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.passiveRadio, 1, 0);
            this.tableLayoutPanel2.Controls.Add(this.activeRadio, 2, 0);
            this.tableLayoutPanel2.Controls.Add(this.label6, 3, 0);
            this.tableLayoutPanel2.Controls.Add(this.label10, 5, 0);
            this.tableLayoutPanel2.Controls.Add(this.continueButton, 7, 0);
            this.tableLayoutPanel2.Controls.Add(this.sourceVoltageTextBox, 4, 0);
            this.tableLayoutPanel2.Controls.Add(this.operatingTemperatureText, 6, 0);
            this.tableLayoutPanel2.Location = new System.Drawing.Point(3, 81);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 1;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(896, 42);
            this.tableLayoutPanel2.TabIndex = 1;
            // 
            // continueButton
            // 
            this.continueButton.Enabled = false;
            this.continueButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.continueButton.ForeColor = System.Drawing.Color.SteelBlue;
            this.continueButton.Location = new System.Drawing.Point(673, 3);
            this.continueButton.Name = "continueButton";
            this.continueButton.Size = new System.Drawing.Size(218, 36);
            this.continueButton.TabIndex = 0;
            this.continueButton.Text = "Continue";
            this.continueButton.UseVisualStyleBackColor = true;
            this.continueButton.Click += new System.EventHandler(this.continueButton_Click);
            // 
            // label9
            // 
            this.label9.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label9.AutoSize = true;
            this.label9.ForeColor = System.Drawing.Color.White;
            this.label9.Location = new System.Drawing.Point(13, 14);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(67, 13);
            this.label9.TabIndex = 2;
            this.label9.Text = "Feeder Type";
            // 
            // passiveRadio
            // 
            this.passiveRadio.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.passiveRadio.AutoSize = true;
            this.passiveRadio.Checked = true;
            this.passiveRadio.ForeColor = System.Drawing.Color.White;
            this.passiveRadio.Location = new System.Drawing.Point(86, 12);
            this.passiveRadio.Name = "passiveRadio";
            this.passiveRadio.Size = new System.Drawing.Size(62, 17);
            this.passiveRadio.TabIndex = 3;
            this.passiveRadio.TabStop = true;
            this.passiveRadio.Text = "Passive";
            this.passiveRadio.UseVisualStyleBackColor = true;
            this.passiveRadio.CheckedChanged += new System.EventHandler(this.passiveRadio_CheckedChanged);
            // 
            // activeRadio
            // 
            this.activeRadio.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.activeRadio.AutoSize = true;
            this.activeRadio.ForeColor = System.Drawing.Color.White;
            this.activeRadio.Location = new System.Drawing.Point(190, 12);
            this.activeRadio.Name = "activeRadio";
            this.activeRadio.Size = new System.Drawing.Size(55, 17);
            this.activeRadio.TabIndex = 3;
            this.activeRadio.Text = "Active";
            this.activeRadio.UseVisualStyleBackColor = true;
            this.activeRadio.CheckedChanged += new System.EventHandler(this.activeRadio_CheckedChanged);
            // 
            // lengthNumericUpDown
            // 
            this.lengthNumericUpDown.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lengthNumericUpDown.ForeColor = System.Drawing.Color.SteelBlue;
            this.lengthNumericUpDown.Location = new System.Drawing.Point(522, 34);
            this.lengthNumericUpDown.Minimum = new decimal(new int[] {
            2,
            0,
            0,
            0});
            this.lengthNumericUpDown.Name = "lengthNumericUpDown";
            this.lengthNumericUpDown.Size = new System.Drawing.Size(91, 20);
            this.lengthNumericUpDown.TabIndex = 2;
            this.lengthNumericUpDown.Value = new decimal(new int[] {
            100,
            0,
            0,
            0});
            this.lengthNumericUpDown.ValueChanged += new System.EventHandler(this.lengthNumericUpDown_ValueChanged);
            this.lengthNumericUpDown.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.lengthNumericUpDown_KeyPress);
            // 
            // label2
            // 
            this.label2.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label2.AutoSize = true;
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(562, 216);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(73, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Node Number";
            this.label2.Visible = false;
            // 
            // label3
            // 
            this.label3.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label3.AutoSize = true;
            this.label3.ForeColor = System.Drawing.Color.SteelBlue;
            this.label3.Location = new System.Drawing.Point(273, 8);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(55, 13);
            this.label3.TabIndex = 1;
            this.label3.Text = "Add Node";
            this.label3.Visible = false;
            // 
            // nodeNumCombo
            // 
            this.nodeNumCombo.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.nodeNumCombo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.nodeNumCombo.Enabled = false;
            this.nodeNumCombo.FormattingEnabled = true;
            this.nodeNumCombo.Location = new System.Drawing.Point(653, 214);
            this.nodeNumCombo.Name = "nodeNumCombo";
            this.nodeNumCombo.Size = new System.Drawing.Size(96, 21);
            this.nodeNumCombo.TabIndex = 2;
            this.nodeNumCombo.Visible = false;
            this.nodeNumCombo.TextChanged += new System.EventHandler(this.nodeNumCombo_TextChanged);
            // 
            // label7
            // 
            this.label7.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label7.AutoSize = true;
            this.label7.ForeColor = System.Drawing.Color.SteelBlue;
            this.label7.Location = new System.Drawing.Point(528, 2);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(93, 26);
            this.label7.TabIndex = 1;
            this.label7.Text = "Length from prev. Node";
            // 
            // cableSelectCombo
            // 
            this.cableSelectCombo.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.cableSelectCombo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cableSelectCombo.ForeColor = System.Drawing.Color.SteelBlue;
            this.cableSelectCombo.FormattingEnabled = true;
            this.cableSelectCombo.Location = new System.Drawing.Point(635, 33);
            this.cableSelectCombo.Name = "cableSelectCombo";
            this.cableSelectCombo.Size = new System.Drawing.Size(99, 21);
            this.cableSelectCombo.TabIndex = 2;
            this.cableSelectCombo.TextChanged += new System.EventHandler(this.cableSelectCombo_TextChanged);
            // 
            // label8
            // 
            this.label8.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label8.AutoSize = true;
            this.label8.ForeColor = System.Drawing.Color.SteelBlue;
            this.label8.Location = new System.Drawing.Point(635, 8);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(34, 13);
            this.label8.TabIndex = 1;
            this.label8.Text = "Cable";
            // 
            // nodeDataGridView
            // 
            this.nodeDataGridView.AllowUserToAddRows = false;
            this.nodeDataGridView.AllowUserToDeleteRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.nodeDataGridView.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.nodeDataGridView.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.nodeDataGridView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.nodeDataGridView.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.DisplayedCells;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.SteelBlue;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.SteelBlue;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.nodeDataGridView.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.nodeDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.nodeDataGridView.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
            this.nodeDataGridView.GridColor = System.Drawing.Color.SteelBlue;
            this.nodeDataGridView.Location = new System.Drawing.Point(3, 245);
            this.nodeDataGridView.Name = "nodeDataGridView";
            this.nodeDataGridView.RowHeadersVisible = false;
            this.nodeDataGridView.Size = new System.Drawing.Size(896, 200);
            this.nodeDataGridView.TabIndex = 4;
            this.nodeDataGridView.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.nodeDataGridView_CellEndEdit);
            this.nodeDataGridView.EditingControlShowing += new System.Windows.Forms.DataGridViewEditingControlShowingEventHandler(this.nodeDataGridView_EditingControlShowing);
            // 
            // tableLayoutPanel5
            // 
            this.tableLayoutPanel5.ColumnCount = 5;
            this.tableLayoutPanel5.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 64.54353F));
            this.tableLayoutPanel5.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 35.45647F));
            this.tableLayoutPanel5.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 169F));
            this.tableLayoutPanel5.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 129F));
            this.tableLayoutPanel5.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 228F));
            this.tableLayoutPanel5.Controls.Add(this.cancelButton, 0, 0);
            this.tableLayoutPanel5.Controls.Add(this.proceedToVCalcButton, 4, 0);
            this.tableLayoutPanel5.Controls.Add(this.selectEndNodeCombo, 1, 0);
            this.tableLayoutPanel5.Controls.Add(this.label18, 2, 0);
            this.tableLayoutPanel5.Controls.Add(this.endNodeCombo, 3, 0);
            this.tableLayoutPanel5.Location = new System.Drawing.Point(3, 451);
            this.tableLayoutPanel5.Name = "tableLayoutPanel5";
            this.tableLayoutPanel5.RowCount = 1;
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel5.Size = new System.Drawing.Size(896, 36);
            this.tableLayoutPanel5.TabIndex = 5;
            // 
            // cancelButton
            // 
            this.cancelButton.ForeColor = System.Drawing.Color.SteelBlue;
            this.cancelButton.Location = new System.Drawing.Point(3, 3);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(106, 30);
            this.cancelButton.TabIndex = 0;
            this.cancelButton.Text = "Cancel";
            this.cancelButton.UseVisualStyleBackColor = true;
            this.cancelButton.Click += new System.EventHandler(this.cancelButton_Click);
            // 
            // proceedToVCalcButton
            // 
            this.proceedToVCalcButton.Enabled = false;
            this.proceedToVCalcButton.ForeColor = System.Drawing.Color.SteelBlue;
            this.proceedToVCalcButton.Location = new System.Drawing.Point(670, 3);
            this.proceedToVCalcButton.Name = "proceedToVCalcButton";
            this.proceedToVCalcButton.Size = new System.Drawing.Size(214, 30);
            this.proceedToVCalcButton.TabIndex = 0;
            this.proceedToVCalcButton.Text = "Proceed to Voltage Drop Calculation";
            this.proceedToVCalcButton.UseVisualStyleBackColor = true;
            this.proceedToVCalcButton.Click += new System.EventHandler(this.proceedToVCalcButton_Click);
            // 
            // selectEndNodeCombo
            // 
            this.selectEndNodeCombo.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.selectEndNodeCombo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.selectEndNodeCombo.Enabled = false;
            this.selectEndNodeCombo.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.selectEndNodeCombo.ForeColor = System.Drawing.Color.SteelBlue;
            this.selectEndNodeCombo.FormattingEnabled = true;
            this.selectEndNodeCombo.Location = new System.Drawing.Point(241, 5);
            this.selectEndNodeCombo.Name = "selectEndNodeCombo";
            this.selectEndNodeCombo.Size = new System.Drawing.Size(71, 26);
            this.selectEndNodeCombo.TabIndex = 6;
            this.selectEndNodeCombo.Visible = false;
            this.selectEndNodeCombo.SelectedIndexChanged += new System.EventHandler(this.selectEndNodeCombo_SelectedIndexChanged);
            // 
            // label18
            // 
            this.label18.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label18.AutoSize = true;
            this.label18.ForeColor = System.Drawing.Color.White;
            this.label18.Location = new System.Drawing.Point(447, 11);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(88, 13);
            this.label18.TabIndex = 5;
            this.label18.Text = "Select End Node";
            // 
            // endNodeCombo
            // 
            this.endNodeCombo.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.endNodeCombo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.endNodeCombo.Enabled = false;
            this.endNodeCombo.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.endNodeCombo.ForeColor = System.Drawing.Color.SteelBlue;
            this.endNodeCombo.FormattingEnabled = true;
            this.endNodeCombo.ItemHeight = 20;
            this.endNodeCombo.Location = new System.Drawing.Point(541, 4);
            this.endNodeCombo.Name = "endNodeCombo";
            this.endNodeCombo.Size = new System.Drawing.Size(123, 28);
            this.endNodeCombo.TabIndex = 6;
            this.endNodeCombo.SelectedIndexChanged += new System.EventHandler(this.endNodeCombo_SelectedIndexChanged);
            // 
            // tableLayoutPanel4
            // 
            this.tableLayoutPanel4.BackColor = System.Drawing.SystemColors.HighlightText;
            this.tableLayoutPanel4.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.Single;
            this.tableLayoutPanel4.ColumnCount = 8;
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 21.71157F));
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 19.9683F));
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 85F));
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 33F));
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 22.79793F));
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 18.30743F));
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 17.05686F));
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 154F));
            this.tableLayoutPanel4.Controls.Add(this.lengthNumericUpDown, 5, 1);
            this.tableLayoutPanel4.Controls.Add(this.label7, 5, 0);
            this.tableLayoutPanel4.Controls.Add(this.nodeNameTextBox, 0, 1);
            this.tableLayoutPanel4.Controls.Add(this.label20, 0, 0);
            this.tableLayoutPanel4.Controls.Add(this.deleteNodeButton, 1, 1);
            this.tableLayoutPanel4.Controls.Add(this.label16, 1, 0);
            this.tableLayoutPanel4.Controls.Add(this.label22, 4, 0);
            this.tableLayoutPanel4.Controls.Add(this.nodeNameCombo, 4, 1);
            this.tableLayoutPanel4.Controls.Add(this.label3, 2, 0);
            this.tableLayoutPanel4.Controls.Add(this.addNodeButton, 2, 1);
            this.tableLayoutPanel4.Controls.Add(this.label5, 3, 1);
            this.tableLayoutPanel4.Controls.Add(this.label8, 6, 0);
            this.tableLayoutPanel4.Controls.Add(this.label17, 7, 0);
            this.tableLayoutPanel4.Controls.Add(this.nodeCount, 7, 1);
            this.tableLayoutPanel4.Controls.Add(this.cableSelectCombo, 6, 1);
            this.tableLayoutPanel4.Enabled = false;
            this.tableLayoutPanel4.Location = new System.Drawing.Point(3, 140);
            this.tableLayoutPanel4.Name = "tableLayoutPanel4";
            this.tableLayoutPanel4.RowCount = 2;
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel4.Size = new System.Drawing.Size(896, 59);
            this.tableLayoutPanel4.TabIndex = 7;
            // 
            // nodeNameTextBox
            // 
            this.nodeNameTextBox.Location = new System.Drawing.Point(4, 33);
            this.nodeNameTextBox.MaxLength = 8;
            this.nodeNameTextBox.Name = "nodeNameTextBox";
            this.nodeNameTextBox.Size = new System.Drawing.Size(127, 20);
            this.nodeNameTextBox.TabIndex = 6;
            this.nodeNameTextBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.nodeNameTextBox_KeyPress);
            this.nodeNameTextBox.Validating += new System.ComponentModel.CancelEventHandler(this.nodeNameTextBox_Validating);
            this.nodeNameTextBox.Validated += new System.EventHandler(this.nodeNameTextBox_Validated);
            // 
            // label20
            // 
            this.label20.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label20.AutoSize = true;
            this.label20.ForeColor = System.Drawing.Color.SteelBlue;
            this.label20.Location = new System.Drawing.Point(4, 8);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(70, 13);
            this.label20.TabIndex = 1;
            this.label20.Text = "Current Node";
            // 
            // deleteNodeButton
            // 
            this.deleteNodeButton.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.deleteNodeButton.Enabled = false;
            this.deleteNodeButton.ForeColor = System.Drawing.Color.SteelBlue;
            this.deleteNodeButton.Location = new System.Drawing.Point(138, 33);
            this.deleteNodeButton.Name = "deleteNodeButton";
            this.deleteNodeButton.Size = new System.Drawing.Size(116, 22);
            this.deleteNodeButton.TabIndex = 4;
            this.deleteNodeButton.Text = "Delete Current Node";
            this.deleteNodeButton.UseVisualStyleBackColor = true;
            this.deleteNodeButton.Click += new System.EventHandler(this.deleteNodeButton_Click);
            // 
            // label16
            // 
            this.label16.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label16.AutoSize = true;
            this.label16.ForeColor = System.Drawing.Color.SteelBlue;
            this.label16.Location = new System.Drawing.Point(162, 8);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(67, 13);
            this.label16.TabIndex = 1;
            this.label16.Text = "Delete Node";
            this.label16.Visible = false;
            // 
            // label22
            // 
            this.label22.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label22.AutoSize = true;
            this.label22.ForeColor = System.Drawing.Color.SteelBlue;
            this.label22.Location = new System.Drawing.Point(381, 8);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(66, 13);
            this.label22.TabIndex = 1;
            this.label22.Text = "Node Select";
            // 
            // nodeNameCombo
            // 
            this.nodeNameCombo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.nodeNameCombo.FormattingEnabled = true;
            this.nodeNameCombo.Location = new System.Drawing.Point(381, 33);
            this.nodeNameCombo.Name = "nodeNameCombo";
            this.nodeNameCombo.Size = new System.Drawing.Size(116, 21);
            this.nodeNameCombo.TabIndex = 7;
            this.nodeNameCombo.TextChanged += new System.EventHandler(this.nodeNameCombo_TextChanged);
            // 
            // addNodeButton
            // 
            this.addNodeButton.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.addNodeButton.Enabled = false;
            this.addNodeButton.ForeColor = System.Drawing.Color.SteelBlue;
            this.addNodeButton.Location = new System.Drawing.Point(262, 33);
            this.addNodeButton.Name = "addNodeButton";
            this.addNodeButton.Size = new System.Drawing.Size(78, 22);
            this.addNodeButton.TabIndex = 4;
            this.addNodeButton.Text = "Add Node";
            this.addNodeButton.UseVisualStyleBackColor = true;
            this.addNodeButton.Click += new System.EventHandler(this.addNodeButton_Click);
            // 
            // label5
            // 
            this.label5.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label5.AutoSize = true;
            this.label5.ForeColor = System.Drawing.Color.SteelBlue;
            this.label5.Location = new System.Drawing.Point(351, 37);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(23, 13);
            this.label5.TabIndex = 1;
            this.label5.Text = "To:";
            // 
            // label17
            // 
            this.label17.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label17.AutoSize = true;
            this.label17.ForeColor = System.Drawing.Color.SteelBlue;
            this.label17.Location = new System.Drawing.Point(741, 8);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(117, 13);
            this.label17.TabIndex = 5;
            this.label17.Text = "Total Number of Nodes";
            // 
            // nodeCount
            // 
            this.nodeCount.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.nodeCount.AutoSize = true;
            this.nodeCount.ForeColor = System.Drawing.Color.SteelBlue;
            this.nodeCount.Location = new System.Drawing.Point(741, 37);
            this.nodeCount.Name = "nodeCount";
            this.nodeCount.Size = new System.Drawing.Size(13, 13);
            this.nodeCount.TabIndex = 5;
            this.nodeCount.Text = "1";
            // 
            // drawingPanel
            // 
            this.drawingPanel.BackColor = System.Drawing.Color.White;
            this.drawingPanel.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.drawingPanel.Location = new System.Drawing.Point(4, 30);
            this.drawingPanel.Name = "drawingPanel";
            this.drawingPanel.Size = new System.Drawing.Size(888, 87);
            this.drawingPanel.TabIndex = 8;
            // 
            // tableLayoutPanel3
            // 
            this.tableLayoutPanel3.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.Single;
            this.tableLayoutPanel3.ColumnCount = 1;
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel3.Controls.Add(this.label19, 0, 0);
            this.tableLayoutPanel3.Controls.Add(this.drawingPanel, 0, 1);
            this.tableLayoutPanel3.Location = new System.Drawing.Point(3, 488);
            this.tableLayoutPanel3.Name = "tableLayoutPanel3";
            this.tableLayoutPanel3.RowCount = 2;
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 93F));
            this.tableLayoutPanel3.Size = new System.Drawing.Size(896, 121);
            this.tableLayoutPanel3.TabIndex = 9;
            // 
            // label19
            // 
            this.label19.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label19.AutoSize = true;
            this.label19.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label19.ForeColor = System.Drawing.Color.White;
            this.label19.Location = new System.Drawing.Point(336, 1);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(223, 24);
            this.label19.TabIndex = 3;
            this.label19.Text = "Main Feeder Topology";
            // 
            // detailsCheckBox
            // 
            this.detailsCheckBox.AutoSize = true;
            this.detailsCheckBox.Enabled = false;
            this.detailsCheckBox.ForeColor = System.Drawing.Color.White;
            this.detailsCheckBox.Location = new System.Drawing.Point(805, 216);
            this.detailsCheckBox.Name = "detailsCheckBox";
            this.detailsCheckBox.Size = new System.Drawing.Size(84, 17);
            this.detailsCheckBox.TabIndex = 11;
            this.detailsCheckBox.Text = "View Details";
            this.detailsCheckBox.UseVisualStyleBackColor = true;
            this.detailsCheckBox.CheckedChanged += new System.EventHandler(this.detailsCheckBox_CheckedChanged);
            // 
            // searchTextBox
            // 
            this.searchTextBox.Location = new System.Drawing.Point(102, 214);
            this.searchTextBox.Name = "searchTextBox";
            this.searchTextBox.Size = new System.Drawing.Size(100, 20);
            this.searchTextBox.TabIndex = 12;
            this.searchTextBox.KeyUp += new System.Windows.Forms.KeyEventHandler(this.searchTextBox_KeyUp);
            // 
            // label4
            // 
            this.label4.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label4.AutoSize = true;
            this.label4.ForeColor = System.Drawing.Color.White;
            this.label4.Location = new System.Drawing.Point(7, 218);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(89, 13);
            this.label4.TabIndex = 13;
            this.label4.Text = "Search Load/DG";
            // 
            // label6
            // 
            this.label6.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label6.AutoSize = true;
            this.label6.ForeColor = System.Drawing.Color.White;
            this.label6.Location = new System.Drawing.Point(285, 8);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(80, 26);
            this.label6.TabIndex = 2;
            this.label6.Text = "Source Voltage [V]";
            // 
            // label10
            // 
            this.label10.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label10.AutoSize = true;
            this.label10.ForeColor = System.Drawing.Color.White;
            this.label10.Location = new System.Drawing.Point(497, 14);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(84, 13);
            this.label10.TabIndex = 2;
            this.label10.Text = "Temperature[°C]";
            // 
            // sourceVoltageTextBox
            // 
            this.sourceVoltageTextBox.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.sourceVoltageTextBox.Location = new System.Drawing.Point(379, 11);
            this.sourceVoltageTextBox.Name = "sourceVoltageTextBox";
            this.sourceVoltageTextBox.Size = new System.Drawing.Size(100, 20);
            this.sourceVoltageTextBox.TabIndex = 4;
            this.sourceVoltageTextBox.Text = "230";
            this.sourceVoltageTextBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.sourceVoltageTextBox_KeyPress);
            this.sourceVoltageTextBox.Validating += new System.ComponentModel.CancelEventHandler(this.sourceVoltageTextBox_Validating);
            this.sourceVoltageTextBox.Validated += new System.EventHandler(this.sourceVoltageTextBox_Validated);
            // 
            // operatingTemperatureText
            // 
            this.operatingTemperatureText.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.operatingTemperatureText.Location = new System.Drawing.Point(587, 11);
            this.operatingTemperatureText.Name = "operatingTemperatureText";
            this.operatingTemperatureText.Size = new System.Drawing.Size(80, 20);
            this.operatingTemperatureText.TabIndex = 4;
            this.operatingTemperatureText.Text = "40";
            this.operatingTemperatureText.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.operatingTemperatureText_KeyPress);
            this.operatingTemperatureText.Validating += new System.ComponentModel.CancelEventHandler(this.operatingTemperatureText_Validating);
            this.operatingTemperatureText.Validated += new System.EventHandler(this.operatingTemperatureText_Validated);
            // 
            // nodeFeederForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.SteelBlue;
            this.ClientSize = new System.Drawing.Size(902, 614);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.searchTextBox);
            this.Controls.Add(this.detailsCheckBox);
            this.Controls.Add(this.tableLayoutPanel3);
            this.Controls.Add(this.tableLayoutPanel4);
            this.Controls.Add(this.tableLayoutPanel5);
            this.Controls.Add(this.nodeDataGridView);
            this.Controls.Add(this.tableLayoutPanel2);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Controls.Add(this.nodeNumCombo);
            this.Controls.Add(this.label2);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "nodeFeederForm";
            this.Text = "New Feeder Design";
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.lengthNumericUpDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nodeDataGridView)).EndInit();
            this.tableLayoutPanel5.ResumeLayout(false);
            this.tableLayoutPanel5.PerformLayout();
            this.tableLayoutPanel4.ResumeLayout(false);
            this.tableLayoutPanel4.PerformLayout();
            this.tableLayoutPanel3.ResumeLayout(false);
            this.tableLayoutPanel3.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox projectNameTextBox;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.NumericUpDown lengthNumericUpDown;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox nodeNumCombo;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.ComboBox cableSelectCombo;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.DataGridView nodeDataGridView;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel5;
        private System.Windows.Forms.Button proceedToVCalcButton;
        private System.Windows.Forms.Button cancelButton;
        private System.Windows.Forms.Button continueButton;
        private System.Windows.Forms.Button createNewProjectButton;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel4;
        private System.Windows.Forms.RadioButton passiveRadio;
        private System.Windows.Forms.RadioButton activeRadio;
        private System.Windows.Forms.Button addNodeButton;
        private System.Windows.Forms.Button deleteNodeButton;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Label nodeCount;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Panel drawingPanel;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.ComboBox selectEndNodeCombo;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel3;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.TextBox nodeNameTextBox;
        private System.Windows.Forms.Label label22;
        private System.Windows.Forms.ComboBox nodeNameCombo;
        private System.Windows.Forms.ComboBox endNodeCombo;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.CheckBox detailsCheckBox;
        private System.Windows.Forms.TextBox searchTextBox;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox sourceVoltageTextBox;
        private System.Windows.Forms.TextBox operatingTemperatureText;

    }
}