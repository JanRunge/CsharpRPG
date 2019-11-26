using STory.GameContent.Items;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace STory.GameContent.NPCs
{
    
    class Merchant: NPC
    {
        public Merchant()
        {
            
            this.inventory.AddItem(new Item(1, 10, "Itemone", "Weapons"));
            this.inventory.AddItem(new Item(1, 50, "Itemedfe", "Armor"));
            this.inventory.AddItem(new Item(1, 20, "Itescne", "Misc"));
            this.inventory.AddItem(new Item(1, 234, "Itemefne", "Armor"));
            this.inventory.AddItem(new Item(1, 111, "Imone", "Misc"));
            this.inventory.AddItem(new Item(1, 4, "Ignmone", "Weapons"));

        }
        public void OpenInventory()
        {
            GenericOption Category()
            {
                printHeader();
                Optionhandler OH = new Optionhandler("pick a category", true);
                OH.setName("Inventory.Category");

                foreach (string i in inventory.GetCategories())
                {
                    GenericOption GO = new GenericOption(i);
                    OH.AddOption(GO);
                }
                GenericOption selected = (GenericOption)OH.selectOption();
                Console.Clear();
                return selected;
            }
            void Itempicker(string category)
            {
                Option selected = null;
                while (selected != Optionhandler.Exit)
                {
                    printHeader();
                    Optionhandler OH = new Optionhandler(true);
                    OH.setName("Inventory.Item");
                    
                    foreach (Item i in inventory.GetAllItems(category))
                    {
                        OH.AddOption(i);//todo: find a way to make it read if not enough gold
                    }
                    
                    selected = OH.selectOption();
                    if (Item.isItem(selected))
                    {
                        this.Buy((Item)selected);
                    }
                    
                    Console.Clear();
                }
            }
            void printHeader()
            {
                Console.Clear();
                CIO.Print("########Inventory:########");
                CIO.Print(this.getGold() + "g");
            }
            while (true)
            {
                GenericOption g = Category();
                if (g == Optionhandler.Exit)
                {
                    return;
                }
                Itempicker(g.name);//hier kann nur eine Exit-option rauskommen
            }
        }
        public void Buy(Item i)
        {
            Program.player.removeGold((int) (i.worth * 1.1));
            Inventory.transferItem(this.inventory, Program.player.inventory, i);
            this.addGold((int)(i.worth * 1.1));
        }
        public void addGold(int amnt)
        {
            this.gold += amnt;
        }
    }
}
