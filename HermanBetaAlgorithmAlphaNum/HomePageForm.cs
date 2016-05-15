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
    public partial class HomePageForm : Form
    {
        public LibrarySet ProjectLibraries { get; private set; }

        public HomePageForm(LibrarySet libraries)
        {
            InitializeComponent();
            ProjectLibraries = libraries;

        }

        private void newFeederButton_Click(object sender, EventArgs e)
        {
            this.Visible = false;
            new LoadSelectForm(ProjectLibraries);
        }

        private void splitContainer1_Panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void splitContainer1_Panel2_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
