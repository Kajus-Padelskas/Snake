using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Snake
{
    internal static class ExtendedQueueMethods
    {
        public static void AddFirst(this Queue<Point> queue, Point point)
        {
            var pointArray = queue.ToArray();
            queue.Clear();
            queue.Enqueue(point);
            foreach (var pointInArray in pointArray)
            {
                queue.Enqueue(pointInArray);
            }
        }

        public static Point Peek(this Queue<Point> queue, int pos)
        {
            var pointArray = queue.ToArray();
            return pointArray[pos];
        }
    }
}
