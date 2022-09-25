using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Channels;
using System.Threading.Tasks;

namespace TaskCmdLineManager
{
    internal class Task
    {
        // Fields
        private string _taskDescription;
        private bool _isCompleted;

        // Property
        public string TaskDescription
        {
            get { return _taskDescription; }
            set { _taskDescription = value; }
        }
        public bool IsCompleted
        {
            get { return _isCompleted; }
            set { _isCompleted = value; }
        }

        // Default Constructor
        public Task()
        {
            this.TaskDescription = "Default";
            this.IsCompleted = false;
        }

        // Constructor
        public Task(string taskDesc, bool isComp)
        {
            this.TaskDescription = taskDesc;
            this.IsCompleted = isComp;
        }

        // class Task methods
        public static Task Initialize(string taskDesc, bool isComp)
        {
            return new Task(taskDesc, isComp);
        }

        public void ShowTask()
        {
            Console.WriteLine($"| Task: {TaskDescription}");
            Console.WriteLine($"| Status: {ShowCompletion()}");
        }

        private string ShowCompletion()
        {
            if (this.IsCompleted == true)
            {
                return "[X]";
            }
            else
            {
                return "[ ]";
            }
        }

        public void ToggleCompletion()
        {
            this.IsCompleted = !IsCompleted;
        }
    }
}
