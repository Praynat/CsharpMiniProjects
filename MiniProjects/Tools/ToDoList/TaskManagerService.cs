using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CsharpMiniProjects.MiniProjects.Tools.ToDoList
{
    internal class TaskManagerService
    {
        public ObservableCollection<TaskModel> Tasks { get; set; }

        public TaskManagerService()
        {
            Tasks = new ObservableCollection<TaskModel>();
        }

        public void UpdateTask(int taskId, string newDescription)
        {
            TaskModel task = Tasks.FirstOrDefault(task => task.Id == taskId);
            if (task != null)
            {
                task.Description = newDescription;
            }
            else
            {
                throw new Exception("The task with this Id wasn't found");
            }
        }
        //ToggleTaskIsComplete(int id)
        public void ToggleTaskIsComplete(int taskId)
        {
            TaskModel task = Tasks.FirstOrDefault(task => task.Id == taskId);
            if (task != null)
            {
                task.IsCompleted = !task.IsCompleted;
            }
            else
            {
                throw new Exception("The task with this Id wasn't found");
            }
        }
        //AddNewTask(task)
        public void AddNewTask(TaskModel task)
        {
            Tasks.Add(task);
        }

    }
}
