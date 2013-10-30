using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SleepyScientist
{
    class Floor : TileableGameObject
    {
        #region Constructor
        
        public Floor(int x, int y, int width, int height)
            : base(x, y, width, height) { }

        #endregion
    }
}
