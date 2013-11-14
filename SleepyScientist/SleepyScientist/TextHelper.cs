using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SleepyScientist
{
    /// <summary>
    /// Class to provide special functions to screen text.
    /// </summary>
    static class TextHelper
    {
        public enum TextAlignment
        {
            Left,
            Center
        };

        #region Attributes

        private static SpriteBatch _batch;          // SpriteBatch to use when drawing text.
        private static SpriteFont _font;            // Font to use when drawing text.
        private static TextAlignment _alignment;    // Alignment to use when drawing text.
        private static Color _textColor;            // Color to use when drawing text.

        #endregion

        #region Properties

        public static SpriteBatch Batch { get { return _batch; } set { _batch = value; } }
        public static SpriteFont Font { get { return _font; } set { _font = value; } }
        public static TextAlignment Alignment { get { return _alignment; } set { _alignment = value; } }
        public static Color TextColor { get { return _textColor; } set { _textColor = value; } }

        #endregion

        #region Methods

        /// <summary>
        /// Draw a string with the current settings.
        /// </summary>
        /// <param name="text">The text to draw.</param>
        /// <param name="pos">The position to draw the text.</param>
        /// <param name="width">The width to fit the text into.</param>
        public static void DrawString(String text, Vector2 pos, float width = -1)
        {
            if (width != -1)
            {
                text = ConstrainWidth(text, width);
            }

            try
            {
                _batch.Begin();
                _batch.DrawString(_font, text, pos, _textColor);
                _batch.End();
            }
            catch( Exception e )
            {
                _batch.DrawString(_font, text, pos, _textColor);
            }

        }

        /// <summary>
        /// Constrain text to a given pixel width.
        /// </summary>
        /// <param name="text">The text to constrain.</param>
        /// <param name="width">The width to constrain to.</param>
        /// <returns>The text contrained to the given with.</returns>
        public static String ConstrainWidth(String text, float width)
        {
            StringBuilder line = new StringBuilder();
            StringBuilder constrainedText = new StringBuilder();
            String[] words = text.Split(new Char[] { ' ' });
            float lineWidth = 0;
            float wordWidth = 0;
            float spaceWidth = _font.MeasureString(" ").X;

            foreach (String word in words)
            {
                wordWidth = _font.MeasureString(word + " ").X;
                // If overflow gets detected.
                if (lineWidth + wordWidth > width)
                {
                    // Check alignment.
                    if (_alignment == TextAlignment.Center)
                    {
                        float remainingSpace = width - lineWidth;
                        int numSpaces = (int)(remainingSpace / 2 / spaceWidth);
                        while (numSpaces-- > 0)
                        {
                            line.Insert(0, " ");
                        }
                    }

                    // Add the whole line to the constrained text.
                    constrainedText.AppendLine(line.ToString());
                    // Reset the current line.
                    line.Clear();
                    lineWidth = 0;
                }

                line.Append(word + " ");
                lineWidth += wordWidth;
            }

            // Check alignment.
            if (_alignment == TextAlignment.Center)
            {
                float remainingSpace = width - lineWidth;
                int numSpaces = (int)(remainingSpace / 2 / spaceWidth);
                while (numSpaces-- > 0)
                {
                    line.Insert(0, " ");
                }
            }

            constrainedText.AppendLine(line.ToString());

            return constrainedText.ToString();
        }

        #endregion
    }
}
