using System;
using System.Collections.Generic;

namespace CelesteEngine
{
    /// <summary>
    /// A class which represents a platform independent input interface.
    /// It is responsible for creating and updating the mouse, keyboard and touch interface.
    /// </summary>
    public abstract class InputManager : ObjectManager<Component>
    {
        #region Properties and Fields

        /// <summary>
        /// The static instance we access this class through.
        /// All platform specific classes must implement this with the constructor for their custom platform input handling.
        /// </summary>
        protected static InputManager instance;
        public static InputManager Instance
        {
            get
            {
                DebugUtils.AssertNotNull(instance, "Input Manager instance must be set for this platform");
                return instance;
            }
        }

        /// <summary>
        /// A map with an event name and a pair of functions (one for pc, one for touchscreen) which we will use to
        /// determine whether the event has occurred.  E.g. a 'clicked' event with functions checking the collider with the mouse or touch input depending on the platform we are on.
        /// </summary>
        protected Dictionary<string, Tuple<Func<bool>, Func<bool>>> EventFuncMap { get; private set; }

        /// <summary>
        /// Empty function used for a no-op (i.e. if a certain event is not supported on a platform).
        /// </summary>
        public static Func<bool> Empty = delegate { DebugUtils.Fail("Not implemented for this platform"); return false; };

        #endregion

        protected InputManager()
        {
            EventFuncMap = new Dictionary<string, Tuple<Func<bool>, Func<bool>>>();
        }

        #region Utility Functions

        /// <summary>
        /// Adds an event for out input manager with the appropriate check functions which we will use at runtime to determine whether input was sufficient on the current platform for the event.
        /// </summary>
        /// <param name="eventName"></param>
        /// <param name="pcCheck"></param>
        /// <param name="touchScreenCheck"></param>
        public void AddInputEvent(string eventName, Func<bool> pcCheck, Func<bool> touchScreenCheck)
        {
            EventFuncMap.Add(eventName, new Tuple<Func<bool>, Func<bool>>(pcCheck, touchScreenCheck));
        }

        /// <summary>
        /// Uses the input eventName to find the platform check functions and then executes the appropriate one, thus tesing whether input was sufficient to meet the event on our platform.
        /// Returns true if platform input was sufficient for the event and false if not.
        /// </summary>
        /// <param name="eventName"></param>
        public abstract bool CheckInputEvent(string eventName);

        #endregion
    }
}
