using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Snake
{
    internal class KeyBoardController
    {
        public Thread KeyboardThread { get; set; }

        public KeyBoardController(SnakeGame game)
        {
            KeyboardThread = new Thread(() =>
            {
                while (game.IsGameOver)
                {
                    var userInput = Console.ReadKey(true);
                    game.ProcessUserCommand(userInput);
                }
            });
        }
    }
}
