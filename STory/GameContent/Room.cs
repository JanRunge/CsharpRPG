using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using STory.GameContent;
using STory.Handlers.Fight;
using STory.Handlers.Option;

namespace STory
{
    public class Room :  Option
    {
        public static Dictionary<Type, Room> AllRooms = new Dictionary<Type, Room>();

        //todo: implement rooms as singletons?
        public string name;
        public Room LastRoom;
        public bool thrownToNewRoom = false;
        public List<NPC> NPCs;
        public List<GenericOption> ActionsInRoom = new List<GenericOption>();
        
        
        public List<Room> nextRooms = new List<Room>();
        public List<Option> ActivitiesInRoom;
        protected void addActivity(Option activity)
        {
            if (ActivitiesInRoom == null)
            {
                ActivitiesInRoom = new List<Option>();
            }
            ActivitiesInRoom.Add(activity);
        }
        protected void addNPC(NPC newNPC)
        {
            if (NPCs == null)
            {
                NPCs = new List<NPC>();
            }
            NPCs.Add(newNPC);
        }
        /// <summary>
        /// This function gets called when the Room is Entered
        /// <para> The magic stuff happens here: sub classes implement their story in this Func</para>
        /// </summary>
        public virtual Boolean OnEnter()
        {
            Console.WriteLine("you entered " + name);
           
            return true;
        }
        /// <summary>
        /// This function gets called when the Room is Exited
        /// </summary>
        public virtual void OnExit()
        {
            Console.WriteLine("you left " + name);
        }
        public void PrintNextOptions()
        {
            Console.WriteLine("The next Areas:");
            int i = 0;
            Console.ForegroundColor= ConsoleColor.Yellow;
            foreach (Room r in this.nextRooms)
            {
                Console.WriteLine(r.name+"["+i+"]");
                i++;
            }
            Console.ForegroundColor= ConsoleColor.White;

        }
        /// <summary>
        /// Add a Room by its type to which can be walked from this room
        /// </summary>
        public void AddRoom(Type t)
        {
            Room r;
            if (!AllRooms.ContainsKey(t))
            {
                r = (Room)Activator.CreateInstance(t);//create instance from type
                AllRooms.Add(t, r);
            }
            else
            {
                r = AllRooms[t];
            }

            AddRoom(r);
        }
        /// <summary>
        /// Add a Room to which can be walked from this room
        /// </summary>
        public void AddRoom(Room r)
        {
            if (!nextRooms.Contains(r))
            {
                this.nextRooms.Add(r);
            }
            r.LastRoom = this;
        }
        public NPC getNPCByName(string s){
            foreach(NPC a in this.NPCs){
                if (a.getName()==s){
                    return a;
                }
            }
            return null;
        }
        public string getText(){
            return this.name;
        }
        public bool isAvailalbe(string _s){
            return true;
        }
        public ConsoleColor GetColor(){
            return CIO.defaultcolor;
        }
        public bool isAvailable()
        {
            return true;
        }

        public string getPreferredCommand()
        {
            return null;
        }
        public void Select() { }
    }
}
    