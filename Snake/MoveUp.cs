namespace Snake
{
    class MoveUp : ICommand
    {
        private Snake snake;

        public MoveUp(Snake snake)
        {
            this.snake = snake;
        }
        public void execute()
        {
            snake.MoveUp();
        }
    }
}