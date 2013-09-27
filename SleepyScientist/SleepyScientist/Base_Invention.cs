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
    class Base_Invention : GameObject
    {
        #region Fields
        public int MAX_USES = 1;
        /// <summary>
        /// Name of the invention (maybe this can just be derived from the type name)
        /// </summary>
        private string name;
        /// <summary>
        /// Number of uses currently left for this invention.
        /// </summary>
        private int numUses;
        /// <summary>
        /// Is the invention currently activated?
        /// </summary>
        private bool activated = false;
        #endregion

        #region Properties
        /// <summary>
        /// Gets or set the name of the invention.
        /// </summary>
        public string Name
        {
            get { return name; }
            set { name = value; }
        }
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
        public Base_Invention(string name, int max_uses, int x, int y, int width, int height) : base( x,  y,  width,  height)
        {
            this.name = name;

            // Set the max uses and the use counter
            this.MAX_USES = max_uses;
            this.numUses = MAX_USES;

            this.activated = false;
        }

        /// <summary>
        /// Operator overload (Invention + Invention) = combo
        /// </summary>
        /// <param name="first">First invention of combo.</param>
        /// <param name="second">Second invention of combo.</param>
        /// <returns></returns>
        public static Base_Invention operator + (Base_Invention first, Base_Invention second)
        {
            Base_Invention combined = new Base_Invention(first.name + second.name, first.numUses + second.numUses,
                                                            first.X, first.Y, first.Width, first.Height);
            return combined;
        }

        public virtual void Use() { numUses--; }

        /// <summary>
        /// Combine an invention with this one.
        /// </summary>
        /// <param name="other">Invention to combine with.</param>
        /// <returns></returns>
        public Base_Invention Combine(Base_Invention other)
        {
            return (this + other);
        }
    }
}
