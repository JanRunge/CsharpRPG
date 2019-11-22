using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace STory
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
