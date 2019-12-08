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
        bool strengthbased;


        private DamageType(string s,bool strengthbased)
        {
            this.description = s;
            this.strengthbased = strengthbased;
        }
        public bool  isStrengthBased()
        {
            return strengthbased;
        }
        public bool isIntelligenceBased()
        {
            return !strengthbased;
        }
        public static DamageType Blunt = new DamageType("Blunt",true);
        public static DamageType Slash = new DamageType("Slash",true);
        public static DamageType Poke = new DamageType("Poke",true);
        public static DamageType Fire = new DamageType("Fire", false);
        public static List<DamageType> allDamageTypes()
        {
            return new List<DamageType> { Blunt, Slash, Poke };
        }
    }
}
