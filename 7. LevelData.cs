using System;
using System.Collections.Generic;
using System.Text;

namespace LABB2._Dungeon
{
    public class LevelData
    {
        private List<LevelElement> elements = new List<LevelElement>();
        public List<LevelElement> Elements 
        {
            get { return elements; }
        }
        public int PlayerStartX { get; private set; }
        public int PlayerStartY { get; private set; }
        public int Height { get; private set; }
        public void Load(string fileName)
        {
            string[] lines = File.ReadAllLines(fileName);
            Height = lines.Length;
            for (int y = 0; y < lines.Length; y++)
            {
                for (int x = 0; x < lines[y].Length; x++)
                {
                    char c = lines[y][x]; // c = case
                    if (c == '#')
                    {
                        elements.Add(new Wall(x, y));
                    }
                    else if (c == 'r')
                    {
                        elements.Add(new Rat(x, y));
                    }
                    else if (c == 's')
                    {
                        elements.Add(new Snake(x, y));
                    }
                    else if (c == '@')
                    {
                        PlayerStartX = x;
                        PlayerStartY = y;
                    }
                }
            }
        }
        public LevelElement? GetLevelElementAt(int x, int y)
        {
            foreach (LevelElement element in elements)
            {
                if (element.X == x && element.Y == y)
                {
                    return element;
                }
            }
            return null;
        }
        public void RemoveDeadEnemies()
        {
            // Loopa baklänges så att borttagning av element inte påverkar indexen.
            for (int i = elements.Count - 1; i >= 0; i--)
            {
                if (elements[i] is Enemy enemy && enemy.HP <= 0)
                {
                    elements.RemoveAt(i);
                }
            }
        }
    }
}
