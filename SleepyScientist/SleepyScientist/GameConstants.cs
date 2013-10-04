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
        public static int SCREEN_WIDTH = 1280;
        public static int SCREEN_HEIGHT = 720;

        // Tile dimensions
        public static int TILE_WIDTH = 50;
        public static int TILE_HEIGHT = TILE_WIDTH;

        // Default movement speeds
        public static int DEFAULT_X_VELOCITY = 5;
        public static int DEFAULT_Y_VELOCITY = 0;
        public static int LADDER_Y_VELOCITY = -5;

        // Floor constants
        public static int FLOOR_HEIGHT = TILE_HEIGHT;

        // Ladder constants
        public static int LADDER_WIDTH = TILE_WIDTH;

        #endregion
    }
}