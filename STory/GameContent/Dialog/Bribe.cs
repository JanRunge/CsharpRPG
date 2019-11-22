using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace STory
{
    public class Bribe : GenericOption
    {
        public Bribe(int Cost, Action a, float chance):base("Bribe (" + Cost + "g)"
                                                            )
        {
            this.AddExecutionAction(a);
            this.SetAvailable(() => Program.player.hasGold(Cost));
            this.AddExecutionAction(() => Program.player.removeGold(Cost));
        }
        public override void onNotAvailable()
        {
            Console.WriteLine("Not Enough money");
        }
        public override void onFailure()
        {
            Console.WriteLine("the guard doesnt accept your bribe");
        }
        public void tryBribe(float chance)
        {
            Random r = new Random();
            double d = r.NextDouble();
            if (!( d <= chance))
            {
                available = ()=>false;
            }
        }
    }
}
