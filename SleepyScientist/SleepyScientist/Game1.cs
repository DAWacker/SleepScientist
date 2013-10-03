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
		MessageLayer _messageLayer;

        // Screen dimensions
        private int screenWidth;
        private int screenHeight;

        // GameObjects
        private Scientist _sleepy;
        private List<Floor> _floors;

        // Textures
        private Texture2D scientist;
        private Texture2D _stairsTexture;
        private Texture2D _ladderTexture;
        private Texture2D _floorTexture;

        private List<Floor> floors;
        private List<Ladder> ladders;

        #endregion

        public Game1()
            : base()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
			_messageLayer = new MessageLayer();
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {

            // Turns off full screen and sets the background to 1080x640
            graphics.IsFullScreen = false;
            graphics.PreferredBackBufferWidth = 1280;
            graphics.PreferredBackBufferHeight = 720;
            graphics.ApplyChanges();

            // Save the new screen dimensions locally
            screenWidth = 1280;
            screenHeight = 720;

            // Save the new screen dimensions for other classes
            GameConstants.SCREEN_WIDTH = screenWidth;
            GameConstants.SCREEN_HEIGHT = screenHeight;

            _sleepy = new Scientist("Sleepy", 100, 320, 50, 50);
            floors = new List<Floor>();
            ladders = new List<Ladder>();

            _floors = new List<Floor>();

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

            // Load in the scientist placeholder
            scientist = this.Content.Load<Texture2D>("Image/scientist");

            // Load content of other GameObjects.
            _floorTexture = this.Content.Load<Texture2D>("Image/floor");
            _stairsTexture = this.Content.Load<Texture2D>("Image/stairs");
            _ladderTexture = this.Content.Load<Texture2D>("Image/ladder");

            // Set the scientist image to the AI
            _sleepy.Image = scientist;

            Floor _floor1;
            Floor _floor2;

            for (int i = 0; i < 22; i++)
            {
                _floor1 = new Floor(i * 50, 370, 50, 50);
                _floor1.Image = _floorTexture;
                floors.Add(_floor1);

                _floor2 = new Floor(i * 50, 170, 50, 50);
                _floor2.Image = _floorTexture;
                floors.Add(_floor2);
            }

            Ladder ladderPiece;

            for (int l = 0; l < 4; l++)
            {
                ladderPiece = new Ladder(400, 320 - (l * 50), 50, 50);
                ladderPiece.Image = _ladderTexture;
                ladders.Add(ladderPiece);
            }

            _sleepy.Ladders = ladders;
            _spriteFont = Content.Load<SpriteFont>("Font/defaultFont");

            // Add some test messages.
            _messageLayer.AddMessage(new Message("Test", 0, 0));
            _messageLayer.AddMessage(new Message("Test 5 Seconds", 0, 30, 5));

            // Initialize test "Level" objects.
            //_floors = new List<Floor>();
            //_ladders = new List<Ladder>();
            //_stairs = new List<Stairs>();

            // Set up the test "Level".
            //Floor floor1 = new Floor(0, screenHeight - 64, screenWidth, 64);
            //Floor floor2 = new Floor(0, screenHeight / 2 - 64, screenWidth, 64);
            //floor1.Image = _floorTexture;
            //floor2.Image = _floorTexture;
            //_floors.Add(floor1);
            //_floors.Add(floor2);
            //SetupLevel(4);

            // Set up the Scientist.
            //_sleepy = new Scientist("Sleepy", 100, _floors[0].Y - 50, 50, 50);
            // Set the scientist image to the AI
            //_sleepy.Image = scientist;
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

            _sleepy.Update();
			_messageLayer.Update(gameTime.ElapsedGameTime.TotalSeconds);

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

            foreach (Message message in _messageLayer.Messages)
            {
                spriteBatch.DrawString(_spriteFont, message.Text, new Vector2(message.X, message.Y), Color.White);
            }

            foreach (Floor tile in floors) { tile.Draw(spriteBatch); }
            foreach (Ladder piece in ladders) { piece.Draw(spriteBatch); }

            // Draw the scientist
            _sleepy.Draw(spriteBatch);

            spriteBatch.End();

            base.Draw(gameTime);
        }

        /// <summary>
        /// Creates and positions Floors and Ladders for testing.
        /// </summary>
        /// <param name="numFloors">The number of floors to create for the test environment.</param>
        /// <param name="createLadders">Should Ladders be added to the test environment?</param>
        private void SetupLevel(int numFloors, bool createLadders = false)
        {
            Random rand = new Random();
            int x = 0;
            int y;
            int width = screenWidth;
            int distanceBetweenFloors = screenHeight / numFloors;
            Floor toAdd;

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
                for (int i = 0; i < numFloors; i++)
                {
                    x = rand.Next(screenWidth);
                    y = screenHeight - distanceBetweenFloors * i - GameConstants.FLOOR_HEIGHT;
                    Ladder toAdd = new Ladder(x, y, GameConstants.LADDER_WIDTH, distanceBetweenFloors);
                    toAdd.Image = _ladderTexture;
                    _ladders.Add(toAdd);
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
                    hasCollided = ai.InteractWith(ladder);                    
                }
            }

            return hasCollided;
        }
    }
}