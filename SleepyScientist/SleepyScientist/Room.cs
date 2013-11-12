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
        private int _startX;
        private int _startY;
        private List<Floor> _floors;

        #endregion

        #region Properties
        
        // Get or set the number of floors
        public int NumberFloors { get { return _numFloors; } set { _numFloors = value; } }

        // Get or set the starting floor of the room
        public int StartFloor { get { return _startFloor; } set { _startFloor = value; } }

        // Get or set the starting coordinates of the scientist in the room
        public int StartX { get { return _startX; } set { _startX = value; } }
        public int StartY { get { return _startY; } set { _startY = value; } }

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
        public Room(int numFloors, int startFloor, int startX, int startY)
        {
            this.NumberFloors = numFloors;
            this.StartFloor = startFloor;
            this.StartX = startX;
            this.StartY = startY;
            this.Floors = new List<Floor>();
        }

        #endregion

        #region Methods

        public void Draw(SpriteBatch batch) { foreach (Floor floor in this.Floors) { floor.Draw(batch); } }

        /// <summary>
        /// Grab and return all the GameObjects contained within this Room.
        /// </summary>
        /// <returns>Every GameObject on every Floor.</returns>
        public List<GameObject> GetGameObjects()
        {
            List<GameObject> roomObjects = new List<GameObject>();
            foreach (Floor floor in this.Floors)
            {
                roomObjects.AddRange(floor.GetGameObjects());
            }

            return roomObjects;
        }

        #endregion
    }
}
