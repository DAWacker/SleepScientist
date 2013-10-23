﻿#region Using Statements
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

        // GameObjects
        private Scientist _sleepy;
        private List<Floor> _floors;
        private List<Ladder> _ladders;
        private List<Stairs> _stairs;
        private List<Invention> _inventions;

        // Textures
        private Texture2D _scientistTexture;
        private Texture2D _stairsTexture;
        private Texture2D _ladderTexture;
        private Texture2D _floorTexture;
        private Texture2D _rocketSkateboardTexture;
        private Texture2D _eggBeaterTexture;
        private Texture2D _jackintheboxTexture;

        // Animations
        private Animation _testAnimation;
        private Animation _testAnimation2;

        // Debug Messages

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

            _sleepy = new Scientist("Sleepy", 0, 0, 50, 50);
            // Initialize test "Level" objects.
            _floors = new List<Floor>();
            _ladders = new List<Ladder>();
            _stairs = new List<Stairs>();
            _inventions = new List<Invention>();

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

            // Load in the scientist placeholder
            _scientistTexture = this.Content.Load<Texture2D>("Image/scientist");

            // Load content of other GameObjects.
            _floorTexture = this.Content.Load<Texture2D>("Image/floor");
            _stairsTexture = this.Content.Load<Texture2D>("Image/stairs");
            _ladderTexture = this.Content.Load<Texture2D>("Image/ladder");
            _rocketSkateboardTexture = this.Content.Load<Texture2D>("Image/rocketSkateboard");
            _eggBeaterTexture = this.Content.Load<Texture2D>("Image/eggBeater");
            _jackintheboxTexture = this.Content.Load<Texture2D>("Image/jackInTheBox");

            // Set the scientist image to the AI
            _sleepy.Image = _scientistTexture;
            
            // Add some test messages.
            MessageLayer.AddMessage(new Message("Test", 0, 0));
            MessageLayer.AddMessage(new Message("Test 5 Seconds", 0, 30, 5));

            // Set up the test "Level".
            // SetupLevel(4, true);
            SetupLevel(4);

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

            
            Invention box = new JackInTheBox("JackInTheBox", screenWidth / 2, _floors[0].Y - _sleepy.Height, 50, 50);
            box.Image = _jackintheboxTexture;
            _inventions.Add(box);
            

            // Set up the Scientist.
            _sleepy.X = 100;
            _sleepy.Y = _floors[0].Y - _sleepy.Height;
            _sleepy.PrevY = _sleepy.Y;
            _sleepy.Ladders = _ladders;
            _sleepy.Stairs = _stairs;
            _sleepy.Floors = _floors;
            _sleepy.Inventions = _inventions;

            // Set up the test animation.
            AnimationLoader.Load("test.xml", Content);

            _testAnimation = AnimationLoader.Sets["Test"].Animations["Test1"];
            _testAnimation2 = AnimationLoader.Sets["Test"].Animations["Test2"];
            _sleepy.Animations = new AnimationSet(AnimationLoader.Sets["Test"]);
            _sleepy.Animations.ChangeAnimation("Test1");
            /*_testAnimation = new Animation("Test");
            _testAnimation.TimePerFrame = .25F;    // Second/Frame
            _testAnimation.Images = new List<Texture2D>() {
                _floorTexture,
                _ladderTexture,
                _rocketSkateboardTexture,
                _scientistTexture
            };*/
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
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // Update global Time class.
            Time.Update((float)gameTime.ElapsedGameTime.TotalSeconds);

            // Update test animations.
            _testAnimation.Update();
            _testAnimation2.Update();

            foreach (Invention invention in _inventions) { invention.Update(); }
            _sleepy.Update();
			MessageLayer.Update(gameTime.ElapsedGameTime.TotalSeconds);

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

            // Draw the test animations.
            spriteBatch.Draw(_testAnimation.CurrentImage(), new Vector2( screenWidth / 2, screenHeight / 2 ), Color.White);
            spriteBatch.Draw(_testAnimation2.CurrentImage(), new Vector2(screenWidth / 2 + 100, screenHeight / 2), Color.White);

            // Draw the level.
            foreach (Floor tile in _floors) { tile.Draw(spriteBatch); }
            foreach (Ladder piece in _ladders) { piece.Draw(spriteBatch); }
            foreach (Stairs piece in _stairs) { piece.Draw(spriteBatch); }
            foreach (Invention invention in _inventions) { invention.Draw(spriteBatch); }

            // Draw the scientist.
            _sleepy.Draw(spriteBatch);

            // Draw the messages.
            MessageLayer.Draw(spriteBatch);

            spriteBatch.End();

            base.Draw(gameTime);
        }

        /// <summary>
        /// Creates and positions Floors and Ladders for testing.
        /// </summary>
        /// <param name="numFloors">The number of floors to create for the test environment.</param>
        /// <param name="createLadders">Should Ladders be added to the test environment?</param>
        private void SetupLevel(int numFloors, bool createLadders = false, bool createStairs = false)
        {
            Random rand = new Random();
            int x = 0;
            int y;
            int width = screenWidth;
            int distanceBetweenFloors = screenHeight / numFloors;

            // Add Floors.
            for (int i = 0; i < numFloors; i++)
            {
                y = screenHeight - distanceBetweenFloors * i - GameConstants.FLOOR_HEIGHT;
                Floor toAdd = new Floor(x, y, width, GameConstants.FLOOR_HEIGHT);
                toAdd.Image = _floorTexture;
                _floors.Add(toAdd);
            }

            // Add Ladders.
            if (createLadders)
            {
                for (int i = 1; i < numFloors; i++)
                {
                    x = rand.Next(screenWidth);
                    y = screenHeight - distanceBetweenFloors * i - GameConstants.FLOOR_HEIGHT;
                    Ladder toAdd = new Ladder(x, y, GameConstants.LADDER_WIDTH, distanceBetweenFloors);
                    toAdd.Image = _ladderTexture;
                    _ladders.Add(toAdd);
                }
            }

            // Add Stairs.
            if (createStairs)
            {
                for (int i = 1; i < numFloors; i++)
                {
                    x = rand.Next(screenWidth);
                    y = screenHeight - distanceBetweenFloors * i - GameConstants.FLOOR_HEIGHT;
                    Stairs toAdd = new Stairs(x, y, GameConstants.LADDER_WIDTH, distanceBetweenFloors);
                    toAdd.Image = _stairsTexture;
                    _stairs.Add(toAdd);
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