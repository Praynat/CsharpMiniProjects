    
using CsharpMiniProjects.Games.SnakeGame;
using CsharpMiniProjects.Tools.WorkHoursManagementApp.Pages;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;


namespace CsharpMiniProjects.HomePage
{
  
    public partial class HomePage : Page
    {
        public HomePage()
        {
            InitializeComponent();
        }
        private void OnToolsButtonClick(object sender, RoutedEventArgs e)
        {
            ToolsSection.Visibility = Visibility.Visible;
            MainSection.Visibility = Visibility.Collapsed;
            GamesSection.Visibility = Visibility.Collapsed;
            this.Focus();

        }

        private void OnGamesButtonClick(object sender, RoutedEventArgs e)
        {
            GamesSection.Visibility = Visibility.Visible;
            MainSection.Visibility = Visibility.Collapsed;
            ToolsSection.Visibility = Visibility.Collapsed;
            this.Focus();
        }
        private void OnGamesBackButtonClick(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new HomePage());
        }
        private void OnToolsBackButtonClick(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new HomePage());
            //GamesSection.Visibility = Visibility.Collapsed;
            //ToolsSection.Visibility = Visibility.Collapsed;
            //MainSection.Visibility = Visibility.Visible;
        }

        private static void ScrollListBox(ListBox listBox, bool scrollUp)
        {
            if (scrollUp && listBox.SelectedIndex > 0)
            {
                listBox.SelectedIndex -= 1;
            }
            else if (!scrollUp && listBox.SelectedIndex < listBox.Items.Count - 1)
            {
                listBox.SelectedIndex += 1;
            }
            listBox.ScrollIntoView(listBox.SelectedItem);
        }

        private void UpButtonGames_Click(object sender, RoutedEventArgs e)
        {
            ScrollListBox(CarouselListBox, true);
        }

        private void DownButtonGames_Click(object sender, RoutedEventArgs e)
        {
            ScrollListBox(CarouselListBox, false);
        }

        private void UpButtonTools_Click(object sender, RoutedEventArgs e)
        {
            ScrollListBox(ToolCarouselListBox, true);
        }

        private void DownButtonTools_Click(object sender, RoutedEventArgs e)
        {
            ScrollListBox(ToolCarouselListBox, false);
        }

        private void CarouselListBox_MouseWheel(object sender, MouseWheelEventArgs e)
        {
            ListBox listBox = (ListBox)sender;
            if (e.Delta > 0)
            {
                if (listBox.SelectedIndex > 0)
                {
                    listBox.SelectedIndex -= 1;
                }
            }
            else if (e.Delta < 0)
            {
                if (listBox.SelectedIndex < listBox.Items.Count - 1)
                {
                    listBox.SelectedIndex += 1;
                }
            }

            e.Handled = true;

            listBox.ScrollIntoView(listBox.SelectedItem);
        }

        // Event handler when clicking on image
        private void Image_MouseLeftButtonDown_Games(object sender, MouseButtonEventArgs e)
        {
            Border? clickedBorder = sender as Border;
            if (clickedBorder != null)
            {
                string? gameTag = clickedBorder.Tag as string; 
                if (gameTag != null)
                {
                    switch (gameTag)
                    {
                        case "SnakeGame":
                            this.NavigationService.Navigate(new SnakeGamePage());
                            break;
                        case "Game2":
                            this.NavigationService.Navigate(new HomePage());
                            break;
                        case "Game3":
                            this.NavigationService.Navigate(new HomePage());
                            break;
                        case "Game4":
                            this.NavigationService.Navigate(new HomePage());
                            break;
                        default:
                            MessageBox.Show("No page found for this game.");
                            break;
                    }
                }
            }
        }

        // Event handler for tool images
        private void Image_MouseLeftButtonDown_Tools(object sender, MouseButtonEventArgs e)
        {
            Border? clickedBorder = sender as Border;
            if (clickedBorder != null)
            {
                string? toolTag = clickedBorder.Tag as string;
                if (toolTag != null)
                {
                    switch (toolTag)
                    {
                        case "WorkHoursManagement":
                            this.NavigationService.Navigate(new WorkHoursHomePage());
                            break;
                        case "Tool2":
                            this.NavigationService.Navigate(new HomePage());
                            break;
                        default:
                            MessageBox.Show("No page found for this tool.");
                            break;
                    }
                }
            }
        }
    }
}
