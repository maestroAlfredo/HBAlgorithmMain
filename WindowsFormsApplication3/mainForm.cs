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
using System.IO;


namespace VoltageDropCalculatorApplication
{
    public partial class mainForm : Form
    {
        public static int loadclick = 1;
        public static int genclick = 1;

        OpenFileDialog ofd;

        DataSet libraryDataSet = new DataSet();

        public mainForm()
        {
            InitializeComponent();
            ofd = new OpenFileDialog();
            //libraryDataSet.ReadXml("Libraries.xml");
        }



        private void button4_Click(object sender, EventArgs e)
        {

        }


        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            nodeFeederForm frm = new nodeFeederForm();
            frm.ShowDialog();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            if (libraryDataSet.Tables.Contains("Load Types")) libraryDataSet.Tables.Remove("Load Types");//remove any previously existing table from the dataset
                        
            DataTable dtLoads = new DataTable(); //creates a new Datatable object for the loads

            dtLoads.TableName = "Load Types";

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
            loadclick = 0;            
            bool enabled = false;
            libraryForm frm = new libraryForm("Loads",enabled);
            frm.ShowDialog();
        }

        private void button7_Click(object sender, EventArgs e)
        {


            DataTable dtGens = new DataTable(); //creates a new Datatable object for the loads

            dtGens.TableName = "Gen Types";

            DataColumn dc1 = new DataColumn("Generator"); //dreates the new datacolumn objects;
            DataColumn dc2 = new DataColumn("alpha");
            DataColumn dc3 = new DataColumn("beta");
            DataColumn dc4 = new DataColumn("circuit breaker");
            DataColumn dc5 = new DataColumn("description");
            DataColumn dc6 = new DataColumn("Selected",typeof(bool));

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


            if (libraryDataSet.Tables.Contains("Gen Types")) libraryDataSet.Tables.Remove("Gen Types");//remove any previously existing table from the dataset
            libraryDataSet.Tables.Add(dtGens);
            libraryDataSet.WriteXml("Libraries.xml", XmlWriteMode.WriteSchema);
            //genclick = 0;
            bool enabled = false;
            libraryForm frm = new libraryForm("Generators", enabled);
            frm.ShowDialog();

        }

        private void editLibraryButton_Click(object sender, EventArgs e)
        {
            bool enabled = true;
            libraryForm frm = new libraryForm("Loads",enabled);            
            frm.ShowDialog();
        }

        private void editLibraryButton_MouseHover(object sender, EventArgs e)
        {
           //infoLabel.Text = "Click this button to edit the libraries that you may have predefined from previous projects";
        }

        private void newLoadLibraryButton_MouseHover(object sender, EventArgs e)
        {
            //infoLabel.Text = "Click this button to create a new load library.Note: Doing this will overite any existing load library";
        }

        private void newGeneratorLibraryButton_MouseHover(object sender, EventArgs e)
        {
           //infoLabel.Text = "Click this button to create a new generator library.Note: Doing this will overite any existing generator library";
        }

        private void newConductorLibraryButton_Click(object sender, EventArgs e)
        {
            DataTable conductorTable = new DataTable(); //creates a new Datatable object for the loads        

            conductorTable.TableName = "Conductor Table";

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

            if (libraryDataSet.Tables.Contains("Conductor Table")) libraryDataSet.Tables.Remove("Conductor Table");//remove any previously existing table from the dataset

            libraryDataSet.Tables.Add(conductorTable);
            libraryDataSet.WriteXml("Libraries.xml",XmlWriteMode.WriteSchema);

            bool enabled = false;
            libraryForm frm = new libraryForm("Conductors", enabled);
            frm.ShowDialog();
        }

        private void splitContainer1_Panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void tableLayoutPanel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void tableLayoutPanel4_Paint(object sender, PaintEventArgs e)
        {

        }

        private void newConductorLibraryButton_MouseHover(object sender, EventArgs e)
        {
            //infoLabel.Text = "Click this button to create a new Conductor Library. Note: Doing this will overite any existing load library";
        }

        private void infoLabel_Click(object sender, EventArgs e)
        {

        }

        private void loadProjectButton_Click(object sender, EventArgs e)
        {
            ofd.Filter = "HBA file(*.hba)|*.hba";
            
            if(ofd.ShowDialog()== DialogResult.OK)
            {
                string fileName = Path.ChangeExtension(ofd.FileName, ".xml");
                nodeFeederForm nff = new nodeFeederForm(fileName);                
                nff.ShowDialog();
            }
        }
    }
}
