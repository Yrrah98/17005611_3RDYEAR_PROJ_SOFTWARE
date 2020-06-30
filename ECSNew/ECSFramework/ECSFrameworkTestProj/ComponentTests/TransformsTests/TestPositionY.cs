using System;
using ECSFramework.Components.Transforms;
using ECSFramework.Interfaces.Generalnterfaces;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ECSFrameworkTestProj.ComponentTests.TransformsTests
{
    [TestClass]
    public class TestPositionY
    {
        /// <summary>
        /// TEST METHOD
        /// </summary>
        [TestMethod]
        public void Test_PositionY()
        {
            IComponent c = new PositionY();
            (c as PositionY).Y = 20;
            float result = 20;
            Assert.AreEqual(result, (c as PositionY).Y);
        }

        [TestMethod]
        public void Test_PositionYAddition()
        {
            IComponent c = new PositionY();
            (c as PositionY).Y = 20;
            (c as PositionY).Y += 4;
            float result = 24;
            Assert.AreEqual(result, (c as PositionY).Y);
        }

        [TestMethod]
        public void Test_PositionYSubtraction()
        {
            IComponent c = new PositionY();
            (c as PositionY).Y = 20;
            (c as PositionY).Y -= 4;
            float result = 16;
            Assert.AreEqual(result, (c as PositionY).Y);
        }

        [TestMethod]
        public void Test_PositionYMultiplication()
        {
            IComponent c = new PositionY();
            (c as PositionY).Y = 20;
            (c as PositionY).Y *= 4;
            float result = 80;
            Assert.AreEqual(result, (c as PositionY).Y);
        }
    }
}
