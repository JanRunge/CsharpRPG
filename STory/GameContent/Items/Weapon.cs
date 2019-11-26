using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using STory.Handlers.Option;
using STory.Types;

namespace STory.GameContent.Items
{
    public class Weapon : Item , Option
    {
        public int damage;
        public DamageType damagetype;
        public string Type;

        public Weapon(int weight, DamageType damagetype, int worth, int damage, string name, string type) : base(weight, worth,name,"Weapons")
        {
            this.damage = damage;
            this.damagetype = damagetype;
        }
        
        public override string getDescription()//for inventory
        {
            return getText(name + "(" + this.damagetype.description + ")");
        }
        public static List<Weapon> ItemsToWeapons(List<Item> i)
        {
            List<Weapon> l = new List<Weapon>();
            foreach (Item r in i)
            {
                l.Add((Weapon)r);
            }
            return l;
        }

        public override string getText()//for fights
        {
            return this.name + " (" + this.damage + " " + this.damagetype.description + "damage)";
        }
    }
}
