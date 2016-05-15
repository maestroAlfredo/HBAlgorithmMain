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
        LibrarySet _libraries;
        public LoadSelectForm(LibrarySet library)
        {
            InitializeComponent();
            _libraries = library;

        }
    }
}
