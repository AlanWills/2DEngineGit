//using Microsoft.VisualStudio.TestTools.UnitTesting;
//using _2DEngine;

//namespace CelesteEngineCelesteEngineUnitTestGameProject.Iterators
//{
//    [TestClass]
//    public class TestBaseObjectIterator : UnitTest
//    {
//        [TestMethod]
//        public void TestBaseObjectIteratorConstructor()
//        {
//            TestEmptyBaseObject root = new TestEmptyBaseObject();
//            BaseObjectIterator<BaseObject> iterator = new BaseObjectIterator<BaseObject>(root);

//            Assert.AreEqual(root, iterator.Current);
//        }

//        [TestMethod]
//        public void TestBaseObjectIteratorOneLevelDown()
//        {
//            TestEmptyObjectManager<BaseObject> objectManager = new TestEmptyObjectManager<BaseObject>();

//            TestEmptyBaseObject root = objectManager.AddChild(new TestEmptyBaseObject(), true, true);
//            TestEmptyBaseObject child1 = root.AddChild(new TestEmptyBaseObject(), true, true);
//            TestEmptyBaseObject child2 = root.AddChild(new TestEmptyBaseObject(), true, true);
//            TestEmptyBaseObject child3 = root.AddChild(new TestEmptyBaseObject(), true, true);

//            objectManager.Update(0);

//            BaseObjectIterator<BaseObject> iterator = new BaseObjectIterator<BaseObject>(root);

//            Assert.AreEqual(root, iterator.Current);

//            Assert.IsTrue(iterator.MoveNext());
//            Assert.AreEqual(child1, iterator.Current);

//            Assert.IsTrue(iterator.MoveNext());
//            Assert.AreEqual(child2, iterator.Current);

//            Assert.IsTrue(iterator.MoveNext());
//            Assert.AreEqual(child3, iterator.Current);

//            // Make sure our iterator does not overflow into bad things
//            Assert.IsFalse(iterator.MoveNext());
//        }

//        [TestMethod]
//        public void TestBaseObjectIteratorReset()
//        {
//            TestEmptyObjectManager<BaseObject> objectManager = new TestEmptyObjectManager<BaseObject>();

//            TestEmptyBaseObject root = objectManager.AddChild(new TestEmptyBaseObject(), true, true);
//            TestEmptyBaseObject child1 = root.AddChild(new TestEmptyBaseObject(), true, true);
//            TestEmptyBaseObject child2 = root.AddChild(new TestEmptyBaseObject(), true, true);
//            TestEmptyBaseObject child3 = root.AddChild(new TestEmptyBaseObject(), true, true);

//            objectManager.Update(0);

//            BaseObjectIterator<BaseObject> iterator = new BaseObjectIterator<BaseObject>(root);

//            Assert.AreEqual(root, iterator.Current);

//            Assert.IsTrue(iterator.MoveNext());
//            Assert.AreEqual(child1, iterator.Current);

//            Assert.IsTrue(iterator.MoveNext());
//            Assert.AreEqual(child2, iterator.Current);

//            Assert.IsTrue(iterator.MoveNext());
//            Assert.AreEqual(child3, iterator.Current);

//            // Make sure our iterator does not overflow into bad things
//            Assert.IsFalse(iterator.MoveNext());

//            iterator.Reset();

//            Assert.AreEqual(root, iterator.Current);

//            Assert.IsTrue(iterator.MoveNext());
//            Assert.AreEqual(child1, iterator.Current);

//            Assert.IsTrue(iterator.MoveNext());
//            Assert.AreEqual(child2, iterator.Current);

//            Assert.IsTrue(iterator.MoveNext());
//            Assert.AreEqual(child3, iterator.Current);

//            // Make sure our iterator does not overflow into bad things
//            Assert.IsFalse(iterator.MoveNext());
//        }

//        /// <summary>
//        /// Utility class for our node iterator tests
//        /// </summary>
//        private class TestExtendedEmptyBaseObject : TestEmptyBaseObject
//        {

//        }

//        [TestMethod]
//        public void TestBaseObjectIteratorOneLevelDownWithTyping()
//        {
//            TestEmptyObjectManager<BaseObject> objectManager = new TestEmptyObjectManager<BaseObject>();

//            TestEmptyBaseObject root = objectManager.AddChild(new TestEmptyBaseObject(), true, true);
//            TestEmptyBaseObject child1 = root.AddChild(new TestEmptyBaseObject(), true, true);
//            TestExtendedEmptyBaseObject child2 = root.AddChild(new TestExtendedEmptyBaseObject(), true, true);
//            TestEmptyBaseObject child3 = root.AddChild(new TestEmptyBaseObject(), true, true);

//            objectManager.Update(0);

//            BaseObjectIterator<TestExtendedEmptyBaseObject> iterator = new BaseObjectIterator<TestExtendedEmptyBaseObject>(root);

//            Assert.AreEqual(root, iterator.Current);

//            Assert.IsTrue(iterator.MoveNext());
//            Assert.AreEqual(child2, iterator.Current);

//            // Make sure our iterator does not overflow into bad things
//            Assert.IsFalse(iterator.MoveNext());
//        }

//        [TestMethod]
//        public void TestBaseObjectIteratorTwoLevelsDown()
//        {
//            TestEmptyObjectManager<BaseObject> objectManager = new TestEmptyObjectManager<BaseObject>();

//            TestEmptyBaseObject root = objectManager.AddChild(new TestEmptyBaseObject(), true, true);
//            TestEmptyBaseObject child1 = root.AddChild(new TestEmptyBaseObject(), true, true);
//            TestEmptyBaseObject child2 = root.AddChild(new TestEmptyBaseObject(), true, true);
//            TestEmptyBaseObject child3 = root.AddChild(new TestEmptyBaseObject(), true, true);
//            TestEmptyBaseObject grandChild1 = child1.AddChild(new TestEmptyBaseObject(), true, true);
//            TestEmptyBaseObject grandChild2 = child1.AddChild(new TestEmptyBaseObject(), true, true);
//            TestEmptyBaseObject grandChild3 = child3.AddChild(new TestEmptyBaseObject(), true, true);

//            objectManager.Update(0);

//            BaseObjectIterator<BaseObject> iterator = new BaseObjectIterator<BaseObject>(root);

//            Assert.AreEqual(root, iterator.Current);

//            Assert.IsTrue(iterator.MoveNext());
//            Assert.AreEqual(child1, iterator.Current);

//            Assert.IsTrue(iterator.MoveNext());
//            Assert.AreEqual(child2, iterator.Current);

//            Assert.IsTrue(iterator.MoveNext());
//            Assert.AreEqual(child3, iterator.Current);

//            Assert.IsTrue(iterator.MoveNext());
//            Assert.AreEqual(grandChild1, iterator.Current);

//            Assert.IsTrue(iterator.MoveNext());
//            Assert.AreEqual(grandChild2, iterator.Current);

//            Assert.IsTrue(iterator.MoveNext());
//            Assert.AreEqual(grandChild3, iterator.Current);

//            // Make sure our iterator does not overflow into bad things
//            Assert.IsFalse(iterator.MoveNext());
//        }

//        [TestMethod]
//        public void TestBaseObjectIteratorTwoLevelsDownWithTyping()
//        {
//            TestEmptyObjectManager<BaseObject> objectManager = new TestEmptyObjectManager<BaseObject>();

//            TestEmptyBaseObject root = objectManager.AddChild(new TestEmptyBaseObject(), true, true);
//            TestExtendedEmptyBaseObject child1 = root.AddChild(new TestExtendedEmptyBaseObject(), true, true);
//            TestEmptyBaseObject child2 = root.AddChild(new TestEmptyBaseObject(), true, true);
//            TestExtendedEmptyBaseObject child3 = root.AddChild(new TestExtendedEmptyBaseObject(), true, true);
//            TestEmptyBaseObject grandChild1 = child1.AddChild(new TestEmptyBaseObject(), true, true);
//            TestExtendedEmptyBaseObject grandChild2 = child1.AddChild(new TestExtendedEmptyBaseObject(), true, true);
//            TestEmptyBaseObject grandChild3 = child3.AddChild(new TestEmptyBaseObject(), true, true);

//            objectManager.Update(0);

//            BaseObjectIterator<TestExtendedEmptyBaseObject> iterator = new BaseObjectIterator<TestExtendedEmptyBaseObject>(root);

//            Assert.AreEqual(root, iterator.Current);

//            Assert.IsTrue(iterator.MoveNext());
//            Assert.AreEqual(child1, iterator.Current);

//            Assert.IsTrue(iterator.MoveNext());
//            Assert.AreEqual(child3, iterator.Current);

//            Assert.IsTrue(iterator.MoveNext());
//            Assert.AreEqual(grandChild2, iterator.Current);

//            // Make sure our iterator does not overflow into bad things
//            Assert.IsFalse(iterator.MoveNext());
//        }

//        /*[TestMethod]
//        public void TestBaseObjectIteratorBonkersTree()
//        {
//            TestEmptyObjectManager<BaseObject> objectManager = new TestEmptyObjectManager<BaseObject>();

//            TestEmptyBaseObject root = objectManager.AddChild(new TestEmptyBaseObject(), true, true);
//            TestEmptyBaseObject child1 = root.AddChild(new TestEmptyBaseObject(), true, true);
//            TestEmptyBaseObject child2 = root.AddChild(new TestEmptyBaseObject(), true, true);
//            TestEmptyBaseObject child3 = root.AddChild(new TestEmptyBaseObject(), true, true);
//            TestEmptyBaseObject grandChild1 = child1.AddChild(new TestEmptyBaseObject(), true, true);
//            TestEmptyBaseObject grandChild2 = child2.AddChild(new TestEmptyBaseObject(), true, true);
//            TestEmptyBaseObject grandChild3 = child3.AddChild(new TestEmptyBaseObject(), true, true);
//            TestEmptyBaseObject greatGrandChild1 = grandChild1.AddChild(new TestEmptyBaseObject(), true, true);
//            TestEmptyBaseObject greatGrandChild2 = grandChild2.AddChild(new TestEmptyBaseObject(), true, true);

//            objectManager.Update(0);

//            BaseObjectIterator<TestEmptyBaseObject> iterator = new BaseObjectIterator<TestEmptyBaseObject>(root);

//            Assert.AreEqual(root, iterator.Current);

//            Assert.IsTrue(iterator.MoveNext());
//            Assert.AreEqual(child1, iterator.Current);

//            Assert.IsTrue(iterator.MoveNext());
//            Assert.AreEqual(child2, iterator.Current);

//            Assert.IsTrue(iterator.MoveNext());
//            Assert.AreEqual(child3, iterator.Current);

//            Assert.IsTrue(iterator.MoveNext());
//            Assert.AreEqual(grandChild1, iterator.Current);

//            Assert.IsTrue(iterator.MoveNext());
//            Assert.AreEqual(grandChild2, iterator.Current);

//            Assert.IsTrue(iterator.MoveNext());
//            Assert.AreEqual(grandChild3, iterator.Current);

//            Assert.IsTrue(iterator.MoveNext());
//            Assert.AreEqual(greatGrandChild1, iterator.Current);

//            Assert.IsTrue(iterator.MoveNext());
//            Assert.AreEqual(greatGrandChild2, iterator.Current);

//            // Make sure our iterator does not overflow into bad things
//            Assert.IsFalse(iterator.MoveNext());
//        }*/
//    }
//}
