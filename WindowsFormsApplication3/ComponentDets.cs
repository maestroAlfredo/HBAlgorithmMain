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


        public ComponentDets(VaultComponent vaultComponent, Library m_Library)
        {
            InitializeComponent();
            
            this.AutoSize = true;
            PropertyInfo[] propertyInfo = vaultComponent.GetType().GetProperties();
            TableLayoutPanel tablelayoutPanel = new TableLayoutPanel();
            this.Controls.Add(tablelayoutPanel);
            tablelayoutPanel.ColumnCount = 2;
            tablelayoutPanel.RowCount = propertyInfo.Length;
            tablelayoutPanel.AutoScroll = true;
            tablelayoutPanel.Dock = DockStyle.Fill;

            for (int row = 0; row < propertyInfo.Length; row++)
            {
                
                tablelayoutPanel.Controls.Add(new Label() { Text = propertyInfo[row].Name });
                if (propertyInfo[row].Name.Equals("VaultName"))
                {
                    tablelayoutPanel.Controls.Add(new ComboBox()
                    {
                        Text = "Enter Vault Name",
                        DataSource = m_Library.ListOfVaults.Select(vault => vault.VaultName).ToList(),
                        DropDownStyle = ComboBoxStyle.DropDownList
                    });

                }
                else { tablelayoutPanel.Controls.Add(new TextBox() { Text = "Enter " + propertyInfo[row].Name });
                                }

            }

            //// Create the "OK" and "Cancel" buttons
            //TableLayoutPanel tablelayoutPanel2 = new TableLayoutPanel();
            //tablelayoutPanel2.ColumnCount = 2; tablelayoutPanel2.RowCount = 1;
            //tablelayoutPanel2.Dock = DockStyle.Bottom;
            //Button inputOK = new Button(); inputOK.Text = "OK";
            //tablelayoutPanel2.Controls.Add(inputOK);
            //tablelayoutPanel2.Controls.Add(new Button() { Text = "Cancel" });

            //this.Controls.Add(tablelayoutPanel2);
        }

        private void ComponentDets_Shown(object sender, EventArgs e)
        {

        }

        private void inputOKButton_Click(object sender, EventArgs e)
        {

        }
    }
}
