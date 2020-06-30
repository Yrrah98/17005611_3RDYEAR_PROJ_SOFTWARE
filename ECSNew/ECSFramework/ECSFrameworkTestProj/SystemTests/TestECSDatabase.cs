using System;
using System.Collections.Generic;
using ECSFramework.Components.Transforms;
using ECSFramework.Interfaces.Generalnterfaces;
using ECSFramework.Interfaces.SystemInterfaces.DataSystems;
using ECSFramework.Systems.DataSystems;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ECSFrameworkTestProj.SystemTests
{
    /// <summary>
    /// AUTHOR: Harry Jones
    /// CLASS PURPOSE: The purpose of this test class is to test the ECSDatabase class
    /// VERSION: 0.1
    /// TestDate: 27/07/2020
    /// </summary>
    [TestClass]
    public class TestECSDatabase
    {
        [TestMethod]
        public void Test_ECSDatabaseAddDatabase()
        {
            // INSTANTIATE an instance of the SystemRequirements as some of 
            // methods of the class are required for ECSDatabase to function
            ISystemRequirements rqmnts = new SystemRequirements();

            // INSTANTIATE an instance of the ECSDatabae class for testing purposes
            IECSDatabase database = new ECSDatabase(rqmnts.CompWithKey, MockGiveEntities, rqmnts.GenerateNewComponentKey);

            // INSTANTIATE a mock list of components
            IList<IComponent> mockCompList = new List<IComponent>();

            // ADD some components to list, any will do
            mockCompList.Add(new PositionX());
            mockCompList.Add(new PositionY());
            mockCompList.Add(new PositionZ());
            // ADD an entity and a list of components to the databse
            database.AddEntity("e1", mockCompList);
            Assert.IsTrue(database.Entities.ContainsKey("e1"));


        }

        /// <summary>
        /// TestMethod: This method will test what happens when a string which doesnt exist
        /// in the entity database is requested
        /// </summary>
        [TestMethod]
        public void Test_ECSDatabaseEntitiesAccess()
        {
            // INSTANTIATE an instance of the SystemRequirements as some of 
            // methods of the class are required for ECSDatabase to function
            ISystemRequirements rqmnts = new SystemRequirements();

            // INSTANTIATE an instance of the ECSDatabae class for testing purposes
            IECSDatabase database = new ECSDatabase(rqmnts.CompWithKey, MockGiveEntities, rqmnts.GenerateNewComponentKey);

            // Assert.ThrowsException - type NullReference exception
            // this statement is true if a KeyNotFoundException is thrown when we try to access an entity
            // which doesn't exist
            Assert.ThrowsException<KeyNotFoundException>(() => database.Entities["none-existent entity"]);


        }

        /// <summary>
        /// MOCKMETHOD: Mock method used for testing purposes of the ECSDatabase class
        /// </summary>
        /// <param name="mockEntity"></param>
        /// <param name="mockLong"></param>
        private void MockGiveEntities(string mockEntity, long mockLong) 
        {

        }
    }
}
