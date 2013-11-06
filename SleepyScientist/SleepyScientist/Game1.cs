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
        private Texture2D _pitTexture;

        // Mouse Input
        private MouseState _prevMouseState;
        private MouseState _curMouseState;

        // Debug Messages

        // Camera
        private Camera _camera;

        // Test
        private bool _begin = false;

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
            _rocketSkateboardTexture = this.Content.Load<Texture2D>("Image/skateboard");
            _eggBeaterTexture = this.Content.Load<Texture2D>("Image/beater");
            _jackintheboxTexture = this.Content.Load<Texture2D>("Image/jack");
            _bedTexture = this.Content.Load<Texture2D>("Image/bed");
            _pitTexture = this.Content.Load<Texture2D>("Image/pit");
            
            // Make these textures static
            GameConstants.FLOOR_TEXTURE = _floorTexture;
            GameConstants.STAIR_TEXTURE = _stairsTexture;
            GameConstants.LADDER_TEXTURE = _ladderTexture;
            GameConstants.ROCKETBOARD_TEXTURE = _rocketSkateboardTexture;
            GameConstants.EGG_TEXTURE = _eggBeaterTexture;
            GameConstants.JACK_TEXTURE = _jackintheboxTexture;
            GameConstants.BED_TEXTURE = _bedTexture;
            GameConstants.PIT_TEXTURE = _pitTexture;

            // Add some test messages.
            MessageLayer.AddMessage(new Message("Test", 0, 0));
            MessageLayer.AddMessage(new Message("Test 5 Seconds", 0, 30, 5));

            // Set up the test "Level".
            // SetupLevel(4, true);
            //SetupLevel(GameConstants.NUMBER_OF_FLOORS, 1, true, true);

            // Set up inventions.
            /*
            Invention toAdd = new RocketSkateboard("RocketSkateboard", screenWidth / 2, _floors[0].Y - _sleepy.Height, 50, 50);
            toAdd.VeloX *= -1;
            toAdd.Image = _rocketSkateboardTexture;
            _inventions.Add(toAdd);
            toAdd = new RocketSkateboard("RocketSkateboard", screenWidth / 2, _floors[1].Y - _sleepy.Height, 50, 50);
            toAdd.Image = _rocketSkateboardTexture;
            _inventions.Add(toAdd);
            toAdd = new RocketSkateboard("RocketSkateboard", screenWidth / 2, _floors[3].Y - _sleepy.Height, 50, 50);
            toAdd.Image = _rocketSkateboardTexture;
            _inventions.Add(toAdd);
             */
            /*
            Invention beater = new EggBeater("EggBeater", screenWidth / 2, _floors[1].Y - _sleepy.Height, 50, 50);
            beater.Image = _eggBeaterTexture;
            _inventions.Add(beater);
            */  

            // Setup test animations.
            //_sleepy.Animations = AnimationLoader.GetSetCopy("Scientist");
            //_sleepy.Animations.ChangeAnimation("Walk");
            //_sleepy.Animations.CurAnimation.Pause();
            /*_testAnimation = new Animation("Test");
            _testAnimation.TimePerFrame = .25F;    // Second/Frame
            _testAnimation.Images = new List<Texture2D>() {
                _floorTexture,
                _ladderTexture,
                _rocketSkateboardTexture,
                _scientistTexture
            };*/

            Room level = LevelLoader.Load("Level01");

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
                _allGameObjects.AddRange(floor.Ladders);
                _allGameObjects.AddRange(floor.Stairs);
                _allGameObjects.AddRange(floor.Inventions);
                _inventions.AddRange(floor.Inventions);
            }
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

            // Update global Time class.
            Time.Update((float)gameTime.ElapsedGameTime.TotalSeconds);

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

                if (!_sleepy.Winner && !_sleepy.Loser) _sleepy.Update();
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
 
            // Draw the level.
            _sleepy.Room.Draw(spriteBatch);

            foreach (Invention invention in _inventions) { invention.Draw(spriteBatch); }

            // Draw the scientist.
            _sleepy.Draw(spriteBatch);

            // Draw the messages.
            MessageLayer.Draw(spriteBatch);

            spriteBatch.End();

            base.Draw(gameTime);
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