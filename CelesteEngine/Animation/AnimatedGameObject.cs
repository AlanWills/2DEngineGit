using CelesteEngineData;
using Microsoft.Xna.Framework;

namespace CelesteEngine
{
    /// <summary>
    /// A simple game object class which implements the AnimationModule
    /// </summary>
    public class AnimatedGameObject : GameObject
    {
        #region Properties and Fields

        /// <summary>
        /// The animation module for our animation
        /// </summary>
        public AnimationModule AnimationModule { get; private set; }

        #endregion

        public AnimatedGameObject(Vector2 localPosition, string animationDataAsset) :
            base(localPosition, animationDataAsset)
        {
            UsesCollider = false;
            AnimationModule = AddModule(new AnimationModule());
        }
        
        #region Virtual Functions

        /// <summary>
        /// Begins playing our animation
        /// </summary>
        public override void Begin()
        {
            base.Begin();

            AnimationModule.IsPlaying = true;
        }

        #endregion
    }
}
