using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
