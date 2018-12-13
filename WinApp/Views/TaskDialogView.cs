using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Entities;
using WinApp.Interfaces;

namespace WinApp.Views
{
    public partial class TaskDialogView : UserControl, IDialogView<Task>
    {
        public bool IsEditing { get; set; }
        public Task Current { get; set; }

        public event EventHandler OnSaveButtonClick;
        public event EventHandler OnCancelButtonClick;

        public TaskDialogView()
        {
            InitializeComponent();
        }

        public void SetData(Task task)
        {
            Current = task;
            textBoxTaskDescription.Text = Current.TaskDescription;
            dateTimePickerTaskDate.Value = Current.TaskDate;
        }

        public Task GetData()
        {
            errorProvider1.Clear();

            bool hasError = false;

            if (String.IsNullOrEmpty(textBoxTaskDescription.Text))
            {
                errorProvider1.SetError(textBoxTaskDescription, "Please enter the task description.");
                hasError = true;
            }

            if (hasError)
                return null;

            Current.TaskDescription = textBoxTaskDescription.Text;
            Current.TaskDate = dateTimePickerTaskDate.Value;
            
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

        public void LoadEmployees(IList<Employee> employees)
        {
            throw new NotImplementedException();
        }

        public void LoadTasks(IList<Task> tasks)
        {
            throw new NotImplementedException();
        }

        public bool ValidateAssignment()
        {
            throw new NotImplementedException();
        }
    }
}
