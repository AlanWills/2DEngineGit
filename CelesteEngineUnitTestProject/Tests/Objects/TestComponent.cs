using Microsoft.VisualStudio.TestTools.UnitTesting;
using CelesteEngineUnitTestFramework;

namespace CelesteEngineCelesteEngineUnitTestGameProject
{
    public class TestComponent : UnitTest
    {
        [TestMethod]
        public void Test_Component_Constructor()
        {
            MockComponent component = new MockComponent();

            component.CheckAlive();
        }

        [TestMethod]
        public void Test_Component_Die()
        {
            MockComponent component = new MockComponent();

            component.CheckAlive();
            component.Die();
            component.CheckDead();
        }

        [TestMethod]
        public void Test_Component_Hide()
        {
            MockComponent component = new MockComponent();

            component.CheckAlive();
            component.Hide();
            component.CheckHidden();
        }

        [TestMethod]
        public void Test_Component_Show()
        {
            MockComponent component = new MockComponent();

            component.Hide();
            component.Show();
            component.CheckAlive();
        }
    }
}