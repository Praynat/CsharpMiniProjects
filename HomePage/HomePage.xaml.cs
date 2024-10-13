    
using CsharpMiniProjects.Games.SnakeGame;
using CsharpMiniProjects.MiniProjects.Games.PongGame;
using CsharpMiniProjects.MiniProjects.Tools.Countries;
using CsharpMiniProjects.Tools.WorkHoursManagementApp.Pages;
using CsharpMiniProjects.UserControls;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;


namespace CsharpMiniProjects.HomePage
{
    public partial class HomePage : Page
    {
    private string activeSection;
        public HomePage()
        {
            InitializeComponent();
            DataContext = new HomePageViewModel();
        }
        private void OnToolsButtonClick(object sender, RoutedEventArgs e)
        {
            // Slide Main out to the right and Tools in from the left
            var slideOutMain = (Storyboard)this.Resources["SlideMainOutToRight"];
            var slideInTools = (Storyboard)this.Resources["SlideToolsInFromLeft"];


            slideOutMain.Begin();
            slideInTools.Begin();
            activeSection = "Tools";
            this.Focus();

        }

        private void OnGamesButtonClick(object sender, RoutedEventArgs e)
        {
            // Slide Main out to the left and Games in from the right
            var slideOutMain = (Storyboard)this.Resources["SlideMainOutToLeft"];
            var slideInGames = (Storyboard)this.Resources["SlideGamesInFromRight"];


            slideOutMain.Begin();
            slideInGames.Begin();
            activeSection = "Games";
            this.Focus();
        }
        private void OnGamesBackButtonClick(object sender, RoutedEventArgs e)
        {
            // Slide Games out to the right and Main in from the left
            var slideOutGames = (Storyboard)this.Resources["SlideGamesOutToRight"];
            var slideInMain = (Storyboard)this.Resources["SlideMainInFromLeft"];


            slideOutGames.Begin();
            slideInMain.Begin();
        }
        private void OnToolsBackButtonClick(object sender, RoutedEventArgs e)
        {
            // Slide Tools out to the left and Main in from the right
            var slideOutTools = (Storyboard)this.Resources["SlideToolsOutToLeft"];
            var slideInMain = (Storyboard)this.Resources["SlideMainInFromRight"];


            slideOutTools.Begin();
            slideInMain.Begin();
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
                case "PongGame":
                    NavigationService.Navigate(new PongGameHomePage());
                    break;
                case "WorkHoursManagement":
                    NavigationService.Navigate(new WorkHoursHomePage());
                    break;
                case "ToDoList":
                    NavigationService.Navigate(new MiniProjects.Tools.ToDoList.ToDoListHomePage());
                    break;
                case "Countries":
                    NavigationService.Navigate(new CountriesHomePage());
                    break;
                case "ExplicitWordMonitor":
                    var mainWindow = new ExplicitWordMonitor.MainWindow();
                    mainWindow.Show();
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

                popupIconImage.Source = new BitmapImage(new Uri(itemData.IconSource, UriKind.RelativeOrAbsolute));

                if (activeSection == "Games")
                {
                    popupInfo.PlacementTarget = TitlesInGamesSection;
                    popupInfo.Placement = System.Windows.Controls.Primitives.PlacementMode.Left;
                    popupInfo.HorizontalOffset = -240;
                }
                else if (activeSection == "Tools")
                {
                    popupInfo.PlacementTarget = TitlesInToolsSection;
                    popupInfo.Placement = System.Windows.Controls.Primitives.PlacementMode.Right;
                    popupInfo.HorizontalOffset = 250;
                    popupInfo.VerticalOffset = -75;
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
