using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SleepyScientist
{
    public static class GameConstants
    {
        #region Attributes

        // Screen dimensions
        public static int SCREEN_WIDTH;
        public static int SCREEN_HEIGHT;

        // Default movement speeds
        public static int DEFAULT_X_VELOCITY = 5;
        public static int DEFAULT_Y_VELOCITY = 0;
        public static int LADDER_Y_VELOCITY = -5;

        // Floor constants
        public static int FLOOR_HEIGHT = 64;

        // Ladder constants
        public static int LADDER_WIDTH = 54;

        #endregion
    }
}