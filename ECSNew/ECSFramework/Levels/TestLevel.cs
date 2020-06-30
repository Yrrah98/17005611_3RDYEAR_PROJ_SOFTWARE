using ECSFramework.Components.Actions;
using ECSFramework.Components.Actions.MovementActions;
using ECSFramework.Components.GraphicComponents;
using ECSFramework.Components.StateData;
using ECSFramework.Components.Tranforms;
using ECSFramework.Components.Transforms;
using ECSFramework.Interfaces.Generalnterfaces;
using ECSFramework.Interfaces.SystemInterfaces.DataSystems;
using ECSFramework.Prefabs;
using ECSFramework.Prefabs.EntityPrefabs;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECSFramework.Levels
{
    public class TestLevel : ILevel
    {

        public TestLevel()
        {

        }

        public void LoadContent(IEntitySystem es, IComponentSystem cs, IECSDatabase db, ContentManager cm)
        {
            String entity;

            IList<IComponent> componentList = new List<IComponent>();

            PrefabSelector prefabSelector = new PrefabSelector();

            /*
             * TO DO : 15/06/2020
             * ENTITIES BOUNDING BOX NOT SETTING UP PROPERLY 
             * NO COLLISIONS ARE WORKING
             * ASSESS ENTITY SET UP OF Bridge work
             * 
             */
            entity = es.CreateNewEntity();
            componentList = prefabSelector.GetComponentSet<PlayerSpaceShip>(cs, cm);

            db.AddEntity(entity, componentList);

            componentList.Clear();

            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 11; j++)
                {
                    componentList.Clear();

                    entity = es.CreateNewEntity();
                    componentList = prefabSelector.GetComponentSet<Invader>(cs, cm);

                    foreach (IComponent c in componentList) 
                    {
                        if (c is PositionX)
                            (c as PositionX).X = ((32 * j) + (32 * j)) + 64;
                        if (c is PositionY)
                            (c as PositionY).Y = 32 * i;
                    }
                    
                    db.AddEntity(entity, componentList);


                }
            }

        }
    }
}
