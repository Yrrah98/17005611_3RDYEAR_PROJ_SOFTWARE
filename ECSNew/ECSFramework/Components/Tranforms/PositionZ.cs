using ECSFramework.Interfaces.Generalnterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECSFramework.Components.Transforms
{
    public class PositionZ : IComponent
    {
        public float Z { get; set; }
        public PositionZ() 
        {

        }
    }
}
