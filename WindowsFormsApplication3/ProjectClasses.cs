using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using System.Xml.Serialization;
using System.Runtime.Serialization;
using System.IO;
using System.Xml;

namespace VoltageDropCalculatorApplication
{
    static class PropertyInfoExtensions
    {
        private static int PropertyOrder(this PropertyInfo propInfo)
        {
            int output;
            var orderAttr = (OrderPropertiesAttribute)propInfo.GetCustomAttributes(typeof(OrderPropertiesAttribute), true).SingleOrDefault();
            output = orderAttr != null ? orderAttr.Order : Int32.MaxValue;
            return output;
        }
    }

    public class OrderPropertiesAttribute : Attribute
    {
        public int Order { get; private set; }
        public OrderPropertiesAttribute(int order)
        {
            Order = order;
        }
    }

    public class DistributionKiosk : ITreeNodeAware<DistributionKiosk>, IDisposable
    {
        /// <summary>
        /// The name of the distribution kiosk
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// The cable that connects the feeder node to the previous node
        /// </summary>
        public Conductor Cable { get; set; }
        /// <summary>
        /// The distance to the previous Node
        /// </summary>
        public double DistanceToPreviousNode { get; set; }
        /// <summary>
        /// The reference to the tree node structure used to build the tree of nodes
        /// </summary>
        public TreeNode<DistributionKiosk> Node { get; set; }
        /// <summary>
        /// The list of loads or generators connected to the load.
        /// </summary>
        public List<Load> loadList { get; set; }       

        /// <summary>
        /// the List of customers that are connected to the different loads.
        /// </summary>
        public List<CustomerGroup> customerList { get; set; }   
        /// <summary>
        /// creates a feeder node
        /// </summary>
        /// <param name="cable">The cable that connects the feeder node to the previous node</param>
        /// <param name="distance">The distance to the previous Node</param>
        /// <param name="parent">The parent Node if supplied</param>
        public DistributionKiosk(Conductor cable, double distance)
        {
            Cable = cable;
            DistanceToPreviousNode = distance;
            loadList = new List<Load>();
            Node = new TreeNode<DistributionKiosk>(this);
        }
        /// <summary>
        /// Adds this node to the Parent node
        /// </summary>
        /// <param name="Parent"></param>
        public void Add(DistributionKiosk nodeToAdd)
        {

            //add a Treenode<FeederNode> to this FeederNode's TreeNode<FeederNode> Node Property
            nodeToAdd.Node.Parent = this.Node;
        }

        #region IDisposable Support
        private bool disposedValue = false; // To detect redundant calls

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: dispose managed state (managed objects).
                }

                // TODO: free unmanaged resources (unmanaged objects) and override a finalizer below.
                // TODO: set large fields to null.

                disposedValue = true;
            }
        }

        // TODO: override a finalizer only if Dispose(bool disposing) above has code to free unmanaged resources.
        // ~FeederNode() {
        //   // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
        //   Dispose(false);
        // }

        // This code added to correctly implement the disposable pattern.
        public void Dispose()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            Dispose(true);
            // TODO: uncomment the following line if the finalizer is overridden above.
            // GC.SuppressFinalize(this);
        }
        #endregion


    }


    public enum LibraryType
    {
        Conductor = 0,
        Load = 1,
        Generator = 2
    }

    public class CustomerGroup
    {
        public int RedPhase { get; set; }
        public int BluePhase { get; set; }
        public int WhitePhase { get; set; }
        public double Distance { get; set; }

        private Load _customerLoad;
        public Load CustomerLoad
        {
            get { return _customerLoad; }
            set
            {
                _customerLoad = value;
            }
        }

        public CustomerGroup(int redPhase, int bluePhase, int whitePhase, Load load, double distance)
        {
            RedPhase = redPhase;
            BluePhase = bluePhase;
            WhitePhase = whitePhase;
            CustomerLoad = load;
            Distance = distance;
        }

        public CustomerGroup()
        {
            
        }
    }

    public enum VaultType
    {
        Conductor = 0,
        Load = 1,
        Generator = 2
    }

    public enum LoadType
    {       
        Load = 1,
        Generator = 2
    }

    public enum ComponentType
    {
        Conductor = 0,
        Load = 1,
        Generator = 2
    }


    [DataContract(Name = "Component", Namespace = "VoltageDropCalculatorApplication")]
    public abstract class VaultComponent
    {
        [DataMember]
        public string Name { get; set; }
        [DataMember]
        public string Description { get; set; }
        [DataMember]
        private ComponentType _componentType;
        [DataMember]
        public string VaultName { get; set; }
        [DataMember]
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
            VaultName = vault.VaultName;
        }

        public static implicit operator VaultType(VaultComponent component)
        {
            VaultType vaultType = VaultType.Generator;
            switch (component.GetComponentType())
            {
                case ComponentType.Conductor:
                    vaultType = VaultType.Conductor;
                    break;
                case ComponentType.Load:
                    vaultType = VaultType.Load;
                    break;
                default:
                    break;
            }
            return vaultType;
        }

       
    }

    [XmlInclude(typeof(VaultComponent))]
    [Serializable]
    public class Conductor : VaultComponent
    {        
        public double Diameter { get; set; }
        public double Rkmt1 { get; set; }
        public double T { get; set; }
        public double k { get; set; }

        public Conductor()
        {
            SetComponentType(ComponentType.Conductor);
        }

        public Conductor(Vault vault, string name, double diameter, double rKmT1, double t, double kparam = 1, string description = "") : base(vault)
        {
            Name = name;
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
            Name = row[properties.Where(item => item.Name.Equals("Name")).First().Name].ToString();
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
        public LoadType LoadType { get; set; }
        public double Alpha { get; set; }
        public double Beta { get; set; }
        public double Cb { get; set; }


        public Load()
        {
            

        }

        public Load(LoadType loadType)
        {
            LoadType = loadType;
            SetComponentType(loadType == LoadType.Load ? ComponentType.Load : ComponentType.Generator);

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
            Name = loadDGName;
            LoadType = loadType;
            Alpha = alpha;
            Beta = beta;
            Cb = cb;
            Description = description;
            SetComponentType(loadType == LoadType.Load ? ComponentType.Load : ComponentType.Generator);
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
            Name = row[properties.Where(item => item.Name.Equals("Name")).First().Name].ToString();
            Alpha = Convert.ToDouble(row[properties.Where(item => item.Name.Equals("Alpha")).First().Name]);
            Beta = Convert.ToDouble(row[properties.Where(item => item.Name.Equals("Beta")).First().Name]);
            Cb = Convert.ToDouble(row[properties.Where(item => item.Name.Equals("Cb")).First().Name]);
            Description = row[properties.Where(item => item.Name.Equals("Description")).First().Name].ToString();
            LoadType = loadType;
            SetComponentType(loadType == LoadType.Load ? ComponentType.Load : ComponentType.Generator);
        }

    }

    [XmlRoot("Vault", Namespace = "VoltageDropCalculatorApplication", IsNullable = false)]
    [DataContract(IsReference =true)]
    public abstract class Vault
    {
        [DataMember]
        public string VaultName { get; set; }
        [DataMember]
        public List<VaultComponent> ComponentList { get; set; }
        [DataMember]
        public Library Library { get; set; }
        [DataMember]
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
            if (this.VaultType != vaultComponent.GetVault().VaultType)
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

        public ConductorVault(string vaultName) : base(vaultName)
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
    [DataContract(IsReference = true, Name = "Library", Namespace = "VoltageDropCalculatorApplication")]
    public abstract class Library
    {
        /// <summary>
        /// The type of the vaults in the library
        /// </summary>
        [DataMember]
        public VaultType VaultType { get; set; }
        /// <summary>
        /// The name of the Library
        /// </summary>
        [DataMember]
        public string LibraryName { get; set; }
        /// <summary>
        /// The type of the Library
        /// </summary>
        /// 
        [DataMember]
        public LibraryType LibraryType { get; set; }
        /// <summary>
        /// The list of Vaults that the Library will contain
        /// </summary>
        /// 
        //[XmlArray("ListOfVaults"), XmlArrayItem("Vault", typeof(Vault))]
        [DataMember]
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
            if (VaultType != vault.VaultType)
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

    [DataContract(IsReference = true, Name = "LibrarySet", Namespace = "VoltageDropCalculatorApplication")]
    public class LibrarySet
    {
        [DataMember]
        public LoadLibrary LoadLibrary { get; private set; }

        [DataMember]
        public GeneratorLibary GeneratorLibrary { get; private set; }

        [DataMember]
        public ConductorLibrary ConductorLibrary { get; private set; }

        public LibrarySet(LoadLibrary loads, GeneratorLibary generators, ConductorLibrary conductors)
        {
            LoadLibrary = loads;
            GeneratorLibrary = generators;
            ConductorLibrary = conductors;
        }
    }

    /// <summary>
    /// This class provides basic read and write functionality for the types in the project.
    /// </summary>
    public static class FileHandlers
    {
        public static FileInfo GenerateFile()
        {
            System.IO.FileInfo file = new System.IO.FileInfo(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "HBAlgorithmLibrariesTest", "libraries.xml"));
            file.Directory.Create(); // If the directory already exists, this method does nothing.          
            return file;
        }

        public static void WriteObject(string path, LibrarySet libraries)
        {

            FileStream fs = new FileStream(path,
            FileMode.Create);
            XmlDictionaryWriter writer = XmlDictionaryWriter.CreateTextWriter(fs);
            DataContractSerializer ser =
                new DataContractSerializer(typeof(LibrarySet), new Type[] { typeof(Library), typeof(ConductorLibrary), typeof(LoadLibrary), typeof(GeneratorLibary), typeof(ConductorVault), typeof(LoadVault), typeof(GeneratorVault), typeof(Conductor), typeof(Load) });
            ser.WriteObject(writer, libraries);
            Console.WriteLine("Finished writing object.");
            writer.Close();
            fs.Close();
        }

        public static void ReadObject(string path, out LibrarySet result)
        {
            // Deserialize an instance of the Person class 
            // from an XML file. First create an instance of the 
            // XmlDictionaryReader.
            FileStream fs = new FileStream(path, FileMode.OpenOrCreate);
            XmlDictionaryReader reader =
                XmlDictionaryReader.CreateTextReader(fs, new XmlDictionaryReaderQuotas());

            // Create the DataContractSerializer instance.
            DataContractSerializer ser =
                new DataContractSerializer(typeof(LibrarySet), new Type[] { typeof(Library), typeof(ConductorLibrary), typeof(LoadLibrary), typeof(GeneratorLibary), typeof(ConductorVault), typeof(LoadVault), typeof(GeneratorVault), typeof(Conductor), typeof(Load) });

            // Deserialize the data and read it from the instance.
            Console.WriteLine("Reading this object:");

            result = (LibrarySet)ser.ReadObject(reader);
            fs.Close();

        }

    }


}
