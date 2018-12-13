using System.Collections.Generic;
using System.Windows.Forms;
using WinApp.Enums;

namespace WinApp.Interfaces
{
    public interface IDataListView
    {
        void SetTitle(string title);

        void SetData<T>(IList<T> data);

        void SetTag(MenuOption menuOption);

        event DataGridViewCellEventHandler OnDataGridViewCellClick;
    }
}
