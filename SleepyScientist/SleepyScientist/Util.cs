using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace SleepyScientist
{
    /// <summary>
    /// Various utility functions.
    /// </summary>
    static class Util
    {
        /// <summary>
        /// Converts a Vector4 into a Rectangle.
        /// Where:
        ///     W = Width
        ///     X = X,
        ///     Y = Y,
        ///     Z = Height
        /// </summary>
        /// <param name="vec">The Vector4 to convert.</param>
        /// <returns></returns>
        public static Rectangle VecToRect(Vector4 vec)
        {
            return new Rectangle
            (
                (int)vec.X,
                (int)vec.Y,
                (int)vec.W,
                (int)vec.Z
            );
        }

        /// <summary>
        /// Converts a Rectangle into a Vector4.
        /// </summary>
        /// <param name="rect">The Rectangle to convert.</param>
        /// <returns></returns>
        public static Vector4 RectToVec(Rectangle rect)
        {
            return new Vector4(rect.Width, rect.X, rect.Y, rect.Height);
        }

        /// <summary>
        /// Tests for collision between two Vector4 instances.
        /// AABB collision test.
        /// </summary>
        /// <param name="src">The first Vector4.</param>
        /// <param name="targ">The second Vector4</param>
        /// <returns>Are src and targ colliding?</returns>
        public static bool VecIntersection(Vector4 src, Vector4 targ)
        {
            float srcMinX = src.X,              // Top-left corner X
                  srcMinY = src.Y,              // Top-left corner Y
                  srcMaxX = src.X + src.W,      // Bot-right corner X
                  srcMaxY = src.Y + src.Z;      // Bot-right corner Y

            float targMinX = targ.X,            // Top-left corner X
                  targMinY = targ.Y,            // Top-left corner Y
                  targMaxX = targ.X + targ.W,   // Bot-right corner X
                  targMaxY = targ.Y + targ.Z;   // Bot-right corner Y

            if (srcMaxX < targMinX || targMaxX < srcMinX) return false;
            if (srcMaxY < targMinY || targMaxY < srcMinY) return false;
            return true;
        }
    }
}
