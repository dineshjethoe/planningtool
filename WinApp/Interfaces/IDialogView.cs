using System;
using System.Collections.Generic;
using Entities;

namespace WinApp.Interfaces
{
    public interface IDialogView<T>
    {
        void SetTitle(string title);
        void SetData(T data);
        T GetData();
        void LoadEmployees(IList<Employee> employees);
        void LoadTasks(IList<Task> tasks);

        bool IsEditing { get; set; }
        T Current { get; set; }

        event EventHandler OnSaveButtonClick;
        event EventHandler OnCancelButtonClick;
    }
}
