using STory.GameContent.Items;
using STory.GameContent.Items.Armors;
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

        


    }
}
