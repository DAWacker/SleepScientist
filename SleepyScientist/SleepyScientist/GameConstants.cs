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
    public static class GameConstants
    {
        #region Attributes

        // Screen dimensions
        public static int SCREEN_WIDTH = 1280;
        public static int SCREEN_HEIGHT = 720;

        // Tile dimensions
        public static int TILE_WIDTH = 50;
        public static int TILE_HEIGHT = TILE_WIDTH;

        // Default movement speeds for the scientist
        public static int DEFAULT_X_VELOCITY = 4;
        public static int DEFAULT_Y_VELOCITY = 0;
        public static int LADDER_Y_VELOCITY = -4;
        public static int DEFAULT_DIRECTION = 1;
        public static int DEFAULT_JUMP_VELOCITY_Y = -12;
        public static int DEFAULT_JUMP_VELOCITY_X = 10;
        
        // User input related
        public static bool MOVING_INVENTION = false;
        public static int SLOW_MOTION = 3;

        // Default movement speeds for inventions
        public static int DEFAULT_INVENTION_X_VELO = 8;
        public static int DEFAULT_INVENTION_Y_VELO = 0;
        public static int INVENTION_LADDER_Y_VELO = -8;

        // Floor constants
        public static int FLOOR_HEIGHT = TILE_HEIGHT;

        // Ladder constants
        public static int LADDER_WIDTH = TILE_WIDTH;
        public static int LADDER_HEIGHT = 200;

        // Message layer constants
        public static double MESSAGE_TIME = 2;

        // Invention constants
        public static int SKATEBOARD_SPEEDUP = 2;

        // Others
        public static int GRAVITY = 1;
        public static int BUFFER = 5;

        // Floor info
        public static int NUMBER_OF_FLOORS = 4;
        public static int DISTANCE_BETWEEN_FLOORS = SCREEN_HEIGHT / NUMBER_OF_FLOORS;

        // Camera constants.
        public static float MINIMUM_ZOOM = 1F;
        public static float ZOOM_STEP = 0.1F;
        public static float ZOOM_LEVEL_0 = 1F;
        public static float ZOOM_LEVEL_1 = 2F;

        // Game Textures
        public static Texture2D JACK_TEXTURE;
        public static Texture2D EGG_TEXTURE;
        public static Texture2D ROCKETBOARD_TEXTURE;
        public static Texture2D LADDER_TEXTURE;
        public static Texture2D FLOOR_TEXTURE;
        public static Texture2D STAIR_TEXTURE;
        public static Texture2D BED_TEXTURE;
        public static Texture2D PIT_TEXTURE;

        #endregion
    }
}