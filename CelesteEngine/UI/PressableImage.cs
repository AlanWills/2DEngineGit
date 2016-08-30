using Microsoft.Xna.Framework;

namespace CelesteEngine
{
    /// <summary>
    /// A class to represent an image which executes a function when pressed.
    /// Really a UI wrapper around the PressableObjectModule.
    /// </summary>
    public class PressableImage : Image
    {
        #region Properties and Fields

        /// <summary>
        /// A reference to our pressable module.
        /// </summary>
        public PressableObjectModule PressableModule { get; private set; }

        #endregion

        public PressableImage(Vector2 localPosition, string textureAsset) :
            this(Vector2.Zero, localPosition, textureAsset)
        {
        }

        public PressableImage(Vector2 size, Vector2 localPosition, string textureAsset) :
            base(size, localPosition, textureAsset)
        {
            UsesCollider = true;

            PressableModule = AddModule(new PressableObjectModule());
            DebugUtils.AssertNotNull(PressableModule);
        }

        public PressableImage(Anchor anchor, int depth, string textureAsset) :
            this(Vector2.Zero, anchor, depth, textureAsset)
        {
        }

        public PressableImage(Vector2 size, Anchor anchor, int depth, string textureAsset) :
            base(size, anchor, depth, textureAsset)
        {
            UsesCollider = true;

            PressableModule = AddModule(new PressableObjectModule());
            DebugUtils.AssertNotNull(PressableModule);
        }
    }
}
