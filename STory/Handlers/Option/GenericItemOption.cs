using STory.GameContent.Items;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace STory.Handlers.Option
{
    /// <summary>
    /// Wraps Actions that can be executed in an Item for the Inventory class
    /// </summary>
    public class GenericItemOption : GenericOption
    {
        Item item;
        public GenericItemOption(Item i) : base(i.getDescription())
        {
            item = i;
        }
        public Item getItem()
        {
            return item;
        }
        /// <summary>
        /// Get All available Options for the Item
        /// </summary>
        public List<GenericOption> getOptions()
        {
            return item.getOptions();
        }
        public string NameOfItem()
        {
            return item.name;
        }
    }
}