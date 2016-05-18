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
    public partial class HomePageForm : Form
    {
        private LibrarySet _libSet;

        public HomePageForm(LibrarySet libraries)
        {
            InitializeComponent();
            _libSet = libraries;

        }

        private void newFeederButton_Click(object sender, EventArgs e)
        {
            new LoadSelectForm(_libSet).ShowDialog();
        }

        private void splitContainer1_Panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void splitContainer1_Panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void editLibraryButton_Click(object sender, EventArgs e)
        {

        }

        private void loadLibraryButton_Click(object sender, EventArgs e)
        {

        }

        private void buttonVaults_Click(object sender, EventArgs e)
        {
            new LibraryFormVault(_libSet).ShowDialog();
        }
    }
}
