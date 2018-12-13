using System.Drawing;
using System.Windows.Forms;
using Entities;

namespace WinApp.Interfaces
{
    public interface IDialogFormView
    {
        event KeyEventHandler KeyUp;

        IDialogView<Task> TaskDialogView { get; }
        IDialogView<Employee> EmployeeDialogView { get; }
        IDialogView<AssignedTask> AssignedTaskDialogView { get; }
        Color BackColor { get; set; }
        void ShowTaskDialogView();
        void ShowEmployeeDialogView();
        void ShowAssignedTaskDialogView();
    }
}
