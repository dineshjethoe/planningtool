using BusinessLogic;
using Entities;

namespace WinApp.EventMessages
{
    public class TaskMessage : Message<Task>
    {
        public TaskMessage(Task task) : base(task)
        {
        }
    }
}
