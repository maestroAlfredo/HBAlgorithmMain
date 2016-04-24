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

namespace VoltageDropCalculatorApplication
{
    public partial class LibraryFormVault : Form
    {

        public Library m_Library { get; set; }
        public List<Library> LibraryList { get; set; }
        private List<string> _vaultNames;
        private DataTable _componentTable;

        public LibraryFormVault()
        {
            InitializeComponent();
            InitializeLibraries();
            SetVaultNames();
            SetDataGridView();

        }

        private void SetVaultNames()
        {
            vaultComboBox.DataSource = m_Library.ListOfVaults.Select(vault => vault.VaultName).ToList();
        }

        private void SetDataGridView()
        {
            //Gets the first IVaultComponent in the Library vault list to be used to set up the datatable columns
            VaultComponent component = m_Library.ListOfVaults[0].ComponentList[0];
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
            key[0] = _componentTable.Columns[0];
            _componentTable.PrimaryKey = key;


            //Populate the datatable with the values in the vault corresponding to displayed text in the vaultComboBox
            Vault vault = m_Library.ListOfVaults.Find(vaultItem => vaultItem.VaultName.Equals(vaultComboBox.Text));

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

            componentDataGridView.DataSource = _componentTable.AsDataView();
        }

        private void _componentTable_TableNewRow(object sender, DataTableNewRowEventArgs e)
        {
            e.Row[AppConstants.VaultColumnName] = vaultComboBox.Text;
        }

        private void InitializeVaultNames()
        {
            _vaultNames = new List<string>();
            foreach (Vault vault in m_Library.ListOfVaults)
            {
                _vaultNames.Add(vault.VaultName);
            }

            //gets the list of vault names to use for the vaultCombobox
            vaultComboBox.DataSource = m_Library.ListOfVaults.Select(vaultName => vaultName.VaultName).ToList();

        }

        private void InitializeLibraries()
        {

            //creates new libraries list
            LibraryList = new List<Library>();

            //creates a conductor library and adds a list of vaults to it
            var conductorLibrary = new ConductorLibrary(AppConstants.ConductorLibraryName);
            conductorLibrary.Add(DefaultVaults.GetConductorVaults());
            LibraryList.Add(conductorLibrary);

            //creates a load library and adds a list of load vaults to it
            var loadLibrary = new LoadLibrary(AppConstants.LoadLibraryName);
            loadLibrary.Add(DefaultVaults.GetLoadVaults());
            LibraryList.Add(loadLibrary);

            //creates a generator library and adds a list of generator vaults to it
            var generatorLibrary = new GeneratorLibary(AppConstants.GeneratorLibraryName);
            generatorLibrary.Add(DefaultVaults.GetGeneratorVaults());
            LibraryList.Add(generatorLibrary);

            m_Library = LibraryList[0]; //gets the first library in the library list to use as the m_Library;            

            //adds the librarynames to the conductors combobox as a datasource
            libraryTypeComboBox.DataSource = LibraryList.Select(item => item.LibraryName).ToList();
        }

        private void loadToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form componentDets = new ComponentDets(new Load(), m_Library);
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
            m_Library = LibraryList.Find(item => item.LibraryName.Equals(libraryTypeComboBox.Text));
            SetVaultNames();
            SetDataGridView();
        }

        private void okButton_Click(object sender, EventArgs e)
        {
            //if the user is Ok with the new changes, add the components to the list of vault components in the vault
            Vault vaultToAddTo = m_Library.ListOfVaults.Find(vaultItem => vaultItem.VaultName.Equals(vaultComboBox.Text)); //find the vault that matches the vault name in the library       
            vaultToAddTo.ComponentList.Clear();
            foreach (DataRow componentRow in _componentTable.Rows)
            {

                switch (m_Library.LibraryType)
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



            var writer = new System.Xml.Serialization.XmlSerializer(typeof(Library), new Type[] { typeof(ConductorLibrary), typeof(LoadLibrary), typeof(GeneratorLibary), typeof(ConductorVault), typeof(LoadVault), typeof(GeneratorVault), typeof(Conductor), typeof(Load)});

            System.IO.FileInfo file = new System.IO.FileInfo(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "HBAlgorithmLibraries", "libraries.xml"));
            file.Directory.Create(); // If the directory already exists, this method does nothing.          

            using (FileStream filestream = File.Create(file.FullName))
            {
                writer.Serialize(filestream, m_Library);
            }


        }

        private void generatorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form componentDets = new ComponentDets(new Load(), m_Library);
            componentDets.Show();
        }

        private void conductorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form componentDets = new ComponentDets(new Conductor(), m_Library);
            componentDets.Show();
        }
    }
}
