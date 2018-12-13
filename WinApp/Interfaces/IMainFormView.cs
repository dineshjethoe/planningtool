using System.Drawing;
using System.Windows.Forms;
using WinApp.Commands;

namespace WinApp.Interfaces
{
    public interface IMainFormView
    {
        event KeyEventHandler KeyUp;

        IDataListView EmployeesDataListView { get; }

        IDataListView TasksDataListView { get; }

        IDataListView AssignedTasksDataListView { get; }

        IMenuView MenuView { get; }

        Color BackColor { get; set; }

        void ShowEmloyeesDataListView();

        void ShowTasksDataListView();

        void ShowAssignedTasksDataListView();
    }
}
