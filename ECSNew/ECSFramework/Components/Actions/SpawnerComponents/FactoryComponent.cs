using ECSFramework.Interfaces.Generalnterfaces;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECSFramework.Components.Actions.SpawnerComponents
{
    public class FactoryComponent : IComponent
    {
        public IDictionary<Keys, IPrefab> _factoryActions;
        public FactoryComponent() 
        {
            _factoryActions = new Dictionary<Keys, IPrefab>();
        }
    }
}
