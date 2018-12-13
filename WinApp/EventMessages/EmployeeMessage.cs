using BusinessLogic;
using Entities;

namespace WinApp.EventMessages
{
    public class EmployeeMessage : Message<Employee>
    {
        public EmployeeMessage(Employee employee) : base(employee)
        {
        }
    }
}
