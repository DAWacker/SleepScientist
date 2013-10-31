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
    class Floor : GameObject
    {

        #region Attributes

        // The inventions on the floor
        public List<Invention> _inventions;

        // The ladders on the floor
        public List<Ladder> _ladders;

        // The stairs on the floor
        public List<Stairs> _stairs;

        #endregion

        #region Properties

        // Gets or sets the inventions on the floor
        public List<Invention> Inventions { get { return _inventions; } set { _inventions = value; } }

        // Gets or sets the ladders on the floor
        public List<Ladder> Ladders { get { return _ladders; } set { _ladders = value; } }

        // Gets or sets the stairs on the floor
        public List<Stairs> Stairs { get { return _stairs; } set { _stairs = value; } }

        #endregion

        #region Constructor

        /// <summary>
        /// Constructor for a Floor
        /// </summary>
        /// <param name="x">The x coordinate of the floor</param>
        /// <param name="y">The y coordinate of the floor</param>
        /// <param name="width">The width of the floor</param>
        /// <param name="height">The height of the floor</param>
        public Floor(int x, int y, int width, int height)
            : base(x, y, width, height) 
        {
            _inventions = new List<Invention>();
            _ladders = new List<Ladder>();
            _stairs = new List<Stairs>();
        }

        #endregion

        #region Methods
        /// <summary>
        /// Override of the draw method for tileable objects.
        /// </summary>
        public override void Draw(SpriteBatch batch, Rectangle? pos = null)
        {

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

            foreach (Ladder ladder in this.Ladders) { ladder.Draw(batch); }
            foreach (Stairs stair in this.Stairs) { stair.Draw(batch); }
            foreach (Invention invention in this.Inventions) { invention.Draw(batch); }
        }
        #endregion
    }
}
