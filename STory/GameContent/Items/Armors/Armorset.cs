using STory.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace STory.GameContent.Items.Armors
{
    public class Armorset
    {
        private Dictionary<Armortype, Armor> Armors = 
            new Dictionary<Armortype, Armor>
            {   {Armortype.Head,null},
                {Armortype.Torso,null },
                {Armortype.Hands,null },
                {Armortype.Legs,null },
                {Armortype.Feet,null },
            };
        Dictionary<DamageType, float> Damageblock = 
            new Dictionary<DamageType, float>
            {   {DamageType.Blunt,0 },
                {DamageType.Poke, 0 },
                {DamageType.Slash, 0 }
            };
        public void Equip(Armor a)
        {
            Unequip(Armors[a.getArmortype()]);

            this.Armors[a.getArmortype()] = a;
            a.equip();
            foreach (DamageType d in DamageType.allDamageTypes())
            {
                increaseDamageBlock(d, a.getArmor(d));
            }
        }
        private void increaseDamageBlock(DamageType d, float a)
        {
            if (Damageblock.ContainsKey(d))
            {
                Damageblock[d] += a ;
            }
            else
            {
                Damageblock[d] = a;
            }

            if (Damageblock[d] <= 0)
            {
                Damageblock.Remove(d);
            }
        }
        public void Unequip(Armor i)
        {
            if (i == null)
            {
                return;
            }
            Armortype key = null;
            //check if it even exists in the set
            foreach (KeyValuePair<Armortype, Armor> a in this.Armors)
            {
                if (a.Value == i)
                {
                    key = a.Key;
                    break;
                }
            }
            //then remove
            if (key != null)
            {
                i.unequip();
                Armors.Remove(key);
                foreach (DamageType d in DamageType.allDamageTypes())
                {
                    increaseDamageBlock(d, -i.getArmor(d));
                }
            }
        }
        public Dictionary<DamageType, float> getDamageBlock()
        {
            return Damageblock;
        }
        public float getDamageBlock(DamageType d)
        {
            if (Damageblock.ContainsKey(d))
            {
                return Damageblock[d];
            }
            else
            {
                return 0;
            }
            
        }
        public List<Armor> GetAllItems()
        {
            List<Armor> returnval = new List<Armor>();
            foreach (KeyValuePair<Armortype, Armor> a in this.Armors)
            {
                returnval.Add(a.Value);
            }
            return returnval;
        }
        public Armor getItem(Armortype a)
        {
            return this.Armors[a];
        }
    }
}
