using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Media3D;

namespace CsharpMiniProjects.Games.SnakeGame
{
    public enum CellState { Empty, Snake, Apple }
    public enum Direction { Up, Down, Left, Right }
    internal class Game
    {
        private Snake snake;

        private Apple apple;
        private Random random;

        private int Width;
        private int Height;

        public int Score { get; private set; }
        public bool GameOver { get; private set; }


        public Game(int  width, int height)
        {
            Width = width;  
            Height = height;
            ResetGame();
        }

        public void ResetGame()
        {
            snake = new Snake(Width/2, Height/2);
            random= new Random();
            GenerateApple();
            Score = 0;
            GameOver = false;
        }

        public CellState GetCellState(int x, int y)
        {
            if (snake.Body.Any(p=>p.X==x && p.Y==y))
                return CellState.Snake;
            if (apple.X==x&&apple.Y==y)
                return CellState.Apple;
            return CellState.Empty;

        }

        public void Update()
        {
            snake.Move(Width,Height);
            if (snake.Head.X==apple.X&&snake.Head.Y==apple.Y)
            {
                snake.Grow();
                GenerateApple();
                Score++;
            }

            if (IsGameOver())
            {
                GameOver = true;
            }
        }

        public void ChangeDirection(Direction direction)
        {
            snake.ChangeDirection(direction);
        }

        public void GenerateApple()
        {
            do
            {
                apple = new Apple(random.Next(Width), random.Next(Height));
            } while (snake.Body.Any(p=>p.X==apple.X && p.Y == apple.Y));
        }

        private bool IsGameOver()
        {
            

            bool collidesWithBody = false;
            for (int i = 0; i < snake.Body.Count - 2; i++)
            {
                if (snake.Body[i].X == snake.Head.X && snake.Body[i].Y == snake.Head.Y)
                {
                    collidesWithBody = true;
                    break;
                }
            }

            return collidesWithBody;
        }
    }
}
