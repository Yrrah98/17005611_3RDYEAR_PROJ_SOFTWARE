using ECSFramework.Interfaces.Generalnterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECSFramework.Components.Transforms
{
    public class PositionX : IComponent
    {
        public float X { get; set; }

        public PositionX() 
        {

        }
    }
}
