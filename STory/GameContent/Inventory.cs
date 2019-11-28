using STory.GameContent.Items;
using STory.GameContent.Items.Armors;
using STory.Handlers.Option;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace STory.GameContent
{
    public class Inventory
    {
        Dictionary<string, List<Item>> itemDictionary = new Dictionary<string, List<Item>>();
        Armorset armorset = new Armorset();

        public int weight;
        public Armorset GetArmorset()
        {
            return armorset;
        }
        public List<Item> GetAllItems()
        {
            List<Item> r = new List<Item>();
            foreach (KeyValuePair<string, List<Item>> kv in itemDictionary){
                foreach (Item i in kv.Value)
                {
                    r.Add(i);
                }
            }
            return r;
        }
        public List<Item> GetAllItems(string category)
        {
            if (!itemDictionary.ContainsKey(category))
            {
                return new List<Item>();
            }
            return itemDictionary[category];
        }
        public List<string> GetCategories()
        {
            List<string> r = new List<string>();
            foreach (KeyValuePair<string, List<Item>> kv in itemDictionary){
                r.Add(kv.Key);
            }
            return r;
        }
        public void AddItems(List<Item> input)
        {
            foreach (Item i in input)
            {
                AddItem(i);
            }
        }
        public void AddItem(Item input)
        {
            if (!itemDictionary.ContainsKey(input.GetCategory()))
            {
                itemDictionary.Add(input.GetCategory(), new List<Item>());
            }
            if (!itemDictionary[input.GetCategory()].Contains(input))
            {
                itemDictionary[input.GetCategory()].Add(input);
                this.weight+=input.weight;
            }
            
        }
        public void RemoveItem(Item input)
        {
            if (Items.Armor.isArmor(input)){
                this.armorset.Unequip((Items.Armor)input);
            }
            if (!itemDictionary.ContainsKey(input.GetCategory()))
            {
                return;
            }
            if(itemDictionary[input.GetCategory()].Contains(input)){
                this.weight-=input.weight;
                itemDictionary[input.GetCategory()].Remove(input);
            }
            if(itemDictionary[input.GetCategory()].Count==0){
                itemDictionary.Remove(input.GetCategory());
            }
        }
        public static void transferItem(Inventory from, Inventory to, Item item){
            from.RemoveItem(item);
            to.AddItem(item);
        }
        public Boolean ContainsItem(Item i)
        {
            if (this.itemDictionary.ContainsKey(i.GetCategory())){
                return itemDictionary[i.GetCategory()].Contains(i);
            }
            else
            {
                return false;
            }

        }
        public Boolean ContainsAnyItems()
        {
            return ! (this.itemDictionary.Count == 0);
        }

        public void Equip(Armor a)
        {
            if (!this.ContainsItem(a))
            {
                this.AddItem(a);
            }
            armorset.Equip(a);
        }
        public void Unequip(Armor a)
        {
             armorset.Unequip(a);
        }
        public void Open(Func<Item, bool> getsCommand, Action<Item> onItemSelection)
        {
            Open(getsCommand, onItemSelection, null, null);
        }
        public void Open(Func<Item, bool> getsCommand, Action<Item> onItemSelection, Func<Item, bool> isAvailable, Func<Item, string> UnavailableMessage)
        {
            Open(getsCommand, onItemSelection, null, null,false);
        }
        public void Open(bool allowItemActions)
        {
            Open((i)=>true, null, null, null, true);
        }

        public void Open(Func<Item,bool> getsCommand, Action<Item> onItemSelection, Func<Item, bool> isAvailable, Func<Item, string> UnavailableMessage,bool allowItemActions)
        {
            GenericOption Category()
            {
                printHeader();
                Optionhandler OH = new Optionhandler("pick a category", true);
                OH.setName("Inventory.Category");

                foreach (string i in this.GetCategories())
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
                    List<Item> Items = this.GetAllItems(category);
                    if (Items.Count == 0)
                    {
                        return;
                    }
                    foreach (Item i in Items)
                    {
                        if (allowItemActions == false)
                        {
                            if (getsCommand(i))
                            {
                                GenericItemOption opt = new GenericItemOption(i);
                                if (isAvailable != null)
                                {
                                    Func<bool> f = () => isAvailable(i);
                                    opt.SetAvailable(f);
                                }
                                if (UnavailableMessage != null)
                                {
                                    Func<string> ff = () => UnavailableMessage(i);
                                    opt.setNotAvailable(ff);
                                }
                                OH.AddOption(opt);
                            }
                            else
                            {
                                CIO.Print(i.getDescription());
                            }
                        }
                        else
                        {
                            GenericItemOption opt = new GenericItemOption(i);
                            OH.AddOption(opt);
                        }
                        
                    }

                    selected = OH.selectOption();
                    if (selected.GetType()==typeof(GenericItemOption))
                    {
                        onItemSelection(((GenericItemOption)selected).getItem());
                    }

                    Console.Clear();
                }
            }
            void printHeader()
            {
                Console.Clear();
                CIO.Print("########Inventory:########");
                //CIO.Print(this.getGold() + "g");//todo
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



    }
}
