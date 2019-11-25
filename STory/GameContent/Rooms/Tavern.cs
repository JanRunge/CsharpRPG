using STory.GameContent.Minigames;
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

            AddRoom(typeof(Town));
            Option selected=null;
            while (selected != Optionhandler.Exit)
            {
                Optionhandler handler = new Optionhandler(true);
                handler.AddOption(new Dicegame());
                selected = handler.selectOption();
            }
            return true;

        }
    }
}
