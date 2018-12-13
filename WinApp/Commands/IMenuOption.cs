using System;

namespace WinApp.Commands
{
    public interface IMenuOption : IDisposable
    {
        void LoadTasks();
        void LoadEmployees();
        void LoadAssignments();
        void Add();
        void Edit();
        void Delete();
    }
}
