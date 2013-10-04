using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SleepyScientist
{
    /**
     * Handles displaying and updating Messages.
     */
    class MessageLayer
    {
        private static Dictionary<string, Message> _messages = new Dictionary<string, Message>();

        public MessageLayer() { }

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
         * Remove message so it doesn't get displayed anymore.
         * @param message The Message to be removed.
         */
        public static void RemoveMessage(Message message)
        {
            _messages.Remove(message.Text);
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
    }
}
