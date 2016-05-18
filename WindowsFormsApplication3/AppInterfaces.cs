using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VoltageDropCalculatorApplication
{
    //public interface IVaultComponent
    //{
    //    ComponentType GetComponentType();
    //    void SetComponentType(ComponentType componentType);       


    //}

    public interface Ilibrary
    {
        void Add(Vault vault);
        void Add(List<Vault> vault);
        void Remove(Vault vault);
    }
}
