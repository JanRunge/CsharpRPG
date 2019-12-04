using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace STory.Handlers.Option
{
    /// <summary>
    /// an special Option-Object which contains Multiple Options within itself.
    /// this way multiple Options can be linked to each other
    /// </summary>
    class Multioption : Option
    {
        /// <summary>
        /// all Options which are linked in this option with their keys
        /// Options are solely added by the optionhandler
        /// </summary>
        public Dictionary<string, Option> options = new Dictionary<string, Option>();
        public string Prefix;
        public Func<ConsoleColor> PrefixColor;

        public ConsoleColor GetColor()
        {
            return PrefixColor();
        }

        public string getPreferredCommand()
        {
            throw new InvalidOperationException("should never be called on this object, but rather on the sub-options");
        }

        public string getText()
        {
            throw new InvalidOperationException("should never be called on this object, but rather on the sub-options");
        }

        public bool isAvailable()
        {
            throw new InvalidOperationException("should never be called on this object, but rather on the sub-options");
        }
        public bool isAvailable(string command)
        {
            return options[command].isAvailable();
        }

        public void Select()
        {
            throw new InvalidOperationException("should never be called on this object, but rather on the sub-options");
        }
        public Option Select(string command)
        {
            Option selected= options[command];
            selected.Select();
            return selected;
        }
    }
}
