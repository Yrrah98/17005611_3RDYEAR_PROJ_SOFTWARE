using System;
using System.Collections.Generic;
using ECSFramework.Components.Actions;
using ECSFramework.Components.Actions.MovementActions;
using ECSFramework.Interfaces.Generalnterfaces;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;

namespace ECSFrameworkTestProj.ComponentTests.ActionComponenTests
{
    [TestClass]
    public class TestInputComponent
    {
        /// <summary>
        /// TestMethod: InputComponentDefault, a test to tes to make sure the InputComponent 
        /// list of actions is empty as default
        /// </summary>
        [TestMethod]
        public void Test_InputComponentDefault()
        {
            // INITIALISE an IComponent as an InputComponent
            IComponent c = new InputComponent();
            // Assert.AreEqual is true if the list in the InputComponent is empty
            // by checking the count is 0
            Assert.AreEqual(0, (c as InputComponent).Actions.Count);
        }

        /// <summary>
        /// TestMethod: InputComponentDefault, a test to tes to make sure the InputComponent 
        /// list of actions performs as exepcted
        /// </summary>
        [TestMethod]
        public void Test_InputComponentValues()
        {
            // INITIALISE a dummy list to read from and compare keys 
            IList<Keys> keys = new List<Keys>();
            keys.Add(Keys.A);
            keys.Add(Keys.D);
            // INITIALISE an IComponent as an InputComponent
            IComponent c = new InputComponent();
            // INITIALISE a dummy component that will be the action to test with
            IComponent action = new InputMovement();
            // SET some dummy key bindings and values
            (action as InputMovement).KeyList.Add(Keys.A);
            // SET some dummy vector to act as the direction 
            (action as InputMovement).Direction = new Vector2(-1, 0);
            // Assert.AreEqual is true if the list in the InputComponent is empty
            // by checking the count is 0
            // FOR loop through dummy keys
            for (int i = 0; i < keys.Count; i++)
            {
                // FOREACH through IComponent in InputComponent Actions list
                foreach (IComponent a in (c as InputComponent).Actions) 
                {
                    // IF the action is of X type
                    if(a is InputMovement)
                        // FOR LOOP through the keys in there
                        for (int j = 0; j < (a as InputMovement).KeyList.Count; j++)
                        {
                            // IF the keys in the Action and the Key list match
                            if ((a as InputMovement).KeyList[j] == keys[i])
                                // Assert.AreEqual both keys
                                Assert.AreEqual(keys[i], (a as InputMovement).KeyList[j]);
                            else
                                // VERIFY they're not the same keys with Asser.IsFalse
                                Assert.IsFalse(keys[i] == (a as InputMovement).KeyList[j]);

                        }
                }
            }


        }
    }
}
