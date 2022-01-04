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
            SnakeHeadPosition = new Point(Constants.SNAKE_START_X_POS + 1, Constants.SNAKE_START_Y_POS);
            SnakeTailPosition = new Point(Constants.SNAKE_START_X_POS, Constants.SNAKE_START_Y_POS);
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
            var direction = CalculateNewTailDirection();
            switch (direction)
            {
                case Position.Up:
                    PushNewTailPosition(SnakeTailPosition.X, SnakeTailPosition.Y - 1);
                    break;
                case Position.Right:
                    PushNewTailPosition(SnakeTailPosition.X+1, SnakeTailPosition.Y);
                    break;
                case Position.Down:
                    PushNewTailPosition(SnakeTailPosition.X, SnakeTailPosition.Y + 1);
                    break;
                case Position.Left:
                    PushNewTailPosition(SnakeTailPosition.X - 1, SnakeTailPosition.Y);
                    break;
                default:
                    break;
            }
        }

        public void PushNewTailPosition(int x, int y)
        {
            var newTailPos = new Point(x, y);
            SnakeBodyPositions.AddFirst(newTailPos);
            SnakeTailPosition = newTailPos;
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

            if (xPosDifference == 0 && yPosDifference == -1) return Position.Up;
            if (xPosDifference == 1 && yPosDifference == 0) return Position.Right;
            if (xPosDifference == 0 && yPosDifference == 1) return Position.Down;
            if (xPosDifference == -1 && yPosDifference == 0) return Position.Left;
            return 0;
        }
    }
}