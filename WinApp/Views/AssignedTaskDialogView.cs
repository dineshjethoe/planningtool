using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Entities;
using Repositories.Interfaces;
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
            comboBoxEmployees.DataSource = employees;
            comboBoxEmployees.DisplayMember = "FirstName";
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
            Current = assignedTask;
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
            if(comboBoxTasks.SelectedValue == null || !Int32.TryParse(comboBoxTasks.SelectedValue.ToString(), out taskId))
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
