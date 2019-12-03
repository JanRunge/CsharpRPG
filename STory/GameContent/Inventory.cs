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
     /// <summary>
     /// Manages All Items and the equipped armor of a Player or NPC
     /// </summary>
    public class Inventory
    {
        //Items are Organized in a dictionary:
        //Each Item-Category has its own entry with a list with all Items of that category
        /// <summary>
        /// All Items in this inventory per Itemcategory
        /// </summary>
        Dictionary<string, List<Item>> itemDictionary = new Dictionary<string, List<Item>>();
        /// <summary>
        /// The CUrrently equipped armor
        /// </summary>
        Armorset armorset = new Armorset();
        /// <summary>
        /// total weight of the inventory
        /// </summary>
        public int weight;
        /// <summary>
        /// get the Currently equipped armorset
        /// </summary>
        public Armorset GetArmorset()
        {
            return armorset;
        }
        /// <summary>
        /// return all Items regardless of category
        /// </summary>
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
        /// <summary>
        /// return all Items in a category
        /// </summary>
        public List<Item> GetAllItems(string category)
        {
            if (!itemDictionary.ContainsKey(category))
            {
                return new List<Item>();
            }
            return itemDictionary[category];
        }
        /// <summary>
        /// return all categories for which an Item exists in this Inventory 
        /// </summary>
        public List<string> GetCategories()
        {
            List<string> r = new List<string>();
            foreach (KeyValuePair<string, List<Item>> kv in itemDictionary){
                r.Add(kv.Key);
            }
            return r;
        }
        /// <summary>
        /// add Items to the inventory
        /// </summary>
        public void AddItems(List<Item> input)
        {
            foreach (Item i in input)
            {
                AddItem(i);
            }
        }
        /// <summary>
        /// add an Item to the inventory
        /// </summary>
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
        /// <summary>
        /// Removes an Item From the inventory.
        /// <para>the Item gets Unequipped if it is equipped</para>
        /// </summary>
        public void RemoveItem(Item input)
        {
            //unequip if equipped
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
            if(itemDictionary[input.GetCategory()].Count==0){ //delete the category entry if there is no item in it
                itemDictionary.Remove(input.GetCategory());
            }
        }
        /// <summary>
        /// Remove an Item FROM one Inventory and add it TO another
        /// </summary>
        public static void transferItem(Inventory from, Inventory to, Item item){
            from.RemoveItem(item);
            to.AddItem(item);
        }
        /// <summary>
        /// checks if the inventory contains the Item
        /// </summary>
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
        /// <summary>
        /// checks if the inventory contains any Items
        /// </summary>
        public Boolean ContainsAnyItems()
        {
            return ! (this.itemDictionary.Count == 0);
        }
        /// <summary>
        /// equip a piece of Armor
        /// </summary>
        public void Equip(Armor a)
        {
            if (!this.ContainsItem(a))
            {
                this.AddItem(a);
            }
            armorset.Equip(a);
        }
        /// <summary>
        /// Unequip a piece of Armor
        /// </summary>
        public void Unequip(Armor a)
        {
             armorset.Unequip(a);
        }
        /// <summary>
        /// Open the Inventory to navigate within it. The Actions defined within Items are NOT available
        /// <para>
        /// getsCommand is a function which takes an Item and returns wether the Item should receive a/many Command/s or not
        /// </para>
        /// <para>onItemSelection is an Action which  is executed with the selected Item</para>
        /// </summary>
        public void Open(Func<Item, bool> getsCommand, Action<Item> onItemSelection)
        {
            Open(getsCommand, onItemSelection, null, null);
        }
        /// <summary>
        /// Open the Inventory to navigate within it. The Actions defined within Items are NOT available
        /// <para>
        /// getsCommand is a function which takes an Item and returns wether the Item should receive a/many Command/s or not
        /// </para>
        /// <para>onItemSelection is an Action which  is executed with the selected Item</para>
        /// <para>isAvailable is a FUnc which determines wether the item is available or not. It is appended to each Option</para>
        /// <para>UnavailableMessage is a Func which retruns the string to be printed when the Optionw as selected & unavailable</para>
        /// </summary>
        public void Open(Func<Item, bool> getsCommand, Action<Item> onItemSelection, Func<Item, bool> isAvailable, Func<Item, string> UnavailableMessage)
        {
            Open(getsCommand, onItemSelection, isAvailable, UnavailableMessage, false);
        }
        /// <summary>
        /// Open the Inventory to navigate within it.
        /// <para>
        /// pick true to generate all Commands for all Items  or false to get No Actions
        /// </summary>
        public void Open(bool allowItemActions)
        {
            Open((i)=>true, null, null, null, true);
        }
        /// <summary>
        /// Open the Inventory to navigate within it.
        /// <para>
        /// getsCommand is a function which takes an Item and returns wether the Item should receive a/many Command/s or not
        /// </para>
        /// <para>onItemSelection is an Action which  is executed with the selected Item</para>
        /// <para>isAvailable is a FUnc which determines wether the item is available or not. It is appended to each Option</para>
        /// <para>UnavailableMessage is a Func which retruns the string to be printed when the Optionw as selected & unavailable</para>
        /// <para>if allowItemActions = false, no Item Actions are allowed</para>
        /// </summary>
        public void Open(Func<Item,bool> getsCommand, Action<Item> onItemSelection, Func<Item, bool> isAvailable, Func<Item, string> UnavailableMessage,bool allowItemActions)
        {
            while (true)
            {
                GenericOption g = PickCategory();
                if (g == Optionhandler.Exit)
                {
                    return;
                }
                NavigateItems(g.name,getsCommand,onItemSelection,isAvailable,UnavailableMessage,allowItemActions);//hier kann nur eine Exit-option rauskommen
                
            }
        }
        /// <summary>
        /// Open the Inventory for a category to navigate within it and execute Actions on the Items.
        /// <para>
        /// getsCommand is a function which takes an Item and returns wether the Item should receive a/many Command/s or not
        /// </para>
        /// <para>onItemSelection is an Action which  is executed with the selected Item</para>
        /// <para>isAvailable is a FUnc which determines wether the item is available or not. It is appended to each Option</para>
        /// <para>UnavailableMessage is a Func which retruns the string to be printed when the Optionw as selected & unavailable</para>
        /// <para>if allowItemActions = false, no Item Actions are allowed</para>
        /// </summary>
        void NavigateItems(string category, Func<Item, bool> getsCommand, Action<Item> onItemSelection, Func<Item, bool> isAvailable, Func<Item, string> UnavailableMessage, bool allowItemActions)
        {
            Option selected = null;
            while (selected != Optionhandler.Exit)
            {
                List<Item> Items = this.GetAllItems(category);
                if (Items.Count == 0)
                {
                    return;
                }
                printHeader();
                Optionhandler OH = new Optionhandler(true);
                OH.setName("Inventory.Item");
                foreach (Item i in Items)
                {
                    if (allowItemActions == false)
                    {
                        if (getsCommand(i))
                        {
                            //Options which are available on the Item are defined on the Item and wrapped by the GenericItemOption
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
                selected = OH.selectOption(false);
                if (selected.GetType() == typeof(GenericItemOption))
                {
                    onItemSelection(((GenericItemOption)selected).getItem());
                }
            }
        }
        /// <summary>
        /// Open the Inventory to pick a category.
        /// </summary>
        protected GenericOption PickCategory()
        {
            printHeader();
            Optionhandler OH = new Optionhandler("pick a category", true);
            OH.setName("Inventory.Category");

            foreach (string i in this.GetCategories())
            {
                GenericOption GO = new GenericOption(i);
                OH.AddOption(GO);
            }
            GenericOption selected = (GenericOption)OH.selectOption(false);
            
            return selected;
        }
        /// <summary>
        /// Print the heading for the inventory
        /// </summary>
        protected void printHeader()
        {
            CIO.Clear();
            CIO.Print("########Inventory:########");
            //CIO.Print(this.getGold() + "gold");//todo
        }


    }
}
