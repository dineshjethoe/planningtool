using BusinessLogic.Services;
using Entities;
using Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using WinApp.Attributes;
using WinApp.Enums;
using WinApp.EventMessages;
using WinApp.Interfaces;
using WinApp.Util;

namespace WinApp.Commands
{
    class AddCommand : CommandBase
    {
        private readonly IDialogFormView dialogFormView;
        private readonly IService<Task> taskService;
        private readonly IService<Employee> employeeService;
        private readonly IService<AssignedTask> assignedTaskService;

        public AddCommand(
            IDialogFormView dialogFormView,
            IService<Task> taskService,
            IService<Employee> employeeService,
            IService<AssignedTask> assignedTaskService)
        {
            this.dialogFormView = dialogFormView;
            this.taskService = taskService;
            this.employeeService = employeeService;
            this.assignedTaskService = assignedTaskService;

            Icon = Properties.Resources.icons8_plus_math_48;
            ToolTip = MenuOption.Add.GetAttribute<MenuOptionAttribute>().Name;
            ShortcutKey = Keys.Alt | Keys.N;
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
            var isEditingMode = false;

            switch (Tag)
            {
                case Task t when (t != null):
                    dialogFormView.TaskDialogView.IsEditing = isEditingMode;
                    dialogFormView.TaskDialogView.SetData(new Task
                    {
                        TaskDescription = string.Empty, TaskDate = DateTime.Now
                    });
                    dialogFormView.ShowTaskDialogView();
                    break;
                case Employee e when (e != null):
                    dialogFormView.EmployeeDialogView.IsEditing = isEditingMode;
                    dialogFormView.EmployeeDialogView.SetData(new Employee
                    {
                        FirstName = string.Empty, LastName = string.Empty
                    });
                    dialogFormView.ShowEmployeeDialogView();
                    break;
                case AssignedTask a when (a != null):
                    dialogFormView.AssignedTaskDialogView.IsEditing = isEditingMode;
                    dialogFormView.AssignedTaskDialogView.SetData(new AssignedTask
                    {
                        AssignmentDate = DateTime.Now,
                        StartTime = DateTime.Now.TimeOfDay,
                        EndTime = DateTime.Now.TimeOfDay,
                    });
                    dialogFormView.ShowAssignedTaskDialogView();
                    break;
                default:
                    //unknown Tag
                    break;
                case null:
                    throw new ArgumentNullException(nameof(Tag));
            }
        }
    }
}
