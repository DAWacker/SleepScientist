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
    class Scientist : AI
    {
        #region Attributes

        // Possible states the scientist could be in
        public enum ScientistState
        {
            Walking,
            JackInTheBox,
            RocketSkates,
            EggBeater,
            LincolnLogs,
            Ladder,
            Stairs,
            NULL
        }

        // Current and previous states of the scientist
        private ScientistState _curState;
        private ScientistState _prevState;

        private List<Ladder> _ladders;
        private List<Invention> _inventions;
        private GameObject _currentTile;

        #endregion

        #region Properties

        public ScientistState CurrentState { get { return _curState; } set { _curState = value; } }
        public ScientistState PreviousState { get { return _prevState; } set { _prevState = value; } }
        public List<Ladder> Ladders { get { return _ladders; } set { _ladders = value; } }
        public List<Invention> Inventions { get { return _inventions; } set { _inventions = value; } }
        public GameObject CurrentTile { get { return _currentTile; } set { _currentTile = value; } }

        #endregion

        #region Constructor

        /// <summary>
        /// Parameterized constructor for the scientist
        /// </summary>
        /// <param name="name">The name</param>
        /// <param name="x">Starting x-coordinate</param>
        /// <param name="y">Starting y-coordinate</param>
        /// <param name="image">The image</param>
        public Scientist(string name, int x, int y, int width, int height)
            : base(name, x, y, width, height)
        {
            _curState = ScientistState.Walking;
            _prevState = ScientistState.Walking;
            _currentTile = null;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Draw the scientist
        /// </summary>
        /// <param name="batch">The sprite batch you want to draw on</param>
        public override void Draw(SpriteBatch batch) { base.Draw(batch); }

        /// <summary>
        /// Update the scientist
        /// </summary>
        public override void Update() 
        {
            // Check if the scientist is using an invention.
            bool inventionCollision = false;
            foreach (Invention invention in this._inventions)
            {
                if (this.RectPosition.Intersects(invention.RectPosition))
                {
                    this.PrevY = this.Y;
                    MessageLayer.AddMessage(new Message("Collided with " + invention.GetType().Name, X, Y, GameConstants.MESSAGE_TIME));
                    InteractWith(invention);
                    this.CurrentTile = invention;
                    inventionCollision = true;
                    break;
                }
                else
                {
                    invention.UnUse();
                }
            }
            if (!inventionCollision)
            {
                this.CurrentState = ScientistState.Walking;
            }

            // Check if the scientist is climbing a ladder
            foreach (Ladder piece in this.Ladders)
            {
                if (this.RectPosition.Intersects(piece.RectPosition))
                {
                    this.PrevY = piece.Y - this.Height;
                    MessageLayer.AddMessage(new Message("Collided with Ladder", X, Y, GameConstants.MESSAGE_TIME));
                    this.CurrentState = ScientistState.Ladder;
                    this.CurrentTile = piece;
                    break;
                }
                else 
                {
                    if (this.CurrentState == ScientistState.Ladder)
                    {
                        this.CurrentState = ScientistState.Walking;
                        this.CurrentTile = null;
                    }
                }
            }

            // Update scientist based on current state.
            switch (this.CurrentState)
            {
                case ScientistState.Ladder:
                    this.VeloX = 0;
                    this.VeloY = GameConstants.LADDER_Y_VELOCITY;
                    break;

                case ScientistState.RocketSkates:
                    // Keep the current UPPED velocity.
                    break;
                case ScientistState.Walking:
                    this.VeloX = GameConstants.DEFAULT_X_VELOCITY * this.Direction;
                    this.VeloY = 0;
                    this.Y = this.PrevY;
                    break;

                default:
                    break;
            }

            base.Update();
        }

        /// Update the scientist's state based off of the interaction.
        /// </summary>
        /// <param name="gameObject">The GameObject being interacted with.</param>
        public override bool InteractWith(Invention invention )
        {
            ScientistState newState = ScientistState.NULL;
            Type objType = invention.GetType();

            // Check gameObject's type and change state accordingly.
            if (objType == typeof(RocketSkateboard))
            {
                newState = ScientistState.RocketSkates;
            }
            else if (objType == typeof(LincolnLogs))
            {
                newState = ScientistState.LincolnLogs;
            }

            // Update scientist's state only if state has changed.
            if (newState != ScientistState.NULL)
            {
                invention.Use(this);
                this.CurrentState = newState;
            }

            return newState != ScientistState.NULL;
        }


        #endregion
    }
}