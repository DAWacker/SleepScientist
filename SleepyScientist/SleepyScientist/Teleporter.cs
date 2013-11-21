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
            float zoomFactor;           // Calculated zoomFactor based value of pos.
            Rectangle prevRectPosition; // Contains the previous position of the game object.

            prevRectPosition = this.RectPosition;
            if (pos != null)
            {
                zoomFactor = (float)pos.Value.Width / this.RectPosition.Width;
                this.RectPosition = pos.Value;    // Temporarily overwrite the position of the pit.
            }
            else
                zoomFactor = 1.0F;

            RectangleVector currentPosition = new RectangleVector(this.X, this.Y, GameConstants.TILE_WIDTH * zoomFactor, GameConstants.TILE_HEIGHT * zoomFactor);
            batch.Draw(this.Top, currentPosition, Color.White);
            for (int i = 0; i < 3; i++)
            {
                currentPosition.Y += GameConstants.TILE_HEIGHT * zoomFactor;
                batch.Draw(this.Tile, currentPosition, Color.White);
            }
            currentPosition.Y += GameConstants.TILE_HEIGHT * zoomFactor;
            batch.Draw(this.Bottom, currentPosition, Color.White);

            this.RectPosition = prevRectPosition;
        }

        #endregion
    }
}
