﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System.Diagnostics;

namespace CelesteEngine
{
    /// <summary>
    /// A module for objects we wish to have click interaction with.
    /// </summary>
    public class ClickableObjectModule : BaseObjectModule, IClickable
    {
        #region Properties and Fields

        /// <summary>
        /// Optional keyboard keys to trigger the click events if set
        /// </summary>
        public Keys LeftClickAccelerator { get; set; }
        public Keys MiddleClickAccelerator { get; set; }
        public Keys RightClickAccelerator { get; set; }

        /// <summary>
        /// These represent a function which we want to execute when this object is clicked by the appropriate mouse button.
        /// To do this, construct a function of the form:
        /// public void Function(Button button) { ... }
        /// 
        /// Then do OnClicked += Function;
        /// 
        /// In fact, more than one function can be set up in this way.
        /// </summary>
        public event OnClicked OnLeftClicked;
        public event OnClicked OnMiddleClicked;
        public event OnClicked OnRightClicked;

        /// <summary>
        /// A variable to mark the current status of the clickable object
        /// </summary>
        private ClickState clickState;
        public ClickState ClickState
        {
            get { return clickState; }
            private set { clickState = value; }
        }

        /// <summary>
        /// A timer to prevent multiply clicks happening in quick succession.
        /// </summary>
        private float CurrentClickTimer { get; set; }

        private const float clickResetTime = 0.05f;
        private const string AcceleratorPressedEvent = "AcceleratorPressedEvent";

        #endregion

        public ClickableObjectModule()
        {
            // Set so that we can immediately click the button
            CurrentClickTimer = clickResetTime;
            ClickState = ClickState.kIdle;

            // By default, the accelerators are set to Keys.None so will not be used
            // Add the accelerator check events to the InputManager
            InputManager.Instance.AddInputEvent(AcceleratorPressedEvent, IsAcceleratorPressed, InputManager.EmptyCheck);
        }

        #region Virtual Functions

        /// <summary>
        /// Update the click state and call the click event if the attached object is clicked
        /// </summary>
        /// <param name="elapsedGameTime"></param>
        /// <param name="mousePosition"></param>
        public override void HandleInput(float elapsedGameTime, Vector2 mousePosition)
        {
            base.HandleInput(elapsedGameTime, mousePosition);

            DebugUtils.AssertNotNull(AttachedBaseObject.Collider);
            if (AttachedBaseObject.Collider.IsMouseOver && ClickState != ClickState.kPressed)
            {
                ClickState = ClickState.kHighlighted;
            }

            // If we have clicked it and it is not already clicked, fire the appropriate event
            if (ClickState != ClickState.kPressed)
            {
                // We have to check the IsClicked Collider flag for each event, because our accelerator does not require the object to be clicked to fire the event

                // One of our click methods should not be null otherwise why use a clickable module?!
                Debug.Assert(OnLeftClicked != null || OnMiddleClicked != null || OnRightClicked != null);

                // If our corresponding event is null and we have either clicked the appropriate mouse button and we are clicked or have an accelerator key set up which has been pressed, then fire the event
                if (OnLeftClicked != null &&
                    ((AttachedBaseObject.Collider.IsClicked && InputManager.Instance.CheckInputEvent(InputManager.ScreenClicked, new object[] { MouseButton.kLeftButton })) ||
                    InputManager.Instance.CheckInputEvent(AcceleratorPressedEvent, new object[] { LeftClickAccelerator })))
                {
                    OnLeftClicked(AttachedBaseObject);
                }
                else if (OnMiddleClicked != null &&
                        ((AttachedBaseObject.Collider.IsClicked && InputManager.Instance.CheckInputEvent(InputManager.ScreenClicked, new object[] { MouseButton.kMiddleButton })) ||
                        InputManager.Instance.CheckInputEvent(AcceleratorPressedEvent, new object[] { MiddleClickAccelerator })))
                {
                    OnMiddleClicked(AttachedBaseObject);
                }
                else if (OnRightClicked != null &&
                        ((AttachedBaseObject.Collider.IsClicked && InputManager.Instance.CheckInputEvent(InputManager.ScreenClicked, new object[] { MouseButton.kRightButton })) ||
                        InputManager.Instance.CheckInputEvent(AcceleratorPressedEvent, new object[] { RightClickAccelerator })))
                {
                    OnRightClicked(AttachedBaseObject);
                }
                else
                {
                    // Don't update click state if we haven't been clicked on
                    return;
                }

                ClickState = ClickState.kPressed;
                CurrentClickTimer = 0;
            }
        }

        /// <summary>
        /// Updates the current click timer for this object to see if it can be clicked again
        /// </summary>
        /// <param name="elapsedGameTime"></param>
        public override void Update(float elapsedGameTime)
        {
            base.Update(elapsedGameTime);

            CurrentClickTimer += elapsedGameTime;
            if (CurrentClickTimer >= clickResetTime)
            {
                ClickState = ClickState.kIdle;
            }
        }

        #endregion

        #region InputManager Check Functions

        /// <summary>
        /// Checks to see whether the keyboard has registered that the key shortcut corresponding to a left click has been pressed.
        /// </summary>
        /// <returns></returns>
        private bool IsAcceleratorPressed(object[] parameters)
        {
            Debug.Assert(parameters.Length == 1);
            Debug.Assert(parameters[0] is Keys);

            return GameKeyboard.Instance.IsKeyPressed((Keys)parameters[0]);
        }

        #endregion

        #region Utility Functions

        /// <summary>
        /// Allows us to simulate this button being clicked and call it's appropriate events.
        /// Will call the events if they exist no matter what
        /// </summary>
        public void ForceClick()
        {
            // One of our click methods should not be null otherwise why use a clickable image?!
            Debug.Assert(OnLeftClicked != null || OnMiddleClicked != null || OnRightClicked != null);

            // If our corresponding event is null and we have either clicked the appropriate mouse button and we are clicked or have an accelerator key set up which has been pressed, then fire the event
            if (OnLeftClicked != null)
            {
                OnLeftClicked(AttachedBaseObject);
            }
            else if (OnMiddleClicked != null)
            {
                OnMiddleClicked(AttachedBaseObject);
            }
            else if (OnRightClicked != null)
            {
                OnRightClicked(AttachedBaseObject);
            }
            else
            {
                // We have been clicked on, but either not by the right mouse, or we have not got an event for that mouse button set up, so just return without updating the ClickState etc.
                return;
            }

            ClickState = ClickState.kPressed;
            CurrentClickTimer = 0;
        }

        /// <summary>
        /// An empty pass through event to use for objects that obtain the ClickableModule by inheritance, but have no click function themselves
        /// </summary>
        /// <param name="clickedObject"></param>
        public static void EmptyClick(BaseObject clickedObject)
        {

        }

        #endregion
    }
}
