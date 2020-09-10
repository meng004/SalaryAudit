using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JournalVoucherAudit.Domain
{
    /// <summary>
    /// 差额
    /// </summary>
    public class Balance
    {
        #region 字段

        /// <summary>
        /// 上月工资
        /// </summary>
        private Salary _last;
        /// <summary>
        /// 本月工资
        /// </summary>
        private Salary _current;
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="last">上月工资</param>
        /// <param name="current">本月工资</param>
        public Balance(Salary last, Salary current)
        {
            _last = last;
            _current = current;
        }

        #endregion

        #region 个人基本信息
        /// <summary>
        /// 部门名称
        /// </summary>
        public string DepartmentName { get { return _current.DepartmentName; } }
        /// <summary>
        /// 人员代码
        /// </summary>
        public string UserId { get { return _current.UserId; } }
        /// <summary>
        /// 姓名
        /// </summary>
        public string UserName { get { return _current.UserName; } }

        #endregion

        #region 工资
        /// <summary>
        /// 上月工资
        /// </summary>
        public Salary Last { get { return _last; } }
        /// <summary>
        /// 本月工资
        /// </summary>
        public Salary Current { get { return _current; } }
        /// <summary>
        /// 上月应发
        /// </summary>
        public decimal PayableOfLast { get { return _last.Payable; } }
        /// <summary>
        /// 本月应发
        /// </summary>
        public decimal PayableOfCurrent { get { return _current.Payable; } }
        /// <summary>
        /// 上月实发
        /// </summary>
        public decimal ActualOfLast { get { return _last.Actual; } }
        /// <summary>
        /// 本月实发
        /// </summary>
        public decimal ActualOfCurrent { get { return _current.Actual; } }
        /// <summary>
        /// 工资差额
        /// </summary>
        public Salary Detail
        {
            get
            {
                var salary = new Salary
                {
                    UserId = _current.UserId,
                    UserName = _current.UserName,
                    DepartmentName = _current.DepartmentName,
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
                    MonthStatus = _current.MonthStatus,
                    ChangedStatus = _current.ChangedStatus
                };
                return salary;
            }
        }
        #endregion

    }
}
