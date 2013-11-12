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
    public enum STATE { MAIN_MENU, LEVEL_SELECT, OPTIONS, PLAY, PAUSE, INSTRUCTIONS }

    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Game
    {

        #region Attributes

        public STATE state = STATE.MAIN_MENU;

        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        SpriteFont _spriteFont;

        // Screen dimensions
        private int screenWidth;
        private int screenHeight;

        // Scroll wheel state
        private int _curScrollWheel;
        private int _deltaScrollWheel;

        // GameObjects
        private Scientist _sleepy;
        private List<Floor> _floors;
        private List<Ladder> _ladders;
        private List<Stairs> _stairs;
        private List<Pit> _pits;
        private List<Invention> _inventions;
        private List<Button> _mainMenuButtons;
        private List<Button> _optionsMenuButtons;
        private List<Button> _levelSelectMenuButtons;
        private List<Button> _pauseMenuButtons;
        private List<Button> _instructionsButtons;

        // Textures
        private Texture2D _stairsTexture;
        private Texture2D _ladderTexture;
        private Texture2D _floorTexture;
        private Texture2D _rocketSkateboardTexture;
        private Texture2D _eggBeaterTexture;
        private Texture2D _jackintheboxTexture;
        private Texture2D _bedTexture;
        private Texture2D _wallTexture;
        private Texture2D _railingTexture;
        private Texture2D _pitLaserLeft;
        private Texture2D _pitLaserRight;
        private Texture2D _pitLaserTile;
        private Texture2D _pitTerminal;
        private Texture2D _doorOpenTexture;
        private Texture2D _doorClosedTexture;

		// Menu stuff.
        private Texture2D _mainMenuButtonTexture;
        private Texture2D _newGameButtonTexture;
        private Texture2D _optionsButtonTexture;
        private Texture2D _levelNumButtonTexture;
        private Texture2D _levelSelectButtonTexture;
        private Texture2D _yesButtonTexture;
        private Texture2D _noButtonTexture;
        private Texture2D _instructionsButtonTexture;
        private Texture2D _resumeButtonTexture;

        private Texture2D _pauseOverlayTexture;
        private Texture2D _instructionsTexture1;

        // Mouse Input
        private MouseState _prevMouseState;
        private MouseState _curMouseState;

        // Keyboard Input
        private KeyboardState _prevKeyboardState;
        private KeyboardState _curKeyboardState;

        // Debug Messages

        // Camera
        private Camera _camera;

        // Test
        private bool _begin = false;
        private int _levelNumber = 1;
        private int _totalLevels = 7;
        private Room level = null;

        #endregion

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
            _ladders = new List<Ladder>();
            _stairs = new List<Stairs>();
            _pits = new List<Pit>();
            _inventions = new List<Invention>();

			// Menu buttons.
            _mainMenuButtons = new List<Button>();
            _optionsMenuButtons = new List<Button>();
            _levelSelectMenuButtons = new List<Button>();
            _pauseMenuButtons = new List<Button>();
            _instructionsButtons = new List<Button>();

            // Initialize Camera.
            _camera = new Camera();

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

            // Load animation sets.
            AnimationLoader.Load("ScientistAnimationSet.xml", Content);

            // Load content of other GameObjects.
            _floorTexture = this.Content.Load<Texture2D>("Image/floor");
            _stairsTexture = this.Content.Load<Texture2D>("Image/stairs");
            _ladderTexture = this.Content.Load<Texture2D>("Image/ladder");
            _rocketSkateboardTexture = this.Content.Load<Texture2D>("Image/rocketSkateboard");
            _eggBeaterTexture = this.Content.Load<Texture2D>("Image/eggBeater");
            _jackintheboxTexture = this.Content.Load<Texture2D>("Image/jackInTheBox");
            _bedTexture = this.Content.Load<Texture2D>("Image/bed");
            _wallTexture = this.Content.Load<Texture2D>("Image/walltile");
            _railingTexture = this.Content.Load<Texture2D>("Image/railing");
            _doorOpenTexture = this.Content.Load<Texture2D>("Image/doorOpen");
            _doorClosedTexture = this.Content.Load<Texture2D>("Image/doorClosed");

            // Load textures for the pits
            _pitLaserLeft = this.Content.Load<Texture2D>("Image/laserLeftEnd");
            _pitLaserRight = this.Content.Load<Texture2D>("Image/laserRightEnd");
            _pitLaserTile = this.Content.Load<Texture2D>("Image/laserTile");
            _pitTerminal = this.Content.Load<Texture2D>("Image/batteryHolder");
            
            // Make these textures static
            GameConstants.FLOOR_TEXTURE = _floorTexture;
            GameConstants.STAIR_TEXTURE = _stairsTexture;
            GameConstants.LADDER_TEXTURE = _ladderTexture;
            GameConstants.ROCKETBOARD_TEXTURE = _rocketSkateboardTexture;
            GameConstants.EGG_TEXTURE = _eggBeaterTexture;
            GameConstants.JACK_TEXTURE = _jackintheboxTexture;
            GameConstants.BED_TEXTURE = _bedTexture;
            GameConstants.RAILING_TEXTURE = _railingTexture;
            GameConstants.PIT_LEFT_END_TEXTURE = _pitLaserLeft;
            GameConstants.PIT_RIGHT_END_TEXTURE = _pitLaserRight;
            GameConstants.PIT_TERMINAL_TEXTURE = _pitTerminal;
            GameConstants.PIT_TILE_TEXTURE = _pitLaserTile;
            GameConstants.DOOR_OPEN_TEXTURE = _doorOpenTexture;
            GameConstants.DOOR_CLOSED_TEXTURE = _doorClosedTexture;

			// Load the menu textures.
            _mainMenuButtonTexture = this.Content.Load<Texture2D>("Image/button_MainMenu");
            _newGameButtonTexture = this.Content.Load<Texture2D>("Image/button_NewGame");
            _optionsButtonTexture = this.Content.Load<Texture2D>("Image/button_Options");
            _levelNumButtonTexture = this.Content.Load<Texture2D>("Image/button_Level");
            _levelSelectButtonTexture = this.Content.Load<Texture2D>("Image/button_LevelSelect");
            _yesButtonTexture = this.Content.Load<Texture2D>("Image/button_Yes");
            _noButtonTexture = this.Content.Load<Texture2D>("Image/button_No");
            _instructionsButtonTexture = this.Content.Load<Texture2D>("Image/button_Instructions");
            _pauseOverlayTexture = this.Content.Load<Texture2D>("Image/pauseOverlay");
            _instructionsTexture1 = this.Content.Load<Texture2D>("Image/test_Instructions1");
            _resumeButtonTexture = this.Content.Load<Texture2D>("Image/button_Resume");
            
            // Create the level.
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

            // Set up Main Menu
            _mainMenuButtons.Add(new Button((screenWidth / 2) - (_newGameButtonTexture.Width / 2), screenHeight / 2 - _newGameButtonTexture.Height, _newGameButtonTexture.Width, _newGameButtonTexture.Height, _newGameButtonTexture));
            _mainMenuButtons.Add(new Button((screenWidth / 2) + (_levelNumButtonTexture.Width / 2), screenHeight / 2 - _levelSelectButtonTexture.Height, _levelSelectButtonTexture.Width, _levelSelectButtonTexture.Height, _levelSelectButtonTexture));
            _mainMenuButtons.Add(new Button((screenWidth / 2), screenHeight / 2 + _optionsButtonTexture.Height, _optionsButtonTexture.Width, _optionsButtonTexture.Height, _optionsButtonTexture));
            _mainMenuButtons.Add(new Button((screenWidth / 2), (screenHeight / 2) + 2 * _instructionsButtonTexture.Height, _instructionsButtonTexture.Width, _instructionsButtonTexture.Height, _instructionsButtonTexture));

            // Set up Options Menu
            _optionsMenuButtons.Add(new Button((screenWidth / 2), _optionsButtonTexture.Height, _optionsButtonTexture.Width, _optionsButtonTexture.Height, _optionsButtonTexture));
            for (int i = 0; i < 3; i++)
            {
                _optionsMenuButtons.Add(new Button((screenWidth / 2) + (_yesButtonTexture.Width / 2), screenHeight / 2 - i * _yesButtonTexture.Height, _yesButtonTexture.Width, _yesButtonTexture.Height, _yesButtonTexture));
                _optionsMenuButtons.Add(new Button((screenWidth / 2) - (_noButtonTexture.Width / 2), screenHeight / 2 - i * _noButtonTexture.Height, _noButtonTexture.Width, _noButtonTexture.Height, _noButtonTexture));
            }
            _optionsMenuButtons.Add(new Button((screenWidth / 2), screenHeight / 2 + _mainMenuButtonTexture.Height, _mainMenuButtonTexture.Width, _mainMenuButtonTexture.Height, _mainMenuButtonTexture));

            // Set up Level Select Menu
            for (int j = 0; j < 3; j++)
            {
                for (int k = 0; k < 3; k++)
                    _levelSelectMenuButtons.Add(new Button(_levelSelectButtonTexture.Width * j, _levelNumButtonTexture.Height * k, _levelNumButtonTexture.Width, _levelNumButtonTexture.Height, _levelNumButtonTexture));
            }
            _levelSelectMenuButtons.Add(new Button((screenWidth / 2), screenHeight / 2 + _mainMenuButtonTexture.Height, _mainMenuButtonTexture.Width, _mainMenuButtonTexture.Height, _mainMenuButtonTexture));

            // Set up the Pause Menu
            _pauseMenuButtons.Add(new Button(0, 0, _pauseOverlayTexture.Width, _pauseOverlayTexture.Height, _pauseOverlayTexture));
            _pauseMenuButtons.Add(new Button((screenWidth / 2), (screenHeight / 2) + _instructionsButtonTexture.Height, _instructionsButtonTexture.Width, _instructionsButtonTexture.Height, _instructionsButtonTexture));
            _pauseMenuButtons.Add(new Button((screenWidth / 2), (screenHeight / 2) - _mainMenuButtonTexture.Height, _resumeButtonTexture.Width, _resumeButtonTexture.Height, _resumeButtonTexture));
            _pauseMenuButtons.Add(new Button((screenWidth / 2), screenHeight / 2, _mainMenuButtonTexture.Width, _mainMenuButtonTexture.Height, _mainMenuButtonTexture));

            // Instructions
            _instructionsButtons.Add(new Button((screenWidth / 2), screenHeight / 2, _resumeButtonTexture.Width, _resumeButtonTexture.Height, _resumeButtonTexture));
            _instructionsButtons.Add(new Button(0, 0, _instructionsTexture1.Width, _instructionsTexture1.Height, _instructionsTexture1));
            //_instructionsButtons.Add(new Button(_instructionsTexture1.Width, 0, _instructionsTexture2.Width, _instructionsTexture2.Height, _instructionsTexture2));
            _instructionsButtons.Add(new Button((screenWidth / 2), (screenHeight / 2) + _mainMenuButtonTexture.Height, _mainMenuButtonTexture.Width, _mainMenuButtonTexture.Height, _mainMenuButtonTexture));
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

            if (state == STATE.PLAY || state == STATE.PAUSE)
            {
                if (_prevKeyboardState.IsKeyDown(Keys.P) && _curKeyboardState.IsKeyUp(Keys.P))
                    state = (state == STATE.PLAY) ? STATE.PAUSE : STATE.PLAY;
            }

            #region Play
            if (state == STATE.PLAY)
            {
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

                Point convertedMousePos = _camera.ToGlobal(new Point(_curMouseState.X, _curMouseState.Y));
				if (_begin)
            	{
                    // Update Game Time.
		            if (GameConstants.MOVING_INVENTION) { Time.Update((float)gameTime.ElapsedGameTime.TotalSeconds / 2); }
		            else { Time.Update((float)gameTime.ElapsedGameTime.TotalSeconds); }
					
		            foreach (Invention invention in _inventions)
		            {
		                invention.Update();
		                Rectangle convertedInventionPos = _camera.ToLocal(invention.RectPosition);
					
		                if (_prevMouseState.LeftButton == ButtonState.Pressed && 
		                _curMouseState.LeftButton == ButtonState.Released &&
		                convertedInventionPos.Contains(new Point(_curMouseState.X, _curMouseState.Y)))
			            {
		                    invention.Clicked = true;
		                    GameConstants.MOVING_INVENTION = true;
						
		                    _camera.ShouldFollowTarget = false;
		                    _camera.Zoom(GameConstants.ZOOM_ROOM_VIEW);
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

		                    _camera.ShouldFollowTarget = true;
		                    _camera.Zoom(GameConstants.ZOOM_INVENTION_VIEW);
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
		                this.Reset();

		                // Load the current level
		                Room level = LevelLoader.Load(_levelNumber);
		                this.level = level;

		                // Create the scientist and set his image
		                _sleepy = new Scientist("Sleepy", level.StartX, level.StartY, 50, 50, level);
                        ResetCamera();
		                this.Load();
		            }

		            // Check if the user lost
		            if (_sleepy.Loser)
		            {
		                _begin = false;
		                this.Reset();

		                // Load the current level
		                Room level = LevelLoader.Load(_levelNumber);
		                this.level = level;

		                //Create the scientist and set his image
		                // Create the scientist and set his image
                        _sleepy = new Scientist("Sleepy", level.StartX, level.StartY, 50, 50, level);
                        ResetCamera();
		                this.Load();
		            }
				}
                else if (_curMouseState.LeftButton == ButtonState.Released &&
                    _prevMouseState.LeftButton == ButtonState.Pressed)
                {
                    _begin = true;

                    // Zoom into the scientist.
                    _camera.Zoom(GameConstants.ZOOM_ROOM_VIEW);
                    _camera.ShouldFollowTarget = true;
                    _camera.Update();
                }

                MessageLayer.Update(gameTime.ElapsedGameTime.TotalSeconds);
            }
            #endregion
            #region Main Menu
            else if (state == STATE.MAIN_MENU)
            {

                if (_prevMouseState.LeftButton == ButtonState.Pressed &&
                    _curMouseState.LeftButton == ButtonState.Released &&
                    _curMouseState.X > _mainMenuButtons[0].X && _curMouseState.X < _mainMenuButtons[0].X + _mainMenuButtons[0].Width &&
                    _curMouseState.Y > _mainMenuButtons[0].Y && _curMouseState.Y < _mainMenuButtons[0].Y + _mainMenuButtons[0].Height)
                {
                    state = STATE.PLAY;
                }
                else if (_prevMouseState.LeftButton == ButtonState.Pressed &&
                    _curMouseState.LeftButton == ButtonState.Released &&
                    _curMouseState.X > _mainMenuButtons[1].X && _curMouseState.X < _mainMenuButtons[1].X + _mainMenuButtons[1].Width &&
                    _curMouseState.Y > _mainMenuButtons[1].Y && _curMouseState.Y < _mainMenuButtons[1].Y + _mainMenuButtons[1].Height)
                {
                    state = STATE.LEVEL_SELECT;
                }
                else if (_prevMouseState.LeftButton == ButtonState.Pressed &&
                    _curMouseState.LeftButton == ButtonState.Released &&
                    _curMouseState.X > _mainMenuButtons[2].X && _curMouseState.X < _mainMenuButtons[2].X + _mainMenuButtons[2].Width &&
                    _curMouseState.Y > _mainMenuButtons[2].Y && _curMouseState.Y < _mainMenuButtons[2].Y + _mainMenuButtons[2].Height)
                {
                    state = STATE.OPTIONS;
                }
                else if (_prevMouseState.LeftButton == ButtonState.Pressed &&
                    _curMouseState.LeftButton == ButtonState.Released &&
                    _curMouseState.X > _mainMenuButtons[3].X && _curMouseState.X < _mainMenuButtons[3].X + _mainMenuButtons[3].Width &&
                    _curMouseState.Y > _mainMenuButtons[3].Y && _curMouseState.Y < _mainMenuButtons[3].Y + _mainMenuButtons[3].Height)
                {
                    state = STATE.INSTRUCTIONS;
                }
            }
            #endregion
            #region Options
            else if (state == STATE.OPTIONS)
            {
                if (_prevMouseState.LeftButton == ButtonState.Pressed &&
                    _curMouseState.LeftButton == ButtonState.Released &&
                    _curMouseState.X > _optionsMenuButtons[_optionsMenuButtons.Count - 1].X && _curMouseState.X < _optionsMenuButtons[_optionsMenuButtons.Count - 1].X + _optionsMenuButtons[_optionsMenuButtons.Count - 1].Width &&
                    _curMouseState.Y > _optionsMenuButtons[_optionsMenuButtons.Count - 1].Y && _curMouseState.Y < _optionsMenuButtons[_optionsMenuButtons.Count - 1].Y + _optionsMenuButtons[_optionsMenuButtons.Count - 1].Height)
                {
                    state = STATE.MAIN_MENU;
                }
            }
            #endregion
            
            #region Level Select
            else if (state == STATE.LEVEL_SELECT)
            {
                if (_prevMouseState.LeftButton == ButtonState.Pressed &&
                    _curMouseState.LeftButton == ButtonState.Released &&
                    _curMouseState.X > _levelSelectMenuButtons[_levelSelectMenuButtons.Count - 1].X && _curMouseState.X < _levelSelectMenuButtons[_levelSelectMenuButtons.Count - 1].X + _levelSelectMenuButtons[_levelSelectMenuButtons.Count - 1].Width &&
                    _curMouseState.Y > _levelSelectMenuButtons[_levelSelectMenuButtons.Count - 1].Y && _curMouseState.Y < _levelSelectMenuButtons[_levelSelectMenuButtons.Count - 1].Y + _levelSelectMenuButtons[_levelSelectMenuButtons.Count - 1].Height)
                {
                    state = STATE.MAIN_MENU;
                }
            }
            #endregion
            #region Pause
            else if (state == STATE.PAUSE)
            {
                if (_prevMouseState.LeftButton == ButtonState.Pressed &&
                    _curMouseState.LeftButton == ButtonState.Released &&
                    _curMouseState.X > _pauseMenuButtons[1].X && _curMouseState.X < _pauseMenuButtons[1].X + _pauseMenuButtons[1].Width &&
                    _curMouseState.Y > _pauseMenuButtons[1].Y && _curMouseState.Y < _pauseMenuButtons[1].Y + _pauseMenuButtons[1].Height)
                {
                    state = STATE.INSTRUCTIONS;
                }
                else if (_prevMouseState.LeftButton == ButtonState.Pressed &&
                    _curMouseState.LeftButton == ButtonState.Released &&
                    _curMouseState.X > _pauseMenuButtons[_pauseMenuButtons.Count - 1].X && _curMouseState.X < _pauseMenuButtons[_pauseMenuButtons.Count - 1].X + _pauseMenuButtons[_pauseMenuButtons.Count - 1].Width &&
                    _curMouseState.Y > _pauseMenuButtons[_pauseMenuButtons.Count - 1].Y && _curMouseState.Y < _pauseMenuButtons[_pauseMenuButtons.Count - 1].Y + _pauseMenuButtons[_pauseMenuButtons.Count - 1].Height)
                {
                    state = STATE.MAIN_MENU;
                }
                else if (_prevMouseState.LeftButton == ButtonState.Pressed &&
                    _curMouseState.LeftButton == ButtonState.Released &&
                    _curMouseState.X > _pauseMenuButtons[2].X && _curMouseState.X < _pauseMenuButtons[2].X + _pauseMenuButtons[2].Width &&
                    _curMouseState.Y > _pauseMenuButtons[2].Y && _curMouseState.Y < _pauseMenuButtons[2].Y + _pauseMenuButtons[2].Height)
                {
                    state = STATE.PLAY;
                }
            }
            #endregion
            #region Instructions
            else if (state == STATE.INSTRUCTIONS)
            {
                if (_prevMouseState.LeftButton == ButtonState.Pressed &&
                   _curMouseState.LeftButton == ButtonState.Released &&
                   _curMouseState.X > _instructionsButtons[_instructionsButtons.Count - 1].X && _curMouseState.X < _instructionsButtons[_instructionsButtons.Count - 1].X + _instructionsButtons[_instructionsButtons.Count - 1].Width &&
                   _curMouseState.Y > _instructionsButtons[_instructionsButtons.Count - 1].Y && _curMouseState.Y < _instructionsButtons[_instructionsButtons.Count - 1].Y + _instructionsButtons[_instructionsButtons.Count - 1].Height)
                {
                    state = STATE.MAIN_MENU;
                }
                else if (_prevMouseState.LeftButton == ButtonState.Pressed &&
                   _curMouseState.LeftButton == ButtonState.Released &&
                   _curMouseState.X > _instructionsButtons[0].X && _curMouseState.X < _instructionsButtons[0].X + _instructionsButtons[0].Width &&
                   _curMouseState.Y > _instructionsButtons[0].Y && _curMouseState.Y < _instructionsButtons[0].Y + _instructionsButtons[0].Height)
                {
                    state = STATE.PLAY;
                }
            }
            #endregion

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

            if (state == STATE.PLAY || state == STATE.PAUSE)
            {
		        // Draw the background
		        GameObject wallTile;
		        for (int x = 0; x < GameConstants.SCREEN_WIDTH; x += 50)
		        {
		            for (int y = 0; y < GameConstants.SCREEN_HEIGHT; y += 50)
		            {
		                wallTile = new GameObject(x, y, 50, 50, GameConstants.DEFAULT_DIRECTION);
		                wallTile.Image = _wallTexture;
		                wallTile.Draw(spriteBatch);
		            }
		        }
				
                _camera.DrawGameObjects(spriteBatch, _sleepy.Room.GetGameObjects());
                _camera.DrawGameObject(spriteBatch, _sleepy );
                
                if (state == STATE.PAUSE)
                {
                    foreach (Button b in _pauseMenuButtons)
                        b.Draw(spriteBatch);
                }
            }
            else if (state == STATE.MAIN_MENU)
            {
                foreach (Button b in _mainMenuButtons)
                    b.Draw(spriteBatch);
            }
            else if (state == STATE.OPTIONS)
            {
                foreach (Button b in _optionsMenuButtons)
                    b.Draw(spriteBatch);
            }
            else if (state == STATE.LEVEL_SELECT)
            {
                foreach (Button b in _levelSelectMenuButtons)
                    b.Draw(spriteBatch);
            }
            else if (state == STATE.INSTRUCTIONS)
            {
                foreach (Button b in _instructionsButtons)
                    b.Draw(spriteBatch);
            }

            spriteBatch.End();

            base.Draw(gameTime);
        }

        /// <summary>
        /// Resets all of the drawable objects in the level
        /// </summary>
        public void Reset()
        {
            _stairs.Clear();
            _ladders.Clear();
            _inventions.Clear();
            _floors.Clear();
            _pits.Clear();
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
        public void Load()
        {
            // Store all the GameObjects.
            // This should be inside of the Level Class when we get to it.
            foreach (Floor floor in this.level.Floors)
            {
                _floors.Add(floor);
                _ladders.AddRange(floor.Ladders);
                _stairs.AddRange(floor.Stairs);
                _pits.AddRange(floor.Pits);
                _inventions.AddRange(floor.Inventions);
            }
        }
    }
}