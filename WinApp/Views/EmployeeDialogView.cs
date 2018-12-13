using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Entities;
using WinApp.Interfaces;

namespace WinApp.Views
{
    public partial class EmployeeDialogView : UserControl, IDialogView<Employee>
    {
        public bool IsEditing { get; set; }
        public Employee Current { get; set; }

        public event EventHandler OnSaveButtonClick;
        public event EventHandler OnCancelButtonClick;

        public EmployeeDialogView()
        {
            InitializeComponent();
        }

        public void SetData(Employee employee)
        {
            Current = employee;
            textBoxFirstName.Text = Current.FirstName;
            textBoxLastName.Text = Current.LastName;
        }

        public Employee GetData()
        {
            errorProvider1.Clear();

            bool hasError = false;

            if(String.IsNullOrEmpty(textBoxFirstName.Text))
            {
                errorProvider1.SetError(textBoxFirstName, "Please enter the firstname.");
                hasError = true;
            }

            if (String.IsNullOrEmpty(textBoxLastName.Text))
            {
                errorProvider1.SetError(textBoxLastName, "Please enter the lastname.");
                hasError = true;
            }

            if (hasError)
                return null;

            Current.FirstName = textBoxFirstName.Text;
            Current.LastName = textBoxLastName.Text;

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
