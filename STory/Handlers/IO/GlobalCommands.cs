﻿using STory.GameContent;
using STory.GameContent.Items.Weapons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using STory.Handlers.Fight;
using STory.GameContent.Items.Armors;

namespace STory
{
    public static class GlobalCommands
    {
        private static Dictionary<string, List<Action>> commands = new Dictionary<string, List<Action>>();
        public static void GiveGold()
        {
            CIO.StartNewContext(new Handlers.IO.Context("How much?"));
            int amount = -1;
            while (amount == -1)
            {
                Console.WriteLine("How much?");
                string input = CIO.ReadLine();
                if (wannaExit(input))
                {
                    return;
                }
                Int32.TryParse(input, out amount);
            }
            Program.player.AddGold(amount);
            CIO.EndContextWithoutReEnter();

        }
        public static void GiveXP()
        {
            int amount = -1;
            while (amount == -1)
            {
                Console.WriteLine("How much?");
                string input = CIO.ReadLine();
                if (wannaExit(input))
                {
                    return;
                }
                Int32.TryParse(input, out amount);
            }
            Program.player.GiveXP(amount);
        }
        public static void GiveSword()
        {
            Program.player.giveItem(new Sword(10, 10, 30, "kauldruns knife"));
            Helmet he = new Helmet(0.2f, 0.2f, 0.2f, 10, 10, "Helgen helmet");
            Program.player.giveItem(he);

        }
        public static void Attack() {
            Console.WriteLine("Which NPC?");
            string input = CIO.ReadLine();
            if (wannaExit(input))
            {
                return;
            }
            Attackable enemy = Program.currentRoom.getNPCWithName(input);
            Fight f = new Fight(enemy);
            f.startFight();
        }
        public static void help() {
            if(CIO.getCurrentContext().name== "Inventory.Category")
            {
                CIO.PrintHelp("help for Inventory");
            }
            else
            {
                
                CIO.PrintHelp("type i for inventory");
                CIO.PrintHelp("type Attack to attack an NPC");

            }


        }

        public static void Loot()
        {
            CIO.StartNewContext(new Handlers.IO.Context("Loot who?"));
            NPC target = null;
            while (target == null)
            {
                Console.WriteLine("Loot who?");
                string input = CIO.ReadLine();
                if (wannaExit(input))
                {
                    return;
                }
                foreach(NPC npc in Program.currentRoom.NPCs)
                {
                    if (npc.name == input){

                    }
                }
            }
            CIO.EndContextWithoutReEnter();
        }

        private static bool wannaExit(string input){
            return input.ToLower() == "e";
        }
        public static Boolean existsCheat(string s){
            return commands.ContainsKey(s);
        }
        public static void executeCheat(string s){
            if(!commands.ContainsKey(s)){
                return;
            }
            Console.ForegroundColor= ConsoleColor.White;
            
            foreach (Action a in commands[s])
            {
                a();
            }
            CIO.ReEnterCurrentContext();
            Console.ForegroundColor= ConsoleColor.Blue;
        }


        public static void LoadAll()
        {
            void AddCheat(string s, Action a)
            {
                s = s.ToLower();
                if (commands.ContainsKey(s))
                {
                    commands[s].Add(a);
                }
                else
                {
                    commands.Add(s, new List<Action>());
                    commands[s].Add(a);
                }
            }
            AddCheat("give gold", () => GlobalCommands.GiveGold());
            AddCheat("i", () => Program.player.OpenInventory());
            AddCheat("Attack", () => Attack());
            AddCheat("h", () => help());
            AddCheat("help", () => help());
            AddCheat("give sword", () => GiveSword());
            AddCheat("give xp", () => GiveXP());
        }
    }
}
