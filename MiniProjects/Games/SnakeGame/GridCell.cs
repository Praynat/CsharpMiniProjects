using System.Windows.Shapes;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows;
using System.Windows.Media.Imaging;
using System.Windows.Media.Media3D;

namespace CsharpMiniProjects.Games.SnakeGame
{
    public class GridCell : Grid
    {
        private Shape shape;
        private Border border;
        private Direction direction { get; set; }


        public GridCell(CellState initialState, int size)
        {
            

            shape = new Rectangle
            {
                Fill = Brushes.Transparent,
                Stretch = Stretch.Fill
            };

           
            Children.Add(shape);
            SetCellState(initialState, size);
        }

        public void SetCellState(CellState state,int size)
        {
            Children.Clear();

            

            switch (state)
            {
                case CellState.Empty:
                    shape = new Rectangle
                    {
                        Fill = Brushes.Transparent,
                        Stretch = Stretch.Fill

                       
                    };
                    break;
                case CellState.Snake:
                    shape = new Rectangle
                    {
                        Fill = Brushes.LightCyan,
                        Stretch = Stretch.Fill,
                        Width = 10,
                        Height=10,
                        RadiusX = 2,
                        RadiusY = 2,

                    };
                    break;
                case CellState.Apple:
                    shape = new Ellipse
                    {
                        Fill = new ImageBrush
                        {
                            ImageSource = new BitmapImage(new Uri("pack://application:,,,/MiniProjects/Games/SnakeGame/myApple.png"))
,
                        },
                        Width = 400/size,
                        Height = 400 / size,
                        Stretch = Stretch.UniformToFill,
                        HorizontalAlignment = HorizontalAlignment.Center,
                        VerticalAlignment = VerticalAlignment.Center
                    };
                    break;
            }
           
            Children.Add(shape); 
        }
    }
}
