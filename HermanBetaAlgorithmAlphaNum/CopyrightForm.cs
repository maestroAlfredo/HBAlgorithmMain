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
using System.IO;

namespace HermanBetaAlgorithmAlphaNum
{
    public partial class Copyright : Form
    {
        public Copyright()
        {
            InitializeComponent();

        }

        private void okButton_Click(object sender, EventArgs e)
        {
            this.Visible = false;
            //checks if the appdata folder has a Libraries.xml defined. If not it creates a Libraries.xml and adds the default vaults to it.
            LibrarySet lib = new LibrarySet(new LoadLibrary("Loads"), new GeneratorLibary("Gens"), new ConductorLibrary("Cables"));            
            if(!File.Exists(FileHandlers.GenerateFile().FullName))
            {                
                //creates a conductor library and adds a list of vaults to it
                var conductorLibrary = new ConductorLibrary(AppConstants.ConductorLibraryName);
                conductorLibrary.Add(DefaultVaults.GetConductorVaults());


                //creates a load library and adds a list of load vaults to it
                var loadLibrary = new LoadLibrary(AppConstants.LoadLibraryName);
                loadLibrary.Add(DefaultVaults.GetLoadVaults());

                //creates a generator library and adds a list of generator vaults to it
                var generatorLibrary = new GeneratorLibary(AppConstants.GeneratorLibraryName);
                generatorLibrary.Add(DefaultVaults.GetGeneratorVaults());

                lib = new LibrarySet(loadLibrary, generatorLibrary, conductorLibrary);

                FileHandlers.WriteObject(FileHandlers.GenerateFile().FullName, lib);
            }
            else
            {
                FileHandlers.ReadObject(FileHandlers.GenerateFile().FullName, out lib);
            }

            new HomePageForm(lib);

        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
