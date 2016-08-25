using CelesteEngine;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CelesteEngineUnitTestFramework;
using System.Diagnostics;
using Microsoft.Xna.Framework;

namespace CelesteEngineUnitTestProject
{
    [TestClass]
    public class TestInputManager : UnitTest
    {
        [TestMethod]
        public void Test_InputManager_TestConstructor()
        {
            Assert.IsNotNull(InputManager.Instance);
        }

        [TestMethod]
        public void Test_InputManager_AddInputEvent()
        {
            InputManager.Instance.AddInputEvent("Test Event", InputManager.Empty, InputManager.Empty);
        }

        [TestMethod]
        public void Test_InputManager_CheckInputEventEmpty()
        {
            Trace.Listeners.Clear();

            InputManager.Instance.AddInputEvent("New Test Event", InputManager.Empty, InputManager.Empty);
            Assert.IsFalse(InputManager.Instance.CheckInputEvent("New Test Event"));

            Trace.Refresh();
        }

        [TestMethod]
        public void Test_InputManager_CheckInputEvent()
        {
            InputManager.Instance.AddInputEvent("New New Test Event", ReturnTrue, ReturnTrue);
            Assert.IsTrue(InputManager.Instance.CheckInputEvent("New New Test Event"));
        }

        private bool ReturnTrue()
        {
            return true;
        }
    }
}