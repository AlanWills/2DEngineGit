using CelesteEngine;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CelesteEngineUnitTestFramework;

namespace CelesteEngineCelesteEngineUnitTestGameProject.Modules
{
    [TestClass]
    public class TestDamageableObjectModule : UnitTest
    {
        [TestMethod]
        public void TestDamageableObjectModuleDamage()
        {
            MockBaseObject baseObject = new MockBaseObject();
            DamageableObjectModule damageModule = baseObject.AddModule(new DamageableObjectModule(10), true, true);

            Assert.IsNotNull(damageModule);
            Assert.AreEqual(baseObject, damageModule.AttachedComponent);
            Assert.AreEqual(baseObject, damageModule.AttachedBaseObject);
            Assert.AreEqual(10, damageModule.Health);

            damageModule.Damage(5, null);
            Assert.AreEqual(5, damageModule.Health);
        }

        [TestMethod]
        public void TestDamageableObjectModuleDead()
        {
            BaseObject baseObject = new MockBaseObject();
            DamageableObjectModule damageModule = baseObject.AddModule(new DamageableObjectModule(10), true, true);

            Assert.IsNotNull(damageModule);
            Assert.AreEqual(baseObject, damageModule.AttachedComponent);
            Assert.AreEqual(baseObject, damageModule.AttachedBaseObject);
            Assert.AreEqual(10, damageModule.Health);

            damageModule.Damage(20, null);
            Assert.AreEqual(0, damageModule.Health);
            Assert.IsTrue(damageModule.Dead);
        }

        /// <summary>
        /// A test event callback for a custom amount of damage.
        /// </summary>
        /// <param name="objectDoingTheDamage"></param>
        /// <param name="inputDamage"></param>
        /// <returns></returns>
        private float CalculateDamageTest(BaseObject objectDoingTheDamage, float inputDamage)
        {
            return 2 * inputDamage;
        }

        /// <summary>
        /// A test event callback
        /// </summary>
        /// <param name="damageableObjectModule"></param>
        private void OnDamageTest(DamageableObjectModule damageableObjectModule, float damageDealt)
        {
            // Just test we have got here
            Assert.IsNotNull(damageableObjectModule);
            Assert.AreEqual(1, damageDealt);
        }

        [TestMethod]
        public void TestDamageableObjectModuleOnDamageEvent()
        {
            MockBaseObject baseObject = new MockBaseObject();
            DamageableObjectModule damageModule = baseObject.AddModule(new DamageableObjectModule(10), true, true);
            damageModule.OnDamage += OnDamageTest;

            damageModule.Damage(1, null);
        }

        [TestMethod]
        public void TestDamageableObjectModuleCalculateDamageEvent()
        {
            MockBaseObject baseObject = new MockBaseObject();
            DamageableObjectModule damageModule = baseObject.AddModule(new DamageableObjectModule(10), true, true);
            damageModule.CalculateDamage += CalculateDamageTest;

            damageModule.Damage(5, null);
            Assert.IsTrue(damageModule.Dead);
        }
    }
}
