using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace STory
{
    /// <summary>
    /// A Specialized GenericOption. Provide a Cost, an action what should be done when succeeded, and chance to succeed, the class wil do the rest
    /// </summary>
    public class Bribe : GenericOption
    {
        public Bribe(int Cost, Action a, float chance):base("Bribe (" + Cost + "g)")
        {
            this.AddTryAction(() => tryBribe(chance));
            this.AddExecutionAction(a);
            this.SetAvailable(() => Program.player.hasGold(Cost));
            this.AddExecutionAction(() => Program.player.removeGold(Cost));
        }
        public override void onNotAvailable()
        {
            CIO.Print("Not Enough money");
        }
        public override void onFailure()
        {
            CIO.Print("the guard doesnt accept your bribe");
        }
        public void tryBribe(float chance)
        {
            Random r = new Random();
            double d = r.NextDouble();
            if (!( d <= chance))
            {
                available = ()=>false;//failed
            }
        }
    }
}
