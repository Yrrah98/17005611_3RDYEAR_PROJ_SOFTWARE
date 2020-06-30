using System;
using System.Collections.Generic;
using ECSFramework;
using ECSFramework.Args;
using ECSFramework.Interfaces.Generalnterfaces;
using ECSFramework.Interfaces.SystemInterfaces.DataSystems;
using ECSFramework.Systems.DataSystems;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.Xna.Framework;

namespace ECSFrameworkTestProj.SystemTests
{
    [TestClass]
    public class TestEventSystem
    {
        /// <summary>
        /// TestMethod: To test that handlers and events can be added/ queued as
        /// expected
        /// </summary>
        [TestMethod]
        public void Test_EventSystem()
        {
            // INITIALISE dummy EventSystem
            IEventSystem eventSystem = new EventSystem();
            // Add handler dummy method TestReceiver
            eventSystem.AddHandler(TestReceiver);
            // INITIALISE a list of dummy strings to go in the event args
            IList<String> dummyString = new List<String>();
            // ADD some string to the dummy string list
            dummyString.Add("dd");
            // INITIALISE a dummy CollisionEventArgs passing in the dummy list of strings
            CollisionEventArgs dummyArgs = new CollisionEventArgs(dummyString);
            // CALL AddToQueue on the dummyArgs class
            eventSystem.AddToQueue(dummyArgs);

            // INSTANTIATE a dummy GameTime variable
            var dummyGT = new GameTime();
            // CAST eventSystem as IECSUpdateable and call Update passing in the gameTime
            (eventSystem as IECSUpdateable).Update(dummyGT);
        }
        
        [TestMethod]
        private void TestReceiver(object source, EventArgs testArgs) 
        {
            // IF the EventArgs is CollisionEventArgs
            if (testArgs is CollisionEventArgs)
            {
                // THEN 
                // Assert.AreEqual check the type of the event, should be CollisionEventArgs
                Assert.AreEqual(typeof(CollisionEventArgs), testArgs.GetType());
                // Assert.AreEqual and check the first entry in the list of strings
                // should be "dd" or whatever string was put in
                Assert.AreEqual("dd", (testArgs as CollisionEventArgs).entities[0]);
            }
        }
    }
}
