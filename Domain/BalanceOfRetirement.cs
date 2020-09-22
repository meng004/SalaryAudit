using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JournalVoucherAudit.Domain
{
    /// <summary>
    /// 离退休人员工资平衡
    /// </summary>
    public class BalanceOfRetirement : Balance<Retirement>
    {
        public BalanceOfRetirement(Retirement last, Retirement current) : base(last, current)
        {

        }
        public override Retirement Detail
        {
            get
            {
                var retirement = new Retirement
                {
                    UserId = this.UserId,
                    UserName = this.UserName,
                    DepartmentName = this.DepartmentName,
                    //应发
                    Basic = _current.Basic - _last.Basic,
                    ProvincialSubsidy = _current.ProvincialSubsidy - _last.ProvincialSubsidy,
                    Reservation = _current.Reservation - _last.Reservation,
                    LivingSubsidy = _current.LivingSubsidy - _last.LivingSubsidy,
                    DefenseSubsidy = _current.DefenseSubsidy - _last.DefenseSubsidy,
                    ProtectingEducation = _current.ProtectingEducation - _last.ProtectingEducation,
                    NuclearSubsidy = _current.NuclearSubsidy - _last.NuclearSubsidy,
                    OnlyChild = _current.OnlyChild - _last.OnlyChild,
                    MiddleMan = _current.MiddleMan - _last.MiddleMan,
                    Allowance = _current.Allowance - _last.Allowance,
                    Nursing = _current.Nursing - _last.Nursing,
                    SpecialSubsidy = _current.SpecialSubsidy - _last.SpecialSubsidy,
                    AdjustedPension = _current.AdjustedPension - _last.AdjustedPension,
                    OccupationalPension = _current.OccupationalPension - _last.OccupationalPension,
                    Payable = _current.Payable - _last.Payable,
                    //扣款
                    Rent = _current.Rent - _last.Rent,
                    Others = _current.Others - _last.Others,
                    Utilities = _current.Utilities - _last.Utilities,
                    Actual = _current.Actual - _last.Actual,
                    //状态标志
                    MonthStatus = this.MonthStatus,
                    ChangedStatus = this.ChangedStatus
                };
                return retirement;
            }
        }
    }
}
