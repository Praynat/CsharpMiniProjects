using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Media;

namespace CsharpMiniProjects.MiniProjects.Tools.ExplicitWordMonitor.Helpers
{
    class ToastHelper
    {
        public static void ShowToast(string message, Window owner)
        {
            // Create a toast notification popup
            Popup toastPopup = new Popup
            {
                PlacementTarget = owner,
                Placement = PlacementMode.Center,
                StaysOpen = false,
                AllowsTransparency = true,
                PopupAnimation = PopupAnimation.Fade,
                IsOpen = true
            };

            // Create a border for the toast
            Border border = new Border
            {
                Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#F8CBA1")),
                CornerRadius = new CornerRadius(5),
                BorderBrush = Brushes.White,
                BorderThickness = new Thickness(2),
                Padding = new Thickness(10),
                Child = new TextBlock
                {
                    Text = message,
                    Foreground = Brushes.Black,
                    FontWeight = FontWeights.DemiBold,
                    TextWrapping = TextWrapping.Wrap,
                    FontSize = 14
                }
            };

            toastPopup.Child = border;

            // Set popup duration and fade out
            var timer = new System.Windows.Threading.DispatcherTimer { Interval = TimeSpan.FromSeconds(3) };
            timer.Tick += (s, e) =>
            {
                toastPopup.IsOpen = false;
                timer.Stop();
            };
            toastPopup.IsOpen = true;
            timer.Start();
        }
    }
}
