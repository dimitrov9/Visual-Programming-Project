﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Roulette
{
    [Serializable]
    public class Game
    {
        ///<summary>
        ///The number of credits avaliable the player has
        ///</summary>
        public   long credits;

        ///<summary>
        ///The Total amount of invested credits 
        ///</summary>
        public   long totalBet;
        ///<summary>
        ///0-36 Array cointaining invested credits for each number
        ///</summary>
        public   long[] numBets;

        public   long first12;
        public   long second12;
        public   long third12;
        ///<summary>
        ///0-6 Array contains the credits of the following bets:<br/>
        ///0. 1-18 <br/>
        ///1. 19-36 <br/>
        ///2. even <br/>
        ///3 odd<br/>
        ///4. red  <br/>
        ///5. black
        ///</summary>
        public   long[] otherBets ;


        public   long prevTotalBet;
        public   long[] prevNumBets ;
        public   long prevFirst12;
        public   long prevSecond12;
        public   long prevThird12;
        public long[] prevOtherBets;

        public   int[] redNumbers ;
        public   int[] blackNumbers;
        // Initializes a game that takes one argumet of Credits how much the player should start with and the other are 0
        //public   Game()
        //{
        //}

        public Game(long initCredits)
        {
            credits = initCredits;
            totalBet = 0;
            numBets = new long[37];
            first12=0;
            second12=0;
            third12=0;
            otherBets = new long[6];
            prevTotalBet = 0;
            prevNumBets = new long[37];
            prevFirst12=0;
            prevSecond12=0;
            prevThird12=0;
            prevOtherBets = new long[6];
            redNumbers = new int[18] { 2, 4, 6, 8, 10, 11, 13, 15, 17, 20, 22, 24, 26, 28, 29, 31, 33, 35 };
            blackNumbers = new int[18] { 1, 3, 5, 7, 9, 12, 14, 16, 18, 19, 21, 23, 25, 27, 30, 32, 34, 36 };
        }
        ///<summary>
        ///Sets the input to credits and sets the bets to 0
        ///</summary>
        public   void SetInitialCredits(long Credits) 
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
        public   int ReturnProfitLoss(int nextGuess)
        {
            int profit = 0;
            profit = (int)(numBets[nextGuess] * 36 );

            if(nextGuess>0){

                 if (nextGuess <= 12)
                {
                    profit += (int)(first12 * 3);
                }
                else if (nextGuess > 12 && nextGuess <= 24)
                {
                    profit += (int)(second12 * 3);
                }
                else if (nextGuess > 24 && nextGuess <= 36)
                {
                    profit += (int)(third12 * 3);
                }


                if ( nextGuess < 19)
                {
                    profit += (int)(otherBets[0] * 2);
                }
                else if (nextGuess > 18 && nextGuess < 37)
                {
                    profit += (int)(otherBets[1] * 2);
                }


                if (nextGuess % 2 == 0)
                {
                    profit += (int)(otherBets[2] * 2);
                }
                else if (nextGuess % 2 == 1)
                {
                    profit += (int)(otherBets[3] * 2);
                }

                if (isRed(nextGuess))
                {
                    profit += (int)(otherBets[4] * 2);
                }
                else if (isBlack(nextGuess))
                {
                    profit += (int)(otherBets[5] * 2);
                }

            }
           
            credits += profit;
            return profit;
        }

        ///<summary>
        ///Resets all current Bets to 0
        ///</summary>
        public   void ResetAllBets()
        {
            totalBet = 0L;
            for(int i=0;i<numBets.Length;i++)
            {
                numBets[i] = 0L;
            }
            first12 = 0;
            second12 = 0;
            third12 = 0;
            for (int i = 0; i < otherBets.Length; i++)
            {
                otherBets[i] = 0;
            }

        }

        ///<summary>
        ///Takes a number and adds 5 to the numBets array at a index of that number, updates totalBet and credits.
        ///</summary>
        public   void InstertNumberBet(int number,int betAmount)
        {
            numBets[number] += betAmount;
            totalBet += betAmount;
            credits -= betAmount;
        }

        public   void InsertTwelves(int number, int betAmount)
        {
            if (number == 1)
            {
                first12 += betAmount;
            }
            else if (number == 2)
            {
                second12 += betAmount;
            }
            else if (number == 3)
            {
                third12 += betAmount;
            }
            totalBet += betAmount;
            credits -= betAmount;
        }

        public   void InsertOtherBets(int number, int betAmount)
        {
            otherBets[number] += betAmount;
            totalBet += betAmount;
            credits -= betAmount;
        }

        ///<summary>
        ///Saves the previus bet in prevTotalBet & prevNumBets
        ///</summary>
        public   void SavePrevBet()
        {
            prevTotalBet = totalBet;
            for (int i = 0; i < 37; i++)
            {
                prevNumBets[i] = numBets[i];
            }
            prevFirst12 = first12;
            prevSecond12 = second12;
            prevThird12 = third12;
            for (int i = 0; i < 6; i++)
            {
                prevOtherBets[i] = otherBets[i];
            }
        }

        ///<summary>
        ///Bets the previous bet
        ///</summary>
        public   void BetPrev()
        {
            totalBet = prevTotalBet;
            for (int i = 0; i < 37; i++)
            {
                numBets[i] = prevNumBets[i];
            }
            first12 = prevFirst12;
            second12 = prevSecond12;
            third12 = prevThird12;
            for (int i = 0; i < 6; i++)
            {
                otherBets[i] = prevOtherBets[i];
            }
            credits -= totalBet;
        }

        ///<summary>
        ///Resets the bets to 0 and returns credit
        ///</summary>
        public   void CancelAllBets()
        {
            credits += totalBet;
            totalBet = 0;
            for (int i = 0; i < 37; i++)
            {
                numBets[i] = 0;
            }
            first12 = 0;
            second12 = 0;
            third12 = 0;
            for (int i = 0; i < 6; i++)
            {
                otherBets[i] = 0;
            }
        }

        ///<summary>
        ///Doubles all the bets
        ///</summary>
        public   void DoubleAllBets()
        {
            credits -= totalBet;
            totalBet *= 2;
            for (int i = 0; i < 37; i++)
            {
                numBets[i] *= 2;
            }
            first12 *= 2;
            second12 *= 2;
            third12 *= 2;
            for (int i = 0; i < 6; i++)
            {
                otherBets[i] *= 2;
            }

        }


        public   bool isRed(int number)
        {
            foreach (int red in redNumbers)
            {
                if (number == red)
                {
                    return true;
                }
            }
            return false;
        }

        public   bool isBlack(int number)
        {
            foreach (int black in blackNumbers)
            {
                if (number == black)
                {
                    return true;
                }
            }
            return false;
        }
    }
}
