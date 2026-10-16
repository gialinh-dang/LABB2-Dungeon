using System;
using System.Collections.Generic;
using System.Text;

namespace LABB2._Dungeon
{
    public abstract class Enemy : LevelElement, ICharacter
    {
        public string Name { get;  }
        public int HP { get; set; }
        public Dice AttackDice { get; }
        public Dice DefenceDice { get; }
        protected Enemy(int x, int y, char symbol, ConsoleColor color, string name, int hp, Dice attackDice, Dice defenceDice)
            : base(x, y, symbol, color)
        {
            Name = name;
            HP = hp;
            AttackDice = attackDice;
            DefenceDice = defenceDice;
        }
        public abstract void Update(Player player, LevelData level);

        // Gemensam metod: Försöker gå ett steg i riktningen (dx, dy).
        // Returnerar true om spelaren har gjort något (gått eller attackerat),annars false om vägen är blockerad.
        protected bool TryMove(int dx, int dy, Player player, LevelData level)
        {
            if (dx == 0 && dy == 0)
            {
                return false; // Ingen rörelseriktning.
            }
            int newX = X + dx;
            int newY = Y + dy;

            // Om spelaren finns på den nya rutan -> ENEMY ATTACKERAR FÖRST.
            if (player.X == newX && player.Y == newY)
            {
                Combat.Fight(this, player);
                return true;
            }
            // Om det finns en vägg eller ett annat monster på den nya rutan -> rörelsen blockeras.
            if (level.GetLevelElementAt(newX, newY) != null)
            {
                return false;
            }
            // Tom ruta -> flytta.
            X = newX;
            Y = newY;
            return true;
        }

public LevelElement? GetElementAt(LevelData level, int x, int y) => level.GetLevelElementAt(x, y);
    }
}
