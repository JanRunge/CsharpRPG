using STory.GameContent.Items;
using STory.Handlers.Option;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace STory.GameContent.NPCs
{
    
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
        public void OpenInventoryForTrade()
        {
            Func<Item, bool> f = i => true;
            Action<Item> a = i => BuyFrom(i);
            this.inventory.Open(f, a);
        }
        public void trade()
        {
            Option selectedOption=null;
            Optionhandler oh = new Optionhandler("trade with "+this.name,true);
            GenericOption opt = new GenericOption("Buy");
            opt.AddExecutionAction(this.OpenInventoryForTrade);
            oh.AddOption(opt);
            opt = new GenericOption("Sell");
            opt.AddExecutionAction(()=>Program.player.OpenInventoryForTrade(this));
            oh.AddOption(opt);

            while (selectedOption != Optionhandler.Exit)
            {
                selectedOption= oh.selectOption();
            }

            
        }
        public void BuyFrom(Item i)
        {
            Program.player.removeGold((int) (i.worth * 1.1));
            Inventory.transferItem(this.inventory, Program.player.inventory, i);
            this.AddGold((int)(i.worth * 1.1));
        }
        public void SellTo(Item i)
        {
            Program.player.AddGold((int)(i.worth *0.9));
            Inventory.transferItem(Program.player.inventory, this.inventory, i);
            this.RemoveGold((int)(i.worth * 0.9));
        }
        
    }
}
