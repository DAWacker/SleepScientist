using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Storage;
using Microsoft.Xna.Framework.GamerServices;

namespace SleepyScientist
{
    public enum STATE { MAIN_MENU, LEVEL_SELECT, PLAY, PAUSE, INSTRUCTIONS }

    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Game
    {
        #region Attributes

        // State variables
        private static STATE _state = STATE.MAIN_MENU;
        public static STATE PrevState = _state;
        public static STATE State
        {
            get{ return _state; }
            // Save previous value and get new value
            set{ PrevState = _state; _state = value; }
        }
        private Menu _menu;

        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        SpriteFont _spriteFont;

        // Screen dimensions
        public static int screenWidth;
        public static int screenHeight;

        // Scroll wheel state
        private int _curScrollWheel;
        private int _deltaScrollWheel;

        // GameObjects
        private Scientist _sleepy;
        private List<Floor> _floors;
        private List<Ladder> _ladders;
        private List<Stairs> _stairs;
        private List<Invention> _inventions;

        // Textures
        private Texture2D _stairsTexture;
        private Texture2D _ladderTexture;
        private Texture2D _floorTexture;
        private Texture2D _rocketSkateboardTexture;
        private Texture2D _eggBeaterTexture;
        private Texture2D _jackintheboxTexture;

        // Mouse Input
        public static MouseState _prevMouseState;
        public static MouseState _curMouseState;

        // Keyboard Input
        public static KeyboardState _prevKeyboardState;
        public static KeyboardState _curKeyboardState;

        // Debug Messages

        // Camera
        private Camera _camera;

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
            _inventions = new List<Invention>();

            // Initialize Camera.
            _camera = new Camera();

            // Initialize menus
            _menu = new Menu();
            _menu.Initialize();

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
			
            // Make these textures static
            GameConstants.FLOOR_TEXTURE = _floorTexture;
            GameConstants.STAIR_TEXTURE = _stairsTexture;
            GameConstants.LADDER_TEXTURE = _ladderTexture;
            GameConstants.ROCKETBOARD_TEXTURE = _rocketSkateboardTexture;
            GameConstants.EGG_TEXTURE = _eggBeaterTexture;
            GameConstants.JACK_TEXTURE = _jackintheboxTexture;

            // Add some test messages.
            MessageLayer.AddMessage(new Message("Test", 0, 0));
            MessageLayer.AddMessage(new Message("Test 5 Seconds", 0, 30, 5));

            Room level = LevelLoader.Load("Level01");
            int startx = level.StartX;

            // Create the scientist and set his image
            _sleepy = new Scientist("Sleepy", level.StartX, level.StartY, 50, 50, level);

            // Store all the GameObjects.
            // This should be inside of the Level Class when we get to it.
            foreach (Floor floor in level.Floors)
            {
                _inventions.AddRange(floor.Inventions);
            }
            _camera.FollowTarget = _sleepy;

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

            if (Keyboard.GetState().IsKeyDown(Keys.OemPlus))
                _camera.ModZoom(GameConstants.ZOOM_STEP);

            if (Keyboard.GetState().IsKeyDown(Keys.OemMinus))
                _camera.ModZoom(-GameConstants.ZOOM_STEP);

            if (Keyboard.GetState().IsKeyDown(Keys.NumPad0))
                _camera.Zoom(1);

            _prevMouseState = _curMouseState;
            _curMouseState = Mouse.GetState();
            _prevKeyboardState = _curKeyboardState;
            _curKeyboardState = Keyboard.GetState();

            if (Keyboard.GetState().IsKeyDown(Keys.NumPad0))
                _camera.Zoom(1);
            
            #region Play
            if (_state == STATE.PLAY)
            {
                if (Keyboard.GetState().IsKeyDown(Keys.OemPlus))
                    _camera.ModZoom(GameConstants.ZOOM_STEP);

                if (Keyboard.GetState().IsKeyDown(Keys.OemMinus))
                    _camera.ModZoom(-GameConstants.ZOOM_STEP);

                if (Keyboard.GetState().IsKeyDown(Keys.NumPad0))
                    _camera.Zoom(1);

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
                        // Zoom out and camera stops following its target.
                        _camera.Zoom(GameConstants.ZOOM_ROOM_VIEW);
                        _camera.ShouldFollowTarget = false;
                    }
                }

                // Update global Time class.
                Time.Update((float)gameTime.ElapsedGameTime.TotalSeconds);

                Point convertedMousePos = _camera.ToGlobal(new Point(_curMouseState.X, _curMouseState.Y));
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
                        invention.VeloX = GameConstants.DEFAULT_INVENTION_X_VELO;
                        Console.WriteLine(invention.TargetX + " : " + (invention.Y + invention.Height - invention.TargetY));
                        invention.DeterminePath();
                        GameConstants.MOVING_INVENTION = false;

                        _camera.ShouldFollowTarget = true;
                        _camera.Zoom(GameConstants.ZOOM_INVENTION_VIEW);
                    }
                }
				/*
		        foreach (Floor floor in _sleepy.Room.Floors)
		        {
		            try { MessageLayer.AddMessage(new Message(floor.Ladders[0].X.ToString(), floor.Ladders[0].X, floor.Ladders[0].Y, GameConstants.MESSAGE_TIME)); }
		            catch { }
		        }
		        */

                _sleepy.Update();
                MessageLayer.Update(gameTime.ElapsedGameTime.TotalSeconds);
                if (_camera.ShouldFollowTarget == false)
                    _camera.UpdateCameraScroll(_curMouseState.X, _curMouseState.Y);

                _camera.Update();
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

            /*
            // Draw the messages.
            MessageLayer.Draw(spriteBatch);
            */

            if (_state == STATE.PLAY || _state == STATE.PAUSE)
            {
                _camera.DrawGameObjects(spriteBatch, _sleepy.Room.GetGameObjects());
                _camera.DrawGameObject(spriteBatch, _sleepy );
            }

            // Draw menus
            _menu.Draw(spriteBatch);

            //MessageLayer.ClearMessages();
            //MessageLayer.AddMessage(new Message("-The objective of the game is to get the scientist to his bed in each level.\n-You may pick up inventions by clicking on them, and move the inventions by clicking on the place you want to move them.", 0, 0));
            //MessageLayer.Draw(spriteBatch);

            spriteBatch.End();

            base.Draw(gameTime);
        }

        /// <summary>
        /// Creates and positions Floors and Ladders for testing.
        /// </summary>
        /// <param name="numFloors">The number of floors to create for the test environment.</param>
        /// <param name="createLadders">Should Ladders be added to the test environment?</param>
        private void SetupLevel(int numFloors, int startFloor, bool createLadders = false, bool createStairs = false)
        {
            Random rand = new Random();
            Room room = new Room(numFloors, startFloor - 1, 0, 0);
            int x;
            int y;
            int width = screenWidth;
            int distanceBetweenFloors = screenHeight / numFloors;

            // Add Floors.
            for (int i = 0; i < numFloors; i++)
            {
                x = 0;
                y = screenHeight - distanceBetweenFloors * i - GameConstants.FLOOR_HEIGHT;
                Floor floor = new Floor(x, y, width, GameConstants.FLOOR_HEIGHT);
                floor.Image = _floorTexture;
                room.Floors.Add(floor);

                // Add ladder
                if (createLadders && i != numFloors - 1)
                {
                    x = rand.Next(screenWidth - GameConstants.TILE_WIDTH);
                    y = floor.Y - distanceBetweenFloors - GameConstants.TILE_HEIGHT;
                    Ladder ladderToAdd = new Ladder(x, y, GameConstants.LADDER_WIDTH, distanceBetweenFloors + GameConstants.TILE_HEIGHT);
                    ladderToAdd.Image = _ladderTexture;
                    floor.Ladders.Add(ladderToAdd);
                }

                if (createStairs && i != 0)
                {
                    x = rand.Next(screenWidth - GameConstants.TILE_WIDTH);
                    y = screenHeight - distanceBetweenFloors * i - GameConstants.FLOOR_HEIGHT;
                    Stairs stair = new Stairs(x, y, GameConstants.LADDER_WIDTH, distanceBetweenFloors);
                    stair.Image = _stairsTexture;
                }
            }
        }

        /// <summary>
        /// !This should be inside of AI, but it requires AI to have a currentFloor and Floors to have a list of Inventions!
        /// Updates the states of the objects.
        /// </summary>
        private bool handleCollisions(AI ai)
        {
            bool hasCollided = false;

            foreach (Ladder ladder in _ladders)
            {
                if ( ladder.RectPosition.Contains( ai.RectPosition.Center ) )
                {
                    //hasCollided = ai.InteractWith(ladder);                    
                }
            }

            return hasCollided;
        }
    }
}