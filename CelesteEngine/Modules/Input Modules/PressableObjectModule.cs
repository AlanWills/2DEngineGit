using Microsoft.Xna.Framework;
using System.Diagnostics;

namespace CelesteEngine
{
    /// <summary>
    /// A module to allow press interaction with the attached object
    /// </summary>
    public class PressableObjectModule : BaseObjectModule, IPressable
    {
        #region Properties and Fields

        /// <summary>
        /// These represent a function which we want to execute when this object is pressed by the appropriate mouse button/touchpad.
        /// To do this, construct a function of the form:
        /// public void Function(Button button) { ... }
        /// 
        /// Then do OnClicked += Function;
        /// 
        /// In fact, more than one function can be set up in this way.
        /// </summary>
        public event OnPressed OnLeftPressed;
        public event OnPressed OnMiddlePressed;
        public event OnPressed OnRightPressed;
        public event OnPressed OnReleased;

        /// <summary>
        /// A bool we will use to track the press state of last frame so that we can trigger the OnReleased event.
        /// </summary>
        private bool PressedLastFrame { get; set; }

        #endregion

        #region Virtual Functions

        /// <summary>
        /// Checks to see whether the attached base object's collider is pressed and if so triggers the appropriate event
        /// </summary>
        /// <param name="elapsedGameTime"></param>
        /// <param name="mousePosition"></param>
        public override void HandleInput(float elapsedGameTime, Vector2 mousePosition)
        {
            base.HandleInput(elapsedGameTime, mousePosition);

            DebugUtils.AssertNotNull(AttachedBaseObject.Collider);

            if (AttachedBaseObject.Collider.IsPressed)
            {
                Debug.Assert(OnLeftPressed != null && OnMiddlePressed != null && OnRightPressed != null);

                if (OnLeftPressed != null &&
                    InputManager.Instance.CheckInputEvent(InputManager.ScreenPressed, new object[] { MouseButton.kLeftButton }))
                {
                    OnLeftPressed.Invoke(AttachedBaseObject);
                }
                else if (OnMiddlePressed != null &&
                    InputManager.Instance.CheckInputEvent(InputManager.ScreenPressed, new object[] { MouseButton.kMiddleButton }))
                {
                    OnMiddlePressed.Invoke(AttachedBaseObject);
                }
                else if (OnRightPressed != null &&
                    InputManager.Instance.CheckInputEvent(InputManager.ScreenPressed, new object[] { MouseButton.kRightButton }))
                {
                    OnRightPressed.Invoke(AttachedBaseObject);
                }
            }
            else
            {
                if (PressedLastFrame)
                {
                    OnReleased.Invoke(AttachedBaseObject);
                }
            }

            PressedLastFrame = AttachedBaseObject.Collider.IsPressed;
        }

        #endregion
    }
}
