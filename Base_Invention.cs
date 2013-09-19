using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sleepy_Scientist_Files
{
    class Base_Invention
    {
        // Name of the invention (maybe this can just be derived from the type name)
        string name;
        // Number of uses allotted to this invention per level
        int numUses = 1;
        // Is the invention currently equipped? (being dragged)
        bool equipped = false;
        // Is the invention currently activated?
        bool activated = false;

        public static Base_Invention operator + (Base_Invention first, Base_Invention second)
        {
            Base_Invention combined = new Base_Invention();
            combined.name = first.name + second.name;
            combined.numUses = first.numUses + second.numUses;
            return combined;
        }

        public void Use()
        {
        }

        public Base_Invention Combine(Base_Invention other)
        {
            return (this + other);
        }
    }
}
