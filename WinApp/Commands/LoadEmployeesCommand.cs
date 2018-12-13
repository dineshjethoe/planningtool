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
    class LoadEmployeesCommand : CommandBase
    {
        private readonly IMainFormView mainFormView;
        private readonly IService<Employee> service;

        public LoadEmployeesCommand(
            IMainFormView mainFormView,
            IService<Employee> service)
        {
            this.mainFormView = mainFormView;
            this.service = service;
            Icon = Properties.Resources.icons8_permanent_job_48;
            ToolTip = MenuOption.Employees.GetAttribute<MenuOptionAttribute>().Name;
            ShortcutKey = Keys.Alt | Keys.E;
            IsEnabled = true;
        }

        public override void Execute()
        {
            mainFormView.ShowEmloyeesDataListView();
            var employees = service.Get(e => !e.IsDeleted);
            mainFormView.EmployeesDataListView.SetData(employees);
            EventAggregator.Instance.Publish(new EmployeeMessage(new Employee { FirstName = string.Empty, LastName = string.Empty }));
        }
    }
}
