using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CsharpMiniProjects.Games.SnakeGame
{
    public class Snake
    {
        public List<Point> Body {  get; set; }
        public Point Head => Body[Body.Count-1];
        private Direction direction { get; set; }
        public Snake(int x, int y) 
        {
            Body= new List<Point> { new Point(x-1,y),new Point(x,y) };
            direction=Direction.Right;
        }

        public void Move(int width, int height)
        {
            Point newHead = new Point(Head.X,Head.Y);

            switch (direction)
            {
                case Direction.Up:
                    newHead.Y--;
                    break;
                case Direction.Down:
                    newHead.Y++;
                    break;
                case Direction.Left:
                    newHead.X--;
                    break;
                case Direction.Right:
                    newHead.X++;
                    break;
            }

            if (newHead.X>=width)
            {
                newHead.X = 0;   
            }

            else if (newHead.X<0)
            {
                newHead.X = width;
            }

            else if (newHead.Y >=height)
            {
                newHead.Y = 0;
            }

            else if (newHead.Y < 0)
            {
                newHead.Y=height;
            }
            
            Body.Add(newHead);
            Body.RemoveAt(0);
        }

        public void ChangeDirection(Direction newDirection)
        {
            if ((direction == Direction.Up && newDirection != Direction.Down) ||
                (direction == Direction.Down && newDirection != Direction.Up) ||
                (direction == Direction.Left && newDirection != Direction.Right) ||
                (direction == Direction.Right && newDirection != Direction.Left))
            {
                direction = newDirection;
            }
        }

        public void Grow()
        {
            Point newPoint;
            Point lastPoint = Body[Body.Count-1];
            switch (direction)
            {
                case Direction.Up:
                    newPoint = new Point(lastPoint.X, lastPoint.Y + 1);
                    break;
                case Direction.Down:
                    newPoint = new Point(lastPoint.X, lastPoint.Y - 1);
                    break;
                case Direction.Left:
                    newPoint = new Point(lastPoint.X + 1, lastPoint.Y);
                    break;
                case Direction.Right:
                    newPoint = new Point(lastPoint.X - 1, lastPoint.Y);
                    break;
                default:
                    newPoint = new Point(lastPoint.X, lastPoint.Y);
                    break;
            }

            Body.Insert(0,newPoint);
        }
    }
     
    public class Point
    {
        public int X {  get; set; } 
        public int Y {  get; set; }

        public Point(int x, int y)
        {
            X = x;
            Y = y;
        }
    }
}
