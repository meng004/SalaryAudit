using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace JournalVoucherAudit.WinformsUI
{
    public partial class FrmStart : Form
    {
        FrmSalary frmSalary;
        FrmRetirement frmRetirement;
        public FrmStart()
        {
            InitializeComponent();

            frmSalary = new FrmSalary()
            {
                FormBorderStyle = FormBorderStyle.None,
                Dock = DockStyle.Fill,
                TopLevel = false
            };
            frmRetirement = new FrmRetirement()
            {
                FormBorderStyle = FormBorderStyle.None,
                Dock = DockStyle.Fill,
                TopLevel = false
            };
            Text = FormTitle;
            // 将窗体Form2显示在tabPage[0]中            
            tabControl1.TabPages[0].Controls.Add(frmSalary);
            tabControl1.TabPages[1].Controls.Add(frmRetirement);
            
        }

        /// <summary>
        /// 读取程序集信息，构造窗体的标题
        /// </summary>
        private string FormTitle
        {
            get
            {
                var title = string.Empty;
                var version = string.Empty;
                //读取程序集的title
                object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyTitleAttribute), false);
                if (attributes.Length > 0)
                {
                    AssemblyTitleAttribute titleAttribute = (AssemblyTitleAttribute)attributes[0];
                    if (!string.IsNullOrWhiteSpace(titleAttribute.Title))
                    {
                        title = titleAttribute.Title;
                    }
                }
                //读取程序集的版本
                version = Assembly.GetExecutingAssembly().GetName().Version.ToString();

                return title + "-" + version;
            }
        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (this.tabControl1.SelectedIndex)
            {
                case 0:                    
                    frmSalary.Show();
                    break;
                case 1:                    
                    frmRetirement.Show();
                    break;
            }
        }


        private void FrmStart_Load(object sender, EventArgs e)
        {
            frmSalary.Show();
        }
    }
}
