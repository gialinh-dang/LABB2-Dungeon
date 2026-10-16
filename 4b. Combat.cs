using System;
using System.Collections.Generic;
using System.Reflection.PortableExecutable;
using System.Text;

namespace LABB2._Dungeon
{
    public static class Combat
    {
        public static Enemy? LastEnemy { get; set;  }
        public static int EnemyHPBefore { get; set; }
        public static void Fight(ICharacter attacker, ICharacter defender)
        {
            // find out which one is the enemy
            Enemy? enemy = null;
            if (attacker is Enemy enemy1)
            {
                enemy = enemy1;
            }
            else if (defender is Enemy enemy2)
            {
                enemy = enemy2;
            }
            // save the enemy's HP before the fight
            // (only the first time this turn, so we see the full change)
            if (enemy!= null && enemy!= LastEnemy)
            {
                LastEnemy = enemy;
                EnemyHPBefore = enemy.HP;
            }


            Attack(attacker, defender);
            // motattack om försvararen överlevde
            if (defender.HP>0)
            {
                Attack(defender, attacker);
            }
        }
        private static void Attack(ICharacter attacker, ICharacter defender)
        {
            int attack = attacker.AttackDice.Throw();
            int defence = defender.DefenceDice.Throw();
            int damage = attack - defence;
            string text = $"{attacker.Name} ({attacker.AttackDice} => {attack}) attacks {defender.Name} ({defender.DefenceDice} => {defence})";
            // ett meddelande som beskriver attacken som nyss inträffade
            if (damage > 0)
            {
                defender.HP -= damage;
                if (defender.HP < 0)
                {
                    defender.HP = 0;
                }
                MessageLog.Add(text + $" and deals {damage} damage.");
            }
            else
            {
                MessageLog.Add(text + "but does no damage.");
            }
            if (defender.HP == 0) 
            {
                MessageLog.Add($"{defender.Name} DIED!!!");
                if (attacker is Player player)
                {
                    player.AddKill(defender);
                }
            }
        }

    }
}
