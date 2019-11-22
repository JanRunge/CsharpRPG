using STory.GameContent;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace STory
{
    public class Level
    {
        
        public static int XPToLevel(int level, Player p)
        {
            int requiredXP = 500;
            for (int i=2; i < level; i++)
            {
                requiredXP = (int)Math.Floor(requiredXP * 1.2);
            }
            return requiredXP-p.XP;
        }
        public static int XPToNextLevel(Player p)
        {
            return XPToLevel(p.level + 1, p);
        }
    }
}
