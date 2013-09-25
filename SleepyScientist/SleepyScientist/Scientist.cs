#region using statements
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Storage;
using Microsoft.Xna.Framework.GamerServices;
#endregion

namespace SleepyScientist
{
    class Scientist : AI
    {
        #region Attributes

        // Possible states the scientist could be in
        public enum AIState
        {
            Walking,
            JackInTheBox,
            RocketSkates,
            EggBeater,
            LincolnLogs,
            Ladder,
            Stairs
        }

        // Current and previous states of the scientist
        private AIState _curState;
        private AIState _prevState;

        #endregion

        #region Properties

        public AIState CurrentState { get { return _curState; } set { _curState = value; } }
        public AIState PreviousState { get { return _prevState; } set { _prevState = value; } }

        #endregion

        #region Constructor

        /// <summary>
        /// Parameterized constructor for the scientist
        /// </summary>
        /// <param name="name">The name</param>
        /// <param name="x">Starting x-coordinate</param>
        /// <param name="y">Starting y-coordinate</param>
        /// <param name="image">The image</param>
        public Scientist(string name, int x, int y, int width, int height, Texture2D image)
            : base(name, x, y, width, height, image)
        {
            _curState = AIState.Walking;
            _prevState = AIState.Walking;
        }

        #endregion
    }
}