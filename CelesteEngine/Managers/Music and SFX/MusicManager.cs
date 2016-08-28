﻿using Microsoft.Xna.Framework.Media;
using System.Collections.Generic;
using System.Diagnostics;

namespace CelesteEngine
{
    public enum QueueType { WaitForCurrent, PlayImmediately }

    public static class MusicManager
    {
        #region Properties and Fields

        /// <summary>
        /// The current playlist of songs
        /// </summary>
        private static List<string> currentPlaylist = new List<string>();
        private static List<string> CurrentPlaylist
        {
            get { return currentPlaylist; }
            set { currentPlaylist = value; }
        }

        /// <summary>
        /// The current song that is playing
        /// </summary>
        private static Song CurrentSong
        {
            get
            {
                Debug.Assert(CurrentSongIndex < CurrentPlaylist.Count);
                return AssetManager.GetMusic(CurrentPlaylist[CurrentSongIndex]);
            }
        }

        /// <summary>
        /// The index in our playlist of the current song playing
        /// </summary>
        private static int currentSongIndex;
        private static int CurrentSongIndex
        {
            get { return currentSongIndex; }
            set
            {
                Debug.Assert(CurrentPlaylist.Count > 0);
                currentSongIndex = value;
                currentSongIndex %= CurrentPlaylist.Count;
            }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Sets the current playlist.
        /// Depending on the QueueType, we will either let the current song that is playing finish, or just immediately start playing our first song in the new playlist
        /// </summary>
        /// <param name="songs"></param>
        /// <param name="queueType"></param>
        public static void SetPlaylist(List<string> songs, QueueType queueType = QueueType.WaitForCurrent)
        {
            // If we are not adding any songs, don't bother doing the rest of this function
            if (songs == null || songs.Count == 0)
                return;

            // If the song we are currently playing is being added again by the new playlist, move it to the back
            // This means we will have to listen to all the others before we hear it again
            if (CurrentPlaylist.Count > 0 && CurrentSong != null)
            {
                string currentSongName = CurrentSong.Name;
                if (songs.Contains(currentSongName))
                {
                    songs.Remove(currentSongName);
                    songs.Add(currentSongName);
                }
            }

            // Resets the current song index so we play from the start of the new playlist
            CurrentPlaylist = songs;
            CurrentSongIndex = 0;

            // If we have specified we wish to play immediately we skip the remainder of the current song and play the next one
            if (queueType == QueueType.PlayImmediately)
            {
                PlayNextSong();
            }
        }

        /// <summary>
        /// Plays the next song on in our current playlist.
        /// If we have reached the end, we start back at the beginning.
        /// </summary>
        public static void PlayNextSong()
        {
            MediaPlayer.Play(CurrentSong);
            CurrentSongIndex++;
        }

        /// <summary>
        /// Checks to see if the media player has reached the end of the current song it is playing, and if it has, begins playing the next one
        /// </summary>
        public static void Update()
        {
            if (CurrentPlaylist.Count > 0 && MediaPlayer.State == MediaState.Stopped)
            {
                // We have stopped playing the current song because we have reached the end, so play the next song
                PlayNextSong();
            }
        }

#endregion
    }
}
