using STory.GameContent.Items;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace STory.Handlers.Option
{
    public class GenericItemOption : GenericOption
    {
        Item item;
        public GenericItemOption(Item i):base(i.getDescription())
        {
            item = i;
        }
        public Item getItem()
        {
            return item;
        }
    }
}
