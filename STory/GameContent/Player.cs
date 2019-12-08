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
    public class Player : Character //todo: implement as Singleton
    {
        public int level=1;
        public int XP=0;

        public int MaxCarryWeight = 200;

        public Player(){
            inventory.AddItem(new STory.GameContent.Items.Weapon(0, DamageType.Blunt, 0, 1, "Fists", "Fists"));
            this.name = "player";
        }
        override public Boolean removeGold(int amnt)
        {
            CIO.Print("removed " + amnt + " gold");
            return base.removeGold(amnt);
        }
        override public void AddGold(int amnt)
        {
            CIO.Print("Received " + amnt + " gold");
            base.AddGold(amnt);
        }
        override public void giveItem(Item i)
        {
            CIO.Print("you received '" + i.name + "'");
            base.giveItem(i);
            
        }
        override public void RemoveItem(Item i)
        {
            CIO.Print("removed '" + i.name + "'");
            base.RemoveItem(i);

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
            Func<Item, bool> available = i => m.hasGold( (int) (i.worth * 0.9));//todo make 0.9 a var/dynamic
            Func<Item, string> onNotavailable = i => "the merhcant doesnt have enough gold";
            this.inventory.Open(selectable, onselect,available,onNotavailable);
        }
        public void OpenSpellbook()
        {
            this.spellBook.Open();
        }
        override public int receiveDamage(int amount, DamageType type)
        {
            int dmg = base.receiveDamage(amount, type);
            CIO.Print(" You lost " + dmg + " HP. " + health + "HP remain");
            if (health <= 0)
            {
                Program.gameOver();
            }
            return dmg;
        }

        override public bool isAlive()
        {
            return true;
        }

        override public int XPOnDeath()
        {
            throw new NotImplementedException();//this function is unused
        }

        override public int RestoreHealth(float amnt)
        {
            int restoredamnt = base.RestoreHealth(amnt);
            CIO.Print("restored "+ restoredamnt + "health");
            return restoredamnt;
        }
        override public void RemoveMana(int amount)
        {
            base.RemoveMana(amount);
            CIO.Print("removed " + amount + "mana");
        }

    }
}
