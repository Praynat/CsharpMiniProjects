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
                new Bold(new Run("Control ")),
                new Run(" the snake by using the arrow keys."),
                new LineBreak(),
                new Bold(new Run("Score points")),
                new Run(" by collecting food."),
                new LineBreak(),
                new Run("But beware! "),
                new Bold(new Run("Colliding ")),
                new Run(" with yourself will make you lose"),
                new LineBreak(),
            },
            Identifier = "SnakeGame" 
        },
        new ListBoxItemData
        {
            Title = "Pong Game",
           ImageSource = "pack://application:,,,/Ressources/Images/PongGame.jpg",
            IconSource = "pack://application:,,,/Ressources/Images/PongGameIcon.png",
            PopupTitle = "The Pong Game",
            PopupDescriptionInlines = new Inline[]
            {
                new Run("The Famous Classic Pong Game with a "),
                new Bold(new Run("twist!")),
                new LineBreak(),
                new Run("Make the ball go "),
                new Bold(new Run("faster ")),
                new Run("by tappping it with the paddle."),
                new LineBreak(),
                new Run("Play against your "),
                new Bold(new Run("friend")),
                new Run(" or against the "),
                new Bold(new Run("AI.")),
            },
            Identifier = "PongGame" 
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
                new Run("This app automatically "),
                new Bold(new Run("detects")),
                new Run(" explicit words from user input and internet DNS requests and "),
                new Bold(new Run("blocks the app")),
                new LineBreak(),
                new Run("There are built-in word lists in "),
                new Bold(new Run("French, English, and Hebrew.")),
                new Run(" as well as a customizable word list with password protection."),
                new LineBreak(),
                new Run("A password ensures the app cannot be closed, providing extra "),
                new Bold(new Run("protection for kids."))
            },
            Identifier = "ExplicitWordMonitor",
            TitleFontSize = 25
},

        new ListBoxItemData
        {
            Title = "To Do List",
            ImageSource = "pack://application:,,,/Ressources/Images/ToDoListPicture.png",
            IconSource = "pack://application:,,,/Ressources/Images/ToDoListIcon.png",
            PopupTitle = "Track Your Expenses",
            PopupDescriptionInlines = new Inline[]
            {
                new Run("Manage your"),
                new Bold(new Run("Tasks")),
            },
            Identifier = "ToDoList"
        },
        new ListBoxItemData
        {
            Title = "Countries Encyclopedia",
              ImageSource = "pack://application:,,,/Ressources/Images/CountriesEncyclopediaPicture.png",
            IconSource = "pack://application:,,,/Ressources/Images/CountriesEncyclopediaIcon.png",
            PopupTitle = "Countries Encyclopedia",
            PopupDescriptionInlines = new Inline[]
            {
                new Bold(new Run("Visualize ")),
                new Run("all existing countries."),
                new LineBreak(),
                new Bold(new Run("Explore ")),
                new Run("all you have always wanted to know about any country that exists"),
            },
            Identifier = "Countries"
        }
    };
    }

}
