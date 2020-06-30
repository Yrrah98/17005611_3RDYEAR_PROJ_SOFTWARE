using System;
using ECSFramework.Components.Transforms;
using ECSFramework.Interfaces.Generalnterfaces;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ECSFrameworkTestProj.ComponentTests.TransformsTests
{
    [TestClass]
    public class TestPositionX
    {
        /// <summary>
        /// TEST METHOD
        /// </summary>
        [TestMethod]
        public void Test_PositionX()
        {
            IComponent c = new PositionX();
            (c as PositionX).X = 20;
            float result = 20;
            Assert.AreEqual(result, (c as PositionX).X);
        }

        [TestMethod]
        public void Test_PositionXAddition() 
        {
            IComponent c = new PositionX();
            (c as PositionX).X = 20;
            (c as PositionX).X += 4;
            float result = 24;
            Assert.AreEqual(result, (c as PositionX).X);
        }

        [TestMethod]
        public void Test_PositionXSubtraction()
        {
            IComponent c = new PositionX();
            (c as PositionX).X = 20;
            (c as PositionX).X -= 4;
            float result = 16;
            Assert.AreEqual(result, (c as PositionX).X);
        }

        [TestMethod]
        public void Test_PositionXMultiplication()
        {
            IComponent c = new PositionX();
            (c as PositionX).X = 20;
            (c as PositionX).X *= 4;
            float result = 80;
            Assert.AreEqual(result, (c as PositionX).X);
        }
    }
}
