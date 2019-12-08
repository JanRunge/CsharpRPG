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
        int receiveDamage(int amount, DamageType type);
        void attack(Attackable a);

        void RemoveMana(int amount);
        int GetIntelligence();
        int GetStrength();

        Boolean isAlive();
        int XPOnDeath();
    }
}
