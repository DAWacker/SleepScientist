// Author: Thomas Bentley

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SleepyScientist.Inventions
{
    class Lincoln_Logs : Base_Invention
    {
        /// <summary>
        /// Constructor for Lincoln Logs invention. Chains to base
        /// </summary>
        /// <param name="name">"Lincoln Logs" - Do we want to hard code this for each invention then pass the hardcoded to the base?</param>
        /// <param name="max_uses">maximum number of uses per level</param>
        /// <param name="x">Initial X position for invention</param>
        /// <param name="y">Initial Y position for invention</param>
        /// <param name="width">Width of invention</param>
        /// <param name="height">Height of invention</param>
        public Lincoln_Logs(string name, int max_uses, int x, int y, int width, int height)
            : base(name, max_uses, x, y, width, height)
        {
        }

        /// <summary>
        /// Method that executes the functionality of a Lincoln Log
        /// </summary>
        public override void Use()
        {
            // Turn into ladder or stairs

            base.Use();
        }
    }
}