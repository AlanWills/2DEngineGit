using Microsoft.Xna.Framework.Audio;
using System.Collections.Generic;
using System.Diagnostics;

namespace CelesteEngine
{
    public static class SFXManager
    {
        #region Properties and Fields

        /// <summary>
        /// A dictionary of sound effect names to loaded sound effects
        /// </summary>
        public static Dictionary<string, SoundEffect> SoundEffects { private get; set; }

        #endregion

        #region Methods
        
        /// <summary>
        /// Returns the stored sound effect with the inputted key
        /// </summary>
        /// <param name="sfxName"></param>
        /// <returns></returns>
        public static SoundEffect GetSoundEffect(string sfxName)
        {
            Debug.Assert(SoundEffects.ContainsKey(sfxName));
            return SoundEffects[sfxName];
        }

        /// <summary>
        /// Returns an instance of the sound effect with the inputted key
        /// </summary>
        /// <param name="sfxName"></param>
        /// <returns></returns>
        public static SoundEffectInstance CreateInstance(string sfxName)
        {
            return GetSoundEffect(sfxName).CreateInstance();
        }

#endregion
    }
}
