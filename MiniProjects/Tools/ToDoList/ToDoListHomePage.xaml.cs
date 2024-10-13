using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace CsharpMiniProjects.MiniProjects.Tools.ToDoList
{
    public partial class ToDoListHomePage : Page
    {
        private TaskManagerService _todoList;

        public ToDoListHomePage()
        {
            InitializeComponent();
            InitializeTasks();
        }

        private void InitializeTasks()
        {
            _todoList = new TaskManagerService();
            _todoList.AddNewTask(new TaskModel(1, "To do homework"));
            _todoList.AddNewTask(new TaskModel(2, "Complete the project"));
            listTasks.ItemsSource = _todoList.Tasks;
        }

        // OnTaskToggled
        private void OnTaskToggled(object sender, RoutedEventArgs e)
        {
            if (sender is CheckBox checkBox && checkBox.DataContext is TaskModel task)
            {
                _todoList.ToggleTaskIsComplete(task.Id);
            }
        }

        

        private void OnSaveEdit(object sender, RoutedEventArgs e)
        {
            // Get the elements
            Button btnSave = sender as Button;
            StackPanel parent = btnSave.Parent as StackPanel;
            TextBox editTextBox = parent.FindName("editTaskDescription") as TextBox;
            TextBlock textBlock = parent.FindName("txtTaskDescription") as TextBlock;
            Button btnDelete = parent.FindName("btnDelete") as Button;
            TaskModel task = textBlock.DataContext as TaskModel;

            // Hide the TextBox and buttons
            editTextBox.Visibility = Visibility.Collapsed;
            btnSave.Visibility = Visibility.Collapsed;
            btnDelete.Visibility = Visibility.Collapsed;
            // Show the TextBlock
            textBlock.Visibility = Visibility.Visible;

            // Update the task description
            textBlock.Text = editTextBox.Text;
            _todoList.UpdateTask(task.Id, editTextBox.Text);
        }

        private void OnDeleteTask(object sender, RoutedEventArgs e)
        {
            // Get the TaskModel associated with the clicked delete button
            if (sender is Button deleteButton && deleteButton.DataContext is TaskModel task)
            {
                // Confirm deletion (optional)
                MessageBoxResult result = MessageBox.Show($"Are you sure you want to delete the task '{task.Description}'?", "Delete Task", MessageBoxButton.YesNo, MessageBoxImage.Warning);
                if (result == MessageBoxResult.Yes)
                {
                    _todoList.RemoveTask(task.Id);
                }
            }
        }

        private void OnAddTask(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(txtNewTask.Text))
            {
                int newId = _todoList.Tasks.Any() ? _todoList.Tasks.Max(t => t.Id) + 1 : 1;
                TaskModel newTask = new TaskModel(newId, txtNewTask.Text);
                _todoList.AddNewTask(newTask);
                txtNewTask.Clear();
            }
        }

        private void OnEditTextBoxLostFocus(object sender, RoutedEventArgs e)
        {
            TextBox editTextBox = sender as TextBox;
            StackPanel parent = editTextBox.Parent as StackPanel;
            TextBlock textBlock = parent.FindName("txtTaskDescription") as TextBlock;
            Button btnSave = parent.FindName("btnSave") as Button;
            Button btnDelete = parent.FindName("btnDelete") as Button;

            
            editTextBox.Visibility = Visibility.Collapsed;
            btnSave.Visibility = Visibility.Collapsed;
            btnDelete.Visibility = Visibility.Collapsed;
            
            textBlock.Visibility = Visibility.Visible;

            if (textBlock.Text != editTextBox.Text)
            {
                textBlock.Text = editTextBox.Text;
                if (textBlock.DataContext is TaskModel task)
                {
                    _todoList.UpdateTask(task.Id, editTextBox.Text);
                }
            }
        }

        private void OnTaskDoubleClicked(object sender, MouseButtonEventArgs e)
        {
            if (sender is ListBoxItem listBoxItem)
            {
                
                e.Handled = true;

                var contentPresenter = FindVisualChild<ContentPresenter>(listBoxItem);
                if (contentPresenter != null)
                {
                   
                    var dataTemplateRoot = VisualTreeHelper.GetChild(contentPresenter, 0);

                
                    var textBlock = FindVisualChildByName<TextBlock>(dataTemplateRoot, "txtTaskDescription");
                    var editTextBox = FindVisualChildByName<TextBox>(dataTemplateRoot, "editTaskDescription");
                    var btnSave = FindVisualChildByName<Button>(dataTemplateRoot, "btnSave");
                    var btnDelete = FindVisualChildByName<Button>(dataTemplateRoot, "btnDelete");

                    if (textBlock != null && editTextBox != null && btnSave != null && btnDelete != null)
                    {
                        
                        textBlock.Visibility = Visibility.Collapsed;
                     
                        editTextBox.Visibility = Visibility.Visible;
                        btnSave.Visibility = Visibility.Visible;
                        btnDelete.Visibility = Visibility.Visible;

                     
                        editTextBox.Text = textBlock.Text;
                       
                        editTextBox.Focus();
                    }
                }
            }
        }

        private T FindVisualChild<T>(DependencyObject parent) where T : DependencyObject
        {
            for (int i = 0; i < VisualTreeHelper.GetChildrenCount(parent); i++)
            {
                DependencyObject child = VisualTreeHelper.GetChild(parent, i);
                if (child != null && child is T tChild)
                    return tChild;
                else
                {
                    T childOfChild = FindVisualChild<T>(child);
                    if (childOfChild != null)
                        return childOfChild;
                }
            }
            return null;
        }

        private T FindVisualChildByName<T>(DependencyObject parent, string name) where T : DependencyObject
        {
            if (parent == null)
                return null;

            for (int i = 0; i < VisualTreeHelper.GetChildrenCount(parent); i++)
            {
                var child = VisualTreeHelper.GetChild(parent, i);
                if (child is FrameworkElement frameworkElement && frameworkElement.Name == name)
                {
                    return (T)child;
                }

                var result = FindVisualChildByName<T>(child, name);
                if (result != null)
                    return result;
            }
            return null;
        }

        private void Grid_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            
            var focusedElement = Keyboard.FocusedElement as DependencyObject;
            if (focusedElement != null)
            {
               
                if (focusedElement is TextBox textBox && textBox.Name == "editTaskDescription")
                {
                   
                    Keyboard.ClearFocus();
                }
            }
        }


        private T FindVisualParent<T>(DependencyObject child) where T : DependencyObject
        {
            DependencyObject parent = VisualTreeHelper.GetParent(child);
            while (parent != null && !(parent is T))
            {
                parent = VisualTreeHelper.GetParent(parent);
            }
            return parent as T;
        }



        private void ReturnButton_Click(object sender, RoutedEventArgs e)
        {
            // Navigate back or to the desired page
            if (NavigationService.CanGoBack)
            {
                NavigationService.GoBack();
            }
            else
            {
                NavigationService.Navigate(new HomePage.HomePage());
            }
        }
    }
}
