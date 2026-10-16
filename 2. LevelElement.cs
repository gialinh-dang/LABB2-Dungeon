using System;
using System.Collections.Generic;
using System.Text;

namespace LABB2._Dungeon
{
  public abstract class LevelElement
    {
        public int X { get; protected set; }
        public int Y { get; protected set; }
        public char Symbol { get; set; }
        public ConsoleColor Color { get; set; }
        protected LevelElement(int x, int y, char symbol, ConsoleColor color)
        {
            X = x;
            Y = y;
            Symbol = symbol;
            Color = color;
        }
        public void Draw()
        {
            Console.SetCursorPosition(X, Y);
            Console.ForegroundColor = Color;
            Console.Write(Symbol);
            Console.ResetColor();
        }
        public double DistanceTo(LevelElement other)
        {
            int dx = X - other.X;
            int dy = Y - other.Y;
            return Math.Sqrt(dx * dx + dy * dy);
        }
    }
}
