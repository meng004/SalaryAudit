namespace JournalVoucherAudit.WinformsUI
{
    partial class FrmRetirement
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmRetirement));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.lbl_Caution = new System.Windows.Forms.Label();
            this.btn_GuoKuFilePath = new System.Windows.Forms.Button();
            this.btn_CaiWuFilePath = new System.Windows.Forms.Button();
            this.txt_GuoKuFilePath = new System.Windows.Forms.TextBox();
            this.txt_CaiWuFilePath = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.lbl_Message = new System.Windows.Forms.Label();
            this.btn_Audit = new System.Windows.Forms.Button();
            this.btn_Export = new System.Windows.Forms.Button();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.txt_retired = new System.Windows.Forms.TextBox();
            this.txt_news = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txt_changed = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.dgv_CaiWu = new System.Windows.Forms.DataGridView();
            this.ChangedStatus = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DepartmentName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.UserId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.UserName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Position = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Scale = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Performance = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.MonthlyReward = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panel2 = new System.Windows.Forms.Panel();
            this.groupBox1.SuspendLayout();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_CaiWu)).BeginInit();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.lbl_Caution);
            this.groupBox1.Controls.Add(this.btn_GuoKuFilePath);
            this.groupBox1.Controls.Add(this.btn_CaiWuFilePath);
            this.groupBox1.Controls.Add(this.txt_GuoKuFilePath);
            this.groupBox1.Controls.Add(this.txt_CaiWuFilePath);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.lbl_Message);
            this.groupBox1.Location = new System.Drawing.Point(32, 32);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(8);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(8);
            this.groupBox1.Size = new System.Drawing.Size(1642, 242);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "比对数据";
            // 
            // lbl_Caution
            // 
            this.lbl_Caution.AutoSize = true;
            this.lbl_Caution.Font = new System.Drawing.Font("宋体", 12F);
            this.lbl_Caution.Location = new System.Drawing.Point(18, 186);
            this.lbl_Caution.Margin = new System.Windows.Forms.Padding(8, 0, 8, 0);
            this.lbl_Caution.Name = "lbl_Caution";
            this.lbl_Caution.Size = new System.Drawing.Size(497, 40);
            this.lbl_Caution.TabIndex = 3;
            this.lbl_Caution.Text = "选择离退休人员工资表.XLS";
            // 
            // btn_GuoKuFilePath
            // 
            this.btn_GuoKuFilePath.Location = new System.Drawing.Point(1440, 120);
            this.btn_GuoKuFilePath.Margin = new System.Windows.Forms.Padding(8);
            this.btn_GuoKuFilePath.Name = "btn_GuoKuFilePath";
            this.btn_GuoKuFilePath.Size = new System.Drawing.Size(188, 58);
            this.btn_GuoKuFilePath.TabIndex = 1;
            this.btn_GuoKuFilePath.Text = "选择...";
            this.btn_GuoKuFilePath.UseVisualStyleBackColor = true;
            this.btn_GuoKuFilePath.Click += new System.EventHandler(this.btn_GuoKuFilePath_Click);
            // 
            // btn_CaiWuFilePath
            // 
            this.btn_CaiWuFilePath.Location = new System.Drawing.Point(1440, 48);
            this.btn_CaiWuFilePath.Margin = new System.Windows.Forms.Padding(8);
            this.btn_CaiWuFilePath.Name = "btn_CaiWuFilePath";
            this.btn_CaiWuFilePath.Size = new System.Drawing.Size(188, 58);
            this.btn_CaiWuFilePath.TabIndex = 0;
            this.btn_CaiWuFilePath.Text = "选择...";
            this.btn_CaiWuFilePath.UseVisualStyleBackColor = true;
            this.btn_CaiWuFilePath.Click += new System.EventHandler(this.btn_CaiWuFilePath_Click);
            // 
            // txt_GuoKuFilePath
            // 
            this.txt_GuoKuFilePath.AllowDrop = true;
            this.txt_GuoKuFilePath.BackColor = System.Drawing.SystemColors.Window;
            this.txt_GuoKuFilePath.Location = new System.Drawing.Point(168, 125);
            this.txt_GuoKuFilePath.Margin = new System.Windows.Forms.Padding(8);
            this.txt_GuoKuFilePath.Name = "txt_GuoKuFilePath";
            this.txt_GuoKuFilePath.ReadOnly = true;
            this.txt_GuoKuFilePath.Size = new System.Drawing.Size(1252, 42);
            this.txt_GuoKuFilePath.TabIndex = 1;
            this.txt_GuoKuFilePath.TabStop = false;
            this.txt_GuoKuFilePath.DragDrop += new System.Windows.Forms.DragEventHandler(this.txt_GuoKuFilePath_DragDrop);
            this.txt_GuoKuFilePath.DragEnter += new System.Windows.Forms.DragEventHandler(this.txt_CaiWuFilePath_DragEnter);
            // 
            // txt_CaiWuFilePath
            // 
            this.txt_CaiWuFilePath.AllowDrop = true;
            this.txt_CaiWuFilePath.BackColor = System.Drawing.SystemColors.Window;
            this.txt_CaiWuFilePath.Location = new System.Drawing.Point(168, 52);
            this.txt_CaiWuFilePath.Margin = new System.Windows.Forms.Padding(8);
            this.txt_CaiWuFilePath.Name = "txt_CaiWuFilePath";
            this.txt_CaiWuFilePath.ReadOnly = true;
            this.txt_CaiWuFilePath.Size = new System.Drawing.Size(1252, 42);
            this.txt_CaiWuFilePath.TabIndex = 1;
            this.txt_CaiWuFilePath.TabStop = false;
            this.txt_CaiWuFilePath.DragDrop += new System.Windows.Forms.DragEventHandler(this.txt_CaiWuFilePath_DragDrop);
            this.txt_CaiWuFilePath.DragEnter += new System.Windows.Forms.DragEventHandler(this.txt_CaiWuFilePath_DragEnter);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(20, 132);
            this.label2.Margin = new System.Windows.Forms.Padding(8, 0, 8, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(133, 30);
            this.label2.TabIndex = 0;
            this.label2.Text = "本月工资";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(20, 60);
            this.label1.Margin = new System.Windows.Forms.Padding(8, 0, 8, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(133, 30);
            this.label1.TabIndex = 0;
            this.label1.Text = "上月工资";
            // 
            // lbl_Message
            // 
            this.lbl_Message.AutoSize = true;
            this.lbl_Message.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold);
            this.lbl_Message.ForeColor = System.Drawing.Color.Red;
            this.lbl_Message.Location = new System.Drawing.Point(1038, 188);
            this.lbl_Message.Margin = new System.Windows.Forms.Padding(8, 0, 8, 0);
            this.lbl_Message.Name = "lbl_Message";
            this.lbl_Message.Size = new System.Drawing.Size(140, 40);
            this.lbl_Message.TabIndex = 4;
            this.lbl_Message.Text = "说明：";
            // 
            // btn_Audit
            // 
            this.btn_Audit.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btn_Audit.Location = new System.Drawing.Point(695, 30);
            this.btn_Audit.Margin = new System.Windows.Forms.Padding(8);
            this.btn_Audit.Name = "btn_Audit";
            this.btn_Audit.Size = new System.Drawing.Size(188, 75);
            this.btn_Audit.TabIndex = 2;
            this.btn_Audit.Text = "核对";
            this.btn_Audit.UseVisualStyleBackColor = true;
            this.btn_Audit.Click += new System.EventHandler(this.btn_Audit_Click);
            // 
            // btn_Export
            // 
            this.btn_Export.Font = new System.Drawing.Font("宋体", 12F);
            this.btn_Export.Location = new System.Drawing.Point(695, 120);
            this.btn_Export.Margin = new System.Windows.Forms.Padding(8);
            this.btn_Export.Name = "btn_Export";
            this.btn_Export.Size = new System.Drawing.Size(188, 75);
            this.btn_Export.TabIndex = 3;
            this.btn_Export.Text = "导出";
            this.btn_Export.UseVisualStyleBackColor = true;
            this.btn_Export.Click += new System.EventHandler(this.btn_Export_Click);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.txt_retired);
            this.groupBox3.Controls.Add(this.txt_news);
            this.groupBox3.Controls.Add(this.label5);
            this.groupBox3.Controls.Add(this.txt_changed);
            this.groupBox3.Controls.Add(this.label4);
            this.groupBox3.Controls.Add(this.label3);
            this.groupBox3.Controls.Add(this.btn_Audit);
            this.groupBox3.Controls.Add(this.btn_Export);
            this.groupBox3.Location = new System.Drawing.Point(1700, 32);
            this.groupBox3.Margin = new System.Windows.Forms.Padding(5);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Padding = new System.Windows.Forms.Padding(5);
            this.groupBox3.Size = new System.Drawing.Size(895, 242);
            this.groupBox3.TabIndex = 5;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "操作";
            // 
            // txt_retired
            // 
            this.txt_retired.Location = new System.Drawing.Point(189, 150);
            this.txt_retired.Name = "txt_retired";
            this.txt_retired.Size = new System.Drawing.Size(323, 42);
            this.txt_retired.TabIndex = 7;
            // 
            // txt_news
            // 
            this.txt_news.Location = new System.Drawing.Point(189, 89);
            this.txt_news.Name = "txt_news";
            this.txt_news.Size = new System.Drawing.Size(323, 42);
            this.txt_news.TabIndex = 7;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(25, 162);
            this.label5.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(133, 30);
            this.label5.TabIndex = 6;
            this.label5.Text = "离世人数";
            // 
            // txt_changed
            // 
            this.txt_changed.Location = new System.Drawing.Point(189, 28);
            this.txt_changed.Name = "txt_changed";
            this.txt_changed.Size = new System.Drawing.Size(323, 42);
            this.txt_changed.TabIndex = 7;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(25, 101);
            this.label4.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(133, 30);
            this.label4.TabIndex = 6;
            this.label4.Text = "新添人数";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(25, 40);
            this.label3.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(133, 30);
            this.label3.TabIndex = 6;
            this.label3.Text = "变动人数";
            // 
            // dgv_CaiWu
            // 
            this.dgv_CaiWu.AllowUserToAddRows = false;
            this.dgv_CaiWu.AllowUserToDeleteRows = false;
            this.dgv_CaiWu.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv_CaiWu.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ChangedStatus,
            this.DepartmentName,
            this.UserId,
            this.UserName,
            this.Position,
            this.Scale,
            this.Performance,
            this.MonthlyReward});
            this.dgv_CaiWu.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgv_CaiWu.Location = new System.Drawing.Point(0, 0);
            this.dgv_CaiWu.Margin = new System.Windows.Forms.Padding(8);
            this.dgv_CaiWu.Name = "dgv_CaiWu";
            this.dgv_CaiWu.ReadOnly = true;
            this.dgv_CaiWu.RowHeadersWidth = 50;
            this.dgv_CaiWu.RowTemplate.Height = 23;
            this.dgv_CaiWu.Size = new System.Drawing.Size(2563, 787);
            this.dgv_CaiWu.TabIndex = 0;
            this.dgv_CaiWu.TabStop = false;
            // 
            // ChangedStatus
            // 
            this.ChangedStatus.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.ColumnHeader;
            this.ChangedStatus.DataPropertyName = "ChangedStatus";
            this.ChangedStatus.HeaderText = "状态";
            this.ChangedStatus.MinimumWidth = 12;
            this.ChangedStatus.Name = "ChangedStatus";
            this.ChangedStatus.ReadOnly = true;
            this.ChangedStatus.Width = 127;
            // 
            // DepartmentName
            // 
            this.DepartmentName.DataPropertyName = "DepartmentName";
            this.DepartmentName.HeaderText = "部门";
            this.DepartmentName.MinimumWidth = 12;
            this.DepartmentName.Name = "DepartmentName";
            this.DepartmentName.ReadOnly = true;
            this.DepartmentName.Width = 150;
            // 
            // UserId
            // 
            this.UserId.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.ColumnHeader;
            this.UserId.DataPropertyName = "UserId";
            this.UserId.HeaderText = "人员代码";
            this.UserId.MinimumWidth = 12;
            this.UserId.Name = "UserId";
            this.UserId.ReadOnly = true;
            this.UserId.Width = 187;
            // 
            // UserName
            // 
            this.UserName.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.ColumnHeader;
            this.UserName.DataPropertyName = "UserName";
            this.UserName.HeaderText = "姓名";
            this.UserName.MinimumWidth = 12;
            this.UserName.Name = "UserName";
            this.UserName.ReadOnly = true;
            this.UserName.Width = 127;
            // 
            // Position
            // 
            this.Position.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.ColumnHeader;
            this.Position.DataPropertyName = "PayableOfLast";
            this.Position.HeaderText = "上月应发";
            this.Position.MinimumWidth = 20;
            this.Position.Name = "Position";
            this.Position.ReadOnly = true;
            this.Position.Width = 187;
            // 
            // Scale
            // 
            this.Scale.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.ColumnHeader;
            this.Scale.DataPropertyName = "PayableOfCurrent";
            this.Scale.HeaderText = "本月应发";
            this.Scale.MinimumWidth = 12;
            this.Scale.Name = "Scale";
            this.Scale.ReadOnly = true;
            this.Scale.Width = 187;
            // 
            // Performance
            // 
            this.Performance.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.ColumnHeader;
            this.Performance.DataPropertyName = "ActualOfLast";
            this.Performance.HeaderText = "上月实发";
            this.Performance.MinimumWidth = 12;
            this.Performance.Name = "Performance";
            this.Performance.ReadOnly = true;
            this.Performance.Width = 187;
            // 
            // MonthlyReward
            // 
            this.MonthlyReward.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.MonthlyReward.DataPropertyName = "ActualOfCurrent";
            this.MonthlyReward.HeaderText = "本月实发";
            this.MonthlyReward.MinimumWidth = 12;
            this.MonthlyReward.Name = "MonthlyReward";
            this.MonthlyReward.ReadOnly = true;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.dgv_CaiWu);
            this.panel2.Location = new System.Drawing.Point(32, 312);
            this.panel2.Margin = new System.Windows.Forms.Padding(5);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(2563, 787);
            this.panel2.TabIndex = 7;
            // 
            // FrmRetirement
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(15F, 30F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(2631, 1116);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(8);
            this.Name = "FrmRetirement";
            this.Text = "人事-财务工资核对系统";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_CaiWu)).EndInit();
            this.panel2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label lbl_Caution;
        private System.Windows.Forms.Button btn_GuoKuFilePath;
        private System.Windows.Forms.Button btn_CaiWuFilePath;
        private System.Windows.Forms.TextBox txt_GuoKuFilePath;
        private System.Windows.Forms.TextBox txt_CaiWuFilePath;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btn_Audit;
        private System.Windows.Forms.Button btn_Export;
        private System.Windows.Forms.Label lbl_Message;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.DataGridView dgv_CaiWu;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txt_retired;
        private System.Windows.Forms.TextBox txt_news;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txt_changed;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.DataGridViewTextBoxColumn ChangedStatus;
        private System.Windows.Forms.DataGridViewTextBoxColumn DepartmentName;
        private System.Windows.Forms.DataGridViewTextBoxColumn UserId;
        private System.Windows.Forms.DataGridViewTextBoxColumn UserName;
        private System.Windows.Forms.DataGridViewTextBoxColumn Position;
        private System.Windows.Forms.DataGridViewTextBoxColumn Scale;
        private System.Windows.Forms.DataGridViewTextBoxColumn Performance;
        private System.Windows.Forms.DataGridViewTextBoxColumn MonthlyReward;
    }
}

