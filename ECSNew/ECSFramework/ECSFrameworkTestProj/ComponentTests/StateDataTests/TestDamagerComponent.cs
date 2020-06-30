using System;
using ECSFramework.Components.StateData;
using ECSFramework.Interfaces.Generalnterfaces;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ECSFrameworkTestProj.ComponentTests.StateDataTests
{
    [TestClass]
    public class TestDamagerComponent
    {
        /// <summary>
        /// TestMethod: DamagerComponent, a method to test the default values of 
        /// a damagercomponent
        /// </summary>
        [TestMethod]
        public void Test_DamagerComponent()
        {
            // INITIALISE a local DamagerComponent
            IComponent c = new DamagerComponent();
            // Assert.AreEqual is true if the damage variables value is 
            // defualted to 0
            Assert.AreEqual(0, (c as DamagerComponent).Damage);
        }

        /// <summary>
        /// TestMethod: DamagerComponent, a method to test the default values of 
        /// a damagercomponent
        /// </summary>
        [TestMethod]
        public void Test_DamagerComponentValues()
        {
            // INITIALISE a local DamagerComponent
            IComponent c = new DamagerComponent();
            // SET damage value to 15
            (c as DamagerComponent).Damage = 15;
            // Assert.AreEqual is true if the damage variables value is 
            // 15
            Assert.AreEqual(15, (c as DamagerComponent).Damage);
        }

        /// <summary>
        /// TestMethod: DamagerComponent, a method to test whether the damge value
        /// is allowed to go below 0 or not. It should not be
        /// </summary>
        [TestMethod]
        public void Test_DamagerComponentValueBoundsBelow()
        {
            // INITIALISE a local DamagerComponent
            IComponent c = new DamagerComponent();
            // SET damage value to -15
            (c as DamagerComponent).Damage = -15;
            // Assert.IsFalse is correct if the value is not -15
            Assert.IsFalse((c as DamagerComponent).Damage == -15);
            // Assert.AreEqual is true if the damage variables value is 
            // -15
            Assert.AreEqual(0, (c as DamagerComponent).Damage);
        }
    }
}
