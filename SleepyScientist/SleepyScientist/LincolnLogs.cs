// Author: Thomas Bentley

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SleepyScientist
{

    class LincolnLogs : Invention
    {
        /// <summary>
        /// Constructor for Lincoln Logs invention. Chains to base
        /// </summary>
        /// <param name="name">"Lincoln Logs"</param>
        /// <param name="x">Initial X position for invention</param>
        /// <param name="y">Initial Y position for invention</param>
        /// <param name="width">Width of invention</param>
        /// <param name="height">Height of invention</param>
        public LincolnLogs(string name, int x, int y, int width, int height, Room room)
            : base(name, x, y, width, height, room) { }

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
