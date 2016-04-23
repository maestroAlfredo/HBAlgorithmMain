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


        public ComponentDets(VaultComponent vaultComponent)
        {
            InitializeComponent();

            PropertyInfo[] propertyInfo = vaultComponent.GetType().GetProperties();
            TableLayoutPanel tablelayoutPanel = new TableLayoutPanel();
            //tablelayoutPanel.Location = new Point()      
            tablelayoutPanel.ColumnCount = 2;
            tablelayoutPanel.RowCount = propertyInfo.Length;
            for (int row = 0; row < propertyInfo.Length; row++)
            {
                tablelayoutPanel.Controls.Add(new Label() { Text = propertyInfo[row].Name });
                tablelayoutPanel.Controls.Add(new TextBox() { Text = "Enter " + propertyInfo[row].Name });

            }           
            this.Controls.Add(tablelayoutPanel);

        }

        private void ComponentDets_Shown(object sender, EventArgs e)
        {

        }
    }
}
