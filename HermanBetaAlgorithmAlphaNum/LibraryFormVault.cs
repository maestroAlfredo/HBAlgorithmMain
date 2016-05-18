using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Reflection;
using System.IO;
using System.Xml;
using System.Runtime.Serialization;
using VoltageDropCalculatorApplication;

namespace HermanBetaAlgorithmAlphaNum
{
    public partial class LibraryFormVault : Form
    {

        private LibrarySet _librarySet;
        private Library _currentLibrary;
        private DataTable _componentTable;
        private BindingSource _bindingSource;

        public LibraryFormVault(LibrarySet libSet)
        {
            InitializeComponent();
            _librarySet = libSet;
            _currentLibrary = _librarySet.ConductorLibrary;
            _bindingSource = new BindingSource();                      
            InitializeCombos();
            SetDataGridView();
            

        }

        private void InitializeCombos()
        {
            libraryTypeComboBox.DataSource = Enum.GetNames(typeof(LibraryType));
            InitializeVaultCombo();
        }

        private void InitializeVaultCombo()
        {
            vaultComboBox.DataSource = _currentLibrary.ListOfVaults;
            vaultComboBox.DisplayMember = "VaultName";
        }


        private void SetDataGridView()
        {
            //Gets the first IVaultComponent in the Library vault list to be used to set up the datatable columns
            VaultComponent component = _currentLibrary.ListOfVaults[0].ComponentList[0];
            PropertyInfo[] propertyInfo = component.GetType().GetProperties();

            //add the corresponding IVaultComponent properties as columns in the datatable
            _componentTable = new DataTable();
            
            foreach (PropertyInfo property in propertyInfo)
            {
                _componentTable.Columns.Add(property.Name, property.PropertyType);
            }

            //make the "VaultName" Column readonly**** 
            _componentTable.Columns[AppConstants.VaultColumnName].ReadOnly = true;

            //makes the first column a primary key of the component table. 
            DataColumn[] key = new DataColumn[1];
            key[0] = _componentTable.Columns["Name"];
            _componentTable.PrimaryKey = key;


            //Populate the datatable with the values in the vault corresponding to displayed text in the vaultComboBox
            Vault vault = (Vault)vaultComboBox.SelectedItem;

            foreach (VaultComponent vaultComponent in vault.ComponentList)
            {
                DataRow row = _componentTable.NewRow();
                foreach (DataColumn column in _componentTable.Columns)
                {
                    row[column] = vaultComponent.GetType().GetProperty(column.ColumnName).GetValue(vaultComponent);
                }
                _componentTable.Rows.Add(row);
            }

            //eventhandler for when the user adds a new row
            _componentTable.TableNewRow += _componentTable_TableNewRow;
            _bindingSource.DataSource = _componentTable;

            componentDataGridView.DataSource = _bindingSource;
        }

        public void RefreshDataGridView()
        {
            SetDataGridView();
        }

        private void _componentTable_TableNewRow(object sender, DataTableNewRowEventArgs e)
        {
            e.Row[AppConstants.VaultColumnName] = vaultComboBox.Text;
        }

        private void loadToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form componentDets = new ComponentDets(_librarySet, typeof(Load));
            //Form componentDets = new ComponentDets();
            componentDets.Show();     
        }

        private void Add_Click(object sender, EventArgs e)
        {

        }

        private void vaultComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {

            SetDataGridView();

        }

        private void libraryTypeComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch ((LibraryType)Enum.Parse(typeof(LibraryType), (string)libraryTypeComboBox.SelectedValue))
            {
                case LibraryType.Conductor:
                    _currentLibrary = _librarySet.ConductorLibrary;
                    break;
                case LibraryType.Generator:
                    _currentLibrary = _librarySet.GeneratorLibrary;
                    break;
                case LibraryType.Load:
                    _currentLibrary = _librarySet.LoadLibrary;
                    break;
            }
            InitializeVaultCombo();
            SetDataGridView();
        }

        private void okButton_Click(object sender, EventArgs e)
        {
            //if the user is Ok with the new changes, add the components to the list of vault components in the vault
            Vault vaultToAddTo = (Vault)vaultComboBox.SelectedItem;     
            vaultToAddTo.ComponentList.Clear();
            foreach (DataRow componentRow in _componentTable.Rows)
            {

                switch (_currentLibrary.LibraryType)
                {
                    case LibraryType.Conductor:
                        new Conductor(componentRow, vaultToAddTo);
                        break;
                    case LibraryType.Generator:
                        new Load(componentRow, LoadType.Generator, vaultToAddTo);
                        break;
                    case LibraryType.Load:
                        new Load(componentRow, LoadType.Load, vaultToAddTo);
                        break;
                    default:
                        new Load(componentRow, LoadType.Load, vaultToAddTo);
                        break;
                }
            }

            FileHandlers.WriteObject(FileHandlers.GenerateFile().FullName, _librarySet);
        }

        private void generatorToolStripMenuItem_Click(object sender, EventArgs e)
        {
          
        }

        private void conductorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form componentDets = new ComponentDets(_librarySet, typeof(Conductor));
            componentDets.Show();
        }

        private void componentToolStripMenuItem1_Click(object sender, EventArgs e)
        {

        }

        private void exportToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void tableLayoutPanelIcon_Paint(object sender, PaintEventArgs e)
        {

        }

        private void pictureBoxNewProj_Click(object sender, EventArgs e)
        {

        }

        private void pictureBoxVault_Click(object sender, EventArgs e)
        {

        }

        private void pictureBoxNewProj_MouseHover(object sender, EventArgs e)
        {
            //ToolTip tt = new ToolTip();
            //tt.SetToolTip(this.pictureBox1, "n p");
            new ToolTip().SetToolTip(this.pictureBoxNewProj, "Create New Project");
        }

        private void pictureBoxLoadProj_Click(object sender, EventArgs e)
        {
            
        }

        private void pictureBoxLoadProj_MouseHover(object sender, EventArgs e)
        {
            new ToolTip().SetToolTip(this.pictureBoxLoadProj, "Load Previous Project");
        }

        private void pictureBoxVault_MouseHover(object sender, EventArgs e)
        {
            new ToolTip().SetToolTip(this.pictureBoxVault, "Create New Vault");
        }

        private void pictureBoxImportVault_MouseHover(object sender, EventArgs e)
        {
            new ToolTip().SetToolTip(this.pictureBoxImportVault, "Import exist vault");
        }

        private void pictureBoxConductor_MouseHover(object sender, EventArgs e)
        {
            new ToolTip().SetToolTip(this.pictureBoxConductor, "Create a new conductor");
        }

        private void pictureBox1_MouseHover(object sender, EventArgs e)
        {
            new ToolTip().SetToolTip(this.pictureBoxLoad, "Create new Load");
        }

        private void pictureBoxGen_MouseHover(object sender, EventArgs e)
        {
            new ToolTip().SetToolTip(this.pictureBoxGen, "Create new DG");
        }

        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void toolStripContainer1_Click(object sender, EventArgs e)
        {

        }

        private void libraryLabel_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void tableLayoutPanel3_Paint(object sender, PaintEventArgs e)
        {

        }

        private void tableLayoutPanel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void tableLayoutPanel4_Paint(object sender, PaintEventArgs e)
        {

        }

        private void removeComponentButton_Click(object sender, EventArgs e)
        {

        }

        private void componentDataGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void cancelButton_Click(object sender, EventArgs e)
        {

        }

        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void fileToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void vaultToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void componentToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void importToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void vaultToolStripMenuItem1_Click(object sender, EventArgs e)
        {

        }

        private void vaultToolStripMenuItem2_Click(object sender, EventArgs e)
        {

        }

        private void componentToolStripMenuItem2_Click(object sender, EventArgs e)
        {

        }

        private void pictureBoxConductor_Click(object sender, EventArgs e)
        {

        }

        private void pictureBoxImportVault_Click(object sender, EventArgs e)
        {

        }

        private void pictureBoxLoad_Click(object sender, EventArgs e)
        {

        }

        private void pictureBoxGen_Click(object sender, EventArgs e)
        {

        }     
    }
}
