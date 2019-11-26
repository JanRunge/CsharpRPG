using STory.GameContent.Minigames;
using STory.GameContent.NPCs;
using STory.Handlers.Option;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace STory.GameContent.Rooms
{
    public class Tavern :Room
    {
        public Tavern()
        {
            this.name = "Tavern";
            this.nextRooms.Clear();

        }
        public override Boolean OnEnter()
        {
            base.OnEnter();
            Merchant merchant = new Merchant();
            addNPC(merchant );
            AddRoom(typeof(Town));

            addActivity(new Dicegame());
            addActivity(new GenericOption("trade with merchant",merchant.trade));

            return true;

        }
    }
}
