using ECSFramework.Args;
using ECSFramework.Components.Actions;
using ECSFramework.Components.Actions.MovementActions;
using ECSFramework.Components.GraphicComponents;
using ECSFramework.Components.Tranforms;
using ECSFramework.Components.Transforms;
using ECSFramework.Interfaces.Generalnterfaces;
using ECSFramework.Interfaces.SystemInterfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECSFramework.Systems.ComponentSystems
{
    public class AnimateableDrawSystem : IComponentReceiver, IECSUpdateable, IRqdComponents, 
        IRender, IEventListener, IEntityRemove
    {
        private IDictionary<String, IInnerDictionary> _entities;

        /// <summary>
        /// PROPERTY: A read only property which contains a list of the types this system requires in order to function
        /// </summary>
        public IReadOnlyList<Type> RqdComponents
        {
            get
            {
                return new List<Type>() { typeof(PositionX), typeof(PositionY), typeof(PositionZ),
                    typeof(AnimateableTexture), typeof(InputComponent) };
            }
        }
        /// <summary>
        /// PROPERTY: A property which contains a binary key for what entities this system will need
        /// </summary>
        public long RequiredKeys { get { return _requiredKeys; } set { if (_requiredKeys == 0) _requiredKeys = value; } }
        private long _requiredKeys;
        // DECLARE a variable of type integer called _frameCounter
        private int _frameCounter;

        private KeyboardState _oldState;

        private KeyboardState _newState;
        public AnimateableDrawSystem() 
        {

        }

        #region IEntityRemove
        public void RemoveEntity(String entity)
        {
            if (_entities.ContainsKey(entity))
                _entities.Remove(entity);
        }
        #endregion

        #region IEventListener
        public void HandleEvent(object source, EventArgs args) 
        {
            // IF the EventArgs passed in is of type InputEventArgs
            if (args is InputEventArgs)
            {
                if (_newState != (args as InputEventArgs)._keyboardState)
                {
                    _oldState = _newState;

                    _newState = (args as InputEventArgs)._keyboardState;
                }
            }

        }
        #endregion

        #region IECSUpdateable
        public void Update(GameTime gameTime)
        {
            _frameCounter += (int)gameTime.ElapsedGameTime.TotalMilliseconds;

            #region What to do with input 
            if (_newState == _oldState)
            {
                if (_entities != null)
                {

                    foreach (String entity in _entities.Keys)
                    {
                        foreach(IComponent c in (_entities[entity].Components[typeof(InputComponent)] as InputComponent).Actions)
                            if(c is InputMovement)
                                foreach (Keys key in (c as InputMovement).KeyList)
                                {
                                    // IF the key is down in the new keyboard state and is up in the old keyboard state
                                    if (_newState.IsKeyDown(key) && _oldState.IsKeyUp(key))
                                    {
                                        // THEN they key has just been pressed,
                                        // SET the old keyboard state to the new keyboard state
                                        _oldState = _newState;
                                    }
                                    // IF the key is Up in the new state and the key is down in the old keyboard state
                                    if (_newState.IsKeyUp(key) && _oldState.IsKeyDown(key))
                                    {
                                        // THEN the key has just been released
                                        // SET the old state to the new state
                                        _oldState = _newState;
                                    }
                                    // IF the key is down in the old state and in the new state 
                                    if (_newState.IsKeyDown(key) && _oldState.IsKeyDown(key))
                                    {
                                        // THEN the key is being held down
                                        // INSTANTIATE a new vector 2 called velocity
                                        // CALL ReturnComponentByType Velocity2DComponent and set the x and y value of the 
                                        // local vector 2 to the Speed variable in the Velocity2DComponent (requires a cast)
                                        Vector2 vel = new Vector2(
                                            (_entities[entity].Components[typeof(VelocityComponent)] as VelocityComponent).CurrentVelocity,
                                            (_entities[entity].Components[typeof(VelocityComponent)] as VelocityComponent).CurrentVelocity);
                                        // IF the InputMovement plane variable is "H", this requires horizontal movement
                                        if ((_entities[entity].Components[typeof(InputMovement)] as InputMovement).Direction.X > 0 || 
                                            (_entities[entity].Components[typeof(InputMovement)] as InputMovement).Direction.X < 0 &&
                                            (_entities[entity].Components[typeof(AnimateableTexture)] as AnimateableTexture).CurrentXFrame <
                                            (_entities[entity].Components[typeof(AnimateableTexture)] as AnimateableTexture).MaxXFrame)
                                        {

                                            if (_frameCounter >= (_entities[entity].Components[typeof(AnimateableTexture)] as AnimateableTexture).SwitchFrame)
                                            {
                                                (_entities[entity].Components[typeof(AnimateableTexture)] as AnimateableTexture).CurrentXFrame += 1;

                                                _frameCounter = 0;

                                            }
                                        }
                                        if ((_entities[entity].Components[typeof(AnimateableTexture)] as AnimateableTexture).CurrentXFrame ==
                                                    (_entities[entity].Components[typeof(AnimateableTexture)] as AnimateableTexture).MaxXFrame)
                                        {
                                            (_entities[entity].Components[typeof(AnimateableTexture)] as AnimateableTexture).CurrentXFrame = 3;
                                        }

                                    }
                                }

                    }
                }
            }
            #endregion
        }
        #endregion

        #region IComponentReceiver
        public void ReceiveEntities(IDictionary<String, IInnerDictionary> pEntities)
        {
            if (_entities == null)
                _entities = new Dictionary<String, IInnerDictionary>();

            foreach (String key in pEntities.Keys)
                if (!_entities.ContainsKey(key))
                    _entities.Add(key, pEntities[key]);

        }
        #endregion

        #region IRender
        public SpriteBatch Draw(SpriteBatch spriteBatch) 
        {
            foreach (String entity in _entities.Keys)
            {
                // DRAW the SpriteAnimComponents texture variables
                spriteBatch.Draw((_entities[entity].Components[typeof(AnimateableTexture)] as AnimateableTexture).AnimateableTxtr,
                    // AT the Components PositionX and Y inside a rectangle whose width and height is specified by the SpriteAnimComponent
                    new Rectangle((int)(_entities[entity].Components[typeof(PositionX)] as PositionX).X,
                    (int)(int)(_entities[entity].Components[typeof(PositionY)] as PositionY).Y,
                    (int)(_entities[entity].Components[typeof(AnimateableTexture)] as AnimateableTexture).Width,
                    (int)(_entities[entity].Components[typeof(AnimateableTexture)] as AnimateableTexture).Height),
                    // the source rectangle is the current frame of the animation on the X and Y axis found in the SpriteAnimComponent 
                    new Rectangle((int)(_entities[entity].Components[typeof(AnimateableTexture)] as AnimateableTexture).CurrentXFrame *
                    (int)(_entities[entity].Components[typeof(AnimateableTexture)] as AnimateableTexture).Width,
                    // AND the width and height is also found in the sprite anim component
                    (int)(_entities[entity].Components[typeof(AnimateableTexture)] as AnimateableTexture).CurrentYFrame *
                    (int)(_entities[entity].Components[typeof(AnimateableTexture)] as AnimateableTexture).Height,
                    (int)(_entities[entity].Components[typeof(AnimateableTexture)] as AnimateableTexture).Width,
                    (int)(_entities[entity].Components[typeof(AnimateableTexture)] as AnimateableTexture).Height), Color.AntiqueWhite);
            }

            return spriteBatch;
        }
        #endregion
    }
}

