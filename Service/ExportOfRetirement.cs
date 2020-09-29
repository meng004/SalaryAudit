using ExcelReport;
using ExcelReport.Driver.NPOI;
using ExcelReport.Renderers;
using JournalVoucherAudit.Domain;
using JournalVoucherAudit.Utility;
using System;

namespace JournalVoucherAudit.Service
{
    public class ExportOfRetirement : Export<Retirement, BalanceOfRetirement>
    {
        /// <summary>
        /// 导出
        /// </summary>
        /// <param name="filename">保存文件名</param>
        /// <param name="audit">工资审计</param>
        public ExportOfRetirement(string filename, AuditOfRetirement audit):base(filename,audit)
        {
        }

        #region 属性

        /// <summary>
        /// 设置excel模板
        /// 格式，@"Template\Template_2.xlsx";
        /// </summary>
        protected override string Template => @"Template\template_retirement.xlsx";

        #endregion

        /// <summary>
        /// 明细表
        /// </summary>
        protected override SheetRenderer Details => new SheetRenderer("明细表",
                    new ParameterRenderer("CurrentDate", CurrentDate),//格式为2019年12月31日
                    new RepeaterRenderer<Retirement>("Reconciliation", Audit.MashupDetailed,
                        // 标志   
                        new ParameterRenderer<Retirement>("ChangedStatus", t => t.ChangedStatus.GetDescription()),
                        new ParameterRenderer<Retirement>("MonthStatus", t => t.MonthStatus.GetDescription()),//== MonthStatus.Current ? "本月" : (t.MonthStatus == MonthStatus.Last ? "上月" : "未知")),
                                                                                                              //人员基本信息
                        new ParameterRenderer<Retirement>("DepartmentName", t => t.DepartmentName),
                        new ParameterRenderer<Retirement>("UserId", t => t.UserId),
                        new ParameterRenderer<Retirement>("UserName", t => t.UserName),
                        //工资科目
                        //基本离退休费
                        new ParameterRenderer<Retirement>("Basic", t => t.Basic),
                        //省批生活补贴
                        new ParameterRenderer<Retirement>("ProvincialSubsidy", t => t.ProvincialSubsidy),
                        //校内保留
                        new ParameterRenderer<Retirement>("Reservation", t => t.Reservation),
                        //生贴
                        new ParameterRenderer<Retirement>("LivingSubsidy", t => t.LivingSubsidy),
                        //国防
                        new ParameterRenderer<Retirement>("DefenseSubsidy", t => t.DefenseSubsidy),
                        //护教
                        new ParameterRenderer<Retirement>("ProtectingEducation", t => t.ProtectingEducation),
                        //核补
                        new ParameterRenderer<Retirement>("NuclearSubsidy", t => t.NuclearSubsidy),
                        //独生子女
                        new ParameterRenderer<Retirement>("OnlyChild", t => t.OnlyChild),
                        //中人基本养老金
                        new ParameterRenderer<Retirement>("MiddleMan", t => t.MiddleMan),
                        //津特贴
                        new ParameterRenderer<Retirement>("Allowance", t => t.Allowance),
                        //护理
                        new ParameterRenderer<Retirement>("Nursing", t => t.Nursing),
                        //特贴
                        new ParameterRenderer<Retirement>("SpecialSubsidy", t => t.SpecialSubsidy),
                        //调整的基本养老金
                        new ParameterRenderer<Retirement>("AdjustedPension", t => t.AdjustedPension),
                        //职业年金
                        new ParameterRenderer<Retirement>("OccupationalPension", t => t.OccupationalPension),
                        //应发工资
                        new ParameterRenderer<Retirement>("Payable", t => t.Payable),
                        //房租
                        new ParameterRenderer<Retirement>("Rent", t => t.Rent),
                        //扣其它
                        new ParameterRenderer<Retirement>("Others", t => t.Others),
                        //水电
                        new ParameterRenderer<Retirement>("Utilities", t => t.Utilities),
                        //实发工资
                        new ParameterRenderer<Retirement>("Actual", t => t.Actual)
                        ));
    }
}
