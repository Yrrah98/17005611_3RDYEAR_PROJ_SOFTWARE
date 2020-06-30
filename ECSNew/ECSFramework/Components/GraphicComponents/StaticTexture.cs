using ECSFramework.Interfaces.Generalnterfaces;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECSFramework.Components.GraphicComponents
{
    public class StaticTexture : IComponent
    {
        public Texture2D Texture { get; set; }

        public float drawWidth { get; set; }

        public float drawHeight { get; set; }
        public StaticTexture() 
        {

        }
    }
}
