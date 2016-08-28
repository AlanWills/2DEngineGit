using Microsoft.Xna.Framework.Input.Touch;

namespace CelesteEngine
{
    /// <summary>
    /// A class which will monitor the touch input from the screen with devices which support it.
    /// Can be queried for use in InputManager events.
    /// </summary>
    public class GameTouchPad : Component
    {
        #region Properties and Fields

        /// <summary>
        /// The static instance we will access this class through.
        /// </summary>
        private static GameTouchPad instance;
        public static GameTouchPad Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new GameTouchPad();
                }

                return instance;
            }
        }

        /// <summary>
        /// The touch collection for this frame - use this in conjunction with the 'IsScreenTouched' flag to check for touch input
        /// </summary>
        public TouchCollection TouchCollection { get; private set; }

        public bool IsScreenTouched
        {
            get
            {
                return TouchCollection.Count > 0;
            }
        }

        #endregion

        #region Virtual Functions

        /// <summary>
        /// Updates this class's touch collection information based on the touch panel state.
        /// </summary>
        /// <param name="elapsedGameTime"></param>
        public override void Update(float elapsedGameTime)
        {
            base.Update(elapsedGameTime);

            TouchCollection touch = TouchPanel.GetState();
        }

        #endregion
    }
}
