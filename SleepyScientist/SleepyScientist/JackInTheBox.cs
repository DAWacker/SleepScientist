// Author: Thomas Bentley

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SleepyScientist
{
    class JackInTheBox : Invention
    {

        #region Constructor

        /// <summary>
        /// Constructor for Jack In The Box invention. Chains to base
        /// </summary>
        /// <param name="name">"Jack In The Box"</param>
        /// <param name="max_uses">maximum number of uses per level</param>
        /// <param name="x">Initial X position for invention</param>
        /// <param name="y">Initial Y position for invention</param>
        /// <param name="width">Width of invention</param>
        /// <param name="height">Height of invention</param>
        public JackInTheBox(string name, int x, int y, int width, int height, Room room, int startFloor)
            : base(name, x, y, width, height, room, startFloor) { }

        #endregion

        #region Method

        /// <summary>
        /// Method that executes the functionality of a Jack In The Box
        /// </summary>
        public override void Use(Scientist scientist)
        {
            // Launch scientist
            // Need to call the animation and what else to launch?

            scientist.Jump();
            base.Use(scientist);
        }

        #endregion
    }
}
