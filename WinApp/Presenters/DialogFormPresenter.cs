using BusinessLogic.Interfaces;
using Entities;
using Repositories.Interfaces;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using WinApp.Attributes;
using WinApp.Commands;
using WinApp.Enums;
using WinApp.Interfaces;
using WinApp.Util;

namespace WinApp
{
    class DialogFormPresenter
    {
        private readonly IDialogFormView dialogFormView;
        private readonly IDialogView<Task> taskDialogView;
        private readonly IDialogView<Employee> employeeDialogView;
        private readonly IDialogView<AssignedTask> assignedTaskDialogView;
        private readonly IService<Task> taskService;
        private readonly IService<Employee> employeeService;
        private readonly IService<AssignedTask> assignedTaskService;
        private readonly IValidateAssignment validateAssignment;

        private readonly IMenuCommand[] commands;

        public DialogFormPresenter(
            IDialogFormView dialogFormView,
            IService<Task> taskService,
            IService<Employee> employeeService,
            IService<AssignedTask> assignedTaskService,
            ISystemInformationService systemInformationService,
            IValidateAssignment validateAssignment,
            IMenuCommand[] commands)
        {
            this.dialogFormView = dialogFormView;

            this.taskDialogView = dialogFormView.TaskDialogView;
            this.taskDialogView.SetTitle("Task");

            this.employeeDialogView = dialogFormView.EmployeeDialogView;
            this.employeeDialogView.SetTitle("Employee");

            this.assignedTaskDialogView = dialogFormView.AssignedTaskDialogView;
            this.assignedTaskDialogView.SetTitle("Assigned Task");

            this.taskDialogView.OnCancelButtonClick += OnCancelButtonClick;
            this.taskDialogView.OnSaveButtonClick += TaskEditFormView_OnSaveButtonClick;

            this.employeeDialogView.OnCancelButtonClick += OnCancelButtonClick; ;
            this.employeeDialogView.OnSaveButtonClick += EmployeeEditView_OnSaveButtonClick; ;

            this.assignedTaskDialogView.OnCancelButtonClick += OnCancelButtonClick; ;
            this.assignedTaskDialogView.OnSaveButtonClick += AssignedTaskEditView_OnSaveButtonClick; ;

            this.taskService = taskService;
            this.employeeService = employeeService;
            this.assignedTaskService = assignedTaskService;

            this.assignedTaskDialogView.LoadEmployees(employeeService.Get(e => !e.IsDeleted));
            this.assignedTaskDialogView.LoadTasks(taskService.Get(t => !t.IsDeleted));

            this.validateAssignment = validateAssignment;

            this.commands = commands;

            if (!systemInformationService.IsHighContrastColourScheme)
            {
                dialogFormView.BackColor = Color.White;
            }
        }

        public bool ValidateAssignment(AssignedTask assignedTask)
        {
            var assignmentIsValid = true;
            var maxHoursPerDay = 8;
            var maxDaysPerWeek = 5;

            var assignments = assignedTaskService.Get(a => a.EmployeeId == assignedTask.EmployeeId
                                    && a.AssignmentDate.Date == assignedTask.AssignmentDate.Date);

            //Check if the employee has assigments already for the selected assigment date
            bool assigneeHasAlreadyAssignments = validateAssignment.AssigneeHasAlreadyAssignmentsOnDate(assignedTask, assignments);

            if (assigneeHasAlreadyAssignments)
            {
                //The employee has assigment(s) already, so check if the selected start time is before the end time of the last assignment 
                //taking into consideration that a task is assigned in sequential order
                var IsTheStartTimeValid = validateAssignment.IsTheStartTimeOfTheNewAssignmentGreaterThanTheEndTimeOfTheLastAssignment(assignedTask, assignments);

                if (!IsTheStartTimeValid)
                {
                    MessageBox.Show("The selected start time should be greater than the end time of the last assignment.");
                    return false;
                }
            }
            else 
            {
                //The employee has no assignment for the selected assignment date
                //so to check if the selected assigment exceeds the maximum working days per week for the employee,
                //take the week and count the working days 
                var firstDateOfWeek = validateAssignment.GetStartDateOfWeek(assignedTask);
                var lastDateOfWeek = validateAssignment.GetEndDateOfWeek(assignedTask);

                var assignmentsOfTheWeek = assignedTaskService.Get(a => a.EmployeeId == assignedTask.EmployeeId
                                    && a.AssignmentDate.Date >= firstDateOfWeek
                                    && a.AssignmentDate.Date <= lastDateOfWeek);

                //An employee can have more than one assignment per day, 
                //so we have to count the unique days, not the assigments
                var assignedDaysPerWeek = (from temp in assignmentsOfTheWeek
                                           select temp.AssignmentDate.Date).Distinct().Count();

                var assignmentExceedsMaxDaysPerWeek = validateAssignment.AssignmentExceedsMaxDaysPerWeek(assignedDaysPerWeek, maxDaysPerWeek);

                if(assignmentExceedsMaxDaysPerWeek)
                {
                    MessageBox.Show($"The maximum days per week is exceeded. The limit is {maxDaysPerWeek} days per week.");
                    return false;
                }
            }

            //If both the above mentioned cases passes then rest 
            //to check if the duration between the selected start time and end time exceeds the maximum working hours per day or not
            var totalHoursAssignedAlready = validateAssignment.GetTotalAssignedHoursOfEmployeeOnDate(assignedTask, assignments);
            var IsMaxHoursPerDayExceeded = validateAssignment.ExceedsMaxHoursPerDay(assignedTask, totalHoursAssignedAlready, maxHoursPerDay);

            if (IsMaxHoursPerDayExceeded)
            {
                MessageBox.Show($"The maximum hours to work on the assignment is exceeded. The limit is {maxHoursPerDay}.");
                return false;
            }

            return assignmentIsValid;
        }

        private void AssignedTaskEditView_OnSaveButtonClick(object sender, System.EventArgs e)
        {
            var assignedTask = assignedTaskDialogView.GetData();

            if (assignedTask == null)
                return;

            if (!ValidateAssignment(assignedTask))
                return;

            if (assignedTaskDialogView.IsEditing)
            {
                assignedTaskService.Update(assignedTask);
            }
            else
            {
                assignedTaskService.Create(assignedTask);
            }

            ExecuteAndClose(MenuOption.AssignedTasks.GetAttribute<MenuOptionAttribute>().Name, sender as Button);
        }

        private void EmployeeEditView_OnSaveButtonClick(object sender, System.EventArgs e)
        {
            var employee = employeeDialogView.GetData();

            if (employee == null)
                return;

            if (employeeDialogView.IsEditing)
            {
                employeeService.Update(employee);
            }
            else
            {
                employeeService.Create(employee);
            }

            ExecuteAndClose(MenuOption.Employees.GetAttribute<MenuOptionAttribute>().Name, sender as Button);
        }

        private void TaskEditFormView_OnSaveButtonClick(object sender, System.EventArgs e)
        {
            var task = taskDialogView.GetData();

            if (task == null)
                return;

            if (taskDialogView.IsEditing)
            {
                taskService.Update(task);
            }
            else
            {
                taskService.Create(task);
            }

            ExecuteAndClose(MenuOption.Tasks.GetAttribute<MenuOptionAttribute>().Name, sender as Button);
        }

        private void OnCancelButtonClick(object sender, System.EventArgs e)
        {
            CloseForm(sender as Button);
        }

        private void ExecuteAndClose(string command, Control control)
        {
            var cmd = commands.FirstOrDefault(c => c.ToolTip == command);

            if (cmd != null)
            {
                cmd.Execute();
            }

            CloseForm(control);
        }

        private void CloseForm(Control control)
        {
            if (control.Parent?.Parent?.Parent?.Parent is DialogForm)
            {
                (control.Parent.Parent.Parent.Parent as DialogForm).Close();
            }
        }
    }
}
