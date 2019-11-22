using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using STory.Handlers.Fight;
using STory.Types;
namespace STory.GameContent.Items.Weapons
{
    public class Sword : Weapon
    {
        
        public Sword(int weight, 
                    int worth, 
                    int damage,
                    string name) : base(weight,
                    DamageType.Slash,
                    worth,
                    damage,
                    name,
                    "Sword")
        {
            
        }
    }
}
