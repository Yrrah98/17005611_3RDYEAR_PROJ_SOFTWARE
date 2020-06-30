using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECSFramework.Args
{
    public class InputEventArgs : EventArgs
    {
        public KeyboardState _keyboardState;

        public InputEventArgs(KeyboardState pKeyboardState)
        {
            _keyboardState = pKeyboardState;
        }
    }
}
