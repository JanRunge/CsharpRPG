using STory.GameContent.Items;
using STory.Handlers.Option;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace STory.GameContent.NPCs
{
    /// <summary>
    /// An NPC which can Trade with the player
    /// </summary>
    public class Merchant: NPC
    {
        public Merchant()
        {
            
            this.inventory.AddItem(new Item(1, 10, "Itemone", "Weapons"));
            this.inventory.AddItem(new Item(1, 50, "Itemedfe", "Armor"));
            this.inventory.AddItem(new Item(1, 20, "Itescne", "Misc"));
            this.inventory.AddItem(new Item(1, 234, "Itemefne", "Armor"));
            this.inventory.AddItem(new Item(1, 111, "Imone", "Misc"));
            this.inventory.AddItem(new Item(1, 4, "Ignmone", "Weapons"));

        }
        /// <summary>
        /// Opens a Dialog for the user to buy items from the merchant.
        /// </summary>
        public void OpenInventoryForTrade()
        {
            Func<Item, bool> f = i => true;
            Action<Item> a = i => BuyFrom(i);
            this.inventory.Open(f, a);
        }
        /// <summary>
        /// Opens a Dialog for the user to trade with the merchant. The user can choose between selling and buying
        /// </summary>
        public void trade()
        {
            Option selectedOption=null;
            Optionhandler oh = new Optionhandler("trade with "+this.name,true);
            GenericOption opt = new GenericOption("Buy");
            opt.AddExecutionAction(this.OpenInventoryForTrade);
            oh.AddOption(opt);
            opt = new GenericOption("Sell");
            opt.AddExecutionAction(()=>Player.getInstance().OpenInventoryForTrade(this));
            oh.AddOption(opt);

            while (selectedOption != Optionhandler.Exit)
            {
                selectedOption= oh.selectOption();
            }
        }
        /// <summary>
        /// Buy an Item from the merchant
        /// </summary>
        public void BuyFrom(Item i)
        {
            Player.getInstance().removeGold((int) (i.worth * 1.1));
            Inventory.transferItem(this.inventory, Player.getInstance().inventory, i);
            this.AddGold((int)(i.worth * 1.1));
        }
        /// <summary>
        /// Sell an Item to the merchant
        /// </summary>
        public void SellTo(Item i)
        {
            Player.getInstance().AddGold((int)(i.worth *0.9));
            Inventory.transferItem(Player.getInstance().inventory, this.inventory, i);
            this.removeGold((int)(i.worth * 0.9));
        }
        
    }
}
