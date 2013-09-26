using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SleepyScientist
{
    class Invention : GameObject
    {
        #region Attributes

        // Name of the invention
        private string _name;

        // Number of uses allotted to this invention per level
        private int _uses;

        // Is the invention currently equipped? (being dragged)
        private bool _equipped;

        // Is the invention currently activated?
        private bool _active;

        #endregion
    
        #region Properties

        // Get or set the name of the invention
        public string Name { get { return _name; } set { _name = value; } }

        // Get or set the number of uses of the invention
        public int Uses { get { return _uses; } set { _uses = value; } }

        // Get or set if the invention is currently equipped
        public bool Equipped { get { return _equipped; } set { _equipped = value; } }

        // Get or set if the invention is currently activated
        public bool Active { get { return _active; } set { _active = value; } }

        #endregion

        #region Constructor

        public Invention(string name, int uses, int x, int y, int width, int height)
            : base(x, y, width, height)
        {
            _name = name;
            _uses = uses;
            _equipped = false;
            _active = false;
        }

        #endregion

        public static Invention operator + (Invention first, Invention second)
        {
            Invention combined = new Invention();
            combined.name = first.name + second.name;
            combined.numUses = first.numUses + second.numUses;
            return combined;
        }

        public void Use()
        {
        }

        public Invention Combine(Invention other)
        {
            return (this + other);
        }
    }
}
