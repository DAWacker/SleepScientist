﻿#region using statements
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
    class AI : GameObject
    {
        #region Attributes

        // Movement
        private int _veloX;
        private int _veloY;

        // General
        private string _name;
        private int _direction;

        #endregion

        #region Properties

        // Get or set the AI's movement
        public int VeloX { get { return _veloX; } set { _veloX = value; } }
        public int VeloY { get { return _veloY; } set { _veloY = value; } }

        // Get or set the AI's name
        public string Name { get { return _name; } set { _name = value; } }

        // Get or set the AI's direction
        public int Direction { get { return _direction; } set { _direction = value; } }

        #endregion

        #region Constructor

        /// <summary>
        /// Parameterized constructor of an AI
        /// </summary>
        /// <param name="name">The name</param>
        /// <param name="x">Starting x-coordinate</param>
        /// <param name="y">Starting y-coordinate</param>
        /// <param name="image">The image</param>
        public AI(string name, int x, int y, int width, int height, Texture2D image)
            : base(x, y, width, height, image)
        {
            // Movement
            _veloX = GameConstants.DEFAULT_X_VELOCITY;
            _veloY = GameConstants.DEFAULT_Y_VELOCITY;

            // General
            _name = name;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Reverses the direction of the AI
        /// </summary>
        public void Reverse() { this.VeloX = -this.VeloX; }

        /// <summary>
        /// Move the AI
        /// </summary>
        public void Move()
        {
            this.X += this.VeloX;
            this.Y += this.VeloY;
        }

        /// <summary>
        /// Prevents the AI from going off the left and right sides of the screen
        /// </summary>
        public void StayOnScreen() { if ((this.X + this.Width) > GameConstants.SCREEN_WIDTH || (this.X < 0)) { this.Reverse(); } }

        #endregion
    }
}