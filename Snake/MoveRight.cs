namespace Snake
{
    class MoveRight : ICommand
    {
        private Snake snake;

        public MoveRight(Snake snake)
        {
            this.snake = snake;
        }
        public void execute()
        {
            snake.MoveRight();
        }
    }
}