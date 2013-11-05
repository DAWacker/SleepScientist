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
            : base(x, y, width, height) { }

        #endregion

        #region Methods

        public override void Draw(SpriteBatch batch, Rectangle? pos = null)
        {
            batch.Draw(this.Image, this.RectPosition, Color.Red);
            // Only draw this much of the _image. Prevents overdraw.
            Rectangle drawClip = new Rectangle(0, 0, Image.Width, Image.Height);
            // Where to draw the current tile. Include offset.
            Rectangle drawDest = new Rectangle(X, Y, Image.Width, Image.Height);

            for (int xOff = 0; xOff < Width; xOff += Image.Width)
            {
                drawDest.X = X + xOff;
                if (xOff + Image.Width > Width)
                {
                    // Prevent overdraw.
                    drawClip.Width = Width - xOff;
                    drawDest.Width = drawClip.Width;
                }

                for (int yOff = 0; yOff < Height; yOff += Image.Height)
                {
                    drawDest.Y = Y + yOff;
                    if (yOff + Image.Height > Height)
                    {
                        // Prevent overdraw.
                        drawClip.Height = Height - yOff;
                        drawDest.Height = drawClip.Height;
                    }
                    if (pos != null)
                        batch.Draw(this.Image, pos.Value, Color.White);
                    else
                        batch.Draw(Image, drawDest, drawClip, Color.White);
                }
                drawDest.Height = Image.Height;
            }
        }

        #endregion
    }
}
