// Author: Thomas Bentley

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SleepyScientist
{
    class RocketSkateboard : Invention
    {
        /// <summary>
        /// Constructor for Rocket Skateboard invention. Chains to base
        /// </summary>
        /// <param name="name">"Rocket Skateboard" - Do we want to hard code this for each invention then pass the hardcoded to the base?</param>
        /// <param name="max_uses">maximum number of uses per level</param>
        /// <param name="x">Initial X position for invention</param>
        /// <param name="y">Initial Y position for invention</param>
        /// <param name="width">Width of invention</param>
        /// <param name="height">Height of invention</param>
        public RocketSkateboard(string name, int max_uses, int x, int y, int width, int height)
            : base(name, max_uses, x, y, width, height)
        {
        }

        /// <summary>
        /// Method that executes the functionality of a Rocket Skateboard
        /// </summary>
        public override void Use()
        {
            Scientist s = new Scientist("", 0, 0, 0, 0);
            s.VeloX *= 3;   // only lasts a certain amount of time, will have to address this

            base.Use();
        }
    }
}
