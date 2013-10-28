// Author: Thomas Bentley

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SleepyScientist
{
    /// <summary>
    /// The base class for an Invention
    /// </summary>
    class Invention : AI
    {
        #region Attributes

        // Is the invention currently activated?
        private bool _active;

        // States for the inventions
        private InventionState _curState;
        private GameObject _curTile;

        // Movement attributes
        private int _laddersHit;
        private int _laddersNeeded;
        private int _targetX;
        private int _targetY;
        private bool _clicked;
        private bool _hasTarget;

        private List<Stairs> _stairs;
        private List<Ladder> _ladders;
        private List<Floor> _floors;

        #endregion

        #region States

        // Possible states for the invention
        public enum InventionState
        {
            Idle,
            Walking,
            JackInTheBox,
            Ladder,
            Stairs
        }

        #endregion

        #region Properties

        // Gets or sets the activation state of the invention.
        public bool Activated { get { return _active; } set { _active = value; } }

        // Get or set the state of the invention
        public InventionState CurrentState { get { return _curState; } set { _curState = value; } }

        // Get or set the current tile of the invention
        public GameObject CurrentTile { get { return _curTile; } set { _curTile = value; } }

        // Get or set if the invention has just been clicked
        public bool Clicked { get { return _clicked; } set { _clicked = value; } }

        // Get or set if the invention has a target location
        public bool HasTarget { get { return _hasTarget; } set { _hasTarget = value; } }

        // Get or set the target coordinates
        public int TargetX { get { return _targetX; } set { _targetX = value; } }
        public int TargetY { get { return _targetY; } set { _targetY = value; } }

        // Get or set ladders needed and hit
        public int LaddersHit { get { return _laddersHit; } set { _laddersHit = value; } }
        public int LaddersNeeded { get { return _laddersNeeded; } set { _laddersNeeded = value; } }

        // Get or set the ladders in the room
        public List<Ladder> Ladders { get { return _ladders; } set { _ladders = value; } }

        //Get or set the stairs in the room
        public List<Stairs> Stairs { get { return _stairs; } set { _stairs = value; } }

        // Get or set the floors in the room
        public List<Floor> Floors { get { return _floors; } set { _floors = value; } }

        #endregion

        #region Constructor

        /// <summary>
        /// Chained Constructor (Base_Invention and Game_Object)
        /// </summary>
        /// <param name="name">Name of this invention</param>
        /// <param name="x">X position</param>
        /// <param name="y">Y position</param>
        /// <param name="width">Width of invention</param>
        /// <param name="height">Height of invention</param>
        public Invention(string name, int x, int y, int width, int height)
            : base(name, x, y, width, height)
        {
            this.CurrentState = InventionState.Idle;
            this.TargetY = 0;
            this.TargetX = 0;
            this.LaddersHit = 0;
            this.LaddersNeeded = 0;
            this.Activated = false;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Operator overload (Invention + Invention) = combo
        /// </summary>
        /// <param name="first">First invention of combo.</param>
        /// <param name="second">Second invention of combo.</param>
        /// <returns></returns>
        public static Invention operator +(Invention first, Invention second) { return new Invention(first.Name + second.Name, first.X, first.Y, first.Width, first.Height); }

        /// <summary>
        /// Method that executes the default functionality of an invention
        /// </summary>
        public virtual void Use( Scientist s )
        {
            if (!this.Activated)
            {
                this.Activated = true;
                this.Target = s;
            }
        }

        /// <summary>
        /// Returns the invention to its default state.
        /// </summary>
        public virtual void UnUse()
        {
            if (this.Activated)
            {
                this.Activated = false;
                this.Target = null;
            }
        }

        public override void Update()
        {
            if (this.HasTarget)
            {
                // Check if the invention is on the ground
                foreach (Floor floor in this.Floors)
                {
                    // Check if the invention is on the ground
                    if (this.RectPosition.Bottom == floor.RectPosition.Top)
                    {
                        this.CurrentTile = floor;
                        this.CurrentState = InventionState.Walking;
                        break;
                    }
                }

                // Check if the invention is colliding with a ladder
                foreach (Ladder piece in this.Ladders)
                {
                    // Check if the invention is moving left or right
                    switch (this.Direction)
                    {
                        // Moving right
                        case 1:
                            if (this.RectPosition.Bottom == piece.RectPosition.Bottom &&
                                this.RectPosition.X > piece.RectPosition.X - GameConstants.BUFFER &&
                                this.RectPosition.X < piece.RectPosition.X + piece.RectPosition.Width &&
                                this.LaddersHit != this.LaddersNeeded)
                            {
                                this.X = piece.X;
                                this.CurrentTile = piece;
                                this.CurrentState = InventionState.Ladder;
                            }
                            break;

                        // Moving left
                        case -1:
                            if (this.RectPosition.Bottom == piece.RectPosition.Bottom &&
                                this.RectPosition.X < piece.RectPosition.X &&
                                this.RectPosition.X > piece.RectPosition.X - piece.RectPosition.Width &&
                                this.LaddersHit != this.LaddersNeeded)
                            {
                                this.X = piece.X;
                                this.CurrentTile = piece;
                                this.CurrentState = InventionState.Ladder;
                            }
                            break;

                        // Something is horribly wrong
                        default:
                            break;
                    }
                }

                // Check if the invention is colliding with stairs
                foreach (Stairs stair in this.Stairs)
                {
                    // Check if the invention is moving left or right
                    switch (this.Direction)
                    {
                        // Moving right
                        case 1:
                            if (this.RectPosition.Bottom == stair.RectPosition.Top &&
                                this.RectPosition.X > stair.RectPosition.X - GameConstants.BUFFER &&
                                this.RectPosition.X < stair.RectPosition.X + stair.RectPosition.Width)
                            {
                                this.CurrentTile = stair;
                                this.CurrentState = InventionState.Stairs;
                            }
                            break;

                        // Moving left
                        case -1:
                            if (this.RectPosition.Bottom == stair.RectPosition.Top &&
                                this.RectPosition.X < stair.RectPosition.X &&
                                this.RectPosition.X > stair.RectPosition.X - stair.RectPosition.Width)
                            {
                                this.CurrentTile = stair;
                                this.CurrentState = InventionState.Stairs;
                            }
                            break;

                        // Something is horribly wrong
                        default:
                            break;
                    }
                }

                // Update invention based on current state.
                switch (this.CurrentState)
                {
                    case InventionState.Ladder:
                        this.VeloX = 0;
                        this.VeloY = GameConstants.INVENTION_LADDER_Y_VELO;

                        // Check if the invention has reached the top of the ladder
                        if (this.RectPosition.Bottom <= this.CurrentTile.RectPosition.Top)
                        {
                            this.LaddersHit++;
                            this.VeloY = 0;
                            this.Y = this.CurrentTile.Y - this.Height;
                            this.CurrentState = InventionState.Walking;
                            this.CurrentTile = null;
                            if (this.X > this.TargetX) { this.Direction = -1; } else { this.Direction = 1; }
                        }

                        break;

                    case InventionState.Stairs:
                        this.VeloX = 0;
                        this.VeloY = -GameConstants.INVENTION_LADDER_Y_VELO;

                        // Check if the invention has reached the bottom of the stairs
                        if (this.RectPosition.Bottom >= this.CurrentTile.RectPosition.Bottom)
                        {
                            this.CurrentState = InventionState.Walking;
                            this.CurrentTile = null;
                        }
                        break;

                    case InventionState.Walking:

                        // Check if the invention is on the floor it needs to be
                        if (this.LaddersHit == this.LaddersNeeded)
                        {
                            switch (this.Direction)
                            {
                                // Moving right
                                case 1:
                                    if (this.X + this.Width >= this.TargetX) { this.ReachedTarget(); }
                                    break;

                                // Moving left
                                case -1:
                                    if (this.X <= this.TargetX) { this.ReachedTarget(); }
                                    break;

                                // Something went horribly wrong
                                default:
                                    break;
                            }
                        }
                        this.VeloX = GameConstants.DEFAULT_INVENTION_X_VELO * this.Direction;
                        this.VeloY = 0;
                        break;

                    default:
                        break;
                }

                // MessageLayer.AddMessage(new Message(this.LaddersHit.ToString(), X - 10, Y - 30, GameConstants.MESSAGE_TIME));
                base.Update();
            }
        }

        public void DeterminePath()
        {
            int verticalChange = this.Y + this.Height - this.TargetY;
            this.LaddersNeeded = verticalChange / GameConstants.DISTANCE_BETWEEN_FLOORS;
            if (this.X > this.TargetX) { this.Direction = -1; } else { this.Direction = 1; }
        }

        public void ReachedTarget()
        {
            this.HasTarget = false;
            this.CurrentState = InventionState.Idle;
            this.VeloX = 0;
            this.VeloY = 0;
            this.TargetX = 0;
            this.TargetY = 0;
            this.LaddersHit = 0;
            this.LaddersNeeded = 0;
        }

        /// <summary>
        /// Combine an invention with this one.
        /// </summary>
        /// <param name="other">Invention to combine with.</param>
        /// <returns></returns>
        public Invention Combine(Invention other)
        {
            return (this + other);
        }

        #endregion
    }
}