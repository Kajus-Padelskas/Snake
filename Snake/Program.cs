using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;

namespace Snake
{
    class Program
    {
        private static readonly int MAP_SIZE = 25;

        static void Main(string[] args)
        {
            SnakeGame game = new(MAP_SIZE);
            game.StartGame();
        }
    }
}
