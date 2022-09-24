using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskCmdLineManager
{
    internal class Task
    {
        // Fields
        private string _taskDescription;
        private bool _isCompleted;

        // Property
        public string TaskDescription { get; set; }
        public bool IsCompleted { get; set; }

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
    }
}
