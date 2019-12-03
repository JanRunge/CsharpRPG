using STory.Handlers.Option;
using STory.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace STory.GameContent.Items
{
    class Potion : Item, Option
    {
        PotionEffect Effect;
        Potionsize size;
        public Potion(int worth, string name, PotionEffect Effect, Potionsize size) : base(size.weight, worth, name, "Potion")
        {
            this.Effect = Effect;
            this.size = size;
            actions.Add(new GenericOption("Drink", () => Drink()));
        }
        public void Drink()
        {//todo: set a breakpoint here to see the context switches causing problems
            Effect.startEffect(this.size);
            Program.player.RemoveItem(this);
        }

    }
}
