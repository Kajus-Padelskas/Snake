using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Channels;
using System.Threading.Tasks;

namespace Snake
{
    class CLIRenderer : IRenderer
    {
        private readonly Board _mapBoard;
        private readonly Snake _snake;
        private readonly Apple _apple;
        public CLIRenderer(Board map, Snake snake, Apple apple)
        {
            this._mapBoard = map;
            this._snake = snake;
            this._apple = apple;
        }
        public void Render()
        {
            var val = _mapBoard.GetElement(_snake.SnakeHeadPosition.Y, _snake.SnakeHeadPosition.Y);
            for (var y = 0; y < _mapBoard.Size; y++)
            {
                for (var x = 0; x < _mapBoard.Size; x++)
                {
                    if(IsSnakeHead(y,x)) Console.Write(2);
                    else if (IsSnakeTail(y, x)) Console.Write(3);
                    else if(IsSnakeBody(y,x)) Console.Write(4);
                    else if(IsApple(y,x)) Console.Write(5);
                    else Console.Write(0);
                }
                Console.WriteLine();
            }
            Console.WriteLine();
        }

        private bool IsSnakeTail(int y, int x)
        {
            return _snake.SnakeTailPosition.Y == y && _snake.SnakeTailPosition.X == x;
        }

        private bool IsSnakeBody(int y, int x)
        {
            return _snake.SnakeBodyPositions.Any(pos => pos.Y == y && pos.X == x);
        }

        private bool IsSnakeHead(int y, int x)
        {
            return _snake.SnakeHeadPosition.Y == y && _snake.SnakeHeadPosition.X == x;
        }

        private bool IsApple(int y, int x)
        {
            return _apple.Position.Y == y && _apple.Position.X == x;
        }
    }
}
