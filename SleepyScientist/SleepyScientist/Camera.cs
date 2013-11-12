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
        private GameObject _followTarget;       // The target, if any, the camera should follow.
        private bool _shouldFollowTarget;       // Should the camera follow its target?

        private float _veloX;                   // The velocity of the camera. Used for camera scrolling.
        
        #endregion

        #region Properties

        public GameObject FollowTarget { get { return _followTarget; } set { _followTarget = value; } }
        public bool ShouldFollowTarget { get { return _shouldFollowTarget; } set { _shouldFollowTarget = value; } }

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
            _followTarget = null;
            _shouldFollowTarget = true;
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
            if (_zoomFactor != GameConstants.ZOOM_INVENTION_VIEW)
            {
                Zoom(GameConstants.ZOOM_INVENTION_VIEW);
                CenterCameraOn(x, y);
            }
        }

        /// <summary>
        /// Center the camera on the given coordinates.
        /// </summary>
        /// <param name="x">x coord.</param>
        /// <param name="y">y coord.</param>
        public void CenterCameraOn(int x, int y)
        {
            // Convert coords to zoomed-scale coords
            x = (int)(x * _zoomFactor);
            y = (int)(y * _zoomFactor);
            // Update camera coords.
            _cameraView.X = (int)(x - _cameraView.Width / 2);
            _cameraView.Y = (int)(y - _cameraView.Height / 2);
            FixOffset();
        }

        /// <summary>
        /// Determines if the camera should scroll based on the coordinate
        /// given.
        /// </summary>
        /// <param name="x">x coord</param>
        /// <param name="y">y coord</param>
        public void UpdateCameraScroll(int x, int y)
        {
            float screenXPercent = (float)x / GameConstants.SCREEN_WIDTH;

            // Check if coords are within the bounds of the scroll boxes.
            if (screenXPercent >= GameConstants.SCROLL_BOUND_RIGHT)
                _veloX = GameConstants.CAMERA_X_VELO;
            else if (screenXPercent <= GameConstants.SCROLL_BOUND_LEFT)
                _veloX = -GameConstants.CAMERA_X_VELO;
            else
                _veloX = 0;
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
        /// Draw the given GameObject with given modifications to scale and position
        /// due to Camera zoomFactor.
        /// Note: The given SpriteBatch should have previously called its Begin method.
        /// </summary>
        /// <param name="spriteBatch">The SpriteBatch to use.</param>
        /// <param name="gObject"></param>
        public void DrawGameObject(SpriteBatch spriteBatch, GameObject gObject ) {
            Rectangle drawPos = ToLocal(gObject.RectPosition);
            gObject.Draw(spriteBatch, drawPos);
        }

        /// <summary>
        /// Draw multiple GameObjects. Uses DrawGameObject().
        /// Note: The given SpriteBatch should have previously called its Begin method.
        /// </summary>
        /// <param name="spriteBatch">The SpriteBatch to use.</param>
        /// <param name="objects">The objects to draw.</param>
        public void DrawGameObjects(SpriteBatch spriteBatch, List<GameObject> objects)
        {
            foreach (GameObject g in objects)
            {
                DrawGameObject(spriteBatch, g);
            }
        }

        /// <summary>
        /// Update the camera position if necessary.
        /// </summary>
        public void Update()
        {
            if (_followTarget != null && _shouldFollowTarget)
            {
                ZoomToLocation( _followTarget.X, _followTarget.Y );
                CenterCameraOn( _followTarget.X, _followTarget.Y );
            }
            else
            {
                _cameraView.X = (int)(_cameraView.X + _veloX);
                FixOffset();
            }
        }

        /// <summary>
        /// Create a locally coordinated Rectangle from a globally coordinated one.
        /// </summary>
        /// <param name="globalRect">The globally coordinated Rectangle.</param>
        /// <returns>A locally coordinated version of globalRect.</returns>
        public Rectangle ToLocal(Rectangle globalRect)
        {
            Rectangle localRect = new Rectangle(
                globalRect.X,
                globalRect.Y,
                globalRect.Width,
                globalRect.Height
            );

            localRect.X = (int)(globalRect.X * _zoomFactor) - _cameraView.X;
            localRect.Y = (int)(globalRect.Y * _zoomFactor) - _cameraView.Y;
            localRect.Width = (int)(globalRect.Width * _zoomFactor);
            localRect.Height = (int)(globalRect.Height * _zoomFactor);

            return localRect;
        }

        /// <summary>
        /// Create a globally coordinated Point from a locally coordinated one.
        /// </summary>
        /// <param name="localPoint">The locally coordinated Point.</param>
        /// <returns>A globally coordinated version of localPoint.</returns>
        public Point ToGlobal(Point localPoint)
        {
            Point globalPoint = new Point(
                localPoint.X,
                localPoint.Y
            );

            globalPoint.X = (int)((localPoint.X + _cameraView.X) / _zoomFactor);
            globalPoint.Y = (int)((localPoint.Y + _cameraView.Y) / _zoomFactor);

            return globalPoint;
        }

        #endregion

    }
}
