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
    class LoadAssignedTasksCommand : CommandBase
    {
        private readonly IMainFormView mainFormView;
        private readonly IDialogFormView dialogFormView;
        private readonly IDialogView<AssignedTask> assignedTaskDialogView;
        private readonly IService<Task> taskService;
        private readonly IService<Employee> employeeService;
        private readonly IService<AssignedTask> assignedTaskService;

        public LoadAssignedTasksCommand(
            IMainFormView mainFormView,
            IDialogFormView dialogFormView,
            IDialogView<AssignedTask> assignedTaskDialogView,
             IService<Task> taskService,
            IService<Employee> employeeService,
            IService<AssignedTask> assignedTaskService)
        {
            this.mainFormView = mainFormView;
            this.dialogFormView = dialogFormView;
            this.assignedTaskDialogView = assignedTaskDialogView;
            this.taskService = taskService;
            this.employeeService = employeeService;
            this.assignedTaskService = assignedTaskService;
            Icon = Properties.Resources.icons8_planner_48;
            ToolTip = MenuOption.AssignedTasks.GetAttribute<MenuOptionAttribute>().Name;
            ShortcutKey = Keys.Alt | Keys.A;
            IsEnabled = true;
        }

        public override void Execute()
        {
            assignedTaskDialogView.LoadEmployees(employeeService.Get(e => !e.IsDeleted));
            assignedTaskDialogView.LoadTasks(taskService.Get(t => !t.IsDeleted));
            var assignedTasks = assignedTaskService.Get(a => !a.IsDeleted, null, "Employee, Task");
            mainFormView.AssignedTasksDataListView.SetData(assignedTasks);

            //This line of code let the Add, Edit and Remove button target (point to) task assignment module
            //So when click on the Add, Edit or Remove button then the operation is performed for task assignment module
            mainFormView.ShowAssignedTasksDataListView();
            EventAggregator.Instance.Publish(
                new AssignedTaskMessage(
                    new AssignedTask
                    {
                        AssignmentDate = DateTime.Now,
                        StartTime = DateTime.Now.TimeOfDay,
                        EndTime = DateTime.Now.TimeOfDay,
                    }));
        }
    }
}
