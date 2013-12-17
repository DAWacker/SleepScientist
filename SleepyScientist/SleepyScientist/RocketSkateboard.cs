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
            // Get a copy of the Scientist Animation
            Animations = AnimationLoader.GetSetCopy("Skateboard");
            Animations.ChangeAnimation("Off");
        }

        /// <summary>
        /// Method that executes the functionality of a Rocket Skateboard
        /// </summary>
        public override void Use(Scientist scientist)
        {
            if (!this.Activated)
            {
                this.Animations.ChangeAnimation("On");
                this.VeloX = Math.Abs(scientist.VeloX) * GameConstants.SKATEBOARD_SPEEDUP * this.Direction;
                base.Use(scientist);
            }
        }

        public override void UnUse()
        {
            if (this.Activated)
            {
                this.Animations.ChangeAnimation("Off");
                base.UnUse();
                this.VeloX /= GameConstants.SKATEBOARD_SPEEDUP;
            }
        }

        public override void Update()
        {
            if (this.Activated || this.HasTarget)
            {
                this.StayOnScreen();
                this.Move();
                // Update the position of the selection box around the invention
                if (GameConstants.MOVING_INVENTION)
                {
                    this.SelectionBox.X = (this.SelectionBox.X + this.VeloX * Time.DeltaTime / GameConstants.SLOW_MOTION);
                    this.SelectionBox.Y = (this.SelectionBox.Y + this.VeloY * Time.DeltaTime / GameConstants.SLOW_MOTION);
                }
                else
                {
                    float updateX = this.VeloX * Time.DeltaTime;
                    this.SelectionBox.X = (this.SelectionBox.X + this.VeloX * Time.DeltaTime);
                    this.SelectionBox.Y = (this.SelectionBox.Y + this.VeloY * Time.DeltaTime);
                }
                base.Update();
            }
        }
    }
}
