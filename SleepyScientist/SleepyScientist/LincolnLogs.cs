﻿// Author: Thomas Bentley

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SleepyScientist
{
    enum STATE { NORMAL, STAIRS, LADDER }

    class LincolnLogs : Invention
    {
        STATE currentState = STATE.NORMAL;

        /// <summary>
        /// Constructor for Lincoln Logs invention. Chains to base
        /// </summary>
        /// <param name="name">"Lincoln Logs" - Do we want to hard code this for each invention then pass the hardcoded to the base?</param>
        /// <param name="max_uses">maximum number of uses per level</param>
        /// <param name="x">Initial X position for invention</param>
        /// <param name="y">Initial Y position for invention</param>
        /// <param name="width">Width of invention</param>
        /// <param name="height">Height of invention</param>
        public LincolnLogs(string name, int max_uses, int x, int y, int width, int height)
            : base(name, max_uses, x, y, width, height)
        {
        }

        /// <summary>
        /// Method that executes the functionality of a Lincoln Log
        /// </summary>
        public override void Use( Scientist s )
        {
            // Turn into ladder or stairs
            // Animation to change
            // currentState = STATE.STAIRS | currentState = STAIRS.LADDER

            base.Use( s );
        }
    }
}
