using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using System.Xml.Serialization;

namespace VoltageDropCalculatorApplication
{
    public enum LibraryType
    {
        Conductor = 0,
        Generator = 1,
        Load = 2
    }

    public enum VaultType
    {
        Conductor = 0,
        Generator = 1,
        Load = 2
    }

    public enum LoadType
    {
        Generator = 1,
        Load = 2
    }

    public enum ComponentType
    {
        Conductor = 0,
        Load = 2
    }

    [XmlRoot("VaultComponent", Namespace = "VoltageDropCalculatorApplication", IsNullable = false)]
    public abstract class VaultComponent
    {
        public string Description { get; set; }        
        private ComponentType _componentType;
        public string VaultName { get; set; }
        private Vault _vault;

        public VaultComponent()
        {

        }
        public VaultComponent(Vault vault)
        {
            VaultName = vault.VaultName;
            _vault = vault;
            _vault.Add(this); //adds the component to the vault specified
        }

        public void SetComponentType(ComponentType componentType)
        {
            _componentType = componentType;
        }

        public ComponentType GetComponentType()
        {
            return _componentType;
        }

        public Vault GetVault()
        {
            return _vault;
        }

        public void SetVault(Vault vault)
        {
            _vault = vault;
        }
    }

    [XmlInclude(typeof(VaultComponent))]
    [Serializable]
    public class Conductor : VaultComponent
    {
        public string ConductorName { get; set; }
        public double Diameter { get; set; }
        public double Rkmt1 { get; set; }
        public double T { get; set; }
        public double k { get; set; }

        public Conductor()
        {

        }

        public Conductor(Vault vault, string name, double diameter, double rKmT1, double t, double kparam = 1, string description = "") : base(vault)
        {
            ConductorName = name;
            Diameter = diameter;
            Rkmt1 = rKmT1;
            T = t;
            k = kparam;
            Description = description;
            SetComponentType(ComponentType.Conductor);
        }

        public Conductor(DataRow row, Vault vault) : base(vault)
        {
            PropertyInfo[] properties = this.GetType().GetProperties();
            ConductorName = row[properties.Where(item => item.Name.Equals("ConductorName")).First().Name].ToString();
            Diameter = Convert.ToDouble(row[properties.Where(item => item.Name.Equals("Diameter")).First().Name]);
            Rkmt1 = Convert.ToDouble(row[properties.Where(item => item.Name.Equals("Rkmt1")).First().Name]);
            T = Convert.ToDouble(row[properties.Where(item => item.Name.Equals("T")).First().Name]);
            k = Convert.ToDouble(row[properties.Where(item => item.Name.Equals("k")).First().Name]);
            Description = row[properties.Where(item => item.Name.Equals("Description")).First().Name].ToString();
            SetComponentType(ComponentType.Conductor);
        }

    }
    /// <summary>
    /// A class that can describe a load based on its properties
    /// </summary>
    
    [XmlInclude(typeof(VaultComponent))]
    [Serializable]
    public class Load : VaultComponent
    {
        public string LoadDGName { get; set; }
        public LoadType LoadType { get; set; }
        public double Alpha { get; set; }
        public double Beta { get; set; }
        public double Cb { get; set; }


        public Load()
        {

        }

        /// <summary>
        /// Constructor that accepts the parameters of the load as values 
        /// </summary>
        /// <param name="vault"> Vault to which the load should be added </param>
        /// <param name="loadDGName"> name of the load </param>
        /// <param name="loadType"> enum that indictes whether the load is a generator or load </param>
        /// <param name="alpha"> the alpha parameter of the generator/load </param>
        /// <param name="beta"> the beta parameter of the generator/load </param>
        /// <param name="cb"> the circuit breaker rating of the load or generator in Amps </param>
        /// <param name="description">Personal description of the load/generator </param>
        public Load(Vault vault, string loadDGName, LoadType loadType, double alpha, double beta, double cb, string description = "") : base(vault)
        {
            LoadDGName = loadDGName;
            LoadType = loadType;
            Alpha = alpha;
            Beta = beta;
            Cb = cb;
            Description = description;
        }

        /// <summary>
        /// Load constructor that accepts a datarow object 
        /// </summary>
        /// <param name="row">the row of the table to which the load belongs</param>
        /// <param name="loadType">the type of the load Generator or Load</param>
        /// <param name="vault">The vault to which the load should be added</param>
        public Load(DataRow row, LoadType loadType, Vault vault) : base(vault)
        {
            PropertyInfo[] properties = this.GetType().GetProperties();
            LoadDGName = row[properties.Where(item => item.Name.Equals("LoadDGName")).First().Name].ToString();
            Alpha = Convert.ToDouble(row[properties.Where(item => item.Name.Equals("Alpha")).First().Name]);
            Beta = Convert.ToDouble(row[properties.Where(item => item.Name.Equals("Beta")).First().Name]);
            Cb = Convert.ToDouble(row[properties.Where(item => item.Name.Equals("Cb")).First().Name]);
            Description = row[properties.Where(item => item.Name.Equals("Description")).First().Name].ToString();
            LoadType = loadType;
        }

    }

    [XmlRoot("Vault", Namespace = "VoltageDropCalculatorApplication", IsNullable = false)]
    public abstract class Vault
    {
        public string VaultName { get; set; }
        public List<VaultComponent> ComponentList { get; set; }
        public Library Library{get;set;}
        public VaultType VaultType { get; set; }

        public Vault()
        {

        }

        public Vault(string vaultName, Library library)
        {
            VaultName = vaultName;                       
            ComponentList = new List<VaultComponent>();
            Library = library;
            Library.Add(this);
        }

        public Vault(string vaultName)
        {
            VaultName = vaultName;
            ComponentList = new List<VaultComponent>();
        }

        public void Add(VaultComponent vaultComponent)
        {
            if(this.VaultType!=vaultComponent.GetVault().VaultType)
            {
                throw new ComponentMismatchException(AppConstants.ComponentMismatchExceptionString);
            }
            else
            {
                vaultComponent.SetVault(this); //makes the current Vault the vault which the component belongs.
                ComponentList.Add(vaultComponent);
            }
            

        }

        public void Add(List<VaultComponent> vaultComponentList)
        {
            foreach (VaultComponent vaultComponent in vaultComponentList)
            {
                Add(vaultComponent);
            }
        }

    }

    [XmlInclude(typeof(Vault))]
    [Serializable]
    public class ConductorVault : Vault
    {
        private ConductorVault()
        {

        }
        public ConductorVault(string vaultName, ConductorLibrary conductorLibrary) : base(vaultName, conductorLibrary)
        {
            VaultType = VaultType.Conductor;
        }

        public ConductorVault(string vaultName):base(vaultName)
        {
            VaultType = VaultType.Conductor;
        }

    }

    [XmlInclude(typeof(Vault))]
    [Serializable]
    public class LoadVault : Vault
    {
        private LoadVault()
        {

        }
        public LoadVault(string vaultName, LoadLibrary loadLibrary) : base(vaultName, loadLibrary)
        {
            VaultType = VaultType.Load;
        }

        public LoadVault(string vaultName) : base(vaultName)
        {
            VaultType = VaultType.Load;
        }
    }

    [XmlInclude(typeof(Vault))]
    [Serializable]
    public class GeneratorVault : Vault
    {
        private GeneratorVault()
        {

        }

        public GeneratorVault(string vaultName, GeneratorLibary generatorLibrary) : base(vaultName, generatorLibrary)
        {
            VaultType = VaultType.Generator;
        }

        public GeneratorVault(string vaultName) : base(vaultName)
        {
            VaultType = VaultType.Generator;
        }
    }

    /// <summary>
    /// A library class
    /// </summary>
    [XmlRoot("Library", Namespace = "VoltageDropCalculatorApplication", IsNullable = false)]
    public abstract class Library
    {
        /// <summary>
        /// The type of the vaults in the library
        /// </summary>
        public VaultType VaultType { get; set; }
        /// <summary>
        /// The name of the Library
        /// </summary>
        public string LibraryName { get; set; }
        /// <summary>
        /// The type of the Library
        /// </summary>
        public LibraryType LibraryType { get; set; }
        /// <summary>
        /// The list of Vaults that the Library will contain
        /// </summary>
        /// 
        [XmlArray("ListOfVaults"), XmlArrayItem("Vault", typeof(Vault))]
        public List<Vault> ListOfVaults { get; set; }

        /// <summary>
        /// Instance of the Library
        /// </summary>
        /// <param name="libraryName">Name of the Library</param>
        public Library(string libraryName)
        {
            ListOfVaults = new List<Vault>();
            LibraryName = libraryName;
        }

        public void Add(Vault vault)
        {
            if(VaultType!=vault.VaultType)
            {
                VaultMismatchException ex = new VaultMismatchException("Cannot add a vault of type " + vault.Library.LibraryType.ToString() + " to type " + LibraryType.ToString());
                throw ex;              
            }
            else
            {
                vault.Library = this; //changes the Library to which the vault was added to the current Library.
                ListOfVaults.Add(vault); //adds the vault to the list of vaults in the library
            }           
        }

        public void Add(List<Vault> vaultList)
        {
            foreach (Vault vault in vaultList)
            {
                Add(vault);
            }
        }

        public Library()
        {

        }

    }

    [XmlInclude(typeof(Library))]
    [Serializable]
    public class ConductorLibrary : Library
    {
        private ConductorLibrary()
        {
           
        }
        public ConductorLibrary(string libraryName) : base(libraryName)
        {
            LibraryType = LibraryType.Conductor;
            VaultType = VaultType.Conductor;
        }

    }

    [XmlInclude(typeof(Library))]
    [Serializable]
    public class LoadLibrary : Library
    {
        private LoadLibrary()
        {
            
        }

        public LoadLibrary(string libraryName) : base(libraryName)
        {
            LibraryType = LibraryType.Load;
            VaultType = VaultType.Load;
        }

    }

    [XmlInclude(typeof(Library))]
    [Serializable]
    public class GeneratorLibary : Library
    {        
        private GeneratorLibary()
        {
            
        }

        public GeneratorLibary(string libraryName) : base(libraryName)
        {
            LibraryType = LibraryType.Generator;
            VaultType = VaultType.Generator;
        }


    }


}
