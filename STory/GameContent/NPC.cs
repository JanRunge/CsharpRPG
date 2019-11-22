using STory.Handlers.Fight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using STory.Types;
using STory.GameContent.Items;
using STory.GameContent.Items.Armors;

namespace STory.GameContent
{
    public class NPC : Attackable
    {
        public float health;
        public Boolean alive = true;
        public string name;
        
        public Inventory inventory = new Inventory();
        Weapon weapon;

        public NPC() { }
        public NPC(string name)
        {
            this.name = name;
            this.health = 100;

            inventory.Equip(new Helmet( 0.02f, 0.02f, 0.02f, 5, 10, "Leather Helmet"));
            inventory.Equip(new Cuirass(0.02f, 0.02f, 0.02f, 5, 10, "Leather Cuirass"));
            inventory.Equip(new Gloves( 0.02f, 0.02f, 0.02f, 5, 10, "Leather Gloves"));
            inventory.Equip(new Greaves(0.02f, 0.02f, 0.02f, 5, 10, "Leather Greaves"));
            inventory.Equip(new Boots(  0.02f, 0.02f, 0.02f, 5, 10, "Leather Boots"));
            equipWeapon(new Weapon(10, DamageType.Slash, 10, 10, "sword name", "Sword"));


        }
        public NPC(Helmet h, Cuirass c, Gloves g, Greaves gg, Boots b, Weapon w)
        {
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


        }
        private void equipWeapon(Weapon w)
        {
            if (!inventory.ContainsItem(w))
            {
                inventory.AddItem(w);
            }
            this.weapon = w;
        }
        public void ClearInventory()
        {
            foreach (Item i in this.inventory.GetAllItems())
            {
                inventory.RemoveItem(i);
            }
        }
        public void FillInventory(List<Item> items)
        {
            inventory.AddItems(items);
        }


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
            while (selectedOption != Optionhandler.Exit && this.inventory.ContainsAnyItems())
            {
                List<Item> allitems = this.inventory.GetAllItems();
                oh.clearOptions();
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
        public float getDefense(DamageType d)
        {
            return inventory.GetArmorset().getDamageBlock(d);
        }
        public string getName() {
            return this.name;

        }
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
        public void attack(Attackable target)
        {
            target.receiveDamage(weapon.damage, weapon.damagetype);
        }
        public bool isAlive()
        {
            return this.alive;
        }
        public int XPOnDeath()
        {
            return 10;
        }
    }
}
