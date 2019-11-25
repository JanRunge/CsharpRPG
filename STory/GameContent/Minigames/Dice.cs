using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace STory.GameContent.Minigames
{
    public class Dice
    {
        int val;
        public int roll()
        {
            Random r = new Random();
            val = r.Next(1, 6);
            return val;
        }
        public int GetValue()
        {
            return val;
        }
    }
}
