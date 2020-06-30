using ECSFramework.Interfaces.Generalnterfaces;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECSFramework.Components.Tranforms
{
    public class PhysicsComponent : IComponent
    {
        public double Mass { get; set; }

        /// <summary>
        /// PROPERTY: IF true entity is dynamic and moves around
        /// IF false entity is a static object
        /// </summary>
        public bool DynamicEntity { get; set; }

        public Rectangle BoundingBox { get; set; }

        public PhysicsComponent() 
        {

        }
    }
}
