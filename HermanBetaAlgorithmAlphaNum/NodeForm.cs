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

        public event OnPropertyChanged OnDistributionKioskChanged;

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


        public DataTable LoadsDataTable { get; set; }
        public DataView LoadsView { get; set; }


        private DataView _loadsView;

        
        
        public NodeForm()
        {
            InitializeComponent();
        }

        private void addNodeButton_Click(object sender, EventArgs e)
        {

        }

        private void nodeNameCombo_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        public delegate void OnPropertyChanged(object sender, EventArgs e);


    }
  
}
