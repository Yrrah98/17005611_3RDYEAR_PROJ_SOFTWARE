using ECSFramework.Interfaces.Generalnterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECSFramework.Interfaces.SystemInterfaces.DataSystems
{
    public interface IECSDatabase
    {

        /// <summary>
        /// PROPERTY: A property to privde access to a dictionary of a list of
        /// components the key is the "entity"
        /// </summary>
        IDictionary<String, IInnerDictionary> Entities { get; }

        IDictionary<Type, long> CompKeys { get; }

        IList<Type> ActiveComponents { get; }

        void AddEntity(String pEntity, IList<IComponent> pComponents);

        void RemoveEntity(String pEntity);
    }
}
