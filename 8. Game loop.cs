using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;

namespace LABB2._Dungeon
{
    public class Game
    {
        private const int VisionRange = 5;
        private readonly LevelData level;
        private readonly Player player;
        private int turn = 0;
        public Game(string filename)
        {
            level = new LevelData();
            level.Load(filename);
            player = new Player(level.PlayerStartX, level.PlayerStartY);
        }
        public void Run()
        {
            Console.CursorVisible = false;
            Draw();
            
            while(true) //// GAMELOOP: Varje varv = en tur 
            {
                ConsoleKey key = Console.ReadKey(true).Key;
                if (key == ConsoleKey.Escape)
                {
                    break; // Exit game
                }
                MessageLog.Clear();
                turn++;
                // Player
                player.Move(key, level);
                level.RemoveDeadEnemies();
                // Enemies
                UpdateEnemies();
                level.RemoveDeadEnemies();

                Draw();

                if (player.HP <= 0)
                {
                    ShowEndMessage("GAME OVER!!!LOSER :)) ", ConsoleColor.Red);
                    break;
                }
                if (!level.Elements.OfType<Enemy>().Any())
                {
                    ShowEndMessage("YOU WINNN!!!", ConsoleColor.Blue);
                    break;
                }

            }
            Console.ResetColor();
            Console.CursorVisible = true;
        }
        private Enemy? FindEnemyNextToPlayer()
        {
            foreach (LevelElement element in level.Elements)
            {
                if (element is Enemy enemy && enemy.DistanceTo(player) <= 1)
                {
                    return enemy;
                }
            }
            return null; 
        }
        private void UpdateEnemies()
        {
            List<Enemy> enemies = level.Elements.OfType<Enemy>().ToList();
            foreach (Enemy enemy in enemies)
            {
                if (player.HP <= 0) return;
                if (enemy.HP <= 0) continue;

                enemy.Update(player, level);
            }

        }
        private void Draw()
        {
            Console.Clear();
            foreach (LevelElement element in level.Elements)
            {
                bool inSight = element.DistanceTo(player) <= VisionRange;
                if (element is Wall wall)
                {
                    if (inSight)
                    {
                        wall.IsDiscovered = true;
                    }
                    if (wall.IsDiscovered)
                    {
                        wall.Draw();

                        //Wall: När de har upptäckts en gång förblir de synliga.
                    }
                }
                else if (inSight)
                {
                    element.Draw();
                    // Enemies: Visas bara när de är inom synfältet.
                }

            }
            player.Draw();
            DrawStatus();
        }
        private void DrawStatus()
        {
            int row = level.Height + 1;

            // PLAYER STATS
            string hpText = $"HP: {player.HP}/{player.MaxHP}";
            string levelText = $"Level: {player.Level}";
            string attackText = $"Attack: {player.AttackDice}";
            string defenceText = $"Defence: {player.DefenceDice}";
            string turnText = $"Turn: {turn}";

            Console.SetCursorPosition(0, row);
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine($"{levelText,-16}{hpText,-19}{attackText,-20}{defenceText,-19}{turnText}");

            //EnemyHP
            Enemy? enemy = Combat.LastEnemy;
            bool justFought = enemy != null;
            
            if (enemy == null)
            {
                enemy = FindEnemyNextToPlayer();
            }
            if (enemy != null)
            {
                int bars = enemy.HP * 10 / enemy.MaxHP;
                string hpBar = new string('#', bars) + new string('-', 10 - bars);
                string enemyText;
                if (justFought && Combat.EnemyHPBefore != enemy.HP)
                {
                    int lost = Combat.EnemyHPBefore - enemy.HP;
                    enemyText = $"HP: {Combat.EnemyHPBefore} -> {enemy.HP}/{enemy.MaxHP} (-{lost})";
                }
                else
                {
                    enemyText = $"HP: {enemy.HP}/{enemy.MaxHP}";
                }
                if (enemy.HP == 0)
                {
                    enemyText += $" [{hpBar}]  DEAD";
                }
                else
                {
                    enemyText += $"  [{hpBar}]";
                }
                Console.SetCursorPosition(0, row + 1);
                Console.ForegroundColor = enemy.Color;
                Console.WriteLine($"{enemy.Name,-6}{enemyText}");
            }
            Console.ResetColor();
            MessageLog.Draw(row + 2);
        }

        private void ShowEndMessage(string text, ConsoleColor color)
        {
            int row = level.Height + 3 + MessageLog.Count + 1;

            Console.SetCursorPosition(0, row);
            Console.ForegroundColor = color;
            Console.WriteLine(text);
            Console.ResetColor();
            Console.WriteLine("Press any key to exit...");
            Console.ReadKey(true);
        }

    }
}