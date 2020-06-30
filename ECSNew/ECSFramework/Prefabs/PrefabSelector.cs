using ECSFramework.Interfaces.Generalnterfaces;
using ECSFramework.Interfaces.SystemInterfaces.DataSystems;
using Microsoft.Xna.Framework.Content;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECSFramework.Prefabs
{
    /// <summary>
    /// AUTHOR: Harry Jones
    /// VERSION: 0.2
    /// CLASS PURPOSE: The purpose of the PrefabSelector class is to provide a means
    /// to generate a prefab without a concrete instantiation
    /// </summary>
    public class PrefabSelector
    {

        public PrefabSelector() 
        {

        }

        public IList<IComponent> GetComponentSet<T>(IComponentSystem cs, ContentManager cm) where T : IPrefab, new()
        {
            IPrefab p = new T();

            IList<IComponent> componentList = new List<IComponent>();

            return p.ReturnComponents(cs, cm, componentList);
        }

        public IList<IComponent> GetComponentSet(IPrefab prefab, IComponentSystem cs, ContentManager cm) 
        {


            IList<IComponent> componentList = new List<IComponent>();

            componentList = prefab.ReturnComponents(cs, cm, componentList);

            return componentList;
        }
    }
}
