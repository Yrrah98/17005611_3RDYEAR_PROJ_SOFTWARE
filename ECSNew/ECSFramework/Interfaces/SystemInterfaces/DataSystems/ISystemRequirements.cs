using ECSFramework.Interfaces.Generalnterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECSFramework.Interfaces.SystemInterfaces.DataSystems
{
    /// <summary>
    /// AUTHOR: Harry Jones
    /// VERSION: 0.2
    /// CLASS PURPOSE: This classes purpose is to setup the requirements of each system
    /// </summary>
    public interface ISystemRequirements
    {

        IDictionary<Type, long> CompWithKey { get; }

        long GenerateSystemRequirements(IRqdComponents pCompReceiver);

        void GenerateNewComponentKey(Type pKey);
    }
}
