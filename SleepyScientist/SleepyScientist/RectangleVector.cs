using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace SleepyScientist
{
    /// <summary>
    /// Class providing Rectangle functionality with float precision.
    /// </summary>
    class RectangleVector
    {
        #region Attributes

        private Vector4 _data;

        #endregion

        #region Properties

        public Vector4 Data { get { return _data; } set { _data = value; } }
        public int Bottom { get { return (int)(_data.Y + _data.Z); } }
        public int Left { get { return (int)(_data.X); } }
        public int Right { get { return (int)(_data.X + _data.W); } }
        public int Top { get { return (int)(_data.Y); } }
        public int Width { get { return (int)(_data.W); } set { _data.W = value; } }
        public int Height { get { return (int)(_data.Z); } set { _data.Z = value; } }
        public Point Center { get { return new Point((int)_data.X, (int)_data.Y); } }
        public float X { get { return _data.X; } set { _data.X = value; } }
        public float Y { get { return _data.Y; } set { _data.Y = value; } }
        public float Z { get { return _data.Z; } set { _data.Z = value; } }
        public float W { get { return _data.W; } set { _data.W = value; } }

        #endregion

        #region Constructor

        /// <summary>
        /// Construct with float values.
        /// </summary>
        /// <param name="x">X coordinate.</param>
        /// <param name="y">Y corrdinate.</param>
        /// <param name="width">Width of the RectangleVector.</param>
        /// <param name="height">Height of the RectangleVector.</param>
        public RectangleVector(float x, float y, float width, float height)
        {
            _data = new Vector4(x, y, height, width);
        }

        /// <summary>
        /// Construct from a Rectangle.
        /// </summary>
        /// <param name="rect">The Rectangle to copy.</param>
        public RectangleVector(Rectangle rect)
        {
            _data = new Vector4(rect.X, rect.Y, rect.Height, rect.Width);
        }

        /// <summary>
        /// Construct from a Vector4.
        /// </summary>
        /// <param name="vec">The Vector4 to copy.</param>
        public RectangleVector(Vector4 vec)
        {
            _data = new Vector4(vec.X, vec.Y, vec.Z, vec.W);
        }

        #endregion

        #region Methods

        /// <summary>
        /// Tests for collision between two RectangleVector instances.
        /// AABB collision test.
        /// </summary>
        /// <param name="targ">The RectangleVector to test.</param>
        /// <returns>Are src and targ colliding?</returns>
        public bool Intersects(RectangleVector targ)
        {
            float thisMinX = this.X,            // Top-left corner X
                  thisMinY = this.Y,            // Top-left corner Y
                  thisMaxX = this.X + this.W,   // Bot-right corner X
                  thisMaxY = this.Y + this.Z;   // Bot-right corner Y

            float targMinX = targ.X,            // Top-left corner X
                  targMinY = targ.Y,            // Top-left corner Y
                  targMaxX = targ.X + targ.W,   // Bot-right corner X
                  targMaxY = targ.Y + targ.Z;   // Bot-right corner Y

            if (thisMaxX < targMinX || targMaxX < thisMinX) return false;
            if (thisMaxY < targMinY || targMaxY < thisMinY) return false;
            return true;
        }

        /// <summary>
        /// Implicit conversion of RectangleVector to a Rectangle.
        /// </summary>
        /// <param name="rect">The Rectangle to convert.</param>
        /// <returns>A new Rectangle converted from a RectangleVector.</returns>
        public static implicit operator Rectangle(RectangleVector rectVec)
        {
            return new Rectangle
            (
                (int)rectVec.X,
                (int)rectVec.Y,
                (int)rectVec.W,
                (int)rectVec.Z
            );
        }

        #endregion
    }
}
