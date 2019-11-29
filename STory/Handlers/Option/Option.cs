using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace STory.Handlers.Option
{
    /// <summary>
    /// An Interface for everything that wants to displayable and choosable in a Dialog / Optionhandler
    /// </summary>
    public interface Option
    {
        /// <summary>
        /// The text to be displayed in an Optionhandler
        /// </summary>
        string getText();
        /// <summary>
        /// The color in which the text of this option should be displayed
        /// </summary>
        ConsoleColor GetColor();
        /// <summary>
        /// If the Option is Choosable. Choosing a not-available Option will print a not available message. To modify the message, see genericOption
        /// </summary>
        Boolean isAvailable();
        /// <summary>
        /// The command which the Option wants to be given. If Multiple Options want the same Command, the Program will terminate
        /// </summary>
        string getPreferredCommand();
        /// <summary>
        /// The logic which needs to be executed when the Option is picked
        /// </summary>
        void Select();
    }
}
