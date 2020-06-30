using ECSFramework.Components.Tranforms;
using ECSFramework.Interfaces.Generalnterfaces;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECSFramework.Interfaces.ToolInterfaces
{
    public interface IQuadTree<T> where T : PhysicsComponent
    {

        void NodeCollisions();

        void AddEntities(IDictionary<String, IInnerDictionary> entities);

        void CheckLevelCollisions(String entity, IComponent physicsComponent);

        IDictionary<String, IInnerDictionary> DynamicEntities { get; set; }

        IDictionary<String, IInnerDictionary> StaticEntities { get; set; }

        void Clear();

        void SubDivide();

        int NodeLevel { get; }

        Rectangle NodeRectangle { get; set; }

        void SetDelegate(CheckCollisions pCheckCollisions);
    }
}
