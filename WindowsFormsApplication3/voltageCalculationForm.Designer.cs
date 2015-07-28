namespace VoltageDropCalculatorApplication
{
    partial class voltageCalculationForm
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
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Series series2 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Series series3 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Series series4 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Series series5 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Title title1 = new System.Windows.Forms.DataVisualization.Charting.Title();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(voltageCalculationForm));
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.sidePanelPictureBox = new System.Windows.Forms.PictureBox();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel4 = new System.Windows.Forms.TableLayoutPanel();
            this.numericUpDownRisk = new System.Windows.Forms.NumericUpDown();
            this.bluePhaseVoltageText = new System.Windows.Forms.RichTextBox();
            this.numericUpDownVoltage = new System.Windows.Forms.NumericUpDown();
            this.whitePhaseVoltageText = new System.Windows.Forms.RichTextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.redPhaseVoltageText = new System.Windows.Forms.RichTextBox();
            this.tempNumUpDown = new System.Windows.Forms.NumericUpDown();
            this.label6 = new System.Windows.Forms.Label();
            this.voltageProfileChart = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
            this.dataGridViewLengths = new System.Windows.Forms.DataGridView();
            this.label8 = new System.Windows.Forms.Label();
            this.buttonDiscardNodeSum = new System.Windows.Forms.Button();
            this.buttonUpdateNodeSumm = new System.Windows.Forms.Button();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.label7 = new System.Windows.Forms.Label();
            this.comboBoxNodeSelect = new System.Windows.Forms.ComboBox();
            this.label9 = new System.Windows.Forms.Label();
            this.nodeSummaryDataGridView = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.sidePanelPictureBox)).BeginInit();
            this.tableLayoutPanel2.SuspendLayout();
            this.tableLayoutPanel4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownRisk)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownVoltage)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tempNumUpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.voltageProfileChart)).BeginInit();
            this.tableLayoutPanel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewLengths)).BeginInit();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nodeSummaryDataGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.splitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.sidePanelPictureBox);
            this.splitContainer1.Panel1.Controls.Add(this.tableLayoutPanel2);
            this.splitContainer1.Panel1MinSize = 641;
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.BackColor = System.Drawing.Color.SteelBlue;
            this.splitContainer1.Panel2.Controls.Add(this.tableLayoutPanel3);
            this.splitContainer1.Panel2.Font = new System.Drawing.Font("Microsoft Sans Serif", 27.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.splitContainer1.Panel2MinSize = 300;
            this.splitContainer1.Size = new System.Drawing.Size(978, 641);
            this.splitContainer1.SplitterDistance = 641;
            this.splitContainer1.TabIndex = 0;
            // 
            // sidePanelPictureBox
            // 
            this.sidePanelPictureBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.sidePanelPictureBox.Image = global::VoltageDropCalculatorApplication.Properties.Resources.classica_play_button_flat_rounded_square_white_on_blue_512x512;
            this.sidePanelPictureBox.Location = new System.Drawing.Point(610, 187);
            this.sidePanelPictureBox.Name = "sidePanelPictureBox";
            this.sidePanelPictureBox.Size = new System.Drawing.Size(24, 71);
            this.sidePanelPictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.sidePanelPictureBox.TabIndex = 5;
            this.sidePanelPictureBox.TabStop = false;
            this.sidePanelPictureBox.Click += new System.EventHandler(this.sidePanelPictureBox_Click);
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.BackColor = System.Drawing.Color.SteelBlue;
            this.tableLayoutPanel2.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.InsetDouble;
            this.tableLayoutPanel2.ColumnCount = 1;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.Controls.Add(this.tableLayoutPanel4, 0, 1);
            this.tableLayoutPanel2.Controls.Add(this.voltageProfileChart, 0, 0);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 2;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 97F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(641, 641);
            this.tableLayoutPanel2.TabIndex = 1;
            // 
            // tableLayoutPanel4
            // 
            this.tableLayoutPanel4.BackColor = System.Drawing.Color.White;
            this.tableLayoutPanel4.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.Single;
            this.tableLayoutPanel4.ColumnCount = 6;
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 18.03109F));
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 17.09845F));
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 15.54404F));
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 15.85492F));
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.tableLayoutPanel4.Controls.Add(this.numericUpDownRisk, 5, 1);
            this.tableLayoutPanel4.Controls.Add(this.bluePhaseVoltageText, 2, 1);
            this.tableLayoutPanel4.Controls.Add(this.numericUpDownVoltage, 3, 1);
            this.tableLayoutPanel4.Controls.Add(this.whitePhaseVoltageText, 1, 1);
            this.tableLayoutPanel4.Controls.Add(this.label1, 0, 0);
            this.tableLayoutPanel4.Controls.Add(this.label2, 1, 0);
            this.tableLayoutPanel4.Controls.Add(this.label3, 2, 0);
            this.tableLayoutPanel4.Controls.Add(this.label4, 3, 0);
            this.tableLayoutPanel4.Controls.Add(this.label5, 4, 0);
            this.tableLayoutPanel4.Controls.Add(this.redPhaseVoltageText, 0, 1);
            this.tableLayoutPanel4.Controls.Add(this.tempNumUpDown, 4, 1);
            this.tableLayoutPanel4.Controls.Add(this.label6, 5, 0);
            this.tableLayoutPanel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel4.Location = new System.Drawing.Point(6, 544);
            this.tableLayoutPanel4.Name = "tableLayoutPanel4";
            this.tableLayoutPanel4.RowCount = 2;
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 46F));
            this.tableLayoutPanel4.Size = new System.Drawing.Size(629, 91);
            this.tableLayoutPanel4.TabIndex = 1;
            // 
            // numericUpDownRisk
            // 
            this.numericUpDownRisk.DecimalPlaces = 2;
            this.numericUpDownRisk.Dock = System.Windows.Forms.DockStyle.Fill;
            this.numericUpDownRisk.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.numericUpDownRisk.Location = new System.Drawing.Point(524, 47);
            this.numericUpDownRisk.Name = "numericUpDownRisk";
            this.numericUpDownRisk.Size = new System.Drawing.Size(101, 26);
            this.numericUpDownRisk.TabIndex = 6;
            this.numericUpDownRisk.Value = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.numericUpDownRisk.ValueChanged += new System.EventHandler(this.numericUpDownRisk_ValueChanged);
            // 
            // bluePhaseVoltageText
            // 
            this.bluePhaseVoltageText.Dock = System.Windows.Forms.DockStyle.Fill;
            this.bluePhaseVoltageText.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bluePhaseVoltageText.ForeColor = System.Drawing.Color.DodgerBlue;
            this.bluePhaseVoltageText.Location = new System.Drawing.Point(221, 47);
            this.bluePhaseVoltageText.Name = "bluePhaseVoltageText";
            this.bluePhaseVoltageText.ReadOnly = true;
            this.bluePhaseVoltageText.Size = new System.Drawing.Size(100, 40);
            this.bluePhaseVoltageText.TabIndex = 1;
            this.bluePhaseVoltageText.Text = "230";
            // 
            // numericUpDownVoltage
            // 
            this.numericUpDownVoltage.DecimalPlaces = 2;
            this.numericUpDownVoltage.Dock = System.Windows.Forms.DockStyle.Fill;
            this.numericUpDownVoltage.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.numericUpDownVoltage.Increment = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.numericUpDownVoltage.Location = new System.Drawing.Point(328, 47);
            this.numericUpDownVoltage.Maximum = new decimal(new int[] {
            500,
            0,
            0,
            0});
            this.numericUpDownVoltage.Minimum = new decimal(new int[] {
            180,
            0,
            0,
            0});
            this.numericUpDownVoltage.Name = "numericUpDownVoltage";
            this.numericUpDownVoltage.Size = new System.Drawing.Size(90, 26);
            this.numericUpDownVoltage.TabIndex = 6;
            this.numericUpDownVoltage.Value = new decimal(new int[] {
            230,
            0,
            0,
            0});
            this.numericUpDownVoltage.ValueChanged += new System.EventHandler(this.numericUpDownVoltage_ValueChanged);
            this.numericUpDownVoltage.Validating += new System.ComponentModel.CancelEventHandler(this.numericUpDownVoltage_Validating);
            // 
            // whitePhaseVoltageText
            // 
            this.whitePhaseVoltageText.Dock = System.Windows.Forms.DockStyle.Fill;
            this.whitePhaseVoltageText.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.whitePhaseVoltageText.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.whitePhaseVoltageText.Location = new System.Drawing.Point(108, 47);
            this.whitePhaseVoltageText.Name = "whitePhaseVoltageText";
            this.whitePhaseVoltageText.ReadOnly = true;
            this.whitePhaseVoltageText.Size = new System.Drawing.Size(106, 40);
            this.whitePhaseVoltageText.TabIndex = 1;
            this.whitePhaseVoltageText.Text = "230";
            // 
            // label1
            // 
            this.label1.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.Black;
            this.label1.Location = new System.Drawing.Point(22, 6);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(79, 32);
            this.label1.TabIndex = 0;
            this.label1.Text = "Red Phase Voltage [V]";
            // 
            // label2
            // 
            this.label2.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.SteelBlue;
            this.label2.Location = new System.Drawing.Point(118, 4);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(96, 36);
            this.label2.TabIndex = 0;
            this.label2.Text = "White Phase Voltage [V]";
            // 
            // label3
            // 
            this.label3.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.SteelBlue;
            this.label3.Location = new System.Drawing.Point(234, 4);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(87, 36);
            this.label3.TabIndex = 0;
            this.label3.Text = "Blue Phase Voltage [V]";
            // 
            // label4
            // 
            this.label4.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.SteelBlue;
            this.label4.Location = new System.Drawing.Point(340, 4);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(78, 36);
            this.label4.TabIndex = 2;
            this.label4.Text = "Source Voltage [V]";
            // 
            // label5
            // 
            this.label5.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.Color.SteelBlue;
            this.label5.Location = new System.Drawing.Point(425, 4);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(92, 36);
            this.label5.TabIndex = 3;
            this.label5.Text = "Temperature [°C]";
            // 
            // redPhaseVoltageText
            // 
            this.redPhaseVoltageText.Dock = System.Windows.Forms.DockStyle.Fill;
            this.redPhaseVoltageText.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.redPhaseVoltageText.ForeColor = System.Drawing.Color.Red;
            this.redPhaseVoltageText.Location = new System.Drawing.Point(4, 47);
            this.redPhaseVoltageText.Name = "redPhaseVoltageText";
            this.redPhaseVoltageText.ReadOnly = true;
            this.redPhaseVoltageText.Size = new System.Drawing.Size(97, 40);
            this.redPhaseVoltageText.TabIndex = 1;
            this.redPhaseVoltageText.Text = "230";
            // 
            // tempNumUpDown
            // 
            this.tempNumUpDown.DecimalPlaces = 2;
            this.tempNumUpDown.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tempNumUpDown.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tempNumUpDown.Location = new System.Drawing.Point(425, 47);
            this.tempNumUpDown.Name = "tempNumUpDown";
            this.tempNumUpDown.Size = new System.Drawing.Size(92, 26);
            this.tempNumUpDown.TabIndex = 3;
            this.tempNumUpDown.Value = new decimal(new int[] {
            40,
            0,
            0,
            0});
            this.tempNumUpDown.ValueChanged += new System.EventHandler(this.tempNumUpDown_ValueChanged);
            // 
            // label6
            // 
            this.label6.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.ForeColor = System.Drawing.Color.SteelBlue;
            this.label6.Location = new System.Drawing.Point(545, 13);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(59, 18);
            this.label6.TabIndex = 4;
            this.label6.Text = "Risk[%]";
            // 
            // voltageProfileChart
            // 
            this.voltageProfileChart.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.voltageProfileChart.BackColor = System.Drawing.Color.WhiteSmoke;
            this.voltageProfileChart.BackImageTransparentColor = System.Drawing.Color.White;
            this.voltageProfileChart.BorderlineColor = System.Drawing.Color.Black;
            this.voltageProfileChart.BorderlineDashStyle = System.Windows.Forms.DataVisualization.Charting.ChartDashStyle.Solid;
            this.voltageProfileChart.BorderlineWidth = 3;
            chartArea1.AxisX.LineColor = System.Drawing.Color.SteelBlue;
            chartArea1.AxisX.MajorGrid.Enabled = false;
            chartArea1.AxisX.Title = "Distance [m]";
            chartArea1.AxisX.TitleFont = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            chartArea1.AxisY.LineColor = System.Drawing.Color.SteelBlue;
            chartArea1.AxisY.MajorGrid.LineColor = System.Drawing.Color.DarkGray;
            chartArea1.AxisY.MajorGrid.LineDashStyle = System.Windows.Forms.DataVisualization.Charting.ChartDashStyle.Dash;
            chartArea1.AxisY.Title = "Feeder Voltage [V]";
            chartArea1.AxisY.TitleFont = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            chartArea1.BackColor = System.Drawing.Color.White;
            chartArea1.BackImageAlignment = System.Windows.Forms.DataVisualization.Charting.ChartImageAlignmentStyle.Center;
            chartArea1.Name = "ChartArea1";
            this.voltageProfileChart.ChartAreas.Add(chartArea1);
            legend1.Alignment = System.Drawing.StringAlignment.Center;
            legend1.Docking = System.Windows.Forms.DataVisualization.Charting.Docking.Bottom;
            legend1.Name = "Legend1";
            legend1.TitleFont = new System.Drawing.Font("Arial Narrow", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.voltageProfileChart.Legends.Add(legend1);
            this.voltageProfileChart.Location = new System.Drawing.Point(6, 16);
            this.voltageProfileChart.Name = "voltageProfileChart";
            series1.BorderWidth = 2;
            series1.ChartArea = "ChartArea1";
            series1.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            series1.Legend = "Legend1";
            series1.MarkerStyle = System.Windows.Forms.DataVisualization.Charting.MarkerStyle.Square;
            series1.Name = "Series1";
            series2.BorderWidth = 2;
            series2.ChartArea = "ChartArea1";
            series2.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            series2.Legend = "Legend1";
            series2.MarkerStyle = System.Windows.Forms.DataVisualization.Charting.MarkerStyle.Square;
            series2.Name = "Series2";
            series3.BorderWidth = 2;
            series3.ChartArea = "ChartArea1";
            series3.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            series3.Legend = "Legend1";
            series3.MarkerStyle = System.Windows.Forms.DataVisualization.Charting.MarkerStyle.Square;
            series3.Name = "Series3";
            series4.BorderColor = System.Drawing.Color.Black;
            series4.BorderDashStyle = System.Windows.Forms.DataVisualization.Charting.ChartDashStyle.Dash;
            series4.BorderWidth = 3;
            series4.ChartArea = "ChartArea1";
            series4.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            series4.Legend = "Legend1";
            series4.Name = "Series4";
            series5.BorderDashStyle = System.Windows.Forms.DataVisualization.Charting.ChartDashStyle.Dash;
            series5.BorderWidth = 3;
            series5.ChartArea = "ChartArea1";
            series5.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            series5.Legend = "Legend1";
            series5.Name = "Series5";
            this.voltageProfileChart.Series.Add(series1);
            this.voltageProfileChart.Series.Add(series2);
            this.voltageProfileChart.Series.Add(series3);
            this.voltageProfileChart.Series.Add(series4);
            this.voltageProfileChart.Series.Add(series5);
            this.voltageProfileChart.Size = new System.Drawing.Size(629, 508);
            this.voltageProfileChart.TabIndex = 0;
            this.voltageProfileChart.Text = "chart1";
            title1.Font = new System.Drawing.Font("Arial", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            title1.Name = "Title1";
            title1.Text = "3-Phase Feeder Voltage Profile";
            this.voltageProfileChart.Titles.Add(title1);
            this.voltageProfileChart.Click += new System.EventHandler(this.voltageProfileChart_Click);
            // 
            // tableLayoutPanel3
            // 
            this.tableLayoutPanel3.BackColor = System.Drawing.Color.SteelBlue;
            this.tableLayoutPanel3.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.Single;
            this.tableLayoutPanel3.ColumnCount = 1;
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel3.Controls.Add(this.dataGridViewLengths, 0, 1);
            this.tableLayoutPanel3.Controls.Add(this.label8, 0, 0);
            this.tableLayoutPanel3.Controls.Add(this.buttonDiscardNodeSum, 0, 6);
            this.tableLayoutPanel3.Controls.Add(this.buttonUpdateNodeSumm, 0, 5);
            this.tableLayoutPanel3.Controls.Add(this.tableLayoutPanel1, 0, 3);
            this.tableLayoutPanel3.Controls.Add(this.label9, 0, 2);
            this.tableLayoutPanel3.Controls.Add(this.nodeSummaryDataGridView, 0, 4);
            this.tableLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tableLayoutPanel3.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel3.Name = "tableLayoutPanel3";
            this.tableLayoutPanel3.RowCount = 7;
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 28F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 165F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 28F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 31F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 52F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 52F));
            this.tableLayoutPanel3.Size = new System.Drawing.Size(333, 641);
            this.tableLayoutPanel3.TabIndex = 1;
            // 
            // dataGridViewLengths
            // 
            this.dataGridViewLengths.AllowUserToAddRows = false;
            this.dataGridViewLengths.AllowUserToDeleteRows = false;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dataGridViewLengths.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dataGridViewLengths.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridViewLengths.BackgroundColor = System.Drawing.Color.White;
            this.dataGridViewLengths.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridViewLengths.EnableHeadersVisualStyles = false;
            this.dataGridViewLengths.Location = new System.Drawing.Point(4, 33);
            this.dataGridViewLengths.Name = "dataGridViewLengths";
            this.dataGridViewLengths.RowHeadersVisible = false;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dataGridViewLengths.RowsDefaultCellStyle = dataGridViewCellStyle2;
            this.dataGridViewLengths.Size = new System.Drawing.Size(325, 159);
            this.dataGridViewLengths.TabIndex = 4;
            this.dataGridViewLengths.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridViewLengths_CellEndEdit);
            this.dataGridViewLengths.CellValidating += new System.Windows.Forms.DataGridViewCellValidatingEventHandler(this.dataGridViewLengths_CellValidating);
            this.dataGridViewLengths.DataError += new System.Windows.Forms.DataGridViewDataErrorEventHandler(this.dataGridViewLengths_DataError);
            this.dataGridViewLengths.EditingControlShowing += new System.Windows.Forms.DataGridViewEditingControlShowingEventHandler(this.dataGridViewLengths_EditingControlShowing);
            this.dataGridViewLengths.Validated += new System.EventHandler(this.dataGridViewLengths_Validated);
            // 
            // label8
            // 
            this.label8.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.ForeColor = System.Drawing.Color.White;
            this.label8.Location = new System.Drawing.Point(55, 3);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(222, 24);
            this.label8.TabIndex = 2;
            this.label8.Text = "Cable and Length Data";
            // 
            // buttonDiscardNodeSum
            // 
            this.buttonDiscardNodeSum.Dock = System.Windows.Forms.DockStyle.Fill;
            this.buttonDiscardNodeSum.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonDiscardNodeSum.Location = new System.Drawing.Point(4, 591);
            this.buttonDiscardNodeSum.Name = "buttonDiscardNodeSum";
            this.buttonDiscardNodeSum.Size = new System.Drawing.Size(325, 46);
            this.buttonDiscardNodeSum.TabIndex = 5;
            this.buttonDiscardNodeSum.Text = "Discard";
            this.buttonDiscardNodeSum.UseVisualStyleBackColor = true;
            this.buttonDiscardNodeSum.EnabledChanged += new System.EventHandler(this.buttonDiscardNodeSum_EnabledChanged);
            this.buttonDiscardNodeSum.Click += new System.EventHandler(this.buttonDiscardNodeSum_Click);
            // 
            // buttonUpdateNodeSumm
            // 
            this.buttonUpdateNodeSumm.Dock = System.Windows.Forms.DockStyle.Fill;
            this.buttonUpdateNodeSumm.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonUpdateNodeSumm.Location = new System.Drawing.Point(4, 538);
            this.buttonUpdateNodeSumm.Name = "buttonUpdateNodeSumm";
            this.buttonUpdateNodeSumm.Size = new System.Drawing.Size(325, 46);
            this.buttonUpdateNodeSumm.TabIndex = 3;
            this.buttonUpdateNodeSumm.Text = "Update";
            this.buttonUpdateNodeSumm.UseVisualStyleBackColor = true;
            this.buttonUpdateNodeSumm.EnabledChanged += new System.EventHandler(this.buttonUpdateNodeSumm_EnabledChanged);
            this.buttonUpdateNodeSumm.Click += new System.EventHandler(this.buttonUpdateNodeSumm_Click);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Controls.Add(this.label7, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.comboBoxNodeSelect, 1, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(4, 228);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(325, 25);
            this.tableLayoutPanel1.TabIndex = 7;
            // 
            // label7
            // 
            this.label7.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label7.AutoSize = true;
            this.label7.ForeColor = System.Drawing.Color.White;
            this.label7.Location = new System.Drawing.Point(55, 6);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(104, 13);
            this.label7.TabIndex = 7;
            this.label7.Text = "Select Node to View";
            // 
            // comboBoxNodeSelect
            // 
            this.comboBoxNodeSelect.Dock = System.Windows.Forms.DockStyle.Fill;
            this.comboBoxNodeSelect.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxNodeSelect.FormattingEnabled = true;
            this.comboBoxNodeSelect.Location = new System.Drawing.Point(165, 3);
            this.comboBoxNodeSelect.Name = "comboBoxNodeSelect";
            this.comboBoxNodeSelect.Size = new System.Drawing.Size(157, 21);
            this.comboBoxNodeSelect.TabIndex = 6;
            this.comboBoxNodeSelect.SelectedIndexChanged += new System.EventHandler(this.comboBoxNodeSelect_SelectedIndexChanged);
            // 
            // label9
            // 
            this.label9.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.ForeColor = System.Drawing.Color.White;
            this.label9.Location = new System.Drawing.Point(66, 198);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(201, 24);
            this.label9.TabIndex = 2;
            this.label9.Text = "Node Summary Data";
            // 
            // nodeSummaryDataGridView
            // 
            this.nodeSummaryDataGridView.AllowUserToAddRows = false;
            this.nodeSummaryDataGridView.AllowUserToDeleteRows = false;
            this.nodeSummaryDataGridView.AllowUserToResizeColumns = false;
            this.nodeSummaryDataGridView.AllowUserToResizeRows = false;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.Color.SteelBlue;
            this.nodeSummaryDataGridView.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle3;
            this.nodeSummaryDataGridView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.nodeSummaryDataGridView.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.nodeSummaryDataGridView.BackgroundColor = System.Drawing.SystemColors.Control;
            this.nodeSummaryDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle4.ForeColor = System.Drawing.Color.SteelBlue;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.nodeSummaryDataGridView.DefaultCellStyle = dataGridViewCellStyle4;
            this.nodeSummaryDataGridView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.nodeSummaryDataGridView.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
            this.nodeSummaryDataGridView.EnableHeadersVisualStyles = false;
            this.nodeSummaryDataGridView.Location = new System.Drawing.Point(4, 260);
            this.nodeSummaryDataGridView.Name = "nodeSummaryDataGridView";
            this.nodeSummaryDataGridView.RowHeadersVisible = false;
            dataGridViewCellStyle5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.nodeSummaryDataGridView.RowsDefaultCellStyle = dataGridViewCellStyle5;
            this.nodeSummaryDataGridView.RowTemplate.DefaultCellStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.nodeSummaryDataGridView.Size = new System.Drawing.Size(325, 271);
            this.nodeSummaryDataGridView.TabIndex = 1;
            this.nodeSummaryDataGridView.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.nodeSummaryDataGridView_CellContentClick);
            this.nodeSummaryDataGridView.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.nodeSummaryDataGridView_CellEndEdit);
            this.nodeSummaryDataGridView.CellValidating += new System.Windows.Forms.DataGridViewCellValidatingEventHandler(this.nodeSummaryDataGridView_CellValidating);
            this.nodeSummaryDataGridView.EditingControlShowing += new System.Windows.Forms.DataGridViewEditingControlShowingEventHandler(this.nodeSummaryDataGridView_EditingControlShowing);
            // 
            // voltageCalculationForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(977, 640);
            this.Controls.Add(this.splitContainer1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximumSize = new System.Drawing.Size(1366, 768);
            this.Name = "voltageCalculationForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Voltage Calculation and Feeder Profile";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.voltageCalculationForm_FormClosing);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.sidePanelPictureBox)).EndInit();
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel4.ResumeLayout(false);
            this.tableLayoutPanel4.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownRisk)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownVoltage)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tempNumUpDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.voltageProfileChart)).EndInit();
            this.tableLayoutPanel3.ResumeLayout(false);
            this.tableLayoutPanel3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewLengths)).EndInit();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nodeSummaryDataGridView)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.RichTextBox redPhaseVoltageText;
        private System.Windows.Forms.RichTextBox whitePhaseVoltageText;
        private System.Windows.Forms.RichTextBox bluePhaseVoltageText;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.DataVisualization.Charting.Chart voltageProfileChart;
        private System.Windows.Forms.NumericUpDown numericUpDownVoltage;
        private System.Windows.Forms.NumericUpDown numericUpDownRisk;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel3;
        private System.Windows.Forms.DataGridView nodeSummaryDataGridView;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel4;
        private System.Windows.Forms.NumericUpDown tempNumUpDown;
        private System.Windows.Forms.Button buttonUpdateNodeSumm;
        private System.Windows.Forms.DataGridView dataGridViewLengths;
        private System.Windows.Forms.PictureBox sidePanelPictureBox;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Button buttonDiscardNodeSum;
        private System.Windows.Forms.ComboBox comboBoxNodeSelect;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label9;
    }
}