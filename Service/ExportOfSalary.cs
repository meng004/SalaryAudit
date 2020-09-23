using ExcelReport.Renderers;
using JournalVoucherAudit.Domain;
using JournalVoucherAudit.Utility;

namespace JournalVoucherAudit.Service
{
    public class ExportOfSalary : Export<BalanceOfSalary, Salary>
    {
        public ExportOfSalary(string filename, AuditOfSalary audit) : base(filename, audit)
        {
        }

        protected override void SetTemplate()
        {
            _template= @"Template\Template_salary.xlsx";
        }
        protected override SheetRenderer Details => new SheetRenderer("明细表",
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
    }
}
