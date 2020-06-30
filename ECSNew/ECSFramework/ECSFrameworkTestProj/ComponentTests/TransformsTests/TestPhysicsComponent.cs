using System;
using ECSFramework.Components.Tranforms;
using ECSFramework.Interfaces.Generalnterfaces;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.Xna.Framework;

namespace ECSFrameworkTestProj.ComponentTests.TransformsTests
{
    [TestClass]
    public class TestPhysicsComponent
    {
        /// <summary>
        /// METHOD: Test_PhysicsComponet, this method will test what the physics component values will
        /// initialise as
        /// </summary>
        [TestMethod]

        public void Test_PhysicsComponent()
        {
            // INSTANTIATE a new PhsyicsComponent
            IComponent c = new PhysicsComponent();
            // Assert.AreEqual returns true if DynamicEntity in component is 
            // defaulted to false
            Assert.AreEqual(false, (c as PhysicsComponent).DynamicEntity);
            // Assert.AreEqual is true if PhysicsComponent mass is null
            // on default val
            Assert.AreEqual(0, (c as PhysicsComponent).Mass);
            // Assert.AreEqual is true if Rectangle in component is equal to Rectangle.Empty
            Assert.AreEqual(Rectangle.Empty, (c as PhysicsComponent).BoundingBox);
        }

        /// <summary>
        /// METHOD: Test_PhysicsComponet, this method will test what the physics component values set and return correctly
        /// </summary>
        [TestMethod]

        public void Test_PhysicsComponentValues()
        {
            // INSTANTIATE a new PhsyicsComponent
            IComponent c = new PhysicsComponent();
            // SET c as PhysicsComponents DynamicEntity variable to true
            (c as PhysicsComponent).DynamicEntity = true;
            // SET c as PhysicsComponents Mass to 22.5 
            (c as PhysicsComponent).Mass = 22.5;
            // SET c as PhysicsComponents bounding box to new Rectangle x = 50, y = 100, width = 64, height = 64
            (c as PhysicsComponent).BoundingBox = new Rectangle(50, 100, 64, 64);
            // Assert.AreEqual returns true if DynamicEntity in component is 
            // defaulted to false
            Assert.AreEqual(true, (c as PhysicsComponent).DynamicEntity);
            // Assert.AreEqual is true if PhysicsComponent mass is null
            // on default val
            Assert.AreEqual(22.5, (c as PhysicsComponent).Mass);
            // Assert.AreEqual is true if Rectangle in component is equal to Rectangle.Empty
            Assert.AreEqual(new Rectangle(50, 100, 64, 64), (c as PhysicsComponent).BoundingBox);
        }
    }
}
