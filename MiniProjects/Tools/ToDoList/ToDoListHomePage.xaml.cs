using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace CsharpMiniProjects.MiniProjects.Tools.ToDoList
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>  
    
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

        //OnTaskToggled
        //get the task (and task id) from the checkBox.DataContext (the DataContext is actually the TodoTask object)
        //excute the toggle function from the model
        private void OnTaskToggled(object sender, RoutedEventArgs e)
        {
            if (sender is CheckBox checkBox && checkBox.DataContext is TaskModel task)
            {
                _todoList.ToggleTaskIsComplete(task.Id);
            }
        }


        private void OnTextBlockMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ClickCount == 2)
            {
                //get the elements
                TextBlock textBlock = sender as TextBlock;
                StackPanel parent = textBlock.Parent as StackPanel;
                TextBox editTextBox = parent.FindName("editTaskDescription") as TextBox;
                Button btnSave = parent.FindName("btnSave") as Button;
                //hide the textBlock
                textBlock.Visibility = Visibility.Collapsed;
                //show the text Box & save button
                editTextBox.Visibility = Visibility.Visible;
                btnSave.Visibility = Visibility.Visible;
                //show int the TextBox.Text the task description
                editTextBox.Text = textBlock.Text;
            }
        }



        private void OnSaveEdit(object sender, RoutedEventArgs e)
        {
            //get the elements
            Button btnSave = sender as Button;
            StackPanel parent = btnSave.Parent as StackPanel;
            TextBox editTextBox = parent.FindName("editTaskDescription") as TextBox;
            TextBlock textBlock = parent.FindName("txtTaskDescription") as TextBlock;
            TaskModel task = textBlock.DataContext as TaskModel;

            //Hide the TextBox
            editTextBox.Visibility = Visibility.Collapsed;
            //Hide the save button
            btnSave.Visibility = Visibility.Collapsed;
            //Show the TextBlock
            textBlock.Visibility = Visibility.Visible;

            //take the TextBox.Text and put in the TextBlock.Text
            textBlock.Text = editTextBox.Text;
            _todoList.UpdateTask(task.Id, editTextBox.Text);
        }


        private void OnAddTask(object sender, RoutedEventArgs e)
        {
            //if txtNewTask.Text!=null || empty string
            if (!string.IsNullOrWhiteSpace(txtNewTask.Text))
            {
                //Create the new Task 
                TaskModel newTask = new TaskModel(_todoList.Tasks.Count + 1, txtNewTask.Text);
                _todoList.AddNewTask(newTask);
                txtNewTask.Clear();
            }

        }
    }
}