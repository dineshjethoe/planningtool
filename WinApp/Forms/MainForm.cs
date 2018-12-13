using System.Windows.Forms;
using WinApp.Commands;
using WinApp.Interfaces;
using WinApp.Views;

namespace WinApp
{
    public partial class MainForm : Form, IMainFormView
    {
        private readonly DataListView employeesDataListView;
        private readonly DataListView tasksDataListView;
        private readonly DataListView assignedTasksDataListView;

        public MainForm()
        {
            InitializeComponent();

            DoubleBuffered = true;
            employeesDataListView = new DataListView() { Dock = DockStyle.Fill };
            tasksDataListView = new DataListView() { Dock = DockStyle.Fill };
            assignedTasksDataListView = new DataListView() { Dock = DockStyle.Fill };
        }

        public IMenuView MenuView { get { return menuView; } }
        public IDataListView EmployeesDataListView { get { return employeesDataListView; } }
        public IDataListView TasksDataListView { get { return tasksDataListView; } }
        public IDataListView AssignedTasksDataListView { get { return assignedTasksDataListView; } }

        public void ShowEmloyeesDataListView()
        {
            panel.Controls.Clear();
            panel.Controls.Add(employeesDataListView);
        }

        public void ShowTasksDataListView()
        {
            panel.Controls.Clear();
            panel.Controls.Add(tasksDataListView);
        }

        public void ShowAssignedTasksDataListView()
        {
            panel.Controls.Clear();
            panel.Controls.Add(assignedTasksDataListView);
        }
    }
}
