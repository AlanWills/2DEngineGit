using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System.Diagnostics;

namespace CelesteEngine
{
    /// <summary>
    /// An enum to describe the different behaviours that our camera can perform
    /// </summary>
    public enum CameraMode
    {
        kFree,      // The camera will move anywhere based on certain input - mouse at edge of screen, or keyboard
        kFixed,     // The camera will not move at all - useful for menu screens
        kFollow,    // The camera will follow an object - this requires the object to follow to be passed in
    }

    /// <summary>
    /// A static class used as an in game camera.
    /// This will also allow us to not render objects if they are not contained within the camera viewport
    /// </summary>
    public class Camera : Component
    {
        #region Properties and Fields

        private static Camera instance;
        public static Camera Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new Camera();
                }

                return instance;
            }
        }

        /// <summary>
        /// A private variable used to determine what behaviour the camera should perform.
        /// This should remain private and instead, to change the camera mode, the appropriate functions 'SetFree', 'SetFixed', 'SetFollow'
        /// should be called.  This is because extra parameters are required for each mode.
        /// </summary>
        private CameraMode CameraMode { get; set; }

        /// <summary>
        /// The position of the Camera - corresponds to where the top left of the screen is.  Again we should not be able to set this outside of this class.
        /// Ways to change the camera position are determined by the CameraMode, and also by a parameter passed in when changing mode ONLY.
        /// </summary>
        public Vector2 Position { get; private set; }

        /// <summary>
        /// A value to determine how fast the camera should move in Free mode.  This should be freely available for alteration.
        /// </summary>
        public float PanSpeed { get; set; }

        /// <summary>
        /// A value to determine how much we increase or decrease the camera zoom by when pressing + or -.
        /// </summary>
        public float ZoomIncrement { get; set; }

        /// <summary>
        /// A value only to be altered and seen by this class, used to determine the zoom of the camera.
        /// This value can be changed in Free or Follow mode by keyboard input ONLY.
        /// </summary>
        private float Zoom { get; set; }

        /// <summary>
        /// This represents the current transformation of the camera calculated from the position and zoom.
        /// It is used as a parameter to SpriteBatch.Begin() to give the impression of a camera when drawing objects
        /// </summary>
        public Matrix TransformationMatrix
        {
            get
            {
                // This could be done with usual operators - *, + etc., but this is an optimisation
                return Matrix.Multiply(Matrix.CreateTranslation(Position.X, Position.Y, 0), Matrix.CreateScale(Zoom));
            }
        }

        /// <summary>
        /// Corresponds to the window of our game - will have coordinates (0, 0, screen width, screen height).
        /// This will be used in determining whether an object is visible or not.
        /// It will need to be set up once, or when our screen size changes.
        /// </summary>
        public Rectangle ViewportRectangleScreenSpace { get; private set; }

        /// <summary>
        /// Corresponds to our viewport rectangle but in game space - means we can check objects in the game world to see whether we should draw them.
        /// </summary>
        public Rectangle ViewportRectangleGameSpace { get; private set; }

        /// <summary>
        /// An extra vector2 used in the calculation of the pan amount for the camera
        /// </summary>
        private Vector2 PanDelta { get; set; }

        /// <summary>
        /// A reference to an object that we will be following when in Follow Mode
        /// </summary>
        private BaseObject FollowingObject { get; set; }

        #region Camera Input Event Names

        private const string CameraPan = "CameraPan";

        #endregion

        #endregion

        private Camera()
        {
            CameraMode = CameraMode.kFixed;
            PanSpeed = 500;
            Zoom = 1;
            ViewportRectangleScreenSpace = new Rectangle(0, 0, ScreenManager.Instance.Viewport.Width, ScreenManager.Instance.Viewport.Height);

            // This can be improved upon by using zoom to affect the length
            ViewportRectangleGameSpace = new Rectangle((int)-Position.X, (int)-Position.Y, ViewportRectangleScreenSpace.Width, ViewportRectangleScreenSpace.Height);
        }

        #region Virtual Functions

        /// <summary>
        /// Sets up input events for camera behaviour.
        /// This should happen here rather than the constructor because we may wish to edit the Camera properties from the constructor of the input manager
        /// </summary>
        public override void Initialise()
        {
            CheckShouldInitialise();

            InputManager.Instance.AddInputEvent(CameraPan, IsKeyDown, InputManager.EmptyCheck);

            base.Initialise();
        }

        /// <summary>
        /// Updates the camera position based on what mode we are in and any appropriate user input - should be called every frame
        /// </summary>
        /// <param name="elapsedGameTime"></param>
        public override void HandleInput(float elapsedGameTime, Vector2 mousePosition)
        {
            base.HandleInput(elapsedGameTime, mousePosition);

            // If we are in fixed camera mode, we should not update anything
            if (CameraMode == CameraMode.kFixed || CameraMode == CameraMode.kFollow) { return; }

            // Handles zooming in or out
            if (GameKeyboard.Instance.IsKeyPressed(InputMap.ZoomIn))
            {
                Zoom += ZoomIncrement;
            }
            else if (GameKeyboard.Instance.IsKeyPressed(InputMap.ZoomOut))
            {
                Zoom -= ZoomIncrement;
                Zoom = MathHelper.Clamp(Zoom, ZoomIncrement, float.MaxValue);
            }

            // We will be updating the position using keyboard input from now on - this only applies to Free mode
            if (CameraMode == CameraMode.kFollow) { return; }

            PanDelta = Vector2.Zero;

            // Handles panning in all four directions
            InputManager.Instance.CheckInputEvent(CameraPan, new object[] { Keys.Left, new Vector2(1, 0) });
            InputManager.Instance.CheckInputEvent(CameraPan, new object[] { Keys.Right, new Vector2(-1, 0) });
            InputManager.Instance.CheckInputEvent(CameraPan, new object[] { Keys.Up, new Vector2(0, 1) });
            InputManager.Instance.CheckInputEvent(CameraPan, new object[] { Keys.Down, new Vector2(0, -1) });

            if (PanDelta != Vector2.Zero)
            {
                PanDelta.Normalize();
                Position += PanDelta * PanSpeed * elapsedGameTime;
            }
        }

        /// <summary>
        /// Updates the camera position based on what mode we are in - should be called every frame
        /// </summary>
        /// <param name="elapsedSeconds"></param>
        public override void Update(float elapsedGameTime)
        {
            base.Update(elapsedGameTime);

            // This can be improved upon by using zoom to affect the length
            ViewportRectangleGameSpace = new Rectangle((int)-Position.X, (int)-Position.Y, ViewportRectangleScreenSpace.Width, ViewportRectangleScreenSpace.Height);

            // Only the follow mode requires no user input to update
            if (CameraMode != CameraMode.kFollow) { return; }

            FocusOnPosition(FollowingObject.WorldPosition, false);
        }

        #endregion

        #region Utility Functions

        /// <summary>
        /// A function used to position the camera so that the inputted position is in the centre of the screen.
        /// </summary>
        /// <param name="focusPosition">The position that we wish to have in the centre of the screen</param>
        /// <param name="screenSpace">A flag to indicate whether the focus position is in screen space or game space.  The camera will transform the point if necessary</param>
        public void FocusOnPosition(Vector2 focusPosition, bool screenSpace)
        {
            if (!screenSpace)
            {
                // Transform into screen space if necessary
                focusPosition = GameToScreenCoords(focusPosition);
            }

            /// Since the camera position corresponds to the opposite of what we expect (just look at movement in update function) and the top left is the position, we subtract the focus position from the screen centre
            Position = new Vector2(ViewportRectangleScreenSpace.Width, ViewportRectangleScreenSpace.Height) * 0.5f - focusPosition;
        }

        /// <summary>
        /// A function for converting a position on the screen into game space, using the zoom and position of the camera
        /// </summary>
        /// <param name="screenPosition">The position on the screen between (0, 0) and (screen width, screen height)</param>
        /// <returns>The game space coordinates corresponding to the inputted screen position</returns>
        public Vector2 ScreenToGameCoords(Vector2 screenPosition)
        {
            // This could be done using ordinary mathematical operators - +, / etc. but this is an optimisation
            return Vector2.Divide(Vector2.Add(Position, screenPosition), Zoom);
        }

        /// <summary>
        /// A function for converting a position in game space into screen space, using the zoom and position of the camera
        /// </summary>
        /// <param name="gamePosition">The position in game space.</param>
        /// <returns>The screen space coordinates corresponding to the inputted game position</returns>
        public Vector2 GameToScreenCoords(Vector2 gamePosition)
        {
            // This could be done using ordinary mathematical operators - +, / etc. but this is an optimisation
            return Vector2.Subtract(Vector2.Multiply(gamePosition, Zoom), Position);
        }

        /// <summary>
        /// Checks each object in the inputted ObjectManager to determine whether it should be drawn this frame.
        /// </summary>
        /// <param name="objectManager"></param>
        /// <param name="screenSpace"></param>
        public void CheckVisibility<T>(ObjectManager<T> objectManager, bool screenSpace) where T : BaseObject
        {
            Rectangle rectangleToUse = screenSpace ? ViewportRectangleScreenSpace : ViewportRectangleGameSpace;
            Vector2 centre = rectangleToUse.Center.ToVector2();

            foreach (BaseObject baseObject in objectManager)
            {
                // If our object's visibility is driven by another object we do not need to perform visibility checks
                if (baseObject.ShouldDraw)
                {
                    continue;
                }

                if (baseObject.UsesCollider)
                {
                    DebugUtils.AssertNotNull(baseObject.Collider);
                    baseObject.ShouldDraw = baseObject.Collider.CheckIntersects(rectangleToUse);
                }
                else
                {
                    // Crude circle check, can definitely be improved upon
                    float x = rectangleToUse.Width + baseObject.Size.X;
                    float y = rectangleToUse.Height + baseObject.Size.Y;
                    baseObject.ShouldDraw = (centre - baseObject.WorldPosition).LengthSquared() <= x * x + y * y;
                }
            }
        }

        #endregion

        #region Changing Camera Mode Functions

        /// <summary>
        /// Sets the CameraMode to free
        /// </summary>
        public void SetFree()
        {
            CameraMode = CameraMode.kFree;
            FollowingObject = null;
        }

        /// <summary>
        /// Sets the CameraMode to free and the camera's position to the inputted value.
        /// </summary>
        /// <param name="resetPosition">The new value of the camera's position</param>
        public void SetFree(Vector2 resetPosition)
        {
            SetFree();
            Position = resetPosition;
        }

        /// <summary>
        /// Sets the CameraMode to follow
        /// </summary>
        public void SetFollow(BaseObject followingObject)
        {
            CameraMode = CameraMode.kFollow;
            FollowingObject = followingObject;
        }

        /// <summary>
        /// Sets the CameraMode to fixed
        /// </summary>
        public void SetFixed()
        {
            CameraMode = CameraMode.kFixed;
            FollowingObject = null;
        }

        /// <summary>
        /// Sets the CameraMode to Fixed and the camera's position to the inputted value
        /// </summary>
        /// <param name="resetPosition">The new value of the camera's position</param>
        public void SetFixed(Vector2 fixedPosition)
        {
            SetFixed();
            Position = fixedPosition;
        }

        #endregion

        #region Input Manager Check Funcs

        /// <summary>
        /// Custom callbacks which allow us to set our PanDelta
        /// </summary>
        /// <param name="parameters"></param>
        /// <returns></returns>
        private bool IsKeyDown(params object[] parameters)
        {
            Debug.Assert(parameters.Length == 2);
            DebugUtils.AssertNotNull((Vector2)parameters[1]);
            bool result = GameKeyboard.IsKeyDown(parameters);

            if (result)
            {
                PanDelta += (Vector2)parameters[1];
            }

            return result;
        }

        /// <summary>
        /// Custom callbacks which allow us to set our PanDelta
        /// </summary>
        /// <param name="parameters"></param>
        /// <returns></returns>
        private bool IsFreeDragged(params object[] parameters)
        {
            bool result = GameTouchPanel.IsFreeDragged(parameters);

            if (result && PanDelta == Vector2.Zero)
            {
                PanDelta += GameTouchPanel.Instance.Gesture.Delta;
            }

            return result;
        }

        #endregion
    }
}
