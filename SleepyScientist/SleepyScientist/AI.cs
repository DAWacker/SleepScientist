#region using statements
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
#endregion

namespace SleepyScientist
{
    class AI : GameObject
    {
        #region Attributes

        // Movement
        private float _veloX;
        private float _veloY;
        private float _prevVeloX;
        private float _prevY;

        // General
        private string _name;
        private AI _target;

        #endregion

        #region Properties

        // Get or set the AI's movement
        public float VeloX { get { return _veloX; } set { _veloX = value; } }
        public float VeloY { get { return _veloY; } set { _veloY = value; } }
        public float PrevVeloX { get { return _prevVeloX; } set { _prevVeloX = value; } }
        public float PrevY { get { return _prevY; } set { _prevY = value; } }

        // Get or set the AI's name
        public string Name { get { return _name; } set { _name = value; } }

        // Get or set target
        public AI Target { get { return _target; } set { _target = value; } }

        #endregion

        #region Constructor

        /// <summary>
        /// Parameterized constructor of an AI
        /// </summary>
        /// <param name="name">The name</param>
        /// <param name="x">Starting x-coordinate</param>
        /// <param name="y">Starting y-coordinate</param>
        /// <param name="image">The image</param>
        public AI(string name, float x, float y, float width, float height)
            : base(x, y, width, height, GameConstants.DEFAULT_DIRECTION)
        {
            // General
            _name = name;

            // Movement
            _veloX = GameConstants.DEFAULT_X_VELOCITY * this.Direction;
            _veloY = 0;
            _prevY = y;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Reverses the direction of the AI
        /// </summary>
        public void Reverse() { this.VeloX = -this.VeloX; this.Direction = -this.Direction;}

        /// <summary>
        /// Move the AI
        /// </summary>
        public void Move()
        {
            // Check if the user is moving an invention
            if (GameConstants.MOVING_INVENTION)
            {
                this.X = (this.X + this.VeloX * Time.DeltaTime / GameConstants.SLOW_MOTION);
                this.Y = (this.Y + this.VeloY * Time.DeltaTime / GameConstants.SLOW_MOTION);
            }
            else
            {
                this.X = (this.X + this.VeloX * Time.DeltaTime);
                this.Y = (this.Y + this.VeloY * Time.DeltaTime);
            }
        }

        /// <summary>
        /// Prevents the AI from going off the left and right sides of the screen
        /// </summary>
        public void StayOnScreen() {
            if (this.X + this.Width > GameConstants.SCREEN_WIDTH || this.X < 0)
            {
                while (this.X + this.Width > GameConstants.SCREEN_WIDTH)
                    this.X--;

                while (this.X < 0)
                    this.X++;

                this.Reverse();
            }
        }

        /// <summary>
        /// Draw the AI
        /// </summary>
        /// <param name="batch">The sprite batch you want to draw on</param>
        public override void Draw(SpriteBatch batch, Rectangle? pos = null) {
            if (Direction == -1)
            {
                if (pos != null)
                    batch.Draw(this.Image, pos.Value, null, Color.White, 0, Vector2.Zero, SpriteEffects.FlipHorizontally, 0);
                else
                    batch.Draw(this.Image, this.RectPosition, null, Color.White, 0, Vector2.Zero, SpriteEffects.FlipHorizontally, 0);
            }
            else
            {
                if (pos != null)
                    base.Draw(batch, pos);
                else
                    base.Draw(batch);
            }
        }

        /// <summary>
        /// Update the AI
        /// </summary>
        public override void Update()
        {
            this.StayOnScreen();
            this.Move();
            base.Update();
        }

        public virtual bool InteractWith(Invention invention)
        {
            // Update the state of the AI.
            return false;
        }

        #endregion
    }
}