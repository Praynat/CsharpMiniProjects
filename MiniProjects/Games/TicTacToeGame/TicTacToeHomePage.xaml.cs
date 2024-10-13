using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace CsharpMiniProjects.MiniProjects.Games.TicTacToeGame
{
    
    public partial class TicTacToeHomePage : Page
    {
        private string[,] board = new string[3, 3];
        private bool isPlayerOneTurn = true;
        private bool isGamePaused = false;
        private bool isGameOver = false;
        private bool isOnePlayerMode = true;
        private string aiDifficulty = "Medium";
        private int playerOneScore = 0;
        private int playerTwoScore = 0;

        public TicTacToeHomePage()
        {
            InitializeComponent();
            
            this.Loaded += (s, e) => this.Focus();

            ModeComboBox.SelectionChanged += ModeComboBox_SelectionChanged;
            DifficultyComboBox.SelectionChanged += DifficultyComboBox_SelectionChanged;
            this.KeyDown += TicTacToeHomePage_KeyDown;
        }

        private void InitializeGame()
        {
            try
            {
                // Clear the board
                board = new string[3, 3];
                isPlayerOneTurn = true;
                isGameOver = false;

                // Remove all children from the GameGrid
                GameGrid.Children.Clear();

                // Recreate and add the grid lines
                AddGridLines();

                // Add buttons to the grid cells
                for (int row = 0; row < 3; row++)
                {
                    for (int col = 0; col < 3; col++)
                    {
                        Button cellButton = new Button
                        {

                            FontSize = 72,
                            FontWeight = FontWeights.Bold,
                            Background = Brushes.Transparent,
                            Foreground = Brushes.Transparent,
                            BorderBrush = new SolidColorBrush(Colors.LightGray),
                            BorderThickness = new Thickness(0),
                            Content = "                                                     ",
                            HorizontalAlignment = HorizontalAlignment.Stretch,
                            VerticalAlignment = VerticalAlignment.Stretch,
                            Opacity = 1,
                            Tag = new Tuple<int, int>(row, col),
                            MinWidth = 100,
                            MinHeight = 100
                        };
                        cellButton.Click += CellButton_Click;
                        Grid.SetRow(cellButton, row);
                        Grid.SetColumn(cellButton, col);
                        Panel.SetZIndex(cellButton, 10);
                        GameGrid.Children.Add(cellButton);
                    }
                }

                UpdateScoreText();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}");
            }
        }
        private void AddGridLines()
        {
            // Add vertical lines
            for (int i = 1; i < 3; i++)
            {
                var line = new Rectangle
                {
                    Width = 2,
                    Fill = Brushes.White,
                    HorizontalAlignment = HorizontalAlignment.Left
                };
                Grid.SetColumn(line, i);
                Grid.SetRowSpan(line, 3);
                Panel.SetZIndex(line, 5);
                GameGrid.Children.Add(line);
            }

            // Add horizontal lines
            for (int i = 1; i < 3; i++)
            {
                var line = new Rectangle
                {
                    Height = 2,
                    Fill = Brushes.White,
                    VerticalAlignment = VerticalAlignment.Top
                };
                Grid.SetRow(line, i);
                Grid.SetColumnSpan(line, 3);
                Panel.SetZIndex(line, 5);
                GameGrid.Children.Add(line);
            }
        }
        private void UpdateScoreText()
        {
            ScoreText.Text = $"{playerOneScore} - {playerTwoScore}";
        }

        private void CellButton_Click(object sender, RoutedEventArgs e)
        {
            if (isGamePaused || isGameOver)
                return;

            Button clickedButton = sender as Button;
            int row = (clickedButton.Tag as Tuple<int, int>).Item1;
            int col = (clickedButton.Tag as Tuple<int, int>).Item2;

            if (string.IsNullOrEmpty(board[row, col]))
            {
                if (isPlayerOneTurn)
                {
                    board[row, col] = "X";
                    clickedButton.Content = "X";
                    clickedButton.Foreground = Brushes.Red; // Make X red
                }
                else
                {
                    board[row, col] = "O";
                    clickedButton.Content = "O";
                    clickedButton.Foreground = Brushes.Green; // Make O blue
                }

                if (CheckWin())
                {
                    GameOver(isPlayerOneTurn ? "Player 1" : "Player 2");
                    return;
                }
                else if (IsBoardFull())
                {
                    GameOver("Draw");
                    return;
                }

                isPlayerOneTurn = !isPlayerOneTurn;

                if (isOnePlayerMode && !isPlayerOneTurn)
                {
                    // AI's turn
                    AITurn();
                }
            }
        }
        private void AITurn()
        {
            // Simple AI implementation based on difficulty
            switch (aiDifficulty)
            {
                case "Easy":
                    EasyAITurn();
                    break;
                case "Medium":
                    MediumAITurn();
                    break;
                case "Hard":
                    HardAITurn();
                    break;
            }
        }

        private void EasyAITurn()
        {
            // Random move
            Random rand = new Random();
            int row, col;
            do
            {
                row = rand.Next(0, 3);
                col = rand.Next(0, 3);
            } while (!string.IsNullOrEmpty(board[row, col]));

            MakeAIMove(row, col);
        }

        private void MediumAITurn()
        {
            // Try to win, otherwise random
            if (!TryWinOrBlock("O"))
            {
                EasyAITurn();
            }
        }

        private void HardAITurn()
        {
            // Try to win, block player, otherwise random
            if (!TryWinOrBlock("O"))
            {
                if (!TryWinOrBlock("X"))
                {
                    EasyAITurn();
                }
            }
        }

        private bool TryWinOrBlock(string symbol)
        {
            // Check rows and columns and diagonals for potential win/block
            for (int i = 0; i < 3; i++)
            {
                // Check rows
                if (CountSymbolInLine(symbol, i, 0, 0, 1) == 2)
                {
                    if (MakeMoveInLine(i, 0, 0, 1))
                        return true;
                }
                // Check columns
                if (CountSymbolInLine(symbol, 0, i, 1, 0) == 2)
                {
                    if (MakeMoveInLine(0, i, 1, 0))
                        return true;
                }
            }
            // Check diagonals
            if (CountSymbolInLine(symbol, 0, 0, 1, 1) == 2)
            {
                if (MakeMoveInLine(0, 0, 1, 1))
                    return true;
            }
            if (CountSymbolInLine(symbol, 0, 2, 1, -1) == 2)
            {
                if (MakeMoveInLine(0, 2, 1, -1))
                    return true;
            }
            return false;
        }

        private int CountSymbolInLine(string symbol, int startRow, int startCol, int rowStep, int colStep)
        {
            int count = 0;
            for (int i = 0; i < 3; i++)
            {
                int row = startRow + i * rowStep;
                int col = startCol + i * colStep;
                if (board[row, col] == symbol)
                    count++;
            }
            return count;
        }

        private bool MakeMoveInLine(int startRow, int startCol, int rowStep, int colStep)
        {
            for (int i = 0; i < 3; i++)
            {
                int row = startRow + i * rowStep;
                int col = startCol + i * colStep;
                if (string.IsNullOrEmpty(board[row, col]))
                {
                    MakeAIMove(row, col);
                    return true;
                }
            }
            return false;
        }

        private void MakeAIMove(int row, int col)
        {
            board[row, col] = "O";
            foreach (UIElement element in GameGrid.Children)
            {
                if (element is Button button)
                {
                    int buttonRow = Grid.GetRow(button);
                    int buttonCol = Grid.GetColumn(button);
                    if (buttonRow == row && buttonCol == col)
                    {
                        button.Content = "O";
                        button.Foreground = Brushes.Green; 
                        break;
                    }
                }
            }


            if (CheckWin())
            {
                GameOver("AI");
                return;
            }
            else if (IsBoardFull())
            {
                GameOver("Draw");
                return;
            }

            isPlayerOneTurn = !isPlayerOneTurn;
        }

        private bool CheckWin()
        {
            // Check rows, columns, and diagonals
            for (int i = 0; i < 3; i++)
            {
                // Rows
                if (!string.IsNullOrEmpty(board[i, 0]) &&
                    board[i, 0] == board[i, 1] && board[i, 1] == board[i, 2])
                {
                    return true;
                }
                // Columns
                if (!string.IsNullOrEmpty(board[0, i]) &&
                    board[0, i] == board[1, i] && board[1, i] == board[2, i])
                {
                    return true;
                }
            }
            // Diagonals
            if (!string.IsNullOrEmpty(board[0, 0]) &&
                board[0, 0] == board[1, 1] && board[1, 1] == board[2, 2])
            {
                return true;
            }
            if (!string.IsNullOrEmpty(board[0, 2]) &&
                board[0, 2] == board[1, 1] && board[1, 1] == board[2, 0])
            {
                return true;
            }
            return false;
        }

        private bool IsBoardFull()
        {
            foreach (var cell in board)
            {
                if (string.IsNullOrEmpty(cell))
                    return false;
            }
            return true;
        }

        private void GameOver(string winner)
        {
            isGameOver = true;
            if (winner == "Player 1")
            {
                playerOneScore++;
                MessageBox.Show("Player 1 Wins!");
            }
            else if (winner == "Player 2")
            {
                playerTwoScore++;
                MessageBox.Show("Player 2 Wins!");
            }
            else if (winner == "AI")
            {
                playerTwoScore++;
                MessageBox.Show("AI Wins!");
            }
            else
            {
                MessageBox.Show("It's a Draw!");
            }
            UpdateScoreText();
            GameOverOverlay.Visibility = Visibility.Visible;
        }

        private void PlayButton_Click(object sender, RoutedEventArgs e)
        {
            // Start the game
            StartOverlay.Visibility = Visibility.Collapsed;
            GameGrid.Visibility = Visibility.Visible;
            isGamePaused = false;
            InitializeGame();
            this.Focus();
        }

        private void ResetButton_Click(object sender, RoutedEventArgs e)
        {
            InitializeGame();
            isGamePaused = false;
            isGameOver = false;
            PauseOverlay.Visibility = Visibility.Collapsed;
            GameOverOverlay.Visibility = Visibility.Collapsed;
            StartOverlay.Visibility = Visibility.Collapsed;
        }
        private void ExitButton_Click(object sender, RoutedEventArgs e)
        {
            InitializeGame();
            StartOverlay.Visibility = Visibility.Visible;
            PauseOverlay.Visibility = Visibility.Collapsed;
            GameOverOverlay.Visibility = Visibility.Collapsed;
        }

        private void PauseButton_Click(object sender, RoutedEventArgs e)
        {
            TogglePause();
        }

        private void ResumeButton_Click(object sender, RoutedEventArgs e)
        {
            TogglePause();
        }

        private void TogglePause()
        {
            if (isGamePaused)
            {
                PauseOverlay.Visibility = Visibility.Collapsed;
                isGamePaused = false;
            }
            else
            {
                PauseOverlay.Visibility = Visibility.Visible;
                isGamePaused = true;
            }
        }

        private void TicTacToeHomePage_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Space)
            {
                TogglePause();
            }
            else if (e.Key == Key.Escape)
            {
                this.NavigationService.Navigate(new HomePage.HomePage());
            }
        }

        private void ReturnButton_Click(object sender, RoutedEventArgs e)
        {
            if (this.NavigationService.CanGoBack)
            {
                this.NavigationService.GoBack();
            }
            else
            {
                this.NavigationService.Navigate(new HomePage.HomePage());
            }
        }


        private void ModeComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (ModeComboBox.SelectedIndex == 0)
            {
                isOnePlayerMode = true;
                DifficultyPanel.Visibility = Visibility.Visible;
            }
            else
            {
                isOnePlayerMode = false;
                DifficultyPanel.Visibility = Visibility.Collapsed;
            }
        }

        private void DifficultyComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (DifficultyComboBox.SelectedItem is ComboBoxItem selectedItem)
            {
                aiDifficulty = selectedItem.Content.ToString();
            }
        }
    }
}
