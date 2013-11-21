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
        public List<Teleporter> _teleporters;

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
        public List<Teleporter> Teleporters { get { return _teleporters; } set { _teleporters = value; } }

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
            this.Teleporters = new List<Teleporter>();
            this.Stairs = new List<Stairs>();
            this.Walls = new List<Wall>();
            this.Pits = new List<Pit>();
        }

        #endregion
    }
}
