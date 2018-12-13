using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using BusinessLogic.Services;
using Entities;
using WinApp.EventMessages;
using WinApp.Interfaces;

namespace WinApp.Views
{
    public partial class AssignedTaskDialogView : UserControl, IDialogView<AssignedTask>
    {
        public bool IsEditing { get; set; }
        public AssignedTask Current { get; set; }

        public event EventHandler OnSaveButtonClick;
        public event EventHandler OnCancelButtonClick;

        public AssignedTaskDialogView()
        {
            InitializeComponent();
        }

        public void LoadEmployees(IList<Employee> employees)
        {
            var employeeList = employees
                                .Select(e => new { e.Id, e.FirstName, e.LastName, FullName = $"{e.FirstName} {e.LastName}" })
                                .ToList();
            comboBoxEmployees.DataSource = employeeList;
            comboBoxEmployees.DisplayMember = "FullName";
            comboBoxEmployees.ValueMember = "Id";
        }

        public void LoadTasks(IList<Task> tasks)
        {
            comboBoxTasks.DataSource = tasks;
            comboBoxTasks.DisplayMember = "TaskDescription";
            comboBoxTasks.ValueMember = "Id";
        }

        public void SetData(AssignedTask assignedTask)
        {
            this.labelTitle.Text = (IsEditing) ? "Edit Assignment" : "New Assignment";
            errorProvider1.Clear();
            Current = assignedTask ?? new AssignedTask
            {
                AssignmentDate = DateTime.Now,
                StartTime = DateTime.Now.TimeOfDay,
                EndTime = DateTime.Now.TimeOfDay,
            };
            comboBoxTasks.SelectedValue = assignedTask.TaskId;
            comboBoxEmployees.SelectedValue = assignedTask.EmployeeId;
            dateTimePickerAssignmentDate.Value = assignedTask.AssignmentDate;
            var today = DateTime.Now.Date;
            timePickerStartTime.Value = today + assignedTask.StartTime;
            timePickerEndTime.Value = today + assignedTask.EndTime;
        }

        public AssignedTask GetData()
        {
            errorProvider1.Clear();
            bool hasError = false;

            int taskId = 0;
            if (comboBoxTasks.SelectedValue == null || !Int32.TryParse(comboBoxTasks.SelectedValue.ToString(), out taskId))
            {
                errorProvider1.SetError(comboBoxTasks, "Please select a task.");
                hasError = true;
            }

            int employeeId = 0;
            if (comboBoxEmployees.SelectedValue == null || !Int32.TryParse(comboBoxEmployees.SelectedValue.ToString(), out employeeId))
            {
                errorProvider1.SetError(comboBoxEmployees, "Please select an employee.");
                hasError = true;
            }

            if (timePickerStartTime.Value.TimeOfDay >= timePickerEndTime.Value.TimeOfDay)
            {
                errorProvider1.SetError(timePickerEndTime, "The end time should be greater than the end start time.");
                hasError = true;
            }

            if (hasError)
                return null;

            Current.TaskId = taskId;
            Current.EmployeeId = employeeId;

            Current.AssignmentDate = dateTimePickerAssignmentDate.Value;
            Current.StartTime = timePickerStartTime.Value.TimeOfDay;
            Current.EndTime = timePickerEndTime.Value.TimeOfDay;

            return Current;
        }

        public void SetTitle(string title)
        {
            this.labelTitle.Text = title;
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            OnSaveButtonClick?.Invoke(sender, e);
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            OnCancelButtonClick?.Invoke(sender, e);
        }
    }
}
