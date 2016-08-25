using CelesteEngine;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CelesteEngineUnitTestFramework;
using System.Collections.Generic;

namespace CelesteEngineCelesteEngineUnitTestGameProject
{
    /// <summary>
    /// Summary description for UnitTest1
    /// </summary>
    [TestClass]
    public class TestObjectManager : UnitTest
    {
        /// <summary>
        /// Our ObjectManager we will be running tests on
        /// </summary>
        private ObjectManager<Component> ObjectManager { get; set; }

        public TestObjectManager()
        {

        }

        private TestContext testContextInstance;

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

        #region Additional test attributes
        //
        // You can use the following additional attributes as you write your tests:
        //
        // Use ClassInitialize to run code before running the first test in the class
        // [ClassInitialize()]
        // public static void MyClassInitialize(TestContext testContext) { }
        //
        // Use ClassCleanup to run code after all tests in a class have run
        // [ClassCleanup()]
        // public static void MyClassCleanup() { }
        //
        // Use TestInitialize to run code before running each test 
        [TestInitialize()]
        public void MyTestInitialize()
        {
            ObjectManager = new ObjectManager<Component>();
            ObjectManager.LoadContent();
            ObjectManager.Initialise();
        }

        //
        // Use TestCleanup to run code after each test has run
        [TestCleanup()]
        public void MyTestCleanup()
        {
            if (ObjectManager.IsAlive)
            {
                // Kill the ObjectManager if needs be (some tests already do this as part of the testing)
                ObjectManager.Die();
            }
        }

        #endregion

        [TestMethod]
        public void TestObjectManagerAddingChildren()
        {
            ObjectManager.AddChild(new MockComponent(), true, true);
            ObjectManager.AddChild(new MockComponent(), true, true);
            ObjectManager.AddChild(new MockComponent(), true, true);

            ObjectManager.Update(0);

            foreach (Component component in ObjectManager)
            {
                component.CheckAlive();
            }

            Assert.AreEqual(3, ObjectManager.ActiveObjectsCount);
        }

        [TestMethod]
        public void TestObjectManagerAddingInitialChildren()
        {
            ObjectManager = new ObjectManager<Component>();

            ObjectManager.AddChild(new MockComponent());
            ObjectManager.AddChild(new MockComponent());

            Assert.AreEqual(0, ObjectManager.ActiveObjectsCount);

            foreach (Component component in ObjectManager)
            {
                component.CheckAlive();
            }

            ObjectManager.LoadContent();
            ObjectManager.Initialise();

            Assert.AreEqual(2, ObjectManager.ActiveObjectsCount);
        }

        [TestMethod]
        public void TestObjectManagerRemoveChild()
        {
            Component component1 = ObjectManager.AddChild(new MockComponent(), true, true);
            Component component2 = ObjectManager.AddChild(new MockComponent(), true, true);

            ObjectManager.RemoveChild(component1);
            ObjectManager.RemoveChild(component2);

            ObjectManager.Update(0);

            component1.CheckDead();
            component2.CheckDead();

            Assert.AreEqual(0, ObjectManager.ActiveObjectsCount);
        }

        [TestMethod]
        public void TestObjectManagerExtractChild()
        {
            Component component1 = ObjectManager.AddChild(new MockComponent(), true, true);
            Component component2 = ObjectManager.AddChild(new MockComponent(), true, true);

            ObjectManager.ExtractChild(component1);
            ObjectManager.ExtractChild(component2);

            ObjectManager.Update(0);

            component1.CheckAlive();
            component2.CheckAlive();

            Assert.AreEqual(0, ObjectManager.ActiveObjectsCount);
        }

        [TestMethod]
        public void TestObjectManagerGetChildrenOfType()
        {
            Component component1 = ObjectManager.AddChild(new MockComponent(), true, true);
            MockInheritedComponent inheritedComponent = ObjectManager.AddChild(new MockInheritedComponent(), true, true);

            ObjectManager.Update(0);

            Assert.AreEqual(2, ObjectManager.ActiveObjectsCount);

            List<Component> expectedList = new List<Component>() { component1, inheritedComponent };
            List<Component> actualList = ObjectManager.GetChildrenOfType<Component>();
            Assert.IsTrue(expectedList.CheckOrderedListsEqual(actualList));

            List<MockInheritedComponent> expectedInheritedList = new List<MockInheritedComponent>() { inheritedComponent };
            List<MockInheritedComponent> actualInheritedList = ObjectManager.GetChildrenOfType<MockInheritedComponent>();
            Assert.IsTrue(expectedInheritedList.CheckOrderedListsEqual(actualInheritedList));
        }

        [TestMethod]
        public void TestObjectManagerFindChild()
        {
            Component component1 = ObjectManager.AddChild(new MockComponent(), true, true);
            component1.Name = "TestComponent1";

            Component component2 = ObjectManager.AddChild(new MockComponent(), true, true);
            component2.Name = "TestComponent2";

            MockInheritedComponent testInheritedComponent = ObjectManager.AddChild(new MockInheritedComponent(), true, true);
            testInheritedComponent.Name = "TestInheritedComponent";

            ObjectManager.Update(0);

            Assert.AreEqual(component1, ObjectManager.FindChild<Component>(x => x.Name == "TestComponent1"));
            Assert.AreEqual(component2, ObjectManager.FindChild<Component>(x => x == component2));
            Assert.AreEqual(testInheritedComponent, ObjectManager.FindChild<MockInheritedComponent>(x => (x.Name == "TestInheritedComponent" || x == testInheritedComponent)));
        }

        [TestMethod]
        public void TestObjectManagerChildExists()
        {
            Component component1 = ObjectManager.AddChild(new MockComponent(), true, true);
            component1.Name = "TestComponent1";

            Component component2 = ObjectManager.AddChild(new MockComponent(), true, true);
            component2.Name = "TestComponent2";

            MockInheritedComponent testInheritedComponent = ObjectManager.AddChild(new MockInheritedComponent(), true, true);
            testInheritedComponent.Name = "TestInheritedComponent";

            ObjectManager.Update(0);

            Assert.IsTrue(ObjectManager.Exists(x => x.Name == "TestComponent1"));
            Assert.IsTrue(ObjectManager.Exists(x => x == component2));
            Assert.IsTrue(ObjectManager.Exists(x => (x.Name == "TestInheritedComponent" || x == testInheritedComponent)));

            Assert.IsFalse(ObjectManager.Exists(x => (x.Name == "NonExistentChild")));
            Assert.IsFalse(ObjectManager.Exists(x => (x.Name == "TestInheritedComponent" && x != testInheritedComponent)));
        }

        [TestMethod]
        public void TestObjectManagerDie()
        {
            ObjectManager.AddChild(new MockComponent());
            ObjectManager.AddChild(new MockComponent());

            ObjectManager.Die();

            ObjectManager.CheckDead();

            foreach (Component component in ObjectManager)
            {
                component.CheckDead();
            }
        }

        [TestMethod]
        public void TestObjectManagerHide()
        {
            ObjectManager.AddChild(new MockComponent(), true, true);
            ObjectManager.AddChild(new MockComponent(), true, true);

            ObjectManager.Update(0);

            // Not for children
            {
                ObjectManager.Hide(false);

                ObjectManager.CheckHidden();

                foreach (Component component in ObjectManager)
                {
                    component.CheckAlive();
                }
            }

            // For children
            {
                ObjectManager.Hide();

                ObjectManager.CheckHidden();

                foreach (Component component in ObjectManager)
                {
                    component.CheckHidden();
                }
            }
        }

        [TestMethod]
        public void TestObjectManagerShow()
        {
            ObjectManager.AddChild(new MockComponent(), true, true);
            ObjectManager.AddChild(new MockComponent(), true, true);

            ObjectManager.Update(0);

            ObjectManager.Hide();

            // Not for children
            {
                ObjectManager.Show(false);

                ObjectManager.CheckAlive();

                foreach (Component component in ObjectManager)
                {
                    component.CheckHidden();
                }
            }

            // For children
            {
                ObjectManager.Show();

                ObjectManager.CheckAlive();

                foreach (Component component in ObjectManager)
                {
                    component.CheckAlive();
                }
            }
        }

        [TestMethod]
        public void TestObjectManagerFirstChild()
        {
            Component component1 = new MockComponent();
            Component component2 = new MockComponent();

            ObjectManager.AddChild(component1, true, true);
            ObjectManager.AddChild(component2, true, true);

            ObjectManager.Update(0);

            Assert.AreEqual(component1, ObjectManager.FirstChild());
        }

        [TestMethod]
        public void TestObjectManagerLastChild()
        {
            Component component1 = new MockComponent();
            Component component2 = new MockComponent();

            ObjectManager.AddChild(component1, true, true);
            ObjectManager.AddChild(component2, true, true);

            ObjectManager.Update(0);

            Assert.AreEqual(component2, ObjectManager.LastChild<Component>());
        }

        [TestMethod]
        public void TestObjectManagerChildAtIndex()
        {
            MockComponent object1 = new MockComponent();
            MockComponent object2 = new MockComponent();
            MockComponent object3 = new MockComponent();
            MockComponent object4 = new MockComponent();

            ObjectManager.AddChild(object1, true, true);
            ObjectManager.AddChild(object2, true, true);
            ObjectManager.AddChild(object3, true, true);
            ObjectManager.AddChild(object4, true, true);

            ObjectManager.Update(0);

            Assert.AreEqual(object1, ObjectManager.ChildAtIndex(0));
            Assert.AreEqual(object2, ObjectManager.ChildAtIndex(1));
            Assert.AreEqual(object3, ObjectManager.ChildAtIndex(2));
            Assert.AreEqual(object4, ObjectManager.ChildAtIndex(3));
        }
    }
}
