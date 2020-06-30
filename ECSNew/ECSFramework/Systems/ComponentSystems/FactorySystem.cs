using ECSFramework.Args;
using ECSFramework.Components.Actions.SpawnerComponents;
using ECSFramework.Components.GraphicComponents;
using ECSFramework.Components.Tranforms;
using ECSFramework.Components.Transforms;
using ECSFramework.Interfaces.Generalnterfaces;
using ECSFramework.Interfaces.SystemInterfaces;
using ECSFramework.Interfaces.SystemInterfaces.ComponentSystems;
using ECSFramework.Interfaces.SystemInterfaces.DataSystems;
using ECSFramework.Prefabs;
using ECSFramework.Prefabs.EntityPrefabs;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECSFramework.Systems.ComponentSystems
{
    public class FactorySystem : IFactorySystem, IComponentReceiver, IEventListener, IPublisher, 
        IRqdComponents, IECSUpdateable, IEntityRemove
    {
        private IDictionary<String, IInnerDictionary> _entities;

        /// <summary>
        /// PROPERTY: A read only property which contains a list of the types this system requires in order to function
        /// </summary>
        public IReadOnlyList<Type> RqdComponents
        {
            get
            {
                return new List<Type>() { typeof(PositionX), typeof(PositionY), typeof(PositionZ), typeof(FactoryComponent) };
            }
        }
        /// <summary>
        /// PROPERTY: A property which contains a binary key for what entities this system will need
        /// </summary>
        public long RequiredKeys { get { return _requiredKeys; } set { if (_requiredKeys == 0) _requiredKeys = value; } }

        private long _requiredKeys;

        // DECLARE a variable of type IEntitySystem called _entitySystem to store a reference
        private IEntitySystem _entitySystem;
        // DECLARE a variable of type IComponentSystem called _componentSystem to store a reference
        private IComponentSystem _componentSystem;
        // DECLARE a variable of type AddToDatabase called _addToDatabase to store a delegate
        private AddToDatabase _addToDatabase;
        // DECLARE a variable of type EnqueuEvent to store a delegate, called _enqueue
        private EnqueueEvent _enqueue;
        // DECLARE a variable of type ContentManager called _content
        private ContentManager _content;

        private KeyboardState _oldState;

        private KeyboardState _newState;

        #region Bullet variables
        private int bulletSpawnDelay = 500;

        private int delay;
        #endregion

        /// <summary>
        /// CONSTRUCTOR for FactorySystem class
        /// </summary>
        public FactorySystem() 
        {

        }

        #region IEntityRemove
        public void RemoveEntity(String entity)
        {
            if (_entities.ContainsKey(entity))
                _entities.Remove(entity);
        }
        #endregion

        #region IECSUpdateable
        public void Update(GameTime gameTime) 
        {
            delay += (int)gameTime.ElapsedGameTime.TotalMilliseconds;

            if (_newState == _oldState)
            {
                if (_entities != null)
                {
                    foreach (String entity in _entities.Keys)
                    {
                        foreach (Keys key in (_entities[entity].Components[typeof(FactoryComponent)] as FactoryComponent)._factoryActions.Keys)
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
                                
                                if ((_entities[entity].Components[typeof(FactoryComponent)] as FactoryComponent)._factoryActions[key] is Bullet)
                                {
                                    if (delay >= bulletSpawnDelay)
                                    {
                                        MakeBullet((_entities[entity].Components[typeof(FactoryComponent)] as FactoryComponent)._factoryActions[key], entity);

                                        delay = 0;
                                    }

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

        #region Private Methods
        private void MakeBullet(IPrefab bullet, String spawner) 
        {
            PrefabSelector prefabSelector = new PrefabSelector();

            String e = _entitySystem.CreateNewEntity();

            IList<IComponent> compList = new List<IComponent>();

            IPrefab p = bullet;

            compList = prefabSelector.GetComponentSet(p, _componentSystem, _content);

            foreach (IComponent c in compList) 
            {
                if (c is PositionX)
                    (c as PositionX).X = (_entities[spawner].Components[typeof(PositionX)] as PositionX).X + 24;
                if (c is PositionY)
                    (c as PositionY).Y = (_entities[spawner].Components[typeof(PositionY)] as PositionY).Y - 16;
            }

            _addToDatabase(e, compList);
        }
        #endregion

        #region IPublisher
        public void InjectEnqueue(EnqueueEvent enqueue) 
        {
            _enqueue = enqueue;
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

        #region IFactorySystem
        public void InjectSystems(IEntitySystem entitySystem, IComponentSystem componentSystem, ContentManager content, AddToDatabase addEntity)
        {
            _entitySystem = entitySystem;

            _componentSystem = componentSystem;

            _addToDatabase = addEntity;

            _content = content;
        }
        #endregion 
    }
}
