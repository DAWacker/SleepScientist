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
        private int _startDirection;
        private List<Floor> _floors;
        private Scientist _scientist;
        private Bed _bed;
        private Door _door;

        #endregion

        #region Properties
        
        // Get or set the number of floors
        public int NumberFloors { get { return _numFloors; } set { _numFloors = value; } }

        // Get or set the starting floor of the room
        public int StartFloor { get { return _startFloor; } set { _startFloor = value; } }

        // Get or set the starting coordinates of the scientist in the room
        public int StartX { get { return _startX; } set { _startX = value; } }
        public int StartY { get { return _startY; } set { _startY = value; } }

        // Get or set the starting direction of the scientist for the room
        public int StartDirection { get { return _startDirection; } set { _startDirection = value; } }

        // Get or set the floors that are in the room
        public List<Floor> Floors { get { return _floors; } set { _floors = value; } }

        // Get or set the bed in the room
        public Bed Bed { get { return _bed; } set { _bed = value; } }

        // Get or set the door in the room
        public Door Door { get { return _door; } set { _door = value; }}

        // Get of set the scientist in the room
        public Scientist Scientist { get { return _scientist; } set { _scientist = value; } }

        #endregion

        #region Constructor

        /// <summary>
        /// Constructor for a Room
        /// </summary>
        /// <param name="numFloors">The number of floors in the room</param>
        /// <param name="ladders">The ladders in the room</param>
        /// <param name="stairs">The stairs in the room</param>
        /// <param name="floors">The floors in the room</param>
        public Room(int numFloors, int startFloor, int startX, int startY, int startDirection, Bed bed)
        {
            this.NumberFloors = numFloors;
            this.StartFloor = startFloor;
            this.StartX = startX;
            this.StartY = startY;
            this.StartDirection = startDirection;
            this.Floors = new List<Floor>();
            this.Bed = bed;
            this.Door = null;
            this.Scientist = null;
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
            List<Invention> inventions = new List<Invention>();
            List<GameObject> railings = new List<GameObject>();
            List<Teleporter> teleporters = new List<Teleporter>();
            List<Stairs> stairs = new List<Stairs>();
            List<Wall> walls = new List<Wall>();
            List<Floor> floors = new List<Floor>();
            List<Pit> pits = new List<Pit>();

            foreach (Floor floor in this.Floors)
            {
                teleporters.AddRange(floor.Teleporters);
                foreach (Stairs stair in floor.Stairs) { railings.Add(stair.Railing); }
                inventions.AddRange(floor.Inventions);
                stairs.AddRange(floor.Stairs);
                walls.AddRange(floor.Walls);
                pits.AddRange(floor.Pits);
                floors.Add(floor);
            }
            roomObjects.AddRange(walls);
            roomObjects.AddRange(floors);
            roomObjects.AddRange(pits);
            roomObjects.AddRange(stairs);
            roomObjects.AddRange(teleporters);
            roomObjects.AddRange(inventions);
            roomObjects.Add(this.Scientist);
            roomObjects.AddRange(railings);
            roomObjects.Add(Bed);
            if (Door != null)
                roomObjects.Add(Door);

            return roomObjects;
        }

        #endregion
    }
}
