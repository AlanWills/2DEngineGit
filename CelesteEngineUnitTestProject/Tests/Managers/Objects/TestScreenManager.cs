using Microsoft.VisualStudio.TestTools.UnitTesting;
using CelesteEngineUnitTestFramework;

namespace CelesteEngineCelesteEngineUnitTestGameProject
{
    [TestClass]
    public class TestScreenManager : UnitTest
    {
        private MockScreenManager ScreenManager { get; set; }

        [ClassInitialize]
        public void SetupScreenManager(TestContext testContext)
        {
            ScreenManager = new MockScreenManager();
            ScreenManager.LoadContent();
            ScreenManager.Initialise();
            ScreenManager.Begin();
        }

        // Not sure these can be done for the moment.  Will need thinking about

        //[TestMethod]
        //public void TestGetCurrentScreenAs()
        //{
        //    MockBaseScreen mockBaseScreen = ScreenManager.AddChild(new MockBaseScreen(), true, true);
        //    ScreenManager.Update(0);

        //    Assert.AreEqual(mockBaseScreen, ScreenManager.GetCurrentScreenAs<MockBaseScreen>());
        //}

        //[TestMethod]
        //public void TestTransition()
        //{
        //    MockBaseScreen mockBaseScreen = ScreenManager.AddChild(new MockBaseScreen(), true, true);
        //    MockBaseScreen transitionScreen = new MockBaseScreen();

        //    ScreenManager.Update(0);
        //    Assert.AreEqual(mockBaseScreen, ScreenManager.CurrentScreen);

        //    ScreenManager.Transition(mockBaseScreen, transitionScreen);
        //    ScreenManager.Update(0);

        //    Assert.AreEqual(transitionScreen, ScreenManager.CurrentScreen);
        //}
    }
}
