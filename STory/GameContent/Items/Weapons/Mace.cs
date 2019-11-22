using STory.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace STory.GameContent.Items.Weapons
{
    class Mace :Weapon
    {
        public Mace(int weight,
                    int worth,
                    int damage,
                    string name) : base(weight,
                    DamageType.Blunt,
                    worth,
                    damage,
                    name,
                    "Mace")
        {

        }
    }
}
