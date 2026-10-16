using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;

namespace LABB2._Dungeon
{
    public class Wall : LevelElement
    {
        public bool IsDiscovered { get; set; }
        public Wall(int x, int y) : base(x, y, '#', ConsoleColor.Gray)
        {
            // no further initialization required if base stores X, Y, Symbol, Color
        }
 
    }
}
