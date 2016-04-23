using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VoltageDropCalculatorApplication
{

    [Serializable]
    public class VaultMismatchException : Exception
    {
        public VaultMismatchException() { }
        public VaultMismatchException(string message) : base(message) { }
        public VaultMismatchException(string message, Exception inner) : base(message, inner) { }
        protected VaultMismatchException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context) : base(info, context)
        { }
    }


    [Serializable]
    public class ComponentMismatchException : Exception
    {
        public ComponentMismatchException() { }
        public ComponentMismatchException(string message) : base(message) { }
        public ComponentMismatchException(string message, Exception inner) : base(message, inner) { }
        protected ComponentMismatchException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context) : base(info, context)
        { }
    }
}
