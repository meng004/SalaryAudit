using ExcelReport;
using ExcelReport.Driver.NPOI;
using ExcelReport.Renderers;
using JournalVoucherAudit.Domain;
using JournalVoucherAudit.Utility;
using System;

namespace JournalVoucherAudit.Service
{
    /// <summary>
    /// 导出
    /// </summary>
    public abstract class Export<B, U>
        where B : Balance<U>
        where U : User, new()
    {
        #region 属性

        protected string _template = @"Template\Template_salary.xlsx";
        /// <summary>
        /// 设置excel模板
        /// @"Template\Template_2.xlsx";
        /// </summary>
        protected abstract void SetTemplate();

        /// <summary>
        /// 模板文件
        /// </summary>
        protected string TemplateFile
        {
            get
            {
                // 获取当前运行目录
                var path = System.AppDomain.CurrentDomain.BaseDirectory;
                //excel模板
                var templateFile = path + _template;
                return templateFile;
            }
        }
        /// <summary>
        /// 导出报表名称
        /// </summary>
        protected string Filename { get; set; }
        /// <summary>
        /// 审计类
        /// </summary>
        protected Audit<B, U> Audit { get; set; }
        //对账日期
        //月份的最后一天
        protected string CurrentDate => DateTime.Today.LastDayOfMonth().ToLongDateString();

        #endregion

        #region excel表

        /// <summary>
        /// 调节表
        /// </summary>
        protected SheetRenderer Balances => new SheetRenderer("调节表",
                new ParameterRenderer("CurrentDate", CurrentDate),
                //累计
                new ParameterRenderer("TotalPayableOfLast", Audit.Last.TotalPayable()),
                new ParameterRenderer("TotalPayableOfCurrent", Audit.Current.TotalPayable()),
                new ParameterRenderer("LastBalanced", Audit.Last.TotalPayable() - Audit.Mashup.BalancePayableOfLast<B, U>()),
                new ParameterRenderer("CurrentBalanced", Audit.Current.TotalPayable() - Audit.Mashup.BalancePayableOfCurrent<B, U>()),
                //差额累计
                new ParameterRenderer("BalancePayableOfLast", Audit.Mashup.BalancePayableOfLast<B, U>()),
                new ParameterRenderer("BalancePayableOfCurrent", Audit.Mashup.BalancePayableOfCurrent<B, U>()),
                new ParameterRenderer("BalanceActualOfLast", Audit.Mashup.BalanceActualOfLast<B, U>()),
                new ParameterRenderer("BalanceActualOfCurrent", Audit.Mashup.BalanceActualOfCurrent<B, U>()),
                new RepeaterRenderer<B>("Reconciliation", Audit.Mashup,
                    new ParameterRenderer<B>("ChangedStatus", t => t.ChangedStatus.GetDescription()),
                    new ParameterRenderer<B>("DepartmentName", t => t.DepartmentName),
                    new ParameterRenderer<B>("UserId", t => t.UserId),
                    new ParameterRenderer<B>("UserName", t => t.UserName),
                    new ParameterRenderer<B>("PayableOfLast", t => t.PayableOfLast),
                    new ParameterRenderer<B>("PayableOfCurrent", t => t.PayableOfCurrent),
                    new ParameterRenderer<B>("ActualOfLast", t => t.ActualOfLast),
                    new ParameterRenderer<B>("ActualOfCurrent", t => t.ActualOfCurrent)
                ));
        /// <summary>
        /// 明细表
        /// </summary>
        protected abstract SheetRenderer Details { get; }

        #endregion

        /// <summary>
        /// 导出
        /// </summary>
        /// <param name="filename">保存文件名</param>
        /// <param name="audit">工资审计</param>
        protected Export(string filename, Audit<B, U> audit)
        {
            Filename = filename;
            Audit = audit;
        }

        /// <summary>
        /// 导出保存
        /// </summary>
        public void Save()
        {
            // 项目启动时，添加
            Configurator.Put(".xlsx", new WorkbookLoader());
            //输出excel
            ExportHelper.ExportToLocal(TemplateFile, Filename, Balances, Details);
        }

    }
}
