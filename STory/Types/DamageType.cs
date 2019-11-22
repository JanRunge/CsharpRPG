using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace STory.Types
{
    public class DamageType
    {
        public string description;
        private DamageType(string s)
        {
            this.description = s;
        }
        //todo make this strings
        public static DamageType Blunt = new DamageType("Blunt");
        public static DamageType Slash = new DamageType("Slash");
        public static DamageType Poke = new DamageType("Poke");
        public static List<DamageType> allDamageTypes()
        {
            return new List<DamageType> { Blunt, Slash, Poke };
        }
    }
}
