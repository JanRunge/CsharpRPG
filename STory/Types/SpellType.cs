using STory.Handlers.Option;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace STory.Types
{
    public class SpellType : Option
    {
        string name;
        public SpellType(string name)
        {
            this.name = name;
        }
        public string getName()
        {
            return name;
        }

        public string getText()
        {
            return getName();
        }

        public ConsoleColor GetColor()
        {
            return CIO.defaultcolor;
        }

        public bool isAvailable()
        {
            return true;
        }

        public string getPreferredCommand()
        {
            return null;
        }

        public void Select()
        {
            
        }

        public static SpellType DamageSpell = new SpellType("DamageSpell");
    }
}
