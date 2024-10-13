using System;
using System.ComponentModel;

namespace CsharpMiniProjects.MiniProjects.Tools.ToDoList
{
    internal class TaskModel : INotifyPropertyChanged
    {
        // Implement INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;

        private int _id;
        public int Id
        {
            get => _id;
            set
            {
                if (_id != value)
                {
                    _id = value;
                    OnPropertyChanged(nameof(Id));
                }
            }
        }

        private string _description;
        public string Description
        {
            get => _description;
            set
            {
                if (_description != value)
                {
                    _description = value;
                    OnPropertyChanged(nameof(Description));
                }
            }
        }

        private bool _isCompleted;
        public bool IsCompleted
        {
            get => _isCompleted;
            set
            {
                if (_isCompleted != value)
                {
                    _isCompleted = value;
                    OnPropertyChanged(nameof(IsCompleted));
                }
            }
        }

        public DateTime CreationTime { get; set; }

        public TaskModel(int id, string description)
        {
            this.Id = id;
            this.Description = description;
            this.IsCompleted = false;
            this.CreationTime = DateTime.Now;
        }

        public override string ToString()
        {
            return $"{Id}. {Description} - {CreationTime.ToString()} - Is Done: {IsCompleted}";
        }

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

            // If the IsCompleted property changes, notify that Description might need to update its TextDecorations
            if (propertyName == nameof(IsCompleted))
            {
                OnPropertyChanged(nameof(Description));
            }
        }
    }
}
