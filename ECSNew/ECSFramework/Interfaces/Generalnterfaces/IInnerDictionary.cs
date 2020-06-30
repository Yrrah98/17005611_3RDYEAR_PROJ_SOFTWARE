using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECSFramework.Interfaces.Generalnterfaces
{
    public interface IInnerDictionary
    {

        IDictionary<Type, IComponent> Components { get; }

        void AddEntry(Type pKey, IComponent pComponent);
    }
}
