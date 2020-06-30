using System;
using ECSFramework.Components.GraphicComponents;
using ECSFramework.Interfaces.Generalnterfaces;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ECSFrameworkTestProj.ComponentTests.GraphicsComponentsTest
{
    [TestClass]
    public class TestStaticTextureComponent
    {
        /// <summary>
        /// TestMethod: StaticTextureComponentDefaults will be used to test default values
        /// </summary>
        [TestMethod]
        public void Test_StaticTextureComponentsDefaults()
        {
            // INITIALISE local StaticTexture for testing
            IComponent c = new StaticTexture();
            // Assert.AreEqual should return true
            // IF the the initial texture is null, and the rest of the
            // values are 0
            Assert.AreEqual(null, (c as StaticTexture).Texture);
            Assert.AreEqual(0, (c as StaticTexture).drawHeight);
            Assert.AreEqual(0, (c as StaticTexture).drawWidth);
        }

        /// <summary>
        /// TestMethod: StaticTextureComponentDefaults will be used to test default values
        /// </summary>
        [TestMethod]
        public void Test_StaticTextureComponentsValues()
        {
            // INITIALISE local StaticTexture for testing
            IComponent c = new StaticTexture();
            // SET the values of the draw height and width to 64
            // don't set or test the texture, as it will be play tested
            (c as StaticTexture).drawHeight = 64;
            (c as StaticTexture).drawWidth = 64;
            // Assert.AreEqual should return true
            // IF the the initial texture is null, and the rest of the
            // values are 0
            Assert.AreEqual(64, (c as StaticTexture).drawHeight);
            Assert.AreEqual(64, (c as StaticTexture).drawWidth);
        }
    }
}
