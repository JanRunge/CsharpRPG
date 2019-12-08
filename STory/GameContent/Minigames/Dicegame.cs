using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace STory.GameContent.Minigames
{
    class Dicegame :Minigame
    {

        public Dicegame() {
            this.name = "dicegame";
        }
        override public void Play()
        {
            /*
             * wahrscheinlichkeit für 6er pasch: 0,028
             * warsch für eine kombi über 8 : 0,389
             * warsch für 3er pasch * 2er pasch zusammen: 0,056
             * 
             * Durschnittlicher gewinn bei 100g wenn 6er pasch dreifach ausschüttet & pasche nicht doppelt zählen
             * 0,389*200 =77,8
             * 0,028*300 = 8,4
             * = 86,2
             * Durschnittlicher gewinn bei 100g wenn 6er pasch dreifach ausschüttet & pasche doppelt zählen
             * 0,389*200 =77,8
             * 0,028*300 = 8,4
             * 0,056*200 =11,2
             * =97,4
             */
            
            Program.player.removeGold(stake);

            int sum = 0;    //the sum of the dices.
            int winningSum = 8;// the number the dices have to reach
            int returnvalue = 0;//the amount the player wins
            Dice dice1 = new Dice();
            Dice dice2 = new Dice();

            dice1.roll();
            dice2.roll();
            CIO.Print(dice1.GetValue() + "&" + dice2.GetValue());
            sum = dice1.GetValue() + dice2.GetValue();
            
            if (sum >= winningSum)
            {
                CIO.Print("You Won!");
                if (sum == 12) //two sixes!
                {
                    returnvalue = stake * 3;
                }
                else
                {
                    returnvalue = stake * 2;
                }
                Program.player.AddGold(returnvalue);
            }
            else
            {
                CIO.Print("You lost.");
            }
            


        }
        override public void PrintRules()
        {
            CIO.Print("exampleRules");
        }
    }
}
