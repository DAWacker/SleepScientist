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
            Stairs
        }

        // Current and previous states of the scientist
        private ScientistState _curState;
        private ScientistState _prevState;

        private List<Ladder> _ladders;
        private GameObject _currentTile;

        #endregion

        #region Properties

        public ScientistState CurrentState { get { return _curState; } set { _curState = value; } }
        public ScientistState PreviousState { get { return _prevState; } set { _prevState = value; } }
        public List<Ladder> Ladders { get { return _ladders; } set { _ladders = value; } }
        public GameObject CurrentTile { get { return _currentTile; } set { _currentTile = value; } }

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
            _currentTile = null;
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
            // Check if the scientist is climbing a ladder
            foreach (Ladder piece in this.Ladders)
            {
                if (this.RectPosition.Intersects(piece.RectPosition))
                {
                    this.CurrentState = ScientistState.Ladder;
                    this.CurrentTile = piece;
                    break;
                }
                else 
                {
                    this.CurrentState = ScientistState.Walking;
                    this.CurrentTile = null;
                }
            }

            switch (this.CurrentState)
            {
                case ScientistState.Ladder:
                    this.VeloX = 0;
                    this.VeloY = GameConstants.LADDER_Y_VELOCITY;
                    break;

                case ScientistState.Walking:
                    this.VeloX = GameConstants.DEFAULT_X_VELOCITY * this.Direction;
                    this.VeloY = 0;
                    break;

                default:
                    break;
            }

            base.Update();
        }
        #endregion
    }
}