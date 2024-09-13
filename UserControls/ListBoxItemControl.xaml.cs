using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using Xceed.Wpf.Toolkit;

namespace CsharpMiniProjects.UserControls
{
    public partial class ListBoxItemControl : UserControl
    {   
        // Custom events to notify when hover on item
        public event EventHandler<ItemEventArgs> ItemMouseEnter;
        public event EventHandler<ItemEventArgs> ItemMouseLeave;

        // Custom event to notify when the item is clicked
        public event EventHandler ItemClicked;
        public ListBoxItemControl()
        {
            InitializeComponent();
            
        }
        private void Border_MouseEnter(object sender, MouseEventArgs e)
        {
            // Raise the ItemMouseEnter event with the current item's data
            ItemMouseEnter?.Invoke(this, new ItemEventArgs(this.DataContext as ListBoxItemData));
        }

        private void Border_MouseLeave(object sender, MouseEventArgs e)
        {
            // Raise the ItemMouseLeave event with the current item's data
            ItemMouseLeave?.Invoke(this, new ItemEventArgs(this.DataContext as ListBoxItemData));
        }

        private void Border_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            // Raise the ItemClicked event
            ItemClicked?.Invoke(this, EventArgs.Empty);
        }

        // A custom EventArgs to pass item data
        public class ItemEventArgs : EventArgs
        {
            public ListBoxItemData ItemData { get; }

            public ItemEventArgs(ListBoxItemData itemData)
            {
                ItemData = itemData;
            }
        }
    }
}
