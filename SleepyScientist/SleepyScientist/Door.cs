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
    class Door : GameObject
    {
        #region Attributes

        // Time remaining until the door closes
        private int _time;

        // Is the door closes or not
        private bool _closed;

        #endregion

        #region Properties
        
        // Get or set the time until the door shuts
        public int Time { get { return _time; } set { _time = value; } }

        // Get or set if the door is closed or not
        public bool Closed { get { return _closed; } set { _closed = value; } }

        #endregion

        #region Constructor

        /// <summary>
        /// Constructor for a door object
        /// </summary>
        /// <param name="x">The x coordinate of the door</param>
        /// <param name="y">The y coordinate of the door</param>
        /// <param name="width">The width of the door</param>
        /// <param name="height">The height of the door</param>
        /// <param name="time">The time remaining until the door closes</param>
        public Door(int x, int y, int width, int height, int time)
            : base(x, y, width, height, GameConstants.DEFAULT_DIRECTION)
        {
            this.Time = time;
            this.Closed = false;
        }

        #endregion
    }
}
