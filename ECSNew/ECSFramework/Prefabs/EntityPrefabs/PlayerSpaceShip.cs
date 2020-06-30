using ECSFramework.Components.Actions;
using ECSFramework.Components.Actions.MovementActions;
using ECSFramework.Components.Actions.SpawnerComponents;
using ECSFramework.Components.GraphicComponents;
using ECSFramework.Components.StateData;
using ECSFramework.Components.Tranforms;
using ECSFramework.Components.Transforms;
using ECSFramework.Interfaces.Generalnterfaces;
using ECSFramework.Interfaces.SystemInterfaces.DataSystems;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECSFramework.Prefabs.EntityPrefabs
{
    public class PlayerSpaceShip : IPrefab
    {

        public PlayerSpaceShip() 
        {

        }

        public IList<IComponent> ReturnComponents(IComponentSystem componentSystem, ContentManager content, IList<IComponent> listToFill) 
        {

            IComponent component;

            IComponent inputComponent;



            component = componentSystem.CreateComponent<PositionX>();
            (component as PositionX).X = 400;
            listToFill.Add(component);
            component = componentSystem.CreateComponent<PositionY>();
            (component as PositionY).Y = 700;
            listToFill.Add(component);
            component = componentSystem.CreateComponent<PositionZ>();
            listToFill.Add(component);
            // INPUT COMPONENT SECTION
            inputComponent = componentSystem.CreateComponent<InputComponent>();
            // RIGHT MOVEMENT
            component = componentSystem.CreateComponent<InputMovement>();
            (component as InputMovement).KeyList.Add(Keys.D);
            (component as InputMovement).KeyList.Add(Keys.Right);
            (component as InputMovement).Direction = new Vector2(1, 0);
            (inputComponent as InputComponent).Actions.Add(component);
            // LEFT MOVEMENT
            component = componentSystem.CreateComponent<InputMovement>();
            (component as InputMovement).KeyList.Add(Keys.A);
            (component as InputMovement).KeyList.Add(Keys.Left);
            (component as InputMovement).Direction = new Vector2(-1, 0);
            (inputComponent as InputComponent).Actions.Add(component);


            listToFill.Add(inputComponent);

            component = componentSystem.CreateComponent<AnimateableTexture>();
            (component as AnimateableTexture).AnimateableTxtr = content.Load<Texture2D>("Spaceship_asset");
            (component as AnimateableTexture).Width = 64;
            (component as AnimateableTexture).Height = 64;
            (component as AnimateableTexture).SwitchFrame = 200;
            (component as AnimateableTexture).MaxXFrame = 4;
            (component as AnimateableTexture).MaxYFrame = 4;

            listToFill.Add(component);

            component = componentSystem.CreateComponent<VelocityComponent>();
            (component as VelocityComponent).CurrentVelocity = 100;
            (component as VelocityComponent).MaxVelocity = 150;
            listToFill.Add(component);

            component = componentSystem.CreateComponent<HitPoints>();
            listToFill.Add(component);

            component = componentSystem.CreateComponent<FactoryComponent>();
            (component as FactoryComponent)._factoryActions.Add(Keys.Space, new Bullet());
            listToFill.Add(component);

            component = componentSystem.CreateComponent<PhysicsComponent>();
            (component as PhysicsComponent).DynamicEntity = true;
            listToFill.Add(component);

            return listToFill;
        }
    }
}
