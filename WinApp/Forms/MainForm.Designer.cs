namespace WinApp
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.menuView = new WinApp.Views.MenuView();
            this.panel = new System.Windows.Forms.Panel();
            this.SuspendLayout();
            // 
            // menuView
            // 
            this.menuView.AutoSize = true;
            this.menuView.Dock = System.Windows.Forms.DockStyle.Top;
            this.menuView.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.menuView.Location = new System.Drawing.Point(0, 0);
            this.menuView.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.menuView.Name = "menuView";
            this.menuView.Size = new System.Drawing.Size(784, 50);
            this.menuView.TabIndex = 0;
            // 
            // panel1
            // 
            this.panel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel.Location = new System.Drawing.Point(0, 50);
            this.panel.Name = "panel1";
            this.panel.Size = new System.Drawing.Size(784, 511);
            this.panel.TabIndex = 1;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(784, 561);
            this.Controls.Add(this.panel);
            this.Controls.Add(this.menuView);
            this.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "MainForm";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "KLM | Planning Tool";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Views.MenuView menuView;
        private System.Windows.Forms.Panel panel;
    }
}