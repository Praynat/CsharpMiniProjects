
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Threading;

namespace CsharpMiniProjects.Games.SnakeGame
{
    public partial class SnakeGamePage : Page
    {
        private Game game;
        private DispatcherTimer gameTimer;
        private DispatcherTimer keyPressTimer;
        public const int GridSize = 20;
        private Direction pendingDirection;
        private bool hasPendingDirection;

        public  SnakeGamePage()
        {
            InitializeComponent();
            InitializeGame();
            this.Focus();
        }

        private void InitializeGame()
        {
            game = new Game(GridSize, GridSize);
            InitializeGameGridBg();
            InitializeGameGrid();

            gameTimer = new DispatcherTimer();
            gameTimer.Tick += GameTimer_Tick;
            gameTimer.Interval = TimeSpan.FromMilliseconds(100);
            gameTimer.Start();

            keyPressTimer = new DispatcherTimer();
            keyPressTimer.Interval = TimeSpan.FromMilliseconds(100);
            keyPressTimer.Tick += KeyPressTimer_Tick;

            KeyDown += SnakeGamePage_KeyDown;
        }

        private void InitializeGameGrid()
        {
            GameGrid.Rows = GridSize;
            GameGrid.Columns = GridSize;

            GameGrid.Children.Clear();

            for (int row = 0; row < GridSize; row++)
            {
                for (int col = 0; col < GridSize; col++)
                {
                    CellState initialState = game.GetCellState(col, row);
                    GridCell gridCell = new GridCell(initialState, GridSize);
                    GameGrid.Children.Add(gridCell);
                }
            }
        }

        private void InitializeGameGridBg()
        {
            GameGridBg.Rows = GridSize;
            GameGridBg.Columns = GridSize;

            for (int row = 0; row < GridSize; row++)
            {
                for (int col = 0; col < GridSize; col++)
                {
                    BorderGridCell borderGridCell = new BorderGridCell();
                    GameGridBg.Children.Add(borderGridCell);
                }
            }
        }

        private void GameTimer_Tick(object sender, EventArgs e)
        {
            game.Update();
            UpdateUI();
            this.Focus();
            if (game.GameOver)
            {
                gameTimer.Stop();
                GameOverOverlay.Visibility = Visibility.Visible;
            }

        }

        private void SnakeGamePage_KeyDown(object sender, KeyEventArgs e)
        {
            if (keyPressTimer.IsEnabled)
            {
                switch (e.Key)
                {
                    case Key.Up:
                        pendingDirection = Direction.Up;
                        break;
                    case Key.Down:
                        pendingDirection = Direction.Down;
                        break;
                    case Key.Left:
                        pendingDirection = Direction.Left;
                        break;
                    case Key.Right:
                        pendingDirection = Direction.Right;
                        break;
                    default:
                        return;
                }
                hasPendingDirection = true;
                return;
            }

            HandleDirectionChange(e.Key);
            keyPressTimer.Start();

        }

        public void HandleDirectionChange(Key key)
        {
            switch (key)
            {
                case Key.Up:
                    game.ChangeDirection(Direction.Up);
                    break;
                case Key.Down:
                    game.ChangeDirection(Direction.Down);

                    break;
                case Key.Left:
                    game.ChangeDirection(Direction.Left);
                    break;
                case Key.Right:
                    game.ChangeDirection(Direction.Right);
                    break;
                case Key.Escape:
                    this.NavigationService.Navigate(new HomePage.HomePage());
                    break;
            }
        }

        private void KeyPressTimer_Tick(object sender, EventArgs e)
        {
            keyPressTimer.Stop();
            if (hasPendingDirection)
            {
                game.ChangeDirection(pendingDirection);
                hasPendingDirection = false;
            }
        }

        private void UpdateUI()
        {
            for (int row = 0; row < GridSize; row++)
            {
                for (int col = 0; col < GridSize; col++)
                {
                    int index = row * GridSize + col;
                    GridCell gridCell = (GridCell)GameGrid.Children[index];
                    gridCell.SetCellState(game.GetCellState(col, row), GridSize);
                }
            }
            ScoreText.Text = game.Score.ToString();
        }

        private void ResetButton_Click(object sender, RoutedEventArgs e)
        {
            game.ResetGame();
            GameOverOverlay.Visibility = Visibility.Collapsed;
            gameTimer.Start();
            UpdateUI();
        }
        private void ReturnButton_Click(object sender, RoutedEventArgs e)
        {
            // Navigate back to the HomePage
            this.NavigationService.Navigate(new HomePage.HomePage());
        }

    }
}
