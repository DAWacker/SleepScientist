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
        private List<Stairs> _stairs;
        private List<Floor> _floors;
        private List<Invention> _inventions;
        private GameObject _currentTile;
        private Invention _prevInvention;

        #endregion

        #region Properties

        public ScientistState CurrentState { get { return _curState; } set { _curState = value; } }
        public ScientistState PreviousState { get { return _prevState; } set { _prevState = value; } }
        public List<Ladder> Ladders { get { return _ladders; } set { _ladders = value; } }
        public List<Stairs> Stairs { get { return _stairs; } set { _stairs = value; } }
        public List<Floor> Floors { get { return _floors; } set { _floors = value; } }
        public List<Invention> Inventions { get { return _inventions; } set { _inventions = value; } }
        public GameObject CurrentTile { get { return _currentTile; } set { _currentTile = value; } }
        public Invention PreviousInvention { get { return _prevInvention; } set { _prevInvention = value; } }

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

            // Check if the scientist is on the ground
            foreach (Floor floor in this.Floors)
            {
                // Check if the scientist is on the ground or just landing
                if (this.RectPosition.Bottom == floor.RectPosition.Top ||
                    (this.RectPosition.Intersects(floor.RectPosition) &&
                      this.CurrentState == ScientistState.JackInTheBox))
                {
                    // If the scientist is in the floor, slowly move him up to the top
                    while (this.RectPosition.Bottom > floor.RectPosition.Top) { this.Y--; }
                    this.CurrentTile = floor;
                    this.CurrentState = ScientistState.Walking;
                }
            }

            // Check if the scientist is using an invention.
            foreach (Invention invention in this.Inventions)
            {
                if (this.RectPosition.Intersects(invention.RectPosition) 
                    && !invention.Activated && !invention.HasTarget)
                {
                    InteractWith(invention);
                    break;
                }
            }

            // Check if the scientist is colliding with a ladder
            foreach (Ladder piece in this.Ladders)
            {
                // Check if the scientist is moving left or right
                switch (this.Direction)
                {
                    // Moving right
                    case 1:
                        if (this.RectPosition.Bottom == piece.RectPosition.Bottom &&
                            this.RectPosition.X > piece.RectPosition.X - GameConstants.BUFFER &&
                            this.RectPosition.X < piece.RectPosition.X + piece.RectPosition.Width)
                        {
                            this.CurrentTile = piece;
                            this.CurrentState = ScientistState.Ladder;
                        }
                        break;

                    // Moving left
                    case -1:
                        if (this.RectPosition.Bottom == piece.RectPosition.Bottom &&
                            this.RectPosition.X < piece.RectPosition.X &&
                            this.RectPosition.X > piece.RectPosition.X - piece.RectPosition.Width)
                        {
                            this.CurrentTile = piece;
                            this.CurrentState = ScientistState.Ladder;
                        }
                        break;

                    // Something is horribly wrong
                    default:
                        break;
                }
            }

            // Check if the scientist is colliding with stairs
            foreach (Stairs stair in this.Stairs)
            {
                // Check if the scientist is moving left or right
                switch (this.Direction)
                {
                    // Moving right
                    case 1:
                        if (this.RectPosition.Bottom == stair.RectPosition.Top &&
                            this.RectPosition.X > stair.RectPosition.X - GameConstants.BUFFER &&
                            this.RectPosition.X < stair.RectPosition.X + stair.RectPosition.Width)
                        {
                            this.CurrentTile = stair;
                            this.CurrentState = ScientistState.Stairs;
                        }
                        break;

                    // Moving left
                    case -1:
                        if (this.RectPosition.Bottom == stair.RectPosition.Top &&
                            this.RectPosition.X < stair.RectPosition.X &&
                            this.RectPosition.X > stair.RectPosition.X - stair.RectPosition.Width)
                        {
                            this.CurrentTile = stair;
                            this.CurrentState = ScientistState.Stairs;
                        }
                        break;

                    // Something is horribly wrong
                    default:
                        break;
                }
            }

            // Update scientist based on current state.
            switch (this.CurrentState)
            {
                case ScientistState.Ladder:
                    this.VeloX = 0;
                    this.VeloY = GameConstants.LADDER_Y_VELOCITY;

                    // Check if the scientist has reached the top of the ladder
                    if (this.RectPosition.Bottom <= this.CurrentTile.RectPosition.Top)
                    {
                        this.VeloY = 0;
                        this.Y = this.CurrentTile.Y - this.Height;
                        this.CurrentState = ScientistState.Walking;
                        this.CurrentTile = null;
                    }
                    break;

                case ScientistState.Stairs:
                    this.VeloX = 0;
                    this.VeloY = -GameConstants.LADDER_Y_VELOCITY;

                    // Check if the scientist has reached the bottom of the stairs
                    if (this.RectPosition.Bottom >= this.CurrentTile.RectPosition.Bottom)
                    {
                        this.CurrentState = ScientistState.Walking;
                        this.CurrentTile = null;
                    }
                    break;

                case ScientistState.RocketSkates:
                    break;

                case ScientistState.EggBeater:
                    break;

                case ScientistState.JackInTheBox:
                    this.VeloY++;
                    break;

                case ScientistState.Walking:
                    this.RefreshInventions();
                    this.VeloX = GameConstants.DEFAULT_X_VELOCITY * this.Direction;
                    this.VeloY = 0;
                    break;

                default:
                    break;
            }
            base.Update();
        }

        public void Jump() 
        { 
            this.VeloY = GameConstants.DEFAULT_JUMP_VELOCITY_Y;
            this.VeloX = GameConstants.DEFAULT_JUMP_VELOCITY_X * this.Direction;
            this.CurrentState = ScientistState.JackInTheBox;
        }

        /// Update the scientist's state based off of the interaction.
        /// </summary>
        /// <param name="gameObject">The GameObject being interacted with.</param>
        public override bool InteractWith(Invention invention )
        {
            ScientistState newState = ScientistState.NULL;
            Type objType = invention.GetType();

            // Check gameObject's type and change state accordingly.
            if (objType == typeof(RocketSkateboard)) { newState = ScientistState.RocketSkates; }
            else if (objType == typeof(LincolnLogs)) { newState = ScientistState.LincolnLogs; }
            else if (objType == typeof(EggBeater)) { newState = ScientistState.EggBeater; }
            else if (objType == typeof(JackInTheBox)) { newState = ScientistState.JackInTheBox; }

            // Update scientist's state only if state has changed.
            if (newState != ScientistState.NULL)
            {
                invention.Use(this);
                this.PreviousInvention = invention;
                this.CurrentState = newState;
            }

            return newState != ScientistState.NULL;
        }

        public void RefreshInventions()
        {
            if (this.PreviousInvention != null) this.PreviousInvention.UnUse();
            this.PreviousInvention = null;
        }

        #endregion
    }
}