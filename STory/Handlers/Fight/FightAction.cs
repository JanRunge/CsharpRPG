using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using STory.GameContent;
using STory.Handlers.Option;

namespace STory.Handlers.Fight
{
    public interface FightAction : Option.Option
    {
        void Use(Character user, Character target);
    }
}
