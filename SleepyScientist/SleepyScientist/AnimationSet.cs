using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SleepyScientist
{
    /// <summary>
    /// AnimationSets contain a dictionary of Animations to provide easy access to any
    /// of them.
    /// </summary>
    class AnimationSet
    {
        #region Attributes
        private String _name;                               // Name of this set.
        private Dictionary<String, Animation> _animations;  // Animations contained in this set.
        private Animation _curAnimation;                    // Current Animation.
        #endregion

        #region Properties
        public String Name
        {
            get { return _name; }
        }

        public Dictionary<String, Animation> Animations
        {
            get { return _animations; }
            set { _animations = value; }
        }

        public Animation CurAnimation
        {
            get { return _curAnimation; }
        }
        #endregion

        #region Constructor
        /// <summary>
        /// Construct set and assign the name.
        /// </summary>
        /// <param name="name">Name of this set.</param>
        public AnimationSet(String name)
        {
            _name = name;
        }

        /// <summary>
        /// Copy construct an AnimationSet.
        /// Copy constructs each Animation so that reusable images are not
        /// wastefully copied.
        /// </summary>
        /// <param name="other">The AnimationSet to copy.</param>
        public AnimationSet(AnimationSet other)
        {
            _name = other._name;
            // Copy all Animations from other AnimationSet.
            foreach (String name in other._animations.Keys)
            {
                _animations[name] = new Animation(other._animations[name]);
            }
        }
        #endregion

        #region Methods
        /// <summary>
        /// Changes the current animation to the named one.
        /// Throws KeyNotFoundException if the animation does not exist
        /// within this AnimationSet.
        /// </summary>
        /// <param name="name">Name of Animation to change to.</param>
        public void ChangeAnimation(String name)
        {
            if (_animations.ContainsKey(name))
            {
                _curAnimation = _animations[name];
            }
            else
            {
                throw new KeyNotFoundException("\""+name+"\" animation not found in the \""+_name+"\" animation set.");
            }
        }
        #endregion
    }
}
