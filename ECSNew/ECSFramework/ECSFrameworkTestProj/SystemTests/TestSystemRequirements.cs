using System;
using System.Collections.Generic;
using ECSFramework.Components.Actions;
using ECSFramework.Components.Actions.MovementActions;
using ECSFramework.Components.Actions.SpawnerComponents;
using ECSFramework.Components.GraphicComponents;
using ECSFramework.Components.StateData;
using ECSFramework.Components.Tranforms;
using ECSFramework.Components.Transforms;
using ECSFramework.CustomExceptions;
using ECSFramework.Interfaces.SystemInterfaces;
using ECSFramework.Interfaces.SystemInterfaces.DataSystems;
using ECSFramework.Systems.ComponentSystems;
using ECSFramework.Systems.DataSystems;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ECSFrameworkTestProj.SystemTests
{
    /// <summary>
    /// TEST CLASS: Test the public facing API of the SystemRequirements class
    /// to see if it functions as required
    /// </summary>
    [TestClass]
    public class TestSystemRequirements
    {
        /// <summary>
        /// TestMethod: This method will test the public GenerateNewComponentKey method of the SystemRequirements class
        /// </summary>
        [TestMethod]
        public void Test_SystemRequirementsComponentKeys1()
        {

            /*
             * TO DO - Test the comp keys with as many components as possible and see if values are as expected
             */
            ISystemRequirements systemRequirements = new SystemRequirements();
            // CALL GenerateNewComponentKey passing in type PositionX 
            systemRequirements.GenerateNewComponentKey(typeof(PositionX));
            // Asser.AreEqual should be true if the value found in the Compkey dictionary
            // at type PositionX is 1
            Assert.AreEqual(1, systemRequirements.CompWithKey[typeof(PositionX)]);
            // CALL GenerateNewComponentKey passing in type PositionY
            systemRequirements.GenerateNewComponentKey(typeof(PositionY));
            // Asser.AreEqual should be true if the value found in the Compkey dictionary
            // at type PositionY is 2
            Assert.AreEqual(2, systemRequirements.CompWithKey[typeof(PositionY)]);
            // CALL GenerateNewComponentKey passing in type PositionZ
            systemRequirements.GenerateNewComponentKey(typeof(PositionZ));
            // Asser.AreEqual should be true if the value found in the Compkey dictionary
            // at type PositionZ is 4
            Assert.AreEqual(4, systemRequirements.CompWithKey[typeof(PositionZ)]);
            // CALL GenerateNewComponentKey passing in type PhysicsComponent
            systemRequirements.GenerateNewComponentKey(typeof(PhysicsComponent));
            // Asser.AreEqual should be true if the value found in the Compkey dictionary
            // at type PhysicsComponent is 8
            Assert.AreEqual(8, systemRequirements.CompWithKey[typeof(PhysicsComponent)]);


        }

        /// <summary>
        /// TestMethod: This method will test the public GenerateNewComponentKey method of the SystemRequirements class
        /// to see if it throws the correct exception or not
        /// </summary>
        [TestMethod]
        public void Test_SystemRequirementsComponentKeysException()
        {

            ISystemRequirements systemRequirements = new SystemRequirements();
            // CALL GenerateNewComponentKey passing in type PositionX 
            systemRequirements.GenerateNewComponentKey(typeof(PositionX));
            // Assert.ThrowsException - type ArgumentException is true if the systemRequirements GenerateNewComponentKey
            // throws the ArgumentException when passing another type which is already contained
            Assert.ThrowsException<ArgumentException>(() => systemRequirements.GenerateNewComponentKey(typeof(PositionX)));
        }

        /// <summary>
        /// TestMethod: This method will test the public GenerateNewComponentKey method of the SystemRequirements class
        /// to see if it throws the correct exception or not
        /// </summary>
        [TestMethod]
        public void Test_SystemRequirementsComponentKeyException2()
        {
            // INSTANTIATE systemRequirements 
            ISystemRequirements systemRequirements = new SystemRequirements();
            // INSTANTIATE an IRqdComponents and make a dummy IRqdComponents class
            IRqdComponents dummySystem = new AIInvaderMovement();
            // INSTANTIATE a long as 0, used in AreEqual assertion
            long val = 0;
            /*
             * The following statement will be used to calculate the expected key which 
             * an entity and its components must satisfy in order for it to be sent 
             * to a system. In the case of the AIInvaderMovement class, the expected result is 31. 
             */
            // FORLOOP through the count of components 
            for (int i = 0; i < dummySystem.RqdComponents.Count; i++)
                // ADD 1 shifted 1 place to the left by the value of i
                val += 1 << i;
            // CALL to GenerateSystemRequirements method, passing in the dummySystem
            Assert.AreEqual(val, systemRequirements.GenerateSystemRequirements(dummySystem));
            // Now to test whether the value is correctly set in the dummy system
            // SET the dummySystems RequiredKeys variable to the returned value of systemRequirements GenerateSystemRequirements
            dummySystem.RequiredKeys = systemRequirements.GenerateSystemRequirements(dummySystem);
            // AIInvaderMovement required key value is/should be 31 or 11111
            Assert.AreEqual(31, dummySystem.RequiredKeys);
            Assert.AreEqual(0b_11111, dummySystem.RequiredKeys);
           
        }
    }
}
