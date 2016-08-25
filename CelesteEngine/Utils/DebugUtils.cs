using System.Diagnostics;

namespace CelesteEngine
{
    public static class DebugUtils
    {
        [Conditional("DEBUG")]
        public static void Fail(string message = "")
        {
            Debug.Assert(false, message);
        }

        [Conditional("DEBUG")]
        public static void AssertNotNull(object nullableObject, string message = "")
        {
            Debug.Assert(nullableObject != null, message);
        }

        [Conditional("DEBUG")]
        public static void AssertNull(object nullableObject, string message = "")
        {
            Debug.Assert(nullableObject == null, message);
        }
    }
}
