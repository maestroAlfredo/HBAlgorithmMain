using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;
namespace VoltageDropCalculatorApplication
{
    public partial class libraryForm : Form
    {
        //private int rowIndex = 0; //int that holds the row index of the load/DG
        //private int addDGInt = 0; //int that tracks the number of DGs
        //private int addLoadInt = 0; //int that tracks the number of loads
        private int addCableInt = 0; //int that tracks the number of Cables

        DataSet libraryDataSet = new DataSet();

        public libraryForm()
        {
            InitializeComponent();
            foreach (DataGridViewColumn column in dataGridViewLoadsDGs.Columns)
            {
                column.SortMode = DataGridViewColumnSortMode.NotSortable;
            }
        }

        public libraryForm(string selectedloadtype, bool enabled, DataSet libraryDataSet)
        {
            InitializeComponent();
            this.libraryDataSet = libraryDataSet;
            loadTypeCombo.Text = selectedloadtype; //constructor to load library form depending on whether load or generator is selected
            loadTypeCombo.Enabled = enabled;
            //libraryDataSet.ReadXml("Libraries.xml");
            List<string> LoadType = new List<string>();
           
            foreach (DataTable dt in libraryDataSet.Tables)
            {
                LoadType.Add(dt.TableName);
            }
            loadTypeCombo.DataSource = LoadType;

            

            if (loadTypeCombo.Text == "Loads")
            {
                dataGridViewLoadsDGs.DataSource = libraryDataSet.Tables["Loads"];

            }
            else if (loadTypeCombo.Text == "Generators")
            {
                dataGridViewLoadsDGs.DataSource = libraryDataSet.Tables["Generators"];
            }
            else
            {
                dataGridViewLoadsDGs.DataSource = libraryDataSet.Tables["Conductors"];
            }

            foreach (DataGridViewColumn column in dataGridViewLoadsDGs.Columns)
            {
                column.SortMode = DataGridViewColumnSortMode.NotSortable;
            }
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        //OnloadMethod loads the datagridview when the form is loaded which is either a load or a generator
        private void LibraryFormGenLoad_Load(object sender, EventArgs e)
        {
            

        }

        //textChanged method checks which text is in the loadTypeComboBox and displays the corresponding library
        private void loadTypeCombo_TextChanged(object sender, EventArgs e)
        {
            addCableButton.Visible = false;
            
            //libraryDataSet1.ReadXml("Libraries.xml");
            if (loadTypeCombo.Text == "Loads")
            {

                //if (!libraryDataSet.Tables.Contains("Load types"))
                //{
                //    string message = "Click \"Reset Libraries\" to continue";
                //    string caption = "No loads available";
                //    MessageBoxButtons buttons = MessageBoxButtons.OK;
                //    DialogResult result = MessageBox.Show(message, caption, buttons, MessageBoxIcon.Error);
                //}

                dataGridViewLoadsDGs.DataSource = libraryDataSet.Tables["Loads"];                

            }

            else if (loadTypeCombo.Text == "Generators")
            {
                dataGridViewLoadsDGs.DataSource = libraryDataSet.Tables["Generators"]; 
            }
            else
            {
                addCableButton.Visible = true;
                dataGridViewLoadsDGs.DataSource = libraryDataSet.Tables["Conductors"]; 
            }
        }

        private void dataGridViewLoadsDGs_CellMouseUp(object sender, DataGridViewCellMouseEventArgs e)
        {
            //if (e.Button == MouseButtons.Right) //finds the right click row index and shows the context menu strip 
            //{
            //    this.dataGridViewLoadsDGs.Rows[e.RowIndex].Selected = true;
            //    this.rowIndex = e.RowIndex;
            //    this.dataGridViewLoadsDGs.CurrentCell = this.dataGridViewLoadsDGs.Rows[e.RowIndex].Cells[1];
            //    this.contextMenuStrip1.Show(this.dataGridViewLoadsDGs, e.Location);
            //    contextMenuStrip1.Show(Cursor.Position);
            //}
        }

        //removes the selected row from the datatable and updates the library
        private void deleteRowToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //DataSet feederProject = new DataSet();
            //if (!this.dataGridViewLoadsDGs.Rows[this.rowIndex].IsNewRow)
            //{
            //    if (loadTypeCombo.Text == "Generators")
            //    {
            //        addDGInt--;
            //        feederProject.ReadXml("Generators.xml");
            //        feederProject.Tables[0].Rows.RemoveAt(this.rowIndex);
            //        for (int i = 0; i < dataGridViewLoadsDGs.Rows.Count-1 - this.rowIndex; i++)
            //        {
            //            if (rowIndex != 0) 
            //            {
            //                feederProject.Tables[0].Rows[this.rowIndex + i][0] = "Gen "+ Convert.ToString(this.rowIndex + i + 1);
            //            }
            //            else
            //            {                            
            //                feederProject.Tables[0].Rows[this.rowIndex + i][0] = "Gen "+ Convert.ToString(i+1);
            //            }
                
            //        }
            //        feederProject.WriteXml("Generators.xml");
            //        dataGridViewLoadsDGs.DataSource = feederProject.Tables[0];
            //    }
            //    else if(loadTypeCombo.Text=="Loads")
            //    {
            //        addLoadInt--;
            //        feederProject.ReadXml("Loads.xml");
            //        feederProject.Tables[0].Rows.RemoveAt(this.rowIndex);
            //        for (int i = 0; i < dataGridViewLoadsDGs.Rows.Count - 1 - this.rowIndex; i++)
            //        {
            //            if (rowIndex != 0)
            //            {
            //                feederProject.Tables[0].Rows[this.rowIndex + i][0] = "Load "+ Convert.ToString(this.rowIndex + i + 1);
            //            }
            //            else
            //            {
            //                feederProject.Tables[0].Rows[this.rowIndex + i][0] = "Load "+ Convert.ToString(i + 1);
            //            }

            //        }
            //        feederProject.WriteXml("Loads.xml");
            //        dataGridViewLoadsDGs.DataSource = feederProject.Tables[0];
            //    }
            //    else
            //    {
            //        addCableInt--;
            //        feederProject.ReadXml("Conductors.xml");
            //        feederProject.Tables[0].Rows.RemoveAt(this.rowIndex);
            //        feederProject.WriteXml("Conductors.xml");
            //        dataGridViewLoadsDGs.DataSource = feederProject.Tables[0];
            //    }
            //}
        }

        //method that adds a load, generator or conductor when the corresponding selction is made in the loadTypeCombo box
        private void addLoadDGButton_Click(object sender, EventArgs e)
        {
            ////method counts the loads or generators in the datagrid view and updates the loadInt and genint counts
            //if ((dataGridViewLoadsDGs.Rows.Count - 1 > 0)||(Convert.ToString(dataGridViewLoadsDGs.Rows[0].Cells[0].Value) !="0"))
            //{
            //    if (loadTypeCombo.Text == "Generators") addDGInt = dataGridViewLoadsDGs.Rows.Count;
            //    else if (loadTypeCombo.Text == "Loads") addLoadInt = dataGridViewLoadsDGs.Rows.Count;
            //    else addCableInt = dataGridViewLoadsDGs.Rows.Count;
            //} 

            //DataSet feederProject = new DataSet();
            //DataTable dt = new DataTable();
            //if (loadTypeCombo.Text == "Generators") addDGInt++;
            //else if (loadTypeCombo.Text == "Loads") addLoadInt++;
            //else addCableInt++;


            //if ((loadTypeCombo.Text == "Generators"))
            //{

            //    feederProject.ReadXml("Generators.xml");

            //    if ((dataGridViewLoadsDGs.Rows.Count <= 2) && (addDGInt <=1)&&(Convert.ToString(dataGridViewLoadsDGs.Rows[0].Cells[0].Value) =="0")) //if the datagrid has only one row then the addLoadButton should overite the first row.
            //    {
            //        feederProject.Tables[0].Rows[0][0] = "Gen "+ Convert.ToString(addDGInt);
            //        feederProject.Tables[0].Rows[0][1] = Convert.ToString(numericUpDownAlpha.Value);
            //        feederProject.Tables[0].Rows[0][2] = Convert.ToString(numericUpDownBeta.Value);
            //        feederProject.Tables[0].Rows[0][3] = Convert.ToString(numericUpDownCB.Value);
            //        feederProject.WriteXml("Generators.xml");
            //        dataGridViewLoadsDGs.DataSource = feederProject.Tables[0];
            //    }
            //    else
            //    {
            //        feederProject.Tables[0].Rows.Add("Gen "+ Convert.ToString(addDGInt), Convert.ToString(numericUpDownAlpha.Value), Convert.ToString(numericUpDownBeta.Value), Convert.ToString(numericUpDownCB.Value));
            //        feederProject.WriteXml("Generators.xml");
            //        dataGridViewLoadsDGs.DataSource = feederProject.Tables[0];
            //    }

            //}
            //else if (loadTypeCombo.Text == "Loads")
            //{
            //    feederProject.ReadXml("Loads.xml");
            //    if ((dataGridViewLoadsDGs.Rows.Count <= 2) && (addLoadInt <=1)&&(Convert.ToString(dataGridViewLoadsDGs.Rows[0].Cells[0].Value) == "0")) //if the datagrid has only one row then the addLoadButton should overite the first row.
            //    {
            //        feederProject.Tables[0].Rows[0][0] = "Load "+Convert.ToString(addLoadInt);
            //        feederProject.Tables[0].Rows[0][1] = Convert.ToString(numericUpDownAlpha.Value);
            //        feederProject.Tables[0].Rows[0][2] = Convert.ToString(numericUpDownBeta.Value);
            //        feederProject.Tables[0].Rows[0][3] = Convert.ToString(numericUpDownCB.Value);
            //        feederProject.WriteXml("Loads.xml");
            //        dataGridViewLoadsDGs.DataSource = feederProject.Tables[0];

            //    }
            //    else
            //    {
            //        feederProject.Tables[0].Rows.Add("Load "+ Convert.ToString(addLoadInt), Convert.ToString(numericUpDownAlpha.Value), Convert.ToString(numericUpDownBeta.Value), Convert.ToString(numericUpDownCB.Value));
            //        feederProject.WriteXml("Loads.xml");
            //        dataGridViewLoadsDGs.DataSource = feederProject.Tables[0];

            //    }

            //}
            //else
            //{
            //    feederProject.ReadXml("Conductors.xml");
            //    if ((dataGridViewLoadsDGs.Rows.Count <= 2) && (addLoadInt <= 1)&&(Convert.ToString(dataGridViewLoadsDGs.Rows[0].Cells[0].Value) == "0")) //if the datagrid has only one row then the addLoadButton should overite the first row.
            //    {
            //        feederProject.Tables[0].Rows[0][0] = cableCodeTextBox.Text;
            //        feederProject.Tables[0].Rows[0][1] = Convert.ToString(numericUpDownAlpha.Value);
            //        feederProject.Tables[0].Rows[0][2] = Convert.ToString(numericUpDownBeta.Value);
            //        feederProject.Tables[0].Rows[0][3] = Convert.ToString(numericUpDownCB.Value);
            //        feederProject.Tables[0].Rows[0][4] = descriptionTextBox.Text;
            //        feederProject.WriteXml("Conductors.xml");
            //        dataGridViewLoadsDGs.DataSource = feederProject.Tables[0];

            //    }
            //    else
            //    {
            //        feederProject.Tables[0].Rows.Add(cableCodeTextBox.Text, Convert.ToString(numericUpDownAlpha.Value), Convert.ToString(numericUpDownBeta.Value), Convert.ToString(numericUpDownCB.Value), descriptionTextBox.Text);
            //        feederProject.WriteXml("Conductors.xml");
            //        dataGridViewLoadsDGs.DataSource = feederProject.Tables[0];

            //    }                
            //}
        }

        public void updateXMLDatabasefile(String selectedXMLType, DataGridView dataGrid)
        {
            DataSet feederProject = new DataSet();
            feederProject.ReadXml(selectedXMLType);

            DataTable loadDGType = new DataTable();

            foreach (DataGridView row in dataGrid.Rows)
            {



            }
            
            loadDGType.WriteXml(selectedXMLType);
        }

        private void doneLoadDG_Click(object sender, EventArgs e)
        {
            //DataTable result = libraryDataSet.Tables["Conductors"].Select("Selected = true").CopyToDataTable();
            this.Close();
        }

        private void cableCodeTextBox_TextChanged(object sender, EventArgs e)
        {
            //if (cableCodeTextBox.Text == "") addLoadDGButton.Enabled = false;
            //else addLoadDGButton.Enabled = true;
        }

        private void resetLibrary_Click(object sender, EventArgs e)
        {
            // Warn the user that old libraries will be erased.
            string message = "This will reset the libraries. Existing data will be lost. Would you like to continue?";
            string caption = "Warning!";
            MessageBoxButtons buttons = MessageBoxButtons.YesNo;
            DialogResult result = MessageBox.Show(message, caption, buttons, MessageBoxIcon.Warning);
            if (result == System.Windows.Forms.DialogResult.Yes)
            {
                saveLibraryButton.Enabled = false;
                if (loadTypeCombo.Text == "Conductors")
                {
                    DataTable conductorTable = new DataTable(); //creates a new Datatable object for the loads        

                    conductorTable.TableName = "Conductors";

                    DataColumn dc1 = new DataColumn("Code", typeof(string)); //dreates the new datacolumn objects;
                    DataColumn dc2 = new DataColumn("R/km@T1", typeof(string));
                    DataColumn dc3 = new DataColumn("T", typeof(double));
                    DataColumn dc4 = new DataColumn("k", typeof(double));
                    DataColumn dc5 = new DataColumn("Description", typeof(string));
                    DataColumn dc6 = new DataColumn("Selected", typeof(bool));



                    conductorTable.Columns.Add(dc1); //associates the columns to the dtLoads datatable
                    conductorTable.Columns.Add(dc2);
                    conductorTable.Columns.Add(dc3);
                    conductorTable.Columns.Add(dc4);
                    conductorTable.Columns.Add(dc5);
                    conductorTable.Columns.Add(dc6);

                    conductorTable.PrimaryKey = new DataColumn[] { conductorTable.Columns["Code"] };

                    conductorTable.Rows.Add("ABC25", 1.20000000000073, 228.0, 0.5, "25 mm2 Al French std", false);
                    conductorTable.Rows.Add("ABC35", 0.8689000000000393, 228.0, 0.7, "35 mm2 Al French std", false);
                    conductorTable.Rows.Add("ABC50", 0.640999999999622, 228, 1.0, "50 mm2 Al French std", false);
                    conductorTable.Rows.Add("ABC70", 0.443000000000211, 228, 1.4, "70 mm2 Al French std", false);
                    conductorTable.Rows.Add("ABC95", 0.320000000000164, 228, 1.9, "95 mm2 Al French std", false);
                    conductorTable.Rows.Add("A10", 1.88999999999942, 241, 1, "Cu", false);
                    conductorTable.Rows.Add("35mmCu", 0.524, 241, 1, "35 mm2 Copper", false);
                    conductorTable.Rows.Add("50mmCu", 0.387, 241, 1, "50 mm2 Copper", false);
                    conductorTable.Rows.Add("70mmCu", 0.268, 241, 1, "70 mm2 Copper", false);
                    conductorTable.Rows.Add("95mmCu", 0.193, 241, 1, "95 mm2 Copper", false);

                    if (libraryDataSet.Tables.Contains("Conductors")) libraryDataSet.Tables.Remove("Conductors");//remove any previously existing table from the dataset

                    libraryDataSet.Tables.Add(conductorTable);
                    libraryDataSet.WriteXml("Libraries.xml", XmlWriteMode.WriteSchema);
                    dataGridViewLoadsDGs.DataSource = libraryDataSet.Tables["Conductors"];

                }
                else if (loadTypeCombo.Text == "Loads")
                {
                    if (libraryDataSet.Tables.Contains("Loads")) libraryDataSet.Tables.Remove("Loads");//remove any previously existing table from the dataset

                    DataTable dtLoads = new DataTable(); //creates a new Datatable object for the loads

                    dtLoads.TableName = "Loads";

                    DataColumn dc1 = new DataColumn("Load", typeof(string)); //dreates the new datacolumn objects;
                    DataColumn dc2 = new DataColumn("alpha", typeof(double));
                    DataColumn dc3 = new DataColumn("beta", typeof(double));
                    DataColumn dc4 = new DataColumn("circuit breaker", typeof(double));
                    DataColumn dc5 = new DataColumn("description", typeof(string));
                    DataColumn dc6 = new DataColumn("Selected", typeof(bool));

                    dtLoads.Columns.Add(dc1); //associates the columns to the dtLoads datatable
                    dtLoads.Columns.Add(dc2);
                    dtLoads.Columns.Add(dc3);
                    dtLoads.Columns.Add(dc4);
                    dtLoads.Columns.Add(dc5);
                    dtLoads.Columns.Add(dc6);

                    dtLoads.PrimaryKey = new DataColumn[] { dtLoads.Columns["Load"] };


                    for (int i = 0; i < 25; i++)
                    {
                        dtLoads.Rows.Add("Load " + (i + 1).ToString(), 0.0, 0.0, 0.0, "myLoad", false);
                        Debug.WriteLine(i);
                    }


                    libraryDataSet.Tables.Add(dtLoads);
                    libraryDataSet.WriteXml("Libraries.xml", XmlWriteMode.WriteSchema);
                    dataGridViewLoadsDGs.DataSource = libraryDataSet.Tables["Loads"];


                }
                else
                {
                    DataTable dtGens = new DataTable(); //creates a new Datatable object for the loads

                    dtGens.TableName = "Generators";

                    DataColumn dc1 = new DataColumn("Generator"); //dreates the new datacolumn objects;
                    DataColumn dc2 = new DataColumn("alpha");
                    DataColumn dc3 = new DataColumn("beta");
                    DataColumn dc4 = new DataColumn("circuit breaker");
                    DataColumn dc5 = new DataColumn("description");
                    DataColumn dc6 = new DataColumn("Selected", typeof(bool));


                    dtGens.Columns.Add(dc1); //associates the columns to the dtLoads datatable
                    dtGens.Columns.Add(dc2);
                    dtGens.Columns.Add(dc3);
                    dtGens.Columns.Add(dc4);
                    dtGens.Columns.Add(dc5);
                    dtGens.Columns.Add(dc6);

                    dtGens.PrimaryKey = new DataColumn[] { dtGens.Columns["Generator"] };

                    for (int i = 0; i < 25; i++)
                    {
                        dtGens.Rows.Add("Gen " + (i + 1).ToString(), 0.0, 0.0, 0.0, "myGen", false);

                    }


                    if (libraryDataSet.Tables.Contains("Generators")) libraryDataSet.Tables.Remove("Generators");//remove any previously existing table from the dataset
                    libraryDataSet.Tables.Add(dtGens);
                    libraryDataSet.WriteXml("Libraries.xml", XmlWriteMode.WriteSchema);
                    dataGridViewLoadsDGs.DataSource = libraryDataSet.Tables["Generators"];
                }

            }

            else
            {
                // do nothing
            }
        }

        private void saveLibraryButton_Click(object sender, EventArgs e)
        {
            libraryDataSet.WriteXml("Libraries.xml",XmlWriteMode.WriteSchema);
            saveLibraryButton.Enabled = false;
            
        }

        private void dataGridViewLoadsDGs_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            Console.Write("Error");

        }

        private void addCableButton_Click(object sender, EventArgs e)
        {
            if (!libraryDataSet.Tables.Contains("Conductors"))
            {
                saveLibraryButton.Enabled = false;
                return;
            }
            saveLibraryButton.Enabled = true;            
            addCableInt = dataGridViewLoadsDGs.Rows.Count;
            addCableInt++;          
            
            libraryDataSet.Tables["Conductors"].Rows.Add("Cable " + addCableInt.ToString(), 0.0, 0.0, 0.0, "description", false);          
            
        }
               

        private void dataGridViewLoadsDGs_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            saveLibraryButton.Enabled = true;
            if((e.ColumnIndex == 0)&&(loadTypeCombo.Text == "Conductors"))
            {
                dataGridViewLoadsDGs.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = "CableCode";

            }
        }

        private void dataGridViewLoadsDGs_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            //string myString = Convert.ToString(dataGridViewLoadsDGs.Rows[e.RowIndex].Cells[e.ColumnIndex].Value);
            //if (myString.Length >= 5)
            //{
            //    string subString = myString.Substring(0, 5);
            //    if (subString == "Cable")
            //    {
            //        e.Cancel = true;
            //    }
            //}
            //dataGridViewLoadsDGs.Rows[e.RowIndex].ErrorText = "invalid input";

        }

      

        private void dataGridViewLoadsDGs_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            

            
            // Clear the row error in case the user presses ESC.   
            dataGridViewLoadsDGs.Rows[e.RowIndex].ErrorText = String.Empty;
        }

        private void dataGridViewLoadsDGs_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsLetterOrDigit(e.KeyChar)&&(e.KeyChar != (char)Keys.Back)) e.Handled = true;
        }

        private void dataGridViewLoadsDGs_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            e.Control.KeyPress -= new KeyPressEventHandler(dataGridViewLoadsDGs_KeyPress);
            //Check columns whatever you required
            if (dataGridViewLoadsDGs.CurrentCell.ColumnIndex == 0)
            {
                TextBox tb = e.Control as TextBox;
                if (tb != null)
                {
                    tb.KeyPress += new KeyPressEventHandler(dataGridViewLoadsDGs_KeyPress);
                }
            }            
        }

        private void dataGridViewLoadsDGs_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

    }
}
