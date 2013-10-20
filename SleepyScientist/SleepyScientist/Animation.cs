using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Storage;
using Microsoft.Xna.Framework.GamerServices;

namespace SleepyScientist
{
    /// <summary>
    /// Contains multiple images to be used in sequence as an animation.
    /// Animations can be paused and designated to only play a certain number
    /// of times.
    /// </summary>
    class Animation
    {
        #region Attributes
        private const int UNPAUSED = -1;// Unpaused status flag.
        private const int CONTINUOUS = -1;// Continuous play flag.
        private List<Texture2D> _images;// The frames of the animation.
        private String _name;           // Name of the animation.
        private int _curFrame;          // Index of the current frame of the animation.
        private float _timePerFrame;    // Time to wait before advancing the frame.
        private float _curFrameTime;    // Current time on current frame.
        private float _pauseTime;       // Time to pause for:
                                        // 0 - paused indefinitely.
                                        // < 0 - default state, not paused.
                                        // > 0 - time to remain paused.
        private int _playAmount;        // Times to loop the animation before stopping.
        #endregion

        #region Properties
        public String Name { get { return _name; } set { _name = value; } }
        public List<Texture2D> Images { get { return _images; } set { _images = value; } }
        #endregion

        #region Constructor
        /// <summary>
        /// Construct an animation and assign its name.
        /// Set default values for member data.
        /// </summary>
        /// <param name="name">Name of the animation.</param>
        public Animation(String name)
        {
            _name = name;
            _curFrameTime = 0;
            _pauseTime = UNPAUSED;
            _playAmount = CONTINUOUS;
        }
        #endregion

        #region Methods
        /// <summary>
        /// Advances current frame if neccessary.
        /// </summary>
        public void Update()
        {
            // If not paused.
            if ( _pauseTime == UNPAUSED &&
            ( _playAmount == CONTINUOUS || _playAmount > 0 ) )
            {
                // Increase time on current frame and advance if neccessary.
                _curFrameTime += Time.DeltaTime;
                if (_curFrameTime > _timePerFrame)
                {
                    // If not set to play continuously, then update and
                    // pause if neccessary.
                    if (_playAmount > 0)
                    {
                        if (--_playAmount == 0)
                        {
                            Pause();
                            return;
                        }
                    }

                    _curFrame = ( _curFrame + 1 ) % _images.Count;
                }
            }
            // Else if paused for only a certain amount of time.
            else if (_pauseTime > 0)
            {
                // Decrease pause time and resume playing if neccessary.
                _pauseTime -= Time.DeltaTime;
                if (_pauseTime <= 0)
                {
                    Resume();
                }
            }
        }

        /// <summary>
        /// Gets the current frame of the animation.
        /// </summary>
        /// <returns>The current frame of the Animation.</returns>
        public Texture2D CurrentImage()
        {
            return _images[_curFrame];
        }

        /// <summary>
        /// Pause the animation on the current frame.
        /// </summary>
        /// <param name="time">If provided, pause for given time
        /// before resuming.</param>
        public void Pause(float time = 0)
        {
            _pauseTime = time;
        }

        /// <summary>
        /// Resumes the animation from where it was paused.
        /// </summary>
        public void Resume()
        {
            _pauseTime = UNPAUSED;
        }

        /// <summary>
        /// Reset animation and play. Called by the constructor.
        /// </summary>
        /// <param name="times">If provided, loop the that many times
        /// before pausing.</param>
        public void Play(int times = 0)
        {
            _playAmount = times;
        }
        #endregion

    }
}
