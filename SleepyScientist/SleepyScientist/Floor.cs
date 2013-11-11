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

        // The walls on the floor
        public List<Wall> _walls;

        // The pits on the floor
        public List<Pit> _pits;

        #endregion

        #region Properties

        // Gets or sets the inventions on the floor
        public List<Invention> Inventions { get { return _inventions; } set { _inventions = value; } }

        // Gets or sets the ladders on the floor
        public List<Ladder> Ladders { get { return _ladders; } set { _ladders = value; } }

        // Gets or sets the stairs on the floor
        public List<Stairs> Stairs { get { return _stairs; } set { _stairs = value; } }

        // Gets or sets the walls on the floor
        public List<Wall> Walls { get { return _walls; } set { _walls = value; } }

        // Gets or sets the pits on the floor
        public List<Pit> Pits { get { return _pits; } set { _pits = value; } }

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
            : base(x, y, width, height, 1) 
        {
            this.Inventions = new List<Invention>();
            this.Ladders = new List<Ladder>();
            this.Stairs = new List<Stairs>();
            this.Walls = new List<Wall>();
            this.Pits = new List<Pit>();
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
            Rectangle drawDest = new Rectangle( (int)X, (int)Y, Image.Width, Image.Height);

            for (int xOff = 0; xOff < Width; xOff += Image.Width)
            {
                drawDest.X = (int)X + xOff;
                if (xOff + Image.Width > Width)
                {
                    // Prevent overdraw.
                    drawClip.Width = (int)Width - xOff;
                    drawDest.Width = drawClip.Width;
                }

                for (int yOff = 0; yOff < Height; yOff += Image.Height)
                {
                    drawDest.Y = (int)Y + yOff;
                    if (yOff + Image.Height > Height)
                    {
                        // Prevent overdraw.
                        drawClip.Height = (int)Height - yOff;
                        drawDest.Height = drawClip.Height;
                    }
                    if (pos != null)
                        batch.Draw(this.Image, pos.Value, Color.White);
                    else
                        batch.Draw(Image, drawDest, drawClip, Color.White);
                }
                drawDest.Height = Image.Height;
            }

            // Draw the pits on the floor
            foreach (Pit pit in this.Pits) { pit.Draw(batch); }
        }
        #endregion
    }
}
