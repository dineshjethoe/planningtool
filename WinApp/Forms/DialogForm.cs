using System.Windows.Forms;
using Entities;
using WinApp.Interfaces;
using WinApp.Views;

namespace WinApp
{
    public partial class DialogForm : Form, IDialogFormView
    {
        private readonly TaskDialogView taskDialogView;
        private readonly EmployeeDialogView employeeDialogView;
        private readonly AssignedTaskDialogView assignedTaskDialogView;

        public DialogForm()
        {
            InitializeComponent();

            taskDialogView = new TaskDialogView() { Dock = DockStyle.Fill};
            employeeDialogView = new EmployeeDialogView() { Dock = DockStyle.Fill };
            assignedTaskDialogView = new AssignedTaskDialogView() { Dock = DockStyle.Fill };
        }

        public IDialogView<Task> TaskDialogView { get { return taskDialogView; } }
        public IDialogView<Employee> EmployeeDialogView { get { return employeeDialogView; } }
        public IDialogView<AssignedTask> AssignedTaskDialogView { get { return assignedTaskDialogView; } }

        public void ShowAssignedTaskDialogView()
        {
            panel.Controls.Clear();
            panel.Controls.Add(assignedTaskDialogView);
            this.ShowDialog();
        }

        public void ShowEmployeeDialogView()
        {
            panel.Controls.Clear();
            panel.Controls.Add(employeeDialogView);
            this.ShowDialog();
        }

        public void ShowTaskDialogView()
        {
            panel.Controls.Clear();
            panel.Controls.Add(taskDialogView);
            this.ShowDialog();
        }
    }
}
