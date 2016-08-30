using System;
using System.Diagnostics;

namespace CelesteEngine
{
    /// <summary>
    /// A class which is basically an Image, but loads it's texture through the ContentManager rather than through the AssetManager.
    /// ONLY to be used in the start up logo screen.
    /// </summary>
    public class Logo : Image
    {
        public Logo() :
            base(ScreenManager.Instance.ScreenCentre, AssetManager.StartupLogoTextureAsset)
        {
        }

        #region Virtual Functions

        /// <summary>
        /// Load the logo from the ContentManager directly rather than from the AssetManager.
        /// This is because it will be created before the Assets are loaded.
        /// </summary>
        public override void LoadContent()
        {
            CheckShouldLoad();

            Texture = AssetManager.GetSprite(TextureAsset);

            base.LoadContent();
        }

        /// <summary>
        /// Set the size of the logo so that it fits onto the screen.
        /// </summary>
        public override void Initialise()
        {
            CheckShouldInitialise();

            base.Initialise();

            float rescale = Math.Min(ScreenManager.Instance.ScreenDimensions.X / Size.X, ScreenManager.Instance.ScreenDimensions.Y / Size.Y);
            if (rescale < 1)
            {
                Debug.Assert(rescale > 0);
                Size *= rescale;
            }
        }

        #endregion
    }
}