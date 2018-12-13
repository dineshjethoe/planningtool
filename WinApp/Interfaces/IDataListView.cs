using System.Collections.Generic;
using System.Windows.Forms;

namespace WinApp.Interfaces
{
    public interface IDataListView
    {
        void SetTitle(string title);
        void SetData<T>(IList<T> data);

        event DataGridViewCellEventHandler OnDataGridViewCellClick;
    }
}
