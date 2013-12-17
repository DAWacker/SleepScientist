using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Audio;

namespace SleepyScientist
{
    /// <summary>
    /// Manages playing music, sound effects, and voice overs.
    /// </summary>
    static class SoundManager
    {        
        #region Attributes

        private static Dictionary<String, List<SoundEffect>> _soundData = new Dictionary<String, List<SoundEffect>>();
        private static Stack<SoundEffectInstance> _curMusic = new Stack<SoundEffectInstance>();
        private static SoundEffectInstance _curVoiceOver;

        #endregion

        #region Methods

        /// <summary>
        /// Add audio to SoundManager.
        /// Specify a type to group the audio being added. This allows
        /// for multiple versions of a sound to exist (not every explosion sounds the same).
        /// </summary>
        /// <param name="type">The type of audio to add.</param>
        /// <param name="toAdd">The audio to add.</param>
        public static void Add(String type, SoundEffect toAdd)
        {
            List<SoundEffect> current;

            if (_soundData.ContainsKey(type))
                current = _soundData[type];
            else
                current = new List<SoundEffect>();

            current.Add(toAdd);
            _soundData[type] = current;
        }

        /// <summary>
        /// Plays music if it has been added to SoundManager.
        /// Before playing the new music, the current music gets paused.
        /// </summary>
        /// <param name="name">The name of the music to play.</param>
        public static void PlayMusic(String name)
        {
            SoundEffectInstance instance = SoundFromGroup(name);
            instance.IsLooped = true;

            // Pause the currently playing music then play the new music.
            if (_curMusic.Count > 0)
                _curMusic.Peek().Pause();

            _curMusic.Push(instance);
            instance.Play();
        }

        /// <summary>
        /// Stop the current music and remove it from stack of current songs.
        /// </summary>
        /// <param name="resumePrevious">Should the previous music be resumed?</param>
        public static void StopMusic(bool resumePrevious = true)
        {
            if (_curMusic.Count > 0)
                _curMusic.Pop().Stop();

            if (resumePrevious && _curMusic.Count > 0)
            {
                // Our version of Monogame currently does not support resuming.
                // However, I hear the 3.2 daily build fixed it.
                //_curMusic.Peek().Resume();
                _curMusic.Peek().Stop();
                _curMusic.Peek().Play();
            }
        }

        /// <summary>
        /// Stop and remove all music from the stack of current songs.
        /// </summary>
        public static void StopAllMusic()
        {
            while (_curMusic.Count > 0)
            {
                _curMusic.Pop().Stop();
            }
        }

        /// <summary>
        /// Play a voice over track.
        /// </summary>
        /// <param name="name">The voice over track group name.</param>
        /// <param name="index">The index of the voice over in the group.</param>
        public static void PlayVoiceOver(String name, int index = 0)
        {
            SoundEffectInstance instance = SoundFromGroup(name, index);
            _curVoiceOver = instance;
            instance.Play();
        }

        /// <summary>
        /// Pause the currently playing voice over track.
        /// </summary>
        public static void PauseVoiceOver()
        {
            _curVoiceOver.Pause();
        }

        /// <summary>
        /// Stop the currently playing voice over track.
        /// </summary>
        public static void StopVoiceOver()
        {
            _curVoiceOver.Stop();
        }

        /// <summary>
        /// Play an instance of the specified SoundEffect.
        /// </summary>
        /// <param name="name">Name of the SoundEffect to play.</param>
        public static void PlayEffect(String name)
        {
            SoundEffectInstance instance = SoundFromGroup(name, -1);
            instance.Play();
        }

        /// <summary>
        /// Get a sound from a specified group if the group exists.
        /// If index is passed a -1 then function will return a random sound from the group.
        /// </summary>
        /// <param name="name">Name of the group to get the sound from.</param>
        /// <param name="index">The sound to get from the named group. Use -1 to get a random sound.</param>
        /// <returns>An instance of a sound contained in the SoundManager.</returns>
        private static SoundEffectInstance SoundFromGroup(String name, int index = 0)
        {
            List<SoundEffect> sounds;
            SoundEffectInstance instance = null;

            // Create the instance.
            if (_soundData.ContainsKey(name))
            {
                sounds = _soundData[name];
                // Grab a random sound from the named group.
                if (index == -1)
                {
                    Random r = new Random();
                    index = r.Next(sounds.Count);
                }
                
                instance = new SoundEffectInstance(sounds[index]);
            }
            else
                SoundNotFound(name);

            return instance;
        }

        /// <summary>
        /// Method gets called if a sound does not exist within the context of SoundManager.
        /// </summary>
        /// <param name="name">Name of sound to include in the Exception thrown.</param>
        private static void SoundNotFound(String name)
        {
            throw new Exception(name + " has not been added to SoundManager.");
        }

        #endregion
    }
}
