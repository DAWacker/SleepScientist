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

namespace SleepyScientist
{
    /// <summary>
    /// A Camera to handling the drawing of GameObjects.
    /// The Camera has the functionality to zoom in to reveal the area in greater detail.
    /// </summary>
    class Camera
    {
        #region Attributes

        private Rectangle _cameraView;          // Where the camera is currently looking.
        private float _zoomFactor;              // The zoom level to view the level with.
                                                // i.e. If value is 2 then level is shown at twice its size.

        #endregion

        #region Constructor

        /// <summary>
        /// Construct with a default zoom level.
        /// </summary>
        /// <param name="initialZoom">Start zoom level.</param>
        public Camera(int initialZoom = 1)
        {
            _zoomFactor = initialZoom;
            _cameraView = new Rectangle(0, 0, GameConstants.SCREEN_WIDTH / initialZoom, GameConstants.SCREEN_HEIGHT / initialZoom);
        }

        #endregion

        #region Methods

        /// <summary>
        /// Zoom immediately to the given value.
        /// </summary>
        /// <param name="zoomFactor">The new zoomFactor.</param>
        public void Zoom(float zoomFactor)
        {
            if (zoomFactor >= GameConstants.MINIMUM_ZOOM)
                _zoomFactor = zoomFactor;
            else
                _zoomFactor = GameConstants.MINIMUM_ZOOM;

            _cameraView.Width = GameConstants.SCREEN_WIDTH;
            _cameraView.Height = GameConstants.SCREEN_HEIGHT;
            _cameraView.X = 0;
            _cameraView.Y = 0;
            FixOffset();
        }

        /// <summary>
        /// Increment or decrement the current zoomFactor.
        /// </summary>
        /// <param name="zoomFactorMod">Amount to modify zoomFactor by.</param>
        public void ModZoom(float zoomFactorMod)
        {
            _zoomFactor += zoomFactorMod;
            if (_zoomFactor < GameConstants.MINIMUM_ZOOM)
                _zoomFactor = GameConstants.MINIMUM_ZOOM;
            FixOffset();
        }

        /// <summary>
        /// Zoom into a location on screen.
        /// </summary>
        /// <param name="x">X coordinate.</param>
        /// <param name="y">Y coordinate.</param>
        public void ZoomToLocation(int x, int y)
        {
            Zoom(GameConstants.ZOOM_INVENTION_VIEW);
            // Convert coords to zoomed-scale coords
            x = (int)(x * _zoomFactor);
            y = (int)(y * _zoomFactor);
            // Update camera coords.
            _cameraView.X = (int)(x - _cameraView.Width / 2);
            _cameraView.Y = (int)(y - _cameraView.Height / 2);
            FixOffset();
        }

        /// <summary>
        /// Called after camera is zoomed to fix any view clipping.
        /// </summary>
        private void FixOffset()
        {
            // Fix x if needed.
            if (_cameraView.X < 0)
                _cameraView.X = 0;
            else if (_cameraView.X + _cameraView.Width > GameConstants.SCREEN_WIDTH * _zoomFactor)
                _cameraView.X = (int)(GameConstants.SCREEN_WIDTH * _zoomFactor - _cameraView.Width);

            // Fix y if needed.
            if (_cameraView.Y < 0)
                _cameraView.Y = 0;
            else if (_cameraView.Y + _cameraView.Height > GameConstants.SCREEN_HEIGHT * _zoomFactor)
                _cameraView.Y = (int)(GameConstants.SCREEN_HEIGHT * _zoomFactor - _cameraView.Height);
        }

        /// <summary>
        /// Draw the given GameObjects with given modifications to scale and position
        /// due to Camera zoomFactor.
        /// Note: The given SpriteBatch should have previously called its Begin method.
        /// </summary>
        /// <param name="spriteBatch">The SpriteBatch to use.</param>
        /// <param name="objects">The objects to draw.</param>
        public void DrawGameObjects(SpriteBatch spriteBatch, List<GameObject> objects)
        {
            Rectangle drawPos = new Rectangle();
            foreach (GameObject g in objects)
            {
                drawPos.X = (int)(g.X * _zoomFactor - _cameraView.X);
                drawPos.Y = (int)(g.Y * _zoomFactor - _cameraView.Y);
                drawPos.Width = (int)(g.Width * _zoomFactor);
                drawPos.Height = (int)(g.Height * _zoomFactor);
                g.Draw(spriteBatch, drawPos);
            }
        }

        #endregion

    }
}
