using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snake
{
    internal class Apple
    {
        public Point Position { get; set; }

        public void GenerateRandomPosition(int size)
        {
            Random random = new Random();
            Position = new Point(random.Next(size), random.Next(size));
        }

        public bool IsAppleCollected(Point currentPos)
        {
            return Position.X == currentPos.X && Position.Y == currentPos.Y;
        }
    }
}
