using ECSFramework.Components;
using ECSFramework.Components.GraphicComponents;
using ECSFramework.Components.Tranforms;
using ECSFramework.Components.Transforms;
using ECSFramework.Interfaces.Generalnterfaces;
using ECSFramework.Interfaces.SystemInterfaces;
using ECSFramework.Interfaces.SystemInterfaces.ComponentSystems;
using ECSFramework.Interfaces.SystemInterfaces.DataSystems;
using ECSFramework.Prefabs.EntityPrefabs;
using ECSFramework.Systems.ComponentSystems;
using ECSFramework.Systems.DataSystems;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECSFramework.Systems
{
    /// <summary>
    /// AUTHOR: Harry Jones
    /// VERSION: 0.2
    /// CLASS PURPOSE: The central system is where all the systems reside, from here
    /// entities are sent to their relevant systems 
    /// </summary>
    public class CentralSystem : ICentralSystem, IECSUpdateable, IRender
    {
        /// <summary>
        /// DECLARE a variable of type IList<IComponentReceiver> called _systemList
        /// </summary>
        private IList<IComponentReceiver> _systemList;
        // DECLARE IECSDatabase called _dataBase
        private IECSDatabase _dataBase;
        // DECLARE an ISystemRequirements as _systemRequirements
        private ISystemRequirements _systemRequirements;
        // DECLARE an IEntitySystem called _entitySystem
        private IEntitySystem _entitySystem;
        // DECLARE an IComponentSystem called _componentSystem
        private IComponentSystem _componentSystem;
        // DECLARE an IPublisher called _eventSystem
        private IEventSystem _eventSystem;
        // DECLARE an IPubliher called _inputSystem
        private IPublisher _inputSystem;
        // DECLARE a ContentManager called _content
        private ContentManager _content;
        // DECLARE a IRemoverSystem called _removerSystem
        private IRemoverSystem _removerSystem;

        /// <summary>
        /// CONSTRUCTOR for class CentralSystem
        /// </summary>
        public CentralSystem(ContentManager content)
        {
            _content = content;
        }

        #region IECSUpdateable
        public void Update(GameTime gameTime)
        {
            foreach (IComponentReceiver c in _systemList)
                if (c is IECSUpdateable)
                    (c as IECSUpdateable).Update(gameTime);

            (_inputSystem as IECSUpdateable).Update(gameTime);

            (_eventSystem as IECSUpdateable).Update(gameTime);
        }
        #endregion

        #region ICentralSystem

        public void ComponentReceiver(String entity, long componentKey)
        {
            // FOREACH through the list of required types of the system
            foreach (IRqdComponents r in _systemList)
            {
                // INITIALISE a new String called strBitKey, to store the entities component key 
                // bitmasked (&) with the required keys for the system
                String strBitKey = Convert.ToString((componentKey & r.RequiredKeys));
                // INITIALISE a new long variable called bitKey, parse it the strBitKey
                long bitKey = long.Parse(strBitKey);
                // IF the bitKey is equal to the systems required keys
                if (bitKey == r.RequiredKeys)
                {
                    // THEN INITIALISE a new local Dicitionary of key type String and value type IInnerDictionary
                    IDictionary<String, IInnerDictionary> e = new Dictionary<String, IInnerDictionary>();

                    IInnerDictionary rqdDictionary = new InnerDictionary();

                    // IF the entity contains a physics component, call a method to set it up
                    if (_dataBase.Entities[entity].Components.ContainsKey(typeof(PhysicsComponent)))
                    {
                        SetupBoundingBox(entity);
                    }
                    // FOREACH IComponent in the entities inner dictionary
                    foreach (IComponent c in _dataBase.Entities[entity].Components.Values)
                    {
                        if (!_systemRequirements.CompWithKey.ContainsKey(c.GetType()))
                            _systemRequirements.GenerateNewComponentKey(c.GetType());
                        // IF the bitkey found at the type of entity in the system requirements CompWithKey dictionary
                        // (bitwise) multiplied with the required system key has a value greater than zero
                        if ((_systemRequirements.CompWithKey[c.GetType()] & r.RequiredKeys) > 0)
                        {
                            // THEN that system requires that component
                            // ADD that components type as the key and add the component to the new inner dictionary
                            rqdDictionary.AddEntry(c.GetType(), c);
                        }
                    }
                    // ADD the entities key as the key and the InnerDictionary found at entities key in the database
                    e.Add(entity, rqdDictionary);
                    // CALL the ReceiveEntities method of the system and pass it the local dictionary
                    (r as IComponentReceiver).ReceiveEntities(e);
                }
            }
        }

        public void Initialise()
        {
            // INITIALISE local variables
            // SET _systemList as a new List
            _systemList = new List<IComponentReceiver>();
            // SET _systemRequirements as a new SystemRequirements
            _systemRequirements = new SystemRequirements();
            // SET _dataBase as a new Database, passing in the ComponentReceiver method into the constructor
            // as a ReceiveEntities delegate
            _dataBase = new ECSDatabase(_systemRequirements.CompWithKey, ComponentReceiver, _systemRequirements.GenerateNewComponentKey);
            // SET _eventSystem as a new EventSystem
            _eventSystem = new EventSystem();
            // SET _entitySystem as a new EntitySystem
            _entitySystem = new EntitySystem();
            // SET _componentSystem as a new ComponentSystem
            _componentSystem = new ComponentSystem();
            // SET _inputSystem as a new InputSystem
            _inputSystem = new InputSystem();
            (_inputSystem as IPublisher).InjectEnqueue(_eventSystem.AddToQueue);
            // SET _removerSystem as a new RemoverSystem
            _removerSystem = new RemoverSystem();
            _eventSystem.AddHandler((_removerSystem as IEventListener).HandleEvent);
            // ADD new InputMovementSystem  
            _systemList.Add(new InputMovementSystem());
            // ADD new HitPointSystem
            _systemList.Add(new HitPointSystem());
            // ADD new StaticDrawSystem
            _systemList.Add(new StaticDrawSystem());
            // ADD new AnimateableDrawSystem
            _systemList.Add(new AnimateableDrawSystem());
            // ADD new AIInvaderMovement 
            _systemList.Add(new AIInvaderMovement());
            // ADD new FactorySystem
            _systemList.Add(new FactorySystem());
            // ADD new BulletSystem
            _systemList.Add(new BulletSystem());
            // ADD new CollisionSystem
            _systemList.Add(new CollisionSystem(_content.Load<Texture2D>("GameEngRect")));

            foreach (IComponentReceiver system in _systemList)
            {
                if (system is IRqdComponents)
                    _systemRequirements.GenerateSystemRequirements((system as IRqdComponents));
                if (system is IRqdComponents)
                    (system as IRqdComponents).RequiredKeys = _systemRequirements.GenerateSystemRequirements((system as IRqdComponents));
                if (system is IPublisher)
                    (system as IPublisher).InjectEnqueue(_eventSystem.AddToQueue);
                if (system is IEventListener)
                    _eventSystem.AddHandler((system as IEventListener).HandleEvent);
                if (system is IFactorySystem)
                    (system as IFactorySystem).InjectSystems(_entitySystem, _componentSystem, _content, _dataBase.AddEntity);
                if (system is IEntityRemove)
                {
                    _removerSystem.InjectRemovers((system as IEntityRemove).RemoveEntity);
                }
            }
        }

        public void LoadContent(ILevel pLvl)
        {
            pLvl.LoadContent(_entitySystem, _componentSystem, _dataBase, _content);
        }

        #endregion

        #region Private methods
        /// <summary>
        /// METHOD: SetupBoundingBox, once the entities have been created a boundingbox needs to be created
        /// which references their position x and y components and their width and height data from there 
        /// graphical component
        /// </summary>
        /// <param name="entity"></param>
        private void SetupBoundingBox(String entity)
        {

            // IF the entity has a animateable texture component
            if(_dataBase.Entities[entity].Components.ContainsKey(typeof(AnimateableTexture)))
                (_dataBase.Entities[entity].Components[typeof(PhysicsComponent)] as PhysicsComponent).BoundingBox =
                new Rectangle((int)(_dataBase.Entities[entity].Components[typeof(PositionX)] as PositionX).X,
                (int)(_dataBase.Entities[entity].Components[typeof(PositionY)] as PositionY).Y,
                (int)(_dataBase.Entities[entity].Components[typeof(AnimateableTexture)] as AnimateableTexture).Width,
                (int)(_dataBase.Entities[entity].Components[typeof(AnimateableTexture)] as AnimateableTexture).Height);

            // IF the entity has a static draw texture component
            if (_dataBase.Entities[entity].Components.ContainsKey(typeof(StaticTexture)))
                (_dataBase.Entities[entity].Components[typeof(PhysicsComponent)] as PhysicsComponent).BoundingBox =
                new Rectangle((int)(_dataBase.Entities[entity].Components[typeof(PositionX)] as PositionX).X,
                (int)(_dataBase.Entities[entity].Components[typeof(PositionY)] as PositionY).Y,
                (int)(_dataBase.Entities[entity].Components[typeof(StaticTexture)] as StaticTexture).drawWidth,
                (int)(_dataBase.Entities[entity].Components[typeof(StaticTexture)] as StaticTexture).drawHeight);

        }
        #endregion

        #region IRender

        public SpriteBatch Draw(SpriteBatch spriteBatch) 
        {
            foreach (IComponentReceiver system in _systemList)
                if (system is IRender)
                    (system as IRender).Draw(spriteBatch);

            return spriteBatch;
        }
        #endregion
    }
}
