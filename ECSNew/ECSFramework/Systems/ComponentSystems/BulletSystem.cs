using ECSFramework.Args;
using ECSFramework.Components;
using ECSFramework.Components.Tranforms;
using ECSFramework.Components.Transforms;
using ECSFramework.Interfaces.Generalnterfaces;
using ECSFramework.Interfaces.SystemInterfaces;
using ECSFramework.Interfaces.SystemInterfaces.DataSystems;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECSFramework.Systems.ComponentSystems
{
    public class BulletSystem : IComponentReceiver, IECSUpdateable, IRqdComponents, IEventListener,
        IPublisher, IEntityRemove
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
                    typeof(BulletComponent) };
            }
        }
        /// <summary>
        /// PROPERTY: A property which contains a binary key for what entities this system will need
        /// </summary>
        public long RequiredKeys { get { return _requiredKeys; } set { if (_requiredKeys == 0) _requiredKeys = value; } }

        private long _requiredKeys;

        private EnqueueEvent _enqueue;

        public BulletSystem() 
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

            if (args is CollisionEventArgs)
            {
#pragma warning disable CS0219 // The variable 'num' is assigned but its value is never used
                int num = 0;
#pragma warning restore CS0219 // The variable 'num' is assigned but its value is never used
                if (_entities.ContainsKey((args as CollisionEventArgs).entities[0]) && !_entities.ContainsKey((args as CollisionEventArgs).entities[1])
                    || _entities.ContainsKey((args as CollisionEventArgs).entities[1]) && !_entities.ContainsKey((args as CollisionEventArgs).entities[0]))
                {
                    if (_entities.ContainsKey((args as CollisionEventArgs).entities[0])) 
                    {
                        RemoveEntityArgs nArgs = new RemoveEntityArgs((args as CollisionEventArgs).entities[0]);

                        _enqueue(nArgs);
                    }
                    if (_entities.ContainsKey((args as CollisionEventArgs).entities[1])) 
                    {
                        RemoveEntityArgs nArgs = new RemoveEntityArgs((args as CollisionEventArgs).entities[1]);

                        _enqueue(nArgs);
                    }
                }
            }
        }
        #endregion

        #region IPublisher
        public void InjectEnqueue(EnqueueEvent enqueue) 
        {
            _enqueue = enqueue;
        }
        #endregion

        #region IECSUpdateable
        public void Update(GameTime gameTime)
        {
            if (_entities != null) 
            {
                String eToRemove = null;

                foreach (String entity in _entities.Keys) 
                {
                    (_entities[entity].Components[typeof(PositionY)] as PositionY).Y -= (float)(240 * gameTime.ElapsedGameTime.TotalSeconds);
                    if ((_entities[entity].Components[typeof(PositionY)] as PositionY).Y <= 0)
                    {
                        eToRemove = entity;
                    }
                }

                if (eToRemove != null)
                {
                    RemoveEntityArgs args = new RemoveEntityArgs(eToRemove);

                    _enqueue(args);
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
    }
}
