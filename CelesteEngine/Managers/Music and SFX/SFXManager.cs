using Microsoft.Xna.Framework.Audio;

namespace CelesteEngine
{
    public static class SFXManager
    {
        #region Methods
        
        /// <summary>
        /// Returns an instance of the sound effect with the inputted key
        /// </summary>
        /// <param name="sfxName"></param>
        /// <returns></returns>
        public static SoundEffectInstance CreateInstance(string sfxName)
        {
            return AssetManager.GetSoundEffect(sfxName).CreateInstance();
        }

#endregion
    }
}
