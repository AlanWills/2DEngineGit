using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input.Touch;
using System;
using System.Diagnostics;

namespace CelesteEngine
{
    /// <summary>
    /// A class which will monitor the touch input from the screen with devices which support it.
    /// Can be queried for use in InputManager events.
    /// </summary>
    public class GameTouchPanel : Component
    {
        #region Properties and Fields

        /// <summary>
        /// The static instance we will access this class through.
        /// </summary>
        private static GameTouchPanel instance;
        public static GameTouchPanel Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new GameTouchPanel();
                }

                return instance;
            }
        }

        /// <summary>
        /// The touch collection for this frame - use this in conjunction with the 'IsScreenTouched' flag to check for touch input
        /// </summary>
        public TouchCollection TouchCollection { get; private set; }

        /// <summary>
        /// The most recent gesture that was performed this frame
        /// </summary>
        public GestureSample Gesture { get; private set; }

        public bool IsScreenTouched
        {
            get
            {
                return TouchCollection.Count > 0;
            }
        }

        /// <summary>
        /// Returns whether a gesture has occurred this frame
        /// </summary>
        public bool IsGestureAvailable { get; private set; }

        #endregion

        public GameTouchPanel()
        {
            TouchPanel.EnabledGestures = GestureType.FreeDrag;
        }

        #region Virtual Functions

        /// <summary>
        /// Updates this class's touch collection information based on the touch panel state.
        /// </summary>
        /// <param name="elapsedGameTime"></param>
        public override void Update(float elapsedGameTime)
        {
            base.Update(elapsedGameTime);

            TouchCollection = TouchPanel.GetState();

            IsGestureAvailable = TouchPanel.IsGestureAvailable;
            if (IsGestureAvailable)
            {
                Gesture = TouchPanel.ReadGesture();
            }
        }

        #endregion

        #region Input Manager Check Funcs

        /// <summary>
        /// Commonly used check function to determine whether the screen was tapped
        /// </summary>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public static bool IsScreenTapped(params object[] parameters)
        {
            return Instance.IsScreenTouched;
        }

        /// <summary>
        /// Commonly used check function to determine whether the screen was pressed
        /// </summary>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public static bool IsScreenPressed(params object[] parameters)
        {
            return Instance.IsScreenTouched;
        }

        /// <summary>
        /// Commonly used check function to determine whether the screen was free dragged, and that the sign of the delta matches the inputted direction
        /// </summary>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public static bool IsFreeDragged(params object[] parameters)
        {
            if (!Instance.IsGestureAvailable) { return false; }

            return Instance.Gesture.GestureType == GestureType.FreeDrag;
        }

        #endregion
    }
}
