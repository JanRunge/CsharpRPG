using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace STory.Handlers.Option
{
    public interface Option
    {
        string getText();
        ConsoleColor? GetColor();
        Boolean isAvailable();
        string getPreferredCommand();
        void Select();
    }
}
