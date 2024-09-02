using System.Windows.Shapes;
using System.Windows.Controls;
using System.Windows.Media;
using System.Reflection;
using System.Windows;
using System.Windows.Media.Effects;

namespace CsharpMiniProjects.Games.SnakeGame
{
    public class BorderGridCell : Grid
    {
        private Shape shape;
        private Border border;

        public BorderGridCell()
        {
            border = new Border
            {
                BorderBrush = Brushes.White,
                BorderThickness = new Thickness(0.2),
            };

            shape = new Rectangle
            {
                Fill = new SolidColorBrush(Color.FromRgb(0x0d, 0x2d, 0x4e)),
                RadiusX = 2,
                RadiusY = 2,
                Stretch = Stretch.Fill,
                Margin = new Thickness(1)
            };
            border.Child = shape;
            Children.Add(border);
            
        }
    }
}
