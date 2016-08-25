using CelesteEngine;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.Xna.Framework;
using CelesteEngineUnitTestFramework;

namespace CelesteEngineCelesteEngineUnitTestGameProject.Objects
{
    [TestClass]
    public class TestBaseObject : UnitTest
    {
        [TestMethod]
        public void TestBaseObjectConstructor()
        {
            MockBaseObject baseObject = new MockBaseObject(Vector2.Zero);
            Assert.AreEqual(1, baseObject.Opacity);
            Assert.AreEqual(Color.White, baseObject.Colour);
            Assert.IsTrue(baseObject.UsesCollider);

            MockBaseObject baseObject1 = new MockBaseObject(new Vector2(20, 20));

            Assert.AreEqual(new Vector2(20, 20), baseObject1.WorldPosition);
            Assert.AreEqual(0, baseObject1.WorldRotation);

            MockBaseObject baseObject2 = new MockBaseObject(new Vector2(30, 30), new Vector2(30, 30), "");

            Assert.AreEqual(new Vector2(30, 30), baseObject2.WorldPosition);
            Assert.AreEqual(0, baseObject2.WorldRotation);
            Assert.AreEqual(new Vector2(30, 30), baseObject2.Size);
        }

        [TestMethod]
        public void TestBaseObjectAddChild()
        {
            MockBaseObject parent = new MockBaseObject(new Vector2(20, 20));
            MockBaseObject child = parent.AddChild(new MockBaseObject(new Vector2(30, 30), new Vector2(30, 30), ""));

            Assert.AreEqual(parent, child.Parent);
        }

        [TestMethod]
        public void TestBaseObjectParenting()
        {
            MockBaseObject parent = new MockBaseObject(new Vector2(20, 20), "");
            MockBaseObject child = parent.AddChild(new MockBaseObject(new Vector2(30, 30), new Vector2(30, 30), ""));

            Assert.AreEqual(parent, child.Parent);
            Assert.AreEqual(new Vector2(50, 50), child.WorldPosition);

            parent.LocalPosition += new Vector2(20, 20);
            Assert.AreEqual(new Vector2(40, 40), parent.WorldPosition);
            Assert.AreEqual(new Vector2(70, 70), child.WorldPosition);

            child.LocalPosition += new Vector2(-10, 10);
            Assert.AreEqual(new Vector2(40, 40), parent.WorldPosition);
            Assert.AreEqual(new Vector2(60, 80), child.WorldPosition);

            parent.CheckAlive();
            child.CheckAlive();

            parent.Die();

            parent.CheckDead();
            child.CheckDead();
        }

        [TestMethod]
        public void TestBaseObjectFindChild()
        {
            MockBaseObject parent = new MockBaseObject(new Vector2(20, 20));
            MockBaseObject child = parent.AddChild(new MockBaseObject());

            Assert.AreEqual(parent, child.Parent);
            Assert.AreEqual(child, parent.FindChild<MockBaseObject>(x => x == child));

            BaseObject namedChild = parent.AddChild(new MockBaseObject());
            namedChild.Name = "TestName";

            Assert.AreEqual(parent, namedChild.Parent);
            Assert.AreEqual(namedChild, parent.FindChild<MockBaseObject>(x => x.Name == "TestName"));
        }

        [TestMethod]
        public void TestBaseObjectChildExists()
        {
            MockBaseObject parent = new MockBaseObject(new Vector2(20, 20));
            MockBaseObject child = parent.AddChild(new MockBaseObject());

            Assert.AreEqual(parent, child.Parent);
            Assert.IsTrue(parent.Exists(x => x == child));

            MockBaseObject secondParent = new MockBaseObject(new Vector2(20, 20));
            MockBaseObject namedChild = secondParent.AddChild(new MockBaseObject());
            namedChild.Name = "TestName";

            Assert.AreEqual(secondParent, namedChild.Parent);
            Assert.IsTrue(secondParent.Exists(x => x.Name == "TestName"));

            Assert.IsFalse(parent.Exists(x => x == namedChild));
            Assert.IsFalse(secondParent.Exists(x => x.Name != "TestName"));
        }

        [TestMethod]
        public void TestBaseObjectReparenting()
        {
            MockBaseObject parent = new MockBaseObject(new Vector2(20, 20));
            MockBaseObject child = parent.AddChild(new MockBaseObject(new Vector2(30, 30), new Vector2(30, 30), ""));

            Assert.AreEqual(parent, child.Parent);
            Assert.AreEqual(new Vector2(50, 50), child.WorldPosition);

            parent = new MockBaseObject(new Vector2(-30, -30));
            child.ReparentTo(parent);

            Assert.AreEqual(parent, child.Parent);
            Assert.AreEqual(Vector2.Zero, child.WorldPosition);

            child.ReparentTo(null);

            Assert.IsNull(child.Parent);
            Assert.AreEqual(new Vector2(30, 30), child.WorldPosition);
        }


        [TestMethod]
        public void Test_BaseObject_HideButNotHideChildren()
        {
            MockBaseObject baseObject = new MockBaseObject();
            MockBaseObject child = new MockBaseObject();

            baseObject.AddChild(child);
            baseObject.Hide(false);

            baseObject.CheckHidden();
            child.CheckAlive();
        }

        [TestMethod]
        public void Test_BaseObject_HideAndHideChildren()
        {
            MockBaseObject baseObject = new MockBaseObject();
            MockBaseObject child = new MockBaseObject();

            baseObject.AddChild(child);
            baseObject.Hide(true);

            baseObject.CheckHidden();
            child.CheckHidden();
        }

        [TestMethod]
        public void Test_BaseObject_ShowButNotShowChildren()
        {
            MockBaseObject baseObject = new MockBaseObject();
            MockBaseObject child = new MockBaseObject();

            baseObject.AddChild(child);
            baseObject.Hide(true);
            baseObject.Show(false);

            baseObject.CheckAlive();
            child.CheckHidden();
        }

        [TestMethod]
        public void Test_BaseObject_ShowAndShowChildren()
        {
            MockBaseObject baseObject = new MockBaseObject();
            MockBaseObject child = new MockBaseObject();

            baseObject.AddChild(child);
            baseObject.Hide(true);
            baseObject.Show(true);

            baseObject.CheckAlive();
            child.CheckAlive();
        }
        
        /// <summary>
        /// A module for unit testing purposes only
        /// </summary>
        private class TestModule : BaseObjectModule { }

        [TestMethod]
        public void Test_BaseObject_AddModule()
        {
            MockBaseObject baseObject = new MockBaseObject();
            TestModule module = baseObject.AddModule(new TestModule());

            Assert.IsNotNull(module);
            Assert.AreEqual(baseObject, module.AttachedComponent);
            Assert.AreEqual(baseObject, module.AttachedBaseObject);
        }

        [TestMethod]
        public void Test_BaseObject_FindModule()
        {
            MockBaseObject baseObject = new MockBaseObject();
            TestModule module = baseObject.AddModule(new TestModule());

            Assert.IsNotNull(module);
            Assert.AreEqual(module, baseObject.FindModule<TestModule>());
        }

        [TestMethod]
        public void TestBaseObjectNextSibling()
        {
            ObjectManager<MockBaseObject> objectManager = new ObjectManager<MockBaseObject>();

            MockBaseObject object1 = new MockBaseObject();
            MockBaseObject object2 = new MockBaseObject();
            MockBaseObject object3 = new MockBaseObject();
            MockBaseObject object4 = new MockBaseObject();

            objectManager.AddChild(object1);

            object1.AddChild(object2);
            object1.AddChild(object3);
            object1.AddChild(object4);

            objectManager.Update(0);

            Assert.IsFalse(object1.HasNextSibling);
            Assert.IsTrue(object2.HasNextSibling);
            Assert.IsTrue(object3.HasNextSibling);
            Assert.IsFalse(object4.HasNextSibling);

            Assert.AreEqual(object3, object2.NextSibling);
            Assert.AreEqual(object4, object3.NextSibling);
        }

        [TestMethod]
        public void TestBaseObjectPreviousSibling()
        {
            ObjectManager<MockBaseObject> objectManager = new ObjectManager<MockBaseObject>();

            MockBaseObject object1 = new MockBaseObject();
            MockBaseObject object2 = new MockBaseObject();
            MockBaseObject object3 = new MockBaseObject();
            MockBaseObject object4 = new MockBaseObject();

            objectManager.AddChild(object1);

            object1.AddChild(object2);
            object1.AddChild(object3);
            object1.AddChild(object4);

            objectManager.Update(0);

            Assert.IsFalse(object1.HasPreviousSibling);
            Assert.IsFalse(object2.HasPreviousSibling);
            Assert.IsTrue(object3.HasPreviousSibling);
            Assert.IsTrue(object4.HasPreviousSibling);

            Assert.AreEqual(object2, object3.PreviousSibling);
            Assert.AreEqual(object3, object4.PreviousSibling);
        }
    }
}