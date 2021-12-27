using System;

namespace Snake
{
    class NothingCommand : ICommand
    {
        public void execute()
        {
            Console.WriteLine("Invalid Command");
        }
    }
}