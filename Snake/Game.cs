using System;

namespace Snake
{
    class SnakeGame
    {
        private readonly Renderer _renderer;
        private readonly Snake _snake;
        private readonly Apple _apple;
        private readonly Board _gameBoard;
        private bool _continueGame;
        public SnakeGame(int mapSize)
        {
            _gameBoard = new Board(mapSize);
            _snake = new Snake();
            _apple = new Apple();
            _renderer = new Renderer(new Board(mapSize), _snake, _apple);
            _continueGame = true;
        }
        public void StartGame()
        {
            GenerateNewApplePosition();
            while (_continueGame)
            {
                _renderer.Render();
                var userInput = Console.ReadKey();
                Console.WriteLine();
                ProcessUserCommand(userInput);
                if (_apple.IsAppleCollected(_snake.SnakeHeadPosition))
                {
                    GenerateNewApplePosition();
                    _snake.GrowTail();
                }
                CheckIfGameEnded();
            }
        }

        private void CheckIfGameEnded()
        {
            if (_snake.DidHeadCollideWithBody())
            {
                _continueGame = false;
            }
            else if (!SnakeNotOutOfBounds())
            {
                _continueGame = false;
            }
        }

        private void ProcessUserCommand(ConsoleKeyInfo userInput)
        {
            try
            {
                var command = GetUserCommand(userInput.KeyChar);
                command.execute();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        private void GenerateNewApplePosition()
        {
            do
            {
                _apple.GenerateRandomPosition(_gameBoard.Size);
            } while (AppleIsGeneratedInSnake());
        }

        private bool AppleIsGeneratedInSnake()
        {
            foreach (var bodyPos in _snake.SnakeBodyPositions)
            {
                if (bodyPos.X == _apple.Position.X && bodyPos.Y == _apple.Position.Y)
                {
                    return true;
                }
            }
            return false;
        }

        private ICommand GetUserCommand(char key)
        {
            return key switch
            {
                'w' => new MoveUp(_snake),
                's' => new MoveDown(_snake),
                'a' => new MoveLeft(_snake),
                'd' => new MoveRight(_snake),
                _ => new NothingCommand()
            };
        }

        private bool SnakeNotOutOfBounds()
        {
            return (_snake.SnakeHeadPosition.Y < _gameBoard.Size && 
                   _snake.SnakeHeadPosition.X < _gameBoard.Size && 
                   _snake.SnakeHeadPosition.Y >= 0 && 
                   _snake.SnakeHeadPosition.X >= 0);
        }
    }
}
