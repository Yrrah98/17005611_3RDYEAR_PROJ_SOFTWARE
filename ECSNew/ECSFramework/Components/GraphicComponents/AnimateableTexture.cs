using ECSFramework.Interfaces.Generalnterfaces;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECSFramework.Components.GraphicComponents
{
    public class AnimateableTexture : IComponent
    {
        /// <summary>
        /// PROPERTY: Width of the draw area
        /// </summary>
        public float Width { get; set; }
        /// <summary>
        /// PROPERTY: Height of the draw area
        /// </summary>
        public float Height { get; set; }
        /// <summary>
        /// PROPERTY: The position on the sprite atlas/sheet where the draw is starting on the X axis 
        /// The draw method multiplies the the draw area by the Current X and Y frames to draw the correct
        /// animation in the sequence
        /// </summary>
        public int CurrentXFrame { get; set; }
        /// <summary>
        /// PROPERTY: The position on the sprite atlas/sheet where the draw is starting on the Y axis
        /// The draw method multiplies the the draw area by the Current X and Y frames to draw the correct
        /// animation in the sequence
        /// </summary>
        public int CurrentYFrame { get; set; }
        /// <summary>
        /// PROPERTY: The number of frames a particular section of the animation is shown 
        /// before it switches into the next frame
        /// </summary>
        public int SwitchFrame { get; set; }
        /// <summary>
        /// PROPERTY: The maximum number of frames on the X axis
        /// </summary>
        public int MaxXFrame { get; set; }
        /// <summary>
        /// PROPERTY: The maxmimum number of frames on the Y axis
        /// </summary>
        public int MaxYFrame { get; set; }
        public Texture2D AnimateableTxtr { get; set; }
        public AnimateableTexture() 
        {

        }
    }
}
