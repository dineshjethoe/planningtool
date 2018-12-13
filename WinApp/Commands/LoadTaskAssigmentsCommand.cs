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
        private readonly IService<AssignedTask> service;

        public LoadAssignedTasksCommand(
            IMainFormView mainFormView,
            IService<AssignedTask> service)
        {
            this.mainFormView = mainFormView;
            this.service = service;
            Icon = Properties.Resources.icons8_planner_48;
            ToolTip = MenuOption.AssignedTasks.GetAttribute<MenuOptionAttribute>().Name;
            ShortcutKey = Keys.Alt | Keys.A;
            IsEnabled = true;
        }

        public override void Execute()
        {
            mainFormView.ShowAssignedTasksDataListView();
            var assignedTasks = service.Get(a => !a.IsDeleted);
            mainFormView.AssignedTasksDataListView.SetData(assignedTasks);
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
