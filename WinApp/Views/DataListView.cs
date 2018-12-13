using Entities;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using WinApp.Enums;
using WinApp.Interfaces;

namespace WinApp.Views
{
    public partial class DataListView : UserControl, IDataListView
    {
        public DataListView()
        {
            InitializeComponent();
            dataGridView.AutoGenerateColumns = true;
        }

        public event DataGridViewCellEventHandler OnDataGridViewCellClick;

        public void SetData<T>(IList<T> data)
        {
            var source = new BindingSource();
            dataGridView.DataSource = null;
            MenuOption? menuOption = (this.dataGridView.Tag is MenuOption?) ? (this.dataGridView.Tag as MenuOption?) : null;

            if (menuOption.HasValue)
            {
                if (menuOption.Value == MenuOption.Tasks)
                {
                    var list = data as List<Task>;
                    var tasks = list
                                .Select(t => new
                                {
                                    t.Id,
                                    t.TaskDescription,
                                    TaskDate = t.TaskDate.ToString("dd MMM yyyy")
                                })
                                .ToList();

                    source.DataSource = tasks;
                }
                else if (menuOption.Value == MenuOption.Employees)
                {
                    var list = data as List<Employee>;
                    var employees = list
                                       .Select(e => new
                                       {
                                           e.Id,
                                           e.FirstName,
                                           e.LastName
                                       })
                                       .ToList();

                    source.DataSource = employees;
                }
                else if (menuOption.Value == MenuOption.AssignedTasks)
                {
                    var list = data as List<AssignedTask>;
                    var assignments = list
                                       .Select(a => new
                                       {
                                           a.Id,
                                           a.Task.TaskDescription,
                                           a.Employee.FirstName,
                                           a.Employee.LastName,
                                           AssignedDate = a.AssignmentDate.ToString("dd MMM yyyy"),
                                           a.StartTime,
                                           a.EndTime
                                       })
                                       .ToList();

                    source.DataSource = assignments;
                }
            }

            dataGridView.DataSource = source;
            dataGridView.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCells);
        }

        public void SetTitle(string title)
        {
            labelTitle.Text = title;
        }

        public void SetTag(MenuOption menuOption)
        {
            dataGridView.Tag = menuOption;
        }

        private void dataGridView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            OnDataGridViewCellClick?.Invoke(sender, e);
        }
    }
}
