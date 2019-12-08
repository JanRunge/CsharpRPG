using STory.GameContent.Items;
using STory.GameContent.Spells;
using STory.Handlers.Fight;
using STory.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace STory.GameContent
{
    public abstract class Character : Attackable
    {
        protected int MaxHealth = 100;
        public float health = 100;
        protected int MaxMana = 100;
        protected int Mana =100;

        protected int Intelligence = 20;
        protected int Strength = 20;

        protected string name;

        public Inventory inventory = new Inventory();

        protected SpellBook spellBook = new SpellBook();


        public void IncreaseStrength(int amount)
        {
            Strength += amount;
        }
        public int GetStrength()
        {
            return Strength;
        }
        public void IncreaseIntelligence(int amount)
        {
            Intelligence += amount;
        }
        public int GetIntelligence()
        {
            return Intelligence;
        }

        public void UnequipWeapon()
        {
            this.inventory.UnequipWeapon();
        }
        public void Equip(Weapon a)
        {
            this.inventory.Equip(a);
        }

        public void LearnSpell(Spell s)
        {
            //todo: check for required intelligence
            this.spellBook.LearnSpell(s);
        }
        public void EquipSpell(Spell s)
        {
            this.spellBook.EquipSpell(s);
        }


        public Boolean hasGold(int amount)
        {
            return this.inventory.hasGold(amount);
        }
        public int getGold()
        {
            return this.inventory.getGold();
        }
        public virtual Boolean removeGold(int amnt)
        {
            return this.inventory.removeGold(amnt);

        }
        public virtual void AddGold(int amnt)
        {
            this.inventory.AddGold(amnt);
        }
        public virtual void giveItem(Item i)
        {
            this.inventory.AddItem(i);
        }
        public virtual void giveItems(List<Item> items)
        {
            foreach(Item i in items)
            {
                giveItem(i);
            }
        }

        public virtual void RemoveItem(Item i)
        {
            this.inventory.RemoveItem(i);
        }
        public void EquipArmor(Armor a)
        {
            inventory.Equip(a);
        }
        public void unequipArmor(Armor i)
        {
            inventory.Unequip(i);
        }
        /// <summary>
        /// get the total damagereduction against that damagetype
        /// </summary>
        public float getDefense(DamageType d)
        {
            return inventory.GetArmorset().getDamageBlock(d);
        }
        public virtual int receiveDamage(int amount, DamageType type)
        {
            int dmg;
            dmg = (int)Math.Floor(amount - (getDefense(type) * amount));
            this.health -= dmg;
            return dmg;
        }
        /// <summary>
        /// Attack the target with the equipped weapon
        /// </summary>
        public void attack(Attackable a)
        {
            Weapon w = this.inventory.GetEquippedWeapon();
            float dmg = w.getDamage(this.Strength,this.Intelligence);
            a.receiveDamage((int)Math.Floor(dmg), w.damagetype);
        }
        public virtual string getName()
        {
            return name;
        }
        public abstract bool isAlive();
        /// <summary>
        /// returns the amount of experience points the Unit sets free when dying
        /// </summary>
        public abstract int XPOnDeath();
        public virtual int RestoreHealth(float amnt)
        {
            int restoredAmnt = (int)Math.Min(amnt, MaxHealth - health);
            this.health += restoredAmnt;
            return restoredAmnt;
        }

        public virtual void RemoveMana(int amount)
        {
            this.Mana -= amount;
        }
        public bool HasMana(int amount)
        {
            return Mana >= amount;
        }
        public List<Spell> getAllSpells()
        {
            return this.spellBook.getAllSpells();
        }
        public Dictionary<SpellType, List<Spell>> getSpells()
        {
            return spellBook.getSpells();
        }
        public List<Spell> getSpells(SpellType t)
        {
            return this.spellBook.getSpells(t);
        }
    }
}
