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
        public enum ScientistState
        {
            Walking,
            JackInTheBox,
            RocketSkates,
            EggBeater,
            LincolnLogs,
            Ladder,
            Stairs,
            NULL
        }

        // Current and previous states of the scientist
        private ScientistState _curState;
        private ScientistState _prevState;

        #endregion

        #region Properties

        public ScientistState CurrentState { get { return _curState; } set { _prevState = _curState;  _curState = value; } }
        public ScientistState PreviousState { get { return _prevState; } set { _prevState = value; } }

        #endregion

        #region Constructor

        /// <summary>
        /// Parameterized constructor for the scientist
        /// </summary>
        /// <param name="name">The name</param>
        /// <param name="x">Starting x-coordinate</param>
        /// <param name="y">Starting y-coordinate</param>
        /// <param name="image">The image</param>
        public Scientist(string name, int x, int y, int width, int height)
            : base(name, x, y, width, height)
        {
            _curState = ScientistState.Walking;
            _prevState = ScientistState.Walking;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Draw the scientist
        /// </summary>
        /// <param name="batch">The sprite batch you want to draw on</param>
        public override void Draw(SpriteBatch batch) { base.Draw(batch); }

        /// <summary>
        /// Update the scientist
        /// </summary>
        public override void Update() 
        {
            switch (_curState)
            {
                case ScientistState.Walking:
                    if (_prevState != ScientistState.Walking)
                    {
                        //VeloX = PrevVeloX; // I had an error with this for some reason.
                        VeloX = GameConstants.DEFAULT_X_VELOCITY;
                        VeloY = GameConstants.DEFAULT_Y_VELOCITY;
                    }
                    break;
                case ScientistState.JackInTheBox:
                    break;
                case ScientistState.RocketSkates:
                    break;
                case ScientistState.EggBeater:
                    break;
                case ScientistState.LincolnLogs:
                    break;
                case ScientistState.Ladder:
                    VeloX = 0;
                    VeloY = GameConstants.LADDER_Y_VELOCITY;
                    break;
                case ScientistState.Stairs:
                    break;
            }

            base.Update(); 
        }

        /// <summary>
        /// Update the scientist's state based off of the interaction.
        /// </summary>
        /// <param name="gameObject">The GameObject being interacted with.</param>
        public override bool InteractWith(GameObject gameObject)
        {
            ScientistState newState = ScientistState.NULL;

            // Check gameObject's type and change state accordingly.
            if (gameObject.GetType() == typeof(Ladder))
            {
                newState = ScientistState.Ladder;
            }
            else if (gameObject.GetType() == typeof(Stairs))
            {
                newState = ScientistState.Stairs;
            }

            // Update scientist's state only if state has changed.
            if (newState != ScientistState.NULL)
            {
                // Also sets previous state.
                CurrentState = newState;
            }

            base.InteractWith(gameObject);

            return newState != ScientistState.NULL;
        }
        #endregion
    }
}