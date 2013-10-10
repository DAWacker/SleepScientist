// Author: Thomas Bentley

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SleepyScientist
{
    class EggBeater : Invention
    {
        /// <summary>
        /// Constructor for Egg Beater invention. Chains to base
        /// </summary>
        /// <param name="name">"Egg Beater" - Do we want to hard code this for each invention then pass the hardcoded to the base?</param>
        /// <param name="max_uses">maximum number of uses per level</param>
        /// <param name="x">Initial X position for invention</param>
        /// <param name="y">Initial Y position for invention</param>
        /// <param name="width">Width of invention</param>
        /// <param name="height">Height of invention</param>
        public EggBeater(string name, int max_uses, int x, int y, int width, int height)
            : base(name, max_uses, x, y, width, height)
        {
        }

        /// <summary>
        /// Method that executes the functionality of an Egg Beater
        /// </summary>
        public override void Use(Scientist s)
        {
            s.Direction *= -1;  // Direction is a really weird member variable...

            base.Use( s );
        }
    }
}
