using CelesteEngine;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CelesteEngineUnitTestFramework;

namespace CelesteEngineCelesteEngineUnitTestGameProject
{
    [TestClass]
    public class TestFlashingObjectModule : UnitTest
    {
        [TestMethod]
        public void TestFlashingObjectModuleFlashFunction()
        {
            MockBaseObject baseObject = new MockBaseObject();

            FlashingObjectModule module = baseObject.AddModule(new FlashingObjectModule(0, 0.9f, 3), true, true);

            Assert.IsNotNull(module);
            Assert.AreEqual(baseObject, module.AttachedComponent);
            Assert.AreEqual(baseObject, module.AttachedBaseObject);
            Assert.AreEqual(0, module.MinOpacity);
            Assert.AreEqual(0.9f, module.MaxOpacity);
            Assert.AreEqual(3, module.LerpSpeed);
            Assert.IsTrue(module.FlashingOut);

            module.Update(0.16f);

            Assert.IsTrue(baseObject.Opacity < 0.9f);  // We have faded out so should definitely be less than 1
            Assert.IsTrue(baseObject.Opacity >= 0); // We should also always be greater than 0
        }

        [TestMethod]
        public void TestFlashingObjectModuleReset()
        {
            MockBaseObject baseObject = new MockBaseObject();

            FlashingObjectModule module = baseObject.AddModule(new FlashingObjectModule(0, 1, 3), true, true);
            
            module.Update(0.16f);

            Assert.IsTrue(baseObject.Opacity < module.MaxOpacity);  // We have faded out so should definitely be less than 1
            Assert.IsTrue(baseObject.Opacity >= module.MinOpacity); // We should also always be greater than 0
            Assert.IsTrue(module.FlashingOut);

            module.Reset();

            Assert.IsTrue(baseObject.Opacity == module.MaxOpacity);
            Assert.IsTrue(module.FlashingOut);
        }
    }
}
