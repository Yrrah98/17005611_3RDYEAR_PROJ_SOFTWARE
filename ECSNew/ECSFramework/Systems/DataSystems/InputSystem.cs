using ECSFramework.Args;
using ECSFramework.Interfaces.Generalnterfaces;
using ECSFramework.Interfaces.SystemInterfaces.DataSystems;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECSFramework.Systems.DataSystems
{
    public class InputSystem : IECSUpdateable, IPublisher
    {
        public EnqueueEvent _enqueue;

        private KeyboardState _oldInput;

        private KeyboardState _newInput;

        public InputSystem() 
        {
            _oldInput = new KeyboardState();

            _newInput = new KeyboardState();
        }


        #region IECSUpdateable
        public void Update(GameTime gameTime) 
        {
            _newInput = Keyboard.GetState();

            if (_oldInput != _newInput)
            {
                InputEventArgs args = new InputEventArgs(_newInput);

                _enqueue(args);

                _oldInput = _newInput;
            }
        }
        #endregion

        #region IPublisher
        public void InjectEnqueue(EnqueueEvent pEnqueue)
        {
            _enqueue = pEnqueue;
        }
        #endregion
    }
}
