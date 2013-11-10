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
        public RocketSkateboard(string name, int x, int y, int width, int height, Room room, int startFloor)
            : base(name, x, y, width, height, room, startFloor)
        {
        }

        /// <summary>
        /// Method that executes the functionality of a Rocket Skateboard
        /// </summary>
        public override void Use(Scientist s)
        {
            if (!this.Activated)
            {
                base.Use(s);
                this.Direction = s.Direction;
                this.VeloX = s.VeloX;
                VeloX *= GameConstants.SKATEBOARD_SPEEDUP;
                s.VeloX *= GameConstants.SKATEBOARD_SPEEDUP;   // only lasts a certain amount of time, will have to address this
                s.X = this.RectPosition.Center.X - s.Width / 2;
                s.Y -= this.Height / 2;
            }
        }

        public override void UnUse()
        {
            if (this.Activated)
            {
                base.UnUse();
                this.VeloX /= GameConstants.SKATEBOARD_SPEEDUP;
            }
        }

        public void Move(int veloX)
        {
            this.VeloX = veloX;
            this.StayOnScreen();
            this.Move();
        }
    }
}
