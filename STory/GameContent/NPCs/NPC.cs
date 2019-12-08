using STory.Handlers.Fight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using STory.Types;
using STory.GameContent.Items;
using STory.GameContent.Items.Armors;
using STory.Handlers.Option;

namespace STory.GameContent
{
    public class NPC : Attackable
    {
        public float health;
        public Boolean alive = true;
        public string name;
        public Faction faction;
        protected Inventory inventory = new Inventory();
        protected int gold = 10;

        public NPC() { }
        public NPC(string name, Faction f):this(name,
            new Helmet(0.02f, 0.02f, 0.02f, 5, 10, "Leather Helmet"),
            new Cuirass(0.02f, 0.02f, 0.02f, 5, 10, "Leather Cuirass"),
            new Gloves(0.02f, 0.02f, 0.02f, 5, 10, "Leather Gloves"),
            new Greaves(0.02f, 0.02f, 0.02f, 5, 10, "Leather Greaves"),
            new Boots(0.02f, 0.02f, 0.02f, 5, 10, "Leather Boots"),
            new Weapon(10, DamageType.Slash, 10, 10, "sword name", "Sword"),
            f
            )
        {

        }
        public NPC(string name, Helmet h, Cuirass c, Gloves g, Greaves gg, Boots b, Weapon w, Faction f)
        {
            this.name = name;
            this.health = 100;
            void equip(Armor a)
            {
                if (a != null)
                {
                    inventory.Equip(a);
                }
            }
            equip(h); equip(c); equip(g); equip(gg); equip(b);
            if (w != null)
            {
                equipWeapon(w);
            }
            this.faction = f;

        }
        private void equipWeapon(Weapon w)
        {
            inventory.Equip(w);
        }
        public void ClearInventory()
        {
            foreach (Item i in this.inventory.GetAllItems())
            {
                inventory.RemoveItem(i);
            }
        }
        public void AddToInventory(List<Item> items)
        {
            inventory.AddItems(items);
        }

        /// <summary>
        /// Open a user dialog which allows th user navigate through the NPCs Inventory and take Items
        /// </summary>
        public void loot()
        {
            if (!this.inventory.ContainsAnyItems())
            {
                CIO.Print(this.name + "'s inventory is empty");
                return;
            }
            GenericOption takeAllOption = new GenericOption("take all");
            Optionhandler oh = new Optionhandler(this.name + "'s inventory ",true);
            Option selectedOption=null;
            //the user stays inside the inventory until all items are gon or he picks exit
            while (selectedOption != Optionhandler.Exit && this.inventory.ContainsAnyItems())
            {
                List<Item> allitems = this.inventory.GetAllItems();
                oh.ClearOptions();
                oh.AddOptions(Optionhandler.ItemToOption(allitems));
                oh.AddOption(takeAllOption);
                selectedOption = oh.selectOption();
                if (selectedOption == takeAllOption)
                {
                    foreach (Item i in allitems)
                    {
                        Inventory.transferItem(this.inventory, Program.player.inventory, i);
                    }
                }
                else
                {
                    Inventory.transferItem(this.inventory, Program.player.inventory, (Item)selectedOption);
                }
            }
        }
        /// <summary>
        /// get the total damagereduction against that damagetype
        /// </summary>
        public float getDefense(DamageType d)
        {
            return inventory.GetArmorset().getDamageBlock(d);
        }
        public string getName() {
            return this.name;
        }
        /// <summary>
        /// Attack the NPC with damage amoutn + type. Removes the health after substraction blocked damage
        /// </summary>
        public void receiveDamage(int amount, DamageType type)
        {
            float dmg;
            dmg= (float)Math.Round( amount - amount* getDefense(type));
            this.health -= dmg;
            CIO.Print(this.name + " lost " + dmg + " HP. " + health + "HP remaining");
            if (health <= 0)
            {
                this.alive = false;
            }
        }
        /// <summary>
        /// Attack the target with the equipped weapon
        /// </summary>
        public void attack(Attackable target)
        {
            target.receiveDamage(inventory.GetEquippedWeapon().damage, inventory.GetEquippedWeapon().damagetype);
        }
        public bool isAlive()
        {
            return this.alive;
        }
        /// <summary>
        /// returns the amount of experience points the Unit sets free when dying
        /// </summary>
        public int XPOnDeath()
        {
            return 10;
        }
        public void RemoveGold(int amnt)
        {
            this.gold -= amnt;
        }
        public int GetGold()
        {
            return this.gold;
        }
        public void AddGold(int amnt)
        {
            this.gold+=amnt;
        }
        public bool HasGold(int amnt)
        {
            return amnt <= gold;
        }
    }
}
