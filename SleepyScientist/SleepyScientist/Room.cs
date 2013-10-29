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

        public int _numFloors;
        public List<Ladder> _ladders;
        public List<Stairs> _stairs;
        public List<Floor> _floors;

        #endregion

        #region Properties

        public int NumberFloors { get { return _numFloors; } set { _numFloors = value; } }
        public List<Ladder> Ladders { get { return _ladders; } set { _ladders = value; } }
        public List<Stairs> Stairs { get { return _stairs; } set { _stairs = value; } }
        public List<Floor> Floors { get { return _floors; } set { _floors = value; } }

        #endregion

        #region Constructor

        public Room(int numFloors, List<Ladder> ladders, List<Stairs> stairs, List<Floor> floors)
        {
            _numFloors = numFloors;
            _ladders = ladders;
            _stairs = stairs;
            _floors = floors;
        }

        #endregion

        #region Methods



        #endregion
    }
}
