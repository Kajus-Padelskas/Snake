using System;
using System.Collections.Generic;

namespace Snake
{
    internal class Board
    {
        public Board(int size)
        {
            Map = new List<List<short>>();

            for (var i = 0; i < size; i++)
            {
                Map.Add(new List<short>());
                for (var j = 0; j < size; j++)
                {
                    Map[i].Add(0);
                }
            }
        }

        public List<List<short>> Map { get; set; }

        public int Size => Map.Count;

        public int GetElement(int i, int j)
        {
            if (IsNotOutOfRange(i,j))
            {
                return Map[i][j];
            }
            return -1;
        }

        private bool IsNotOutOfRange(int i, int j)
        {
            return i >= 0 && i < Size && j >= 0 && j < Size;
        }
    }
}