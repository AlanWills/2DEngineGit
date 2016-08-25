using CelesteEngine;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.Xna.Framework;
using CelesteEngineUnitTestFramework;

namespace CelesteEngineCelesteEngineUnitTestGameProject.Colliders
{
    [TestClass]
    public class TestRectangleCollider : UnitTest
    {
        [TestMethod]
        public void TestRectangleColliderConstructor()
        {
            GameObject gameObject = new GameObject(new Vector2(100, 100), "");
            gameObject.Size = new Vector2(50, 50);
            RectangleCollider collider = new RectangleCollider(gameObject);

            Rectangle expectedRectangle = new Rectangle(75, 75, 50, 50);
            Assert.AreEqual(expectedRectangle, collider.Bounds);
        }

        [TestMethod]
        public void TestRectangleColliderPointIntersection()
        {
            GameObject gameObject = new GameObject(new Vector2(100, 100), "");
            gameObject.Size = new Vector2(50, 50);
            RectangleCollider collider = new RectangleCollider(gameObject);

            Vector2 intersectionPoint = new Vector2(100, 100);
            Assert.IsTrue(collider.CheckIntersects(intersectionPoint));

            intersectionPoint = new Vector2(200, 200);
            Assert.IsFalse(collider.CheckIntersects(intersectionPoint));
        }

        [TestMethod]
        public void TestRectangleColliderRectangleIntersection()
        {
            GameObject gameObject = new GameObject(new Vector2(100, 100), "");
            gameObject.Size = new Vector2(50, 50);
            RectangleCollider collider = new RectangleCollider(gameObject);

            Rectangle intersectionRectangle = new Rectangle(0, 0, 100, 100);
            Assert.IsTrue(collider.CheckIntersects(intersectionRectangle));

            intersectionRectangle = new Rectangle(50, 50, 50, 50);
            Assert.IsTrue(collider.CheckIntersects(intersectionRectangle));

            intersectionRectangle = new Rectangle(0, 0, 10, 10);
            Assert.IsFalse(collider.CheckIntersects(intersectionRectangle));

            intersectionRectangle = new Rectangle(175, 100, 50, 50);
            Assert.IsFalse(collider.CheckIntersects(intersectionRectangle));
        }

        [TestMethod]
        public void TestRectangleColliderOnRectangleColliderCollision()
        {
            GameObject gameObject = new GameObject(new Vector2(100, 100), "");
            gameObject.Size = new Vector2(50, 50);
            RectangleCollider collider = new RectangleCollider(gameObject);

            GameObject gameObject2 = new GameObject(new Vector2(150, 100), "");
            gameObject2.Size = new Vector2(60, 60);
            RectangleCollider collider2 = new RectangleCollider(gameObject2);

            GameObject gameObject3 = new GameObject(new Vector2(200, 100), "");
            gameObject3.Size = new Vector2(60, 60);
            RectangleCollider collider3 = new RectangleCollider(gameObject3);

            Assert.IsTrue(collider.CheckCollisionWith(collider2));
            Assert.IsFalse(collider.CheckCollisionWith(collider3));
            Assert.IsTrue(collider2.CheckCollisionWith(collider3));
        }
    }
}
