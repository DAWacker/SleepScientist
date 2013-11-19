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
    class Teleporter : GameObject
    {
        #region Attributes

        // The tileable piece of the teleporter
        private Texture2D _tile;

        // The top piece of the teleporter
        private Texture2D _top;

        // The bottom piece of the teleporter
        private Texture2D _bottom;

        #endregion

        #region Properties

        // Gets or sets the tileable piece of the teleporter
        public Texture2D Tile { get { return _tile; } set { _tile = value; } }

        // Gets or sets the top piece of the teleporter
        public Texture2D Top { get { return _top; } set { _top = value; } }

        // Gets or sets the bottom piece of the teleporter
        public Texture2D Bottom { get { return _bottom; } set { _bottom = value; } }

        #endregion

        #region Constructor

        public Teleporter(int x, int y, int width, int height)
            : base(x, y, width, height, GameConstants.DEFAULT_DIRECTION) { }

        #endregion

        #region Methods

        public override void Draw(SpriteBatch batch, Rectangle? pos = null)
        {
            RectangleVector currentPosition = new RectangleVector(this.X, this.Y, GameConstants.TILE_WIDTH, GameConstants.TILE_HEIGHT);
            batch.Draw(this.Top, currentPosition, Color.White);
            for (int i = 0; i < 3; i++)
            {
                currentPosition.Y += GameConstants.TILE_HEIGHT;
                batch.Draw(this.Tile, currentPosition, Color.White);
            }
            currentPosition.Y += GameConstants.TILE_HEIGHT;
            batch.Draw(this.Bottom, currentPosition, Color.White);
        }

        #endregion
    }
}
