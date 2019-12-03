using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace STory.Types
{
    public class Potionsize
    {
        string description;
        public int weight;
        public float defaultMultiplicator;

        Potionsize(string desc, int weight, float defaultMultiplicator)
        {
            // if an effect doesnt provide its own multiplicators, the defaultmultiplicator is used.
            this.description = desc;
            this.weight = weight;
            this.defaultMultiplicator = defaultMultiplicator;
        }
        public static Potionsize Tiny = new Potionsize("Tiny",1,0.5f);
        public static Potionsize Small = new Potionsize("Small",1,0.7f);
        public static Potionsize Medium = new Potionsize("Medium",2,1f);
        public static Potionsize Large = new Potionsize("Large",2,1.5f);
        public static Potionsize Huge = new Potionsize("Huge",3,2f);

        public static List<Potionsize> AllSizes()
        {
            return new List<Potionsize> { Tiny, Small, Medium, Large, Huge };
        }
    }
}
