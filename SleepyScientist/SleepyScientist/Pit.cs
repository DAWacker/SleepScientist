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
    class Pit : GameObject
    {
        #region Attributes

        // Left end point of the pit
        private Texture2D _leftEnd;

        // Tile piece of the pit
        private Texture2D _tile;

        // Right end point of the pit
        private Texture2D _rightEnd;

        // Battery terminal of the pit
        private Texture2D _terminal;

        #endregion

        #region Properties

        // Get or set the left end of the pit
        public Texture2D LeftEnd { get { return _leftEnd; } set { _leftEnd = value; } }

        // Get or set the center piece of the pit (tileable)
        public Texture2D Tile { get { return _tile; } set { _tile = value; } }

        // Get or set the right end of the pit
        public Texture2D RightEnd { get { return _rightEnd; } set { _rightEnd = value; } }

        // Get or set the left battery terminal for the pit
        public Texture2D Terminal { get { return _terminal; } set { _terminal = value; } }

        #endregion

        #region Constructor

        /// <summary>
        /// Constructor for a pit
        /// </summary>
        /// <param name="x">The x coordinate of the pit</param>
        /// <param name="y">The y coordinate of the pit</param>
        /// <param name="width">The width of the pit</param>
        /// <param name="height">the height of the pit</param>
        public Pit(int x, int y, int width, int height)
            : base(x, y, width, height, GameConstants.DEFAULT_DIRECTION) { }

        #endregion

        #region Methods

        /// <summary>
        /// Overrides the GameObject draw method
        /// </summary>
        /// <param name="batch">The sprite batch to draw on</param>
        /// <param name="pos">The rectangle position of the pit</param>
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

            RectangleVector terminal;
            float length = this.Width / 50 / zoomFactor;
            RectangleVector currentPosition = new RectangleVector(this.X - GameConstants.TILE_WIDTH * zoomFactor, this.Y - GameConstants.TILE_HEIGHT * zoomFactor, this.Tile.Width * zoomFactor, this.Tile.Height * zoomFactor);

            // Draw the left battery terminal
            terminal = new RectangleVector(this.X - this.Terminal.Width * zoomFactor, this.Y - this.Terminal.Height * zoomFactor, this.Terminal.Width * zoomFactor, this.Terminal.Height * zoomFactor);

            batch.Draw(this.Terminal, terminal, Color.White);
            currentPosition.X += GameConstants.TILE_WIDTH * zoomFactor;
            currentPosition.Y += GameConstants.TILE_HEIGHT * zoomFactor;

            // Check if the pit doesn't not need to be tileable
            if (length == 2)
            {
                batch.Draw(this.LeftEnd, currentPosition, Color.White);
                currentPosition.X += GameConstants.TILE_WIDTH * zoomFactor;
                batch.Draw(this.RightEnd, currentPosition, Color.White);
            }

            // The pit needs to be tileable in the center
            else
            {
                // Draw the left end of the pit lasers
                batch.Draw(this.LeftEnd, currentPosition, Color.White);
                currentPosition.X += GameConstants.TILE_WIDTH * zoomFactor;
                length -= 2;

                // Draw a center piece until you have reached the end of the pit
                while (length > 0)
                {
                    // Draw the tileable center piece
                    batch.Draw(this.Tile, currentPosition, Color.White);
                    currentPosition.X += GameConstants.TILE_WIDTH * zoomFactor;
                    length--;
                }

                // Draw the right end of the pit lasers
                batch.Draw(this.RightEnd, currentPosition, Color.White);
            }

            // Draw the right battery terminal
            currentPosition.X += GameConstants.TILE_WIDTH * zoomFactor;
            currentPosition.Y -= GameConstants.TILE_HEIGHT * zoomFactor;
            terminal.X = currentPosition.X;
            terminal.Y = currentPosition.Y;
            batch.Draw(this.Terminal, terminal, null, Color.White, 0, Vector2.Zero, SpriteEffects.FlipHorizontally, 0);

            this.RectPosition = prevRectPosition;
        }

        #endregion
    }
}
