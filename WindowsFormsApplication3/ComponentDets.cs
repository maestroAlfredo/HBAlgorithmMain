using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace VoltageDropCalculatorApplication
{
    public partial class ComponentDets : Form
    {
        public VaultComponent VaultComponent { get; set; }
        private LibraryFormVault parent;
        private PropertyInfo[] componentProperties;
        private TableLayoutPanel formTableLayoutPanel;
        private List<Vault> listOfVaults;
        private Library library;
        private ComboBox vaultNameComboBox;
        private ErrorProvider _errorProvider;

        public ErrorProvider ErrorProvider
        {
            get { return _errorProvider; }

            set { _errorProvider = value; }
        }

        





        public ComponentDets(VaultComponent vaultComponent, LibraryFormVault parentApplication)
        {
            InitializeComponent();
            parent = parentApplication;
            this.AutoSize = true;
            VaultComponent = vaultComponent;
            ErrorProvider = new ErrorProvider();     
            
            
            componentProperties = vaultComponent.GetType().GetProperties();
            formTableLayoutPanel = new TableLayoutPanel();
            formTableLayoutPanel.CellBorderStyle = TableLayoutPanelCellBorderStyle.Single;
            formTableLayoutPanel.AutoSizeMode = AutoSizeMode.GrowOnly;
            formTableLayoutPanel.AutoSize = true;

            this.tableLayoutPanel2.Controls.Add(formTableLayoutPanel);
            formTableLayoutPanel.ColumnCount = 2;
            formTableLayoutPanel.RowCount = componentProperties.Length;
            formTableLayoutPanel.Dock = DockStyle.Fill;
            formTableLayoutPanel.Anchor = AnchorStyles.None;

            componentProperties.Switch(componentProperties.Length - 3, 0);
            listOfVaults = parent.LibraryList.Where(library => library.VaultType == vaultComponent).First().ListOfVaults;
            library = parent.LibraryList.Where(library => library.VaultType == vaultComponent).First();



            for (int row = 0; row < componentProperties.Length; row++)
            {

                formTableLayoutPanel.Controls.Add(new Label() { Text = componentProperties[row].Name, Anchor = AnchorStyles.Right});
                if (componentProperties[row].Name.Equals("VaultName"))
                {
                    
                    formTableLayoutPanel.Controls.Add(vaultNameComboBox = new ComboBox()
                    {
                        Text = "Enter Vault Name",
                        DataSource =listOfVaults.Select(vault => vault.VaultName).ToList(),
                        Name = componentProperties[row].Name,
                        DropDownStyle = ComboBoxStyle.DropDownList
                    });
                    vaultNameComboBox.Dock = DockStyle.Fill;
                    VaultComponent.SetVault(listOfVaults.First());
                    vaultNameComboBox.SelectedIndexChanged += VaultNameCombo_SelectedIndexChanged;

                }
                else if (componentProperties[row].Name.Equals("LoadType"))
                {
                    ComboBox loadTypeCombo;
                    formTableLayoutPanel.Controls.Add(loadTypeCombo = new ComboBox()
                    {
                        DataSource = Enum.GetValues(typeof(LoadType)),
                        Name = componentProperties[row].Name,
                        DropDownStyle = ComboBoxStyle.DropDownList
                    });
                    loadTypeCombo.Dock = DockStyle.Fill;
                    loadTypeCombo.SelectedIndexChanged += LoadTypeCombo_SelectedIndexChanged;
                }
                else {

                    //if the property is of type string create a textbox, else create a numeric up down control
                    if (componentProperties[row].PropertyType.Equals(typeof(string)))
                    {
                        TextBox controlTextBox;
                        formTableLayoutPanel.Controls.Add(controlTextBox = new TextBox() { Text = "Enter " + componentProperties[row].Name, Name = componentProperties[row].Name });
                        if(controlTextBox.Name == "Name")
                        {
                            
                            controlTextBox.CausesValidation = true;
                            controlTextBox.Validating += ControlTextBox_Validating;
                            controlTextBox.Validated += ControlTextBox_Validated;
                        }                       
                        controlTextBox.Dock = DockStyle.Fill;
                    }
                    else
                    {
                        NumericUpDown numUpDown;
                        formTableLayoutPanel.Controls.Add(numUpDown = new NumericUpDown()
                        {
                            Name = componentProperties[row].Name,
                            Maximum = int.MaxValue,
                            Minimum = int.MinValue,
                            DecimalPlaces = 3,
                            Increment = 1,
                            Value = 0
                        });
                        numUpDown.CausesValidation = true;
                        numUpDown.Validated += NumUpDown_Validated;
                        numUpDown.Dock = DockStyle.Fill;                                          
                    }

                }

            }

        }

        private void ControlTextBox_Validating(object sender, CancelEventArgs e)
        {
           if(VaultComponent.GetVault().ComponentList.FindIndex(item=>item.Name.Equals((sender as TextBox).Text))!=-1)
            {
                e.Cancel = true;
                
                TextBox textbox = sender as TextBox;
                textbox.Select(0, textbox.Text.Length);
                string errorMsg = "A component with the Name: " + textbox.Text + " exists in the Vault: " + VaultComponent.GetVault().VaultName + ". Please choose another name or edit existing component.";
                ErrorProvider.SetIconAlignment(formTableLayoutPanel, ErrorIconAlignment.TopRight);
                ErrorProvider.SetError(formTableLayoutPanel, errorMsg);
            }
           
        }

        private void NumUpDown_Validated(object sender, EventArgs e)
        {
            NumericUpDown numUpDown = sender as NumericUpDown;
            VaultComponent.GetType().GetProperty(numUpDown.Name).SetValue(VaultComponent, Convert.ToDouble(numUpDown.Value));
        }

        private void ControlTextBox_Validated(object sender, EventArgs e)
        {                      
            TextBox textBoxInput = sender as TextBox;
            ErrorProvider.SetError(formTableLayoutPanel, "");
            VaultComponent.GetType().GetProperty(textBoxInput.Name).SetValue(VaultComponent, textBoxInput.Text);
        }

        private void LoadTypeCombo_SelectedIndexChanged(object sender, EventArgs e)
        {
            VaultComponent.GetType().GetProperty((sender as ComboBox).Name).SetValue(VaultComponent, (LoadType)Enum.Parse(typeof(LoadType), (sender as ComboBox).Text));
            var myLoad = VaultComponent as Load;
            switch (myLoad.LoadType)
            {
                case LoadType.Load:
                    myLoad.SetComponentType(ComponentType.Load);
                    break;
                case LoadType.Generator:
                    myLoad.SetComponentType(ComponentType.Generator);
                    break;
                default:
                    break;
            }
            listOfVaults = parent.LibraryList.Where(item => item.VaultType == VaultComponent).First().ListOfVaults;
            vaultNameComboBox.DataSource = listOfVaults.Select(vault => vault.VaultName).ToList();
            
        }

        private void VaultNameCombo_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox vaultNameCombo = sender as ComboBox;
            VaultComponent.SetVault(listOfVaults.Where(vault => vault.VaultName == vaultNameCombo.Text).First());
        }

        private void ComponentDets_Shown(object sender, EventArgs e)
        {

        }

        private void inputOKButton_Click(object sender, EventArgs e)
        {
            VaultComponent.GetVault().Add(VaultComponent); //adds the component to the vault
            parent.RefreshDataGridView(); //refreshes the datagridview in the parent form
            this.Close();
        }
    }
}
