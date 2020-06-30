using ECSFramework.Components;
using ECSFramework.Components.GraphicComponents;
using ECSFramework.Components.Tranforms;
using ECSFramework.Components.Transforms;
using ECSFramework.Interfaces.Generalnterfaces;
using ECSFramework.Interfaces.SystemInterfaces.DataSystems;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECSFramework.Prefabs.EntityPrefabs
{
    public class Bullet : IPrefab
    {

        public Bullet() 
        {

        }

        public IList<IComponent> ReturnComponents(IComponentSystem componentSystem, ContentManager content, IList<IComponent> componentList) 
        {
            IComponent c;

            // POSITIONAL ELEMENTS
            c = componentSystem.CreateComponent<PositionX>();
            componentList.Add(c);
            c = componentSystem.CreateComponent<PositionY>();
            componentList.Add(c);
            c = componentSystem.CreateComponent<PositionZ>();
            componentList.Add(c);

            // PHYSICS
            c = componentSystem.CreateComponent<VelocityComponent>();
            (c as VelocityComponent).CurrentVelocity = 300;
            (c as VelocityComponent).MaxVelocity = 500;

            componentList.Add(c);
            c = componentSystem.CreateComponent<StaticTexture>();
            (c as StaticTexture).Texture = content.Load<Texture2D>("Bullet");
            (c as StaticTexture).drawWidth = 16;
            (c as StaticTexture).drawHeight = 16;
            componentList.Add(c);

            c = componentSystem.CreateComponent<PhysicsComponent>();
            (c as PhysicsComponent).DynamicEntity = true;
            componentList.Add(c);
            

            c = componentSystem.CreateComponent<BulletComponent>();
            componentList.Add(c);


            return componentList;
        }
    }
}
