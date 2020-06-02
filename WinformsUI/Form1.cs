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
        /// 生效规则
        /// 默认按凭证号、金额与记录数匹配
        /// </summary>
        private ActiveRule _rule = ActiveRule.NumberAmountAndCount;

        /// <summary>
        /// 报表类型，与配置文件中的key相同
        /// </summary>
        private const string _CaiWu = "CaiWu";
        private const string _GuoKu = "GuoKu";

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
        /// 依据财务报表的标题，如：[41010101]事业收入_教育事业收入_纳入专户管理的非税收入
        /// 读取配置文件
        /// 调节表（国库标题）、调节表sheet名称、国库报表的资金性质
        /// </summary>
        private List<string> GetConfig
        {
            get
            {
                var caiwuTitle = _caiWuTitle;
                //取科目编号和科目名称
                //[41010101]事业收入_教育事业收入_纳入专户管理的非税收入
                //编号:41010101
                //名称:事业收入_教育事业收入_纳入专户管理的非税收入
                var index_begin = caiwuTitle.IndexOf('[');
                var index_end = caiwuTitle.IndexOf(']');

                var number = caiwuTitle.Substring(index_begin + 1, index_end - index_begin - 1);
                var name = caiwuTitle.Substring(index_end + 1);

                //依据财务报表科目编号，读取配置文件中的国库报表标题和导出excel的sheet名称
                //"零余额公共"101101
                //"零余额非税";//101102
                //"财政拨款";//400101
                //"教育事业收入";//41010101 
                var value = ConfigurationManager.AppSettings[number];

                //使用逗号做分隔符
                var titles = value.Split(',');
                var list = titles.ToList();
                //报表名称放在最后
                list.Add(name);
                return list;
            }
        }

        /// <summary>
        /// 读取国库报表的资金性质
        /// </summary>
        private string GetNatureOfFunds
        {
            get
            {
                var titles = GetConfig;
                string natureOfFunds = titles[2];
                return natureOfFunds;
            }
        }
        /// <summary>
        /// 读取配置文件中的字段
        /// 依据财务报表标题，读取国库报表标题和excel中sheet的名称
        /// </summary>
        /// <param name="caiwuTitle">财务报表的标题</param>
        /// <returns></returns>
        private Tuple<string, string, string> GetReportTitles
        {
            get
            {
                Tuple<string, string, string> result =
                    new Tuple<string, string, string>(string.Empty, string.Empty, string.Empty);

                var titles = GetConfig;

                if (titles.Count > 0)
                {
                    //依次为财务科目名称，调节表中国库科目名称，导出excel的sheet名称、国库报表资金性质
                    result = new Tuple<string, string, string>(titles.LastOrDefault(), titles[0], titles[1]);
                }
                return result;
            }
        }
        /// <summary>
        /// 依据界面规则选中情况，设置rule
        /// </summary>
        private void SetRule()
        {
            if (chk_AmountWithCount.Checked)
            {
                _rule = _rule | ActiveRule.AmountWithCount;
            }
            if (chk_NumberAmountAndCount.Checked)
            {
                _rule = _rule | ActiveRule.NumberAmountAndCount;
            }
            if (chk_AbsWithAmount.Checked)
            {
                _rule = _rule | ActiveRule.AbsWithAmount;
            }
            if (chk_NumberWithAmount.Checked)
            {
                _rule = _rule | ActiveRule.NumberWithAmount;
            }
            if (chk_NumberWithSingleRecord.Checked)
            {
                _rule = _rule | ActiveRule.NumberWithSingleRecord;
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
        /// 财务数据列表
        /// </summary>
        private IList<CaiWuItem> CaiWuData
        {
            get
            {
                //检查文件路径
                if (string.IsNullOrWhiteSpace(txt_CaiWuFilePath.Text))
                {
                    return new List<CaiWuItem>();
                }
                //读取excel，封装为对象列表
                //var excelImportCaiWu = new Import(txt_CaiWuFilePath.Text, 4);

                //读取标题行的行号
                var index = GetTitleIndex(_CaiWu);
                var excelImportCaiWu = new Import(txt_CaiWuFilePath.Text, index[0], index[1]);
                var items = excelImportCaiWu.ReadCaiWu<CaiWuItem>();

                //取文件标题
                //_caiWuTitle = excelImportCaiWu.CaiWuTitle;

                return items.ToList();
            }
        }
        /// <summary>
        /// 国库数据列表
        /// </summary>
        private IList<GuoKuItem> GuoKuData
        {
            get
            {
                //检查文件路径
                if (string.IsNullOrWhiteSpace(txt_GuoKuFilePath.Text))
                {
                    return new List<GuoKuItem>();
                }
                //读取excel，封装为对象列表
                //var excelImportGuoKu = new Import(txt_GuoKuFilePath.Text, 1);

                //读取标题行的行号
                var index = GetTitleIndex(_GuoKu);
                //创建导入对象
                var excelImportGuoKu = new Import(txt_GuoKuFilePath.Text, index[0], index[1]);
                //创建国库数据项
                //var natureOfFunds = GetNatureOfFunds;
                var items = excelImportGuoKu.ReadGuoKu<GuoKuItem>();
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
            dgv_GuoKu.RowPostPaint += dgv_GuoKu_RowPostPaint;
            Text = FormTitle;
            lbl_Caution.Text = caution;
            lbl_Message.Text = string.Empty;
            //不允许自动生成列
            dgv_CaiWu.AutoGenerateColumns = false;
            dgv_GuoKu.AutoGenerateColumns = false;
            //禁用规则
            chk_AbsWithAmount.Enabled = false;
            chk_NumberWithAmount.Enabled = false;
            chk_NumberWithSingleRecord.Enabled = false;
            chk_AmountWithCount.Enabled = false;
            //失效规则
            chk_AmountWithCount.Checked = false;
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
            if (!CaiWuData.Any() || !GuoKuData.Any())
            {
                lbl_Message.Text = Resources.ErrorMessage;
                return;
            }

            //重置消息
            lbl_Message.Text = string.Empty;
            //设置rule
            SetRule();
            //对账
            var caiWuAudit = new CaiWuAudit(_rule);
            var guoKuAudit = new GuoKuAudit(_rule);
            //取不符合要求的数据
            var caiWuException = caiWuAudit.Audit(CaiWuData, GuoKuData);
            var guoKuException = guoKuAudit.Audit(CaiWuData, GuoKuData);

            //统计数据
            var caiwu_total = CaiWuData.Sum(t => t.CreditAmount);
            var guoku_total = GuoKuData.Sum(t => t.Amount);
            var caiwu_subtotal = caiWuException.Sum(t => t.CreditAmount);
            var guoku_subtotal = guoKuException.Sum(t => t.Amount);
            var caiwu_balance = caiwu_total - caiwu_subtotal;
            var guoku_balance = guoku_total - guoku_subtotal;
            //是否平衡
            var isBalance = (caiwu_balance - guoku_balance) < 1e-7;
            //设置提示信息
            lbl_Caution.Text = string.Format(post_caution, isBalance ? "已" : "未");
            lbl_Caution.ForeColor = Color.Red;
            //关闭操作提示信息
            lbl_Message.Text = string.Empty;

            //转换为可排序列表
            var caiWuSort = new SortableBindingList<CaiWuItem>(caiWuException.OrderBy(t => t.Remark).ToList());

            var guoKuSort = new SortableBindingList<GuoKuItem>(guoKuException.OrderBy(t => t.PaymentNumber).ToList());
            //绑定数据            
            dgv_CaiWu.DataSource = caiWuSort;//caiWuSort.OrderBy(t => t.CreditAmount).ToList();

            dgv_GuoKu.DataSource = guoKuSort;//.OrderBy(t => t.Amount).ToList();
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
            var caiWus = dgv_CaiWu.DataSource as IEnumerable<CaiWuItem>;
            var guoKus = dgv_GuoKu.DataSource as IEnumerable<GuoKuItem>;
            //检查数据源
            if (caiWus == null || guoKus == null)
            {
                lbl_Message.Text = Resources.ErrorMessage;
                return;
            }
            //合并两个集合为一个集合，便于报表处理
            var table = new TiaoJieTable(caiWus, guoKus);
            //计算发生额累计
            var caiWuTotal = CaiWuData.Sum(t => t.CreditAmount);
            var guoKuTotal = GuoKuData.Sum(t => t.Amount);
            //设置报表内标题与sheet名称
            //var reportTitles = GetReportTitles;
            var reportTitles = new Tuple<string, string, string>(string.Empty, string.Empty, "工资");
            //文件名
            var voucherDate = DateTime.Today;
            //处理数据集为空
            //if (table.Data.Count() == 0)
            //{
            //    voucherDate = CaiWuData.First().VoucherDate.ToDateTime(); 
            //}
            //else
            //{
            //    voucherDate = table.Data.First().VoucherDate.ToDateTime();
            //}

            var filename = $"{voucherDate.Year}年{voucherDate.Month}月-{reportTitles.Item3}-对账单";
            //保存文件对话
            SaveFileDialog saveFileDlg = new SaveFileDialog { Filter = Resources.FileFilter_xlsx_first, FileName = filename };

            if (DialogResult.OK.Equals(saveFileDlg.ShowDialog()))
            {
                //导出excel
                var export = new Export();
                export.Save(saveFileDlg.FileName, reportTitles, caiWuTotal, guoKuTotal, table.Data);
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
                dgv_CaiWu.RowHeadersWidth,
                e.RowBounds.Height);
            TextRenderer.DrawText(e.Graphics, (e.RowIndex + 1).ToString(),
                dgv_CaiWu.RowHeadersDefaultCellStyle.Font,
                rectangle,
                dgv_CaiWu.RowHeadersDefaultCellStyle.ForeColor,
                TextFormatFlags.VerticalCenter | TextFormatFlags.Right);
        }
        /// <summary>
        /// 为DataGridView添加序号
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dgv_GuoKu_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            Rectangle rectangle = new Rectangle(e.RowBounds.Location.X,
                e.RowBounds.Location.Y,
                dgv_GuoKu.RowHeadersWidth,
                e.RowBounds.Height);
            TextRenderer.DrawText(e.Graphics, (e.RowIndex + 1).ToString(),
                dgv_GuoKu.RowHeadersDefaultCellStyle.Font,
                rectangle,
                dgv_GuoKu.RowHeadersDefaultCellStyle.ForeColor,
                TextFormatFlags.VerticalCenter | TextFormatFlags.Right);
        }
        #endregion

        #region 生效规则
        /// <summary>
        /// 金额与记录数
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void chk_AmountWithCount_CheckedChanged(object sender, EventArgs e)
        {
            if (chk_AmountWithCount.Checked)
                _rule = _rule | ActiveRule.AmountWithCount;
            else
                _rule = _rule & ~ActiveRule.AmountWithCount;
        }
        /// <summary>
        /// 单凭证分多笔支付
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void chk_NumberWithAmount_CheckedChanged(object sender, EventArgs e)
        {
            if (chk_NumberWithAmount.Checked)
                _rule = _rule | ActiveRule.NumberWithAmount;
            else
                _rule = _rule & ~ActiveRule.NumberWithAmount;
        }
        /// <summary>
        /// 同凭证分多笔支付
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void chk_AbsWithAmount_CheckedChanged(object sender, EventArgs e)
        {
            if (chk_AbsWithAmount.Checked)
                _rule = _rule | ActiveRule.AbsWithAmount;
            else
                _rule = _rule & ~ActiveRule.AbsWithAmount;
        }
        /// <summary>
        /// 单笔凭证号与小计
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void chk_NumberWithSingleRecord_CheckedChanged(object sender, EventArgs e)
        {
            if (chk_NumberWithSingleRecord.Checked)
                _rule = _rule | ActiveRule.NumberWithSingleRecord;
            else
                _rule = _rule & ~ActiveRule.NumberWithSingleRecord;
        }
        /// <summary>
        /// 凭证号、金额与记录数
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void chk_NumberAmountAndCount_CheckedChanged(object sender, EventArgs e)
        {
            if (chk_NumberAmountAndCount.Checked)
                _rule = _rule | ActiveRule.NumberAmountAndCount;
            else
                _rule = _rule & ~ActiveRule.NumberAmountAndCount;
        }
        #endregion


    }
}