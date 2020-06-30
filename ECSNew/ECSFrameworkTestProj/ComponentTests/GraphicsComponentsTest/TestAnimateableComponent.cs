using System;
using ECSFramework.Components.GraphicComponents;
using ECSFramework.Interfaces.Generalnterfaces;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna;
using Microsoft.Xna.Framework;
using ECSFramework;
using System.ComponentModel.Design;
using System.Windows.Forms;

namespace ECSFrameworkTestProj.ComponentTests.GraphicsComponentsTest
{
    [TestClass]
    public class TestAnimateableComponent
    {
        /// <summary>
        /// TestMethod: Tests the default values of the AnimateableTexture component
        /// </summary>
        [TestMethod]
        public void Test_AnimateableComponentDefaults()
        {
            // INITIALISE local IComponent as AnimateableTexture component
            IComponent c = new AnimateableTexture();
            // Assert.AreEqual is true if the texture is null as default
            Assert.AreEqual(null, (c as AnimateableTexture).AnimateableTxtr);
            // Assert.AreEqual is true if the MaxXFrame, MaxYFrame, CurrentXFrame,
            // Height, Width and CurrentYFrame and SwitchFrame are all 0 as default
            Assert.AreEqual(0, (c as AnimateableTexture).MaxXFrame);
            Assert.AreEqual(0, (c as AnimateableTexture).MaxYFrame);
            Assert.AreEqual(0, (c as AnimateableTexture).CurrentXFrame);
            Assert.AreEqual(0, (c as AnimateableTexture).CurrentYFrame);
            Assert.AreEqual(0, (c as AnimateableTexture).Width);
            Assert.AreEqual(0, (c as AnimateableTexture).Height);
            Assert.AreEqual(0, (c as AnimateableTexture).SwitchFrame);

        }

        /// <summary>
        /// TestMethod: Tests whether the values set and return as they should,
        /// Texture variable of the component will not be tested as it can be play tested 
        /// </summary>
        [TestMethod]
        public void Test_AnimateableComponentValues()
        {
            

            // INITIALISE local IComponent as AnimateableTexture component
            IComponent c = new AnimateableTexture();
            // SET the components, MaxXFrame, MaxYFrame, CurrentXFrame, CurrentYFrame,
            // Width, Height and SwitchFrame to:
            // 4, 4, 2, 2, 64, 64 and 200 respectively
            (c as AnimateableTexture).MaxXFrame = 4;
            (c as AnimateableTexture).MaxYFrame = 4;
            (c as AnimateableTexture).CurrentXFrame = 2;
            (c as AnimateableTexture).CurrentYFrame = 2;
            (c as AnimateableTexture).Width = 64;
            (c as AnimateableTexture).Height = 64;
            (c as AnimateableTexture).SwitchFrame = 200;
            // Assert.AreEqual is true if the MaxXFrame, MaxYFrame, CurrentXFrame,
            // Height, Width and CurrentYFrame and SwitchFrame are all 0 as default
            Assert.AreEqual(4, (c as AnimateableTexture).MaxXFrame);
            Assert.AreEqual(4, (c as AnimateableTexture).MaxYFrame);
            Assert.AreEqual(2, (c as AnimateableTexture).CurrentXFrame);
            Assert.AreEqual(2, (c as AnimateableTexture).CurrentYFrame);
            Assert.AreEqual(64, (c as AnimateableTexture).Width);
            Assert.AreEqual(64, (c as AnimateableTexture).Height);
            Assert.AreEqual(200, (c as AnimateableTexture).SwitchFrame);

        }
    }
}
