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
    class LoadTasksCommand : CommandBase
    {
        private readonly IMainFormView mainFormView;
        private readonly IService<Task> service;

        public LoadTasksCommand(
            IMainFormView mainFormView,
            IService<Task> service)
        {
            this.mainFormView = mainFormView;
            this.service = service;

            Icon = Properties.Resources.icons8_list_48;
            ToolTip = MenuOption.Tasks.GetAttribute<MenuOptionAttribute>().Name;
            ShortcutKey = Keys.Alt | Keys.T;
            IsEnabled = true;
        }

        public override void Execute()
        {
            mainFormView.ShowTasksDataListView();
            var tasks = service.Get(t => !t.IsDeleted);
            mainFormView.TasksDataListView.SetData(tasks);
            EventAggregator.Instance.Publish(new TaskMessage(new Task { TaskDescription = string.Empty, TaskDate = DateTime.Now }));
        }
    }
}
