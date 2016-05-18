using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using HermanBetaAlgorithmAlphaNum;
using System.Reflection;

namespace HermanBetaAlgorithmAlphaNum
{
    public partial class LoadSelectForm : Form
    {
        private LibrarySet _library;
        private DataTable _generatorDataTable;
        private DataTable _loadDataTable;
        private BindingSource _loadBindingSource;
        private BindingSource _genBindingSource;

        public LoadSelectForm(LibrarySet library)
        {
            InitializeComponent();
            _library = library;
            InitializeTreeNodeViews();
            InitializeDatatables();
            InitializeDataGridViews();       
            
        }

        private void InitializeDataGridViews()
        {
            dataGridViewSelectedGenerators.DataSource = _genBindingSource;
            dataGridViewSelectedLoads.DataSource = _loadBindingSource;
        }

        private void InitializeDatatables()
        {
            _generatorDataTable = new DataTable();
            _loadDataTable = new DataTable();

            foreach(var property in typeof(Load).GetProperties())
            {
                _generatorDataTable.Columns.Add(property.Name, property.PropertyType);
                _loadDataTable.Columns.Add(property.Name, property.PropertyType);
            }
            _loadBindingSource = new BindingSource();
            _genBindingSource= new BindingSource();

            _loadBindingSource.DataSource = _loadDataTable;
            _genBindingSource.DataSource = _generatorDataTable;
        }

        private void InitializeTreeNodeViews()
        {

            treeViewGenerators.AfterCheck += node_AfterCheck;
            treeViewLoads.AfterCheck += node_AfterCheck;
            //adds the vaults as rootnodes and components as subnodes
            for(int rootNode = 0; rootNode<_library.LoadLibrary.ListOfVaults.Count; rootNode++)
            {
                treeViewLoads.Nodes.Add(_library.LoadLibrary.ListOfVaults[rootNode].VaultName);
                for(int child = 0; child < _library.LoadLibrary.ListOfVaults[rootNode].ComponentList.Count; child++)
                {
                    treeViewLoads.Nodes[rootNode].Nodes.Add(_library.LoadLibrary.ListOfVaults[rootNode].ComponentList[child].GetName());
                }
            }

            for (int rootNode = 0; rootNode < _library.GeneratorLibrary.ListOfVaults.Count; rootNode++)
            {
                treeViewGenerators.Nodes.Add(_library.GeneratorLibrary.ListOfVaults[rootNode].VaultName);
                for (int child = 0; child < _library.GeneratorLibrary.ListOfVaults[rootNode].ComponentList.Count; child++)
                {
                    treeViewGenerators.Nodes[rootNode].Nodes.Add(_library.GeneratorLibrary.ListOfVaults[rootNode].ComponentList[child].GetName());
                }
            }

        }

        private void node_AfterCheck(object sender, TreeViewEventArgs e)
        {
            // The code only executes if the user caused the checked state to change.
            if (e.Action != TreeViewAction.Unknown)
            {
                if (e.Node.Nodes.Count > 0)
                {
                    /* Calls the CheckAllChildNodes method, passing in the current 
                    Checked value of the TreeNode whose checked state changed. */
                    CheckAllChildNodes(e.Node, e.Node.Checked);
                }
            }

            //clear the loads and gens datatables
            _generatorDataTable.Rows.Clear();
            _loadDataTable.Rows.Clear();

            //Add selected components to the loads and gen datatables
            foreach(var node in GetCheckedNodes(treeViewLoads))
            {
                //creates a new row of the schema
                var row = _loadDataTable.NewRow();
                foreach(DataColumn column in _loadDataTable.Columns)
                {
                    row[column] = _library.LoadLibrary.ListOfVaults.Find(item => item.VaultName.Equals(node.Parent.Text)).ComponentList.Find(component => component.GetName().Equals(node.Text)).GetType().GetProperty(column.ColumnName).GetValue(_library.LoadLibrary.ListOfVaults.Find(item => item.VaultName.Equals(node.Parent.Text)).ComponentList.Find(component => component.GetName().Equals(node.Text)));
                }
                _loadDataTable.Rows.Add(row);
            }

            foreach(var node in GetCheckedNodes(treeViewGenerators))
            {
                var row = _generatorDataTable.NewRow();
                foreach (DataColumn column in _generatorDataTable.Columns)
                {
                    row[column] = _library.GeneratorLibrary.ListOfVaults.Find(item => item.VaultName.Equals(node.Parent.Text)).ComponentList.Find(component => component.GetName().Equals(node.Text)).GetType().GetProperty(column.ColumnName).GetValue(_library.GeneratorLibrary.ListOfVaults.Find(item => item.VaultName.Equals(node.Parent.Text)).ComponentList.Find(component => component.GetName().Equals(node.Text)));
                }
                _generatorDataTable.Rows.Add(row);
            }


        }

        private IEnumerable<TreeNode> GetCheckedNodes(TreeView treeView)
        {
            foreach (TreeNode nodeItem in treeView.Nodes)
            {
                foreach (TreeNode nodeItem2 in nodeItem.Nodes)
                {
                    if (nodeItem2.Checked)
                    {
                        yield return nodeItem2;
                    }
                }
            }

        }

        private void CheckAllChildNodes(TreeNode treeNode, bool nodeChecked)
        {
            foreach (TreeNode node in treeNode.Nodes)
            {
                node.Checked = nodeChecked;
                if (node.Nodes.Count > 0)
                {
                    // If the current node has child nodes, call the CheckAllChildsNodes method recursively.
                    CheckAllChildNodes(node, nodeChecked);
                }
            }
        }

        private void treeViewLoads_AfterSelect(object sender, TreeViewEventArgs e)
        {
            
        }
    }
}
