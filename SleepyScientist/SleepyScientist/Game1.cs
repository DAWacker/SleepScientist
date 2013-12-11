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
    public enum STATE { MAIN_MENU, LEVEL_SELECT, PLAY, PAUSE, INSTRUCTIONS, GAME_OVER }

    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Game
    {

        #region Attributes

        // State variables
        private  STATE _state = STATE.MAIN_MENU;
        public STATE PrevState = STATE.MAIN_MENU;   // error can't access _state?
        public  STATE State
        {
            get{ return _state; }
            // Save previous value and get new value
            set { PrevState = _state; _state = value; }
        }
        private Menu _menu;

        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        SpriteFont _spriteFont;

        // Screen dimensions
        public int screenWidth;
        public int screenHeight;

        // Scroll wheel state
        private int _curScrollWheel;
        //private int _deltaScrollWheel;

        // GameObjects
        private Scientist _sleepy;
        private List<Floor> _floors;
        private List<Teleporter> _teleporters;
        private List<Stairs> _stairs;
        private List<Pit> _pits;
        private List<Invention> _inventions;

        // Mouse Input
        public MouseState _prevMouseState;
        public MouseState _curMouseState;

        // Keyboard Input
        public KeyboardState _prevKeyboardState;
        public KeyboardState _curKeyboardState;

        // Debug Messages

        // Camera
        private Camera _camera;

        // Test
        private bool _begin = false;
        public int _levelNumber = 7;
        public int _totalLevels = 7;
        private Room level = null;

        #endregion

        public int StartLevel { get { return _levelNumber; } }

        public Game1()
            : base()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {

            // Turns off full screen and sets the background
            graphics.IsFullScreen = false;
            graphics.PreferredBackBufferWidth = GameConstants.SCREEN_WIDTH;
            graphics.PreferredBackBufferHeight = GameConstants.SCREEN_HEIGHT;
            graphics.ApplyChanges();

            // Save the new screen dimensions locally
            screenWidth = GameConstants.SCREEN_WIDTH;
            screenHeight = GameConstants.SCREEN_HEIGHT;

            // Initialize scroll wheel position.
            _curScrollWheel = Mouse.GetState().ScrollWheelValue;

            // Initialize test "Level" objects.
            _floors = new List<Floor>();
            _teleporters = new List<Teleporter>();
            _stairs = new List<Stairs>();
            _pits = new List<Pit>();
            _inventions = new List<Invention>();

            // Initialize Camera.
            _camera = new Camera();

            // Initialize menus
            _menu = new Menu();
            _menu.Initialize(this);

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            // Load the font for the Messages and give it to the MessageLayer.
            _spriteFont = Content.Load<SpriteFont>("Font/defaultFont");
            MessageLayer.Font = _spriteFont;

            // Set up the TextHelper.
            TextHelper.Batch = spriteBatch;
            TextHelper.Font = _spriteFont;
            TextHelper.TextColor = Color.White;
            TextHelper.Alignment = TextHelper.TextAlignment.Center;

            // Load animation sets.
            AnimationLoader.Load("ScientistAnimationSet.xml", Content);
            
            // Make these textures static
            GameConstants.FLOOR_TEXTURE = this.Content.Load<Texture2D>("Image/floor"); ;
            GameConstants.STAIR_TEXTURE = this.Content.Load<Texture2D>("Image/stairs");
            GameConstants.TELEPORTER_TILE_TEXTURE = this.Content.Load<Texture2D>("Image/teleporter_tile");
            GameConstants.TELEPORTER_BOTTOM_TEXTURE = this.Content.Load<Texture2D>("Image/teleporter_bottom");
            GameConstants.TELEPORTER_TOP_TEXTURE = this.Content.Load<Texture2D>("Image/teleporter_top");
            GameConstants.ROCKETBOARD_TEXTURE = this.Content.Load<Texture2D>("Image/skateboard");
            GameConstants.EGG_TEXTURE = this.Content.Load<Texture2D>("Image/eggbeater");
            GameConstants.JACK_TEXTURE = this.Content.Load<Texture2D>("Image/jack_inthe_box");
            GameConstants.BED_TEXTURE = this.Content.Load<Texture2D>("Image/bed");
            GameConstants.RAILING_TEXTURE = this.Content.Load<Texture2D>("Image/railing");
            GameConstants.PIT_LEFT_END_TEXTURE = this.Content.Load<Texture2D>("Image/laser_left_end");
            GameConstants.PIT_RIGHT_END_TEXTURE = this.Content.Load<Texture2D>("Image/laser_right_end");
            GameConstants.PIT_TERMINAL_TEXTURE = this.Content.Load<Texture2D>("Image/battery_holder");
            GameConstants.PIT_TILE_TEXTURE = this.Content.Load<Texture2D>("Image/laser_tile");
            GameConstants.DOOR_OPEN_TEXTURE = this.Content.Load<Texture2D>("Image/door_open");
            GameConstants.DOOR_CLOSED_TEXTURE = this.Content.Load<Texture2D>("Image/door_closed");
            GameConstants.BACKGROUND_TEXTURE = this.Content.Load<Texture2D>("Image/wall_tile");
            GameConstants.WALL_TEXTURE = this.Content.Load<Texture2D>("Image/floor");
            GameConstants.BLANK = this.Content.Load<Texture2D>("Image/blank");

            Room level = LevelLoader.Load(_levelNumber);

            // This startx is a test to see if the loader broke
            int startx = level.StartX;

            // Create the scientist
            _sleepy = new Scientist("Sleepy", level.StartX, level.StartY, 50, 50, level);
            ResetCamera();

            // Store all the GameObjects.
            foreach (Floor floor in level.Floors)
            {
                _inventions.AddRange(floor.Inventions);
            }

            // Load content for menus
            _menu.LoadContent(this.Content);
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            // Handle input.
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            _prevMouseState = _curMouseState;
            _curMouseState = Mouse.GetState();
            _prevKeyboardState = _curKeyboardState;
            _curKeyboardState = Keyboard.GetState();

            foreach (Invention invention in _inventions)
            {
                Rectangle convertedInventionPos = _camera.ToLocal(invention.SelectionBox);

                if (convertedInventionPos.Contains(new Point(_curMouseState.X, _curMouseState.Y))) { invention.Hovered = true; }
                else { invention.Hovered = false; }
            }

            #region Play
            if (_state == STATE.PLAY)
            {
                #region Camera Code
                /*
                // Update mouse
                _deltaScrollWheel = Mouse.GetState().ScrollWheelValue - _curScrollWheel;

                if (_deltaScrollWheel != 0)
                {
                    _curScrollWheel = Mouse.GetState().ScrollWheelValue;
                    // If scroll up.
                    if (_deltaScrollWheel > 0)
                    {
                        // Zoom in.
                        _camera.ZoomToLocation(Mouse.GetState().X, Mouse.GetState().Y);
                        // Camera resumes following target if player is not moving an invention.
                        if (GameConstants.MOVING_INVENTION == false)
                            _camera.ShouldFollowTarget = true;
                    }
                    // If scroll down.
                    else
                    {
                        // Zoom out only if player is moving an invention.
                        if ( GameConstants.MOVING_INVENTION == true )
                            _camera.Zoom(GameConstants.ZOOM_ROOM_VIEW);
                    }
                }
                 */
                #endregion

                Point convertedMousePos = _camera.ToGlobal(new Point(_curMouseState.X, _curMouseState.Y));
				if (_begin)
            	{
                    // Update Game Time.
		            if (GameConstants.MOVING_INVENTION) { Time.Update((float)gameTime.ElapsedGameTime.TotalSeconds / 2); }
		            else { Time.Update((float)gameTime.ElapsedGameTime.TotalSeconds); }

		            foreach (Invention invention in _inventions)
		            {
		                invention.Update();
		                Rectangle convertedInventionPos = _camera.ToLocal(invention.SelectionBox);

                        // Reset inventions
                        if (invention.Activated && !invention.HasTarget && !_sleepy.RectPosition.Intersects(invention.SelectionBox)) { invention.UnUse(); }

                        if (!invention.Activated && !invention.HasTarget)
                        {
                            if (convertedInventionPos.Contains(new Point(_curMouseState.X, _curMouseState.Y))) { invention.Hovered = true; }
                            else { invention.Hovered = false; }

                            if (_prevMouseState.LeftButton == ButtonState.Pressed &&
                            _curMouseState.LeftButton == ButtonState.Released &&
                            convertedInventionPos.Contains(new Point(_curMouseState.X, _curMouseState.Y)))
                            {
                                invention.Clicked = true;
                                GameConstants.MOVING_INVENTION = true;

                                //_camera.ShouldFollowTarget = false;
                                //_camera.Zoom(GameConstants.ZOOM_ROOM_VIEW);
                                break;
                            }

                            if (invention.Clicked && _prevMouseState.LeftButton == ButtonState.Pressed &&
                                _curMouseState.LeftButton == ButtonState.Released)
                            {
                                invention.HasTarget = true;
                                invention.Clicked = false;
                                invention.TargetX = convertedMousePos.X;
                                invention.TargetY = convertedMousePos.Y;
                                invention.VeloX = GameConstants.DEFAULT_INVENTION_X_VELOCITY;
                                invention.DeterminePath();
                                GameConstants.MOVING_INVENTION = false;

                                //_camera.ShouldFollowTarget = true;
                                //_camera.Zoom(GameConstants.ZOOM_INVENTION_VIEW);
                            }
                        }
		            }

		            _sleepy.Update();
		            MessageLayer.Update(gameTime.ElapsedGameTime.TotalSeconds);
		            if (_camera.ShouldFollowTarget == false)
		                _camera.UpdateCameraScroll(_curMouseState.X, _curMouseState.Y);

		            _camera.Update();

					// Check if the user won
		            if (_sleepy.Winner)
		            {
		                _begin = false;
		                if (_levelNumber == _totalLevels) { _levelNumber = 1; }
		                else { _levelNumber++; }

                        this.SetupLevel(this._levelNumber);
		            }

		            // Check if the user lost
		            if (_sleepy.Loser)
		            {
		                _begin = false;

                        this.State = STATE.GAME_OVER;

		                this.SetupLevel(this._levelNumber);
		            }
				}
                else if (_curMouseState.LeftButton == ButtonState.Released &&
                    _prevMouseState.LeftButton == ButtonState.Pressed)
                {
                    _begin = true;

                    // Zoom into the scientist.
                    //_camera.Zoom(GameConstants.ZOOM_ROOM_VIEW);
                    //_camera.ShouldFollowTarget = true;
                    //_camera.Update();
                }

                MessageLayer.Update(gameTime.ElapsedGameTime.TotalSeconds);
            }
            #endregion

            // Update menus
            _menu.Update();

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            spriteBatch.Begin();

            if (_state == STATE.PLAY || _state == STATE.PAUSE)
            {
		        // Draw the background
		        GameObject wallTile;
		        for (int x = 0; x < GameConstants.SCREEN_WIDTH; x += 50)
		        {
		            for (int y = 0; y < GameConstants.SCREEN_HEIGHT; y += 50)
		            {
		                wallTile = new GameObject(x, y, 50, 50, GameConstants.DEFAULT_DIRECTION);
		                wallTile.Image = GameConstants.BACKGROUND_TEXTURE;
		                wallTile.Draw(spriteBatch);
		            }
		        }
				
                _camera.DrawGameObjects(spriteBatch, _sleepy.Room.GetGameObjects());
            }

            // Draw menus
            _menu.Draw(spriteBatch);
            MessageLayer.Draw(spriteBatch);

            spriteBatch.End();

            base.Draw(gameTime);
        }

        /// <summary>
        /// Resets the camera to view the entire room and target the new scientist.
        /// Should be called at the start of each level.
        /// </summary>
        public void ResetCamera()
        {
            _camera.FollowTarget = _sleepy;
            _camera.Zoom(GameConstants.ZOOM_ROOM_VIEW);
            _camera.ShouldFollowTarget = false;
            _camera.Update();
        }

        /// <summary>
        /// Loads all of the drawable objects in the level
        /// </summary>
        public void SetupLevel(int levelNum)
        {
            // Stop updating
            _begin = false;

            // Stop moving invention if we are.
            GameConstants.MOVING_INVENTION = false;

            // Reset all of the drawable objects in the level
            _stairs.Clear();
            _teleporters.Clear();
            _inventions.Clear();
            _floors.Clear();
            _pits.Clear();

            // Load the current level
            Room level = LevelLoader.Load(_levelNumber);
            this.level = level;

            //Create the scientist and set his image
            // Create the scientist and set his image
            _sleepy = new Scientist("Sleepy", level.StartX, level.StartY, 50, 50, level);
            ResetCamera();

            // Store all the GameObjects.
            // This should be inside of the Level Class when we get to it.
            foreach (Floor floor in this.level.Floors)
            {
                _floors.Add(floor);
                _teleporters.AddRange(floor.Teleporters);
                _stairs.AddRange(floor.Stairs);
                _pits.AddRange(floor.Pits);
                _inventions.AddRange(floor.Inventions);
            }
        }
    }
}