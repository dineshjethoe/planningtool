namespace WinApp.Views
{
    partial class MenuView
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.buttonTasks = new System.Windows.Forms.ToolStripButton();
            this.buttonEmployee = new System.Windows.Forms.ToolStripButton();
            this.buttonAssignments = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.buttonAdd = new System.Windows.Forms.ToolStripButton();
            this.buttonEdit = new System.Windows.Forms.ToolStripButton();
            this.buttonDelete = new System.Windows.Forms.ToolStripButton();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.buttonTasks,
            this.buttonEmployee,
            this.buttonAssignments,
            this.toolStripSeparator1,
            this.buttonAdd,
            this.buttonEdit,
            this.buttonDelete});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(200, 25);
            this.toolStrip1.TabIndex = 0;
            this.toolStrip1.Text = "Menu";
            // 
            // buttonTasks
            // 
            this.buttonTasks.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.buttonTasks.Image = global::WinApp.Properties.Resources.icons8_list_48;
            this.buttonTasks.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.buttonTasks.Name = "buttonTasks";
            this.buttonTasks.Size = new System.Drawing.Size(23, 22);
            this.buttonTasks.Text = "Tasks";
            // 
            // buttonEmployee
            // 
            this.buttonEmployee.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.buttonEmployee.Image = global::WinApp.Properties.Resources.icons8_permanent_job_48;
            this.buttonEmployee.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.buttonEmployee.Name = "buttonEmployee";
            this.buttonEmployee.Size = new System.Drawing.Size(23, 22);
            this.buttonEmployee.Text = "Employees";
            // 
            // buttonAssignments
            // 
            this.buttonAssignments.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.buttonAssignments.Image = global::WinApp.Properties.Resources.icons8_planner_48;
            this.buttonAssignments.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.buttonAssignments.Name = "buttonAssignments";
            this.buttonAssignments.Size = new System.Drawing.Size(23, 22);
            this.buttonAssignments.Text = "Assignments";
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // buttonAdd
            // 
            this.buttonAdd.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.buttonAdd.Image = global::WinApp.Properties.Resources.icons8_plus_math_48;
            this.buttonAdd.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.buttonAdd.Name = "buttonAdd";
            this.buttonAdd.Size = new System.Drawing.Size(23, 22);
            this.buttonAdd.Text = "Add";
            // 
            // buttonEdit
            // 
            this.buttonEdit.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.buttonEdit.Image = global::WinApp.Properties.Resources.icons8_pencil_48;
            this.buttonEdit.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.buttonEdit.Name = "buttonEdit";
            this.buttonEdit.Size = new System.Drawing.Size(23, 22);
            this.buttonEdit.Text = "Edit";
            // 
            // buttonDelete
            // 
            this.buttonDelete.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.buttonDelete.Image = global::WinApp.Properties.Resources.icons8_trash_can_48;
            this.buttonDelete.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.buttonDelete.Name = "buttonDelete";
            this.buttonDelete.Size = new System.Drawing.Size(23, 22);
            this.buttonDelete.Text = "Delete";
            // 
            // MenuView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.toolStrip1);
            this.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "MenuView";
            this.Size = new System.Drawing.Size(200, 30);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton buttonTasks;
        private System.Windows.Forms.ToolStripButton buttonEmployee;
        private System.Windows.Forms.ToolStripButton buttonAssignments;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton buttonAdd;
        private System.Windows.Forms.ToolStripButton buttonEdit;
        private System.Windows.Forms.ToolStripButton buttonDelete;
    }
}
