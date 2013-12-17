using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SleepyScientist
{
    class BatteryHolder : GameObject
    {
        public BatteryHolder(int x, int y, int width, int height)
            : base(x, y, width, height, GameConstants.DEFAULT_DIRECTION)
        {
            // Get a copy of the Eggbeater Animation
            Animations = AnimationLoader.GetSetCopy("BatteryHolder");
            Animations.ChangeAnimation("Sparking");
        }
    }
}
