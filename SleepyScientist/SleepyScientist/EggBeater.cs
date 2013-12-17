// Author: Thomas Bentley

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SleepyScientist
{
    class EggBeater : Invention
    {

        #region Constructor

        /// <summary>
        /// Constructor for Egg Beater invention. Chains to base
        /// </summary>
        /// <param name="name">"Egg Beater"</param>
        /// <param name="max_uses">maximum number of uses per level</param>
        /// <param name="x">Initial X position for invention</param>
        /// <param name="y">Initial Y position for invention</param>
        /// <param name="width">Width of invention</param>
        /// <param name="height">Height of invention</param>
        public EggBeater(string name, int x, int y, int width, int height, Room room, int startFloor)
            : base(name, x, y, width, height, room, startFloor)
        {
            // Get a copy of the Eggbeater Animation
            Animations = AnimationLoader.GetSetCopy("Eggbeater");
            Animations.ChangeAnimation("Idle");
        }

        #endregion

        #region Methods

        /// <summary>
        /// Method that executes the functionality of an Egg Beater
        /// </summary>
        public override void Use(Scientist scientist)
        {
            // Reverse the direction of the 
            scientist.Reverse();
            base.Use(scientist);
        }

        #endregion
    }
}
