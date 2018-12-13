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
    class EditCommand : CommandBase
    {
        private readonly IDialogFormView dialogFormView;
        private readonly IService<Task> taskService;
        private readonly IService<Employee> employeeService;
        private readonly IService<AssignedTask> assignedTaskService;

        public EditCommand(
            IDialogFormView dialogFormView,
            IService<Task> taskService,
            IService<Employee> employeeService,
            IService<AssignedTask> assignedTaskService)
        {
            this.dialogFormView = dialogFormView;
            this.taskService = taskService;
            this.employeeService = employeeService;
            this.assignedTaskService = assignedTaskService;

            Icon = Properties.Resources.icons8_pencil_48;
            ToolTip = MenuOption.Edit.GetAttribute<MenuOptionAttribute>().Name;
            ShortcutKey = Keys.Alt | Keys.U;
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
            var isEditingMode = true;

            switch (Tag)
            {
                case Task t when (t != null):
                    dialogFormView.TaskDialogView.IsEditing = isEditingMode;
                    var task = taskService.GetById(t.Id);
                    dialogFormView.TaskDialogView.SetData(task);
                    dialogFormView.ShowTaskDialogView();
                    break;
                case Employee e when (e != null):
                    dialogFormView.EmployeeDialogView.IsEditing = isEditingMode;
                    var employee = employeeService.GetById(e.Id);
                    dialogFormView.EmployeeDialogView.SetData(employee);
                    dialogFormView.ShowEmployeeDialogView();
                    break;
                case AssignedTask a when (a != null):
                    dialogFormView.AssignedTaskDialogView.IsEditing = isEditingMode;
                    var assignedTask = assignedTaskService.GetById(a.Id);
                    dialogFormView.AssignedTaskDialogView.SetData(assignedTask);
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
