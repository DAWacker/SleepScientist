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

namespace SleepyScientist {
	/// <summary>
	/// This is the main type for your game
	/// </summary>
	public class Game1 : Game {
		GraphicsDeviceManager graphics;
		SpriteBatch spriteBatch;
        SpriteFont _spriteFont;
        MessageLayer _messageLayer;

		public Game1()
			: base() {
			graphics = new GraphicsDeviceManager( this );
			Content.RootDirectory = "Content";
            _messageLayer = new MessageLayer();
		}

		/// <summary>
		/// Allows the game to perform any initialization it needs to before starting to run.
		/// This is where it can query for any required services and load any non-graphic
		/// related content.  Calling base.Initialize will enumerate through any components
		/// and initialize them as well.
		/// </summary>
		protected override void Initialize() {
			// TODO: Add your initialization logic here

			base.Initialize();
		}

		/// <summary>
		/// LoadContent will be called once per game and is the place to load
		/// all of your content.
		/// </summary>
		protected override void LoadContent() {
			// Create a new SpriteBatch, which can be used to draw textures.
			spriteBatch = new SpriteBatch( GraphicsDevice );

			// TODO: use this.Content to load your game content here
            _spriteFont = Content.Load<SpriteFont>("defaultFont");
            // Add some test messages.
            _messageLayer.AddMessage(new Message("Test", 0, 0));
            _messageLayer.AddMessage(new Message("Test 5 Seconds", 0, 30, 5));
		}

		/// <summary>
		/// UnloadContent will be called once per game and is the place to unload
		/// all content.
		/// </summary>
		protected override void UnloadContent() {
			// TODO: Unload any non ContentManager content here
		}

		/// <summary>
		/// Allows the game to run logic such as updating the world,
		/// checking for collisions, gathering input, and playing audio.
		/// </summary>
		/// <param name="gameTime">Provides a snapshot of timing values.</param>
		protected override void Update( GameTime gameTime ) {
			if ( GamePad.GetState( PlayerIndex.One ).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown( Keys.Escape ) )
				Exit();

			// TODO: Add your update logic here
            _messageLayer.Update(gameTime.ElapsedGameTime.TotalSeconds);

			base.Update( gameTime );
		}

		/// <summary>
		/// This is called when the game should draw itself.
		/// </summary>
		/// <param name="gameTime">Provides a snapshot of timing values.</param>
		protected override void Draw( GameTime gameTime ) {
			GraphicsDevice.Clear( Color.CornflowerBlue );

			// TODO: Add your drawing code here
            DrawMessageLayer();

			base.Draw( gameTime );
		}

        /**
         * Handles drawing for all the Messages inside _messageLayer.
         */
        public void DrawMessageLayer()
        {
            foreach (Message message in _messageLayer.Messages)
            {
                spriteBatch.Begin();
                spriteBatch.DrawString(_spriteFont, message.Text, new Vector2(message.X, message.Y), Color.White);
                spriteBatch.End();
            }
        }
	}
}
