using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace CsharpMiniProjects.MiniProjects.Tools.ToDoList
{
    public class BooleanToTextDecorationConverter : IValueConverter
    {
        private static readonly TextDecorationCollection Strikethrough = TextDecorations.Strikethrough;
        private static readonly TextDecorationCollection NoTextDecoration = new TextDecorationCollection();

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            bool isCompleted = (bool)value;
            return isCompleted ? Strikethrough : NoTextDecoration;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
