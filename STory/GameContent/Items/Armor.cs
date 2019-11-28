using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using STory.Types;

namespace STory.GameContent.Items
{
    /// <summary>
    /// a piece of armor which can be equipped by the player or NPCs
    /// </summary>
    public abstract class Armor : Item
    {
        
        protected Armortype type;
        Dictionary<DamageType, float> armorvals;
        Boolean equipped = false;

        public Armor(Dictionary<DamageType, float> Armorvals, Armortype t, int weight, int worth, string name): base(weight, worth, name, "Armor")
        {
            this.armorvals = Armorvals;
            type = t;
            actions.Add(new GenericOption("equip", () => Program.player.EquipArmor(this)));

        }
        public Armor(float slashArmor, float bluntarmor, float pokearmor, Armortype t, int weight, int worth, string name)
            : this(new Dictionary<DamageType, float> {  { DamageType.Slash, slashArmor },
                                                        { DamageType.Poke, pokearmor },
                                                        { DamageType.Blunt, bluntarmor },
                                                     }
                   ,t , weight, worth, name)
        {
        }
        /// <summary>
        /// returns the damagereduction for the given damagetype
        /// </summary>
        public float getArmor(DamageType d)
        {
            if (this.armorvals.ContainsKey(d))
            {
                return armorvals[d];
            }
            else
            {
                return 0;
            }
        }
        
        public Armortype getArmortype()
        {
            return this.type;
        }
        public static Boolean isArmor(object o)
        {
            return o.GetType().Namespace.Contains("Armor");
        }
        public void equip()
        {
            equipped = true;
        }
        public void unequip()
        {
            equipped = false;
        }
        public override ConsoleColor GetColor()
        {
            if (equipped)
            {
                return ConsoleColor.DarkYellow;
            }
            else if(this.IsBetterThan(Program.player.inventory.GetArmorset().getItem(this.getArmortype())))
            {
                return ConsoleColor.Green;
            }
            else
            {
                return CIO.defaultcolor;
            }
        }
        public Boolean IsEquipped()
        {
            return this.equipped;
        }
        /// <summary>
        /// Checks if the Armorpiec is better  (has a higher total of Damagereduction) than the param
        /// </summary>
        public Boolean IsBetterThan(Armor a)
        {
            if (a == null)
            {
                return true;
            }
            float sumThis=0;
            float sumOther=0;
            foreach(DamageType d in DamageType.allDamageTypes())
            {
                sumThis += this.getArmor(d);
                sumOther += a.getArmor(d);
            }
            return sumThis > sumOther;
        }
    }
}
