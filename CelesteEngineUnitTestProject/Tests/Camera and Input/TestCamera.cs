﻿using CelesteEngine;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.Xna.Framework;
using CelesteEngineUnitTestFramework;

namespace CelesteEngineCelesteEngineUnitTestGameProject
{
    [TestClass]
    public class TestCamera : UnitTest
    {
        [TestMethod]
        public void Test_Camera_FocusOnPosition()
        {
            Vector2 expectedPosition = new Vector2();

            Camera.Instance.FocusOnPosition(new Vector2(500, 500), true);
            Assert.AreEqual(expectedPosition, Camera.Instance.Position);
        }

        // Haven't written code to initialise the camera yet so can't run these tests
        /*[TestMethod]
        public void TestCameraModes()
        {
            Vector2 expectedPosition = Vector2.Zero;

            Camera.SetFixed(expectedPosition);
            Assert.AreEqual(expectedPosition, Camera.Position);

            expectedPosition = new Vector2(500, 500);
            Camera.SetFree(expectedPosition);
            Assert.AreEqual(expectedPosition, Camera.Position.Value);

            expectedPosition += new Vector2(600, 600);
            Camera.Position.Value += new Vector2(100, 100);
            Assert.AreEqual(expectedPosition, Camera.Position.Value);

            expectedPosition = Vector2.Zero;
            Camera.SetFixed(expectedPosition);
            Assert.AreEqual(expectedPosition, Camera.Position.Value);
        }

        [TestMethod]
        public void TestGameToScreenCoords()
        {
            // Reset the camera
            Camera.SetFree(Vector2.Zero);

            Vector2 gamePosition = new Vector2(500, 500);
            Vector2 expectedScreenPosition = new Vector2(500, 500);
            Assert.AreEqual(expectedScreenPosition, Camera.GameToScreenCoords(gamePosition));

            Camera.Position.Value = new Vector2(-500, -500);
            expectedScreenPosition = new Vector2(0, 0);
            Assert.AreEqual(expectedScreenPosition, Camera.GameToScreenCoords(gamePosition));

            Camera.Position.Value = new Vector2(500, 500);
            expectedScreenPosition = new Vector2(1000, 1000);
            Assert.AreEqual(expectedScreenPosition, Camera.GameToScreenCoords(gamePosition));
        }*/
    }
}
