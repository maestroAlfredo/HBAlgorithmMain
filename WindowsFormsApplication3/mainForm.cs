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
            // Check if Libraries.xml exists, if it doesn't tell the user to create a library.
            if (File.Exists("Libraries.xml"))
            {
                DataSet ds = new DataSet();
                ds.ReadXml("Libraries.xml");
                if (!ds.Tables.Contains("Loads"))
                {
                    string message = "You do not have any Loads defined! Go to \"Edit Existing Libaries\" to define loads";
                    string caption = "No Loads defined!";
                    MessageBoxButtons buttons = MessageBoxButtons.OK;
                    DialogResult result = MessageBox.Show(message, caption, buttons, MessageBoxIcon.Error);
                }

                else
                {
                    nodeFeederForm frm = new nodeFeederForm();
                    frm.ShowDialog();
                }
            }

            else
            {
                string message = "You do not have a libary defined! Go to \"Libaries\" to create new libraries";
                string caption = "No library available!";
                MessageBoxButtons buttons = MessageBoxButtons.OK;
                DialogResult result = MessageBox.Show(message, caption, buttons, MessageBoxIcon.Error);
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            // Warn the user that old libraries will be erased.
            string message = "This will erase any existing libraries. If there are currently no libraries available or you would like to create a new library, you may continue.";
            string caption = "Warning!";
            MessageBoxButtons buttons = MessageBoxButtons.OKCancel;
            DialogResult result = MessageBox.Show(message, caption, buttons, MessageBoxIcon.Warning);

            if (result == System.Windows.Forms.DialogResult.OK)
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
                loadclick = 0;
                bool enabled = false;
                libraryForm frm = new libraryForm("Loads", enabled, libraryDataSet);
                frm.ShowDialog();
            }

            else
            {
                // do nothing
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            // Warn the user that old libraries will be erased.
            string message = "This will erase any existing libraries. If there are currently no libraries available or you would like to create a new library, you may continue.";
            string caption = "Warning!";
            MessageBoxButtons buttons = MessageBoxButtons.OKCancel;
            DialogResult result = MessageBox.Show(message, caption, buttons, MessageBoxIcon.Warning);

            if (result == System.Windows.Forms.DialogResult.OK)
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
                //genclick = 0;
                bool enabled = false;
                libraryForm frm = new libraryForm("Generators", enabled, libraryDataSet);
                frm.ShowDialog();
            }

            else
            {
                // do nothing
            }
        }

        private void editLibraryButton_Click(object sender, EventArgs e)
        {
            // Check if Libraries.xml exists, if it doesn't tell the user to create a library.
            if (File.Exists("Libraries.xml"))
            {
                DataSet ds = new DataSet();
                ds.ReadXml("Libraries.xml");
                bool enabled = true;
                libraryForm frm = new libraryForm("Loads", enabled, ds);
                frm.saveLibraryButton.Text = "Save";

                if (!ds.Tables.Contains("Loads"))
                {
                    //string message = "Click \"reset libraries\" to continue.";
                    //string caption = "No loads available";
                    //MessageBoxButtons buttons = MessageBoxButtons.OK;
                    //DialogResult result = MessageBox.Show(message, caption, buttons, MessageBoxIcon.Error);
                }

                frm.ShowDialog();

            }

            else
            {
                string message = "There's no library defined. Would you like to create a new one?";
                string caption = "No library available!";
                MessageBoxButtons buttons = MessageBoxButtons.YesNo;
                DialogResult result = MessageBox.Show(message, caption, buttons, MessageBoxIcon.Error);

                if (result == System.Windows.Forms.DialogResult.Yes) newConductorLibraryButton.PerformClick();
                else
                {
                    // do nothing.
                }
            }
        }

        private void editLibraryButton_MouseHover(object sender, EventArgs e)
        {
            //infoLabel.Text = "Click this button to edit the libraries that you may have predefined from previous projects";
            labelMainFormText.Text = "Edit existing libraries";
            labelMainFormText.Font = new Font(labelMainFormText.Font, FontStyle.Italic);

            // Make the text on the label bold
            var font = editLibraryButton.Font;
            editLibraryButton.Font = new Font(font, FontStyle.Bold);
            font.Dispose();
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
            // Warn the user that old libraries will be erased.
            string message = "This will erase any existing libraries. If there are currently no libraries available or you would like to create a new library, you may continue.";
            string caption = "Warning!";
            MessageBoxButtons buttons = MessageBoxButtons.OKCancel;
            DialogResult result = MessageBox.Show(message, caption, buttons, MessageBoxIcon.Warning);

            if (result == System.Windows.Forms.DialogResult.OK)
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

                /* Then reset the loads & DG's*/
                resetLoadsGens(libraryDataSet);

                bool enabled = true;
                libraryForm frm = new libraryForm("Conductors", enabled, libraryDataSet);
                frm.saveLibraryButton.Text = "Save";
                frm.ShowDialog();



            }

            else
            {
                // do nothing
            }
        }

        public void resetLoadsGens(DataSet libraryDataSet)
        {
            /* reset the loads and gens */
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
            //dataGridViewLoadsDGs.DataSource = libraryDataSet.Tables["Loads"];

            DataTable dtGens = new DataTable(); //creates a new Datatable object for the loads

            dtGens.TableName = "Generators";

            DataColumn dcg1 = new DataColumn("Generator"); //dreates the new datacolumn objects;
            DataColumn dcg2 = new DataColumn("alpha");
            DataColumn dcg3 = new DataColumn("beta");
            DataColumn dcg4 = new DataColumn("circuit breaker");
            DataColumn dcg5 = new DataColumn("description");
            DataColumn dcg6 = new DataColumn("Selected", typeof(bool));


            dtGens.Columns.Add(dcg1); //associates the columns to the dtLoads datatable
            dtGens.Columns.Add(dcg2);
            dtGens.Columns.Add(dcg3);
            dtGens.Columns.Add(dcg4);
            dtGens.Columns.Add(dcg5);
            dtGens.Columns.Add(dcg6);

            dtGens.PrimaryKey = new DataColumn[] { dtGens.Columns["Generator"] };

            for (int i = 0; i < 25; i++)
            {
                dtGens.Rows.Add("Gen " + (i + 1).ToString(), 0.0, 0.0, 0.0, "myGen", false);

            }


            if (libraryDataSet.Tables.Contains("Generators")) libraryDataSet.Tables.Remove("Generators");//remove any previously existing table from the dataset
            libraryDataSet.Tables.Add(dtGens);
            libraryDataSet.WriteXml("Libraries.xml", XmlWriteMode.WriteSchema);
            //dataGridViewLoadsDGs.DataSource = libraryDataSet.Tables["Generators"];

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

            if (ofd.ShowDialog() == DialogResult.OK)
            {
                string fileName = Path.ChangeExtension(ofd.FileName, ".xml");
                nodeFeederForm nff = new nodeFeederForm(fileName);
                nff.ShowDialog();
            }
        }

        private void splitContainer1_Panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void newFeederButton_MouseHover(object sender, EventArgs e)
        {
            labelMainFormText.Text = "Create a new feeder profile";
            labelMainFormText.Font = new Font(labelMainFormText.Font, FontStyle.Italic);

            // Make the text on the label bold
            var font = newFeederButton.Font;
            newFeederButton.Font = new Font(font, FontStyle.Bold);
            font.Dispose();

        }

        private void newFeederButton_MouseLeave(object sender, EventArgs e)
        {
            labelMainFormText.Text = "";

            // Make the text on the label regular
            var font = newFeederButton.Font;
            newFeederButton.Font = new Font(font, FontStyle.Regular);
            font.Dispose();
        }

        private void loadProjectButton_MouseHover(object sender, EventArgs e)
        {
            labelMainFormText.Text = "Work on a previously saved project";
            labelMainFormText.Font = new Font(labelMainFormText.Font, FontStyle.Italic);

            // Make the text on the label bold
            var font = loadProjectButton.Font;
            loadProjectButton.Font = new Font(font, FontStyle.Bold);
            font.Dispose();
        }

        private void loadProjectButton_MouseLeave(object sender, EventArgs e)
        {
            labelMainFormText.Text = "";

            // Make the text on the label regular
            var font = loadProjectButton.Font;
            loadProjectButton.Font = new Font(font, FontStyle.Regular);
            font.Dispose();
        }

        private void newConductorLibraryButton_MouseHover_1(object sender, EventArgs e)
        {
            labelMainFormText.Text = "Work on a previously saved project";
            labelMainFormText.Font = new Font(labelMainFormText.Font, FontStyle.Italic);

            // Make the text on the label bold
            var font = newConductorLibraryButton.Font;
            newConductorLibraryButton.Font = new Font(font, FontStyle.Bold);
            font.Dispose();
        }

        private void newConductorLibraryButton_MouseLeave(object sender, EventArgs e)
        {
            labelMainFormText.Text = "";
            labelMainFormText.Font = new Font(labelMainFormText.Font, FontStyle.Italic);

            // Make the text on the label bold
            var font = newConductorLibraryButton.Font;
            newConductorLibraryButton.Font = new Font(font, FontStyle.Regular);
            font.Dispose();
        }

        private void editLibraryButton_MouseLeave(object sender, EventArgs e)
        {
            labelMainFormText.Text = "Work on a previously saved project";
            labelMainFormText.Font = new Font(labelMainFormText.Font, FontStyle.Italic);

            // Make the text on the label bold
            var font = editLibraryButton.Font;
            editLibraryButton.Font = new Font(font, FontStyle.Regular);
            font.Dispose();
        }
    }
}
