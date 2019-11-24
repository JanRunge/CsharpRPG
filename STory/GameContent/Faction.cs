using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace STory.GameContent
{
    public class Faction
    {
        string name;
        int bounty = 0;

        public Faction(string s)
        {
            name = s;
        }
        public static Faction bandits = new Faction("Bandit");
        public static Faction warriors = new Faction("Warriors");

        public static List<Faction> GetAllFactions()
        {
            return new List<Faction> { bandits, warriors };
        }

        public int getBounty()
        {
            return bounty;
        }
        public void raiseBounty(int amount)
        {
            bounty += amount;
        }
        public void lowerBounty(int amount)
        {
            bounty -= amount;
        }

    }
}
