using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using STory.Types;

namespace STory.GameContent.Items.Armors
{
    public class Helmet : Armor
    {
        
        public Helmet(Dictionary<DamageType, float> Armorvals, int weight, int worth, string name) : base(Armorvals, Armortype.Head, weight, worth, name)
        {
            type = Armortype.Head;
        }

        public Helmet(float slashArmor, float bluntarmor, float pokearmor, int weight, int worth, string name) : base(slashArmor, bluntarmor, pokearmor, Armortype.Head, weight, worth, name)
        {
        }
    }
}
