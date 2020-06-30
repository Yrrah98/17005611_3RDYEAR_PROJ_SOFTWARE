using ECSFramework.Interfaces.Generalnterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECSFramework.Components.Transforms
{
    public class PositionY : IComponent
    {
        public float Y { get; set; }
        public PositionY() 
        {

        }
    }
}
