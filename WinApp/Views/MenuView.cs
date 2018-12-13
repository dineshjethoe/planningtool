using System.Drawing;
using System.Windows.Forms;
using WinApp.Commands;

namespace WinApp.Views
{
    public partial class MenuView : UserControl, IMenuView
    {
        const int WIDTH = 50;
        const int HEIGHT = 50;

        public MenuView()
        {
            InitializeComponent();
            ResizeToolStripButtons();
        }

        public void SetCommands(IMenuCommand[] commands)
        {
            toolStrip1.Items.Clear();
            foreach (var command in commands)
            {
                var button = new ToolStripButton();
                button.Text = command.ToolTip;
                button.Image = command.Icon;
                button.Enabled = command.IsEnabled;
                button.ImageScaling = ToolStripItemImageScaling.None;
                button.DisplayStyle = ToolStripItemDisplayStyle.Image;
                var c = command; // create a closure around the command
                command.PropertyChanged += (o, s) => {
                    button.Text = c.ToolTip;
                    button.Image = c.Icon;
                    button.Enabled = c.IsEnabled;
                };
                button.Click += (o, s) => c.Execute();
                toolStrip1.Items.Add(button);
            }
        }

        private void ResizeToolStripButton(ToolStripButton toolStripButton)
        {
            toolStripButton.AutoSize = false;
            toolStripButton.Width = WIDTH;

            //resize the image of the button to the new size
            int sourceWidth = buttonAdd.Image.Width;
            int sourceHeight = toolStripButton.Image.Height;
            Bitmap b = new Bitmap(WIDTH, HEIGHT);
            using (Graphics g = Graphics.FromImage((Image)b))
            {
                g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                g.DrawImage(toolStripButton.Image, 0, 0, WIDTH, HEIGHT);
            }
            Image myResizedImg = (Image)b;

            //put the resized image back to the button and change toolstrip's ImageScalingSize property 
            toolStripButton.Image = myResizedImg;
        }

        private void ResizeToolStripButtons()
        {
            toolStrip1.AutoSize = false; toolStrip1.Height = HEIGHT;
            toolStrip1.ImageScalingSize = new Size(WIDTH, HEIGHT);

            for (int i = 0; i < toolStrip1.Items.Count; i++)
            {
                if(toolStrip1.Items[i] is ToolStripButton)
                {
                    ResizeToolStripButton(toolStrip1.Items[i] as ToolStripButton);
                }
            }
        }
    }
}
