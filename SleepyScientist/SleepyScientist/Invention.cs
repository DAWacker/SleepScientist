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
        #region Fields
        public int MAX_USES = 1;
        /// <summary>
        /// Number of uses currently left for this invention.
        /// </summary>
        private int numUses;
        /// <summary>
        /// Is the invention currently activated?
        /// </summary>
        private bool activated = false;
        #endregion

        // States for the inventions
        private InventionState _curState;
        private InventionState _prevState;

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
        /// <summary>
        /// Gets or sets the number of uses available to this invention
        /// </summary>
        public int NumUses
        {
            get { return numUses; }
            set { numUses = value; }
        }
        /// <summary>
        /// Gets or sets the activation state of the invention.
        /// </summary>
        public bool Activated
        {
            get { return activated; }
            set { activated = value; }
        }

        // Get or set the state of the invention
        public InventionState CurrentState { get { return _curState; } set { _curState = value; } }
        public InventionState PreviousState { get { return _prevState; } set { _prevState = value; } }
        #endregion

        /// <summary>
        /// Chained Constructor (Base_Invention and Game_Object)
        /// </summary>
        /// <param name="name">Name of this invention</param>
        /// <param name="uses">Max number of uses per invention</param>
        /// <param name="x">X position</param>
        /// <param name="y">Y position</param>
        /// <param name="width">Width of invention</param>
        /// <param name="height">Height of invention</param>
        public Invention(string name, int max_uses, int x, int y, int width, int height)
            : base(name, x, y, width, height)
        {
            // Set the max uses and the use counter
            this.MAX_USES = max_uses;
            this.numUses = MAX_USES;

            _curState = InventionState.Idle;
            _prevState = InventionState.Idle;
            this.activated = false;
        }

        /// <summary>
        /// Operator overload (Invention + Invention) = combo
        /// </summary>
        /// <param name="first">First invention of combo.</param>
        /// <param name="second">Second invention of combo.</param>
        /// <returns></returns>
        public static Invention operator +(Invention first, Invention second)
        {
            Invention combined = new Invention(first.Name + second.Name, first.numUses + second.numUses,
                                                            first.X, first.Y, first.Width, first.Height);
            return combined;
        }

        /// <summary>
        /// Method that executes the default functionality of an invention
        /// </summary>
        public virtual void Use( Scientist s ) { numUses--; }

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