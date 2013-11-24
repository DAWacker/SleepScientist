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

        // Debugging
        public static bool DEBUG = false;

        // Euler integration constants
        public static float EULER_SCALE = 50f;

        // Time
        public static float TIME_FAST_FORWARD = 3F;

        // Screen dimensions
        public static int SCREEN_WIDTH = 1280;
        public static int SCREEN_HEIGHT = 720;

        // Tile dimensions
        public static int TILE_WIDTH = 50;
        public static int TILE_HEIGHT = TILE_WIDTH;

        // Default movement speeds for the scientist
        public static float DEFAULT_X_VELOCITY = 4 * EULER_SCALE;
        public static float DEFAULT_Y_VELOCITY = 0;
        public static int DEFAULT_DIRECTION = 1;
        public static float DEFAULT_JUMP_VELOCITY_Y = -12 * EULER_SCALE;
        public static float DEFAULT_JUMP_VELOCITY_X = 10 * EULER_SCALE;
        public static float JUMP_UPSTAIRS_VELOCITY_Y = -23 * EULER_SCALE;
        public static float JUMP_UPSTAIRS_VELOCITY_X = 12 * EULER_SCALE;
        
        // User input related
        public static bool MOVING_INVENTION = false;
        public static int SLOW_MOTION = 4;

        // Default movement speeds for inventions
        public static float DEFAULT_INVENTION_X_VELOCITY = DEFAULT_X_VELOCITY * 4;
        public static float DEFAULT_INVENTION_Y_VELOCITY = 0;

        // Floor constants
        public static int FLOOR_HEIGHT = TILE_HEIGHT;

        // Teleporter info
        public static Texture2D TELEPORTER_TILE_TEXTURE;
        public static Texture2D TELEPORTER_TOP_TEXTURE;
        public static Texture2D TELEPORTER_BOTTOM_TEXTURE;
        public static int TELEPORTER_WIDTH = 50;
        public static int TELEPORTER_HEIGHT = 200;
        public static float TELEPORTER_X_VELOCITY = 0;
        public static float TELEPORTER_Y_VELOCITY = -5 * EULER_SCALE;
        public static float INVENTION_TELEPORTER_X_VELOCITY = 0;
        public static float INVENTION_TELEPORTER_Y_VELOCITY = -8 * EULER_SCALE;

        // Stair constants
        public static int STAIR_WIDTH = 300;
        public static int STAIR_HEIGHT = 250;
        public static float STAIR_X_VELOCITY = 4 * EULER_SCALE;
        public static float STAIR_Y_VELOCITY = 3 * EULER_SCALE;
        public static float INVENTION_STAIR_X_VELOCITY = 8 * EULER_SCALE;
        public static float INVENTION_STAIR_Y_VELOCITY = 6 * EULER_SCALE;

        // Message layer constants
        public static double MESSAGE_TIME = 2;

        // Others
        public static float GRAVITY = -DEFAULT_JUMP_VELOCITY_Y / 11.5f * EULER_SCALE;
        public static int BUFFER = 5;

        // Pit info
        public static Texture2D PIT_LEFT_END_TEXTURE;
        public static Texture2D PIT_RIGHT_END_TEXTURE;
        public static Texture2D PIT_TILE_TEXTURE;
        public static Texture2D PIT_TERMINAL_TEXTURE;

        // Floor info
        public static int NUMBER_OF_FLOORS = 3;
        public static int DISTANCE_BETWEEN_FLOORS = 200;
        public static Texture2D FLOOR_TEXTURE;

        // Door info
        public static int DOOR_WIDTH = 50;
        public static int DOOR_HEIGHT = 150;
        public static Texture2D DOOR_OPEN_TEXTURE;
        public static Texture2D DOOR_CLOSED_TEXTURE;

        // Bed info
        public static int BED_WIDTH = 150;
        public static int BED_HEIGHT = 50;
        public static Texture2D BED_TEXTURE;

        // Wall info
        public static int WALL_WIDTH = 10;
        public static int WALL_HEIGHT = 200;
        public static Texture2D WALL_TEXTURE;

        // Skateboard info
        public static int SKATEBOARD_WIDTH = 100;
        public static int SKATEBOARD_HEIGHT = 25;
        public static int SKATEBOARD_SPEEDUP = 2;
        public static Texture2D ROCKETBOARD_TEXTURE;

        // JackInTheBox info
        public static int JACK_WIDTH = 50;
        public static int JACK_HEIGHT = 50;
        public static Texture2D JACK_TEXTURE;

        // EggBeater info
        public static int EGGBEATER_WIDTH = 12;
        public static int EGGBEATER_HEIGHT = 25;
        public static Texture2D EGG_TEXTURE;

        // Camera constants.
        public static float MINIMUM_ZOOM = 1F;
        public static float ZOOM_STEP = 0.1F;
        public static float ZOOM_ROOM_VIEW = 1F;
        public static float ZOOM_INVENTION_VIEW = 1.75F;
        public static float CAMERA_X_VELO = 9.0F;
        public static float SCROLL_BOUND_LEFT = 0.1F;
        public static float SCROLL_BOUND_RIGHT = 1 - SCROLL_BOUND_LEFT;

        // Game Textures
        public static Texture2D STAIR_TEXTURE;
        public static Texture2D RAILING_TEXTURE;
        public static Texture2D BACKGROUND_TEXTURE;

        #endregion
    }
}