using System;
using System.Threading;
using System.Windows.Forms;
using BusinessLogic.Interfaces;
using Castle.Windsor;
using Castle.Windsor.Installer;
using Entities;
using Repositories.Interfaces;
using WinApp.Commands;
using WinApp.Views;

namespace WinApp
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.SetUnhandledExceptionMode(UnhandledExceptionMode.CatchException);
            Application.ThreadException += ApplicationOnThreadException;
            AppDomain.CurrentDomain.UnhandledException += CurrentDomainOnUnhandledException;
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            var container = new WindsorContainer();
            container.Install(FromAssembly.This());

            var validateAssignmentService = container.Resolve<IValidateAssignment>();
            var systemInformationService = container.Resolve<ISystemInformationService>();
            var taskService = container.Resolve<IService<Task>>();
            var employeeService = container.Resolve<IService<Employee>>();
            var assignedTaskService = container.Resolve<IService<AssignedTask>>();

            var mainForm = new MainForm();
            var dialogForm = new DialogForm();
            var employeesDataListView = new DataListView();
            var tasksDataListView = new DataListView();
            var assignedTasksDataListView = new DataListView();
            var taskEditView = new TaskDialogView();
            var employeeEditView = new EmployeeDialogView();
            var assignedTaskEditView = new AssignedTaskDialogView();
            
            var commands = new IMenuCommand[]
            {
                new LoadTasksCommand(mainForm, container.Resolve<IService<Task>>()),
                new LoadEmployeesCommand(mainForm, container.Resolve<IService<Employee>>()),
                new LoadAssignedTasksCommand(mainForm, container.Resolve<IService<AssignedTask>>()),
                new AddCommand(dialogForm, taskService, employeeService, assignedTaskService),
                new EditCommand(dialogForm, taskService, employeeService, assignedTaskService),
                new RemoveCommand(mainForm, taskService, employeeService, assignedTaskService),
            };

            var mainFormPresenter = new MainFormPresenter(
                mainForm,
                employeesDataListView,
                tasksDataListView,
                assignedTasksDataListView,
                systemInformationService,
                commands);
            
            var editFormPresenter = new DialogFormPresenter(
                dialogForm,
                taskService,
                employeeService,
                assignedTaskService,
                systemInformationService,
                validateAssignmentService,
                commands);

            Application.Run(mainForm);
        }

        private static void CurrentDomainOnUnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            var message = String.Format("Sorry, something went wrong.\r\n" +
                "{0}\r\n" +
                "Please contact support.",
                ((Exception)e.ExceptionObject).Message);

            Console.WriteLine("ERROR {0}: {1}",
                DateTimeOffset.Now, e.ExceptionObject);

            MessageBox.Show(message, "Unexpected Error");

        }

        private static void ApplicationOnThreadException(object sender, ThreadExceptionEventArgs e)
        {
            var message = String.Format("Sorry, something went wrong.\r\n" +
              "{0}\r\n" +
              "Please contact support.",
                e.Exception.Message);

            Console.WriteLine("ERROR {0}: {1}",
                DateTimeOffset.Now, e.Exception);

            MessageBox.Show(message, "Unexpected Error");
        }
    }
}
