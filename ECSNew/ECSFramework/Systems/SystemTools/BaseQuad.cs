using ECSFramework.Components.Tranforms;
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
    public class BaseQuad : IECSUpdateable
    {
        private IList<IQuadTree<PhysicsComponent>> _quads;

        private IDictionary<String, IInnerDictionary> _overlappers;

        private int level;

        private Rectangle _baseRectangle;

        private CheckCollisions _checkCollisions;

        public BaseQuad(Rectangle BaseRectangle, CheckCollisions checkCollisions, Texture2D quadTexture) 
        {
            level = 1;

            _baseRectangle = BaseRectangle;

            _overlappers = new Dictionary<String, IInnerDictionary>();

            _checkCollisions = checkCollisions;

            Divide(quadTexture);
        }

        #region Private methods
        private void Divide(Texture2D quadTexture) 
        {
            int x, y, w, h;

            x = _baseRectangle.Left;

            y = _baseRectangle.Top;

            w = _baseRectangle.Width / 2;

            h = _baseRectangle.Height / 2;

            if (_quads == null)
                _quads = new List<IQuadTree<PhysicsComponent>>();

            _quads.Add(new QuadTree<PhysicsComponent>(new Rectangle(x, y, w, h), level + 1, _checkCollisions, quadTexture));
            _quads.Add(new QuadTree<PhysicsComponent>(new Rectangle(x + w, y, w, h), level + 1, _checkCollisions, quadTexture));
            _quads.Add(new QuadTree<PhysicsComponent>(new Rectangle(x, y + h, w, h), level + 1, _checkCollisions, quadTexture));
            _quads.Add(new QuadTree<PhysicsComponent>(new Rectangle(x + w, y + h, w, h), level + 1, _checkCollisions, quadTexture));
        }
        #endregion

        public void AddEntities(IDictionary<String, IInnerDictionary> entities)
        {
            _overlappers.Clear();

            IDictionary<String, IInnerDictionary> newDict;

            foreach (IQuadTree<PhysicsComponent> quad in _quads) 
            {
                newDict = new Dictionary<String, IInnerDictionary>();
                foreach (String entity in entities.Keys)
                {
                    if (quad.NodeRectangle.Contains((entities[entity].Components[typeof(PhysicsComponent)] as PhysicsComponent).BoundingBox))
                    {
                        newDict.Add(entity, entities[entity]);
                    }
                    else if (!_overlappers.ContainsKey(entity))
                    {
                        _overlappers.Add(entity, entities[entity]);
                    }
                }
                quad.AddEntities(newDict);
            }

            foreach (IQuadTree<PhysicsComponent> quad in _quads)
                quad.NodeCollisions();
        }

        #region IECSUpdateabe
        public void Update(GameTime gameTime) 
        {

        }
        #endregion

    }
}
