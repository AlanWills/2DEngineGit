using Microsoft.VisualStudio.TestTools.UnitTesting;
using CelesteEngineUnitTestFramework;

namespace CelesteEngineCelesteEngineUnitTestGameProject
{
    [TestClass]
    public class TestHighlightOnHoverModule : UnitTest
    {
        /*
        [TestMethod]
        public void TestHighlightOnHoverModuleLerp()
        {
            BaseObject baseObject = new BaseObject(Vector2.Zero, "");

            HighlightOnHoverModule module = baseObject.AddModule(new HighlightOnHoverModule(Color.Black, Color.White), true, true);

            Assert.IsNotNull(module);
            Assert.AreEqual(baseObject, module.AttachedComponent);
            Assert.AreEqual(baseObject, module.AttachedBaseObject);
            Assert.AreEqual(Color.Black, module.DefaultColour);
            Assert.AreEqual(Color.White, module.HighlightedColour);
            Assert.AreEqual(0.5, module.LerpAmount);
            Assert.AreEqual(BlendMode.kLerp, module.BlendMode);

            module.Update(0.16f);

            Assert.AreEqual(Color.Black, baseObject.Colour);  // We have not had our mouse over so our colour should be Color.Black
        }

        [TestMethod]
        public void TestFlashingObjectModuleReset()
        {
            BaseObject baseObject = new BaseObject(Vector2.Zero, "");

            HighlightOnHoverModule module = baseObject.AddModule(new HighlightOnHoverModule(Color.Black, Color.White, BlendMode.kBinary), true, true);

            Assert.IsNotNull(module);
            Assert.AreEqual(baseObject, module.AttachedComponent);
            Assert.AreEqual(baseObject, module.AttachedBaseObject);
            Assert.AreEqual(Color.Black, module.DefaultColour);
            Assert.AreEqual(Color.White, module.HighlightedColour);
            Assert.AreEqual(0.5, module.LerpAmount);
            Assert.AreEqual(BlendMode.kLerp, module.BlendMode);

            module.Update(0.16f);

            Assert.AreEqual(Color.Black, baseObject.Colour);  // We have not had our mouse over so our colour should be Color.Black
        }

        // Probably need to implement more tests with the actual mouse
        */
    }
}
