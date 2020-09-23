using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JournalVoucherAudit.Domain
{
    /// <summary>
    /// 工资差额
    /// 抽象类
    /// </summary>
    public abstract class Balance<T> where T: User 
    {
        #region 字段

        /// <summary>
        /// 上月工资
        /// </summary>
        protected T _last;
        /// <summary>
        /// 本月工资
        /// </summary>
        protected T _current;

        protected Balance(T last, T current)
        {
            _last = last;
            _current = current;
        }
        ///// <summary>
        ///// 构造函数
        ///// </summary>
        ///// <param name="last">上月工资</param>
        ///// <param name="current">本月工资</param>
        //public Balance(T last, T current)
        //{
        //    _last = last;
        //    _current = current;
        //}

        #endregion

        #region 个人基本信息
        /// <summary>
        /// 部门名称
        /// </summary>
        public string DepartmentName => _current.MonthStatus == MonthStatus.Unknown 
                                                             ? _last.DepartmentName 
                                                             : _current.DepartmentName;
        /// <summary>
        /// 人员代码
        /// </summary>
        public string UserId => _current.MonthStatus == MonthStatus.Unknown
                                                     ? _last.UserId
                                                     : _current.UserId;
        /// <summary>
        /// 姓名
        /// </summary>
        public string UserName => _current.MonthStatus == MonthStatus.Unknown
                                                       ? _last.UserName
                                                       : _current.UserName;
        /// <summary>
        /// 工资变动事由
        /// </summary>
        public ChangedStatus ChangedStatus
        {
            get
            {
                //如果退休
                if (_current.ChangedStatus == ChangedStatus.Unknown)
                    return _last.ChangedStatus;
                else
                    return _current.ChangedStatus;
            }
        }
        /// <summary>
        /// 月度状态，本月或上月
        /// </summary>
        public MonthStatus  MonthStatus
        {
            get
            {
                //如果退休
                if (_current.MonthStatus==MonthStatus.Unknown)
                    return _last.MonthStatus;
                else
                    return _current.MonthStatus;
            }
        }
        #endregion

        #region 工资
        /// <summary>
        /// 上月工资
        /// </summary>
        public T Last { get { return _last; } }
        /// <summary>
        /// 本月工资
        /// </summary>
        public T Current { get { return _current; } }
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
        public abstract T Detail { get; }

        ///// <summary>
        ///// 工资差额
        ///// </summary>
        //public T Detail
        //{
        //    get
        //    {
        //        var salary = new Salary
        //        {
        //            UserId = this.UserId,
        //            UserName = this.UserName,
        //            DepartmentName = this.DepartmentName,
        //            //应发
        //            Position = _current.Position - _last.Position,
        //            Scale = _current.Scale - _last.Scale,
        //            Performance = _current.Performance - _last.Performance,
        //            MonthlyReward = _current.MonthlyReward - _last.MonthlyReward,
        //            Talent = _current.Talent - _last.Talent,
        //            Title = _current.Title - _last.Title,
        //            HealthOfFemale = _current.HealthOfFemale - _last.HealthOfFemale,
        //            HousingSubsidy = _current.HousingSubsidy - _last.HousingSubsidy,
        //            TenPercent = _current.TenPercent - _last.TenPercent,
        //            ProtectingEducation = _current.ProtectingEducation - _last.ProtectingEducation,
        //            SpecialSubsidy = _current.SpecialSubsidy - _last.SpecialSubsidy,
        //            DefenseSubsidy = _current.DefenseSubsidy - _last.DefenseSubsidy,
        //            WageOfTemporaryStaff = _current.WageOfTemporaryStaff - _last.WageOfTemporaryStaff,
        //            PerformanceOfTemporaryStaff = _current.PerformanceOfTemporaryStaff - _last.PerformanceOfTemporaryStaff,
        //            Payable = _current.Payable - _last.Payable,
        //            //扣款
        //            Rent = _current.Rent - _last.Rent,
        //            TotalTax = _current.TotalTax - _last.TotalTax,
        //            Fund = _current.Fund - _last.Fund,
        //            MedicalInsurance = _current.MedicalInsurance - _last.MedicalInsurance,
        //            EndowmentInsurance = _current.EndowmentInsurance - _last.EndowmentInsurance,
        //            OccupationalPension = _current.OccupationalPension - _last.OccupationalPension,
        //            Others = _current.Others - _last.Others,
        //            Water = _current.Water - _last.Water,
        //            Actual = _current.Actual - _last.Actual,
        //            PerformanceOfLastMonth = _current.PerformanceOfLastMonth - _last.PerformanceOfLastMonth,
        //            WithholdingTax = _current.WithholdingTax - _last.WithholdingTax,
        //            //状态标志
        //            MonthStatus = this.MonthStatus,
        //            ChangedStatus = this.ChangedStatus
        //        };
        //        return salary;
        //    }
        //}
        #endregion
    }
}
