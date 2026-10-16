using System;
using System.Collections.Generic;
using System.Text;

namespace LABB2._Dungeon
{
    public static class MessageLog
    {
        private static readonly List<String> messages = new List<string>();
        public static int Count { get { return messages.Count; } }
                
        public static void Add(string message) 
        {
            messages.Add(message);
        }
        public static void Clear()
        {
            messages.Clear();
        }
        public static void Draw(int startRow)
        {
            for (int i = 0; i < messages.Count; i++)
            {
                int row = startRow + i;
                if (row >=0 && row < Console.BufferHeight)
                {
                    Console.SetCursorPosition(0, row);
                    Console.Write(messages[i]);
                }
                
            }
        }

    }
}
