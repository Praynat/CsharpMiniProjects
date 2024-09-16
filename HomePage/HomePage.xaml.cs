    
using CsharpMiniProjects.Games.SnakeGame;
using CsharpMiniProjects.Tools.WorkHoursManagementApp.Pages;
using CsharpMiniProjects.UserControls;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;


namespace CsharpMiniProjects.HomePage
{

    public partial class HomePage : Page
    {
        public HomePage()
        {
            InitializeComponent();
            DataContext = new HomePageViewModel();
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
            // Access the ScrollViewer within the ListBox
            var scrollViewer = FindVisualChild<ScrollViewer>(listBox);

            if (scrollViewer != null)
            {
                // Scroll up or down by a small offset
                if (scrollUp)
                {
                    scrollViewer.LineUp();
                }
                else
                {
                    scrollViewer.LineDown();
                }
            }
        }


        private void UpButtonGames_Click(object sender, RoutedEventArgs e)
        {
            ScrollListBox(GameCarouselListBox, true);
        }

        private void DownButtonGames_Click(object sender, RoutedEventArgs e)
        {
            ScrollListBox(GameCarouselListBox, false);
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
            var scrollViewer = FindVisualChild<ScrollViewer>(listBox);

            if (scrollViewer != null)
            {
                if (e.Delta > 0) // Scroll up
                {
                    scrollViewer.LineUp();
                }
                else if (e.Delta < 0) // Scroll down
                {
                    scrollViewer.LineDown();
                }

                e.Handled = true; // Mark the event as handled to prevent further propagation
            }
        }

        // Helper function to find the ScrollViewer inside the ListBox
        private static T FindVisualChild<T>(DependencyObject parent) where T : DependencyObject
        {
            for (int i = 0; i < VisualTreeHelper.GetChildrenCount(parent); i++)
            {
                var child = VisualTreeHelper.GetChild(parent, i);
                if (child != null && child is T)
                {
                    return (T)child;
                }

                var childOfChild = FindVisualChild<T>(child);
                if (childOfChild != null)
                {
                    return childOfChild;
                }
            }
            return null;
        }

        private void ListBoxItemControl_ItemClicked(object sender, EventArgs e)
        {
            // Get the data context of the clicked item
            var control = sender as ListBoxItemControl;
            var data = control?.DataContext as ListBoxItemData;

            if (data != null)
            {
                // Determine which page to navigate to based on data.Title or other properties
                NavigateToPage(data);
            }
        }

        private void NavigateToPage(ListBoxItemData itemData)
        {
            if (itemData == null)
            {
                MessageBox.Show("Item data is null.");
                return;
            }

            switch (itemData.Identifier)
            {
                case "SnakeGame":
                    NavigationService.Navigate(new SnakeGamePage());
                    break;
                case "Tetris":
                    NavigationService.Navigate(new SnakeGamePage());
                    break;
                case "WorkHoursManagement":
                    NavigationService.Navigate(new WorkHoursHomePage());
                    break;
                case "ExplicitWordMonitor":
                    NavigationService.Navigate(new ExplicitWordMonitor.HomePage.WordFilterHomePage());
                    break;
                default:
                    MessageBox.Show("No page associated with this item.");
                    break;
            }
        }

        private void ListBoxItemControl_Loaded(object sender, RoutedEventArgs e)
        {
            // Get the control that was just loaded
            var control = sender as ListBoxItemControl;

            // Subscribe to the custom events for mouse enter and leave
            if (control != null)
            {
                control.ItemMouseEnter += ListBoxItemControl_ItemMouseEnter;
                control.ItemMouseLeave += ListBoxItemControl_ItemMouseLeave;
            }
        }

        // Event handler for mouse enter to show the popup
        private void ListBoxItemControl_ItemMouseEnter(object sender, ListBoxItemControl.ItemEventArgs e)
        {
            var itemData = e.ItemData;
            if (itemData != null)
            {
                // Set the Popup content
                popupTitleTextBlock.Text = itemData.PopupTitle;
                popupDescriptionTextBlock.Inlines.Clear();
                foreach (var inline in itemData.PopupDescriptionInlines)
                {
                    popupDescriptionTextBlock.Inlines.Add(inline);
                }

                if (GamesSection.Visibility == Visibility.Visible)
                {
                    popupInfo.PlacementTarget = TitlesInGamesSection;
                    popupInfo.Placement = System.Windows.Controls.Primitives.PlacementMode.Left;
                    popupInfo.HorizontalOffset = -240;
                }
                else if (ToolsSection.Visibility == Visibility.Visible)
                {
                    popupInfo.PlacementTarget = TitlesInToolsSection;
                    popupInfo.Placement = System.Windows.Controls.Primitives.PlacementMode.Right;
                    popupInfo.HorizontalOffset = 250;
                }
                popupInfo.IsOpen = true;
            }
        }

        // Event handler for mouse leave to hide the popup
        private void ListBoxItemControl_ItemMouseLeave(object sender, ListBoxItemControl.ItemEventArgs e)
        {
            popupInfo.IsOpen = false;
        }


    }

}
