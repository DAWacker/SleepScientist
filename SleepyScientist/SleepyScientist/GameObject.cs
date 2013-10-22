#region using statements
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
#endregion

namespace SleepyScientist
{
    class GameObject
    {
        #region Attributes

        // The rectangle position of the game object
        private Rectangle _rectPosition;

        // The image of the game object
        private Texture2D _image;

        // The animations of the game object.
        private AnimationSet _animations;

        #endregion

        #region Properties

        // Get or set the rectangle of the game object
        public Rectangle RectPosition { get { return _rectPosition; } set { _rectPosition = value; } }
        
        // Get or set the image of the game object
        public Texture2D Image {
            get {
                if (_animations != null)
                    return _animations.CurAnimation.CurrentImage();
                else
                    return _image;
            }
            set { _image = value; }
        }

        // Get or set the x-coordinate of the game object
        public int X { get { return _rectPosition.X; } set { _rectPosition.X = value; } }
        
        // Get or set the y-coordinate of the game object
        public int Y { get { return _rectPosition.Y; } set { _rectPosition.Y = value; } }

        // Get or set the width of the game object
        public int Width { get { return _rectPosition.Width; } set { _rectPosition.Width = value; } }
        
        // Get or set the height of the game object
        public int Height { get { return _rectPosition.Height; } set { _rectPosition.Height = value; } }

        // Get or set the AnimationSet of the game object.
        public AnimationSet Animations { get { return _animations; } set { _animations = value; } }

        #endregion

        #region Constructor

        /// <summary>
        /// Parameterized constructor of a game object
        /// </summary>
        /// <param name="x">Starting x-coordinate</param>
        /// <param name="y">Starting y-coordinate</param>
        /// <param name="width">Width</param>
        /// <param name="height">Height</param>
        public GameObject(int x, int y, int width, int height) 
        { 
            _rectPosition = new Rectangle(x, y, width, height);
            _animations = null;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Draw the game object
        /// </summary>
        /// <param name="batch">The sprite batch you want to draw on</param>
        public virtual void Draw(SpriteBatch batch) { batch.Draw(this.Image, this.RectPosition, Color.White); }

        /// <summary>
        /// Update the GameObject, namely the animation.
        /// </summary>
        public virtual void Update()
        {
            if ( _animations != null )
                _animations.CurAnimation.Update();
        }

        #endregion
    }
}