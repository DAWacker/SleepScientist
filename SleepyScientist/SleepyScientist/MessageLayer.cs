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
    /**
     * Handles displaying and updating Messages.
     */
    static class MessageLayer
    {
        private static Dictionary<string, Message> _messages = new Dictionary<string, Message>();
        private static SpriteFont _spriteFont;

        /**
         * Add a Message to _messages using its text as the key.
         * @param message The Message to be added.
         * @param allowOverwrite Can a repeat Message be added?
         */
        public static void AddMessage(Message message, bool allowOverwrite = true )
        {
            if (allowOverwrite)
                _messages[message.Text] = message;
            else
            {
                if (!_messages.ContainsKey(message.Text))
                    _messages.Add(message.Text, message);
            }
        }

        /**
         * Draw all the messages to the screen.
         * @param spriteBatch <p>The SpriteBatch to use.
         * assumes that its Begin method was called prior to this method.
         */
        public static void Draw(SpriteBatch spriteBatch)
        {
            foreach (Message message in MessageLayer.Messages)
            {
                // Reposition the message if going to be drawn off-screen.
                Vector2 dimensions = _spriteFont.MeasureString(message.Text);
                if (dimensions.X + message.X > GameConstants.SCREEN_WIDTH)
                {
                    message.X = GameConstants.SCREEN_WIDTH - (int)dimensions.X;
                }
                else if (message.X < 0)
                {
                    message.X = 0;
                }
                if (dimensions.Y + message.Y > GameConstants.SCREEN_HEIGHT)
                {
                    message.Y = GameConstants.SCREEN_HEIGHT - (int)dimensions.Y;
                }
                else if (message.Y < 0)
                {
                    message.Y = 0;
                }

                // Draw the message.
                spriteBatch.DrawString(_spriteFont, message.Text, new Vector2(message.X, message.Y), Color.White);
            }
        }

        /**
         * Remove message so it doesn't get displayed anymore.
         * @param message The Message to be removed.
         */
        public static void RemoveMessage(Message message)
        {
            _messages.Remove(message.Text);
        }

        /// <summary>
        /// Clear all messages.
        /// </summary>
        public static void ClearMessages()
        {
            _messages.Clear();
        }

        /**
         * Update all the Messages and remove those that expire.
         * @param time The time to update the Messages by.
         */
        public static void Update(double time)
        {
            List<Message> toRemove = new List<Message>();

            foreach (Message message in _messages.Values)
            {
                if (message.Update(time))
                    toRemove.Add(message);
            }

            foreach ( Message message in toRemove )
                RemoveMessage(message);
        }

        // Getters and Setters
        public static IEnumerable<Message> Messages { get { return _messages.Values; } }
        public static SpriteFont Font { get { return _spriteFont; } set { _spriteFont = value; } }
    }
}
