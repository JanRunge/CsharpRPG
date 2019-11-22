using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using STory.Types;

namespace STory.GameContent.Items.Armors
{
    public class Greaves :Armor
    {
        
        public Greaves(Dictionary<DamageType, float> Armorvals, int weight, int worth, string name) : base(Armorvals, Armortype.Legs, weight, worth, name)
        {

        }

        public Greaves(float slashArmor, float bluntarmor, float pokearmor, int weight, int worth, string name) : base(slashArmor, bluntarmor, pokearmor, Armortype.Legs, weight, worth, name)
        {
        }
    }
}
