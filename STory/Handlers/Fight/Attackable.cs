using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using STory.Types;

namespace STory.Handlers.Fight
{
    public interface Attackable
    {
        string getName();
        void receiveDamage(int amount, DamageType type);
        void attack(Attackable a);
        Boolean isAlive();
        int XPOnDeath();
    }
}
