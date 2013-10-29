#region Using Statements
using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Storage;
using Microsoft.Xna.Framework.GamerServices;
#endregion

namespace SleepyScientist
{
    class Room
    {
        #region Attributes

        private int _numFloors;
        private int _startFloor;
        private List<Floor> _floors;

        #endregion

        #region Properties
        
        // Get or set the number of floors
        public int NumberFloors { get { return _numFloors; } set { _numFloors = value; } }

        // Get or set the starting floor of the room
        public int StartFloor { get { return _startFloor; } set { _startFloor = value; } }

        // Get or set the floors that are in the room
        public List<Floor> Floors { get { return _floors; } set { _floors = value; } }

        #endregion

        #region Constructor

        /// <summary>
        /// Constructor for a Room
        /// </summary>
        /// <param name="numFloors">The number of floors in the room</param>
        /// <param name="ladders">The ladders in the room</param>
        /// <param name="stairs">The stairs in the room</param>
        /// <param name="floors">The floors in the room</param>
        public Room(int numFloors, int startFloor)
        {
            _numFloors = numFloors;
            _startFloor = startFloor;
            _floors = new List<Floor>();
        }

        #endregion

        #region Methods

        public void Draw(SpriteBatch batch) { foreach (Floor floor in this.Floors) { floor.Draw(batch); } }

        #endregion
    }
}
