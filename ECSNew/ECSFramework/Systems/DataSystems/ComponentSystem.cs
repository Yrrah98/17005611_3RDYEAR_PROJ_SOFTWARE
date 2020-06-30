using ECSFramework.Interfaces.Generalnterfaces;
using ECSFramework.Interfaces.SystemInterfaces.DataSystems;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECSFramework.Systems.DataSystems
{
    public class ComponentSystem : IComponentSystem
    {

        public ComponentSystem() 
        {

        }

        public IComponent CreateComponent<T>() where T : IComponent, new() 
        {
            return new T();
        }
    }
}
