using ECSFramework.Interfaces.Generalnterfaces;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECSFramework.Components.Tranforms
{
    public class VelocityComponent : IComponent
    {
        public Vector2 Direction { get; set; }
        public float MaxVelocity { get; set; }
        public float CurrentVelocity { get; set; }
        public VelocityComponent() 
        {
        }
    }
}
