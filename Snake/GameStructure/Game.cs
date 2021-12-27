using System;
using System.Linq;
using System.Threading;
using Snake.Renderer;

namespace Snake
{
    class SnakeGame
    {
        private readonly Snake _snake;
        private readonly Apple _apple;
        private readonly Board _gameBoard;
        private IRenderer _renderer;
        private ICommand _command;
        private char _lastUserInput;
        public bool IsGameOver { get; set; }
        public SnakeGame(int mapSize)
        {
            _gameBoard = new Board(mapSize);
            _snake = new Snake();
            _apple = new Apple();
            _renderer = new CLIRenderer(_gameBoard, _snake, _apple);
            IsGameOver = true;
            _command = new MoveRight(_snake);
            _lastUserInput = 'd';
        }

        public void UseCLIRenderer(bool b)
        {
            if (b)
            {
                _renderer = new CLIRenderer(_gameBoard, _snake, _apple);
            }
        }
        public void UseGUIRenderer(bool b)
        {
            if (b)
            {
                _renderer = new GUIRenderer(_gameBoard, _snake, _apple);
            }
        }
        public void StartGame()
        {
            GenerateNewApplePosition();
            KeyBoardController controller = new KeyBoardController(this);
            controller.KeyboardThread.Start();
            while (IsGameOver)
            {
                _renderer.Render();
                Thread.Sleep(500);
                _command.execute();
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
                IsGameOver = false;
            }
            else if (!SnakeNotOutOfBounds())
            {
                IsGameOver = false;
            }
        }
        private bool SnakeNotOutOfBounds()
        {
            return (_snake.SnakeHeadPosition.Y < _gameBoard.Size &&
                    _snake.SnakeHeadPosition.X < _gameBoard.Size &&
                    _snake.SnakeHeadPosition.Y >= 0 &&
                    _snake.SnakeHeadPosition.X >= 0);
        }
        public void ProcessUserCommand(ConsoleKeyInfo userInput)
        {
            
            try
            {
                if (CommandIsOpposite(userInput.KeyChar)) return;
                _command = GetUserCommand(userInput.KeyChar);
                _lastUserInput = userInput.KeyChar;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        private bool CommandIsOpposite(char userInput)
        {
            if ((_lastUserInput == 'd' && userInput == 'a') || (_lastUserInput == 'a' && userInput == 'd')) 
                return true;
            if ((_lastUserInput == 'w' && userInput == 's') || (_lastUserInput == 's' && userInput == 'w'))
                return true;
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
        private void GenerateNewApplePosition()
        {
            do
            {
                _apple.GenerateRandomPosition(_gameBoard.Size);
            } while (AppleIsGeneratedInSnake());
        }

        private bool AppleIsGeneratedInSnake()
        {
            return _snake.SnakeBodyPositions.Any(bodyPos => bodyPos.X == _apple.Position.X && bodyPos.Y == _apple.Position.Y);
        }
    }
}
