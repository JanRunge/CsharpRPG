using STory.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using STory.Handlers.Fight;
using STory.GameContent.Items;
using STory.GameContent.Items.Armors;

namespace STory.GameContent
{
    public class Player : Attackable
    {
        int gold;
        public int level=1;
        public int XP=0;
        public Items.Weapon Weapon;


        public float health = 100;
        public int Intelligence = 20;
        public int Strength = 20;

        public Inventory inventory = new Inventory();
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
            STory.GameContent.Items.Weapon w = new STory.GameContent.Items.Weapon(0,DamageType.Blunt,0,1,"Fists","Fists");
            inventory.AddItem(w);
        }
        public Boolean hasGold(int amount)
        {
            return this.gold >= amount;
        }
        public int getGold()
        {
            return gold;
        }
        public Boolean removeGold(int amnt)
        {
            if (hasGold(amnt))
            {
                this.gold = this.gold - amnt;
                return true;
            }
            return false;
        }
        public void AddGold(int amnt)
        {
            this.gold += amnt;
            Console.WriteLine("Received " + amnt + " gold");
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
                this.Strength += 1;

            }else if (result == oIntelligence)
            {
                this.Intelligence += 1;
            }
        }
        public void OpenInventory()
        {
            GenericOption Category(){
                printHeader();
                Optionhandler OH = new Optionhandler("pick a category",true);
                OH.setName("Inventory.Category");

                foreach (string i in inventory.GetCategories())
                {
                    GenericOption GO = new GenericOption(i);
                    OH.AddOption(GO);
                }
                GenericOption selected = (GenericOption) OH.selectOption();
                Console.Clear();
                return selected;
            }
            void Item(string category){
                Option selected=null;
                while (selected != Optionhandler.Exit)
                {
                    printHeader();
                    Optionhandler OH = new Optionhandler(true);
                    OH.setName("Inventory.Item");
                    if (category == "Armor")
                    {
                        foreach (Item i in inventory.GetAllItems(category))
                        {
                            OH.AddOption(i);
                        }
                    }
                    else
                    {
                        foreach (Item i in inventory.GetAllItems(category))
                        {
                            CIO.Print(i.getDescription());
                        }
                    }
                    selected = (Option)OH.selectOption();
                    if (Armor.isArmor(selected))
                    {
                        Armor a = (Armor)selected;
                        if (a.IsEquipped())
                        {
                            this.unequipArmor(a);
                        }
                        else
                        {
                            this.EquipArmor(a);
                        }
                        
                    }
                    Console.Clear();
                }
            }
            void printHeader(){
                Console.Clear();
                CIO.Print("########Inventory:########");
                CIO.Print(this.getGold() + "g");
            }
            while (true){
                GenericOption g = Category();
                if(g== Optionhandler.Exit)
                {
                    return;
                }
                Item(g.name);//hier kann nur eine Exit-option rauskommen
            }
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
            float dmg= Weapon.damage;
            dmg = dmg * getDamageMultiplicator(Weapon.damagetype);
            a.receiveDamage((int)Math.Floor(dmg) , Weapon.damagetype);
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
    }
}
