using System;
using System.Collections.Generic;
using System.Drawing;

namespace Snake
{
    class Program
    {
        static void Main(string[] args)
        {
            SnakeGame game = new SnakeGame(10);
            game.StartGame();
        }
    }
}
