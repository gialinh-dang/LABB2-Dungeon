using System;
using System.Collections.Generic;
using System.Text;

namespace LABB2._Dungeon
{
    public class Dice
    {
        private readonly int numberOfDice;
        private readonly int sidesPerDice;
        private readonly int modifier;
        public Dice(int numberOfDice, int sidesPerDice, int modifier)
        {
            this.numberOfDice = numberOfDice;
            this.sidesPerDice = sidesPerDice;
            this.modifier = modifier;
        }
        public int Throw()
        {
            int total = 0;
            for (int i = 0; i < numberOfDice; i++)
            {
                total += Random.Shared.Next(1, sidesPerDice + 1);
            }
            return total + modifier;
        }
       
        public override string ToString()
        {
            return $"{numberOfDice}d{sidesPerDice}+{modifier}";
        }
    }
}
