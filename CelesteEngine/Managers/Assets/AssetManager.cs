using CelesteEngineData;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Media;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;

namespace CelesteEngine
{
    /// <summary>
    /// A class to load and cache all the assets (including XML data).
    /// This will be done on startup in the Game1 LoadContent function
    /// </summary>
    public static class AssetManager
    {
        #region File Paths

        public const string SpriteFontsPath = "SpriteFonts";
        public const string SpritesPath = "Sprites";
        public const string EffectsPath = "Effects";
        public static string DataPath = "Data";
        public static string MusicPath = "Music";
        public static string SFXPath = "SFX";
        public static string OptionsPath = Path.Combine("Options", "Options");
        public static string PlayerGameDataPath = Path.Combine("Options", "PlayerGameData");

        #endregion

        #region Default Assets

        // UI
        public const string MouseTextureAsset = "UI\\Cursor";
        public const string DefaultSpriteFontAsset = "DefaultSpriteFont";
        public const string StartupLogoTextureAsset = "UI\\Logo";

        // Buttons
        public const string DefaultButtonTextureAsset = "UI\\Button";
        public const string DefaultButtonHighlightedTextureAsset = "UI\\ButtonHighlighted";
        public const string DefaultNarrowButtonTextureAsset = "UI\\NarrowButton";
        public const string DefaultNarrowButtonHighlightedTextureAsset = "UI\\NarrowButtonHighlighted";

        // Text boxes and Dialog boxes
        public const string DefaultTextBoxTextureAsset = "UI\\Menu";

        // Sliders
        public const string DefaultSliderBarTextureAsset = "UI\\SliderBar";
        public const string DefaultSliderHandleTextureAsset = "UI\\BlueSliderDown";

        // Bars
        public const string DefaultBarForegroundTextureAsset = "UI\\BarBackground";
        public const string DefaultBarBackgroundTextureAsset = "UI\\BarBackground";

        // Text Entry box
        public const string DefaultTextEntryBoxTextureAsset = "UI\\TextEntryBox";

        // Panels and Menus
        public const string DefaultEmptyTextureAsset = "UI\\EmptyPanelBackground";
        public const string DefaultMenuTextureAsset = "UI\\Menu";

        // Lights
        public const string DefaultPointLightTextureAsset = "PointLightMask";
        public const string AmbientLightTextureAsset = "AmbientLightMask";

        // Game Objects
        public const string EmptyGameObjectDataAsset = "GameObjects\\Empty";

        #endregion

        #region Properties

        private static Dictionary<string, SpriteFont> SpriteFonts = new Dictionary<string, SpriteFont>();
        private static Dictionary<string, Texture2D> Sprites = new Dictionary<string, Texture2D>();
        private static Dictionary<string, Effect> Effects = new Dictionary<string, Effect>();
        private static Dictionary<string, BaseData> Data = new Dictionary<string, BaseData>();
        private static Dictionary<string, SoundEffect> SoundEffects = new Dictionary<string, SoundEffect>();
        private static Dictionary<string, Song> Songs = new Dictionary<string, Song>();

        #endregion

        /// <summary>
        /// Loads all the assets from the default spritefont, sprites and data directories.
        /// Formats them into dictionaries so that they can be obtained with just the filename (minus the filetype)
        /// </summary>
        /// <param name="content"></param>
        public static void LoadAssets(ContentManager content)
        {
            SpriteFonts = Load<SpriteFont>(content, SpriteFontsPath);
            Sprites = Load<Texture2D>(content, SpritesPath);
            Effects = Load<Effect>(content, EffectsPath);
            Data = Load<BaseData>(content, DataPath);
            Songs = Load<Song>(content, MusicPath);
            SoundEffects = Load<SoundEffect>(content, SFXPath);
        }

        /// <summary>
        /// Loads all the assets of an inputted type that exist in our Content folder
        /// </summary>
        /// <typeparam name="T">The type of asset to load</typeparam>
        /// <param name="content">The ContentManager we will use to load our content</param>
        /// <param name="path">The path of the assets we wish to load</param>
        /// <returns>Returns the dictionary of all loading content</returns>
        private static Dictionary<string, T> Load<T>(ContentManager content, string path)
        {
            Dictionary<string, T> objects = new Dictionary<string, T>();
            List<string> files = AssetCollectionManager.AssetCollectionTechnique != null ?  AssetCollectionManager.AssetCollectionTechnique.GetAllXnbFilesInDirectory(content, path) : new List<string>();

            for (int i = 0; i < files.Count; i++)
            {
                // Remove the directoryPath from the start of the string
                files[i] = files[i].Remove(0, path.Length + 2);

                // Remove the .xnb at the end
                files[i] = files[i].Split('.')[0];

                try
                {
                    objects.Add(files[i], LoadAssetFromDisc<T>(Path.Combine(path, files[i])));
                }
                catch { DebugUtils.Fail("Problem loading asset: " + files[i]); }
            }

            return objects;
        }
        
        /// <summary>
        /// A wrapper for loading content directly using the ContentManager.
        /// Should only be used as a last resort.
        /// </summary>
        /// <typeparam name="T">The type of content to load</typeparam>
        /// <param name="path">The full path of the object from the ContentManager directory e.g. Sprites\\UI\\Cursor</param>
        /// <param name="extension">The extension of the file we are trying to load - used for error checking</param>
        /// <returns>The loaded content</returns>
        private static T LoadAssetFromDisc<T>(string path, bool createIfDoesNotExist = false)
        {
            ContentManager content = ScreenManager.Instance.Content;
            T asset = default(T);

            // Because File.Exists relies on extensions and we do not use extensions, we cannot do a test here.
            // We use try catch here instead.
            // Ouch - I know, but there's no real nice workaround, unless we can check without extensions
            try
            {
                asset = content.Load<T>(path);
            }
            catch
            {
                if (createIfDoesNotExist)
                {
                    asset = (T)Activator.CreateInstance(typeof(T));
                }
                else
                {
                    asset = default(T);
                }
            }
            
            DebugUtils.AssertNotNull(asset);
            return asset;
        }
        
        #region Utility Functions

        /// <summary>
        /// Get a loaded SpriteFont
        /// </summary>
        /// <param name="path">The full path of the SpriteFont, e.g. "DefaultSpriteFont"</param>
        /// <returns>Returns the sprite font</returns>
        public static SpriteFont GetSpriteFont(string path)
        {
            SpriteFont spriteFont;

            if (!SpriteFonts.TryGetValue(path, out spriteFont))
            {
                spriteFont = LoadAssetFromDisc<SpriteFont>(Path.Combine(SpriteFontsPath, path));
            }

            DebugUtils.AssertNotNull(spriteFont);
            return spriteFont;
        }

        /// <summary>
        /// Get a pre-loaded sprite
        /// </summary>
        /// <param name="path">The full path of the Sprite, e.g. "UI\\Cursor"</param>
        /// <returns>Returns the texture</returns>
        public static Texture2D GetSprite(string path)
        {
            Texture2D sprite;

            if (!Sprites.TryGetValue(path, out sprite))
            {
                sprite = LoadAssetFromDisc<Texture2D>(Path.Combine(SpritesPath, path));
            }

            DebugUtils.AssertNotNull(sprite);
            return sprite;
        }

        /// <summary>
        /// Get a pre-loaded effect
        /// </summary>
        /// <param name="path">The full path of the Effect, e.g. "LightEffect"</param>
        /// <returns>Returns the effect</returns>
        public static Effect GetEffect(string path)
        {
            Effect effect;

            if (!Effects.TryGetValue(path, out effect))
            {
                effect = LoadAssetFromDisc<Effect>(Path.Combine(EffectsPath, path));
            }

            DebugUtils.AssertNotNull(effect);
            return effect;
        }

        /// <summary>
        /// Get a pre-loaded sound effect
        /// </summary>
        /// <param name="path">The full path of the SoundEffect, e.g. "UI\\ButtonHover"</param>
        /// <returns></returns>
        public static SoundEffect GetSoundEffect(string path)
        {
            SoundEffect soundEffect;

            if (!SoundEffects.TryGetValue(path, out soundEffect))
            {
                soundEffect = LoadAssetFromDisc<SoundEffect>(Path.Combine(SFXPath, path));
            }

            DebugUtils.AssertNotNull(soundEffect);
            return soundEffect;
        }

        /// <summary>
        /// Get a pre-loaded music file
        /// </summary>
        /// <param name="path">The full path of the SoundEffect, e.g. "Menu\\Emotional"</param>
        /// <returns></returns>
        public static Song GetMusic(string path)
        {
            Song song;

            if (!Songs.TryGetValue(path, out song))
            {
                song = LoadAssetFromDisc<Song>(Path.Combine(MusicPath, path));
            }

            DebugUtils.AssertNotNull(song);
            return song;
        }

        /// <summary>
        /// Get a pre-loaded data file.
        /// If it cannot be accessed, it loads it from disc.
        /// </summary>
        /// <typeparam name="T">The type we wish to case the data to</typeparam>
        /// <param name="name">The full path of the data file relative to the Data directory, e.g. Screens\\BaseScreenData</param>
        /// <returns>Returns the data</returns>
        public static T GetData<T>(string name, bool createIfDoesNotExist = false) where T : BaseData
        {
            BaseData data = null;

            if (Data.TryGetValue(name, out data))
            {
                Debug.Assert(data is T);
                return data as T;
            }

            return LoadAssetFromDisc<T>(Path.Combine(DataPath, name), createIfDoesNotExist);
        }

        /// <summary>
        /// Returns a list of all the key value pairs in our dictionary who's data is castable to type T
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static List<T> GetAllDataOfType<T>() where T : BaseData
        {
            List<T> data = new List<T>();

            foreach (KeyValuePair<string, BaseData> registeredPair in Data)
            {
                if (registeredPair.Value is T)
                {
                    data.Add(registeredPair.Value as T);
                }
            }

            return data;
        }

        /// <summary>
        /// Returns a list of all the key value pairs in our dictionary who's data is castable to type T
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static List<KeyValuePair<string, T>> GetAllDataPairsOfType<T>() where T : BaseData
        {
            List<KeyValuePair<string, T>> data = new List<KeyValuePair<string, T>>();

            foreach (KeyValuePair<string, BaseData> registeredPair in Data)
            {
                if (registeredPair.Value is T)
                {
                    data.Add(new KeyValuePair<string, T>(registeredPair.Key, registeredPair.Value as T));
                }
            }

            return data;
        }

        /// <summary>
        /// Saves data to an XML file on disc.
        /// </summary>
        /// <typeparam name="T">THe type of data we wish to save</typeparam>
        /// <param name="data">The data we are saving</param>
        /// <param name="path">The full path of where to save the data e.g. "Screens\\MainMenuScreen"</param>
        public static void SaveData<T>(T data, string path) where T : BaseData
        {
            DebugUtils.AssertNotNull(data);
            DebugUtils.Fail("TODO");
            //XmlDataSerializer.Serialize(data, ScreenManager.Instance.Content.FullRootDirectory() + "\\" + DataPath + path);
        }

#endregion
    }
}