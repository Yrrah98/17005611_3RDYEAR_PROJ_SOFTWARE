using ECSFramework.Components.Tranforms;
using ECSFramework.Components.Transforms;
using ECSFramework.Interfaces.Generalnterfaces;
using ECSFramework.Interfaces.ToolInterfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECSFramework.Systems.SystemTools
{
    public class QuadTree<T> : IQuadTree<T> where T : PhysicsComponent
    {
        public IDictionary<string, IInnerDictionary> DynamicEntities { get; set; }
        public IDictionary<string, IInnerDictionary> StaticEntities { get; set; }

        private const int MAX_ENTITIES = 4;

        private const int MAX_LEVELS = 8;

        public int NodeLevel { get; }

        public Rectangle NodeRectangle { get; set; }

        private IList<IQuadTree<PhysicsComponent>> _quads;

        private CheckCollisions _checkCollisions;

        public IList<String> DynamicKeys { get; private set; }

        private Texture2D _quadTxt;

        public QuadTree(Rectangle quadArea, int level, CheckCollisions checkCollisions, Texture2D quadText) 
        {
            this.NodeLevel = level;

            this.NodeRectangle = quadArea;

            this._checkCollisions = checkCollisions;

            DynamicKeys = new List<String>();

            DynamicEntities = new Dictionary<String, IInnerDictionary>();

            StaticEntities = new Dictionary<String, IInnerDictionary>();

            _quadTxt = quadText;
        }

        public void AddEntities(IDictionary<String, IInnerDictionary> entities)
        {

            // CALL to Clear method
            Clear();

            // IF the entity dicitonaries are null
            if (DynamicEntities == null)
            {
                // THEN
                // INSTANTIATE the list of entities 
                DynamicEntities = new Dictionary<String, IInnerDictionary>();

                StaticEntities = new Dictionary<String, IInnerDictionary>();
            }

            foreach (String entity in entities.Keys) 
            {
                if ((entities[entity].Components[typeof(PhysicsComponent)] as PhysicsComponent).DynamicEntity)
                {
                    DynamicEntities.Add(entity, entities[entity]);
                    if (DynamicKeys == null)
                        DynamicKeys = new List<String>();
                    DynamicKeys.Add(entity);
                }                
                if (!(entities[entity].Components[typeof(PhysicsComponent)] as PhysicsComponent).DynamicEntity)
                    StaticEntities.Add(entity, entities[entity]);
            }

            // IF the list of entities in this quad is greater than the MAX_ENTITIES
            if (this.DynamicEntities.Count + StaticEntities.Count >= MAX_ENTITIES && this.NodeLevel < MAX_LEVELS)
            {
                // THEN
                // CALL SubDivide in this quad
                SubDivide();
            }
        }

        public void Clear()
        {
            if (DynamicKeys != null)
                DynamicKeys.Clear();

            if (DynamicEntities != null)
                DynamicEntities.Clear();

            if (StaticEntities != null)
                StaticEntities.Clear();

            if (_quads != null)
            {
                foreach (IQuadTree<T> quad in _quads)
                    quad.Clear();
                _quads.Clear();

            }
        }

        public void NodeCollisions()
        {

            for (int i = 0; i < DynamicKeys.Count; i++)
            {
                String entity1 = DynamicKeys[i];
                for (int j = i + 1; j < DynamicKeys.Count - 1; j++) 
                {
                    String entity2 = DynamicKeys[j];
                    if ((DynamicEntities[entity1].Components[typeof(PhysicsComponent)] as PhysicsComponent).BoundingBox.Intersects(
                        (DynamicEntities[entity2].Components[typeof(PhysicsComponent)] as PhysicsComponent).BoundingBox))
                    {
                        _checkCollisions(entity1, entity2);
                    }
                }
                if (_quads != null)
                    foreach (IQuadTree<T> quad in _quads)
                        foreach (String e in quad.DynamicEntities.Keys)
                            if ((DynamicEntities[entity1].Components[typeof(PhysicsComponent)] as PhysicsComponent).BoundingBox.Intersects(
                                (quad.DynamicEntities[e].Components[typeof(PhysicsComponent)] as PhysicsComponent).BoundingBox))
                                _checkCollisions(entity1, e);

            }

            // IF _quads is not null
            if (_quads != null)
                // THEN FOREACH through the list of quads
                foreach (IQuadTree<T> q in _quads)
                    // CALL to NodeCollisions method in each quad
                    q.NodeCollisions();

        }

        public void CheckLevelCollisions(String entity, IComponent physicsComp) 
        {
            
        }

        public void SetDelegate(CheckCollisions pCheckCollisions)
        {
            _checkCollisions = pCheckCollisions;
        }

        public void SubDivide()
        {
            if (_quads == null)
            {
                _quads = new List<IQuadTree<PhysicsComponent>>();

                int x, y, w, h;

                x = NodeRectangle.Left;
                y = NodeRectangle.Top;
                w = NodeRectangle.Width / 2;
                h = NodeRectangle.Height / 2;

                _quads.Add(new QuadTree<PhysicsComponent>(new Rectangle(x, y, w, h), NodeLevel + 1, _checkCollisions, _quadTxt));
                _quads.Add(new QuadTree<PhysicsComponent>(new Rectangle(x + w, y, w, h), NodeLevel + 1, _checkCollisions, _quadTxt));
                _quads.Add(new QuadTree<PhysicsComponent>(new Rectangle(x, y + h, w, h), NodeLevel + 1, _checkCollisions, _quadTxt));
                _quads.Add(new QuadTree<PhysicsComponent>(new Rectangle(x + w, y + h, w, h), NodeLevel + 1, _checkCollisions, _quadTxt));

                foreach (IQuadTree<T> quad in _quads)
                    quad.SetDelegate(_checkCollisions);
            }

            List<String> entitiesAdded = new List<String>();

            IDictionary<String, IInnerDictionary> d = new Dictionary<String, IInnerDictionary>();


            foreach (String entity in DynamicEntities.Keys)
                foreach(IQuadTree<T> quad in _quads)
                    if (quad.NodeRectangle.Contains((DynamicEntities[entity].Components[typeof(PhysicsComponent)] as PhysicsComponent).BoundingBox))
                    {

                        d.Add(entity, DynamicEntities[entity]);

                        if (DynamicKeys.Contains(entity))
                            DynamicKeys.Remove(entity);

                        entitiesAdded.Add(entity);
                    }
            foreach (String entity in StaticEntities.Keys)
                foreach(IQuadTree<T> quad in _quads)
                    if (quad.NodeRectangle.Contains((StaticEntities[entity].Components[typeof(PhysicsComponent)] as PhysicsComponent).BoundingBox))
                    {
                        d.Add(entity, StaticEntities[entity]);

                        entitiesAdded.Add(entity);
                    }

            foreach (IQuadTree<T> quad in _quads)
            {
                quad.AddEntities(d);
            }

        }
    }
}
