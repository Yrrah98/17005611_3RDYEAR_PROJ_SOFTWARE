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
    public class AIInvaderMovement : IECSUpdateable, IComponentReceiver, IRqdComponents, IEntityRemove, IEventListener, IPublisher
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
            typeof(VelocityComponent), typeof(AIMovementComponent) };
            }
        }
        /// <summary>
        /// PROPERTY: A property which contains a binary key for what entities this system will need
        /// </summary>
        public long RequiredKeys { get { return _requiredKeys; } set { if (_requiredKeys == 0) _requiredKeys = value; } }

        private long _requiredKeys;

        private EnqueueEvent _enqueue;

        public AIInvaderMovement() 
        {

        }

        #region IPublisher
        public void InjectEnqueue(EnqueueEvent enqueue) 
        {
            _enqueue = enqueue;
        }
        #endregion

        #region IEventListener
        public void HandleEvent(object source, EventArgs args) 
        {
            if (args is CollisionEventArgs) 
            {
                foreach (String s in (args as CollisionEventArgs).entities)
                    if (_entities.ContainsKey(s))
                    {
                        Console.WriteLine("y");
                        RemoveEntityArgs nArgs = new RemoveEntityArgs(s);

                        _enqueue(nArgs);
                    }
            }
        }
        #endregion

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
            foreach (String entity in _entities.Keys)
            {
                (_entities[entity].Components[typeof(PositionX)] as PositionX).X +=
                    (_entities[entity].Components[typeof(VelocityComponent)] as VelocityComponent).CurrentVelocity *
                    (_entities[entity].Components[typeof(VelocityComponent)] as VelocityComponent).Direction.X *
                    (float)gameTime.ElapsedGameTime.TotalSeconds;

                if ((_entities[entity].Components[typeof(PositionX)] as PositionX).X >= 800 - 32)
                {
                    FlipDirection(entity);
                }

                if ((_entities[entity].Components[typeof(PositionX)] as PositionX).X <= 0)
                {
                    FlipDirection(entity);
                }
            }
        }
        #endregion

        #region Private methods
        private void FlipDirection(String pEntity)
        {
            Vector2 flipV = new Vector2(-1, 0);

            foreach (String k in _entities.Keys)
            {
                (_entities[k].Components[typeof(VelocityComponent)] as VelocityComponent).Direction *= flipV;

                (_entities[k].Components[typeof(PositionY)] as PositionY).Y += 32;
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
