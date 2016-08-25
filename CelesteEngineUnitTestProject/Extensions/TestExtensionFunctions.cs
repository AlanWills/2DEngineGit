using CelesteEngine;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace CelesteEngineCelesteEngineUnitTestGameProject
{
    public static class TestExtensionFunctions
    {
        /// <summary>
        /// Performs unit test asserts on the IsAlive, ShouldHandleInput, ShouldUpdate and ShouldDraw properties to make sure they are all true
        /// </summary>
        /// <param name="component"></param>
        public static void CheckAlive(this Component component)
        {
            Assert.IsTrue(component.IsAlive);
            Assert.IsTrue(component.ShouldHandleInput);
            Assert.IsTrue(component.ShouldUpdate);
            Assert.IsTrue(component.ShouldDraw);
        }

        /// <summary>
        /// Performs unit test asserts on the IsAlive, ShouldHandleInput, ShouldUpdate and ShouldDraw properties to make sure they are all false
        /// </summary>
        /// <param name="component"></param>
        public static void CheckDead(this Component component)
        {
            Assert.IsFalse(component.IsAlive);
            Assert.IsFalse(component.ShouldHandleInput);
            Assert.IsFalse(component.ShouldUpdate);
            Assert.IsFalse(component.ShouldDraw);
        }

        /// <summary>
        /// Performs unit test asserts on the ShouldHandleInput, ShouldUpdate and ShouldDraw properties to make sure they are all false and the IsAlive to make sure it is true
        /// </summary>
        /// <param name="component"></param>
        public static void CheckHidden(this Component component)
        {
            Assert.IsTrue(component.IsAlive);
            Assert.IsFalse(component.ShouldHandleInput);
            Assert.IsFalse(component.ShouldUpdate);
            Assert.IsFalse(component.ShouldDraw);
        }

        /// <summary>
        /// Checks that the two inputted lists have the same objects in them.
        /// Assumes the elements are ordered the same, so if they are the same, they will be in the same order.
        /// Will fail even if the lists contain the same elements, but are in a different order.
        /// Much quicker than the unordered check.
        /// Does perform UnitTest asserts, but the output can still be checked.
        /// </summary>
        /// <param name="expected">Our expected list of objects</param>
        /// <param name="actual">Our actual list of objects</param>
        public static bool CheckOrderedListsEqual<T>(this List<T> expected, List<T> actual)
        {
            // Check sizes are the same
            Assert.AreEqual(expected.Count, actual.Count);

            if (expected.Count != actual.Count)
            {
                Assert.Fail("Lists have different number of elements");
                return false;
            }

            for (int i = 0; i < expected.Count; i++)
            {
                bool result = expected[i].Equals(actual[i]);
                Assert.IsTrue(result);

                if (!result)
                {
                    // Return at the first sign of inconsistency - saves us time doing further checks on an already failed test
                    return false;
                }
            }

            return true;
        }

        /// <summary>
        /// Checks whether two lists have the same elements regardless of ordering.
        /// Slower than the ordered check so use that wherever possible.
        /// </summary>
        /// <param name="expected">The expected list of objects</param>
        /// <param name="actual">The actual list of objects</param>
        public static bool CheckUnorderedListsEqual<T>(this List<T> expected, List<T> actual)
        {
            // Check sizes are the same
            Assert.AreEqual(expected.Count, actual.Count);

            if (expected.Count != actual.Count)
            {
                Assert.Fail("Lists have different number of elements");
                return false;
            }

            for (int i = 0; i < expected.Count; i++)
            {
                bool result = actual.Exists(x => x.GetType().Equals(expected[i]));
                Assert.IsTrue(result);

                if (!result)
                {
                    // Return at the first sign of inconsistency - saves us time doing further checks on an already failed test
                    return false;
                }
            }

            return true;
        }
    }
}
