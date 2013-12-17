using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SleepyScientist
{
    /**
     * Information to be displayed to the player.
     */
    class Message
    {
        string _text;
        double _timeRemaining;
        int _x;
        int _y;

        /**
         * Create a new Message.
         * @param text The Message's message.
         * @param timeRemaining How long the Message should be displayed for. If not specified, the message will be displayed forever.
         */
        public Message(String text, int x, int y, double timeRemaining = double.MinValue)
        {
            _text = text;
            _timeRemaining = timeRemaining;
            _x = x;
            _y = y;
        }

        /**
         * Update Message.
         * @param time The amount of time to decrease _timeRemaining by.
         * @return Has the Message expired?
         */
        public bool Update(double time)
        {
            if (_timeRemaining != double.MinValue)
                _timeRemaining -= time;

            return _timeRemaining <= 0 && _timeRemaining != double.MinValue;
        }

        // Getters and Setters.
        public string Text { get { return _text; } set { _text = value; } }
        public int X { get { return _x; } set { _x = value; } }
        public int Y { get { return _y; } set { _y = value; } }
    }
}
