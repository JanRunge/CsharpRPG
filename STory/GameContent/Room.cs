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
        /// <summary>
        /// Add an Option to the Room which will be choosable when the OnEnter method completes
        /// </summary>
        protected void addActivity(Option activity)
        {
            if (ActivitiesInRoom == null)
            {
                ActivitiesInRoom = new List<Option>();
            }
            ActivitiesInRoom.Add(activity);
        }
        /// <summary>
        /// Add an NPC to the Room
        /// </summary>
        protected void addNPC(NPC newNPC)
        {
            if (NPCs == null)
            {
                NPCs = new List<NPC>();
            }
            NPCs.Add(newNPC);
        }
        /// <summary>
        /// Print the enter-message
        /// </summary>
        public virtual Boolean OnEnter()
        {
            
            /// This function gets called when the Room is Entered
            /// The magic stuff happens here: sub classes implement their story in this Func
            Console.WriteLine("you entered " + name);
           
            return true;
        }
        /// <summary>
        /// Print the leave-message
        /// </summary>
        public virtual void OnExit()
        {// This function gets called when the Room is Exited
            Console.WriteLine("you left " + name);
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
        /// <summary>
        /// Get the NPC inside the Room which has the name
        /// <para> returns null if none is found </para>
        /// </summary>
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
    