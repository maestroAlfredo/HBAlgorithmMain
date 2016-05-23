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


namespace HermanBetaAlgorithmAlphaNum
{
    public partial class LoadSelectForm : Form
    {
        private LibrarySet _libraries;
        private BindingSource _loadSource;
        private BindingSource _genSource;

        
        
        
        public LoadSelectForm(LibrarySet library)
        {
            InitializeComponent();
            _libraries = library;
            InitializeTreeNodes();
            _loadSource = new BindingSource();
            _genSource = new BindingSource();
            dataGridViewSelectedGenerators.DataSource = _genSource;
            dataGridViewSelectedLoads.DataSource = _loadSource;

        }

        private void InitializeTreeNodes()
        {
            
            _libraries.LoadLibrary.ListOfVaults.ForEach(vault => treeViewLoads.Nodes.Add(vault.VaultName).Nodes.AddRange(((vault.ComponentList.Select(x => new TreeNode(x.GetName())).ToArray()))));
            _libraries.GeneratorLibrary.ListOfVaults.ForEach(vault => treeViewGenerators.Nodes.Add(vault.VaultName).Nodes.AddRange(((vault.ComponentList.Select(x => new TreeNode(x.GetName())).ToArray()))));
            treeViewGenerators.AfterCheck += node_AfterCheck;
            treeViewLoads.AfterCheck += node_AfterCheck;
            treeViewGenerators.Tag = _libraries.GeneratorLibrary.ListOfVaults;
            treeViewLoads.Tag = _libraries.LoadLibrary.ListOfVaults;
        }

        private void CheckAllChildNodes(TreeNode treeNode, bool nodeChecked)
        {
            foreach (TreeNode node in treeNode.Nodes)
            {
                node.Checked = nodeChecked;
                if (node.Nodes.Count > 0)
                {
                    // If the current node has child nodes, call the CheckAllChildsNodes method recursively.
                    this.CheckAllChildNodes(node, nodeChecked);
                }
            }
        }

        // NOTE   This code can be added to the BeforeCheck event handler instead of the AfterCheck event.
        // After a tree node's Checked property is changed, all its child nodes are updated to the same value.
        private void node_AfterCheck(object sender, TreeViewEventArgs e)
        {
            // The code only executes if the user caused the checked state to change.
            if (e.Action != TreeViewAction.Unknown)
            {
                if (e.Node.Nodes.Count > 0)
                {
                    /* Calls the CheckAllChildNodes method, passing in the current 
                    Checked value of the TreeNode whose checked state changed. */
                    this.CheckAllChildNodes(e.Node, e.Node.Checked);
                }
            }
            List<Load> compListLoads = new List<Load>(GetComponentList(treeViewLoads).Select(x => (Load)x));
            List<Load> compListGens = new List<Load>(GetComponentList(treeViewGenerators).Select(x => (Load)x));

            _loadSource.DataSource = compListLoads.ToDataTable();
            _genSource.DataSource = compListGens.ToDataTable();

        }

        private List<VaultComponent> GetComponentList(object sender)
        {
            List<Vault> vaultList = (List<Vault>)(sender as TreeView).Tag;
            List<VaultComponent> compList = new List<VaultComponent>();
            foreach (TreeNode node in (sender as TreeView).Nodes)
            {
                if (node.Checked)
                {
                    foreach (TreeNode node2 in node.Nodes)
                    {
                        if (node2.Checked)
                        {
                            compList.Add(vaultList.Find(x => x.VaultName == node.Text).ComponentList.Find(y => y.GetName().Equals(node2.Text)));
                        }
                    }

                }
            }
            return compList;
        }
        private void treeView2_AfterSelect(object sender, TreeViewEventArgs e)
        {

        }
    }
}
