using System;

namespace Snake
{
    class NothingCommand : ICommand
    {
        private readonly ICommand _lastUserCommand;
        public NothingCommand(ICommand command)
        {
            _lastUserCommand = command;
        }
        public void execute()
        {
            _lastUserCommand.execute();
        }
    }
}