using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Roulette
{
    public static class Game
    {
        ///<summary>
        ///The number of credits avaliable the player has
        ///</summary>
        public static long credits;


        ///<summary>
        ///The Total amount of invested credits 
        ///</summary>
        public static long totalBet;
        ///<summary>
        ///0-36 Array cointaining invested credits for each number
        ///</summary>
        public static long[] numBets = new long[37];


        private static long prevTotalBet;
        private static long[] prevNumBets = new long[37];
        

        // Initializes a game that takes one argumet of Credits how much the player should start with and the other are 0
        //public static Game()
        //{
        //}


        ///<summary>
        ///Sets the input to credits and sets the bets to 0
        ///</summary>
        public static void SetInitialCredits(long Credits) 
        {
            credits = Credits;

            totalBet = 0L;
            for (int i = 0; i < numBets.Length; i++)
            {
                numBets[i] = 0L;
            }

            prevTotalBet = 0L;
            for (int i = 0; i <  prevNumBets.Length; i++)
            {
                prevNumBets[i] = 0L;
            }
        }

        ///<summary>
        ///Takes a number, returns the profit or loss, and adds it to credits
        ///</summary>
        public static int ReturnProfitLoss(int nextGuess)
        {
            int profit = 0;
            if (nextGuess == 1)
            {
                 profit = (Int32)(numBets[1] * 36 - totalBet);
            }
            credits += profit;
            return profit;
        }

        ///<summary>
        ///Resets all current Bets to 0
        ///</summary>
        public static void ResetAllBets()
        {
            totalBet = 0L;
            for(int i=0;i<numBets.Length;i++)
            {
                numBets[i] = 0L;
            }

        }

        ///<summary>
        ///Takes a number and adds 5 to the numBets array at a index of that number, updates totalBet and credits.
        ///</summary>
        public static void InstertNumberBet(int number)
        {
            numBets[number] += 5;
            totalBet += 5;
            credits -= 5;
        }
    }
}
