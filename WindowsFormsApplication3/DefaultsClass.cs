using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace VoltageDropCalculatorApplication
{
    /// <summary>
    /// This class creates default Vaults as well as components. The components are added to the vaults. Ultimately the vaults are added to lists which can then be added to Libraries. 
    /// </summary>
    //class for default vaults
    public static class DefaultVaults
    {
        private static List<Vault> _conductorVaultList;
        private static List<Vault> _loadVaultList;
        private static List<Vault> _generatorVaultList;


        //initializes the vaults and the components inside the vaults
        private static void SetDefaults()
        {

            //creates two new instances of conductor vaults
            _conductorVaultList = new List<Vault>();
            ConductorVault conductorVault1 = new ConductorVault("Vconductor1");
            ConductorVault conductorVault2 = new ConductorVault("Vconductor2");

            //adds the conductor vaults to the conductor vault list
            _conductorVaultList.Add(conductorVault1);
            _conductorVaultList.Add(conductorVault2);


            //Adds components to each of the conductor vaults in the conductor vault lists
            foreach(ConductorVault vault in _conductorVaultList)
            {
                //instance of the new conductor to be added. NB. The conductor is added to the list of components of the vault in the parameter list
                new Conductor(vault, "ABC35", 35, 0.87, 228, 0.7, "ABC 35sq.mm Al French Standard");
                new Conductor(vault, "ABC25", 25, 1.2, 228, 0.5, "ABC 25sq.mm Al French Standard");
                new Conductor(vault, "ABC50", 50, 0.64, 228, 1, "ABC 50sq.mm Al French Standard");
            }

            _loadVaultList = new List<Vault>();
            LoadVault loadVault1 = new LoadVault("VLoad1");
            LoadVault loadVault2 = new LoadVault("VLoad2");

            _loadVaultList.Add(loadVault1);
            _loadVaultList.Add(loadVault2);

            foreach(LoadVault vault in _loadVaultList)
            {
               new Load(vault, AppConstants.Load1Name, LoadType.Load, 1.5, 4, 60);
               new Load(vault, AppConstants.Load2Name, LoadType.Load, 4, 1.5, 60);              
            }

            _generatorVaultList = new List<Vault>();

            GeneratorVault genVault1 = new GeneratorVault("VGen1");
            GeneratorVault genVault2 = new GeneratorVault("VGen2");

            _generatorVaultList.Add(genVault1);
            _generatorVaultList.Add(genVault2);

            foreach (GeneratorVault vault in _generatorVaultList)
            {
                new Load(vault, AppConstants.Gen1Name, LoadType.Generator, 1.5, 4, 60);
                new Load(vault, AppConstants.Gen2Name, LoadType.Generator, 4, 1.5, 60);
            }        
        }

        public static List<Vault> GetConductorVaults()
        {
            SetDefaults();
            return _conductorVaultList;
        }

        public static List<Vault> GetGeneratorVaults()
        {
            SetDefaults();
            return _generatorVaultList;
        }

        public static List<Vault> GetLoadVaults()
        {
            SetDefaults();
            return _loadVaultList;
        }
    }

    }
