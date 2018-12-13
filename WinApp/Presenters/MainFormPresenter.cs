using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using BusinessLogic.Interfaces;
using BusinessLogic.Services;
using Entities;
using WinApp.Commands;
using WinApp.EventMessages;
using WinApp.Interfaces;

namespace WinApp
{
    class MainFormPresenter
    {
        private readonly IMainFormView mainFormView;
        private readonly IDataListView employeesDataListView;
        private readonly IDataListView tasksDataListView;
        private readonly IDataListView assignedTasksDataListView;
        private readonly ISystemInformationService systemInforamtionService;
        private readonly IMenuView menuView;
        private readonly IMenuCommand[] commands;

        public MainFormPresenter(IMainFormView mainFormView,
            IDataListView employeesDataListView,
            IDataListView tasksDataListView,
            IDataListView assignedTasksDataListView,
            ISystemInformationService systemInformationService,
            IMenuCommand[] commands)
        {
            this.mainFormView = mainFormView;
            this.commands = commands;

            menuView = mainFormView.MenuView;
            menuView.SetCommands(commands);

            employeesDataListView = mainFormView.EmployeesDataListView;
            employeesDataListView.SetTitle("Employees");
            employeesDataListView.OnDataGridViewCellClick += OnDataGridViewCellClick;
            tasksDataListView = mainFormView.TasksDataListView;
            tasksDataListView.SetTitle("Tasks");
            tasksDataListView.OnDataGridViewCellClick += OnDataGridViewCellClick;
            assignedTasksDataListView = mainFormView.AssignedTasksDataListView;
            assignedTasksDataListView.SetTitle("Assigned Tasks");
            assignedTasksDataListView.OnDataGridViewCellClick += OnDataGridViewCellClick;

            mainFormView.KeyUp += MainFormViewOnKeyUp;

            if (!systemInformationService.IsHighContrastColourScheme)
            {
                mainFormView.BackColor = Color.White;
            }
        }

        private void OnDataGridViewCellClick(object sender, DataGridViewCellEventArgs e)
        {
            DataGridView dgv = sender as DataGridView;

            if (dgv == null)
                return;

            if (e.RowIndex > -1)
            {
                PublishMessage(dgv.Rows[e.RowIndex].DataBoundItem);
            }
        }

        private void PublishMessage(object dataBoundItem)
        {
            switch (dataBoundItem)
            {
                case Task t when (t != null):
                    EventAggregator.Instance.Publish(new TaskMessage(t));
                    break;
                case Employee e when (e != null):
                    EventAggregator.Instance.Publish(new EmployeeMessage(e));
                    break;
                case AssignedTask a when (a != null):
                    EventAggregator.Instance.Publish(new AssignedTaskMessage(a));
                    break;
                default:
                    //unknown object
                    break;
                case null:
                    throw new ArgumentNullException(nameof(dataBoundItem));
            }
        }

        private void MainFormViewOnKeyUp(object sender, KeyEventArgs keyEventArgs)
        {
            var command = commands.FirstOrDefault(c => c.ShortcutKey == keyEventArgs.KeyCode);
            if (command != null)
            {
                command.Execute();
                keyEventArgs.Handled = true;
            }
        }
    }
}
