using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JournalVoucherAudit.Domain
{
    /// <summary>
    /// 在职人员工资平衡
    /// </summary>
    public class BalanceOfSalary : Balance<Salary>
    {

        public BalanceOfSalary(Salary last, Salary current) : base(last, current)
        {

        }

        /// <summary>
        /// 工资差额
        /// </summary>
        public override Salary Detail
        {
            get
            {
                var salary = new Salary
                {
                    UserId = this.UserId,
                    UserName = this.UserName,
                    DepartmentName = this.DepartmentName,
                    //应发
                    Position = _current.Position - _last.Position,
                    Scale = _current.Scale - _last.Scale,
                    Performance = _current.Performance - _last.Performance,
                    MonthlyReward = _current.MonthlyReward - _last.MonthlyReward,
                    Talent = _current.Talent - _last.Talent,
                    Title = _current.Title - _last.Title,
                    HealthOfFemale = _current.HealthOfFemale - _last.HealthOfFemale,
                    HousingSubsidy = _current.HousingSubsidy - _last.HousingSubsidy,
                    TenPercent = _current.TenPercent - _last.TenPercent,
                    ProtectingEducation = _current.ProtectingEducation - _last.ProtectingEducation,
                    SpecialSubsidy = _current.SpecialSubsidy - _last.SpecialSubsidy,
                    DefenseSubsidy = _current.DefenseSubsidy - _last.DefenseSubsidy,
                    WageOfTemporaryStaff = _current.WageOfTemporaryStaff - _last.WageOfTemporaryStaff,
                    PerformanceOfTemporaryStaff = _current.PerformanceOfTemporaryStaff - _last.PerformanceOfTemporaryStaff,
                    Payable = _current.Payable - _last.Payable,
                    //扣款
                    Rent = _current.Rent - _last.Rent,
                    TotalTax = _current.TotalTax - _last.TotalTax,
                    Fund = _current.Fund - _last.Fund,
                    MedicalInsurance = _current.MedicalInsurance - _last.MedicalInsurance,
                    EndowmentInsurance = _current.EndowmentInsurance - _last.EndowmentInsurance,
                    OccupationalPension = _current.OccupationalPension - _last.OccupationalPension,
                    Others = _current.Others - _last.Others,
                    Water = _current.Water - _last.Water,
                    Actual = _current.Actual - _last.Actual,
                    PerformanceOfLastMonth = _current.PerformanceOfLastMonth - _last.PerformanceOfLastMonth,
                    WithholdingTax = _current.WithholdingTax - _last.WithholdingTax,
                    //状态标志
                    MonthStatus = this.MonthStatus,
                    ChangedStatus = this.ChangedStatus
                };
                return salary;
            }
        }

    }
}
