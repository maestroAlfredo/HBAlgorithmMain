using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using System.IO;

namespace VoltageDropCalculatorApplication
{
    public partial class nodeFeederForm : Form
    {

        public static int newProjectClicked = 1; //tracks if the user has clicked the create new project button
        public string projectName;
        public string projectSavedName = "";
        double p;
        double t2;
        double rT2;
        double k1;
        double Vs;
        bool ShownForm = false;
        Graphics drawArea;
        int k; //k is the number of nodes in the main feeder
        ErrorProvider errorProvider1 = new ErrorProvider();

        SolidBrush sb;


        public static double lengthTol = 0.01;


        int nodeCountInt = 1;
        int loadCount;
        int genCount;
        DataSet nodeDataSet = new DataSet();
        DataSet tempNodeDataSet = new DataSet(); //Dataset that stores the tables that are only in the main feeder
        List<int> mfNodeList = new List<int>(); //intlist that stores the nodes in the main feeder
        DataSet projectDataSet = new DataSet();
        DataSet loadedDataSet = new DataSet(); //loaded dataset that will contain the tables from the saved .hba file
        DataSet parameters = new DataSet();
        DataTable paramDataTable = new DataTable("Parameter Table");
        DataTable resultLoadsGens = new DataTable();

        DataTable tempTable = new DataTable();//temporary table that stores the node data to put in the main feederdataset
        DataSet libraryDataSet = new DataSet();
        BindingList<int> nodeNumList = new BindingList<int>();
        BindingList<string> nodeNameList = new BindingList<string>();
        TreeNode rootNode = new TreeNode();
        //List<string> addList;

         string fileNameAppData = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\HBAlgorithm\\", "Libraries.xml");
         string originalfileNameAppData = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\HBAlgorithm\\", "Untitled.xml"); //original xml fileName

        public nodeFeederForm()
        {
            InitializeComponent();
            addHandlers();
            sb = new SolidBrush(Color.SteelBlue);

            libraryDataSet.ReadXml(fileNameAppData); //read the  library xml  
            List<string> cableString = libraryDataSet.Tables["Conductors"].AsEnumerable().Select(x => x[0].ToString()).ToList();
            cableSelectCombo.DataSource = cableString;

            mfNodeList.Add(1); //adds the first node to the main feeder node list            
            loadCount = 0;
            genCount = 0;

            drawArea = drawingPanel.CreateGraphics();

            p = 10;
            t2 = 40.0;
            Vs = 230.0;


            DataColumn dc1 = new DataColumn("active", typeof(bool));
            DataColumn dc2 = new DataColumn("passive", typeof(bool));
            DataColumn dc3 = new DataColumn("sourceVoltage", typeof(double));
            DataColumn dc4 = new DataColumn("temp", typeof(double));
            DataColumn dc5 = new DataColumn("Current Node", typeof(string));

            paramDataTable.Columns.Add(dc1); //associates the columns to the parameter datatable
            paramDataTable.Columns.Add(dc2);
            paramDataTable.Columns.Add(dc3);
            paramDataTable.Columns.Add(dc4);
            paramDataTable.Columns.Add(dc5);

            paramDataTable.Rows.Add(false, true, Vs, t2, "node1");

            //newProjectClicked = 0;
            //projectName = "Untitled" + ".xml"; //creates a string in projectName that will contain the reference to the xml file
            this.Text = "Untitled";
            projectName = originalfileNameAppData;
            nodeNameTextBox.Text = "node 1";
            tableLayoutPanel4.Enabled = true;
            proceedToVCalcButton.Enabled = true;
            drawArea.FillEllipse(sb, 20, drawingPanel.Height / 2, 10, 10);
            detailsCheckBox.Enabled = true;

            //totalNodeNumberNumericUpDown.Enabled = false;
            //continueButton.Enabled = false;
            selectEndNodeCombo.Enabled = true;
            selectEndNodeCombo.DataSource = nodeNumList;
            nodeNumList.Add(1);
            nodeNameList.Add(nodeNameTextBox.Text);
            //activeRadio.Enabled = false;
            //passiveRadio.Enabled = false;
            nodeNumCombo.Enabled = true;
            nodeNumCombo.DataSource = nodeNumList;
            nodeNameCombo.DataSource = nodeNameList;

            selectEndNodeCombo.Enabled = true;
            selectEndNodeCombo.DataSource = nodeNumList;
            endNodeCombo.Enabled = true;
            endNodeCombo.DataSource = nodeNameList;
            addNodeButton.Enabled = true;



            DataTable nodeDataTable = new DataTable();
            List<string> headings = new List<string> { "Node", "Name", "Load/DG", "Red", "White", "Blue", "Alpha", "Beta", "Cb", "Length", "Cable", "Rp", "Rn", "Parent", "Children" };
            for (int i = 0; i < headings.Count; i++)
            {
                nodeDataTable.Columns.Add(headings[i]); //adds the headings to the nodeDataTable 
                if ((i == 3) || (i == 4) || (i == 5))
                {
                    nodeDataTable.Columns[i].DataType = typeof(int);
                }
            }

            //querires the datatables for the loads and gens where the user has selected
            DataTable resultLoads = new DataTable();
            var table = libraryDataSet.Tables["Loads"].Select("Selected = true");
            if (table.AsEnumerable().Any())
            {
                resultLoads = table.CopyToDataTable();
                loadCount = resultLoads.Rows.Count;
            }

            DataTable resultGens = new DataTable();
            var table1 = libraryDataSet.Tables["Generators"].Select("Selected = true");
            if (table1.AsEnumerable().Any())
            {
                resultGens = table1.CopyToDataTable();
                genCount = resultGens.Rows.Count;
            }

            if (activeRadio.Checked == true) p = 90.0;

            foreach (DataRow dr in resultGens.Rows)
            {
                if (table.AsEnumerable().Any()) resultLoads.Rows.Add(dr.ItemArray);
                else
                {
                    resultLoads = resultGens.Copy();
                    break;
                }

            }

            resultLoadsGens = resultLoads.Copy();
            for (int i = 0; i < resultLoads.Rows.Count; i++)
            {
                nodeDataTable.Rows.Add("1", nodeNameTextBox.Text, resultLoads.Rows[i][0], 0.0, 0.0, 0.0, resultLoads.Rows[i]["alpha"], resultLoads.Rows[i]["beta"], resultLoads.Rows[i]["circuit breaker"], calculateLengths(lengthNumericUpDown.Value, i), cableSelectCombo.Text, calculateRp(rT2, calculateLengths(lengthNumericUpDown.Value, i)), calculateRn(rT2, calculateLengths(lengthNumericUpDown.Value, i), k1), 0, 0);
            }
            nodeDataTable.TableName = "node1";
            tempTable = nodeDataTable.Copy();
            tempTable.Rows[0][0] = "0"; //makes the first entry zero .
            nodeDataSet.Tables.Add(nodeDataTable);


            nodeDataSet.WriteXml(projectName);
            nodeDataGridView.DataSource = nodeDataSet.Tables[0];
            nodeDataGridView.Columns[0].Visible = false;
            nodeDataGridView.Columns[9].Visible = false;
            nodeDataGridView.Columns[10].Visible = false;
            nodeDataGridView.Columns[11].Visible = false;
            nodeDataGridView.Columns[12].Visible = false;
            nodeDataGridView.Columns[13].Visible = false;
            nodeDataGridView.Columns[14].Visible = false;
            nodeDataGridView.Columns[11].DefaultCellStyle.Format = "N3";
            nodeDataGridView.Columns[12].DefaultCellStyle.Format = "N3";
            closeTableEdits();

        }

        //constructor within constructor... helps to load the same form with the relevant objects initialized 
        public nodeFeederForm(string projectFileName)
        {
            InitializeComponent();
            addHandlers();
            sb = new SolidBrush(Color.SteelBlue);

            string projhba = Path.ChangeExtension(projectFileName, ".hba");
            projectSavedName = projhba;

            if (File.Exists(projectFileName))
            {
                System.IO.File.Delete(projectFileName);
            }
            System.IO.File.Move(projhba, projectFileName);
            projectDataSet.ReadXml(projectFileName); //read the  projectXML 
            libraryDataSet.Tables.Clear();
            libraryDataSet.Merge(projectDataSet.Tables["Conductors"]);
            libraryDataSet.Merge(projectDataSet.Tables["Loads"]);
            if (projectDataSet.Tables.Contains("Generators")) libraryDataSet.Merge(projectDataSet.Tables["Generators"]);
            paramDataTable = projectDataSet.Tables["Parameter Table"];
            List<string> cableString = libraryDataSet.Tables["Conductors"].AsEnumerable().Select(x => x[0].ToString()).ToList();
            cableSelectCombo.DataSource = cableString;

            mfNodeList.Add(1); //adds the first node to the main feeder node list            
            loadCount = 0;
            genCount = 0;

            //querires the datatables for the loads and gens where the user has selected
            DataTable resultLoads = new DataTable();
            var table = libraryDataSet.Tables["Loads"].Select("Selected = true");
            if (table.AsEnumerable().Any())
            {
                resultLoads = table.CopyToDataTable();
                loadCount = resultLoads.Rows.Count;
            }

            DataTable resultGens = new DataTable();
            var table1 = libraryDataSet.Tables["Generators"].Select("Selected = true");
            if (table1.AsEnumerable().Any())
            {
                resultGens = table1.CopyToDataTable();
                genCount = resultGens.Rows.Count;
            }

            drawArea = drawingPanel.CreateGraphics();

            p = 10;
            if ((bool)projectDataSet.Tables["Parameter Table"].Rows[0]["active"])
            {
                p = 90;
                activeRadio.Checked = true;
                passiveRadio.Checked = false;
            }
            else
            {
                activeRadio.Checked = false;
                passiveRadio.Checked = true;
            }

            t2 = Convert.ToDouble(projectDataSet.Tables["Parameter Table"].Rows[0]["temp"]);
            Vs = Convert.ToDouble(projectDataSet.Tables["Parameter Table"].Rows[0]["sourceVoltage"]);




            //newProjectClicked = 0;
            projectName = projectFileName;
            string fullpath;
            fullpath = Path.ChangeExtension(projectFileName, ".hba");
            this.Text = Path.GetFileNameWithoutExtension(fullpath);
            if (File.Exists(fullpath))
            {
                System.IO.File.Delete(fullpath);
            }
            System.IO.File.Move(projectFileName, fullpath);
            //createNewProjectButton.Enabled = false;
            //projectNameTextBox.Enabled = false;
            //SolidBrush sb = new SolidBrush(Color.SteelBlue);
            string tableName = Convert.ToString(projectDataSet.Tables["Parameter Table"].Rows[0]["Current Node"]); //string that has the tablename of the last node that was selected
            nodeNameTextBox.Text = Convert.ToString(projectDataSet.Tables[tableName].Rows[0]["Name"]);
            tableLayoutPanel4.Enabled = true;
            proceedToVCalcButton.Enabled = true;
            drawArea.FillEllipse(sb, 20, drawingPanel.Height / 2, 10, 10);
            detailsCheckBox.Enabled = true;

            //totalNodeNumberNumericUpDown.Enabled = false;
            //continueButton.Enabled = false;
            selectEndNodeCombo.Enabled = true;
            selectEndNodeCombo.DataSource = nodeNumList;

            //initialize the nodeNumlist based on the number of nodes in the project
            nodeCountInt = 0;
            nodeDataSet.Tables.Clear();
            for (int i = 0; i < projectDataSet.Tables.Count - 4; i++)
            {
                nodeNumList.Add(i + 1);
                nodeNameList.Add(Convert.ToString(projectDataSet.Tables[i + 4].Rows[0]["Name"]));
                DataTable dtCopy = new DataTable();
                dtCopy = projectDataSet.Tables[i + 4].Copy();
                nodeDataSet.Tables.Add(dtCopy);
                nodeCountInt++;
            }

            operatingTempNumUpDown.Value = Convert.ToDecimal(t2);
            sourceVoltageNumUpDown.Value = Convert.ToDecimal(Vs);
            //activeRadio.Enabled = false;
            //passiveRadio.Enabled = false;
            nodeCount.Text = nodeCountInt.ToString();
            nodeNumCombo.Enabled = true;
            nodeNumCombo.DataSource = nodeNumList;
            nodeNameCombo.DataSource = nodeNameList;

            selectEndNodeCombo.Enabled = true;
            selectEndNodeCombo.DataSource = nodeNumList;
            endNodeCombo.Enabled = true;
            endNodeCombo.DataSource = nodeNameList;
            addNodeButton.Enabled = true;

            nodeNameCombo.Text = Convert.ToString(nodeDataSet.Tables[Convert.ToString(projectDataSet.Tables["Parameter Table"].Rows[0]["Current Node"])].Rows[0]["Name"]);
            nodeDataGridView.DataSource = nodeDataSet.Tables[Convert.ToString(projectDataSet.Tables["Parameter Table"].Rows[0]["Current Node"])];
            tempTable = nodeDataSet.Tables[0].Copy();
            tempTable.Rows[0][0] = "0"; //makes the first entry zero .
            foreach (DataRow dr in tempTable.Rows)
            {
                dr[3] = 0.0;
                dr[4] = 0.0;
                dr[5] = 0.0;
            }
            nodeDataGridView.Columns[0].Visible = false;
            nodeDataGridView.Columns[9].Visible = false;
            nodeDataGridView.Columns[10].Visible = false;
            nodeDataGridView.Columns[11].Visible = false;
            nodeDataGridView.Columns[12].Visible = false;
            nodeDataGridView.Columns[13].Visible = false;
            nodeDataGridView.Columns[14].Visible = false;
            nodeDataGridView.Columns[11].DefaultCellStyle.Format = "N3";
            nodeDataGridView.Columns[12].DefaultCellStyle.Format = "N3";
            closeTableEdits();

        }

        private void projectNameTextBox_TextChanged(object sender, EventArgs e)
        {
            //if (projectNameTextBox.Text == "") createNewProjectButton.Enabled = false;
            //else createNewProjectButton.Enabled = true;
        }

        private void createNewProjectButton_Click(object sender, EventArgs e)
        {

            //newProjectClicked = 0;
            //projectName = projectNameTextBox.Text + ".xml"; //creates a string in projectName that will contain the reference to the xml file
            //createNewProjectButton.Enabled = false;
            //projectNameTextBox.Enabled = false;
            //continueButton.Enabled = true;

        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void continueButton_Click(object sender, EventArgs e)
        {

        }
        //checks to see if the nodeNumComboBox Text has been changed and then updates the datagrid view to the selected table.
        private void nodeNumCombo_TextChanged(object sender, EventArgs e)
        {
            if (nodeDataSet.Tables.Contains("node" + nodeNumCombo.Text))
            {
                nodeNameTextBox.Text = nodeDataSet.Tables["node" + nodeNumCombo.Text].Rows[0][1].ToString();
                lengthNumericUpDown.Value = Convert.ToDecimal(Convert.ToDouble(nodeDataSet.Tables["node" + nodeNumCombo.Text].Rows[0][9]) + ((loadCount == 0) ? (genCount - 1.0) * lengthTol : (loadCount - 1.0) * lengthTol));
                cableSelectCombo.Text = nodeDataSet.Tables["node" + nodeNumCombo.Text].Rows[0][10].ToString();

            }


            if (nodeNumCombo.Text == "1") { deleteNodeButton.Enabled = false; }//disable a user from deleting node 1. 
            else deleteNodeButton.Enabled = true;



            for (int i = 0; i < nodeCountInt; i++)
            {
                if (((Convert.ToInt16(nodeNumCombo.Text) - 1) == i) && (nodeDataSet.Tables.Count > 0))
                {
                    nodeDataGridView.DataSource = nodeDataSet.Tables[i];

                }
            }
        }

        private void proceedToVCalcButton_Click(object sender, EventArgs e)
        {
            bool finished = true;
            foreach (DataTable dtable in nodeDataSet.Tables)
            {
                if (Convert.ToString(dtable.Rows[0][0]) == "0")
                {
                    finished = false;
                    break;
                }
            }

            //if all the tables have valid data proceed
            if (finished)
            {
                tempNodeDataSet.Tables.Clear();

                DataTable[] nodeVecDataTables = createNodeVecDatatable(mfNodeList);

                for (int i = 0; i < nodeVecDataTables.GetLength(0); i++)
                {
                    tempNodeDataSet.Tables.Add(nodeVecDataTables[i]);
                }

                DataTable[] voltageVectorDataTable = new DataTable[3];
                DataRow voltageRow;

                for (int k = 0; k < 3; k++)
                {
                    voltageVectorDataTable[k] = new DataTable();
                    for (int i = 0; i < 35; i++)
                    {
                        //creates column headings numbered 1 to 35
                        voltageVectorDataTable[k].Columns.Add(Convert.ToString(i + 1), typeof(Double));
                    }


                    for (int i = 0; i < (loadCount + genCount) * mfNodeList.Count; i++)
                    {
                        voltageRow = voltageVectorDataTable[k].NewRow();
                        for (int j = 0; j < 35; j++)
                        {
                            voltageRow[j] = 0.0;
                        }
                        voltageVectorDataTable[k].Rows.Add(voltageRow);
                    }
                    tempNodeDataSet.Tables.Add(voltageVectorDataTable[k]);
                }
                //double[] voltageRowDouble = new double[35];

                //tempNodeDataSet.WriteXml(projectName);

                //voltageCalculationForm frm = new voltageCalculationForm(projectName, p, t2, Vs, loadCount, genCount, mfNodeList, nodeDataSet);
                voltageCalculationForm frm2 = new voltageCalculationForm(p, t2, Vs, loadCount, genCount, lengthTol, mfNodeList, nodeDataSet, tempNodeDataSet, nodeDataGridView, libraryDataSet, lengthNumericUpDown, this);
                frm2.ShowDialog();
            }
            else
            {
                MessageBox.Show("Cannot Proceed without specifying Loads or DGs for each individual node! Please check that all nodes have at least one Load or Generator");
            }
        }

        private void cableSelectCombo_TextChanged(object sender, EventArgs e)
        {
            if (ShownForm) changeFormTitle();
            //goes through the conductor library first column to see if the combobox text matches the library conductor column text
            for (int i = 0; i < libraryDataSet.Tables["Conductors"].Rows.Count; i++)
            {
                if (Convert.ToString(libraryDataSet.Tables["Conductors"].Rows[i][0]) == cableSelectCombo.Text)
                {
                    t2 = Convert.ToDouble(operatingTempNumUpDown.Value);
                    rT2 = Convert.ToDouble(libraryDataSet.Tables["Conductors"].Rows[i][1]) * (Convert.ToDouble(libraryDataSet.Tables["Conductors"].Rows[i][2]) + t2) / (Convert.ToDouble(libraryDataSet.Tables["Conductors"].Rows[i][2]) + 20.0);
                    k1 = Convert.ToDouble(libraryDataSet.Tables["Conductors"].Rows[i][3]);
                    break;
                }
            }

            if (nodeDataSet.Tables.Contains("node" + nodeNumCombo.Text))
            {
                for (int i = 0; i < nodeDataSet.Tables["node" + nodeNumCombo.Text].Rows.Count; i++)
                {
                    nodeDataSet.Tables["node" + nodeNumCombo.Text].Rows[i][10] = cableSelectCombo.Text;
                    nodeDataSet.Tables["node" + nodeNumCombo.Text].Rows[i][11] = calculateRp(rT2, calculateLengths(lengthNumericUpDown.Value, i));
                    nodeDataSet.Tables["node" + nodeNumCombo.Text].Rows[i][12] = calculateRn(rT2, calculateLengths(lengthNumericUpDown.Value, i), k1);
                }
            }
        }

        private void lengthNumericUpDown_ValueChanged(object sender, EventArgs e)
        {
            if (ShownForm) changeFormTitle();
            //goes through the conductor library first column to see if the combobox text matches the library conductor column text
            for (int i = 0; i < libraryDataSet.Tables["Conductors"].Rows.Count; i++)
            {
                if (Convert.ToString(libraryDataSet.Tables["Conductors"].Rows[i][0]) == cableSelectCombo.Text)
                {
                    rT2 = Convert.ToDouble(libraryDataSet.Tables["Conductors"].Rows[i][1]) * (Convert.ToDouble(libraryDataSet.Tables["Conductors"].Rows[i][2]) + t2) / (Convert.ToDouble(libraryDataSet.Tables["Conductors"].Rows[i][2]) + 20.0);
                    k1 = Convert.ToDouble(libraryDataSet.Tables["Conductors"].Rows[i][3]);
                }
            }

            if (nodeDataSet.Tables.Contains("node" + nodeNumCombo.Text))
            {
                for (int i = 0; i < nodeDataSet.Tables["node" + nodeNumCombo.Text].Rows.Count; i++)
                {
                    nodeDataSet.Tables["node" + nodeNumCombo.Text].Rows[i][9] = calculateLengths(lengthNumericUpDown.Value, i);
                    nodeDataSet.Tables["node" + nodeNumCombo.Text].Rows[i][11] = calculateRp(rT2, calculateLengths(lengthNumericUpDown.Value, i));
                    nodeDataSet.Tables["node" + nodeNumCombo.Text].Rows[i][12] = calculateRn(rT2, calculateLengths(lengthNumericUpDown.Value, i), k1);
                }

            }

            
            //mfNodeDictionary.Clear();

            if (selectEndNodeCombo.Text != "")
            {
                mfNodeList.Clear();//Clears the main feeder node list                
                getMainFeederNodes(Convert.ToInt32(selectEndNodeCombo.Text), mfNodeList);
                drawPoints(mfNodeList);
            }
            else
            {
                drawPoints(mfNodeList);
            }
        }

        private void addNodeButton_Click(object sender, EventArgs e)
        {
            editTable();            
            if (ShownForm) changeFormTitle();
            //totalNodeNumberNumericUpDown.Value = totalNodeNumberNumericUpDown.Value + 1;
            nodeCountInt++;
            TreeNode thisNode = new TreeNode();
            //rootNode.;            

            //drawArea.FillEllipse(sb, Cordinate(nodeCountInt).X, Cordinate(nodeCountInt).Y, 10, 10);

            DataTable nodeDataTable = new DataTable();
            List<string> headings = new List<string> { "Node", "Name", "Load/DG", "Red", "White", "Blue", "Alpha", "Beta", "Cb", "Length", "Cable", "Rp", "Rn", "Parent", "Children" };
            for (int i = 0; i < headings.Count; i++)
            {
                nodeDataTable.Columns.Add(headings[i]); //adds the headings to the nodeDataTable  
                if((i==3)||(i==4)||(i==5))
                {
                    nodeDataTable.Columns[i].DataType = typeof(int);
                }
            }

            //querires the datatables for the loads and gens where the user has selected

            DataTable resultLoads = new DataTable();

            var table = libraryDataSet.Tables["Loads"].Select("Selected = true");
            if (table.AsEnumerable().Any())
            {
                resultLoads = table.CopyToDataTable();
                loadCount = resultLoads.Rows.Count;
            }

            DataTable resultGens = new DataTable();
            var table1 = libraryDataSet.Tables["Generators"].Select("Selected = true");
            if (table1.AsEnumerable().Any())
            {
                resultGens = table1.CopyToDataTable();
                genCount = resultGens.Rows.Count;
            }


            //adds the selected Gens to the selected loads table
            foreach (DataRow dr in resultGens.Rows)
            {
                if (table.AsEnumerable().Any()) resultLoads.Rows.Add(dr.ItemArray);
                else
                {
                    resultLoads = resultGens.Copy();

                    break;
                }
            }


            resultLoadsGens = resultLoads.Copy();

            for (int i = 0; i < resultLoads.Rows.Count; i++)
            {

                nodeDataTable.Rows.Add(autoGeneratedNodeName("node " + nodeCountInt.ToString()).Remove(0, 5), autoGeneratedNodeName("node " + nodeCountInt.ToString()), resultLoads.Rows[i][0], 0.0, 0.0, 0.0, resultLoads.Rows[i]["alpha"], resultLoads.Rows[i]["beta"], resultLoads.Rows[i]["circuit breaker"], calculateLengths(lengthNumericUpDown.Value, i), cableSelectCombo.Text, calculateRp(rT2, calculateLengths(lengthNumericUpDown.Value, i)), calculateRn(rT2, calculateLengths(lengthNumericUpDown.Value, i), k1), nodeNumCombo.Text, 0);

            }

            //names the table in the datatable and adds it to the dataset
            nodeDataTable.TableName = "node" + Convert.ToString(nodeCountInt);
            nodeDataSet.Tables.Add(nodeDataTable);

            if (Convert.ToString(nodeDataSet.Tables[Convert.ToInt16(nodeNumCombo.Text) - 1].Rows[0][14]) != "0")
            {
                nodeDataSet.Tables[Convert.ToInt16(nodeNumCombo.Text) - 1].Rows[0][14] = Convert.ToString(nodeDataSet.Tables[Convert.ToInt16(nodeNumCombo.Text) - 1].Rows[0][14]) + ";" + Convert.ToString(nodeCountInt);
            }
            else
            {
                nodeDataSet.Tables[Convert.ToInt16(nodeNumCombo.Text) - 1].Rows[0][14] = Convert.ToString(nodeCountInt);
            }

            //TreeNode<int> thisNode = myNode.Children.Add(;
            //myNode = thisNode;
            DataSet ds = new DataSet();
            ds.WriteXml(projectName); //clear the xml file
            nodeDataSet.WriteXml(projectName);

            nodeCount.Text = Convert.ToString(nodeCountInt);

            nodeNumList.Add(Convert.ToInt16(nodeCountInt));
            nodeNameList.Add(autoGeneratedNodeName("node " + nodeCountInt.ToString()));
            nodeNumCombo.DataSource = nodeNumList;
            nodeNameCombo.DataSource = nodeNameList;
            selectEndNodeCombo.DataSource = nodeNumList;
            endNodeCombo.DataSource = nodeNameList;
            //nodeNumCombo.Update();
            nodeNumCombo.SelectedIndex = nodeNumCombo.Items.Count - 1;
            nodeNameCombo.SelectedIndex = nodeNameCombo.Items.Count - 1;


            lengthNumericUpDown.Value = 100M;
            nodeDataGridView.DataSource = nodeDataSet.Tables[nodeCountInt - 1];//changes the datagrid view to display the new node.   
            closeTableEdits();
        }

        //This function separates the items in the delimited string (parameter 1) at the delimiter (parameter 2), 
        //creates a new generic string list, and then adds each item to the new list. Finally, it returns the filled list.
        public static List<string> ConvertStringsToStringList(string items, char separator)
        {
            List<string> list = new List<string>();
            string[] listItmes = items.Split(separator);
            foreach (string item in listItmes)
            {
                list.Add(item);
            }
            return list;
        }

        private void deleteNodeButton_Click(object sender, EventArgs e)
        {
            const string message = "Clicking this button will delete the currently displayed node and ALL its child nodes. Do you want to proceed?";
            const string caption = "Delete Node";
            var result = MessageBox.Show(message, caption,
                                         MessageBoxButtons.YesNo,
                                         MessageBoxIcon.Exclamation);

            // If the no button was pressed ... 
            if (result == DialogResult.No)
            {
                //Do Nothing               

            }
            else
            {
                if (ShownForm) changeFormTitle();
                editTable();
                //removes the node from the children of the parent node by setting the children field to zero or null
                string parentNode = Convert.ToString(nodeDataSet.Tables["node" + nodeNumCombo.Text].Rows[0][13]);
                if (Convert.ToString(nodeDataSet.Tables["node" + parentNode].Rows[0][14]).Length == 1)
                {
                    nodeDataSet.Tables["node" + parentNode].Rows[0][14] = "0"; //sets the children of the parent node to zero if there was only one child
                }
                else
                {
                    string replacement = null;
                    string originalString = Convert.ToString(nodeDataSet.Tables["node" + parentNode].Rows[0][14]);
                    originalString = originalString.Replace(originalString.Contains(nodeNumCombo.Text + ";") ? nodeNumCombo.Text + ";" : nodeNumCombo.Text, replacement); //updates the children to null if there is more than one child
                    nodeDataSet.Tables["node" + parentNode].Rows[0][14] = originalString;
                }

                deleteNode("node" + nodeNumCombo.Text);
                nodeCountInt = nodeDataSet.Tables.Count;


                for (int i = 0; i < nodeDataSet.Tables.Count; i++)
                {
                    string originalName = nodeDataSet.Tables[i].TableName;
                    //originalName = originalName.Remove(0, 4);

                    renameNode(originalName, "node" + (i + 1).ToString()); //renames the remaining nodes                              

                }
                for (int i = 0; i < nodeDataSet.Tables.Count; i++) nodeDataSet.Tables[i].TableName = "node" + (i + 1).ToString(); //renames the tables.

                for (int i = nodeNumList.Count - 1; i >= nodeDataSet.Tables.Count; i--)
                {
                    nodeNumList.RemoveAt(i);
                }

                DataSet ds = new DataSet();//emptydataset to clear the  xml files of previous tables
                ds.WriteXml(projectName);
                nodeDataSet.WriteXml(projectName);

                nodeCountInt = nodeDataSet.Tables.Count;
                nodeCount.Text = Convert.ToString(nodeCountInt);
                nodeNumCombo.DataSource = nodeNumList;
                nodeNameCombo.DataSource = nodeNameList;
                nodeNameCombo.Update();
                nodeNumCombo.Update();
                nodeNumCombo.SelectedIndex = nodeNumCombo.Items.Count - 1;
                nodeNameCombo.SelectedIndex = nodeNameCombo.Items.Count - 1;
                nodeNameTextBox.Text = nodeNameCombo.Text;
                nodeDataGridView.DataSource = nodeDataSet.Tables[nodeDataSet.Tables.Count - 1];
                drawArea.Clear(Color.White);
                drawPoints(mfNodeList);
                closeTableEdits();
            }            
        }

        //recursive method that deletes a node as well as its children and their children etc
        private void deleteNode(string nodeNumber)
        {
            if ((nodeNumber != "node0") && (nodeNumber != "node"))
            {
                string childNodes = Convert.ToString(nodeDataSet.Tables[nodeNumber].Rows[0][14]);
                List<string> nodesToRemoveList = new List<string>();

                nodesToRemoveList = ConvertStringsToStringList(childNodes, ';');
                //if (nodesToRemoveList.Contains("0")) nodesToRemoveList.Remove("0");
                if (nodesToRemoveList.Contains(null)) nodesToRemoveList.Remove(null);

                for (int i = 0; i < nodesToRemoveList.Count; i++)
                {
                    deleteNode("node" + Convert.ToString(nodesToRemoveList[i]));
                }
                nodeNameList.RemoveAt(nodeNameList.IndexOf(nodeDataSet.Tables[nodeNumber].Rows[0][1].ToString()));
                nodeDataSet.Tables.Remove(nodeNumber);

            }

        }

        private void renameNode(string oldNodeName, string newNodeName)
        {

            string childNodes = Convert.ToString(nodeDataSet.Tables[oldNodeName].Rows[0][14]);
            string newNodeNum = Convert.ToString(newNodeName).Remove(0, 4);
            string oldNodeNum = Convert.ToString(oldNodeName).Remove(0, 4);
            List<string> nodesToChangeParent = new List<string>();
            DataTable dt = new DataTable();

            int[] array = new int[2];
            //array[0] = 5;
            //array[1] = 6;

            //updates the children of  the node
            nodesToChangeParent = ConvertStringsToStringList(childNodes, ';');
            if (oldNodeNum == "8")
            { Console.Write(Convert.ToString(nodesToChangeParent[0])); }
            for (int i = 0; i < nodesToChangeParent.Count; i++)
            {
                if ((Convert.ToString(nodesToChangeParent[i]) != "0") && (Convert.ToString(nodesToChangeParent[i]) != ""))
                {
                    nodeDataSet.Tables["node" + Convert.ToString(nodesToChangeParent[i])].Rows[0][13] = newNodeNum;

                }
            }

            //updates the parents of the node
            string parentNode = Convert.ToString(nodeDataSet.Tables[oldNodeName].Rows[0][13]);
            if (parentNode != "0")
            {
                dt = (nodeDataSet.Tables["node" + parentNode] != null) ? nodeDataSet.Tables["node" + parentNode] : nodeDataSet.Tables[Convert.ToInt16(parentNode) - 1];
                if (Convert.ToString(dt.Rows[0][14]).Length == 1)
                {
                    dt.Rows[0][14] = newNodeNum; //sets the children of the parent node to newName if there was only one child
                }
                else
                {
                    string replacement = newNodeNum;
                    string originalString = Convert.ToString(dt.Rows[0][14]);
                    //originalString = originalString.Replace(originalString.Contains(oldNodeNum + ";") ? oldNodeNum + ";" :";"+ oldNodeNum, replacement); //updates the children to newName
                    originalString = originalString.Replace(oldNodeNum, replacement);
                    dt.Rows[0][14] = originalString;
                }

            }

        }

        //method gets the nodes in a main feeder list and then calculates the positions of those nodes.
        private void drawPoints(List<int> mainfeederList)
        {
            drawArea.Clear(Color.White);
            //SolidBrush sb = new SolidBrush(Color.SteelBlue);
            Pen myPen = new Pen(sb);
            double drawingSpace = (double)drawingPanel.Width - 30;
            

            int y = drawingPanel.Height / 2;

            if(mainfeederList.Count<=5)
            {
                double nodespacing = drawingSpace / mainfeederList.Count;
                for (int i = 0; i < mainfeederList.Count; i++)
                {
                    string label = "node 1";
                    if (nodeDataSet.Tables.Contains("node" + mainfeederList[i].ToString()))
                    {
                        label = nodeDataSet.Tables["node" + mainfeederList[i].ToString()].Rows[0][1].ToString();
                    }
                    System.Drawing.Font drawFont = new System.Drawing.Font("Arial", 10);
                    System.Drawing.Font drawFont1 = new System.Drawing.Font("Arial", 8);
                    System.Drawing.Font drawFont2 = new System.Drawing.Font("Arial", 6);

                    if (i == 0)
                    {

                        drawArea.DrawEllipse(myPen, 0, y - 5, 20, 20);
                        drawArea.DrawEllipse(myPen, 10, y - 5, 20, 20);
                        drawArea.DrawLine(myPen, 30, y + 5, 60, y + 5);
                        drawArea.DrawString(Convert.ToInt16(Math.Round(Convert.ToDouble(nodeDataSet.Tables[0].Rows[0][9]))) + "m", drawFont2, sb, 33, (float)y - 10);

                    }
                    drawArea.FillEllipse(sb, 60 + (i * (int)nodespacing), y, 10, 10); //draws the circle
                    if (i != mainfeederList.Count - 1)
                    {
                        drawArea.DrawLine(myPen, 60 + (i * (float)nodespacing), y + 5, 60 + ((i + 1) * (float)nodespacing), y + 5); //draws the lines
                    }

                    if (nodeDataSet.Tables.Contains("node" + mainfeederList[i].ToString()))
                    {
                        drawArea.DrawString(Convert.ToInt16(Math.Round(Convert.ToDouble(nodeDataSet.Tables[mainfeederList[i] - 1].Rows[0][9]))) + "m", drawFont1, sb, (60 + ((i - 1) * (float)nodespacing) + 60 + ((i) * (float)nodespacing)) / 2, (float)y - 20); //draws the label
                    }

                    drawArea.DrawString(label, drawFont, sb, 60 + (i * (float)nodespacing), (float)y - 20);//draws the label

                }

            }
            else //only display the last 6 nodes
            {
                double nodespacing = drawingSpace / 6;
                for (int i = 0; i < 6; i++)
                {
                    string label = "node 1";
                    if (nodeDataSet.Tables.Contains("node" + mainfeederList[mainfeederList.Count() - (6 - i)].ToString()))
                    {
                        label = nodeDataSet.Tables["node" + mainfeederList[mainfeederList.Count() - (6 - i)].ToString()].Rows[0][1].ToString(); //captures the label
                    }
                    System.Drawing.Font drawFont = new System.Drawing.Font("Arial", 10);
                    System.Drawing.Font drawFont1 = new System.Drawing.Font("Arial", 8);
                    System.Drawing.Font drawFont2 = new System.Drawing.Font("Arial", 6);

                    if (i == 0)
                    {
                        float[] dashValues = { 5, 2, 5, 2 };
                        Pen dashedPen = new Pen(Color.SteelBlue, 2);
                        dashedPen.DashPattern = dashValues;
                        drawArea.DrawEllipse(myPen, 0, y - 5, 20, 20);
                        drawArea.DrawEllipse(myPen, 10, y - 5, 20, 20);
                        drawArea.DrawLine(dashedPen, 30, y + 5, 60, y + 5);
                        //drawArea.DrawLine()
                        //drawArea.DrawString(Convert.ToInt16(Math.Round(Convert.ToDouble(nodeDataSet.Tables[0].Rows[0][9]))) + "m", drawFont2, sb, 33, (float)y - 10);

                    }
                    drawArea.FillEllipse(sb, 60 + (i * (int)nodespacing), y, 10, 10); //draws the circle
                    if (i != 6 - 1)
                    {
                        drawArea.DrawLine(myPen, 60 + (i * (float)nodespacing), y + 5, 60 + ((i + 1) * (float)nodespacing), y + 5); //draws the lines
                    }

                    if (nodeDataSet.Tables.Contains("node" + mainfeederList[mainfeederList.Count()-(6-i)].ToString())) 
                    {
                        if(i!=0) drawArea.DrawString(Convert.ToInt16(Math.Round(Convert.ToDouble(nodeDataSet.Tables[mainfeederList[mainfeederList.Count() - (6 - i)] - 1].Rows[0][9]))) + "m", drawFont1, sb, (60 + ((i - 1) * (float)nodespacing) + 60 + ((i) * (float)nodespacing)) / 2, (float)y - 20); //draws the label
                    }

                    drawArea.DrawString(label, drawFont, sb, 60 + (i * (float)nodespacing), (float)y - 20);//draws the label                    

                }


            }
            

        }
        //recursive method gets the nodes in the main feeder based on what the user specifies as the last node.
        private void getMainFeederNodes(int lastNode, List<int> mfNodeList)
        {

            if (mfNodeList.Count == 0)
            {
                mfNodeList.Add(lastNode); //adds the end node to the list
                // mfNodeDictionary.Add(lastNode, "node" + lastNode.ToString());
            }

            if (lastNode != 1)
            {

                mfNodeList.Add(Convert.ToInt16(nodeDataSet.Tables["node" + lastNode.ToString()].Rows[0][13])); //adds the parent node to the list
                // mfNodeDictionary.Add(Convert.ToInt16(nodeDataSet.Tables["node" + lastNode.ToString()].Rows[0][13]), "node" + lastNode.ToString());

                getMainFeederNodes(Convert.ToInt16(nodeDataSet.Tables["node" + lastNode.ToString()].Rows[0][13]), mfNodeList);
            }
            mfNodeList.Sort();

        }

        //recursive method that returns a datatable with summed up loads and dgs for nodes which are not on the main feeder.
        public void addtempNodeTable(int nodeNumber, List<int> mfNodeList)
        {
            if (nodeNumber != 0)
            {
                if ((string)tempTable.Rows[0][0] == "0")
                {
                    tempTable.Rows[0][0] = nodeNumber.ToString();
                    for (int rows = 0; rows < tempTable.Rows.Count; rows++)
                    {
                        for (int rows1 = 0; rows1 < nodeDataSet.Tables["node" + nodeNumber.ToString()].Rows.Count; rows1++)
                        {
                            if ((string)tempTable.Rows[rows][2] == (string)nodeDataSet.Tables["node" + nodeNumber.ToString()].Rows[rows1][2])
                            {
                                for (int cols = 0; cols < tempTable.Columns.Count; cols++)
                                {
                                    if ((cols != 3) && (cols != 4) && (cols != 5)) tempTable.Rows[rows][cols] = nodeDataSet.Tables["node" + nodeNumber.ToString()].Rows[rows1][cols];
                                }
                                tempTable.Rows[rows][3] = Convert.ToDouble(tempTable.Rows[rows][3]) + Convert.ToDouble(nodeDataSet.Tables["node" + nodeNumber.ToString()].Rows[rows1][3]);
                                tempTable.Rows[rows][4] = Convert.ToDouble(tempTable.Rows[rows][4]) + Convert.ToDouble(nodeDataSet.Tables["node" + nodeNumber.ToString()].Rows[rows1][4]);
                                tempTable.Rows[rows][5] = Convert.ToDouble(tempTable.Rows[rows][5]) + Convert.ToDouble(nodeDataSet.Tables["node" + nodeNumber.ToString()].Rows[rows1][5]);
                                break;
                            }
                        }
                    }
                }

                string childNodes = Convert.ToString(nodeDataSet.Tables["node" + nodeNumber.ToString()].Rows[0][14]);
                List<string> nodesToAddTemp = new List<string>();
                List<string> nodesToAdd = new List<string>();

                nodesToAddTemp = ConvertStringsToStringList(childNodes, ';');

                List<string> mfeederList = mfNodeList.ConvertAll<string>(delegate(int k) { return k.ToString(); }); //converts the mainfeeder nodelist which is an int list to a stringlist for later comparison
                nodesToAdd = nodesToAddTemp.Except(mfeederList).ToList();
                if (nodesToAdd.Contains("")) nodesToAdd.RemoveAll(s => s.Equals(""));
                for (int i = 0; i < nodesToAdd.Count; i++)
                {
                    addtempNodeTable(Convert.ToInt16(nodesToAdd[i]), mfNodeList);
                }

                if (nodesToAdd.Contains("0")) nodesToAdd.Remove("0"); //removes a zero from the nodes to add list.
                if (nodesToAdd.Contains(null)) nodesToAdd.Remove(null); //removes a zero from the nodes to add list.

                //has to go through each of the rows of the datatable of the child and adds the customers where the loadname and number match
                for (int i = 0; i < nodesToAdd.Count; i++)
                {
                    for (int rows = 0; rows < tempTable.Rows.Count; rows++)
                    {
                        for (int rows1 = 0; rows1 < nodeDataSet.Tables["node" + Convert.ToString(nodesToAdd[i])].Rows.Count; rows1++)
                        {
                            if ((string)tempTable.Rows[rows][2] == (string)nodeDataSet.Tables["node" + Convert.ToString(nodesToAdd[i])].Rows[rows1][2])
                            {
                                tempTable.Rows[rows][3] = Convert.ToDouble(tempTable.Rows[rows][3]) + Convert.ToDouble(nodeDataSet.Tables["node" + Convert.ToString(nodesToAdd[i])].Rows[rows1][3]);
                                tempTable.Rows[rows][4] = Convert.ToDouble(tempTable.Rows[rows][4]) + Convert.ToDouble(nodeDataSet.Tables["node" + Convert.ToString(nodesToAdd[i])].Rows[rows1][4]);
                                tempTable.Rows[rows][5] = Convert.ToDouble(tempTable.Rows[rows][5]) + Convert.ToDouble(nodeDataSet.Tables["node" + Convert.ToString(nodesToAdd[i])].Rows[rows1][5]);
                                break;
                            }
                        }
                    }
                }
            }
        }

        private void clearTempTable()
        {
            for (int i = 0; i < tempTable.Rows.Count; i++)
            {
                for (int j = 0; j < tempTable.Columns.Count; j++)
                {
                    if (j != 2) tempTable.Rows[i][j] = "0";
                }
            }
        }

        private DataTable addTableToMainFeeder(int nodeNumber, List<int> mfNodeList)
        {
            DataTable dt = new DataTable();
            addtempNodeTable(nodeNumber, mfNodeList);
            dt = tempTable.Copy();
            clearTempTable();
            return dt;
        }


        private void selectEndNodeCombo_SelectedIndexChanged(object sender, EventArgs e)
        {
            mfNodeList.Clear();//Clears the main feeder node list           
            drawArea.Clear(Color.White);
            getMainFeederNodes(Convert.ToInt32(selectEndNodeCombo.Text), mfNodeList);
            drawPoints(mfNodeList);
        }


        private void editTable()
        {
            for (int i = 0; i < nodeDataGridView.Columns.Count; i++)
            {
                nodeDataGridView.Columns[i].ReadOnly = false;
            }
        }

        private void closeTableEdits()
        {
            for (int i = 0; i < nodeDataGridView.Columns.Count; i++)
            {
                if ((i != 3) && (i != 4) && (i != 5))
                {
                    nodeDataGridView.Columns[i].ReadOnly = true;
                }
            }
        }

        //allows only digits to be entered in the customer section of the datagridview
        private void nodeDataGridView_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            e.Control.KeyPress -= new KeyPressEventHandler(Column1_KeyPress);
            if ((nodeDataGridView.CurrentCell.ColumnIndex > 2) || (nodeDataGridView.CurrentCell.ColumnIndex < 5))//Desired Columns
            {
                TextBox tb = e.Control as TextBox;
                if (tb != null)
                {
                    tb.KeyPress += new KeyPressEventHandler(Column1_KeyPress);
                }
            }
        }

        //method to check if the pressed value is a digit
        private void Column1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar)) e.Handled = true;
        }


        private void nodeNameTextBox_Validating(object sender, CancelEventArgs e)
        {
            if (nodeNameTextBox.Text == "")
            {
                e.Cancel = true;
                this.errorProvider1.SetError(nodeNameTextBox, "name cannot be null");
            }

            for (int i = 0; i < nodeDataSet.Tables.Count; i++)
            {
                if ((nodeDataSet.Tables[i].Rows[0][1].ToString() == nodeNameTextBox.Text) && ((string)nodeDataGridView.Rows[0].Cells[1].Value) != nodeNameTextBox.Text)
                {
                    e.Cancel = true;
                    nodeNameTextBox.Select(0, nodeNameTextBox.Text.Length);

                    this.errorProvider1.SetError(nodeNameTextBox, "name already exists please select another");

                }
            }
        }

        private void nodeNameTextBox_Validated(object sender, EventArgs e)
        {
            errorProvider1.SetError(nodeNameTextBox, "");
            nodeNameList[nodeNameList.IndexOf(nodeDataSet.Tables["node" + nodeNumCombo.Text].Rows[0][1].ToString())] = nodeNameTextBox.Text;

            if (nodeDataSet.Tables.Contains("node" + nodeNumCombo.Text))
            {
                for (int i = 0; i < nodeDataSet.Tables["node" + nodeNumCombo.Text].Rows.Count; i++)
                {
                    nodeDataSet.Tables["node" + nodeNumCombo.Text].Rows[i][1] = nodeNameTextBox.Text;
                }
            }

            drawArea.Clear(Color.White);
            drawPoints(mfNodeList);
        }

        private void nodeNameTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Space) e.Handled = true; ;
            //if (e.KeyChar == (char)Keys.Enter) lengthNumericUpDown.Focus();
        }

        private void lengthNumericUpDown_KeyPress(object sender, KeyPressEventArgs e)
        {
            //if (e.KeyChar == (char)Keys.Enter) addNodeButton.Focus();
        }

        private void nodeNameCombo_TextChanged(object sender, EventArgs e)
        {
            if (ShownForm) changeFormTitle();
            nodeNumCombo.Text = Convert.ToString(nodeNameList.IndexOf(nodeNameCombo.Text) + 1);
            int a = 6;
            int b = a + 1;
        }

        //recursive method that reutrns the autogenerated node name. It also caters for a scenario where the node already exists and will simply increse the number.
        private string autoGeneratedNodeName(string nodeName)
        {
            int num;

            if (nodeNameList.Contains(nodeName))
            {
                string _numString = nodeName.Remove(0, 5);
                num = Convert.ToInt16(_numString);
                num++;
                nodeName = "node " + num.ToString();
                autoGeneratedNodeName(nodeName);
            }
            return nodeName;
        }

        private void endNodeCombo_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ShownForm) changeFormTitle();
            selectEndNodeCombo.SelectedIndex = nodeNameList.IndexOf(endNodeCombo.Text);
        }

        //method calculates the length of the first row of the node datatable.
        public double calculateLengths(decimal lengthInput, int rowIndex)
        {
            double calculatedLength = lengthTol;
            if ((rowIndex == 0) && (loadCount != 0))
            {
                calculatedLength = Convert.ToDouble(lengthInput) - (lengthTol * (Convert.ToDouble(loadCount) - 1.0));
                return calculatedLength;
            }
            else if ((rowIndex == 0) && (genCount != 0))
            {
                calculatedLength = Convert.ToDouble(lengthInput) - (lengthTol * (Convert.ToDouble(genCount) - 1.0));
                return calculatedLength;
            }
            else
            {
                calculatedLength = lengthTol;
            }
            return calculatedLength;
        }

        public string calculateRp(double rkm, double calculatedLength)
        {
            return ((rkm / 1000.0) * calculatedLength).ToString();
        }

        public string calculateRn(double rKm, double caclulatedLength, double k1)
        {
            return (rKm / 1000.0 * k1 * caclulatedLength).ToString();
        }

        //method that returns the nodeVector datatables for all mainFeederNodes
        private DataTable[] createNodeVecDatatable(List<int> mfNodeList)
        {
            List<int> mfList = new List<int>(mfNodeList);
            k = mfNodeList.Count;

            DataTable[] nodeVecDataTables = new DataTable[k];
            for (int i = 0; i < k; i++) nodeVecDataTables[i] = new DataTable();


            for (int j = 0; j < k; j++)
            {
                //code to create a nodeVectorTable which will be used to calculate the voltage vector;
                //DataSet nodeDataSet = new DataSet();
                //nodeDataSet.ReadXml(projectName);
                DataTable[] dt = new DataTable[mfList.Count];

                //code to add the main feeder datatables into the main feeder dataset
                for (int i = 0; i < mfList.Count; i++)
                {
                    dt[i] = new DataTable();
                    dt[i] = addTableToMainFeeder(mfList[i], mfList);
                    dt[i].TableName = "node" + mfList[i].ToString();
                    if (tempNodeDataSet.Tables.Contains("node" + mfList[i].ToString())) tempNodeDataSet.Tables.Remove("node" + mfList[i].ToString());
                    tempNodeDataSet.Tables.Add(dt[i]);
                }


                //Create the column headers.
                List<string> headings = new List<string> { "Node", "load", "Red", "White", "Blue", "Alpha", "Beta", "Cb", "Length", "Cable", "Rp", "Rn" };

                //adds the column headers to the table
                for (int i = 0; i < headings.Count; i++) nodeVecDataTables[j].Columns.Add(headings[i]); //adds headings to the nodeVecDataTable;

                int nodeCountInt = 0;
                //adds rows to the table
                for (int i = 0; i < (loadCount + genCount) * mfList.Count; i++)
                {
                    if (i % (loadCount + genCount) == 0) nodeCountInt++;
                    nodeVecDataTables[j].Rows.Add(Convert.ToString(nodeCountInt), "load", 0.0, 0.0, 0.0, 999.0, 999.0, 0.0, 0.0, 0.0, 0.0, 0.0); //adds rows and sets all their values to zero except a and b
                }

                for (int i = 0; i < mfList.Count; i++)
                {
                    string loadDgTypeString = "blank";
                    int loadStartInt = (loadCount + genCount) * i; //variable which shows where the loads start in the table
                    int genStartInt = loadStartInt + loadCount;



                    for (int rows = 0; rows < tempNodeDataSet.Tables[i].Rows.Count; rows++)
                    {
                        //if (rows % 10 == 0) nodeVecDataTable.Rows[rows][7] = (double)lengthNumericUpDown.Value; //Assigns the first length to the first load.
                        loadDgTypeString = Convert.ToString(tempNodeDataSet.Tables[i].Rows[rows][2]); //gets the text in the 2nd column of the node datatable
                        if (loadDgTypeString[0] == 'L') loadStartInt++;
                        if (loadDgTypeString[0] == 'G') genStartInt++;
                        for (int cols = 1; cols < tempNodeDataSet.Tables[i].Columns.Count - 2; cols++)
                        {
                            if ((loadDgTypeString[0] == 'L'))
                            {

                                if ((cols > 2) && (cols != 10))
                                {
                                    nodeVecDataTables[j].Rows[loadStartInt - 1][0] = Convert.ToString(tempNodeDataSet.Tables[i].Rows[rows][1]);
                                    nodeVecDataTables[j].Rows[loadStartInt - 1][1] = Convert.ToString(tempNodeDataSet.Tables[i].Rows[rows][2]);
                                    nodeVecDataTables[j].Rows[loadStartInt - 1][cols - 1] = Convert.ToDouble(tempNodeDataSet.Tables[i].Rows[rows][cols]);
                                }
                                if (cols == 10)
                                {
                                    nodeVecDataTables[j].Rows[loadStartInt - 1][cols - 1] = tempNodeDataSet.Tables[i].Rows[rows][cols].ToString();
                                }

                            }

                            if (loadDgTypeString[0] == 'G')
                            {

                                if ((cols > 2) && (cols != 10))
                                {
                                    nodeVecDataTables[j].Rows[genStartInt - 1][0] = Convert.ToString(tempNodeDataSet.Tables[i].Rows[rows][1]);
                                    nodeVecDataTables[j].Rows[genStartInt - 1][1] = Convert.ToString(tempNodeDataSet.Tables[i].Rows[rows][2]);
                                    nodeVecDataTables[j].Rows[genStartInt - 1][cols - 1] = Convert.ToDouble(tempNodeDataSet.Tables[i].Rows[rows][cols]);
                                }
                                if (cols == 10)
                                {
                                    nodeVecDataTables[j].Rows[genStartInt - 1][cols - 1] = tempNodeDataSet.Tables[i].Rows[rows][cols].ToString();
                                }
                            }
                        }
                    }
                }

                mfList.RemoveAt(k - 1 - j);
                tempNodeDataSet.Tables.Clear();
            }
            return nodeVecDataTables;
        }

        private void detailsCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (detailsCheckBox.Checked == true)
            {
                //nodeDataGridView.Columns[0].Visible = true;
                //nodeDataGridView.Columns[9].Visible = !false;
                nodeDataGridView.Columns[10].Visible = !false;
                //nodeDataGridView.Columns[11].Visible = !false;
                //nodeDataGridView.Columns[12].Visible = !false;
                nodeDataGridView.Columns[13].Visible = !false;
                nodeDataGridView.Columns[14].Visible = !false;
                nodeDataGridView.Columns[11].DefaultCellStyle.Format = "N3";
                nodeDataGridView.Columns[12].DefaultCellStyle.Format = "N3";

            }
            else
            {
                nodeDataGridView.Columns[9].Visible = false;
                nodeDataGridView.Columns[10].Visible = false;
                nodeDataGridView.Columns[11].Visible = false;
                nodeDataGridView.Columns[12].Visible = false;
                nodeDataGridView.Columns[13].Visible = false;
                nodeDataGridView.Columns[14].Visible = false;
            }
        }

        private void searchTextBox_KeyUp(object sender, KeyEventArgs e)
        {
            string searchString = searchTextBox.Text;
            //filter based on the substring typed in the search textbox
            string filter = "[Load/DG] LIKE '*" + searchTextBox.Text + "*'";
            DataView myDataView = new DataView(nodeDataSet.Tables["node" + nodeNumCombo.Text], filter, string.Empty, DataViewRowState.CurrentRows);
            nodeDataGridView.DataSource = myDataView;
        }

        private void nodeDataGridView_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            //nodeDataSet.AcceptChanges();
            //DataTable dt = new DataTable();
            //dt = (DataTable)nodeDataGridView.DataSource;

            //string loadname = dt.Rows[e.RowIndex][2].ToString();
            //loadname = "Load/DG like '"+loadname+"'";
            //DataRow [] drArray;
            //drArray = nodeDataSet.Tables["node"+nodeNumCombo.Text].Select(loadname);
            //int? rowInDt = new System.Data.DataView(dt).ToTable(false, new[] { "1" })
            //    .AsEnumerable()
            //    .Select(row => row.Field<string>("1")) // ie. project the col(s) needed
            //    .ToList()
            //    .FindIndex(col => col == "G"); // returns 2

            ////if (dt.Rows[e.RowIndex][e.ColumnIndex].ToString() != nodeVecDataSet.Tables[0].Rows[e.RowIndex][e.ColumnIndex].ToString())
        }

        private void passiveRadio_CheckedChanged(object sender, EventArgs e)
        {
            if (passiveRadio.Checked == true)
            {
                p = 10.0;
            }
            else p = 90.0;
            if (ShownForm) changeFormTitle();
        }

        private void activeRadio_CheckedChanged(object sender, EventArgs e)
        {
            if (activeRadio.Checked == true) p = 90.0;
            else p = 10.0;
            if (ShownForm) changeFormTitle();
        }

        //private void sourceVoltageTextBox_Validating(object sender, CancelEventArgs e)
        //{
        //    if (sourceVoltageTextBox.Text == "")
        //    {
        //        e.Cancel = true;
        //        this.errorProvider1.SetError(sourceVoltageTextBox, "voltage cannot be null");
        //    }
        //}

        //private void sourceVoltageTextBox_Validated(object sender, EventArgs e)
        //{
        //    errorProvider1.SetError(sourceVoltageTextBox, "");
        //    Vs = Convert.ToDouble(sourceVoltageTextBox.Text);

        //}

        //private void sourceVoltageTextBox_KeyPress(object sender, KeyPressEventArgs e)
        //{
        //    if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
        //    {
        //        e.Handled = true;
        //    }
        //}

        private void nodeFeederForm_Load(object sender, EventArgs e)
        {
            // Create the ToolTip and associate with the Form container.
            ToolTip toolTip1 = new ToolTip();

            // Set up the delays for the ToolTip.
            toolTip1.AutoPopDelay = 5000;
            toolTip1.InitialDelay = 1000;
            toolTip1.ReshowDelay = 500;
            // Force the ToolTip text to be displayed whether or not the form is active.
            toolTip1.ShowAlways = true;

            // Set up the ToolTip text for the Button and Checkbox.
            toolTip1.SetToolTip(this.addNodeButton, "Clicking this button will add a node to the one that's currently shown.");
            toolTip1.SetToolTip(this.deleteNodeButton, "Clicking this button will remove the currently shown node and all its child nodes.");

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void label9_Click(object sender, EventArgs e)
        {

        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (projectSavedName != "")
            {
                projectDataSet.Tables.Clear();
                this.Text = Path.GetFileNameWithoutExtension(projectSavedName);
                string fileName = Path.ChangeExtension(projectSavedName, ".xml");
                
                foreach (DataTable dt in libraryDataSet.Tables)
                {
                    projectDataSet.Tables.Add(dt.Copy());
                }


                projectDataSet.Tables.Add(paramDataTable);
                nodeDataSet.AcceptChanges();
                foreach (DataTable dt in nodeDataSet.Tables)
                {
                    projectDataSet.Tables.Add(dt.Copy());
                }

                projectDataSet.Tables["Parameter Table"].Rows[0]["Current Node"] = "node" + nodeNumCombo.Text;
                if (activeRadio.Checked)
                {
                    projectDataSet.Tables["Parameter Table"].Rows[0]["active"] = true;
                    projectDataSet.Tables["Parameter Table"].Rows[0]["passive"] = false;
                }
                else
                {
                    projectDataSet.Tables["Parameter Table"].Rows[0]["active"] = false;
                    projectDataSet.Tables["Parameter Table"].Rows[0]["passive"] = true;
                }
                projectDataSet.Tables["Parameter Table"].Rows[0]["temp"] = Convert.ToDouble(operatingTempNumUpDown.Value);
                projectDataSet.Tables["Parameter Table"].Rows[0]["sourceVoltage"] = Convert.ToDouble(sourceVoltageNumUpDown.Value);

                projectDataSet.WriteXml(fileName, XmlWriteMode.WriteSchema);

                if (File.Exists(projectSavedName))
                {
                    System.IO.File.Delete(projectSavedName);
                }
                System.IO.File.Move(fileName, projectSavedName);
            }
            else
            {
                saveAsToolStripMenuItem_Click(sender, e);
            }
        }

        private void loadsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int loadCountOld = loadCount;
            int genCountOld = genCount;
            DataSet originalLibraryDataSet = new DataSet();
            
            foreach (DataTable dt in libraryDataSet.Tables)
            {
                originalLibraryDataSet.Tables.Add(dt.Copy());
            }
            libraryForm frm = new libraryForm("Loads", true, libraryDataSet);
            frm.saveToProject.Enabled = false;            
            frm.ShowDialog();

            if (frm.cancel)
            {
                libraryDataSet.Tables.Clear();
                foreach (DataTable dt in originalLibraryDataSet.Tables)
                {
                    libraryDataSet.Tables.Add(dt.Copy());
                }
            }
            else
            {
                //change the formtitle
                changeFormTitle();
            }

            
        
            //Code picks up any changes to the loads database and loads them
            DataTable resultLoads = new DataTable();
            nodeDataSet.AcceptChanges();

            var table = libraryDataSet.Tables["Loads"].Select("Selected = true");
            if (table.AsEnumerable().Any())
            {
                resultLoads = table.CopyToDataTable();
                loadCount = resultLoads.Rows.Count;
            }

            DataTable resultGens = new DataTable();
            var table1 = libraryDataSet.Tables["Generators"].Select("Selected = true");
            if (table1.AsEnumerable().Any())
            {
                resultGens = table1.CopyToDataTable();
                genCount = resultGens.Rows.Count;
            }


            //adds the selected Gens to the selected loads table
            foreach (DataRow dr in resultGens.Rows)
            {
                if (table.AsEnumerable().Any()) resultLoads.Rows.Add(dr.ItemArray);
                else
                {
                    resultLoads = resultGens.Copy();

                    break;
                }
            }
            resultLoadsGens = resultLoads.Copy();
            DataSet nodeDataHolder = new DataSet(); //dataset that keeps the current datatables.
            foreach (DataTable dt in nodeDataSet.Tables)
            {
                nodeDataHolder.Tables.Add(dt.Copy());
            }

            nodeDataSet.Tables.Clear();

            for (int i = 0; i < nodeCountInt; i++)
            {
                double lengthSum = 0;
                decimal lengthSumDecimal = 0.0M;
                double rT2L = 0;
                double k1L = 0;
                for (int x = 0; x < nodeDataHolder.Tables[i].Rows.Count; x++)
                {
                    lengthSum = lengthSum + Convert.ToDouble(nodeDataHolder.Tables[i].Rows[x]["Length"]);

                }
                
                lengthSumDecimal = Convert.ToDecimal(lengthSum - (double)genCountOld * lengthTol);
                
                string tableName = "node" + (i + 1).ToString();
                DataTable nodeDataTable = new DataTable(tableName);
                List<string> headings = new List<string> { "Node", "Name", "Load/DG", "Red", "White", "Blue", "Alpha", "Beta", "Cb", "Length", "Cable", "Rp", "Rn", "Parent", "Children" };
                for (int j = 0; j < headings.Count; j++)
                {
                    nodeDataTable.Columns.Add(headings[j]); //adds the headings to the nodeDataTable                 
                }
                //gets the correct cable parameters and calculates them for that particular table
                for (int y = 0; y < libraryDataSet.Tables["Conductors"].Rows.Count; y++)
                {
                    if (Convert.ToString(libraryDataSet.Tables["Conductors"].Rows[y][0]) == Convert.ToString(nodeDataHolder.Tables[i].Rows[0]["Cable"]))
                    {
                        rT2L = Convert.ToDouble(libraryDataSet.Tables["Conductors"].Rows[y][1]) * (Convert.ToDouble(libraryDataSet.Tables["Conductors"].Rows[y][2]) + t2) / (Convert.ToDouble(libraryDataSet.Tables["Conductors"].Rows[y][2]) + 20.0);
                        k1L = Convert.ToDouble(libraryDataSet.Tables["Conductors"].Rows[y][3]);
                        break;
                    }
                }

                //querires the datatables for the loads and gens where the user has selected. 
                       

                for (int k = 0; k < resultLoads.Rows.Count; k++)
                {                                                  
                    nodeDataTable.Rows.Add(nodeDataHolder.Tables[i].Rows[0]["Node"], nodeDataHolder.Tables[i].Rows[0]["Name"], resultLoads.Rows[k][0], 0.0, 0.0, 0.0, resultLoads.Rows[k]["alpha"], resultLoads.Rows[k]["beta"], resultLoads.Rows[k]["circuit breaker"], calculateLengths(lengthSumDecimal, k), nodeDataHolder.Tables[i].Rows[0]["Cable"], calculateRp(rT2L, calculateLengths(lengthSumDecimal, k)), calculateRn(rT2L, calculateLengths(lengthSumDecimal, k), k1L), ((k == 0) ? nodeDataHolder.Tables[i].Rows[0]["Parent"] : ""), ((k == 0) ? nodeDataHolder.Tables[i].Rows[0]["Children"] : ""));
                }

                //add the datatable into the nodeDataSet
                nodeDataSet.Tables.Add(nodeDataTable);

                //Populates the new nodeDataTable with the RWB customer numbers                        
                               
            }
            tempTable = nodeDataSet.Tables[0].Copy();
            tempTable.Rows[0][0] = "0";
            foreach (DataRow dr in tempTable.Rows)
            {
                dr[3] = dr[4] = dr[5] = 0.0;
            }

            for (int i = 0; i < nodeCountInt; i++)
            {
                for (int z = 0; z < nodeDataHolder.Tables[i].Rows.Count; z++)
                {
                    for (int a = 0; a < nodeDataSet.Tables[i].Rows.Count; a++)
                    {
                        if (Convert.ToString(nodeDataHolder.Tables[i].Rows[z]["Name"]) == Convert.ToString(nodeDataSet.Tables[i].Rows[a]["Name"]) && Convert.ToString(nodeDataHolder.Tables[i].Rows[z]["Load/DG"]) == Convert.ToString(nodeDataSet.Tables[i].Rows[a]["Load/DG"]))
                        {
                            nodeDataSet.Tables[i].Rows[a]["Red"] = nodeDataHolder.Tables[i].Rows[z]["Red"];
                            nodeDataSet.Tables[i].Rows[a]["White"] = nodeDataHolder.Tables[i].Rows[z]["White"];
                            nodeDataSet.Tables[i].Rows[a]["Blue"] = nodeDataHolder.Tables[i].Rows[z]["Blue"];                            
                        }
                    }

                }
            }
            

            nodeDataHolder.Tables.Clear();
            nodeDataGridView.DataSource = nodeDataSet.Tables["node" + nodeNumCombo.Text];            
        }

        private void generatorsToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void nodeDataGridView_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if ((e.ColumnIndex == 11 || e.ColumnIndex == 12) && e.RowIndex != this.nodeDataGridView.NewRowIndex)
            {
                double d = double.Parse(e.Value.ToString());
                e.Value = d.ToString("N2");
            }

            if ((e.ColumnIndex >= 2) && (e.ColumnIndex <= 8))
            {
                var cell = nodeDataGridView.Rows[e.RowIndex].Cells[e.ColumnIndex];
                // Set the Cell's ToolTipText.  In this case we're retrieving the value stored in 
                // another cell in the same row (see my note below).
                if (resultLoadsGens.Rows.Count > 0)
                {
                    cell.ToolTipText = resultLoadsGens.Rows[e.RowIndex]["description"].ToString();
                }
            }

        }

        private void operatingTemperatureText_TextChanged(object sender, EventArgs e)
        {

        }

        private void operatingTempNumUpDown_ValueChanged(object sender, EventArgs e)
        {
            if (ShownForm) changeFormTitle();
            double t_new = Convert.ToDouble(operatingTempNumUpDown.Value);
            DataSet ds = new DataSet();
            if (!projectDataSet.Tables.Contains("Conductors"))
            {
                ds.ReadXml(fileNameAppData);
            }
            else
            {
                ds.Merge(projectDataSet.Tables["Conductors"]);
            }




            if (nodeDataSet.Tables.Count != 0)
            {
                for (int i = 0; i < nodeCountInt; i++)
                {
                    for (int rows = 0; rows < nodeDataSet.Tables[i].Rows.Count; rows++)
                    {
                        string cable = nodeDataSet.Tables[i].Rows[rows][10].ToString();
                        DataRow dr = ds.Tables["Conductors"].AsEnumerable().SingleOrDefault(r => r.Field<string>("Code") == cable);
                        double T = Convert.ToDouble(dr["T"]);

                        nodeDataSet.Tables[i].Rows[rows][11] = Convert.ToDouble(nodeDataSet.Tables[i].Rows[rows][11]) * (T + t_new) / (T + t2);
                        nodeDataSet.Tables[i].Rows[rows][12] = Convert.ToDouble(nodeDataSet.Tables[i].Rows[rows][12]) * (T + t_new) / (T + t2);
                    }
                }
            }


            t2 = t_new;
        }

        private void sourceVoltageNumUpDown_ValueChanged(object sender, EventArgs e)
        {
            Vs = Convert.ToDouble(sourceVoltageNumUpDown.Value);
            if (ShownForm) changeFormTitle();
            
        }

        private void nodeFeederForm_Shown(object sender, EventArgs e)
        {
            drawArea.Clear(Color.White);
            this.ShownForm = true;
           
            drawPoints(mfNodeList);
        }

        private void nodeDataGridView_CellLeave(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void nodeDataGridView_CellValidated(object sender, DataGridViewCellEventArgs e)
        {
           
        }

        private void saveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {

            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "HBA file(*.hba)|*.hba";
            if (sfd.ShowDialog() == DialogResult.OK)
            {
                projectDataSet.Tables.Clear();
                string path = sfd.FileName;

                this.Text = Path.GetFileNameWithoutExtension(path);
                string fileName = Path.ChangeExtension(sfd.FileName, ".xml");
                projectSavedName = sfd.FileName;
                foreach (DataTable dt in libraryDataSet.Tables)
                {
                    projectDataSet.Tables.Add(dt.Copy());
                }


                projectDataSet.Tables.Add(paramDataTable);
                nodeDataSet.AcceptChanges();
                foreach (DataTable dt in nodeDataSet.Tables)
                {
                    projectDataSet.Tables.Add(dt.Copy());
                }

                projectDataSet.Tables["Parameter Table"].Rows[0]["Current Node"] = "node" + nodeNumCombo.Text;
                if (activeRadio.Checked)
                {
                    projectDataSet.Tables["Parameter Table"].Rows[0]["active"] = true;
                    projectDataSet.Tables["Parameter Table"].Rows[0]["passive"] = false;
                }
                else
                {
                    projectDataSet.Tables["Parameter Table"].Rows[0]["active"] = false;
                    projectDataSet.Tables["Parameter Table"].Rows[0]["passive"] = true;
                }
                projectDataSet.Tables["Parameter Table"].Rows[0]["temp"] = Convert.ToDouble(operatingTempNumUpDown.Value);
                projectDataSet.Tables["Parameter Table"].Rows[0]["sourceVoltage"] = Convert.ToDouble(sourceVoltageNumUpDown.Value);

                projectDataSet.WriteXml(fileName, XmlWriteMode.WriteSchema);

                if (File.Exists(path))
                {
                    System.IO.File.Delete(path);
                }
                System.IO.File.Move(fileName, path);
            }

        }

        private void nodeFeederForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (this.Text[this.Text.Length - 1] == '*')
            {

                const string message = "Are you sure that you would like to close the project with unsaved changes?";
                const string caption = "HBAlgorithm";
                var result = MessageBox.Show(message, caption,
                                             MessageBoxButtons.YesNoCancel,
                                             MessageBoxIcon.Exclamation);

                // If the no button was pressed ... 
                if (result == DialogResult.No)
                {
                    // prompt to save and cancel the closure of the form.
                    saveToolStripMenuItem_Click(sender, e);
                    e.Cancel = true;
                    
                }

                else if (result == DialogResult.Cancel)
                {
                    e.Cancel = true;
                }

                else { }
                
            }
            
        }

        private void addHandlers()
        {
            foreach (TextBox control in Controls.OfType<TextBox>())
            {
                control.TextChanged += new EventHandler(OnContentChanged);
            }
            foreach (ComboBox control in Controls.OfType<ComboBox>())
            {
                control.SelectedIndexChanged += new EventHandler(OnContentChanged);
            }
            foreach (CheckBox control in Controls.OfType<CheckBox>())
            {
                control.CheckedChanged += new EventHandler(OnContentChanged);
            }
        }

        protected void OnContentChanged(object sender, EventArgs e)
        {
            if (ContentChanged != null)
                ContentChanged(this, new EventArgs());
            
            
        }

        private void nodeNameText_OnContentChanged(object sender, EventArgs e)
        {
            
        }

        public event EventHandler ContentChanged;


        private void nodeDataGridView_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            changeFormTitle();
        }

        private void changeFormTitle()
        {
            string b = this.Text;
            if (b[b.Length - 1] == '*')
            {

            }
            else
            {
                this.Text = this.Text + "*";
            }

        }

        private void sourceVoltageNumUpDown_MouseClick(object sender, MouseEventArgs e)
        {

        }

        private void sourceVoltageNumUpDown_Validated(object sender, EventArgs e)
        {

        }

        private void sourceVoltageNumUpDown_Enter(object sender, EventArgs e)
        {

        }

        private void nominalVoltageUpDown_ValueChanged(object sender, EventArgs e)
        {
            if (ShownForm) changeFormTitle();
        }

        private void nodeNameTextBox_TextChanged(object sender, EventArgs e)
        {
            if (ShownForm) changeFormTitle();
        }

        private void nodeNameCombo_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void cableSelectCombo_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void tableLayoutPanel6_Paint(object sender, PaintEventArgs e)
        {

        }

        private void drawingPanel_Paint(object sender, PaintEventArgs e)
        {

        }

        private void nodeDataGridView_Validating(object sender, CancelEventArgs e)
        {

        }

        private void nodeDataGridView_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            
                if (String.IsNullOrEmpty(e.FormattedValue.ToString()))
                {
                    nodeDataGridView.Rows[e.RowIndex].ErrorText =
                        "Cells cannnot be empty";
                    e.Cancel = true;
                }
            
        }

        public void setSourceVoltageNumUpDown(double number)
        {
            sourceVoltageNumUpDown.Value = Convert.ToDecimal(number);
        }

        public void setOperatingTempNumUpDown(double number)
        {
            operatingTempNumUpDown.Value = Convert.ToDecimal(number);
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "HBA file(*.hba)|*.hba";

            if (ofd.ShowDialog() == DialogResult.OK)
            {
                
                
                string fileName = Path.ChangeExtension(ofd.FileName, ".xml");                
                //this.Hide();
                nodeFeederForm nff = new nodeFeederForm(fileName);
                nff.ShowDialog();
            }

        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AboutBox1 abBox = new AboutBox1();
            abBox.ShowDialog();
        }
    }
}
