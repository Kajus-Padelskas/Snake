namespace Snake
{
    class MoveLeft : ICommand
    {
        private Snake snake;

        public MoveLeft(Snake snake)
        {
            this.snake = snake;
        }
        public void execute()
        {
            snake.MoveLeft();
        }
    }
}