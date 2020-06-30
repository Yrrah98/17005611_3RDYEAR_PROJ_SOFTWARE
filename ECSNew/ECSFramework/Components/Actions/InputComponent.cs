using ECSFramework.Interfaces.Generalnterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECSFramework.Components.Actions
{
    public class InputComponent : IComponent
    {
        public IList<IComponent> Actions { get; set; }
        public InputComponent() 
        {
            Actions = new List<IComponent>();
        }
    }
}
