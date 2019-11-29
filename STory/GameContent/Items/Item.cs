using STory.Handlers.Option;
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
        protected List<GenericOption> actions ;//things you can do with this item in your inventory

        public Item(int weight,  int worth, string name,string category)
        {
            this.weight = weight;
            this.worth = worth;
            this.name = name;
            this.category = category;
            actions = new List<GenericOption> { new GenericOption("Drop", () => Program.player.inventory.RemoveItem(this)) };
        }
        //todo: refactor all these getText functions
        public virtual string getDescription()
        {
            //description for 
            string s = this.name + " " + weight + "kg " + worth + "g";
            return s;
        }
        public virtual string AppendWeightAndWorth(string s)
        {
            s += " "+weight + "kg " + worth + "g";
            return s;
        }

        public virtual string getText()
        {//from the Option interface
            return getDescription();
        }

        public virtual ConsoleColor GetColor()
        {
            return CIO.defaultcolor;
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
        /// <summary>
        /// Returns all Actions which are aviable on that Item in the Playerinventory
        /// </summary>
        public List<GenericOption> getOptions()
        {
            return this.actions;
        }

    }
}
