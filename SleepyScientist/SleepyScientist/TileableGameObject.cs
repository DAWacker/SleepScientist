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
    class TileableGameObject : GameObject
    {
        #region Constructor
        
        public TileableGameObject(int x, int y, int width, int height)
            : base(x, y, width, height) { }

        #endregion

        #region Methods
        /// <summary>
        /// Override of the draw method for tileable objects.
        /// </summary>
        public override void Draw(SpriteBatch batch, Rectangle? pos = null)
        {
            float zoomFactor;           // Calculated zoomFactor based value of pos.
            Rectangle prevRectPosition; // Contains the previous position of the game object.
            Rectangle drawClip;         // Only draw this much of the _image. Prevents overdraw.
            Rectangle drawDest;         // Where to draw the current tile. Include offset.

            prevRectPosition = this.RectPosition;
            if (pos != null)
            {
                zoomFactor = (float)pos.Value.Width / this.RectPosition.Width;
                this.RectPosition = pos.Value;    // Temporarily overwrite the position of the GameObject.
            }
            else
                zoomFactor = 1.0F;

            drawClip = new Rectangle(0, 0, Image.Width, Image.Height);
            drawDest = new Rectangle(X, Y, (int)(zoomFactor * Image.Width), (int)(zoomFactor * Image.Height));

            // Draw width of object.
            for (int xOff = 0; xOff < Width; xOff += drawDest.Width)
            {
                drawDest.X = X + xOff;
                if (xOff + drawDest.Width > Width)
                {
                    // Prevent overdraw.
                    drawClip.Width = Width - xOff;
                    drawDest.Width = drawClip.Width;
                }

                // Draw height of object.
                for (int yOff = 0; yOff < Height; yOff += drawDest.Height)
                {
                    drawDest.Y = Y + yOff;
                    if (yOff + drawDest.Height > Height)
                    {
                        // Prevent overdraw.
                        drawClip.Height = Height - yOff;
                        drawDest.Height = drawClip.Height;
                    }
                    batch.Draw(Image, drawDest, drawClip, Color.White);
                }
                drawDest.Height = (int)(zoomFactor * Image.Height);
            }

            this.RectPosition = prevRectPosition;
        }
        #endregion
    }
}
