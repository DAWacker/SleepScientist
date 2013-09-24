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
    class AI
    {
        #region Attributes

        // Location
        private int _x;
        private int _y;

        // Movement
        private int _speed;
        private int _xVelo;
        private int _yVelo;

        // General
        private string _name;
        private Texture2D _image;

        #endregion

        #region Properties

        // Get or set the AI's current position
        public int X { get { return _x; } set { _x = value; } }
        public int Y { get { return _y; } set { _y = value; } }

        // Get or set the AI's speed and velocities (movement)
        public int Speed { get { return _speed; } set { _speed = value; } }
        public int XVelo { get { return _xVelo; } set { _xVelo = value; } }
        public int YVelo { get { return _yVelo; } set { _yVelo = value; } }

        // Get or set the AI's name
        public string Name { get { return _name; } set { _name = value; } }

        // Get or set the AI's image
        public Texture2D Image { get { return _image; } set { _image = value; } }

        #endregion

        #region Constructor

        /// <summary>
        /// Paramaterized constructor of an AI
        /// </summary>
        /// <param name="name">The name</param>
        /// <param name="x">The starting x coordinate</param>
        /// <param name="y">The starting y coordinate</param>
        /// <param name="image">The image</param>
        public AI(string name, int x, int y, Texture2D image)
        {
            // Location
            _x = x;
            _y = y;

            // Movement
            _speed = 10;
            _xVelo = 0;
            _yVelo = 0;

            // General
            _name = name;
            _image = image;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Reverses the direction of the AI
        /// </summary>
        public void Reverse() { XVelo = -XVelo; }

        #endregion
    }
}
