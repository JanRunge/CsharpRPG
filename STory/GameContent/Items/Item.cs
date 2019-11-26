using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace STory.GameContent.Items
{
    public class Item : Option
    {
        
        public int weight;
        public int worth;
        public string name;
        protected string category;
        public bool sellable;
        public Item(int weight,  int worth, string name,string category)
        {
            this.weight = weight;
            this.worth = worth;
            this.name = name;
            this.category = category;
        }
        public virtual string getDescription()
        {
            string s = this.name + " " + weight + "kg " + worth + "g";
            return s;
        }
        public virtual string getText(string s)
        {
            s += " "+weight + "kg " + worth + "g";
            return s;
        }

        public virtual string getText()
        {
            return this.name+ " " + weight + "kg " + worth + "g";
        }

        public virtual ConsoleColor? GetColor()
        {
            return null;
        }

        public bool isAvailable()
        {
            return true;
        }

        public string getPreferredCommand()
        {
            return null;
        }
        public void Select() { }
        public String GetCategory()
        {
            return this.category;
        }
        public static bool isItem(Option o)
        {
            return o.GetType().Namespace.Contains("Items");
        }

    }
}
