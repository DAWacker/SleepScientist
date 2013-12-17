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
    class Wall : GameObject
    {
        #region Constructor

        /// <summary>
        /// Constructor for a wall
        /// </summary>
        /// <param name="x">The x coordinate of the wall</param>
        /// <param name="y">The y coordinate of the wall</param>
        /// <param name="width">The width of the wall</param>
        /// <param name="height">The height of the wall</param>
        public Wall(int x, int y, int width, int height)
            : base(x, y, width, height, GameConstants.DEFAULT_DIRECTION) { }

        #endregion
    }
}
