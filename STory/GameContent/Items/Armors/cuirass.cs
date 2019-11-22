using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using STory.Types;

namespace STory.GameContent.Items.Armors
{
    public class Cuirass : Armor
    {
        public Cuirass(Dictionary<DamageType, float> Armorvals, int weight, int worth, string name) : base(Armorvals, Armortype.Torso, weight, worth, name)
        {

        }

        public Cuirass(float slashArmor, float bluntarmor, float pokearmor, int weight, int worth, string name) : base(slashArmor, bluntarmor, pokearmor, Armortype.Torso, weight, worth, name)
        {
        }
    }
}
