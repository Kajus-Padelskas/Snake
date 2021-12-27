using System;
using System.Collections.Generic;
using System.Drawing;

namespace Snake
{
    class Snake
    {
        public Queue<Point> SnakeBodyPositions { get; set; } // Pabandyti Deque cia naudoti
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
                case 1:
                    newTailPos = new Point(SnakeTailPosition.X, SnakeTailPosition.Y - 1);
                    SnakeBodyPositions.AddFirst(newTailPos);
                    SnakeTailPosition = newTailPos;
                    break;
                case 2:
                    newTailPos = new Point(SnakeTailPosition.X+1, SnakeTailPosition.Y);
                    SnakeBodyPositions.AddFirst(newTailPos);
                    SnakeTailPosition = newTailPos;
                    break;
                case 3:
                    newTailPos = new Point(SnakeTailPosition.X, SnakeTailPosition.Y + 1);
                    SnakeBodyPositions.AddFirst(newTailPos);
                    SnakeTailPosition = newTailPos;
                    break;
                case 4:
                    newTailPos = new Point(SnakeTailPosition.X - 1, SnakeTailPosition.Y);
                    SnakeBodyPositions.AddFirst(newTailPos);
                    SnakeTailPosition = newTailPos;
                    break;
                default:
                    break;
            }
        }

        internal bool DidHeadCollideWithBody()
        {
            var snakeBody = SnakeBodyPositions.ToArray();
            for (var i = 0; i < snakeBody.Length-1; i++)
            {
                if (snakeBody[i].X == SnakeHeadPosition.X && snakeBody[i].Y == SnakeHeadPosition.Y) return true;
            }
            return false;
        }

        public int CalculateNewTailDirection()
        {
            var subTailPos = SnakeBodyPositions.Peek(1);
            var xPosDifference = SnakeTailPosition.X - subTailPos.X;
            var yPosDifference = SnakeTailPosition.Y - subTailPos.Y;

            if (xPosDifference == 0 && yPosDifference == -1) return 1;
            if (xPosDifference == 1 && yPosDifference == 0) return 2;
            if (xPosDifference == 0 && yPosDifference == 1) return 3;
            if (xPosDifference == -1 && yPosDifference == 0) return 4;
            return 0;
        }
    }
}