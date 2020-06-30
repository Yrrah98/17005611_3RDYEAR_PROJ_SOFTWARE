using ECSFramework.Args;
using ECSFramework.Components.Actions;
using ECSFramework.Components.Actions.MovementActions;
using ECSFramework.Components.Tranforms;
using ECSFramework.Components.Transforms;
using ECSFramework.Interfaces.Generalnterfaces;
using ECSFramework.Interfaces.SystemInterfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECSFramework.Systems.ComponentSystems
{
    /// <summary>
    /// AUTHOR: Harry Jones
    /// VERSION: 0.2
    /// CLASS PURPOSE: The purpose of this class is to provide functionality for any entity which is controlled
    /// by the input of the player
    /// </summary>
    public class InputMovementSystem : IComponentReceiver, IRqdComponents, IECSUpdateable, IEventListener
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
            typeof(VelocityComponent), typeof(InputComponent) };
            }
        }
        /// <summary>
        /// PROPERTY: A property which contains a binary key for what entities this system will need
        /// </summary>
        public long RequiredKeys { get { return _requiredKeys; } set { if (_requiredKeys == 0) _requiredKeys = value; } }

        private long _requiredKeys;

        private KeyboardState _oldState;

        private KeyboardState _newState;
        /// <summary>
        /// CONSTRUCTOR for InputMovementSystem
        /// </summary>
        public InputMovementSystem() 
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
        public void HandleEvent(object pSource, EventArgs pArgs) 
        {
            // IF the EventArgs passed in is of type InputEventArgs
            if (pArgs is InputEventArgs)
            {
                if (_newState != (pArgs as InputEventArgs)._keyboardState)
                {
                    _oldState = _newState;

                    _newState = (pArgs as InputEventArgs)._keyboardState;
                }
            }

            if(pArgs is CollisionEventArgs)
                foreach(String s in (pArgs as CollisionEventArgs).entities)
                    if(_entities.ContainsKey(s))
                        Console.WriteLine("s");

        }
        #endregion

        #region IECSUpdateable
        public void Update(GameTime gameTime)
        {
            if (_newState == _oldState)
            {
                if (_entities != null)
                {
                    foreach (String entity in _entities.Keys)
                    {
                        IComponent inputComp = (_entities[entity].Components[typeof(InputComponent)] as InputComponent);
                        foreach (IComponent action in (inputComp as InputComponent).Actions)
                            if (action is InputMovement)
                            {
                                foreach (Keys key in (action as InputMovement).KeyList)
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
                                        (_entities[entity].Components[typeof(PositionX)] as PositionX).X += vel.X *
                                                (action as InputMovement).Direction.X *
                                                (float)gameTime.ElapsedGameTime.TotalSeconds;
                                        (_entities[entity].Components[typeof(PositionY)] as PositionY).Y += vel.Y *
                                                (action as InputMovement).Direction.Y *
                                                (float)gameTime.ElapsedGameTime.TotalSeconds;

                                    }
                                }
                            }

                    }
                }
            }
            else
                _oldState = _newState;
        }
        #endregion

        #region IComponentReceiver
        public void ReceiveEntities(IDictionary<String, IInnerDictionary> pEntities) 
        {
            if (_entities == null)
                _entities = new Dictionary<String, IInnerDictionary>();

            foreach (String key in pEntities.Keys)
                if (!_entities.ContainsKey(key))
                {
                    _entities.Add(key, pEntities[key]);
                }

        }
        #endregion
    }
}
