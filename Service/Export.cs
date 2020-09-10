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

namespace JournalVoucherAudit.Service
{
    /// <summary>
    /// 导出
    /// </summary>
    public class Export
    {
        #region 属性
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
                var templateFile = path + @"Template\Template_new.xlsx";
                return templateFile;
            }
        }
        /// <summary>
        /// 导出报表名称
        /// </summary>
        private string Filename { get; set; }
        /// <summary>
        /// 审计类
        /// </summary>
        private SalaryAudit Audit { get; set; }
        //对账日期
        //月份的最后一天
        private string CurrentDate => DateTime.Today.LastDayOfMonth().ToLongDateString();

        #endregion

        #region 累计
        /// <summary>
        /// 上月应发总计
        /// </summary>
        private decimal TotalPayableOfLast => Audit.Last.Sum(t => t.Payable);
        /// <summary>
        /// 本月应发总计
        /// </summary>
        private decimal TotalPayableOfCurrent => Audit.Current.Sum(t => t.Payable);
        /// <summary>
        /// 上月实发总计
        /// </summary>
        private decimal TotalActualOfLast => Audit.Last.Sum(t => t.Actual);
        /// <summary>
        /// 本月实发总计
        /// </summary>
        private decimal TotalActualOfCurrent => Audit.Current.Sum(t => t.Actual);

        #endregion

        #region 差额累计
        /// <summary>
        /// 上月应发
        /// </summary>
        private decimal BalancePayableOfLast => Audit.Mashup.Sum(t => t.PayableOfLast);
        /// <summary>
        /// 本月应发
        /// </summary>
        private decimal BalancePayableOfCurrent => Audit.Mashup.Sum(t => t.PayableOfCurrent);
        /// <summary>
        /// 上月实发
        /// </summary>
        private decimal BalanceActualOfLast => Audit.Mashup.Sum(t => t.ActualOfLast);
        /// <summary>
        /// 本月实发
        /// </summary>
        private decimal BalanceActualOfCurrent => Audit.Mashup.Sum(t => t.ActualOfCurrent);
        #endregion

        #region excel表

        /// <summary>
        /// 调节表
        /// </summary>
        private SheetRenderer Balances => new SheetRenderer("调节表",
                new ParameterRenderer("CurrentDate", CurrentDate),
                //累计
                new ParameterRenderer("TotalPayableOfLast", TotalPayableOfLast),
                new ParameterRenderer("TotalPayableOfCurrent", TotalPayableOfCurrent),
                new ParameterRenderer("TotalActualOfLast", TotalActualOfLast),
                new ParameterRenderer("TotalActualOfCurrent", TotalActualOfCurrent),
                //差额累计
                new ParameterRenderer("BalancePayableOfLast", BalancePayableOfLast),
                new ParameterRenderer("BalancePayableOfCurrent", BalancePayableOfCurrent),
                new ParameterRenderer("BalanceActualOfLast", BalanceActualOfLast),
                new ParameterRenderer("BalanceActualOfCurrent", BalanceActualOfCurrent),
                new RepeaterRenderer<Balance>("Reconciliation", Audit.Mashup,
                    new ParameterRenderer<Balance>("ChangedStatus", t => t.Current.ChangedStatus.GetDescription()),
                    new ParameterRenderer<Balance>("DepartmentName", t => t.DepartmentName),
                    new ParameterRenderer<Balance>("UserId", t => t.UserId),
                    new ParameterRenderer<Balance>("UserName", t => t.UserName),
                    new ParameterRenderer<Balance>("PayableOfLast", t => t.PayableOfLast),
                    new ParameterRenderer<Balance>("PayableOfCurrent", t => t.PayableOfCurrent),
                    new ParameterRenderer<Balance>("ActualOfLast", t => t.ActualOfLast),
                    new ParameterRenderer<Balance>("ActualOfCurrent", t => t.ActualOfCurrent)
                ));
        /// <summary>
        /// 明细表
        /// </summary>
        private SheetRenderer Details => new SheetRenderer("明细表",
                    new ParameterRenderer("CurrentDate", CurrentDate),//格式为2019年12月31日
                    new RepeaterRenderer<Salary>("Reconciliation", Audit.MashupDetailed,
                        // 标志   
                        new ParameterRenderer<Salary>("ChangedStatus", t => t.ChangedStatus.GetDescription()),
                        new ParameterRenderer<Salary>("MonthStatus", t => t.MonthStatus.GetDescription()),//== MonthStatus.Current ? "本月" : (t.MonthStatus == MonthStatus.Last ? "上月" : "未知")),
                        //人员基本信息
                        new ParameterRenderer<Salary>("DepartmentName", t => t.DepartmentName),
                        new ParameterRenderer<Salary>("UserId", t => t.UserId),
                        new ParameterRenderer<Salary>("UserName", t => t.UserName),
                        //工资科目
                        //岗位工资
                        new ParameterRenderer<Salary>("Position", t => t.Position),
                        //薪级工资
                        new ParameterRenderer<Salary>("Scale", t => t.Scale),
                        //基础绩效工资
                        new ParameterRenderer<Salary>("Performance", t => t.Performance),
                        //月度奖励绩效
                        new ParameterRenderer<Salary>("MonthlyReward", t => t.MonthlyReward),
                        //人才绩效
                        new ParameterRenderer<Salary>("Talent", t => t.Talent),
                        //职称绩效
                        new ParameterRenderer<Salary>("Title", t => t.Title),
                        //女职工卫生费
                        new ParameterRenderer<Salary>("HealthOfFemale", t => t.HealthOfFemale),
                        //住房补贴
                        new ParameterRenderer<Salary>("HousingSubsidy", t => t.HousingSubsidy),
                        //百分之十
                        new ParameterRenderer<Salary>("TenPercent", t => t.TenPercent),
                        //护教
                        new ParameterRenderer<Salary>("ProtectingEducation", t => t.ProtectingEducation),
                        //特贴
                        new ParameterRenderer<Salary>("SpecialSubsidy", t => t.SpecialSubsidy),
                        //国防津贴
                        new ParameterRenderer<Salary>("DefenseSubsidy", t => t.DefenseSubsidy),
                        //临聘技术人员工资
                        new ParameterRenderer<Salary>("WageOfTemporaryStaff", t => t.WageOfTemporaryStaff),
                        //临聘技术人员绩效
                        new ParameterRenderer<Salary>("PerformanceOfTemporaryStaff", t => t.PerformanceOfTemporaryStaff),
                        //应发
                        new ParameterRenderer<Salary>("Payable", t => t.Payable),
                        //房租
                        new ParameterRenderer<Salary>("Rent", t => t.Rent),
                        //合计扣税
                        new ParameterRenderer<Salary>("TotalTax", t => t.TotalTax),
                        //公积金
                        new ParameterRenderer<Salary>("Fund", t => t.Fund),
                        //医保
                        new ParameterRenderer<Salary>("MedicalInsurance", t => t.MedicalInsurance),
                        //养老
                        new ParameterRenderer<Salary>("EndowmentInsurance", t => t.EndowmentInsurance),
                        //职业年金
                        new ParameterRenderer<Salary>("OccupationalPension", t => t.OccupationalPension),
                        //其它
                        new ParameterRenderer<Salary>("Others", t => t.Others),
                        //水费
                        new ParameterRenderer<Salary>("Water", t => t.Water),
                        //实发
                        new ParameterRenderer<Salary>("Actual", t => t.Actual),
                        //上月其他绩效
                        new ParameterRenderer<Salary>("PerformanceOfLastMonth", t => t.PerformanceOfLastMonth),
                        //上月预扣税
                        new ParameterRenderer<Salary>("WithholdingTax", t => t.WithholdingTax)
                        ));

        #endregion

        /// <summary>
        /// 导出
        /// </summary>
        /// <param name="filename">保存文件名</param>
        /// <param name="audit">工资审计</param>
        public Export(string filename, SalaryAudit audit)
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
            ExportHelper.ExportToLocal(TemplateFile, Filename, Balances,Details);
        }

    }
}
