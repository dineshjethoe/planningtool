using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using WinApp.Interfaces;

namespace WinApp.Views
{
    public partial class DataListView : UserControl, IDataListView
    {
        public DataListView()
        {
            InitializeComponent();
        }

        public event DataGridViewCellEventHandler OnDataGridViewCellClick;

        public void SetData<T>(IList<T> data)
        {
            var source = new BindingSource();
            source.DataSource = data.ToList();
            dataGridView.AutoGenerateColumns = true;
            dataGridView.DataSource = source;
        }

        public void SetTitle(string title)
        {
            labelTitle.Text = title;
        }

        private void dataGridView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            OnDataGridViewCellClick?.Invoke(sender, e);
        }
    }
}
