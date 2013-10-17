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
        private InventionState _prevState;

        #endregion

        // Possible states for the invention
        public enum InventionState
        {
            Idle,
            Walking,
            JackInTheBox,
            Ladder,
            Stairs
        }

        #region Properties

        // Gets or sets the activation state of the invention.
        public bool Activated { get { return _active; } set { _active = value; } }

        // Get or set the state of the invention
        public InventionState CurrentState { get { return _curState; } set { _curState = value; } }
        public InventionState PreviousState { get { return _prevState; } set { _prevState = value; } }

        #endregion

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
            _curState = InventionState.Idle;
            _prevState = InventionState.Idle;
            _active = false;
        }

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
            this.StayOnScreen();
        }

        /// <summary>
        /// Move the invention to a specific coordinate on the level.
        /// (might do this some other way than just x/y)
        /// </summary>
        /// <param name="x">X-coordinate</param>
        /// <param name="y">Y-coordinate</param>
        public void Move(int x, int y)
        {
            // Pathing code here
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
    }
}