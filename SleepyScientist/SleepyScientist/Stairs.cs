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

        // The railing for the stairs
        private GameObject _railing;

        #endregion

        #region Properties

        // Get or set the railing for the stairs
        public GameObject Railing { get { return _railing; } set { _railing = value; } }

        // Set the railing texture for the stairs
        public Texture2D RailingTexture { set { _railing.Image = value; } }

        #endregion

        #region Constructor

        /// <summary>
        /// Constructor for stairs
        /// </summary>
        /// <param name="x">The x coordinate of the stairs</param>
        /// <param name="y">The y coordinate of the stairs</param>
        /// <param name="width">The width of the stairs</param>
        /// <param name="height">The height of the stairs</param>
        /// <param name="direction">The direction of the stairs</param>
        public Stairs(int x, int y, int width, int height, int direction)
            : base(x, y, width, height, direction)
        {
            this.Direction = direction;
            this.Railing = new GameObject(x, y, width, height, direction);
        }

        #endregion
    }
}
