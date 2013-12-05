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

        // Has the player won
        private bool _winner;

        // Has the player lost
        private bool _loser;

        // Is the scientist on a skateboard
        private RocketSkateboard _skateboard;

        #endregion

        #region States

        // Possible states the scientist could be in
        public enum ScientistState
        {
            Walking,
            JackInTheBox,
            RocketSkates,
            EggBeater,
            Batteries,
            Teleporter,
            Stairs,
            Bed,
            Pit,
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

        // Get or set if the player has won the level
        public bool Winner { get { return _winner; } set { _winner = value; } }

        // Get or set if the player has lost the level
        public bool Loser { get { return _loser; } set { _loser = value; } }

        // Get or set the scientist's skateboard
        public RocketSkateboard Skateboard { get { return _skateboard; } set { _skateboard = value; } }

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
            this.Room.Scientist = this;
            this.Direction = this.Room.StartDirection;
            this.CurrentState = ScientistState.Walking;
            this.PreviousState = ScientistState.Walking;
            this.CurrentFloor = room.Floors[room.StartFloor - 1];
            this.CurrentTile = room.Floors[room.StartFloor - 1];
            this.FloorNumber = room.StartFloor - 1;
            this.PrevY = this.Y;
            this.Winner = false;
            this.Loser = false;
            this.Skateboard = null;

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
            // Check if the scientist is on the ground or just landing
            if (this.RectPosition.Bottom == this.CurrentFloor.RectPosition.Top ||
                (this.RectPosition.Intersects(this.CurrentFloor.RectPosition) &&
                    this.CurrentState == ScientistState.JackInTheBox && this.VeloY > 0))
            {
                // If the scientist is in the floor, slowly move him up to the top
                while (this.RectPosition.Bottom > this.CurrentFloor.RectPosition.Top) { this.Y--; }
                this.CurrentTile = this.CurrentFloor;
                this.CurrentState = ScientistState.Walking;
            }

            // Check if the scientist is using an invention.
            if (this.CurrentState != ScientistState.Teleporter && this.CurrentState != ScientistState.Stairs)
            {
                foreach (Invention invention in this.CurrentFloor.Inventions)
                {
                    // Check if the scientist is colliding with an invention
                    if (this.RectPosition.Intersects(invention.RectPosition)
                        && !invention.Activated && !invention.HasTarget )
                    {
                        // Check if the scientist isn't on a skateboard yet
                        if (invention.GetType() == typeof(RocketSkateboard) && this.Skateboard == null)
                        {
                            this.X = invention.X + 25;
                            this.Y = invention.Y - (this.Height / 2) - 5;
                            this.Skateboard = (RocketSkateboard)invention;
                            InteractWith(invention);
                        }

                        // Check if the invention is a jack in the box
                        if (invention.GetType() == typeof(JackInTheBox))
                        {
                            // Make sure you are not on the top floor
                            if (this.FloorNumber != Room.NumberFloors - 1)
                            {
                                // Loop through all of the stairs on the floor above you
                                for (int i = 0; i < this.Room.Floors[this.FloorNumber + 1].Stairs.Count; i++)
                                {
                                    Stairs stair = this.Room.Floors[this.FloorNumber + 1].Stairs[i];
                                    // Check which way the staircase is facing
                                    switch (stair.Direction)
                                    {
                                        case 1:
                                            // Check if the jack in the box is placed at the bottom of the stairs
                                            if (((stair.RectPosition.Intersects(invention.RectPosition) && invention.X > stair.X + stair.Width - 25) ||
                                                invention.X > stair.X + stair.Width && invention.X < stair.X + stair.Width + 25) && this.Direction == -stair.Direction)
                                            {
                                                this.Jump(true);
                                                this.CurrentFloor = this.Room.Floors[this.FloorNumber + 1];
                                                this.CurrentTile = this.Room.Floors[this.FloorNumber + 1];
                                                i = 10;
                                            }

                                            // If the jack in the box isn't at the bottom of the stairs, do a normal jump
                                            else { this.InteractWith(invention); }
                                            if (this.Skateboard != null) { this.Skateboard.VeloX = 0; }
                                            this.Skateboard = null;
                                            break;

                                        case -1:
                                            // Check if the jack in the box is placed at the bottom of the stairs
                                            if (((stair.RectPosition.Intersects(invention.RectPosition) && invention.X < stair.X + 25) ||
                                                invention.X > stair.X - invention.Width - 25 && invention.X < stair.X - invention.Width) && this.Direction == -stair.Direction)
                                            {
                                                this.Jump(true);
                                                this.CurrentFloor = this.Room.Floors[this.FloorNumber + 1];
                                                this.CurrentTile = this.Room.Floors[this.FloorNumber + 1];
                                                i = 10;
                                            }

                                            // If the jack in the box isn't at the bottom of the stairs, do a normal jump
                                            else { this.InteractWith(invention); }
                                            if (this.Skateboard != null) { this.Skateboard.VeloX = 0; }
                                            this.Skateboard = null;
                                            break;
                                    }

                                    if (i == 10) { this.FloorNumber++; break; }
                                }
                                // If the floor above has no stairs, interact with the invention normally
                                if (this.CurrentState != ScientistState.JackInTheBox) { if (this.Room.Floors[this.FloorNumber + 1].Stairs.Count == 0) { this.InteractWith(invention); } }
                            }
                            // If the jack in the box isn't at the bottom of the stairs, do a normal jump
                            else { this.InteractWith(invention); }
                        }

                        // Interact normally with other inventions
                        else { this.InteractWith(invention); }
                        break;
                    }
                }
            }

            Rectangle positionCheck;
            if (this.Skateboard != null) { positionCheck = this.Skateboard.RectPosition; }
            else { positionCheck = this.RectPosition; }

            // Check if the scientist is colliding with a Teleporter
            foreach (Teleporter piece in this.CurrentFloor.Teleporters)
            {
                // Check if the scientist is moving left or right
                switch (this.Direction)
                {
                    // Moving right
                    case 1:
                        if (positionCheck.Bottom == piece.RectPosition.Bottom &&
                            this.RectPosition.X > piece.RectPosition.X - GameConstants.BUFFER &&
                            this.RectPosition.X < piece.RectPosition.X + piece.RectPosition.Width)
                        {
                            this.CurrentTile = piece;
                            this.CurrentState = ScientistState.Teleporter;
                            if (this.Skateboard != null) { this.Skateboard.VeloX = 0; }
                            this.Skateboard = null;
                        }
                        break;

                    // Moving left
                    case -1:
                        if (positionCheck.Bottom == piece.RectPosition.Bottom &&
                            this.RectPosition.X < piece.RectPosition.X &&
                            this.RectPosition.X > piece.RectPosition.X - piece.RectPosition.Width)
                        {
                            this.CurrentTile = piece;
                            this.CurrentState = ScientistState.Teleporter;
                            if (this.Skateboard != null) { this.Skateboard.VeloX = 0; }
                            this.Skateboard = null;
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
                        if (positionCheck.Intersects(stair.RectPosition) &&
                            this.RectPosition.X < stair.RectPosition.X + this.Width + 25 &&
                            this.RectPosition.X > stair.RectPosition.X &&
                            this.RectPosition.Top == stair.RectPosition.Top &&
                            this.Direction == stair.Direction)
                        {
                            this.CurrentTile = stair;
                            this.CurrentState = ScientistState.Stairs;
                            if (this.Skateboard != null) { this.Skateboard.VeloX = 0; }
                            this.Skateboard = null;
                        }
                        break;

                    // Moving left
                    case -1:
                        if (positionCheck.Intersects(stair.RectPosition) &&
                            this.RectPosition.X < stair.RectPosition.X + stair.Width - this.Width &&
                            this.RectPosition.X > stair.RectPosition.X + stair.Width - this.Width - 25 &&
                            this.RectPosition.Top == stair.RectPosition.Top &&
                            this.Direction == stair.Direction)
                        {
                            this.CurrentTile = stair;
                            this.CurrentState = ScientistState.Stairs;
                            if (this.Skateboard != null) { this.Skateboard.VeloX = 0; }
                            this.Skateboard = null;
                        }
                        break;

                    // Something is horribly wrong
                    default:
                        break;
                }
            }

            // Check if the scientist has fallen into a pit
            foreach (Pit pit in this.CurrentFloor.Pits)
            {
                // Check if the scientist is touching the pit
                if (positionCheck.Bottom == pit.RectPosition.Top &&
                    this.X + this.Width > pit.X && this.X < pit.X + pit.Width)
                {
                    this.CurrentTile = pit;
                    this.CurrentState = ScientistState.Pit;
                    if (this.Skateboard != null) { this.Skateboard.VeloX = 0; }
                    this.Skateboard = null;
                }
            }

            // Check if the scientist has reached the bed
            if (positionCheck.Intersects(this.Room.Bed.RectPosition)) { this.CurrentState = ScientistState.Bed; }

            // Check if the scientist hit a wall
            if (this.CurrentState != ScientistState.JackInTheBox) { foreach (Wall wall in this.CurrentFloor.Walls) { if (this.RectPosition.Intersects(wall.RectPosition)) { this.Reverse(); } } }

            // Update scientist based on current state.
            switch (this.CurrentState)
            {
                case ScientistState.Teleporter:
                    this.VeloX = 0;
                    this.VeloY = GameConstants.TELEPORTER_Y_VELOCITY;

                    // Check if the scientist has reached the top of the teleporter
                    if (this.RectPosition.Top <= this.CurrentTile.RectPosition.Top)
                    {
                        this.VeloY = 0;
                        this.Y = this.CurrentTile.Y;
                        this.CurrentState = ScientistState.Walking;
                        this.CurrentFloor = this.Room.Floors[this.FloorNumber + 1];
                        this.CurrentTile = this.Room.Floors[this.FloorNumber + 1];
                        this.FloorNumber++;
                    }
                    base.Update();
                    break;

                case ScientistState.Stairs:
                    this.VeloX = GameConstants.STAIR_X_VELOCITY * this.Direction;
                    this.VeloY = GameConstants.STAIR_Y_VELOCITY;

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
                    base.Update();
                    break;

                case ScientistState.RocketSkates:
                    this.Skateboard.Update();
                    this.Direction = this.Skateboard.Direction;
                    this.X = this.Skateboard.X + 25;
                    this.Y = this.Skateboard.Y - this.Height + 10;
                    break;

                case ScientistState.EggBeater:
                    base.Update();
                    break;

                case ScientistState.JackInTheBox:
                    if (GameConstants.MOVING_INVENTION)
                    {
                        this.VeloY += GameConstants.GRAVITY * Time.DeltaTime / GameConstants.SLOW_MOTION;
                    }
                    else
                    {
                        this.VeloY += GameConstants.GRAVITY * Time.DeltaTime;
                    }
                    base.Update();
                    break;

                case ScientistState.Bed:
                    this.Winner = true;
                    break;

                case ScientistState.Pit:
                    this.Loser = true;
                    break;

                case ScientistState.Walking:
                    this.RefreshInventions();
                    this.VeloX = GameConstants.DEFAULT_X_VELOCITY * this.Direction;
                    this.VeloY = 0;
                    base.Update();
                    break;

                default:
                    break;
            }
        }

        /// <summary>
        /// Makes the scientist perform a jump
        /// </summary>
        /// <param name="upstairs">Is the jump going to send the scientist upstairs</param>
        public void Jump(bool upstairs=false) 
        {
            // Check if the jump is going to send the scientist upstairs
            if (upstairs)
            {
                this.VeloX = GameConstants.JUMP_UPSTAIRS_VELOCITY_X * this.Direction;
                this.VeloY = GameConstants.JUMP_UPSTAIRS_VELOCITY_Y;
            }
            // If not, do a normal jump
            else
            {
                this.VeloY = GameConstants.DEFAULT_JUMP_VELOCITY_Y;
                this.VeloX = GameConstants.DEFAULT_JUMP_VELOCITY_X * this.Direction;
            }
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
            else if (objType == typeof(Batteries)) { newState = ScientistState.Batteries; }
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

        /// <summary>
        /// Refreshes inventions that were previously used so they can be used again
        /// </summary>
        public void RefreshInventions()
        {
            if (this.PreviousInvention != null) this.PreviousInvention.UnUse();
            this.PreviousInvention = null;
        }

        #endregion
    }
}