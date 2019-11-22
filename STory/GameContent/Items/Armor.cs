﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using STory.Types;

namespace STory.GameContent.Items
{
    public abstract class Armor : Item
    {
        Dictionary<DamageType, float> armorvals;
        protected Armortype type;
        Boolean equipped = false;
        public Armor(Dictionary<DamageType, float> Armorvals, Armortype t, int weight, int worth, string name)
        {
            this.armorvals = Armorvals;
            type = t;
            this.category = "Armor";
            this.weight = weight;
            this.worth = worth;
            this.name = name;

        }
        public Armor(float slashArmor, float bluntarmor,float pokearmor, Armortype t, int weight, int worth, string name)
            : this(new Dictionary<DamageType, float> {  { DamageType.Slash, slashArmor },
                                                        { DamageType.Poke, pokearmor },
                                                        { DamageType.Blunt, bluntarmor },
                                                     }
                   ,t , weight, worth, name
            )
        {
        }
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
        public override ConsoleColor? GetColor()
        {
            //todo: change color depending on wether the item is better than the equipped one
            if (equipped)
            {
                return ConsoleColor.DarkYellow;
            }
            else
            {
                return null;
            }
        }
        public Boolean IsEquipped()
        {
            return this.equipped;
        }
        public Boolean isBetterThan(Armor a)
        {
            return false; //TODO
        }
    }
}