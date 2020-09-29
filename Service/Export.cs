using ExcelReport;
using ExcelReport.Driver.NPOI;
using ExcelReport.Renderers;
using JournalVoucherAudit.Domain;
using JournalVoucherAudit.Utility;
using NPOI.Extend;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Security;

namespace JournalVoucherAudit.Service
{
    /// <summary>
    /// 导出
    /// </summary>
    public abstract class Export<U, B>
        where U: User, new()
        where B: Balance<U>
        
    {
        #region 属性

        protected abstract string Template { get; }
        /// <summary>
        /// 模板文件
        /// </summary>
        private string TemplateFile
        {
            get
            {
                // 获取当前运行目录
                var path = System.AppDomain.CurrentDomain.BaseDirectory;
                //excel模板
                var templateFile = path + Template;
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
        protected Audit<U, B> Audit { get; set; }
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
                //累计
                new ParameterRenderer("TotalPayableOfLast", Audit.Last.TotalPayable()),
                new ParameterRenderer("TotalPayableOfCurrent", Audit.Current.TotalPayable()),
                new ParameterRenderer("LastBalanced", Audit.Last.TotalPayable() - Audit.Mashup.BalancePayableOfLast<U, B>()),
                new ParameterRenderer("CurrentBalanced", Audit.Current.TotalPayable() - Audit.Mashup.BalancePayableOfCurrent<U, B>()),
                //差额累计
                new ParameterRenderer("BalancePayableOfLast", Audit.Mashup.BalancePayableOfLast<U, B>()),
                new ParameterRenderer("BalancePayableOfCurrent", Audit.Mashup.BalancePayableOfCurrent<U, B>()),
                new ParameterRenderer("BalanceActualOfLast", Audit.Mashup.BalanceActualOfLast<U, B>()),
                new ParameterRenderer("BalanceActualOfCurrent", Audit.Mashup.BalanceActualOfCurrent<U, B>()),
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
        //private SheetRenderer Details => new SheetRenderer("明细表",
        //            new ParameterRenderer("CurrentDate", CurrentDate),//格式为2019年12月31日
        //            new RepeaterRenderer<Retirement>("Reconciliation", Audit.MashupDetailed,
        //                // 标志   
        //                new ParameterRenderer<Retirement>("ChangedStatus", t => t.ChangedStatus.GetDescription()),
        //                new ParameterRenderer<Retirement>("MonthStatus", t => t.MonthStatus.GetDescription()),//== MonthStatus.Current ? "本月" : (t.MonthStatus == MonthStatus.Last ? "上月" : "未知")),
        //                                                                                                      //人员基本信息
        //                new ParameterRenderer<Retirement>("DepartmentName", t => t.DepartmentName),
        //                new ParameterRenderer<Retirement>("UserId", t => t.UserId),
        //                new ParameterRenderer<Retirement>("UserName", t => t.UserName),
        //                //工资科目
        //                //基本离退休费
        //                new ParameterRenderer<Retirement>("Basic", t => t.Basic),
        //                //省批生活补贴
        //                new ParameterRenderer<Retirement>("ProvincialSubsidy", t => t.ProvincialSubsidy),
        //                //校内保留
        //                new ParameterRenderer<Retirement>("Reservation", t => t.Reservation),
        //                //生贴
        //                new ParameterRenderer<Retirement>("LivingSubsidy", t => t.LivingSubsidy),
        //                //国防
        //                new ParameterRenderer<Retirement>("DefenseSubsidy", t => t.DefenseSubsidy),
        //                //护教
        //                new ParameterRenderer<Retirement>("ProtectingEducation", t => t.ProtectingEducation),
        //                //核补
        //                new ParameterRenderer<Retirement>("NuclearSubsidy", t => t.NuclearSubsidy),
        //                //独生子女
        //                new ParameterRenderer<Retirement>("OnlyChild", t => t.OnlyChild),
        //                //中人基本养老金
        //                new ParameterRenderer<Retirement>("MiddleMan", t => t.MiddleMan),
        //                //津特贴
        //                new ParameterRenderer<Retirement>("Allowance", t => t.Allowance),
        //                //护理
        //                new ParameterRenderer<Retirement>("Nursing", t => t.Nursing),
        //                //特贴
        //                new ParameterRenderer<Retirement>("SpecialSubsidy", t => t.SpecialSubsidy),
        //                //调整的基本养老金
        //                new ParameterRenderer<Retirement>("AdjustedPension", t => t.AdjustedPension),
        //                //职业年金
        //                new ParameterRenderer<Retirement>("OccupationalPension", t => t.OccupationalPension),
        //                //应发工资
        //                new ParameterRenderer<Retirement>("Payable", t => t.Payable),
        //                //房租
        //                new ParameterRenderer<Retirement>("Rent", t => t.Rent),
        //                //扣其它
        //                new ParameterRenderer<Retirement>("Others", t => t.Others),
        //                //水电
        //                new ParameterRenderer<Retirement>("Utilities", t => t.Utilities),
        //                //实发工资
        //                new ParameterRenderer<Retirement>("Actual", t => t.Actual)
        //                ));

        #endregion

        /// <summary>
        /// 导出
        /// </summary>
        /// <param name="filename">保存文件名</param>
        /// <param name="audit">工资审计</param>
        public Export(string filename, Audit<U,B> audit)
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
