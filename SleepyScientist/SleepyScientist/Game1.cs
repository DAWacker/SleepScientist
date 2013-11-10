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
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Game
    {

        #region Attributes

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
        private List<GameObject> _allGameObjects;

        // Textures
        private Texture2D _scientistTexture;
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

        // Mouse Input
        private MouseState _prevMouseState;
        private MouseState _curMouseState;

        // Debug Messages

        // Camera
        private Camera _camera;

        // Test
        private bool _begin = false;
        private int _levelNumber = 4;
        private int _totalLevels = 6;
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

            // Initialize Camera.
            _camera = new Camera();

            // Initialize what the Camera will be drawing.
            _allGameObjects = new List<GameObject>();

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

            // Load in the scientist placeholder
            _scientistTexture = this.Content.Load<Texture2D>("Image/scientist");

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

            // Add some test messages.
            MessageLayer.AddMessage(new Message("Test", 0, 0));
            MessageLayer.AddMessage(new Message("Test 5 Seconds", 0, 30, 5));

            Room level = LevelLoader.Load(_levelNumber);

            // This startx is a test to see if the loader broke
            int startx = level.StartX;

            // Create the scientist and set his image
            _sleepy = new Scientist("Sleepy", level.StartX, level.StartY, 50, 50, level);
            _sleepy.Image = _scientistTexture;

            // Store all the GameObjects.
            // This should be inside of the Level Class when we get to it.
            foreach (Floor floor in level.Floors)
            {
                _allGameObjects.Add(floor);
                _floors.Add(floor);
                _allGameObjects.AddRange(floor.Ladders);
                _ladders.AddRange(floor.Ladders);
                _allGameObjects.AddRange(floor.Stairs);
                _stairs.AddRange(floor.Stairs);
                _allGameObjects.AddRange(floor.Pits);
                _pits.AddRange(floor.Pits);
                _allGameObjects.AddRange(floor.Inventions);
                _inventions.AddRange(floor.Inventions);
            }
            if (level.Door != null) { _allGameObjects.Add(level.Door); }
            _allGameObjects.Add(_sleepy);

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


            // Update mouse
            _deltaScrollWheel = Mouse.GetState().ScrollWheelValue - _curScrollWheel;

            if (_deltaScrollWheel != 0)
            {
                _curScrollWheel = Mouse.GetState().ScrollWheelValue;
                if (_deltaScrollWheel > 0)
                    _camera.ZoomToLocation(Mouse.GetState().X, Mouse.GetState().Y);
            }

            _prevMouseState = _curMouseState;
            _curMouseState = Mouse.GetState();

            if (_begin)
            {
                foreach (Invention invention in _inventions)
                {
                    invention.Update();
                    if (_prevMouseState.LeftButton == ButtonState.Pressed &&
                        _curMouseState.LeftButton == ButtonState.Released &&
                        _curMouseState.X > invention.X && _curMouseState.X < invention.X + invention.Width &&
                        _curMouseState.Y > invention.Y && _curMouseState.Y < invention.Y + invention.Height)
                    {
                        invention.Clicked = true;
                        GameConstants.MOVING_INVENTION = true;
                        break;
                    }

                    if (invention.Clicked && _prevMouseState.LeftButton == ButtonState.Pressed &&
                        _curMouseState.LeftButton == ButtonState.Released)
                    {
                        invention.HasTarget = true;
                        invention.Clicked = false;
                        invention.TargetX = _curMouseState.X;
                        invention.TargetY = _curMouseState.Y;
                        invention.VeloX = GameConstants.DEFAULT_INVENTION_X_VELOCITY;
                        invention.DeterminePath();
                        GameConstants.MOVING_INVENTION = false;
                    }
                }

                if (GameConstants.MOVING_INVENTION) { Time.Update((float)gameTime.ElapsedGameTime.TotalSeconds / 2); }
                else { Time.Update((float)gameTime.ElapsedGameTime.TotalSeconds); }

                _sleepy.Update();

                // Check if the door closed
                if (_sleepy.Room.Door != null) { if (Time.CurTime >= _sleepy.Room.Door.Time) { _sleepy.Loser = true; } }

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
                    _sleepy.Image = _scientistTexture;
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
                    _sleepy.Image = _scientistTexture;
                    this.Load();
                }

                MessageLayer.Update(gameTime.ElapsedGameTime.TotalSeconds);
            }
            else if (_curMouseState.LeftButton == ButtonState.Released &&
                _prevMouseState.LeftButton == ButtonState.Pressed) { _begin = true; }

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

            // Draw the floors
            foreach (Floor floor in _floors) { floor.Draw(spriteBatch); }

            // Draw the ladders
            foreach (Ladder ladder in _ladders) { ladder.Draw(spriteBatch); }

            // Draw the stairs
            foreach (Stairs stair in _stairs) { stair.Draw(spriteBatch); }

            // Draw the inventions
            foreach (Invention invention in _inventions) { invention.Draw(spriteBatch); }

            // Draw the pits
            foreach (Pit pit in _pits) { pit.Draw(spriteBatch); }

            // Draw the bed
            _sleepy.Room.Bed.Draw(spriteBatch);

            // Draw the scientist.
            _sleepy.Draw(spriteBatch);

            // Draw the door
            if (_sleepy.Room.Door != null) { _sleepy.Room.Door.Draw(spriteBatch); }

            // Draw the stair railings
            foreach (Stairs stair in _stairs) { stair.Railing.Draw(spriteBatch); }

            // Draw the messages.
            MessageLayer.Draw(spriteBatch);

            spriteBatch.End();

            base.Draw(gameTime);
        }

        /// <summary>
        /// Resets all of the drawable objects in the level
        /// </summary>
        public void Reset()
        {
            _allGameObjects.Clear();
            _stairs.Clear();
            _ladders.Clear();
            _inventions.Clear();
            _floors.Clear();
            _pits.Clear();
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
                _allGameObjects.Add(floor);
                _floors.Add(floor);
                _allGameObjects.AddRange(floor.Ladders);
                _ladders.AddRange(floor.Ladders);
                _allGameObjects.AddRange(floor.Stairs);
                _stairs.AddRange(floor.Stairs);
                _allGameObjects.AddRange(floor.Pits);
                _pits.AddRange(floor.Pits);
                _allGameObjects.AddRange(floor.Inventions);
                _inventions.AddRange(floor.Inventions);
            }
            _allGameObjects.Add(_sleepy);
        }
    }
}