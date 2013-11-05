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
    class Floor : TileableGameObject
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
            this.Inventions = new List<Invention>();
            this.Ladders = new List<Ladder>();
            this.Stairs = new List<Stairs>();
            this.Walls = new List<Wall>();
        }

        #endregion

        #region Methods
        /// <summary>
        /// Override of the draw method for tileable objects.
        /// </summary>
        public override void Draw(SpriteBatch batch, Rectangle? pos = null)
        {
            base.Draw(batch, pos);
            /*
            // Draw the ladders on the floor
            foreach (Ladder ladder in this.Ladders) { ladder.Draw(batch, pos); }
            // Draw the walls on the floor
            foreach (Wall wall in this.Walls) { wall.Draw(batch, pos); }
            // Draw the stairs on the floor
            foreach (Stairs stair in this.Stairs) { stair.Draw(batch, pos); }
            // Draw the inventions on the floor
            foreach (Invention invention in this.Inventions) { invention.Draw(batch, pos); }
             */
        }
        #endregion
    }
}
