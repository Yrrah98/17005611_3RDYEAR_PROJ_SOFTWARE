using System;
using ECSFramework.Components.Tranforms;
using ECSFramework.Interfaces.Generalnterfaces;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.Xna.Framework;

namespace ECSFrameworkTestProj.ComponentTests.TransformsTests
{
    [TestClass]
    public class TestVelocityComponent
    {
        /// <summary>
        /// TestMethod: Test_VelocityComponent a method which tests the default value of
        /// a VelocityComponent
        /// </summary>
        [TestMethod]
        public void Test_VelocityComponent()
        {
            // INITIALISE local VelocityComponent
            IComponent c = new VelocityComponent();
            // Assert.AreEqual true if MaxVelocity is defaulted as 0
            Assert.AreEqual(0, (c as VelocityComponent).MaxVelocity);
            // Assert.AreEqual true if CurrentVelocity is defaulted to 0
            Assert.AreEqual(0, (c as VelocityComponent).CurrentVelocity);
            // Assert.AreEqual true if Direction is (0,0)
            Assert.AreEqual(Vector2.Zero, (c as VelocityComponent).Direction);
        }

        /// <summary>
        /// TestMethod: Test_VelocityComponent a method which tests whether
        /// the values of VelocityComponent set and return as intended
        /// </summary>
        [TestMethod]
        public void Test_VelocityComponentValues()
        {
            // INITIALISE local VelocityComponent
            IComponent c = new VelocityComponent();
            // SET MaxVelocity of VelocityComponent to 500
            (c as VelocityComponent).MaxVelocity = 500;
            // SET CurrentVelocity of VelocityComponent to 250
            (c as VelocityComponent).CurrentVelocity = 250;
            // SET Direction of VelocityComponent to -1,1
            (c as VelocityComponent).Direction = new Vector2(-1, 1);
            // Assert.AreEqual true if MaxVelocity is returning 500
            Assert.AreEqual(500, (c as VelocityComponent).MaxVelocity);
            // Assert.AreEqual true if CurrentVelocity is returning 250
            Assert.AreEqual(250, (c as VelocityComponent).CurrentVelocity);
            // Assert.AreEqual true if Direction.X is -1
            Assert.AreEqual(-1, (c as VelocityComponent).Direction.X);
            // Assert.AreEqual true if Direction.Y is 1
            Assert.AreEqual(1, (c as VelocityComponent).Direction.Y);
        }
    }
}
