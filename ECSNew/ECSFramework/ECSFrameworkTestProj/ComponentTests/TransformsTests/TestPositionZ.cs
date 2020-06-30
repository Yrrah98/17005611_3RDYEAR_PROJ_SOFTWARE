using System;
using ECSFramework.Components.Transforms;
using ECSFramework.Interfaces.Generalnterfaces;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ECSFrameworkTestProj.ComponentTests.TransformsTests
{
    [TestClass]
    public class TestPositionZ
    {
        /// <summarZ>
        /// TEST METHOD
        /// </summarZ>
        [TestMethod]
        public void Test_PositionZ()
        {
            IComponent c = new PositionZ();
            (c as PositionZ).Z = 20;
            float result = 20;
            Assert.AreEqual(result, (c as PositionZ).Z);
        }

        [TestMethod]
        public void Test_PositionZAddition()
        {
            IComponent c = new PositionZ();
            (c as PositionZ).Z = 20;
            (c as PositionZ).Z += 4;
            float result = 24;
            Assert.AreEqual(result, (c as PositionZ).Z);
        }

        [TestMethod]
        public void Test_PositionZSubtraction()
        {
            IComponent c = new PositionZ();
            (c as PositionZ).Z = 20;
            (c as PositionZ).Z -= 4;
            float result = 16;
            Assert.AreEqual(result, (c as PositionZ).Z);
        }

        [TestMethod]
        public void Test_PositionZMultiplication()
        {
            IComponent c = new PositionZ();
            (c as PositionZ).Z = 20;
            (c as PositionZ).Z *= 4;
            float result = 80;
            Assert.AreEqual(result, (c as PositionZ).Z);
        }
    }
}
