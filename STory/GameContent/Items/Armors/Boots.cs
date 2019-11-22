using STory.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace STory.GameContent.Items.Armors
{
    public class Boots : Armor
    {
        public Boots(Dictionary<DamageType, float> Armorvals, int weight, int worth, string name) : base(Armorvals, Armortype.Feet, weight, worth, name)
        {

        }

        public Boots(float slashArmor, float bluntarmor, float pokearmor, int weight, int worth, string name) : base(slashArmor, bluntarmor, pokearmor, Armortype.Feet, weight, worth, name)
        {
        }
    }
}
