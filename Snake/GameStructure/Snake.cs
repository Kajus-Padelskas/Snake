using System;
using System.Collections.Generic;
using System.Drawing;

namespace Snake
{
    internal class Snake
    {
        public Queue<Point> SnakeBodyPositions { get; set; } 
        public Point SnakeTailPosition { get; set; }
        public Point SnakeHeadPosition { get; set; }

        public Snake()
        {
            SnakeHeadPosition = new Point(Constants.SNAKE_START_X_POS + 1, Constants.SNAKE_START_Y_POS + 1);
            SnakeTailPosition = new Point(Constants.SNAKE_START_X_POS, Constants.SNAKE_START_Y_POS + 1);
            SnakeBodyPositions = new Queue<Point>();
            SnakeBodyPositions.Enqueue(SnakeTailPosition);
            SnakeBodyPositions.Enqueue(SnakeHeadPosition);
        }

        private void ShiftBody()
        {
            SnakeBodyPositions.Dequeue();
            SnakeTailPosition = SnakeBodyPositions.Peek();
            SnakeBodyPositions.Enqueue(SnakeHeadPosition);
        }

        internal void MoveLeft()
        {
            SnakeHeadPosition = MoveHeadLeft();
            ShiftBody();
        }
        private Point MoveHeadLeft()
        {
            var head = SnakeHeadPosition;
            head.X -= 1;
            return head;
        }
        internal void MoveRight()
        {
            SnakeHeadPosition = MoveHeadRight();
            ShiftBody();
        }
        private Point MoveHeadRight()
        {
            var head = SnakeHeadPosition;
            head.X += 1;
            return head;
        }
        internal void MoveUp()
        {
            SnakeHeadPosition = MoveHeadUp();
            ShiftBody();
        }
        private Point MoveHeadUp()
        {
            var head = SnakeHeadPosition;
            head.Y -= 1;
            return head;
        }
        internal void MoveDown()
        {
            SnakeHeadPosition = MoveHeadDown();
            ShiftBody();
        }
        private Point MoveHeadDown()
        {
            var head = SnakeHeadPosition;
            head.Y += 1;
            return head;
        }
        public void GrowTail()
        {
            Point newTailPos;
            var direction = CalculateNewTailDirection();
            switch (direction)
            {
                case Position.Up:
                    newTailPos = new Point(SnakeTailPosition.X, SnakeTailPosition.Y - 1);
                    SnakeBodyPositions.AddFirst(newTailPos);
                    SnakeTailPosition = newTailPos;
                    break;
                case Position.Right:
                    newTailPos = new Point(SnakeTailPosition.X+1, SnakeTailPosition.Y);
                    SnakeBodyPositions.AddFirst(newTailPos);
                    SnakeTailPosition = newTailPos;
                    break;
                case Position.Down:
                    newTailPos = new Point(SnakeTailPosition.X, SnakeTailPosition.Y + 1);
                    SnakeBodyPositions.AddFirst(newTailPos);
                    SnakeTailPosition = newTailPos;
                    break;
                case Position.Left:
                    newTailPos = new Point(SnakeTailPosition.X - 1, SnakeTailPosition.Y);
                    SnakeBodyPositions.AddFirst(newTailPos);
                    SnakeTailPosition = newTailPos;
                    break;
                default:
                    break;
            }
        }

        public bool DidHeadCollideWithBody()
        {
            var snakeBody = SnakeBodyPositions.ToArray();
            for (var i = 0; i < snakeBody.Length-1; i++)
            {
                if (snakeBody[i].X == SnakeHeadPosition.X && snakeBody[i].Y == SnakeHeadPosition.Y) return true;
            }
            return false;
        }

        public Position CalculateNewTailDirection()
        {
            var subTailPos = SnakeBodyPositions.Peek(1);
            var xPosDifference = SnakeTailPosition.X - subTailPos.X;
            var yPosDifference = SnakeTailPosition.Y - subTailPos.Y;

            return xPosDifference switch
            {
                0 when yPosDifference == -1 => Position.Up,
                1 when yPosDifference == 0 => Position.Right,
                0 when yPosDifference == 1 => Position.Down,
                -1 when yPosDifference == 0 => Position.Left,
                _ => 0
            };
        }
    }

    enum Position
    {
        Up = 1, Right = 2, Down = 3, Left = 4
    }
}