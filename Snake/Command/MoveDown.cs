namespace Snake
{
    class MoveDown : ICommand
    {
        private Snake snake;

        public MoveDown(Snake snake)
        {
            this.snake = snake;
        }
        public void execute()
        {
            snake.MoveDown();
        }
    }
}