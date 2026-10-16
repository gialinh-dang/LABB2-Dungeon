using System;
using System.Collections.Generic;
using System.Text;

namespace LABB2._Dungeon
{
     public class Player : LevelElement, ICharacter
    {
        public string Name { get; set; }
        public int HP { get; set; }
        public Dice AttackDice { get; set; }
        public Dice DefenceDice { get; set; }

        public int MaxHP { get; set; }
        public int Level { get; set; }
        public int RatKills { get; set; }
        public int SnakeKills { get; set; }

        public Player(int x, int y) : base(x, y, '@', ConsoleColor.DarkYellow)
        {
            Name = "Player";
            MaxHP = 100;
            HP = MaxHP;
            Level = 1;
            AttackDice = new Dice(2, 6, 3);
            DefenceDice = new Dice(2, 6, 1);
        }
        public void Move(ConsoleKey key, LevelData level)
        {
            int newX = X;
            int newY = Y;

            if(key == ConsoleKey.UpArrow || key == ConsoleKey.W)
            {
                newY--;
            }
            else if (key == ConsoleKey.DownArrow || key == ConsoleKey.S)
            {
                newY++;
            }
            else if (key == ConsoleKey.LeftArrow || key == ConsoleKey.A)
            {
                newX--;
            }
            else if (key == ConsoleKey.RightArrow || key == ConsoleKey.D)
            {
                newX++;
            }
            else
            {
                return;  //any other key = stand still
            }
            LevelElement? element = level.GetLevelElementAt(newX, newY);
            if (element == null)
            {
                X = newX;
                Y = newY;
            }
            else if (element is Enemy enemy)
            {
                Combat.Fight(this, enemy);
            }
        }
        public void AddKill(ICharacter enemy) //called from Combat when the player kills an enemy
        {
            if (enemy is Rat)
            {
                RatKills++; 
                if (RatKills % 5 == 0)
                {
                    MessageLog.Add("You killed 5 rats!!!");
                    Heal(MaxHP / 7);
                }
            }
            else if (enemy is Snake)
            {
                SnakeKills++;
                if (SnakeKills % 3 == 0)
                {
                    MessageLog.Add("You killed 3 snakes!!!");
                    Heal(MaxHP / 6);
                }
            }
        }
        private void Heal (int amount)
        {
            if (HP + amount > MaxHP)
            {
                HP = MaxHP;
                LevelUp();
            }
            else
            {
                HP += amount;
                MessageLog.Add($"You healed {amount} HP. HP is {HP} now.");
            }
        }
        private void LevelUp()
        {
            Level++;
            AttackDice = new Dice(2, 6, 3 * Level);
            DefenceDice = new Dice(2, 6, 1 * (Level - 1));
            MessageLog.Add($"LEVELUP, HURRAAA!!! You are level {Level} now. Attack:{AttackDice}, Defence:{DefenceDice}");

        }
    }
}
