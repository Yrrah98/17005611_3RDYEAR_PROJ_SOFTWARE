using ECSFramework.Components;
using ECSFramework.Components.GraphicComponents;
using ECSFramework.Components.Tranforms;
using ECSFramework.Components.Transforms;
using ECSFramework.Interfaces.Generalnterfaces;
using ECSFramework.Interfaces.SystemInterfaces.DataSystems;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECSFramework.Prefabs.EntityPrefabs
{
    public class Invader : IPrefab
    {

        public Invader() 
        {

        }

        public IList<IComponent> ReturnComponents(IComponentSystem componentSystem, ContentManager content, IList<IComponent> listToFill) 
        {

            IComponent c;

            c = componentSystem.CreateComponent<PositionX>();
            listToFill.Add(c);
            c = componentSystem.CreateComponent<PositionY>();
            listToFill.Add(c);
            c = componentSystem.CreateComponent<PositionZ>();
            listToFill.Add(c);
            c = componentSystem.CreateComponent<AIMovementComponent>();
            listToFill.Add(c);
            c = componentSystem.CreateComponent<VelocityComponent>();
            (c as VelocityComponent).CurrentVelocity = 32;
            (c as VelocityComponent).MaxVelocity = 40;
            (c as VelocityComponent).Direction = new Vector2(1, 0);
            listToFill.Add(c);
            c = componentSystem.CreateComponent<StaticTexture>();
            (c as StaticTexture).Texture = content.Load<Texture2D>("GhostlyGuide");
            (c as StaticTexture).drawWidth = (c as StaticTexture).Texture.Width;
            (c as StaticTexture).drawHeight = (c as StaticTexture).Texture.Height;
            listToFill.Add(c);
            c = componentSystem.CreateComponent<PhysicsComponent>();
            (c as PhysicsComponent).DynamicEntity = true;
            listToFill.Add(c);

            return listToFill;

        } 
    }
}
