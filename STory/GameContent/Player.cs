using STory.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using STory.Handlers.Fight;
using STory.GameContent.Items;
using STory.GameContent.Items.Armors;
using STory.Handlers.Option;
using STory.GameContent.NPCs;

namespace STory.GameContent
{
    public class Player : Attackable
    {
        public int level=1;
        public int XP=0;

        public float health = 100;
        int MaxHealth = 100;
        int Intelligence = 20;
        int Strength = 20;
        public int MaxCarryWeight = 200;

        public Inventory inventory = new Inventory();

        public void IncreaseStrength(int amount)
        {
            Strength += amount;
            MaxCarryWeight += amount * 10;
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
        public float getDamageMultiplicator(DamageType type)
        {
            if (type.isStrengthBased())
            {
                return 1+(0.01f * (Strength - 20));
            }
            else
            {
                return 1+(0.01f * (Intelligence - 20));
            }
        }
        public Player(){
            inventory.AddItem(new STory.GameContent.Items.Weapon(0, DamageType.Blunt, 0, 1, "Fists", "Fists"));
        }
        public Boolean hasGold(int amount)
        {
            return this.inventory.hasGold(amount);
        }
        public int getGold()
        {
            return this.inventory.getGold();
        }
        public Boolean removeGold(int amnt)
        {
            return this.inventory.removeGold(amnt);
            
        }
        public void AddGold(int amnt)
        {
            this.inventory.AddGold(amnt);
            CIO.Print("Received " + amnt + " gold");
        }
        public void giveItem(Item i)
        {
            this.inventory.AddItem(i);
            CIO.Print("you received '" + i.name + "'");
        }
        public void RemoveItem(Item i)
        {
            this.inventory.RemoveItem(i);            
            CIO.Print("removed '" + i.name + "'");
        }
        
        public void GiveXP(int amount)
        {
            CIO.Print("received " + amount + "XP");
            XP += amount;
            if (Level.XPToLevel(this.level + 1, this) <= 0)
            {
                levelUp();
            }

        }
        private void levelUp()
        {
            this.level++;
            CIO.Print("Reached level "+level);
            this.health += (int)Math.Floor( health * 0.02);
            GenericOption oStrength=new GenericOption("Strength");
            GenericOption oIntelligence=new GenericOption("Intelligence");
            Optionhandler oh = new Optionhandler("Choose an Attribute to upgrade ");
            oh.AddOption(oStrength);
            oh.AddOption(oIntelligence);
            Option result = (Option)oh.selectOption();
           
            if (result== oStrength)
            {
                this.IncreaseStrength(1);

            }else if (result == oIntelligence)
            {
                this.IncreaseIntelligence(1);
            }
        }
        public void OpenInventory()
        {
            void equipIfArmor(Item i)
            {
                if (Armor.isArmor(i))
                {
                    this.EquipArmor((Armor) i);
                }
            }
            Func<Item, bool> f = i => i.GetCategory() == "Armor";
            Action<Item> a = i => equipIfArmor(i);
            this.inventory.Open(true);
        }
        public void OpenInventoryForTrade(Merchant m)
        {
            Func<Item, bool> selectable = i => true;
            Action<Item> onselect = i => m.SellTo(i);
            Func<Item, bool> available = i => m.HasGold( (int) (i.worth * 0.9));//todo make 0.9 a var/dynamic
            Func<Item, string> onNotavailable = i => "the merhcant doesnt have enough gold";
            this.inventory.Open(selectable, onselect,available,onNotavailable);
        }
        public void EquipArmor(Armor a)
        {
            inventory.Equip(a);
        }
        public void unequipArmor(Armor i)
        {
            inventory.Unequip(i);
        }
        public void receiveDamage(int amount, DamageType type)
        {
            float dmg;

            Dictionary<DamageType, float> d = inventory.GetArmorset().getDamageBlock();
            
            dmg = amount -(d[type]*amount);
            this.health -= dmg;
            CIO.Print(" You lost " + dmg + " HP. " + health + "HP remain");
            if (health <= 0)
            {
                Program.gameOver();
            }
        }
        public void attack(Attackable a)
        {
            Weapon w = this.inventory.GetEquippedWeapon();
            float dmg= w.damage;
            dmg = dmg * getDamageMultiplicator(w.damagetype);
            a.receiveDamage((int)Math.Floor(dmg) , w.damagetype);
        }
        public string getName()
        {
            return "player";

        }

        public bool isAlive()
        {
            return true;
        }

        public int XPOnDeath()
        {
            throw new NotImplementedException();//this function is unused
        }

        public void RestoreHealth(float amnt)
        {
            CIO.Print("restored "+Math.Min(amnt, MaxHealth - health)+ "health");
            this.health = Math.Min(MaxHealth, this.health + amnt);
        }
    }
}
