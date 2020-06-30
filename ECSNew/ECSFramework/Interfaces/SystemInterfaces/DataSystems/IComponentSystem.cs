using ECSFramework.Interfaces.Generalnterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECSFramework.Interfaces.SystemInterfaces.DataSystems
{
    public interface IComponentSystem
    {

        IComponent CreateComponent<T>() where T : IComponent, new();
    }
}
