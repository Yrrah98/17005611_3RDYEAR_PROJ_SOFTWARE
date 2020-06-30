using ECSFramework.Args;
using ECSFramework.Components.Actions.SpawnerComponents;
using ECSFramework.Components.Tranforms;
using ECSFramework.Components.Transforms;
using ECSFramework.Interfaces.Generalnterfaces;
using ECSFramework.Interfaces.SystemInterfaces;
using ECSFramework.Interfaces.SystemInterfaces.DataSystems;
using ECSFramework.Systems.SystemTools;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECSFramework.Systems.ComponentSystems
{
    public class CollisionSystem : IComponentReceiver, IPublisher, IRqdComponents, IECSUpdateable,
        IEventListener, IEntityRemove
    {
        private IDictionary<String, IInnerDictionary> _entities;

        /// <summary>
        /// PROPERTY: A read only property which contains a list of the types this system requires in order to function
        /// </summary>
        public IReadOnlyList<Type> RqdComponents
        {
            get
            {
                return new List<Type>() { typeof(PhysicsComponent) };
            }
        }
        /// <summary>
        /// PROPERTY: A property which contains a binary key for what entities this system will need
        /// </summary>
        public long RequiredKeys { get { return _requiredKeys; } set { if (_requiredKeys == 0) _requiredKeys = value; } }

        private long _requiredKeys;

        private BaseQuad _baseQuad;

        private EnqueueEvent _enqueue;
        public CollisionSystem(Texture2D quadTexture)
        {
            _baseQuad = new BaseQuad(new Rectangle(0, 0, 800, 800), CheckCollision, quadTexture);
        }

        #region IEventListener
        public void HandleEvent(object source, EventArgs args)
        {
        }
        #endregion

        #region IEntityRemove
        public void RemoveEntity(String entity)
        {
            if (_entities.ContainsKey(entity))
                _entities.Remove(entity);
        }
        #endregion

        #region IPublisher
        public void InjectEnqueue(EnqueueEvent enqueue)
        {
            _enqueue = enqueue;
        }
        #endregion

        #region Private Methods
        private void CheckCollision(String e1, String e2)
        {
            IList<String> a = new List<String>();

            a.Add(e1);
            a.Add(e2);

            CollisionEventArgs args = new CollisionEventArgs(a);

            _enqueue(args);
        }
        #endregion

        #region IECSUpdateable
        public void Update(GameTime gameTime)
        {
            if (_entities != null)
                _baseQuad.AddEntities(_entities);

        }
        #endregion

        #region IComponentReceiver
        public void ReceiveEntities(IDictionary<String, IInnerDictionary> pEntities)
        {
            if (_entities == null)
                _entities = new Dictionary<String, IInnerDictionary>();

            foreach (String key in pEntities.Keys)
            {
                if (!_entities.ContainsKey(key))
                    _entities.Add(key, pEntities[key]);
            }
        }
        #endregion

    }
}
