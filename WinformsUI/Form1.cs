﻿using JournalVoucherAudit.Domain;
using JournalVoucherAudit.Service;
using JournalVoucherAudit.Utility;
using JournalVoucherAudit.WinformsUI.Properties;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;

namespace JournalVoucherAudit.WinformsUI
{
    public partial class Form1 : Form
    {
        #region 字段

        private static string caution = "请先将工资导出文件另存为xls文件";
        private static string post_caution = "调节后余额 {0} 平衡";

        /// <summary>
        /// 报表类型，与配置文件中的key相同
        /// </summary>
        private const string _Last = "Last";
        private const string _Current = "Current";

        #endregion

        #region 帮助方法

        /// <summary>
        /// 按照报表名称，读取财务/国库数据标题列的行号
        /// </summary>
        /// <param name="reportName"></param>
        /// <returns></returns>
        private int[] GetTitleIndex(string reportName)
        {
            //读取配置文件中标题行的行号
            var value = ConfigurationManager.AppSettings[reportName];
            var index = value.Split(',').ToList();
            var result = index.Select<string, int>(x => Convert.ToInt32(x)).ToArray();
            return result;
        }
        /// <summary>
        /// 本月应发工资变动记录
        /// 前面为新增工资账户，后面为现有账户上月与本月工资
        /// </summary>
        private SalaryAudit Audit
        {
            get
            {
                //对账
                var audit = new SalaryAudit(LastSalaries, CurrentSalaries);
                
                return audit;
            }
        }
        /// <summary>
        /// 获取统计量
        /// 上月应发合计，本月应发合计，上月应发变动合计，本月应发变动合计
        /// 上月调节余额，本月调余额，本月实发工资异动人数，本页新增工资人数
        /// </summary>
        private Dictionary<string, decimal> SalaryStatistics
        {
            get
            {
                var notEquals = Audit.MashupAll;
                //上月应发合计
                var last_total = LastSalaries.Sum(t => t.Payable);
                //本月应发合计
                var current_total = CurrentSalaries.Sum(t => t.Payable);
                //上月应发变动合计
                var last_subtotal = notEquals.Where(t => t.Status == MonthStatus.Last).Sum(t => t.Payable);
                //本月应发变动合计
                var current_subtotal = notEquals.Where(t => t.Status == MonthStatus.Current).Sum(t => t.Payable);
                //计算调节余额
                //未出现在本月的上月工资的应发合计
                var notInCurrent = Audit.NotInCurrent.Sum(t => t.Payable);
                //上月平衡=上月合计-未出现在本月合计-本月发生异动的上月合计
                var last_balance = last_total - notInCurrent - last_subtotal;

                //本月新增
                var newInCurrent = Audit.NewSalaries.Sum(t => t.Payable);
                //本月平衡=本月合计-本月新增-本月发生异动的本月合计
                var current_balance = current_total - newInCurrent - current_subtotal;
                //本月实发工资异动人数
                var changed_Count = Audit.ChangedByExistId.Item2.Count;
                //本月新增工资人数
                var newSalaries_Count = Audit.NewSalaries.Count;

                var dict = new Dictionary<string, decimal>
                {                    
                    { "last_total", last_total },
                    { "current_total", current_total },
                    { "last_subtotal", last_subtotal },
                    { "current_subtotal", current_subtotal },
                    { "last_balance", last_balance },
                    { "current_balance", current_balance },
                    { "changed_count", changed_Count },
                    { "newSalaries_count", newSalaries_Count }
                };

                return dict;
            }
        }

        #endregion

        #region 属性

        /// <summary>
        /// 财务文件的标题
        /// 财政补助收入
        /// 教育事业收入
        /// 零余额公共财政预算
        /// 零余额纳入专户管理的非税收入
        /// </summary>
        private string _caiWuTitle = string.Empty;
        /// <summary>
        /// 上月工资列表
        /// </summary>
        private IList<Salary> LastSalaries
        {
            get
            {
                //检查文件路径
                if (string.IsNullOrWhiteSpace(txt_CaiWuFilePath.Text))
                {
                    return new List<Salary>();
                }

                //读取标题行的行号
                var index = GetTitleIndex(_Last);
                var excelImportCaiWu = new SalaryImport(txt_CaiWuFilePath.Text, index[0], index[1]);
                var items = excelImportCaiWu.Salaries;

                //取文件标题
                //_caiWuTitle = excelImportCaiWu.CaiWuTitle;

                return items.ToList();
            }
        }
        /// <summary>
        /// 本月工资列表
        /// </summary>
        private IList<Salary> CurrentSalaries
        {
            get
            {
                //检查文件路径
                if (string.IsNullOrWhiteSpace(txt_GuoKuFilePath.Text))
                {
                    return new List<Salary>();
                }

                //读取标题行的行号
                var index = GetTitleIndex(_Current);
                //创建导入对象
                var excelImportGuoKu = new SalaryImport(txt_GuoKuFilePath.Text, index[0], index[1]);
                //创建国库数据项
                //var natureOfFunds = GetNatureOfFunds;
                var items = excelImportGuoKu.Salaries;
                return items.ToList();
            }
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

        #endregion

        #region 初始化

        public Form1()
        {
            InitializeComponent();
            dgv_CaiWu.RowPostPaint += dgv_CaiWu_RowPostPaint;
            Text = FormTitle;
            lbl_Caution.Text = caution;
            lbl_Message.Text = string.Empty;
            //不允许自动生成列
            dgv_CaiWu.AutoGenerateColumns = false;
            //只读
            txt_ChangedCount.ReadOnly = true;
            txt_currentPayable.ReadOnly = true;
            txt_lastPayable.ReadOnly = true;
            txt_newSalariesCount.ReadOnly = true;
        }

        #endregion

        #region 财务文件路径
        /// <summary>
        /// 选择财务excel文件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_CaiWuFilePath_Click(object sender, EventArgs e)
        {
            var dialog = new OpenFileDialog { Filter = Resources.FileFilter_xls_first };
            if (DialogResult.OK.Equals(dialog.ShowDialog()))
            {
                //保存文件路径                
                txt_CaiWuFilePath.Text = dialog.FileName;
            }
        }
        /// <summary>
        /// 释放拖放文件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txt_CaiWuFilePath_DragDrop(object sender, DragEventArgs e)
        {
            //获取文件路径
            string path = ((Array)e.Data.GetData(DataFormats.FileDrop)).GetValue(0).ToString();
            txt_CaiWuFilePath.Text = path;
        }
        /// <summary>
        /// 拖放文件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txt_CaiWuFilePath_DragEnter(object sender, DragEventArgs e)
        {
            //拖放的是文件
            e.Effect = e.Data.GetDataPresent(DataFormats.FileDrop) ? DragDropEffects.Link : DragDropEffects.None;
        }

        #endregion

        #region 国库文件路径
        /// <summary>
        /// 选择国库excel文件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_GuoKuFilePath_Click(object sender, EventArgs e)
        {
            var dialog = new OpenFileDialog { Filter = Resources.FileFilter_xls_first };
            if (DialogResult.OK.Equals(dialog.ShowDialog()))
            {
                //保存文件路径
                txt_GuoKuFilePath.Text = dialog.FileName;
            }
        }

        /// <summary>
        /// 释放拖放文件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txt_GuoKuFilePath_DragDrop(object sender, DragEventArgs e)
        {
            //获取文件路径
            string path = ((Array)e.Data.GetData(DataFormats.FileDrop)).GetValue(0).ToString();
            txt_GuoKuFilePath.Text = path;
        }

        #endregion

        #region 对账与导出

        /// <summary>
        /// 对账
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_Audit_Click(object sender, EventArgs e)
        {
            //检查数据文件
            if (!LastSalaries.Any() || !CurrentSalaries.Any())
            {
                lbl_Message.Text = Resources.ErrorMessage;
                return;
            }

            //重置消息
            lbl_Message.Text = string.Empty;

            //显示
            txt_lastPayable.Text = SalaryStatistics["last_total"].ToString();
            txt_currentPayable.Text = SalaryStatistics["current_total"].ToString();
            txt_ChangedCount.Text = SalaryStatistics["changed_count"].ToString();
            txt_newSalariesCount.Text = SalaryStatistics["newSalaries_count"].ToString();
            //是否平衡
            //var isBalance = (SalaryStatistics["last_balance"] - SalaryStatistics["current_balance"]) < 1e-7m;
            //设置提示信息
            //lbl_Caution.Text = string.Format(post_caution, isBalance ? "已" : "未");
            //lbl_Caution.ForeColor = Color.Red;

            //关闭操作提示信息
            lbl_Message.Text = string.Empty;

            //转换为可排序列表
            var caiWuSort = new SortableBindingList<Salary>(Audit.MashupAll);

            //绑定数据            
            dgv_CaiWu.DataSource = caiWuSort;//caiWuSort.OrderBy(t => t.CreditAmount).ToList();
        }

        /// <summary>
        /// 导出excel报表
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_Export_Click(object sender, EventArgs e)
        {
            //关闭提示消息
            lbl_Caution.Text = string.Empty;

            //取出不符合要求的数据
            var changed_Salaries = dgv_CaiWu.DataSource as IEnumerable<Salary>;

            //检查数据源
            if (changed_Salaries == null)
            {
                lbl_Message.Text = Resources.ErrorMessage;
                return;
            }
            //获取应发合计
            var last_total = SalaryStatistics["last_total"];
            var current_total = SalaryStatistics["current_total"];
            //文件名
            var voucherDate = DateTime.Today;
            var filename = $"{voucherDate.Year}年{voucherDate.Month}月-工资对账单";
            //保存文件对话
            SaveFileDialog saveFileDlg = new SaveFileDialog { Filter = Resources.FileFilter_xlsx_first, FileName = filename };

            if (DialogResult.OK.Equals(saveFileDlg.ShowDialog()))
            {
                //导出excel
                var export = new Export();
                export.Save(saveFileDlg.FileName, Audit.MashupAll, SalaryStatistics);
                //提示消息
                lbl_Message.Text = Resources.ResultMessage;
            }
        }

        #endregion

        #region DataGridView添加序号
        /// <summary>
        /// 为DataGridView添加序号
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dgv_CaiWu_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            Rectangle rectangle = new Rectangle(e.RowBounds.Location.X,
                e.RowBounds.Location.Y,
                40,
                //dgv_CaiWu.RowHeadersWidth,
                e.RowBounds.Height);
            TextRenderer.DrawText(e.Graphics, (e.RowIndex + 1).ToString(),
                dgv_CaiWu.RowHeadersDefaultCellStyle.Font,
                rectangle,
                dgv_CaiWu.RowHeadersDefaultCellStyle.ForeColor,
                TextFormatFlags.VerticalCenter | TextFormatFlags.Right);
        }




        #endregion


    }
}
