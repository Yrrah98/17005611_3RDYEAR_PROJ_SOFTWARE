using ECSFramework.Interfaces.Generalnterfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECSFramework.Components.Actions.MovementActions
{
    public class InputMovement : IComponent
    {
        public IList<Keys> KeyList { get; set; }

        public Vector2 Direction { get; set; }
        public InputMovement() 
        {
            KeyList = new List<Keys>();
        }
    }
}
