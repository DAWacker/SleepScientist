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
    class Button : GameObject
    {
        #region Properties

        /// <summary>
        /// Overrided to prevent draw scaling.
        /// </summary>
        public override Texture2D Image
        {
            get { return base.Image; }
            set
            {
                base.Image = value;
                this.Width = value.Width;
                this.Height = value.Height;
            }
        }

        #endregion

        public Button(int x, int y, int width, int height, Texture2D image)
            : base(x, y, width, height, image)
        {
        }

        public override void Draw(SpriteBatch batch, Rectangle? pos = null)
        {
            base.Draw(batch, pos);
            //batch.DrawString(MessageLayer.Font, "New Game", new Vector2(this.RectPosition.X, this.RectPosition.Y), Color.Black);
        }
    }
}
