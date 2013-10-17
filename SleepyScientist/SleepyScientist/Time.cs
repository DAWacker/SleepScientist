using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SleepyScientist
{
    /// <summary>
    /// Globally accessible program time.
    /// </summary>
    static class Time
    {
        #region Properties
        private static float _curTime = 0;  // Current game time.
        private static float _deltaTime = 0;// Time difference between updates.
        #endregion

        #region Attributes
        public static float CurTime { get { return _curTime; } set { _curTime = value; } }
        public static float DeltaTime { get { return _deltaTime; } set { _deltaTime = value; } }
        #endregion

        #region Methods
        /// <summary>
        /// Updates the state of Time.
        /// </summary>
        /// <param name="deltaTime">The amount to update by.</param>
        public static void Update(float deltaTime)
        {

        }
        #endregion
    }
}
