using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Storage;
using Microsoft.Xna.Framework.GamerServices;

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
        private int _stairsHit;
        private int _stairsNeeded;
        private int _targetX;
        private int _targetY;
        private bool _clicked;
        private bool _hasTarget;

        private Room _room;
        private Floor _curFloor;
        private int _curFloorNum;

        private List<GameObject> _path;

        private bool _hovered;
        private RectangleVector _selectionBox;

        #endregion

        #region States

        // Possible states for the invention
        public enum InventionState
        {
            Idle,
            Walking,
            JackInTheBox,
            Teleporter,
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

        // Get or set the stairs needed and hit
        public int StairsHit { get { return _stairsHit; } set { _stairsHit = value; } }
        public int StairsNeeded { get { return _stairsNeeded; } set { _stairsNeeded = value; } }

        // Get or set the ladders in the room
        public Room Room { get { return _room; } set { _room = value; } }

        // Get or set the floor number the invention is on
        public int FloorNumber { get { return _curFloorNum; } set { _curFloorNum = value; } }

        // Get or set the current floor the invention is on
        public Floor CurrentFloor { get { return _curFloor; } set { _curFloor = value; } }

        // Get or set the selection box of the invention
        public RectangleVector SelectionBox { get { return _selectionBox; } set { _selectionBox = value; } }

        // Get or set if the invention has the mouse hovering over it
        public bool Hovered { get { return _hovered; } set { _hovered = value; } }

        // Get or set the path of the invention
        public List<GameObject> Path { get { return _path; } set { _path = value; } }

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
        public Invention(string name, float x, float y, float width, float height, Room room, int startFloor)
            : base(name, x, y, width, height)
        {
            this.CurrentState = InventionState.Idle;
            this.TargetY = 0;
            this.TargetX = 0;
            this.LaddersHit = 0;
            this.LaddersNeeded = 0;
            this.StairsHit = 0;
            this.StairsNeeded = 0;
            this.Activated = false;
            this.Hovered = false;
            this.Room = room;
            this.CurrentFloor = room.Floors[startFloor-1];
            this.CurrentTile = room.Floors[startFloor-1];
            this.FloorNumber = startFloor - 1;
            this.Path = new List<GameObject>();

            float selectionWidth = 50 * ((width / 50) + 1);
            float offsetX = (selectionWidth - width) / 2;
            float selectionHeight = 50 * ((height / 50) + 1);
            float offsetY = (selectionHeight - height) / 2;
            this.SelectionBox = new RectangleVector(x - offsetX, y - offsetY, selectionWidth, selectionHeight);
        }

        #endregion

        #region Methods

        /// <summary>
        /// Operator overload (Invention + Invention) = combo
        /// </summary>
        /// <param name="first">First invention of combo.</param>
        /// <param name="second">Second invention of combo.</param>
        /// <returns></returns>
        public static Invention operator +(Invention first, Invention second) { return new Invention(first.Name + second.Name, first.X, first.Y, first.Width, first.Height, first.Room, first.FloorNumber); }

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

        /// <summary>
        /// Update the invention
        /// </summary>
        public override void Update()
        {
            // Check if the invention has a target destination
            if (this.HasTarget)
            {
                // Check if there are still routes in the path
                if (this.Path.Count != 0)
                {
                    // Get the first item on the invention's path
                    GameObject route = this.Path[0];

                    // Check if the item is a staircase or ladder
                    switch (route.GetType().ToString())
                    {
                        // The route is a staircase
                        case "SleepyScientist.Stairs":
                            Stairs stairs = (Stairs)route;
                            // Check the direction of the stairs
                            switch (stairs.Direction)
                            {
                                // Stairs are facing right
                                case 1:
                                    // Check the direction of the invention
                                    switch (this.Direction)
                                    {
                                        // Invention is facing right
                                        case 1:
                                            // Check if the invention needs to go down the stairs
                                            if (this.StairsNeeded > 0)
                                            {
                                                // Check if the invention is hitting those stairs
                                                if (this.RectPosition.Intersects(stairs.RectPosition) &&
                                                    this.X > stairs.X && this.StairsNeeded != this.StairsHit)
                                                {
                                                    this.CurrentTile = stairs;
                                                    this.CurrentState = InventionState.Stairs;
                                                }
                                            }
                                            // Check if the invention needs to go up the stairs and is past them
                                            else if (this.LaddersNeeded > 0 && this.X > stairs.X + stairs.Width) { this.Reverse(); }
                                            break;

                                        // Invention is facing left
                                        case -1:
                                            // Check if the invention needs to go up the stairs
                                            if (this.LaddersNeeded > 0)
                                            {
                                                // Check if the invention is hitting those stairs
                                                if (this.RectPosition.Intersects(stairs.RectPosition) &&
                                                    this.X < stairs.X + stairs.Width - this.Width &&
                                                    this.X > stairs.X + stairs.Width - this.Width - 10 &&
                                                    this.LaddersNeeded != this.LaddersHit)
                                                {
                                                    this.CurrentTile = stairs;
                                                    this.CurrentState = InventionState.Teleporter;
                                                }
                                            }
                                            // Check if the invention needs to go down the stairs and is past them
                                            else if (this.StairsNeeded > 0 && this.X - this.Width < stairs.X) { this.Reverse(); }
                                            break;
                                    }
                                    break;

                                // Stairs are facing left
                                case -1:
                                    // Check the direction of the invention
                                    switch (this.Direction)
                                    {
                                        // Invention is facing right
                                        case 1:
                                            // Check if the invention needs to go up the stairs
                                            if (this.LaddersNeeded > 0)
                                            {
                                                // Check if the invention is hitting those stairs
                                                if (this.RectPosition.Intersects(stairs.RectPosition) &&
                                                    this.X > stairs.X && this.LaddersNeeded != this.LaddersHit)
                                                {
                                                    this.CurrentTile = stairs;
                                                    this.CurrentState = InventionState.Teleporter;
                                                }
                                            }
                                            // Check if the invention needs to go down the stairs and is past them
                                            else if (this.StairsNeeded > 0 && this.X > stairs.X + stairs.Width) { this.Reverse(); }
                                            break;

                                        // Invention is facing left
                                        case -1:
                                            //Check if the invention needs to go down the stairs
                                            if (this.StairsNeeded > 0)
                                            {
                                                // Check if the invention is hitting those stairs
                                                if (this.RectPosition.Intersects(stairs.RectPosition) &&
                                                    this.X < stairs.X + stairs.Width - this.Width &&
                                                    this.StairsNeeded != this.StairsHit)
                                                {
                                                    this.CurrentTile = stairs;
                                                    this.CurrentState = InventionState.Stairs;
                                                }
                                            }
                                            // Check if the invention needs to go up the stairs and is past them
                                            else if (this.LaddersNeeded > 0 && this.X < stairs.X - this.Width) { this.Reverse(); }
                                            break;
                                    }
                                    break;
                            }
                            break;

                        // The route is a ladder
                        case "SleepyScientist.Teleporter":
                            Teleporter ladder = (Teleporter)route;
                             // Check if the invention is moving left or right
                            switch (this.Direction)
                            {
                                // Moving right
                                case 1:
                                    // Check if the invention needs to go up the ladder
                                    if (this.LaddersNeeded > 0)
                                    {
                                        // Check if the invention is hitting the ladder
                                        if (this.RectPosition.Bottom == ladder.RectPosition.Bottom &&
                                            this.RectPosition.X > ladder.RectPosition.X - GameConstants.BUFFER &&
                                            this.RectPosition.X < ladder.RectPosition.X + ladder.RectPosition.Width &&
                                            this.LaddersHit != this.LaddersNeeded)
                                        {
                                            this.X = ladder.X;
                                            this.CurrentTile = ladder;
                                            this.CurrentState = InventionState.Teleporter;
                                        }
                                    }
                                    // Check if the invention needs to go down the ladder
                                    else if (this.StairsNeeded > 0)
                                    {
                                        // Check if the ladder is hitting the ladder
                                        if (this.RectPosition.Top == ladder.RectPosition.Top &&
                                            this.RectPosition.X > ladder.RectPosition.X - GameConstants.BUFFER &&
                                            this.RectPosition.X < ladder.RectPosition.X + ladder.RectPosition.Width &&
                                            this.StairsHit != this.StairsNeeded)
                                        {
                                            this.X = ladder.X;
                                            this.CurrentTile = ladder;
                                            this.CurrentState = InventionState.Stairs;
                                        }
                                    }
                                    break;

                                // Moving left
                                case -1:
                                    // Check if the invention needs to go up the ladder
                                    if (this.LaddersNeeded > 0)
                                    {
                                        // Check if the invention is hitting the ladder
                                        if (this.RectPosition.Bottom == ladder.RectPosition.Bottom &&
                                            this.RectPosition.X < ladder.RectPosition.X &&
                                            this.RectPosition.X > ladder.RectPosition.X - ladder.RectPosition.Width &&
                                            this.LaddersHit != this.LaddersNeeded)
                                        {
                                            this.X = ladder.X;
                                            this.CurrentTile = ladder;
                                            this.CurrentState = InventionState.Teleporter;
                                        }
                                    }
                                    // Check if the invention needs to go down the ladder
                                    else if (this.StairsNeeded > 0)
                                    {
                                        // Check if the invention is hitting the ladder
                                        if (this.RectPosition.Top == ladder.RectPosition.Top &&
                                            this.RectPosition.X < ladder.RectPosition.X &&
                                            this.RectPosition.X > ladder.RectPosition.X - ladder.RectPosition.Width &&
                                            this.StairsHit != this.StairsNeeded)
                                        {
                                            this.X = ladder.X;
                                            this.CurrentTile = ladder;
                                            this.CurrentState = InventionState.Stairs;
                                        }
                                    }
                                    break;
                            }
                            break;
                    }
                }

                // Check the current state of the invention
                switch (this.CurrentState)
                {
                    // The invention is going down something
                    case InventionState.Stairs:
                        // Check if the invention is going down a ladder or staircase
                        switch (this.CurrentTile.GetType().ToString())
                        {
                            // The invention is going down a staircase
                            case "SleepyScientist.Stairs":
                                this.VeloX = GameConstants.INVENTION_STAIR_X_VELOCITY * this.Direction;
                                this.VeloY = GameConstants.INVENTION_STAIR_Y_VELOCITY;
                                break;
                            // The invention is going down a ladder
                            case "SleepyScientist.Teleporter":
                                this.VeloX = GameConstants.INVENTION_TELEPORTER_X_VELOCITY;
                                this.VeloY = -GameConstants.INVENTION_TELEPORTER_Y_VELOCITY;
                                break;
                        }
                        // Check if the invention has reached the bottom of the stairs
                        if (this.RectPosition.Bottom >= this.CurrentTile.RectPosition.Bottom)
                        {
                            this.StairsHit++;
                            this.VeloY = 0;
                            this.CurrentFloor.Inventions.Remove(this);
                            this.CurrentState = InventionState.Walking;
                            this.CurrentTile = this.Room.Floors[this.FloorNumber - 1];
                            this.CurrentFloor = this.Room.Floors[this.FloorNumber - 1];
                            this.CurrentFloor.Inventions.Add(this);
                            this.Y = this.CurrentTile.Y - this.Height;
                            this.FloorNumber--;

                            // Remove this part from the invention's path and get the next piece of the path
                            this.Path.RemoveAt(0);
                            if (this.Path.Count != 0) { if (this.X > this.Path[0].X) { this.Direction = -1; } else { this.Direction = 1; } }
                            else if (this.X > this.TargetX) { this.Direction = -1; } else { this.Direction = 1; }
                        }
                        break;

                    // The invention is going up something
                    case InventionState.Teleporter:
                        // Check if the invention is going up a ladder or staircase
                        switch (this.CurrentTile.GetType().ToString())
                        {
                            // The invention is going up a staircase
                            case "SleepyScientist.Stairs":
                                this.VeloX = GameConstants.INVENTION_STAIR_X_VELOCITY * this.Direction;
                                this.VeloY = -GameConstants.INVENTION_STAIR_Y_VELOCITY;
                                break;
                            // The invention is going up a ladder
                            case "SleepyScientist.Teleporter":
                                this.VeloX = GameConstants.INVENTION_TELEPORTER_X_VELOCITY;
                                this.VeloY = GameConstants.INVENTION_TELEPORTER_Y_VELOCITY;
                                break;
                        }
                        // Check if the invention has reached the top of the stairs
                        if (this.RectPosition.Top <= this.CurrentTile.RectPosition.Top)
                        {
                            this.LaddersHit++;
                            this.VeloY = 0;
                            this.CurrentFloor.Inventions.Remove(this);
                            this.CurrentState = InventionState.Walking;
                            this.CurrentTile = this.Room.Floors[this.FloorNumber + 1];
                            this.CurrentFloor = this.Room.Floors[this.FloorNumber + 1];
                            this.CurrentFloor.Inventions.Add(this);
                            this.Y = this.CurrentTile.Y - this.Height;
                            this.FloorNumber++;

                            // Remove this part from the invention's path and get the next piece of the path
                            this.Path.RemoveAt(0);
                            if (this.Path.Count != 0) { if (this.X > this.Path[0].X) { this.Direction = -1; } else { this.Direction = 1; } }
                            else if (this.X > this.TargetX) { this.Direction = -1; } else { this.Direction = 1; }
                        }
                        break;

                    // The invention is walking on the floor
                    case InventionState.Walking:

                        // Check if the invention is on the floor it needs to be
                        if (this.LaddersHit == this.LaddersNeeded && this.StairsHit == this.StairsNeeded)
                        {
                            // Check if target would place invention on a pit.
                            foreach (Pit curPit in CurrentFloor.Pits)
                            {
                                // If the target is on a pit then change the target so it isn't.
                                if (this.TargetX > curPit.X && this.TargetX < curPit.X + curPit.Width)
                                {
                                    // If the target is over the left half of the pit
                                    if ( this.TargetX < curPit.X + curPit.Width / 2 )
                                        // then move invention to left of pit.
                                        this.TargetX = (int)(curPit.X);
                                    // Else the target is over the right half of the pit
                                    else
                                        // then move invention to right of pit. 
                                        this.TargetX = (int)(curPit.X + curPit.Width);
                                }
                            }

                            // Check the direction of the invention
                            switch (this.Direction)
                            {
                                // Moving right
                                case 1:
                                    // Check if the invention has reached its target destination
                                    if (this.X + this.Width / 2 >= this.TargetX) { this.ReachedTarget(); }
                                    break;

                                // Moving left
                                case -1:
                                    // Check if the invention has reached its target destination
                                    if (this.X + this.Width / 2 <= this.TargetX) { this.ReachedTarget(); }
                                    break;
                            }
                        }
                        this.VeloX = GameConstants.DEFAULT_INVENTION_X_VELOCITY * this.Direction;
                        this.VeloY = 0;
                        break;
                }

                // Update the position of the selection box around the invention
                if (GameConstants.MOVING_INVENTION)
                {
                    this.SelectionBox.X = (this.SelectionBox.X + this.VeloX * Time.DeltaTime / GameConstants.SLOW_MOTION);
                    this.SelectionBox.Y = (this.SelectionBox.Y + this.VeloY * Time.DeltaTime / GameConstants.SLOW_MOTION);
                }
                else
                {
                    float updateX = this.VeloX * Time.DeltaTime;
                    this.SelectionBox.X = (this.SelectionBox.X + this.VeloX * Time.DeltaTime);
                    this.SelectionBox.Y = (this.SelectionBox.Y + this.VeloY * Time.DeltaTime);
                }

                base.Update();
            }
        }

        /// <summary>
        /// Logic behind how the invention determines where he should go
        /// </summary>
        public void DeterminePath()
        {
            // Compute the difference in y location
            int verticalChange = (int)(this.Y + this.Height - this.TargetY);
            // Check if the invention needs to go up
            if (verticalChange > 0) { this.LaddersNeeded = verticalChange / GameConstants.DISTANCE_BETWEEN_FLOORS; }
            // Check if the invetion needs to go down
            else { this.StairsNeeded = (Math.Abs(verticalChange) + 100) / GameConstants.DISTANCE_BETWEEN_FLOORS; }

            // Check if the target is on the same floor
            if (this.LaddersNeeded == 0 && this.StairsNeeded == 0) 
            {
                this.CurrentState = InventionState.Walking;
                if (this.X > this.TargetX) { this.Direction = -1; } else { this.Direction = 1; }
                return; 
            }

            int currentFloor = this.FloorNumber;
            bool foundPath = false;
            int ladders = 0;
            int stairs = 0;
            List<GameObject> path = new List<GameObject>();
            bool isPathPossible = true;

            // Check if ladders are needed but don't exist.
            if (this.LaddersNeeded > 0 && this.Room.Floors[this.FloorNumber].Teleporters.Count == 0)
            {
                isPathPossible = false;
            }

            // Check if stairs are needed but don't exist.
            if (this.StairsNeeded > 0 && this.Room.Floors[this.FloorNumber + 1].Stairs.Count == 0)
            {
                isPathPossible = false;
            }

            // Exit the function if there isn't a path to target.
            if (isPathPossible == false) { ReachedTarget(); return; }

            // Loop until a path has been found
            while (!foundPath)
            {
                List<GameObject> potentialRoutes = new List<GameObject>();
                // Need to go up
                if (this.LaddersNeeded > 0)
                {
                    // Pool together all of the potential routes the invention can take on this floor
                    if (this.Room.Floors[currentFloor].Teleporters.Count != 0) potentialRoutes.AddRange(this.Room.Floors[currentFloor].Teleporters);
                    if (this.Room.Floors[currentFloor + 1].Stairs.Count != 0) potentialRoutes.AddRange(this.Room.Floors[currentFloor + 1].Stairs);

                    // If the potential routes are not zero, find the closest
                    if (potentialRoutes.Count != 0)
                    {
                        // Find the closest route to take
                        GameObject closest = potentialRoutes[0];
                        foreach (GameObject item in potentialRoutes) { if (Math.Abs(this.X - item.X) < Math.Abs(this.X - closest.X)) { closest = item; } }
                        path.Add(closest);
                        ladders++;
                        // Check if the invention has reached its destination floor
                        if (ladders == this.LaddersNeeded)
                        {
                            // Set the path to the invention
                            this.Path = path;
                            foundPath = true;
                        }
                        else { currentFloor++; }
                    }
                }
                // Need to go down
                if (this.StairsNeeded > 0)
                {
                    // Pool together all of the potential routes the invention can take on this floor
                    if (this.Room.Floors[currentFloor].Stairs.Count != 0) potentialRoutes.AddRange(this.Room.Floors[currentFloor].Stairs);
                    if (this.Room.Floors[currentFloor - 1].Teleporters.Count != 0) potentialRoutes.AddRange(this.Room.Floors[currentFloor - 1].Teleporters);

                    // If the potential routes are not zero, find the closest
                    if (potentialRoutes.Count != 0)
                    {
                        // Find the closest route to take
                        GameObject closest = potentialRoutes[0];
                        foreach (GameObject item in potentialRoutes) { if (Math.Abs(this.X - item.X) < Math.Abs(this.X - closest.X)) { closest = item; } }
                        path.Add(closest);
                        stairs++;
                        // Check if the invention has reached its destination floor
                        if (stairs == this.StairsNeeded)
                        {
                            // Set the path to the invention
                            this.Path = path;
                            foundPath = true;
                        }
                        else { currentFloor--; }
                    }
                }
            }
            // Check which direction the invention should go towards the first route in the path
            if (this.X > this.Path[0].X) { this.Direction = -1; } else { this.Direction = 1; }
            this.CurrentState = InventionState.Walking;
        }
        
        /// <summary>
        /// Once the invention has reached its target, his attributes are reset accordingly
        /// </summary>
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
            this.StairsHit = 0;
            this.StairsNeeded = 0;
        }

        public override void Draw(SpriteBatch batch, Rectangle? pos = null)
        {
            if (pos != null) { if (this.Hovered) { batch.Draw(GameConstants.BLANK, pos.Value, Color.Red); } }
            base.Draw(batch, pos);
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