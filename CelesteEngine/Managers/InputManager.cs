using System;
using System.Collections.Generic;
using System.Diagnostics;
using CheckFuncs = System.Tuple<System.Func<object[], bool>, System.Func<object[], bool>>;

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
        /// Yeah this is rough right now - need to improve this somehow
        /// </summary>
        private static InputManager instance;
        public static InputManager Instance
        {
            get
            {
                DebugUtils.AssertNotNull(instance, "Input Manager instance must be set for this platform");
                return instance;
            }
            set { instance = value; }
        }

        /// <summary>
        /// A map with an event name and a pair of functions (one for pc, one for touchscreen) which we will use to
        /// determine whether the event has occurred.  E.g. a 'clicked' event with functions checking the collider with the mouse or touch input depending on the platform we are on.
        /// </summary>
        protected Dictionary<string, Tuple<Func<object[], bool>, Func<object[], bool>>> EventFuncMap { get; private set; }

        /// <summary>
        /// Empty function used for a no-op (i.e. if a certain event is not supported on a platform).
        /// </summary>
        public static Func<object[], bool> Empty = delegate { DebugUtils.Fail("Not implemented for this platform"); return false; };

        #endregion

        protected InputManager()
        {
            EventFuncMap = new Dictionary<string, CheckFuncs>();
            EventFuncMap.Add(ScreenClicked, new CheckFuncs(IsMouseButtonClicked, IsScreenTapped));
        }

        #region Utility Functions

        /// <summary>
        /// Adds an event for out input manager with the appropriate check functions which we will use at runtime to determine whether input was sufficient on the current platform for the event.
        /// If the event already exists, it will just exit without failure - this will save extra 'isEventAdded' flags on classes adding their own events.
        /// </summary>
        /// <param name="eventName"></param>
        /// <param name="pcCheck"></param>
        /// <param name="touchScreenCheck"></param>
        public void AddInputEvent(string eventName, Func<object[], bool> pcCheck, Func<object[], bool> touchScreenCheck)
        {
            if (EventFuncMap.ContainsKey(eventName)) { return; }

            EventFuncMap.Add(eventName, new CheckFuncs(pcCheck, touchScreenCheck));
        }

        /// <summary>
        /// Uses the input eventName to find the platform check functions and then executes the appropriate one, thus tesing whether input was sufficient to meet the event on our platform.
        /// Returns true if platform input was sufficient for the event and false if not.
        /// </summary>
        /// <param name="eventName"></param>
        public abstract bool CheckInputEvent(string eventName, params object[] parameters);

        #endregion

        #region Common Input Check Functions

        #region Clicking Check Funcs

        /// <summary>
        /// Corresponds to the screen being interacted with - either a click with the mouse or a tap on a touch screen.
        /// </summary>
        /// <returns></returns>
        public const string ScreenClicked = "ScreenClicked";

        private static bool IsMouseButtonClicked(params object[] parameters)
        {
            Debug.Assert(parameters.Length == 1);
            Debug.Assert(parameters[0] is MouseButton);

            return GameMouse.Instance.IsClicked((MouseButton)parameters[0]);
        }

        private static bool IsScreenTapped(params object[] parameters)
        {
            return GameTouchPad.Instance.IsScreenTouched;
        }

        #endregion

        #endregion
    }
}
