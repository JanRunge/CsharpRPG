using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace STory.Handlers.Option
{
    class Multioption : Option
    {
        public Dictionary<string, Option> options = new Dictionary<string, Option>();
        public string Prefix;

        public ConsoleColor GetColor()
        {
            throw new NotImplementedException();
        }

        public string getPreferredCommand()
        {
            throw new NotImplementedException();
        }

        public string getText()
        {
            throw new NotImplementedException();
        }

        public bool isAvailable()
        {
            throw new NotImplementedException();
        }
        public bool isAvailable(string command)
        {
            return options[command].isAvailable();
        }

        public void Select()
        {
            throw new NotImplementedException();
        }
        public Option Select(string command)
        {
            Option selected= options[command];
            selected.Select();
            return selected;

        }
    }
}
