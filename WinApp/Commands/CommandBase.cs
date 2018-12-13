using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace WinApp.Commands
{
    abstract class CommandBase : IMenuCommand
    {
        private bool isEnabled;
        private Image icon;
        private string toolTip;
        private object tag;
        public event PropertyChangedEventHandler PropertyChanged;

        protected CommandBase()
        {
            isEnabled = true;
        }

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public abstract void Execute();

        public bool IsEnabled
        {
            get { return isEnabled; }
            set
            {
                if (isEnabled != value)
                {
                    isEnabled = value;
                    OnPropertyChanged("IsEnabled");
                }
            }
        }

        public Image Icon
        {
            get { return icon; }
            set
            {
                if (icon != value)
                {
                    icon = value;
                    OnPropertyChanged("Icon");
                }
            }
        }

        public string ToolTip
        {
            get { return toolTip; }
            set
            {
                if (toolTip != value)
                {
                    toolTip = value;
                    OnPropertyChanged("ToolTip");
                }
            }
        }

        public object Tag
        {
            get { return tag; }
            set
            {
                if (tag != value)
                {
                    tag = value;
                    OnPropertyChanged("Tag");
                }
            }
        }

        public Keys ShortcutKey { get; set; }
    }
}
