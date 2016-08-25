using CelesteEngine;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CelesteEngineUnitTestFramework;

namespace CelesteEngineCelesteEngineUnitTestGameProject
{
    [TestClass]
    public class TestLifeTimeModule : UnitTest
    {
        [TestMethod]
        public void Test_LifeTimeModule_KillsObjectWhenLifeTimeUp()
        {
            MockBaseObject mockBaseObject = new MockBaseObject();

            mockBaseObject.AddModule(new LifeTimeModule(3));
            mockBaseObject.Update(3.5f);

            mockBaseObject.CheckDead();
        }
        
        [TestMethod]
        public void Test_LifeTimeModule_DoesntKillObjectWhenLifeTimeLeft()
        {
            MockBaseObject mockBaseObject = new MockBaseObject();

            mockBaseObject.AddModule(new LifeTimeModule(3));
            mockBaseObject.Update(1.5f);

            mockBaseObject.CheckAlive();
        }

        [TestMethod]
        public void Test_LifeTimeModule_KillsObjectOnlyWhenLifeTimeGreaterThanTotalElapsedTime()
        {
            MockBaseObject mockBaseObject = new MockBaseObject();

            mockBaseObject.AddModule(new LifeTimeModule(3));
            mockBaseObject.Update(3);

            mockBaseObject.CheckAlive();

            mockBaseObject.Update(0.001f);

            mockBaseObject.CheckDead();
        }
    }
}
