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
    class Ladder : GameObject
    {
        #region Constructor

        public Ladder(int x, int y, int width, int height)
            : base(x, y, width, height) { }

        #endregion

        #region Methods
        public void Draw(SpriteBatch batch)
        {
            Rectangle drawClip = new Rectangle(0, 0, Image.Width, Image.Height);
            Rectangle drawDest = new Rectangle(X, Y, Image.Width, Image.Height);

            for (int yOff = 0; yOff < Height; yOff += Image.Height)
            {
                drawDest.Y = Y + yOff;
                if (yOff + Image.Height > Height)
                {
                    drawClip.Height = Height - yOff;
                }
                batch.Draw(Image, drawDest, drawClip, Color.White);
            }
        }
        #endregion
    }
}
