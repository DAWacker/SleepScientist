using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Storage;
using Microsoft.Xna.Framework.GamerServices;

namespace SleepyScientist
{
    /// <summary>
    /// Provides a way to set up game animations by loading information about them
    /// from a file. This provides a more flexible way to deal with animations than
    /// a sprite sheet would.
    /// </summary>
    static class AnimationLoader
    {
        #region Attributes
        private static Dictionary<String, AnimationSet> _sets = 
            new Dictionary<String, AnimationSet>();     // Every loaded AnimationSet thus far.
        private static XmlTextReader _reader;           // What reads the XML files.
        #endregion

        #region Properties
        public static Dictionary<String, AnimationSet> Sets {
            get { return _sets; }
        }
        #endregion

        #region Methods
        /// <summary>
        /// Creates an AnimationSet from an AnimationSet xml file and adds the resulting
        /// set to the list of created sets.
        /// </summary>
        /// <param name="filename">Name of the file to load.</param>
        /// <param name="content">Game's ContentManager.</param>
        /// <returns>The newly created AnimationSet.</returns>
        public static AnimationSet Load(String filename, ContentManager content)
        {
            AnimationSet newSet = null;       // Return value.
            Dictionary<String, Animation> newAnimations =
                new Dictionary<String, Animation>();    // All the animations
                                                        // for the return value.
            Animation newAnimation = null;      // Current Animation to add.
            List<Texture2D> newFrames = null;   // The frames for the animation currently
                                                // being created.
            String newAnimationName = null;     // Name of the Animation currently being
                                                // created.

            try
            {
                // Load the reader with the data file and ignore all white space nodes.         
                _reader = new XmlTextReader("Content/"+filename);
                _reader.WhitespaceHandling = WhitespaceHandling.None;

                // Parse the file.
                while (_reader.Read())
                {
                    switch (_reader.NodeType)
                    {
                        case XmlNodeType.Element:
                            switch (_reader.Name)
                            {
                                case "AnimationSet":
                                    _reader.Read();     // Should be Name element.
                                    _reader.Read();     // Should be Name value.
                                    newSet = new AnimationSet(_reader.Value);
                                    break;
                                case "Animation":       // Beginning of new Animation.
                                    // Add the previous Animation if one was created.
                                    if (newAnimationName != null)
                                    {
                                        newAnimation.Images = newFrames;
                                        newAnimations[newAnimationName] = newAnimation;
                                    }
                                    _reader.Read();     // Should be Name element.
                                    _reader.Read();     // Should be Name value.
                                    // Create a new animation.
                                    newAnimationName = _reader.Value;
                                    newAnimation = new Animation(newAnimationName);
                                    newFrames = new List<Texture2D>();
                                    break;
                                case "Fps":
                                    _reader.Read();     // Should be Fps value.
                                    newAnimation.FramesPerTime = float.Parse(_reader.Value);
                                    break;
                                case "Frame":
                                    _reader.Read();     // Should be Frame value.
                                                        // (filename)
                                    newFrames.Add(content.Load<Texture2D>(_reader.Value));
                                    break;
                            }
                            break;
                    }
                }
                // Add the last animation.
                if (newSet != null)
                {
                    newAnimation.Images = newFrames;
                    newAnimations[newAnimationName] = newAnimation;
                }
            }
            finally
            {
                if (_reader != null)
                    _reader.Close();
            }
            
            // Give the animations to the new set to return;
            if (newSet != null)
            {
                newSet.Animations = newAnimations;
                _sets[newSet.Name] = newSet;
            }

            return newSet;
        }
        #endregion
    }
}
