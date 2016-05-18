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

namespace VoltageDropCalculatorApplication
{
    public partial class LibraryFormVault : Form
    {

        private static Library m_Library;
        public List<Library> LibraryList { get; set; }
        private List<string> _vaultNames;
        private DataTable _componentTable;
        private FileInfo _file;

        public static Library CurrentLibrary
        {
            get
            {
                return m_Library;
            }
            set
            {
                m_Library = value;
            }
        }

        public LibraryFormVault()
        {
            _file = GenerateFile();                    
            InitializeComponent();
            InitializeLibraries();
            SetVaultNames();
            SetDataGridView();
            

        }

        private void SetVaultNames()
        {
            vaultComboBox.DataSource = CurrentLibrary.ListOfVaults.Select(vault => vault.VaultName).ToList();
        }

        private void SetDataGridView()
        {
            //Gets the first IVaultComponent in the Library vault list to be used to set up the datatable columns
            VaultComponent component = CurrentLibrary.ListOfVaults[0].ComponentList[0];
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
            _componentTable.Columns["Name"].SetOrdinal(0);


            //Populate the datatable with the values in the vault corresponding to displayed text in the vaultComboBox
            Vault vault = CurrentLibrary.ListOfVaults.Find(vaultItem => vaultItem.VaultName.Equals(vaultComboBox.Text));

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

        public void RefreshDataGridView()
        {
            SetDataGridView();
        }

        private void _componentTable_TableNewRow(object sender, DataTableNewRowEventArgs e)
        {
            e.Row[AppConstants.VaultColumnName] = vaultComboBox.Text;
        }

        private void InitializeVaultNames()
        {
            _vaultNames = new List<string>();
            foreach (Vault vault in CurrentLibrary.ListOfVaults)
            {
                _vaultNames.Add(vault.VaultName);
            }

            //gets the list of vault names to use for the vaultCombobox
            vaultComboBox.DataSource = CurrentLibrary.ListOfVaults.Select(vaultName => vaultName.VaultName).ToList();

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

            CurrentLibrary = LibraryList[0]; //gets the first library in the library list to use as the m_Library;            

            //adds the librarynames to the conductors combobox as a datasource
            libraryTypeComboBox.DataSource = LibraryList.Select(item => item.LibraryName).ToList();
        }

        private void loadToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form componentDets = new ComponentDets(new Load(LoadType.Load), this);
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
            CurrentLibrary = LibraryList.Find(item => item.LibraryName.Equals(libraryTypeComboBox.Text));
            SetVaultNames();
            SetDataGridView();
        }

        private void okButton_Click(object sender, EventArgs e)
        {
            //if the user is Ok with the new changes, add the components to the list of vault components in the vault
            Vault vaultToAddTo = CurrentLibrary.ListOfVaults.Find(vaultItem => vaultItem.VaultName.Equals(vaultComboBox.Text)); //find the vault that matches the vault name in the library       
            vaultToAddTo.ComponentList.Clear();
            foreach (DataRow componentRow in _componentTable.Rows)
            {

                switch (CurrentLibrary.LibraryType)
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



            var writer = new System.Xml.Serialization.XmlSerializer(typeof(Library), new Type[] { typeof(ConductorLibrary), typeof(LoadLibrary), typeof(GeneratorLibary), typeof(ConductorVault), typeof(LoadVault), typeof(GeneratorVault), typeof(Conductor), typeof(Load) });
            WriteObject(_file.FullName);

            Vault result;
            ReadObject(_file.FullName, out result);
            Console.WriteLine("Vault Written Successfully");
            //ReadObject(_file.FullName);
            //using (FileStream filestream = File.Create(file.FullName))
            //{
            //    writer.Serialize(filestream, CurrentLibrary);
            //}


        }

        private static FileInfo GenerateFile()
        {
            System.IO.FileInfo file = new System.IO.FileInfo(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "HBAlgorithmLibrariesTest", "libraries.xml"));
            file.Directory.Create(); // If the directory already exists, this method does nothing.          
            return file;
        }

        public static void WriteObject(string path)
        {

            FileStream fs = new FileStream(path,
            FileMode.Create);
            XmlDictionaryWriter writer = XmlDictionaryWriter.CreateTextWriter(fs);
            DataContractSerializer ser =
                new DataContractSerializer(typeof(Library), new Type[] { typeof(ConductorLibrary), typeof(LoadLibrary), typeof(GeneratorLibary), typeof(ConductorVault), typeof(LoadVault), typeof(GeneratorVault), typeof(Conductor), typeof(Load) });
            ser.WriteObject(writer, CurrentLibrary.ListOfVaults[0]);
            Console.WriteLine("Finished writing object.");
            writer.Close();

            fs.Close();
        }

        public static void ReadObject(string path, out Vault result)
        {
            // Deserialize an instance of the Person class 
            // from an XML file. First create an instance of the 
            // XmlDictionaryReader.
            FileStream fs = new FileStream(path, FileMode.OpenOrCreate);
            XmlDictionaryReader reader =
                XmlDictionaryReader.CreateTextReader(fs, new XmlDictionaryReaderQuotas());

            // Create the DataContractSerializer instance.
            DataContractSerializer ser =
                new DataContractSerializer(typeof(Library), new Type[] { typeof(ConductorLibrary), typeof(LoadLibrary), typeof(GeneratorLibary), typeof(ConductorVault), typeof(LoadVault), typeof(GeneratorVault), typeof(Conductor), typeof(Load) });

            // Deserialize the data and read it from the instance.
            Console.WriteLine("Reading this object:");

            result =  (Vault)ser.ReadObject(reader);
            fs.Close();

        }


        private void generatorToolStripMenuItem_Click(object sender, EventArgs e)
        {
          
        }

        private void conductorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form componentDets = new ComponentDets(new Conductor(), this);
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
