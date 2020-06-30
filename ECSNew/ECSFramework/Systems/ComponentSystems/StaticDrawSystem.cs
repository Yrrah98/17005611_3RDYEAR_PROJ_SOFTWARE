using ECSFramework.Args;
using ECSFramework.Components.GraphicComponents;
using ECSFramework.Components.Transforms;
using ECSFramework.Interfaces.Generalnterfaces;
using ECSFramework.Interfaces.SystemInterfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECSFramework.Systems.ComponentSystems
{
    public class StaticDrawSystem : IComponentReceiver, IECSUpdateable, IRender, IRqdComponents, IEventListener,
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
                return new List<Type>() { typeof(PositionX), typeof(PositionY), typeof(PositionZ),
                    typeof(StaticTexture) };
            }
        }
        /// <summary>
        /// PROPERTY: A property which contains a binary key for what entities this system will need
        /// </summary>
        public long RequiredKeys { get { return _requiredKeys; } set { if (_requiredKeys == 0) _requiredKeys = value; } }

        private long _requiredKeys;

        public StaticDrawSystem() 
        {

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

        #region IRender
        public SpriteBatch Draw(SpriteBatch spriteBatch)
        {
            if (_entities != null)
            {

                foreach (String entity in _entities.Keys)
                {
                    spriteBatch.Draw((_entities[entity].Components[typeof(StaticTexture)] as StaticTexture).Texture,
                        new Rectangle((int)(_entities[entity].Components[typeof(PositionX)] as PositionX).X,
                        (int)(_entities[entity].Components[typeof(PositionY)] as PositionY).Y,
                        (int)(_entities[entity].Components[typeof(StaticTexture)] as StaticTexture).drawWidth,
                        (int)(_entities[entity].Components[typeof(StaticTexture)] as StaticTexture).drawHeight), 
                        Color.AntiqueWhite);
                }
            }

            return spriteBatch;
        }
        #endregion
    }
}
