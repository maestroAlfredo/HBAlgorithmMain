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

namespace HermanBetaAlgorithmAlphaNum
{
    public partial class AddCustomersForm : Form
    {
        private CustomerGroup _customer;
        NodeForm _parent;
        private double _distance;
        private DistributionKiosk _distributionKiosk;

        public CustomerGroup Customer
        {
            get
            {
                return _customer;
            }
        }
        public AddCustomersForm(NodeForm form)
        {
            _distributionKiosk = form.DistributionKiosk;
            comboBoxDistributionKiosk.DataSource = _parent.ListOfDistributionKiosks;
            comboBoxDistributionKiosk.DisplayMember = "Name";
            comboBoxLoadGen.DataSource = _parent.ListOfLoadsandGenerators;
            comboBoxLoadGen.DisplayMember = "LoadName";
            comboBoxLoadGen.ValueMember = "Load";
            InitializeComponent();
        }

        private void OKButton_Click(object sender, EventArgs e)
        {
            if(_distributionKiosk.customerList.Contains(Customer))
            {
                
            }
            else
            {
                _distributionKiosk.customerList.Add(new CustomerGroup((int)numericUpDownRed.Value, (int)numericUpDownBlue.Value, (int)numericUpDownWhite.Value, Customer.CustomerLoad, _distance));
            }

            //Update the datagridview in the parent
            _parent.UpdateDataGrid();
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void comboBoxDistributionKiosk_SelectedIndexChanged(object sender, EventArgs e)
        {
            _distributionKiosk = comboBoxDistributionKiosk.SelectedItem as DistributionKiosk;
        }

        private void comboBoxLoadGen_SelectedIndexChanged(object sender, EventArgs e)
        {
            Customer.CustomerLoad = (Load)comboBoxLoadGen.SelectedValue;
        }
    }
}
