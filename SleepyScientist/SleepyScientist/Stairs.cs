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
    class Stairs : GameObject
    {
        #region Attributes

        // The direction the stairs are facing
        private int _direction;

        #endregion

        #region Properties

        // Get or set the direction the stairs are facing
        public int Direction { get { return _direction; } set { _direction = value; } }

        #endregion

        #region Constructor

        public Stairs(int x, int y, int width, int height, int direction)
            : base(x, y, width, height)
        {
            this.Direction = direction;
        }

        #endregion
    }
}
