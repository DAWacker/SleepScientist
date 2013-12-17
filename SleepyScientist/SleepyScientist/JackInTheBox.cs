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
            : base(name, x, y, width, height, room, startFloor)
        {
            // Get a copy of the Scientist Animation
            Animations = AnimationLoader.GetSetCopy("Jack");
            Animations.ChangeAnimation("Closed");
        }

        #endregion

        #region Method

        /// <summary>
        /// Method that executes the functionality of a Jack In The Box
        /// </summary>
        public override void Use(Scientist scientist)
        {
            // Launch scientist
            // Need to call the animation and what else to launch?
            this.Animations.ChangeAnimation("Open");
            this.Animations.CurAnimation.Play(1);
            this.Animations.QueueAnimation("Closed");
            scientist.Jump();
            base.Use(scientist);
        }

        #endregion
    }
}
