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
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.label6 = new System.Windows.Forms.Label();
            this.passiveRadio = new System.Windows.Forms.RadioButton();
            this.activeRadio = new System.Windows.Forms.RadioButton();
            this.label10 = new System.Windows.Forms.Label();
            this.operatingTempNumUpDown = new System.Windows.Forms.NumericUpDown();
            this.sourceVoltageNumUpDown = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
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
            this.label19 = new System.Windows.Forms.Label();
            this.detailsCheckBox = new System.Windows.Forms.CheckBox();
            this.searchTextBox = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.saveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveAsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.loadsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel6 = new System.Windows.Forms.TableLayoutPanel();
            this.nominalVoltageUpDown = new System.Windows.Forms.NumericUpDown();
            this.label11 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.tableLayoutPanel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.operatingTempNumUpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.sourceVoltageNumUpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lengthNumericUpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nodeDataGridView)).BeginInit();
            this.tableLayoutPanel5.SuspendLayout();
            this.tableLayoutPanel4.SuspendLayout();
            this.drawingPanel.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.tableLayoutPanel6.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nominalVoltageUpDown)).BeginInit();
            this.SuspendLayout();
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.Single;
            this.tableLayoutPanel2.ColumnCount = 5;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel2.Controls.Add(this.label6, 2, 0);
            this.tableLayoutPanel2.Controls.Add(this.passiveRadio, 0, 1);
            this.tableLayoutPanel2.Controls.Add(this.activeRadio, 1, 1);
            this.tableLayoutPanel2.Controls.Add(this.label10, 3, 0);
            this.tableLayoutPanel2.Controls.Add(this.label11, 4, 0);
            this.tableLayoutPanel2.Controls.Add(this.operatingTempNumUpDown, 3, 1);
            this.tableLayoutPanel2.Controls.Add(this.sourceVoltageNumUpDown, 2, 1);
            this.tableLayoutPanel2.Controls.Add(this.nominalVoltageUpDown, 4, 1);
            this.tableLayoutPanel2.Controls.Add(this.label1, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.label9, 1, 0);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 2;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 29F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 39F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(891, 63);
            this.tableLayoutPanel2.TabIndex = 1;
            // 
            // label6
            // 
            this.label6.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label6.AutoSize = true;
            this.label6.ForeColor = System.Drawing.Color.White;
            this.label6.Location = new System.Drawing.Point(397, 9);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(96, 13);
            this.label6.TabIndex = 2;
            this.label6.Text = "Source Voltage [V]";
            this.label6.Click += new System.EventHandler(this.label6_Click);
            // 
            // passiveRadio
            // 
            this.passiveRadio.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.passiveRadio.AutoSize = true;
            this.passiveRadio.Checked = true;
            this.passiveRadio.ForeColor = System.Drawing.Color.White;
            this.passiveRadio.Location = new System.Drawing.Point(82, 44);
            this.passiveRadio.Name = "passiveRadio";
            this.passiveRadio.Size = new System.Drawing.Size(14, 13);
            this.passiveRadio.TabIndex = 3;
            this.passiveRadio.TabStop = true;
            this.passiveRadio.UseVisualStyleBackColor = true;
            this.passiveRadio.CheckedChanged += new System.EventHandler(this.passiveRadio_CheckedChanged);
            // 
            // activeRadio
            // 
            this.activeRadio.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.activeRadio.AutoSize = true;
            this.activeRadio.ForeColor = System.Drawing.Color.White;
            this.activeRadio.Location = new System.Drawing.Point(260, 44);
            this.activeRadio.Name = "activeRadio";
            this.activeRadio.Size = new System.Drawing.Size(14, 13);
            this.activeRadio.TabIndex = 3;
            this.activeRadio.UseVisualStyleBackColor = true;
            this.activeRadio.CheckedChanged += new System.EventHandler(this.activeRadio_CheckedChanged);
            // 
            // label10
            // 
            this.label10.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label10.AutoSize = true;
            this.label10.ForeColor = System.Drawing.Color.White;
            this.label10.Location = new System.Drawing.Point(581, 9);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(84, 13);
            this.label10.TabIndex = 2;
            this.label10.Text = "Temperature[°C]";
            // 
            // operatingTempNumUpDown
            // 
            this.operatingTempNumUpDown.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.operatingTempNumUpDown.DecimalPlaces = 2;
            this.operatingTempNumUpDown.Increment = new decimal(new int[] {
            1,
            0,
            0,
            131072});
            this.operatingTempNumUpDown.Location = new System.Drawing.Point(568, 40);
            this.operatingTempNumUpDown.Name = "operatingTempNumUpDown";
            this.operatingTempNumUpDown.Size = new System.Drawing.Size(110, 20);
            this.operatingTempNumUpDown.TabIndex = 5;
            this.operatingTempNumUpDown.Value = new decimal(new int[] {
            40,
            0,
            0,
            0});
            this.operatingTempNumUpDown.ValueChanged += new System.EventHandler(this.operatingTempNumUpDown_ValueChanged);
            // 
            // sourceVoltageNumUpDown
            // 
            this.sourceVoltageNumUpDown.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.sourceVoltageNumUpDown.DecimalPlaces = 2;
            this.sourceVoltageNumUpDown.Location = new System.Drawing.Point(390, 40);
            this.sourceVoltageNumUpDown.Maximum = new decimal(new int[] {
            400,
            0,
            0,
            0});
            this.sourceVoltageNumUpDown.Name = "sourceVoltageNumUpDown";
            this.sourceVoltageNumUpDown.Size = new System.Drawing.Size(110, 20);
            this.sourceVoltageNumUpDown.TabIndex = 6;
            this.sourceVoltageNumUpDown.Value = new decimal(new int[] {
            230,
            0,
            0,
            0});
            this.sourceVoltageNumUpDown.ValueChanged += new System.EventHandler(this.sourceVoltageNumUpDown_ValueChanged);
            this.sourceVoltageNumUpDown.Enter += new System.EventHandler(this.sourceVoltageNumUpDown_Enter);
            this.sourceVoltageNumUpDown.MouseClick += new System.Windows.Forms.MouseEventHandler(this.sourceVoltageNumUpDown_MouseClick);
            this.sourceVoltageNumUpDown.Validated += new System.EventHandler(this.sourceVoltageNumUpDown_Validated);
            // 
            // label1
            // 
            this.label1.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(49, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(80, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Passive Feeder";
            this.label1.Click += new System.EventHandler(this.label9_Click);
            // 
            // label9
            // 
            this.label9.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label9.AutoSize = true;
            this.label9.ForeColor = System.Drawing.Color.White;
            this.label9.Location = new System.Drawing.Point(231, 9);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(73, 13);
            this.label9.TabIndex = 2;
            this.label9.Text = "Active Feeder";
            this.label9.Click += new System.EventHandler(this.label9_Click);
            // 
            // lengthNumericUpDown
            // 
            this.lengthNumericUpDown.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lengthNumericUpDown.ForeColor = System.Drawing.Color.SteelBlue;
            this.lengthNumericUpDown.Location = new System.Drawing.Point(514, 44);
            this.lengthNumericUpDown.Minimum = new decimal(new int[] {
            2,
            0,
            0,
            0});
            this.lengthNumericUpDown.Name = "lengthNumericUpDown";
            this.lengthNumericUpDown.Size = new System.Drawing.Size(123, 20);
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
            this.label2.Location = new System.Drawing.Point(204, 9);
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
            this.label3.Location = new System.Drawing.Point(248, 11);
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
            this.nodeNumCombo.Location = new System.Drawing.Point(609, 5);
            this.nodeNumCombo.Name = "nodeNumCombo";
            this.nodeNumCombo.Size = new System.Drawing.Size(77, 21);
            this.nodeNumCombo.TabIndex = 2;
            this.nodeNumCombo.Visible = false;
            this.nodeNumCombo.TextChanged += new System.EventHandler(this.nodeNumCombo_TextChanged);
            // 
            // label7
            // 
            this.label7.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label7.AutoSize = true;
            this.label7.ForeColor = System.Drawing.Color.SteelBlue;
            this.label7.Location = new System.Drawing.Point(516, 11);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(119, 13);
            this.label7.TabIndex = 1;
            this.label7.Text = "Length from prev. Node";
            // 
            // cableSelectCombo
            // 
            this.cableSelectCombo.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.cableSelectCombo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cableSelectCombo.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cableSelectCombo.ForeColor = System.Drawing.Color.SteelBlue;
            this.cableSelectCombo.FormattingEnabled = true;
            this.cableSelectCombo.Location = new System.Drawing.Point(644, 44);
            this.cableSelectCombo.Name = "cableSelectCombo";
            this.cableSelectCombo.Size = new System.Drawing.Size(90, 21);
            this.cableSelectCombo.TabIndex = 2;
            this.cableSelectCombo.SelectedIndexChanged += new System.EventHandler(this.cableSelectCombo_SelectedIndexChanged);
            this.cableSelectCombo.TextChanged += new System.EventHandler(this.cableSelectCombo_TextChanged);
            // 
            // label8
            // 
            this.label8.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label8.AutoSize = true;
            this.label8.ForeColor = System.Drawing.Color.SteelBlue;
            this.label8.Location = new System.Drawing.Point(672, 11);
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
            this.nodeDataGridView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.nodeDataGridView.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
            this.nodeDataGridView.GridColor = System.Drawing.Color.SteelBlue;
            this.nodeDataGridView.Location = new System.Drawing.Point(3, 189);
            this.nodeDataGridView.Name = "nodeDataGridView";
            this.nodeDataGridView.RowHeadersVisible = false;
            this.nodeDataGridView.Size = new System.Drawing.Size(891, 193);
            this.nodeDataGridView.TabIndex = 4;
            this.nodeDataGridView.CellBeginEdit += new System.Windows.Forms.DataGridViewCellCancelEventHandler(this.nodeDataGridView_CellBeginEdit);
            this.nodeDataGridView.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.nodeDataGridView_CellEndEdit);
            this.nodeDataGridView.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.nodeDataGridView_CellFormatting);
            this.nodeDataGridView.CellLeave += new System.Windows.Forms.DataGridViewCellEventHandler(this.nodeDataGridView_CellLeave);
            this.nodeDataGridView.CellValidated += new System.Windows.Forms.DataGridViewCellEventHandler(this.nodeDataGridView_CellValidated);
            this.nodeDataGridView.EditingControlShowing += new System.Windows.Forms.DataGridViewEditingControlShowingEventHandler(this.nodeDataGridView_EditingControlShowing);
            // 
            // tableLayoutPanel5
            // 
            this.tableLayoutPanel5.ColumnCount = 5;
            this.tableLayoutPanel5.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 64.54353F));
            this.tableLayoutPanel5.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 35.45647F));
            this.tableLayoutPanel5.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 116F));
            this.tableLayoutPanel5.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 129F));
            this.tableLayoutPanel5.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 241F));
            this.tableLayoutPanel5.Controls.Add(this.proceedToVCalcButton, 4, 0);
            this.tableLayoutPanel5.Controls.Add(this.selectEndNodeCombo, 1, 0);
            this.tableLayoutPanel5.Controls.Add(this.label18, 2, 0);
            this.tableLayoutPanel5.Controls.Add(this.endNodeCombo, 3, 0);
            this.tableLayoutPanel5.Controls.Add(this.cancelButton, 0, 0);
            this.tableLayoutPanel5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel5.Location = new System.Drawing.Point(3, 388);
            this.tableLayoutPanel5.Name = "tableLayoutPanel5";
            this.tableLayoutPanel5.RowCount = 1;
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel5.Size = new System.Drawing.Size(891, 40);
            this.tableLayoutPanel5.TabIndex = 5;
            // 
            // cancelButton
            // 
            this.cancelButton.ForeColor = System.Drawing.Color.SteelBlue;
            this.cancelButton.Location = new System.Drawing.Point(3, 3);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(255, 31);
            this.cancelButton.TabIndex = 0;
            this.cancelButton.Text = "Cancel";
            this.cancelButton.UseVisualStyleBackColor = true;
            this.cancelButton.Click += new System.EventHandler(this.cancelButton_Click);
            // 
            // proceedToVCalcButton
            // 
            this.proceedToVCalcButton.Enabled = false;
            this.proceedToVCalcButton.ForeColor = System.Drawing.Color.SteelBlue;
            this.proceedToVCalcButton.Location = new System.Drawing.Point(652, 3);
            this.proceedToVCalcButton.Name = "proceedToVCalcButton";
            this.proceedToVCalcButton.Size = new System.Drawing.Size(236, 31);
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
            this.selectEndNodeCombo.Location = new System.Drawing.Point(264, 7);
            this.selectEndNodeCombo.Name = "selectEndNodeCombo";
            this.selectEndNodeCombo.Size = new System.Drawing.Size(137, 26);
            this.selectEndNodeCombo.TabIndex = 6;
            this.selectEndNodeCombo.Visible = false;
            this.selectEndNodeCombo.SelectedIndexChanged += new System.EventHandler(this.selectEndNodeCombo_SelectedIndexChanged);
            // 
            // label18
            // 
            this.label18.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label18.AutoSize = true;
            this.label18.ForeColor = System.Drawing.Color.White;
            this.label18.Location = new System.Drawing.Point(429, 13);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(88, 13);
            this.label18.TabIndex = 5;
            this.label18.Text = "Select End Node";
            // 
            // endNodeCombo
            // 
            this.endNodeCombo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.endNodeCombo.Enabled = false;
            this.endNodeCombo.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.endNodeCombo.ForeColor = System.Drawing.Color.SteelBlue;
            this.endNodeCombo.FormattingEnabled = true;
            this.endNodeCombo.ItemHeight = 20;
            this.endNodeCombo.Location = new System.Drawing.Point(523, 3);
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
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 12.5F));
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 12.5F));
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 12.5F));
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 7.865169F));
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 12.58427F));
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 14.83146F));
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 11.01124F));
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 17.07865F));
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
            this.tableLayoutPanel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel4.Enabled = false;
            this.tableLayoutPanel4.Location = new System.Drawing.Point(3, 72);
            this.tableLayoutPanel4.Name = "tableLayoutPanel4";
            this.tableLayoutPanel4.RowCount = 2;
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 48.3871F));
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 51.6129F));
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel4.Size = new System.Drawing.Size(891, 74);
            this.tableLayoutPanel4.TabIndex = 7;
            // 
            // nodeNameTextBox
            // 
            this.nodeNameTextBox.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.nodeNameTextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.nodeNameTextBox.Location = new System.Drawing.Point(4, 43);
            this.nodeNameTextBox.MaxLength = 8;
            this.nodeNameTextBox.Name = "nodeNameTextBox";
            this.nodeNameTextBox.Size = new System.Drawing.Size(103, 22);
            this.nodeNameTextBox.TabIndex = 6;
            this.nodeNameTextBox.TextChanged += new System.EventHandler(this.nodeNameTextBox_TextChanged);
            this.nodeNameTextBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.nodeNameTextBox_KeyPress);
            this.nodeNameTextBox.Validating += new System.ComponentModel.CancelEventHandler(this.nodeNameTextBox_Validating);
            this.nodeNameTextBox.Validated += new System.EventHandler(this.nodeNameTextBox_Validated);
            // 
            // label20
            // 
            this.label20.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label20.AutoSize = true;
            this.label20.ForeColor = System.Drawing.Color.SteelBlue;
            this.label20.Location = new System.Drawing.Point(23, 11);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(64, 13);
            this.label20.TabIndex = 1;
            this.label20.Text = "Node Name";
            // 
            // deleteNodeButton
            // 
            this.deleteNodeButton.Dock = System.Windows.Forms.DockStyle.Fill;
            this.deleteNodeButton.Enabled = false;
            this.deleteNodeButton.ForeColor = System.Drawing.Color.SteelBlue;
            this.deleteNodeButton.Location = new System.Drawing.Point(114, 39);
            this.deleteNodeButton.Name = "deleteNodeButton";
            this.deleteNodeButton.Size = new System.Drawing.Size(103, 31);
            this.deleteNodeButton.TabIndex = 4;
            this.deleteNodeButton.Text = "Delete Node";
            this.deleteNodeButton.UseVisualStyleBackColor = true;
            this.deleteNodeButton.Click += new System.EventHandler(this.deleteNodeButton_Click);
            // 
            // label16
            // 
            this.label16.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label16.AutoSize = true;
            this.label16.ForeColor = System.Drawing.Color.SteelBlue;
            this.label16.Location = new System.Drawing.Point(132, 11);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(67, 13);
            this.label16.TabIndex = 1;
            this.label16.Text = "Delete Node";
            this.label16.Visible = false;
            // 
            // label22
            // 
            this.label22.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label22.AutoSize = true;
            this.label22.ForeColor = System.Drawing.Color.SteelBlue;
            this.label22.Location = new System.Drawing.Point(422, 11);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(66, 13);
            this.label22.TabIndex = 1;
            this.label22.Text = "Node Select";
            // 
            // nodeNameCombo
            // 
            this.nodeNameCombo.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.nodeNameCombo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.nodeNameCombo.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.nodeNameCombo.ForeColor = System.Drawing.Color.SteelBlue;
            this.nodeNameCombo.FormattingEnabled = true;
            this.nodeNameCombo.Location = new System.Drawing.Point(403, 44);
            this.nodeNameCombo.Name = "nodeNameCombo";
            this.nodeNameCombo.Size = new System.Drawing.Size(104, 21);
            this.nodeNameCombo.TabIndex = 7;
            this.nodeNameCombo.SelectedIndexChanged += new System.EventHandler(this.nodeNameCombo_SelectedIndexChanged);
            this.nodeNameCombo.TextChanged += new System.EventHandler(this.nodeNameCombo_TextChanged);
            // 
            // addNodeButton
            // 
            this.addNodeButton.Dock = System.Windows.Forms.DockStyle.Fill;
            this.addNodeButton.Enabled = false;
            this.addNodeButton.ForeColor = System.Drawing.Color.SteelBlue;
            this.addNodeButton.Location = new System.Drawing.Point(224, 39);
            this.addNodeButton.Name = "addNodeButton";
            this.addNodeButton.Size = new System.Drawing.Size(103, 31);
            this.addNodeButton.TabIndex = 4;
            this.addNodeButton.Text = "Add Node";
            this.addNodeButton.UseVisualStyleBackColor = true;
            this.addNodeButton.Click += new System.EventHandler(this.addNodeButton_Click);
            // 
            // label5
            // 
            this.label5.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label5.AutoSize = true;
            this.label5.ForeColor = System.Drawing.Color.SteelBlue;
            this.label5.Location = new System.Drawing.Point(334, 48);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(23, 13);
            this.label5.TabIndex = 1;
            this.label5.Text = "To:";
            // 
            // label17
            // 
            this.label17.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label17.AutoSize = true;
            this.label17.ForeColor = System.Drawing.Color.SteelBlue;
            this.label17.Location = new System.Drawing.Point(755, 11);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(117, 13);
            this.label17.TabIndex = 5;
            this.label17.Text = "Total Number of Nodes";
            // 
            // nodeCount
            // 
            this.nodeCount.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.nodeCount.AutoSize = true;
            this.nodeCount.ForeColor = System.Drawing.Color.SteelBlue;
            this.nodeCount.Location = new System.Drawing.Point(807, 48);
            this.nodeCount.Name = "nodeCount";
            this.nodeCount.Size = new System.Drawing.Size(13, 13);
            this.nodeCount.TabIndex = 5;
            this.nodeCount.Text = "1";
            // 
            // drawingPanel
            // 
            this.drawingPanel.AutoSize = true;
            this.drawingPanel.BackColor = System.Drawing.Color.White;
            this.drawingPanel.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.drawingPanel.Controls.Add(this.label13);
            this.drawingPanel.Controls.Add(this.label12);
            this.drawingPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.drawingPanel.Location = new System.Drawing.Point(3, 459);
            this.drawingPanel.Name = "drawingPanel";
            this.drawingPanel.Size = new System.Drawing.Size(891, 80);
            this.drawingPanel.TabIndex = 8;
            this.drawingPanel.Paint += new System.Windows.Forms.PaintEventHandler(this.drawingPanel_Paint);
            // 
            // label19
            // 
            this.label19.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label19.AutoSize = true;
            this.label19.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label19.ForeColor = System.Drawing.Color.White;
            this.label19.Location = new System.Drawing.Point(337, 431);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(223, 24);
            this.label19.TabIndex = 3;
            this.label19.Text = "Main Feeder Topology";
            // 
            // detailsCheckBox
            // 
            this.detailsCheckBox.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.detailsCheckBox.AutoSize = true;
            this.detailsCheckBox.Enabled = false;
            this.detailsCheckBox.ForeColor = System.Drawing.Color.White;
            this.detailsCheckBox.Location = new System.Drawing.Point(804, 7);
            this.detailsCheckBox.Name = "detailsCheckBox";
            this.detailsCheckBox.Size = new System.Drawing.Size(84, 17);
            this.detailsCheckBox.TabIndex = 11;
            this.detailsCheckBox.Text = "View Details";
            this.detailsCheckBox.UseVisualStyleBackColor = true;
            this.detailsCheckBox.CheckedChanged += new System.EventHandler(this.detailsCheckBox_CheckedChanged);
            // 
            // searchTextBox
            // 
            this.searchTextBox.Location = new System.Drawing.Point(98, 3);
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
            this.label4.Location = new System.Drawing.Point(3, 9);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(89, 13);
            this.label4.TabIndex = 13;
            this.label4.Text = "Search Load/DG";
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.helpToolStripMenuItem,
            this.helpToolStripMenuItem1});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.RenderMode = System.Windows.Forms.ToolStripRenderMode.Professional;
            this.menuStrip1.Size = new System.Drawing.Size(897, 24);
            this.menuStrip1.TabIndex = 14;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openToolStripMenuItem,
            this.toolStripSeparator1,
            this.saveToolStripMenuItem,
            this.saveAsToolStripMenuItem,
            this.toolStripSeparator2,
            this.exitToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // openToolStripMenuItem
            // 
            this.openToolStripMenuItem.Name = "openToolStripMenuItem";
            this.openToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.openToolStripMenuItem.Text = "Open";
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(149, 6);
            // 
            // saveToolStripMenuItem
            // 
            this.saveToolStripMenuItem.Name = "saveToolStripMenuItem";
            this.saveToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.saveToolStripMenuItem.Text = "Save";
            this.saveToolStripMenuItem.Click += new System.EventHandler(this.saveToolStripMenuItem_Click);
            // 
            // saveAsToolStripMenuItem
            // 
            this.saveAsToolStripMenuItem.Name = "saveAsToolStripMenuItem";
            this.saveAsToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.saveAsToolStripMenuItem.Text = "Save As";
            this.saveAsToolStripMenuItem.Click += new System.EventHandler(this.saveAsToolStripMenuItem_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(149, 6);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.exitToolStripMenuItem.Text = "Exit";
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.loadsToolStripMenuItem});
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            this.helpToolStripMenuItem.Size = new System.Drawing.Size(39, 20);
            this.helpToolStripMenuItem.Text = "Edit";
            // 
            // loadsToolStripMenuItem
            // 
            this.loadsToolStripMenuItem.Name = "loadsToolStripMenuItem";
            this.loadsToolStripMenuItem.Size = new System.Drawing.Size(118, 22);
            this.loadsToolStripMenuItem.Text = "Libraries";
            this.loadsToolStripMenuItem.Click += new System.EventHandler(this.loadsToolStripMenuItem_Click);
            // 
            // helpToolStripMenuItem1
            // 
            this.helpToolStripMenuItem1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.aboutToolStripMenuItem});
            this.helpToolStripMenuItem1.Name = "helpToolStripMenuItem1";
            this.helpToolStripMenuItem1.Size = new System.Drawing.Size(44, 20);
            this.helpToolStripMenuItem1.Text = "Help";
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            this.aboutToolStripMenuItem.Size = new System.Drawing.Size(107, 22);
            this.aboutToolStripMenuItem.Text = "About";
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.drawingPanel, 0, 6);
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel2, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel4, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.nodeDataGridView, 0, 3);
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel5, 0, 4);
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel6, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.label19, 0, 5);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 24);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 7;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 69F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 80F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 37F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 70F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 46F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 30F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(897, 542);
            this.tableLayoutPanel1.TabIndex = 15;
            this.tableLayoutPanel1.Paint += new System.Windows.Forms.PaintEventHandler(this.tableLayoutPanel1_Paint);
            // 
            // tableLayoutPanel6
            // 
            this.tableLayoutPanel6.ColumnCount = 5;
            this.tableLayoutPanel6.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel6.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel6.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 405F));
            this.tableLayoutPanel6.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 83F));
            this.tableLayoutPanel6.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 105F));
            this.tableLayoutPanel6.Controls.Add(this.label4, 0, 0);
            this.tableLayoutPanel6.Controls.Add(this.searchTextBox, 1, 0);
            this.tableLayoutPanel6.Controls.Add(this.label2, 2, 0);
            this.tableLayoutPanel6.Controls.Add(this.nodeNumCombo, 3, 0);
            this.tableLayoutPanel6.Controls.Add(this.detailsCheckBox, 4, 0);
            this.tableLayoutPanel6.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel6.Location = new System.Drawing.Point(3, 152);
            this.tableLayoutPanel6.Name = "tableLayoutPanel6";
            this.tableLayoutPanel6.RowCount = 1;
            this.tableLayoutPanel6.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel6.Size = new System.Drawing.Size(891, 31);
            this.tableLayoutPanel6.TabIndex = 8;
            this.tableLayoutPanel6.Paint += new System.Windows.Forms.PaintEventHandler(this.tableLayoutPanel6_Paint);
            // 
            // nominalVoltageUpDown
            // 
            this.nominalVoltageUpDown.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.nominalVoltageUpDown.DecimalPlaces = 2;
            this.nominalVoltageUpDown.Location = new System.Drawing.Point(746, 40);
            this.nominalVoltageUpDown.Maximum = new decimal(new int[] {
            400,
            0,
            0,
            0});
            this.nominalVoltageUpDown.Name = "nominalVoltageUpDown";
            this.nominalVoltageUpDown.Size = new System.Drawing.Size(110, 20);
            this.nominalVoltageUpDown.TabIndex = 7;
            this.nominalVoltageUpDown.Value = new decimal(new int[] {
            230,
            0,
            0,
            0});
            this.nominalVoltageUpDown.ValueChanged += new System.EventHandler(this.nominalVoltageUpDown_ValueChanged);
            // 
            // label11
            // 
            this.label11.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label11.AutoSize = true;
            this.label11.ForeColor = System.Drawing.Color.White;
            this.label11.Location = new System.Drawing.Point(751, 9);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(100, 13);
            this.label11.TabIndex = 2;
            this.label11.Text = "Nominal Voltage [V]";
            this.label11.Click += new System.EventHandler(this.label6_Click);
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.ForeColor = System.Drawing.Color.SteelBlue;
            this.label12.Location = new System.Drawing.Point(-2, 13);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(33, 13);
            this.label12.TabIndex = 2;
            this.label12.Text = "Node";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.ForeColor = System.Drawing.Color.SteelBlue;
            this.label13.Location = new System.Drawing.Point(-2, 0);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(41, 13);
            this.label13.TabIndex = 2;
            this.label13.Text = "Source";
            // 
            // nodeFeederForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.SteelBlue;
            this.ClientSize = new System.Drawing.Size(897, 566);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Controls.Add(this.menuStrip1);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.MaximumSize = new System.Drawing.Size(1366, 768);
            this.MinimumSize = new System.Drawing.Size(917, 608);
            this.Name = "nodeFeederForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "New Feeder Design";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.nodeFeederForm_FormClosing);
            this.Load += new System.EventHandler(this.nodeFeederForm_Load);
            this.Shown += new System.EventHandler(this.nodeFeederForm_Shown);
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.operatingTempNumUpDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.sourceVoltageNumUpDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lengthNumericUpDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nodeDataGridView)).EndInit();
            this.tableLayoutPanel5.ResumeLayout(false);
            this.tableLayoutPanel5.PerformLayout();
            this.tableLayoutPanel4.ResumeLayout(false);
            this.tableLayoutPanel4.PerformLayout();
            this.drawingPanel.ResumeLayout(false);
            this.drawingPanel.PerformLayout();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.tableLayoutPanel6.ResumeLayout(false);
            this.tableLayoutPanel6.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nominalVoltageUpDown)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

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
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem saveToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveAsToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem loadsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
        private System.Windows.Forms.NumericUpDown operatingTempNumUpDown;
        private System.Windows.Forms.NumericUpDown sourceVoltageNumUpDown;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel6;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.NumericUpDown nominalVoltageUpDown;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label13;

    }
}