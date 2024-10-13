using CsharpMiniProjects.MiniProjects.Games.PongGame.Models;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace CsharpMiniProjects.MiniProjects.Games.PongGame
{
    public partial class PongGameHomePage : Page
    {
        private Game game;

        public PongGameHomePage()
        {
            InitializeComponent();

            // Initialize paddles and ball
            GamePaddle paddle1 = new GamePaddle(this.Paddle1, GameCanvas);
            GamePaddle paddle2 = new GamePaddle(this.Paddle2, GameCanvas);
            GameBall gameBall = new GameBall(this.Ball, GameCanvas);

            // Initialize game
            game = new Game(GameCanvas, paddle1, paddle2, gameBall, ScoreText1, ScoreText2,
                            LeftWallTop, LeftWallBottom, RightWallTop, RightWallBottom);

            // Attach key event handlers to GameCanvas
            GameCanvas.KeyDown += GameCanvas_KeyDown;
            GameCanvas.KeyUp += GameCanvas_KeyUp;

            // Ensure GameCanvas is focusable
            GameCanvas.Focusable = true;

            CompositionTarget.Rendering += GameLoop;
        }



        private void GameLoop(object sender, EventArgs e)
        {
            game.GameLoop();

        }

        private void PlayButton_Click(object sender, RoutedEventArgs e)
        {
            MainMenuGrid.Visibility = Visibility.Collapsed;
            PauseMenuGrid.Visibility = Visibility.Collapsed;
            PauseButton.Visibility = Visibility.Visible;
            game.StartGame();
           
            // Set focus back to the GameCanvas
            GameCanvas.Focus();
            Keyboard.Focus(GameCanvas);
        }
        private void Button_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Space || e.Key == Key.Enter)
            {
                e.Handled = true; // Prevent the button from handling the key press
            }
        }

        private void PauseButton_Click(object sender, RoutedEventArgs e)
        {
            game.PauseGame();

            if (game.IsPaused)
            {
                PauseMenuGrid.Visibility = Visibility.Visible;
                PauseButton.Visibility = Visibility.Collapsed;
                DifficultyComboBox.IsEnabled = false;
            }
            else
            {
                PauseMenuGrid.Visibility = Visibility.Collapsed;
                PauseButton.Visibility = Visibility.Visible;
                DifficultyComboBox.IsEnabled = true;
            }
        }

        private void ResetBallButton_Click(object sender, RoutedEventArgs e)
        {
            game.ResetBall();
        }

        private void ExitButton_Click(object sender, RoutedEventArgs e)
        {
            game.PauseGame();
            PauseMenuGrid.Visibility = Visibility.Collapsed;
            MainMenuGrid.Visibility = Visibility.Visible;
            PauseButton.Visibility = Visibility.Collapsed;
            DifficultyComboBox.IsEnabled = true;


            game.ScorePlayer1 = 0;
            game.ScorePlayer2 = 0;

            ScoreText1.Text = "0";
            ScoreText2.Text = "0";

            game.ResetBall();
            UpdateDifficultyPanelVisibility();
            DifficultyComboBox.Focus();
            game.IsPaused = true;
        }

        private void GameCanvas_KeyDown(object sender, KeyEventArgs e)
        {
            game.OnKeyDown(e.Key);
            e.Handled = true; // Prevent further processing
        }

        private void GameCanvas_KeyUp(object sender, KeyEventArgs e)
        {
            game.OnKeyUp(e.Key);
            e.Handled = true;
        }

      private void GameModeComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // Skip if game or combo box is null
            if (game == null || GameModeComboBox == null || DifficultyPanel == null)
            {
                return;
            }

            var selectedItem = GameModeComboBox.SelectedItem as ComboBoxItem;
            if (selectedItem != null)
            {
                string selectedContent = selectedItem.Content.ToString();
                if (selectedContent == "Two Players")
                {
                    game.IsTwoPlayer = true;
                    DifficultyPanel.Visibility = Visibility.Collapsed;
                }
                else if (selectedContent == "One Player")
                {
                    game.IsTwoPlayer = false;
                    DifficultyPanel.Visibility = Visibility.Visible;
                }
            }
        }

        private void DifficultyComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // Skip if game or combo box is null
            if (game == null || DifficultyComboBox == null)
            {
                return;
            }

            var selectedItem = DifficultyComboBox.SelectedItem as ComboBoxItem;
            if (selectedItem != null)
            {
                string content = selectedItem.Content.ToString();
                switch (content)
                {
                    case "Easy":
                        game.DifficultyLevel = 1;
                        break;
                    case "Medium":
                        game.DifficultyLevel = 2;
                        break;
                    case "Hard":
                        game.DifficultyLevel = 3;
                        break;
                }
            }
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            this.Focus();
            Keyboard.Focus(this);
        }

        private void UpdateDifficultyPanelVisibility()
        {
            var selectedItem = GameModeComboBox.SelectedItem as ComboBoxItem;
            if (selectedItem != null)
            {
                if (selectedItem.Content.ToString() == "Two Players")
                {
                    game.IsTwoPlayer = true;
                    DifficultyPanel.Visibility = Visibility.Collapsed;
                }
                else if (selectedItem.Content.ToString() == "One Player")
                {
                    game.IsTwoPlayer = false;
                    DifficultyPanel.Visibility = Visibility.Visible;
                }
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
    }
}
