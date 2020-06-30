using ECSFramework.Args;
using ECSFramework.Components.StateData;
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
    public class HitPointSystem : IComponentReceiver, IRqdComponents, IECSUpdateable, IPublisher, IEventListener,
        IEntityRemove
    {

        private IDictionary<String, IInnerDictionary> _entities;

        /// <summary>
        /// PROPERTY: A read only property which contains a list of the types this system requires in order to function
        /// </summary>
        public IReadOnlyList<Type> RqdComponents
        {
            get
            {
                return new List<Type>() { typeof(HitPoints) };
            }
        }
        /// <summary>
        /// PROPERTY: A property which contains a binary key for what entities this system will need
        /// </summary>
        public long RequiredKeys { get { return _requiredKeys; } set { if (_requiredKeys == 0) _requiredKeys = value; } }

        private long _requiredKeys;

        private EnqueueEvent _enqueue;

        public HitPointSystem() 
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
            if (pArgs is DamageArgs)
            {
                (_entities[((DamageArgs)pArgs).Entity].Components[typeof(HitPoints)] as HitPoints).CurrentHitPoints -= ((DamageArgs)pArgs).Damage;
            }

        }
        #endregion

        #region IPublisher
        public void InjectEnqueue(EnqueueEvent pEnqueue) 
        {
            _enqueue = pEnqueue;
        }
        #endregion 

        #region IECSUpdateable
        public void Update(GameTime gameTime) 
        {

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
