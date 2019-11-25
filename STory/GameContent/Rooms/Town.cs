using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace STory.GameContent.Rooms
{
    class Town : Room
    {
        public Town()
        {
            this.name = "town";
            this.nextRooms.Clear();

        }
        public override Boolean OnEnter()
        {
            base.OnEnter();
            
            AddRoom(typeof(Forest_start));
            AddRoom(typeof(Palast));
            AddRoom(typeof(Tavern));
            AddRoom(this.LastRoom);


            CIO.PrintStory("Welcome to Flavortown!!");
            CIO.PrintStory("A Huge palace overshadows the City");
            return true;

        }
        public override void OnExit()
        {
            base.OnExit();
        }
    }
}
