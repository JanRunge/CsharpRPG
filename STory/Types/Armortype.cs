using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace STory.Types
{
    public class Armortype
    {
        string description;

        public Armortype(string s)
        {
            description = s;
        }
        //todo make this strings
        public static Armortype Head = new Armortype("Head");
        public static Armortype Hands = new Armortype("Hands");
        public static Armortype Feet = new Armortype("Feet");
        public static Armortype Torso = new Armortype("Torso");
        public static Armortype Legs = new Armortype("Legs");
        public static List<Armortype> GetAllArmortypes()
        {
            return new List<Armortype> { Head, Hands, Feet, Torso, Legs };
        }


    }
}
