using System.Collections.ObjectModel;
using System.Windows.Documents;

public class HomePageViewModel
{
    // Collection for games
    public ObservableCollection<ListBoxItemData> GameItems { get; set; }

    // Collection for tools
    public ObservableCollection<ListBoxItemData> ToolItems { get; set; }

    public HomePageViewModel()
    {
        // Populate GameItems collection
        GameItems = new ObservableCollection<ListBoxItemData>
    {
        new ListBoxItemData
        {
            Title = "Snake Game",
            ImageSource = "pack://application:,,,/Ressources/Images/SnakeGamePicture.png",
            IconSource = "pack://application:,,,/Ressources/Images/SnakeGameIcon.png",
            PopupTitle = "Snake Game",
            PopupDescriptionInlines = new Inline[]
            {
                new Run("Control the snake using the arrow keys."),
                new LineBreak(),
                new Run("Try to eat as much food as possible without hitting the walls."),
                new LineBreak(),
                new Bold(new Run("Score points")),
                new Run(" by collecting food. Avoid the obstacles!")
            },
            Identifier = "SnakeGame" 
        },
        new ListBoxItemData
        {
            Title = "Tetris",
           ImageSource = "pack://application:,,,/Ressources/Images/SnakeGamePicture.png",
            IconSource = "pack://application:,,,/Ressources/Images/SnakeGameIcon.png",
            PopupTitle = "Tetris Instructions",
            PopupDescriptionInlines = new Inline[]
            {
                new Run("Arrange blocks to form complete lines."),
                new LineBreak(),
                new Bold(new Run("Clear as many lines")),
                new Run(" as you can to earn a high score.")
            },
            Identifier = "Tetris" 
        }
    };

        // Populate ToolItems collection
        ToolItems = new ObservableCollection<ListBoxItemData>
    {
        new ListBoxItemData
        {
            Title = "Work Hours\nManagement",
            ImageSource = "pack://application:,,,/Ressources/Images/WorkHoursManagementPicture.png",
            IconSource = "pack://application:,,,/Ressources/Images/WorkHoursManagementIcon.png",
            PopupTitle = "Manage Your Work Hours",
            PopupDescriptionInlines = new Inline[]
            {
                new Run("Easily manage, "),
                new Bold(new Run("set")),
                 new Run(" and track your "),
                new Bold(new Run("daily work hours")),
                new Run("."),
                new LineBreak(),
                new Run("Add "),
                new Bold(new Run("bonus time")),
                new Run(" or adjust for "),
                new Bold(new Run("missed hours")),
                new Run("."),
                new LineBreak(),
                new Bold(new Run("Calculate")), new Run(" total hours across "),
                new Bold(new Run("date ranges")), new Run(" for accurate results."),
                new LineBreak(),
                new Bold(new Run("Set")), new Run(" your "),
                new Bold(new Run("hourly fee")), new Run(" and calculate total salary.")
            },
            Identifier = "WorkHoursManagement" 
        },
        new ListBoxItemData
        {
            Title = "Explicit Words Monitor",
            ImageSource = "pack://application:,,,/Ressources/Images/ExplicitWordFilterPicture.jpg",
            IconSource = "pack://application:,,,/Ressources/Images/ExplicitWordFilterIcon.png",
            PopupTitle = "Filter Explicit Words",
            PopupDescriptionInlines = new Inline[]
            {
                new Run("Monitor your daily, weekly, and monthly expenses."),
                new LineBreak(),
                new Bold(new Run("Set budgets")),
                new Run(" and get detailed reports.")
            },
            Identifier = "ExplicitWordMonitor",
            TitleFontSize = 25
        },
        new ListBoxItemData
        {
            Title = "To Do List",
              ImageSource = "pack://application:,,,/Ressources/Images/ExplicitWordFilterPicture.jpg",
            IconSource = "pack://application:,,,/Ressources/Images/ExplicitWordFilterIcon.png",
            PopupTitle = "Track Your Expenses",
            PopupDescriptionInlines = new Inline[]
            {
                new Run("Manage your"),
                new Bold(new Run("Tasks")),
            },
            Identifier = "ToDoList"
        }
    };
    }

}
