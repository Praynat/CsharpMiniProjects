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
        private bool isGamePaused;
        private bool isMouseDown = false;


        public SnakeGamePage()
        {
            InitializeComponent();
            InitializeGame();
            this.Loaded += (s, e) => this.Focus();
        }

        private void InitializeGame()
        {
            game = new Game(GridSize, GridSize);
            InitializeGameGridBg();
            InitializeGameGrid();

            gameTimer = new DispatcherTimer();
            gameTimer.Tick += GameTimer_Tick;
            gameTimer.Interval = TimeSpan.FromMilliseconds(100);

            keyPressTimer = new DispatcherTimer();
            keyPressTimer.Interval = TimeSpan.FromMilliseconds(100);
            keyPressTimer.Tick += KeyPressTimer_Tick;

            KeyDown += SnakeGamePage_KeyDown;

            // Show overlay and pause the game at the start
            PlayPauseOverlay.Visibility = Visibility.Visible;
            isGamePaused = true;
        }

        private void GameTimer_Tick(object sender, EventArgs e)
        {
            game.Update();
            UpdateUI();

            // Only set focus if the mouse hasn't been clicked
            if (!isMouseDown)
            {
                this.Focus();
            }

            if (game.GameOver)
            {
                gameTimer.Stop();
                GameOverOverlay.Visibility = Visibility.Visible;
            }
        }


        private void PlayButton_Click(object sender, RoutedEventArgs e)
        {
            // Hide the play/pause overlay and start the game
            PlayPauseOverlay.Visibility = Visibility.Collapsed;
            isGamePaused = false;
            gameTimer.Start();
        }

        private void PauseButton_Click(object sender, RoutedEventArgs e)
        {
            // Toggle pause state and show/hide the overlay
            if (isGamePaused)
            {
                PlayPauseOverlay.Visibility = Visibility.Collapsed;
                gameTimer.Start();
            }
            else
            {
                PlayPauseOverlay.Visibility = Visibility.Visible;
                gameTimer.Stop();
            }
            isGamePaused = !isGamePaused;
        }
        



        private void ResetButton_Click(object sender, RoutedEventArgs e)
        {
            game.ResetGame();
            PlayPauseOverlay.Visibility = Visibility.Collapsed;
            GameOverOverlay.Visibility = Visibility.Collapsed;
            isGamePaused = false;
            gameTimer.Start();
            UpdateUI();
        }

        private void SnakeGamePage_KeyDown(object sender, KeyEventArgs e)
        {
            
            if (isGamePaused)
                return;
            if (e.Key == Key.Space)
            {
                TogglePause(); // Handle spacebar press to toggle pause
                return; // Don't handle further keypresses if it's a pause action
            }

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
        private void TogglePause()
        {
            if (isGamePaused)
            {
                // Resume the game
                gameTimer.Start();
                PlayPauseOverlay.Visibility = Visibility.Collapsed;
                isGamePaused = false;
            }
            else
            {
                // Pause the game
                gameTimer.Stop();
                PlayPauseOverlay.Visibility = Visibility.Visible;
                isGamePaused = true;
            }
        }

        private void HandleDirectionChange(Key key)
        {
            if (isGamePaused)
                return;

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
            if (isGamePaused)
                return;

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
            GameGridBg.Children.Clear();

            for (int row = 0; row < GridSize; row++)
            {
                for (int col = 0; col < GridSize; col++)
                {
                    BorderGridCell borderGridCell = new BorderGridCell();
                    GameGridBg.Children.Add(borderGridCell);
                }
            }
        }
        private void ReturnButton_Click(object sender, RoutedEventArgs e)
        {
            // Navigate back to the HomePage
            this.NavigationService.Navigate(new HomePage.HomePage());
        }
    }
}
