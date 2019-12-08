using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace STory.Types
{
    public class SpellType
    {
        string name;
        public SpellType(string name)
        {
            this.name = name;
        }

        public static SpellType DamageSpell = new SpellType("DamageSpell");
    }
}
