using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace STory
{
    public class Item : Option
    {
        
        public int weight;
        public int worth;
        public string name;
        protected string category;
        public bool sellable;
        protected void init(int weight,  int worth, string name)
        {
            this.weight = weight;
            this.worth = worth;
            this.name = name;
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

        public string getText()
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

    }
}
