using BusinessLogic;
using Entities;

namespace WinApp.EventMessages
{
    public class AssignedTaskMessage : Message<AssignedTask>
    {
        public AssignedTaskMessage(AssignedTask assignedTask) : base(assignedTask)
        {
        }
    }
}
