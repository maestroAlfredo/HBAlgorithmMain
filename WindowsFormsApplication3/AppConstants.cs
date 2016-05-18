using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HermanBetaAlgorithmAlphaNum
{
    public class AppConstants
    {

        #region LibraryNames
        public const string ConductorLibraryName = "Conductors";
        public const string LoadLibraryName = "Loads";
        public const string GeneratorLibraryName = "Generators";
        #endregion


        #region DefaultVaultNames
        public const string Vault1Name = "Vault 1";
        public const string Vault2Name = "Vault 2";
        #endregion

        #region DefaultLoadNames
        public const string Load1Name = "Load 1";
        public const string Load2Name = "Load 2";        
        #endregion


        #region DefaultGeneratorNames
        public const string Gen1Name = "Gen 1";
        public const string Gen2Name = "Gen 2";
        #endregion

        #region ComponentTableColumnNames
        public const string VaultColumnName = "VaultName";
        #endregion


        #region errorhandling messages
        public const string VaultMismatchException = "Vault Mismatch Exception";
        public const string ComponentMismatchExceptionString = "Component Mismatch Exception";
        #endregion
    }
}
