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

        // Current and previous states of the scientist
        private ScientistState _curState;
        private ScientistState _prevState;

        // The room (level) the scientist is in
        private Room _room;

        // The current floor number the scientist is on
        private int _curFloorNum;

        // The current floor the scientist is on
        private Floor _curFloor;

        // The current tile the scientist is on
        private GameObject _currentTile;

        // The previous invention the scientist used
        private Invention _prevInvention;

        private int _initialX;
        private int _initialY;

        #endregion

        #region States

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

        #endregion

        #region Properties

        // Get or set the current state of the scientist
        public ScientistState CurrentState { get { return _curState; } set { _curState = value; } }

        // Get or set the previous state of the scientst
        public ScientistState PreviousState { get { return _prevState; } set { _prevState = value; } }
        
        // Get or set the room the scientist is in
        public Room Room { get { return _room; } set { _room = value; } }

        // Get or set the floor number the scientist is on
        public int FloorNumber { get { return _curFloorNum; } set { _curFloorNum = value; } }

        // Get or set the current floor the scientist is on
        public Floor CurrentFloor { get { return _curFloor; } set { _curFloor = value; } }
        
        // Get or set the current tile the scientist is on
        public GameObject CurrentTile { get { return _currentTile; } set { _currentTile = value; } }
        
        // Get or set the previous invention the scientist used
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
        public Scientist(string name, int x, int y, int width, int height, Room room)
            : base(name, x, y, width, height)
        {
            this.Room = room;
            this.CurrentState = ScientistState.Walking;
            this.PreviousState = ScientistState.Walking;
            this.CurrentFloor = room.Floors[room.StartFloor - 1];
            this.CurrentTile = room.Floors[room.StartFloor - 1];
            this.FloorNumber = room.StartFloor - 1;
            this.PrevY = this.Y;

            _initialX = x;
            _initialY = y;

            // Get a copy of the Scientist Animation
            Animations = AnimationLoader.GetSetCopy("Scientist");
            Animations.ChangeAnimation("Walk");
        }

        #endregion

        #region Methods
        
        /// <summary>
        /// Update the scientist
        /// </summary>
        public override void Update() 
        {

            /*
            try { MessageLayer.AddMessage(new Message(this.FloorNumber + " : " + this.CurrentFloor.Ladders[0].X, this.X - 10, this.Y - 30, GameConstants.MESSAGE_TIME)); }
            catch { MessageLayer.AddMessage(new Message((this.CurrentFloor.Ladders == null).ToString(), this.X - 10, this.Y - 30, GameConstants.MESSAGE_TIME)); }
            */

            // Check if the scientist is on the ground or just landing
            if (this.RectPosition.Bottom == this.CurrentFloor.RectPosition.Top ||
                (this.RectPosition.Intersects(this.CurrentFloor.RectPosition) &&
                    this.CurrentState == ScientistState.JackInTheBox))
            {
                // If the scientist is in the floor, slowly move him up to the top
                while (this.RectPosition.Bottom > this.CurrentFloor.RectPosition.Top) { this.Y--; }
                this.CurrentTile = this.CurrentFloor;
                this.CurrentState = ScientistState.Walking;
            }

            // Check if the scientist hit a wall
            foreach (Wall wall in this.CurrentFloor.Walls) { if (this.RectPosition.Intersects(wall.RectPosition)) { this.Reverse(); } }

            // Check if the scientist is using an invention.
            foreach (Invention invention in this.CurrentFloor.Inventions)
            {
                // Check if the scientist is colliding with an invention
                if (this.RectPosition.Intersects(invention.RectPosition) 
                    && !invention.Activated && !invention.HasTarget && !invention.Clicked)
                {
                    InteractWith(invention);
                    break;
                }
            }

            // Check if the scientist is colliding with a ladder
            foreach (Ladder piece in this.CurrentFloor.Ladders)
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
            foreach (Stairs stair in this.CurrentFloor.Stairs)
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
                    if (this.RectPosition.Top <= this.CurrentTile.RectPosition.Top)
                    {
                        this.VeloY = 0;
                        this.Y = this.CurrentTile.Y;
                        this.CurrentState = ScientistState.Walking;
                        this.CurrentFloor = this.Room.Floors[this.FloorNumber + 1];
                        this.CurrentTile = this.Room.Floors[this.FloorNumber + 1];
                        this.FloorNumber++;
                    }
                    break;

                case ScientistState.Stairs:
                    this.VeloX = 0;
                    this.VeloY = -GameConstants.LADDER_Y_VELOCITY;

                    // Check if the scientist has reached the bottom of the stairs
                    if (this.RectPosition.Bottom >= this.CurrentTile.RectPosition.Bottom)
                    {
                        this.VeloY = 0;
                        this.CurrentState = ScientistState.Walking;
                        this.CurrentFloor = this.Room.Floors[this.FloorNumber - 1];
                        this.CurrentTile = this.Room.Floors[this.FloorNumber - 1];
                        this.Y = this.CurrentTile.Y - GameConstants.TILE_HEIGHT;
                        this.FloorNumber--;
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