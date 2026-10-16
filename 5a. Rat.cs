using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace LABB2._Dungeon
{
     public class Rat : Enemy
    {
        public Rat(int x, int y)
           : base(x, y, 'r', ConsoleColor.DarkRed, "Rat", 10,  // Alla stats för Rat (HP 10, 1d6+3, 1d6+1).
                 new Dice(1, 6, 3), // Attack 1d6+3
                 new Dice(1, 6, 1)) // Defence 1d6+1
        {
        }
        // Gå ett steg i en slumpmässig riktning.
        public override void Update(Player player, LevelData level)
        {
            int direction = Random.Shared.Next(4); // 0,1,2 elr 3
            switch (direction)
            {
                case 0: TryMove(0, -1, player, level); break; //upp
                case 1: TryMove(0, 1, player, level); break;  //ner
                case 2: TryMove(-1, 0, player, level); break; //vänster
                case 3: TryMove(1, 0, player, level); break;  //höger
            }
        }

    }
}
