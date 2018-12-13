using System;
using System.Windows.Forms;
using BusinessLogic.Services;
using Entities;
using Repositories.Interfaces;
using WinApp.Attributes;
using WinApp.Enums;
using WinApp.EventMessages;
using WinApp.Interfaces;
using WinApp.Util;

namespace WinApp.Commands
{
    class RemoveCommand : CommandBase
    {
        private readonly IMainFormView mainFormView;
        private readonly IService<Task> taskService;
        private readonly IService<Employee> employeeService;
        private readonly IService<AssignedTask> assignedTaskService;

        public RemoveCommand(
            IMainFormView mainFormView,
            IService<Task> taskService,
            IService<Employee> employeeService,
            IService<AssignedTask> assignedTaskService)
        {
            this.mainFormView = mainFormView;
            this.taskService = taskService;
            this.employeeService = employeeService;
            this.assignedTaskService = assignedTaskService;

            Icon = Properties.Resources.icons8_trash_can_48;
            ToolTip = MenuOption.Remove.GetAttribute<MenuOptionAttribute>().Name;
            ShortcutKey = Keys.Alt | Keys.X;
            IsEnabled = false;
            Tag = null;

            EventAggregator.Instance.Subscribe<TaskMessage>(e => IsEnabled = true);
            EventAggregator.Instance.Subscribe<TaskMessage>(e => Tag = e.Entity);
            EventAggregator.Instance.Subscribe<EmployeeMessage>(e => IsEnabled = true);
            EventAggregator.Instance.Subscribe<EmployeeMessage>(e => Tag = e.Entity);
            EventAggregator.Instance.Subscribe<AssignedTaskMessage>(e => IsEnabled = true);
            EventAggregator.Instance.Subscribe<AssignedTaskMessage>(e => Tag = e.Entity);
        }

        public override void Execute()
        {
            switch (Tag)
            {
                case Task t when (t != null):
                    if (ConfirmBox($"Are you sure you want to remove task id {t.Id}?"))
                    {
                        taskService.Delete(t.Id);
                        var tasks = taskService.Get(task => !task.IsDeleted);
                        mainFormView.TasksDataListView.SetData(tasks);
                        mainFormView.ShowTasksDataListView();
                    }
                    break;
                case Employee e when (e != null):
                    if (ConfirmBox($"Are you sure you want to remove employee id {e.Id}?"))
                    {
                        employeeService.Delete(e.Id);
                        var employees = employeeService.Get(emp => !emp.IsDeleted);
                        mainFormView.EmployeesDataListView.SetData(employees);
                        mainFormView.ShowEmloyeesDataListView();
                    }
                    break;
                case AssignedTask a when (a != null):
                    if (ConfirmBox($"Are you sure you want to remove the task assignment with Id {a.Id}?"))
                    {
                        assignedTaskService.Delete(a.Id);
                        var assignedTasks = assignedTaskService.Get(at => !at.IsDeleted);
                        mainFormView.AssignedTasksDataListView.SetData(assignedTasks);
                        mainFormView.ShowAssignedTasksDataListView();
                    }
                    break;
                default:
                    //unknown Tag
                    break;
                case null:
                    throw new ArgumentNullException(nameof(Tag));
            }
        }

        private bool ConfirmBox(string message)
        {
            DialogResult rs = MessageBox.Show(message, "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            return (rs == DialogResult.Yes);
        }
    }
}
