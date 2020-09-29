namespace JournalVoucherAudit.WinformsUI
{
    partial class FrmStart
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
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabSalary = new System.Windows.Forms.TabPage();
            this.tabRetirement = new System.Windows.Forms.TabPage();
            this.tabControl1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabSalary);
            this.tabControl1.Controls.Add(this.tabRetirement);
            this.tabControl1.Location = new System.Drawing.Point(13, 13);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(2647, 1285);
            this.tabControl1.TabIndex = 0;
            this.tabControl1.SelectedIndexChanged += new System.EventHandler(this.tabControl1_SelectedIndexChanged);
            // 
            // tabSalary
            // 
            this.tabSalary.Location = new System.Drawing.Point(10, 48);
            this.tabSalary.Name = "tabSalary";
            this.tabSalary.Padding = new System.Windows.Forms.Padding(3);
            this.tabSalary.Size = new System.Drawing.Size(2627, 1227);
            this.tabSalary.TabIndex = 0;
            this.tabSalary.Text = "在职";
            this.tabSalary.UseVisualStyleBackColor = true;
            // 
            // tabRetirement
            // 
            this.tabRetirement.Location = new System.Drawing.Point(10, 48);
            this.tabRetirement.Name = "tabRetirement";
            this.tabRetirement.Padding = new System.Windows.Forms.Padding(3);
            this.tabRetirement.Size = new System.Drawing.Size(2627, 1227);
            this.tabRetirement.TabIndex = 1;
            this.tabRetirement.Text = "离退休";
            this.tabRetirement.UseVisualStyleBackColor = true;
            // 
            // FrmStart
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(15F, 30F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(2672, 1310);
            this.Controls.Add(this.tabControl1);
            this.Name = "FrmStart";
            this.Text = "FrmStart";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.FrmStart_Load);
            this.tabControl1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabSalary;
        private System.Windows.Forms.TabPage tabRetirement;
    }
}