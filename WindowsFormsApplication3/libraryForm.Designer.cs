namespace HermanBetaAlgorithmAlphaNum
{
    partial class libraryForm
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
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(libraryForm));
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.selectLibraryLabel = new System.Windows.Forms.Label();
            this.loadTypeCombo = new System.Windows.Forms.ComboBox();
            this.resetLibrary = new System.Windows.Forms.Button();
            this.addCableButton = new System.Windows.Forms.Button();
            this.dataGridViewLoadsDGs = new System.Windows.Forms.DataGridView();
            this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
            this.doneLoadDG = new System.Windows.Forms.Button();
            this.saveLibraryButton = new System.Windows.Forms.Button();
            this.saveToProject = new System.Windows.Forms.Button();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.deleteRowToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewLoadsDGs)).BeginInit();
            this.tableLayoutPanel3.SuspendLayout();
            this.contextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 4;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 29.78723F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 70.21277F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 176F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 194F));
            this.tableLayoutPanel1.Controls.Add(this.selectLibraryLabel, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.loadTypeCombo, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.resetLibrary, 3, 0);
            this.tableLayoutPanel1.Controls.Add(this.addCableButton, 2, 0);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(25, 117);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(857, 29);
            this.tableLayoutPanel1.TabIndex = 0;
            this.tableLayoutPanel1.Paint += new System.Windows.Forms.PaintEventHandler(this.tableLayoutPanel1_Paint);
            // 
            // selectLibraryLabel
            // 
            this.selectLibraryLabel.AutoSize = true;
            this.selectLibraryLabel.Location = new System.Drawing.Point(3, 0);
            this.selectLibraryLabel.Name = "selectLibraryLabel";
            this.selectLibraryLabel.Size = new System.Drawing.Size(126, 13);
            this.selectLibraryLabel.TabIndex = 1;
            this.selectLibraryLabel.Text = "Select Load or Generator";
            // 
            // loadTypeCombo
            // 
            this.loadTypeCombo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.loadTypeCombo.FormattingEnabled = true;
            this.loadTypeCombo.Location = new System.Drawing.Point(148, 3);
            this.loadTypeCombo.Name = "loadTypeCombo";
            this.loadTypeCombo.Size = new System.Drawing.Size(176, 21);
            this.loadTypeCombo.TabIndex = 0;
            this.loadTypeCombo.TextChanged += new System.EventHandler(this.loadTypeCombo_TextChanged);
            // 
            // resetLibrary
            // 
            this.resetLibrary.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.resetLibrary.Location = new System.Drawing.Point(684, 3);
            this.resetLibrary.Name = "resetLibrary";
            this.resetLibrary.Size = new System.Drawing.Size(170, 23);
            this.resetLibrary.TabIndex = 2;
            this.resetLibrary.Text = "Reset Library";
            this.resetLibrary.UseVisualStyleBackColor = true;
            this.resetLibrary.Click += new System.EventHandler(this.resetLibrary_Click);
            // 
            // addCableButton
            // 
            this.addCableButton.Location = new System.Drawing.Point(489, 3);
            this.addCableButton.Name = "addCableButton";
            this.addCableButton.Size = new System.Drawing.Size(170, 23);
            this.addCableButton.TabIndex = 3;
            this.addCableButton.Text = "Add Cable";
            this.addCableButton.UseVisualStyleBackColor = true;
            this.addCableButton.Click += new System.EventHandler(this.addCableButton_Click);
            // 
            // dataGridViewLoadsDGs
            // 
            this.dataGridViewLoadsDGs.AllowUserToAddRows = false;
            this.dataGridViewLoadsDGs.AllowUserToDeleteRows = false;
            this.dataGridViewLoadsDGs.AllowUserToResizeColumns = false;
            this.dataGridViewLoadsDGs.AllowUserToResizeRows = false;
            this.dataGridViewLoadsDGs.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridViewLoadsDGs.BackgroundColor = System.Drawing.Color.AliceBlue;
            this.dataGridViewLoadsDGs.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridViewLoadsDGs.DefaultCellStyle = dataGridViewCellStyle1;
            this.dataGridViewLoadsDGs.Location = new System.Drawing.Point(25, 152);
            this.dataGridViewLoadsDGs.Name = "dataGridViewLoadsDGs";
            this.dataGridViewLoadsDGs.RowHeadersVisible = false;
            this.dataGridViewLoadsDGs.Size = new System.Drawing.Size(857, 406);
            this.dataGridViewLoadsDGs.TabIndex = 2;
            this.dataGridViewLoadsDGs.DataSourceChanged += new System.EventHandler(this.dataGridViewLoadsDGs_DataSourceChanged);
            this.dataGridViewLoadsDGs.CellBeginEdit += new System.Windows.Forms.DataGridViewCellCancelEventHandler(this.dataGridViewLoadsDGs_CellBeginEdit);
            this.dataGridViewLoadsDGs.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridViewLoadsDGs_CellContentClick);
            this.dataGridViewLoadsDGs.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridViewLoadsDGs_CellEndEdit);
            this.dataGridViewLoadsDGs.CellMouseUp += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dataGridViewLoadsDGs_CellMouseUp);
            this.dataGridViewLoadsDGs.CellValidating += new System.Windows.Forms.DataGridViewCellValidatingEventHandler(this.dataGridViewLoadsDGs_CellValidating);
            this.dataGridViewLoadsDGs.DataError += new System.Windows.Forms.DataGridViewDataErrorEventHandler(this.dataGridViewLoadsDGs_DataError);
            this.dataGridViewLoadsDGs.EditingControlShowing += new System.Windows.Forms.DataGridViewEditingControlShowingEventHandler(this.dataGridViewLoadsDGs_EditingControlShowing);
            this.dataGridViewLoadsDGs.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.dataGridViewLoadsDGs_KeyPress);
            // 
            // tableLayoutPanel3
            // 
            this.tableLayoutPanel3.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.Single;
            this.tableLayoutPanel3.ColumnCount = 3;
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 60F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel3.Controls.Add(this.doneLoadDG, 2, 0);
            this.tableLayoutPanel3.Controls.Add(this.saveLibraryButton, 0, 0);
            this.tableLayoutPanel3.Controls.Add(this.saveToProject, 1, 0);
            this.tableLayoutPanel3.Location = new System.Drawing.Point(25, 590);
            this.tableLayoutPanel3.Name = "tableLayoutPanel3";
            this.tableLayoutPanel3.RowCount = 1;
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel3.Size = new System.Drawing.Size(857, 33);
            this.tableLayoutPanel3.TabIndex = 3;
            // 
            // doneLoadDG
            // 
            this.doneLoadDG.Location = new System.Drawing.Point(687, 4);
            this.doneLoadDG.Name = "doneLoadDG";
            this.doneLoadDG.Size = new System.Drawing.Size(152, 25);
            this.doneLoadDG.TabIndex = 3;
            this.doneLoadDG.Text = "Close";
            this.doneLoadDG.UseVisualStyleBackColor = true;
            this.doneLoadDG.Click += new System.EventHandler(this.doneLoadDG_Click);
            // 
            // saveLibraryButton
            // 
            this.saveLibraryButton.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.saveLibraryButton.Enabled = false;
            this.saveLibraryButton.Location = new System.Drawing.Point(301, 4);
            this.saveLibraryButton.Name = "saveLibraryButton";
            this.saveLibraryButton.Size = new System.Drawing.Size(208, 25);
            this.saveLibraryButton.TabIndex = 4;
            this.saveLibraryButton.Text = "Save and Overwrite Existing Libraries";
            this.saveLibraryButton.UseVisualStyleBackColor = true;
            this.saveLibraryButton.EnabledChanged += new System.EventHandler(this.saveLibraryButton_EnabledChanged);
            this.saveLibraryButton.Click += new System.EventHandler(this.saveLibraryButton_Click);
            // 
            // saveToProject
            // 
            this.saveToProject.Enabled = false;
            this.saveToProject.Location = new System.Drawing.Point(516, 4);
            this.saveToProject.Name = "saveToProject";
            this.saveToProject.Size = new System.Drawing.Size(164, 25);
            this.saveToProject.TabIndex = 5;
            this.saveToProject.Text = "Save and use in Project only";
            this.saveToProject.UseVisualStyleBackColor = true;
            this.saveToProject.Click += new System.EventHandler(this.saveToProject_Click);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.deleteRowToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(134, 26);
            // 
            // deleteRowToolStripMenuItem
            // 
            this.deleteRowToolStripMenuItem.Name = "deleteRowToolStripMenuItem";
            this.deleteRowToolStripMenuItem.Size = new System.Drawing.Size(133, 22);
            this.deleteRowToolStripMenuItem.Text = "Delete Row";
            this.deleteRowToolStripMenuItem.Click += new System.EventHandler(this.deleteRowToolStripMenuItem_Click);
            // 
            // libraryForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(916, 647);
            this.Controls.Add(this.tableLayoutPanel3);
            this.Controls.Add(this.dataGridViewLoadsDGs);
            this.Controls.Add(this.tableLayoutPanel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "libraryForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Library Editor";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.libraryForm_FormClosing);
            this.Load += new System.EventHandler(this.LibraryFormGenLoad_Load);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewLoadsDGs)).EndInit();
            this.tableLayoutPanel3.ResumeLayout(false);
            this.contextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.ComboBox loadTypeCombo;
        private System.Windows.Forms.DataGridView dataGridViewLoadsDGs;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel3;
        private System.Windows.Forms.Button doneLoadDG;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem deleteRowToolStripMenuItem;
        private System.Windows.Forms.Label selectLibraryLabel;
        private System.Windows.Forms.Button resetLibrary;
        public System.Windows.Forms.Button saveLibraryButton;
        private System.Windows.Forms.Button addCableButton;
        public System.Windows.Forms.Button saveToProject;
    }
}