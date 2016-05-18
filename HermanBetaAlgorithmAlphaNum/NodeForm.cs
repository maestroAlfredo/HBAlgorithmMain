using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using VoltageDropCalculatorApplication;
using System.Reflection;
using System.IO;
using System.Xml;
using System.Runtime.Serialization;

namespace HermanBetaAlgorithmAlphaNum
{
    /// <summary>
    /// This form shows a feeder node object and it's position in the feeder node.
    /// </summary>
    public partial class NodeForm : Form
    {
        public DistributionKiosk _distributionKiosk; 
        // the distribution kiosk        
        public DistributionKiosk DistributionKiosk
        {
            get
            {
                return _distributionKiosk;
            }

            set
            {
                if(OnDistributionKioskChanged!=null)
                {
                    OnDistributionKioskChanged(this, EventArgs.Empty);
                }
                _distributionKiosk = value;
            }
        }

        public DataTable CustomersTable
        {
            get
            {
                return GenerateCustomerTable(DistributionKiosk.customerList);
            }
        }

        private DataTable GenerateCustomerTable(List<CustomerGroup> distributionKioskCustomers)
        {
            DataTable customerTable = new DataTable(DistributionKiosk.Name);
            //adds the columns to the datatable
            foreach(var prop in new CustomerGroup().GetType().GetProperties())
            {
                if(prop.PropertyType!=typeof(Load))
                {
                    customerTable.Columns.Add(prop.Name, prop.PropertyType);
                }
                else
                {
                    foreach(var p in new Load().GetType().GetProperties())
                    {
                        customerTable.Columns.Add(p.Name, p.PropertyType);
                    }
                }                
            }
            //populates the datatable
            foreach(CustomerGroup customer in distributionKioskCustomers)
            {
                DataRow customerRow = customerTable.NewRow();
                foreach (var prop in customer.GetType().GetProperties())
                {
                    if (prop.PropertyType != typeof(Load))
                    {
                        customerRow[prop.Name] = prop.GetValue(customer);
                    }
                    else
                    {
                        foreach (var p in new Load().GetType().GetProperties())
                        {
                            customerRow[p.Name] = p.GetValue(customer.CustomerLoad);
                        }
                    }
                }
            }

            return customerTable;
        }

        internal void UpdateDataGrid()
        {
            nodeDataGridView.DataSource = new DataView(CustomersTable);
        }

        public event OnPropertyChanged OnDistributionKioskChanged;
        public event OnPropertyChanged OnCustomerListChanged;

        public TreeNode<DistributionKiosk>.TreeTraversalType _disposeTraversal;
        public TreeNode<DistributionKiosk>.TreeTraversalType DisposeTraversal
        {
            get
            {
                return _disposeTraversal;
            }
            set
            {
                _disposeTraversal = value;
            }
        }

        private List<DistributionKiosk> _listOfDistributionKiosks;
        public List<DistributionKiosk> ListOfDistributionKiosks
        {
            get
            {
                _listOfDistributionKiosks.Clear();
                addNodeToList(this.DistributionKiosk.Node.Root.Value);
                return _listOfDistributionKiosks;
            }
        }

        private void addNodeToList(DistributionKiosk root)
        {
            if (DisposeTraversal == TreeNode<DistributionKiosk>.TreeTraversalType.BottomUp)
            {
                foreach (TreeNode<DistributionKiosk> node in root.Node.Children)
                {
                    addNodeToList(node.Value);
                }
            }

            _listOfDistributionKiosks.Add(root.Node.Value);

            if (DisposeTraversal == TreeNode<DistributionKiosk>.TreeTraversalType.TopDown)
            {
                foreach (TreeNode<DistributionKiosk> node in root.Node.Children)
                {
                    addNodeToList(node.Value);
                }
            }

        }


        
        public DataView LoadsView { get; set; }


        private DataView _loadsView;
        private DataTable _loadsTable;
        private List<CustomerGroup> _distributionKioskCustomers;

        public CableVaultWrapper ListOfCablesOfProject { get; private set; }
        public LoadVaultWrapper ListOfLoadsandGenerators { get; private set; }

        public NodeForm()
        {
            InitializeComponent();
            OnCustomerListChanged += NodeForm_OnLoadListChanged;
            InitializeComboBoxes();
            OnDistributionKioskChanged += NodeForm_OnDistributionKioskChanged;

        }

        private void NodeForm_OnDistributionKioskChanged(object sender, EventArgs e)
        {
            //update the datagridview;
            nodeDataGridView.DataSource = new DataView(CustomersTable);
        }

        private void InitializeComboBoxes()
        {
            nodeNameCombo.DataSource = ListOfDistributionKiosks;
            nodeNameCombo.DisplayMember = "Name";
            cableSelectCombo.DataSource = ListOfCablesOfProject;
            cableSelectCombo.DisplayMember = "ConductorName";
            cableSelectCombo.ValueMember = "Conductor";
        }


        private void NodeForm_OnLoadListChanged(object sender, EventArgs e)
        {

        }

        private void addNodeButton_Click(object sender, EventArgs e)
        {
            //create new DistributionKiosk and make the current node its parent
            var kiosk= new DistributionKiosk((Conductor)cableSelectCombo.SelectedValue, (double)lengthNumericUpDown.Value);
            //new DistributionKiosk()
        }

        private void nodeNameCombo_SelectedIndexChanged(object sender, EventArgs e)
        {
            DistributionKiosk = (DistributionKiosk)nodeNameCombo.SelectedItem;
        }

        public delegate void OnPropertyChanged(object sender, EventArgs e);

        private void cableSelectCombo_SelectedIndexChanged(object sender, EventArgs e)
        {
            DistributionKiosk.Cable = (Conductor)cableSelectCombo.SelectedValue;
        }

        private void lengthNumericUpDown_ValueChanged(object sender, EventArgs e)
        {
            DistributionKiosk.DistanceToPreviousNode = (double)lengthNumericUpDown.Value;
        }

        private void pictureBoxAddConductor_MouseHover(object sender, EventArgs e)
        {
            new ToolTip().SetToolTip(this.pictureBoxAddConductor, "Add new conductor");
        }

        private void pictureBoxAddLoad_MouseHover(object sender, EventArgs e)
        {
            new ToolTip().SetToolTip(this.pictureBoxAddLoad, "Add a load");
        }

        private void pictureBoxAddGen_MouseHover(object sender, EventArgs e)
        {
            new ToolTip().SetToolTip(this.pictureBoxAddGen, "Add new generator");
        }
    }

    /// <summary>
    /// wraps the conductor into a class with its vault and cable
    /// </summary>
    public class CableVaultWrapper
    {
        public Conductor Conductor { get; private set; }
        public string ConductorName
        {
            get { return Conductor.Name + "->" + Conductor.VaultName; }
        }

        public CableVaultWrapper(Conductor conductor)
        {
            Conductor = conductor;
        }      
    }

    public class LoadVaultWrapper
    {
        public Load Load { get; private set; }
        public string LoadName
        {
            get { return Load.Name + "->" + Load.VaultName; }
        }

        public LoadVaultWrapper(Load load)
        {
            Load = load;
        }
    }
  
}
