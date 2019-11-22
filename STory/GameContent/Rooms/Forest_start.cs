using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace STory.GameContent.Rooms
{
    class Forest_start : Room
    {
        public Forest_start()
        {
            this.name = "Forest";
            this.nextRooms.Clear();
        }
        public override bool OnEnter()
        {
            
            AddRoom(typeof(Town));
            // AddRoom(typeof(Cave));
            AddRoom(typeof(Banditcamp));
            base.OnEnter();
            return true;
            

        }
        
        public override void OnExit()
        {
            base.OnExit();
        }

    }
}
