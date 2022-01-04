using System;
using System.Linq;
using System.Threading;
using Snake.Renderer;

namespace Snake
{
    internal class SnakeGame
    {
        private readonly Snake _snake;
        private readonly Apple _apple;
        private readonly Board _gameBoard;
        private readonly KeyBoardController _controller;
        private IRenderer _renderer;
        private ICommand _command;
        private double _gameSpeed;
        public bool IsGameOver { get; set; }
        public SnakeGame(int mapSize)
        {
            _gameBoard = new Board(mapSize);
            _snake = new Snake();
            _apple = new Apple();
            _renderer = new CLIRenderer(_gameBoard, _snake, _apple);
            IsGameOver = false;
            _command = new MoveRight(_snake);
            _controller = new KeyBoardController(this);
            _gameSpeed = Constants.MAX_RENDERING_TIMEOUT - mapSize * mapSize;
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
            var initialGameSpeedInMs = _gameSpeed;
            if(_gameBoard.Size < 0 || _gameBoard.Size > 25)
            {
                Console.WriteLine($"Map size is to small or to big, it should be at least the size of 5 or below or equal to 25, but yours is {_gameBoard.Size}");
            } else
            {
                GenerateNewApplePosition();
                _controller.KeyboardThread.Start();
                while (!IsGameOver)
                {
                    _renderer.Render();
                    Thread.Sleep((int)_gameSpeed);
                    ProcessUserCommand(_controller.UserInput);
                    _command.execute();
                    if (_apple.IsAppleCollected(_snake.SnakeHeadPosition))
                    {
                        GenerateNewApplePosition();
                        _snake.GrowTail();
                        _gameSpeed -= initialGameSpeedInMs / (_gameBoard.Size * _gameBoard.Size) ;
                    }
                    CheckIfGameEnded();
                    Console.Clear();
                }
                ShowUserGameStatus();
            }
        }

        private void ShowUserGameStatus()
        {
            Console.WriteLine(_snake.SnakeBodyPositions.Count == _gameBoard.Size * _gameBoard.Size
                ? "Congratz you won"
                : "Get good kid");
        }

        private void CheckIfGameEnded()
        {
            if (_snake.DidHeadCollideWithBody())
            {
                IsGameOver = true;
            }
            else if (!SnakeNotOutOfBounds())
            {
                IsGameOver = true;
            }
            else if (_snake.SnakeBodyPositions.Count == _gameBoard.Size * _gameBoard.Size)
            {
                IsGameOver = true;
            }
        }
        private bool SnakeNotOutOfBounds()
        {
            return (_snake.SnakeHeadPosition.Y < _gameBoard.Size &&
                    _snake.SnakeHeadPosition.X < _gameBoard.Size &&
                    _snake.SnakeHeadPosition.Y >= 0 &&
                    _snake.SnakeHeadPosition.X >= 0);
        }
        public void ProcessUserCommand(char userInput)
        {
            
            try
            {
                if (CommandIsOpposite(userInput)) return;
                _command = GetUserCommand(userInput);
                _controller.LastUserInput = userInput;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        private bool CommandIsOpposite(char userInput)
        {
            if ((_controller.LastUserInput == 'd' && userInput == 'a') || (_controller.LastUserInput == 'a' && userInput == 'd')) 
                return true;
            if ((_controller.LastUserInput == 'w' && userInput == 's') || (_controller.LastUserInput == 's' && userInput == 'w'))
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
                _ => new NothingCommand(_command)
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
