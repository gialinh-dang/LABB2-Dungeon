using System;
using System.Collections.Generic;
using System.Text;

namespace LABB2._Dungeon
{
    public class Snake : Enemy 
    {
        public Snake(int x, int y)
            : base(x, y, 's', ConsoleColor.DarkGreen, "Snake", 20,
                  new Dice (2, 4, 3), //Attck 2d4+3
                  new Dice (1, 8, 5)) //Defence 1d8+5
        {
        }
        public override void Update(Player player, LevelData level)
        {
            if (DistanceTo(player) > 2) // Om spelaren är mer än 2 rutor bort -> stå stilla.
            {
                return;
            }
            // Flyktriktning = motsatt riktning från spelaren.
            int diffX = X - player.X;
            int diffY = Y - player.Y;
            int stepX = Math.Sign(diffX);
            int stepY = Math.Sign(diffY);

            if(Math.Abs(diffX) >= Math.Abs(diffY))  // Om den riktningen är blockerad, försök med den andra axeln.
            {
                if (!TryMove(stepX, 0, player, level))
                {
                    TryMove(0, stepY, player, level);
                }
            }
            else
            {
                if (!TryMove(0, stepY, player, level))
                {
                    TryMove(stepX, 0, player, level);
                }
            }
        }

    }
}
