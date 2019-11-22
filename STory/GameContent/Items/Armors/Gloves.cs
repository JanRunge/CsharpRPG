using STory.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace STory.GameContent.Items.Armors
{
    public class Gloves : Armor
    {
        public Gloves(Dictionary<DamageType, float> Armorvals, int weight, int worth, string name) : base(Armorvals, Armortype.Hands, weight, worth, name)
        {

        }

        public Gloves(float slashArmor, float bluntarmor, float pokearmor, int weight, int worth, string name) : base(slashArmor, bluntarmor, pokearmor, Armortype.Hands, weight, worth, name)
        {
        }
    }
}
