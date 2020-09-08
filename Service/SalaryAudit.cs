using JournalVoucherAudit.Domain;
using System;
using System.Collections.Generic;
using System.Linq;

namespace JournalVoucherAudit.Service
{
    public class SalaryAudit
    {
        /// <summary>
        /// 上月工资
        /// </summary>
        private IList<Salary> _last_month_salaries;
        /// <summary>
        /// 本月工资
        /// </summary>
        private IList<Salary> _current_month_salaries;
        /// <summary>
        /// 工资审计
        /// </summary>
        /// <param name="last_month_salaries">上月工资</param>
        /// <param name="current_month_salaries">本月工资</param>
        public SalaryAudit(IList<Salary> last_month_salaries, IList<Salary> current_month_salaries)
        {
            _last_month_salaries = last_month_salaries;
            _current_month_salaries = current_month_salaries;
        }

        /// <summary>
        /// 取本月工资与上月相同的记录
        /// </summary>
        public IList<Salary> EqualsWithLastMonth
        {
            get
            {
                var salaries = _current_month_salaries.Intersect(_last_month_salaries, new SalaryEqualityComparer()).ToList();
                salaries.ForEach(t => t.ChangedStatus = ChangedStatus.UnChanged);
                
                return salaries;
            }
        }

        /// <summary>
        /// 排除应发金额相等之后的记录，Item1为上月工资，Item2为本月工资
        /// </summary>
        public Tuple<IList<Salary>, IList<Salary>> NotEqualsWithLastMonth
        {
            get
            {
                //取应发相等的工资
                var equals = EqualsWithLastMonth;
                //上月应发不相等的工资
                var last_salaries = _last_month_salaries.Except(equals).ToList();
                last_salaries.ForEach(t => t.ChangedStatus = ChangedStatus.Regulated); 
                
                //本月应发不相等的工资
                var current_salaries = _current_month_salaries.Except(equals).ToList();
                current_salaries.ForEach(t => t.ChangedStatus = ChangedStatus.Regulated);

                //设置月度状态，上月或本月
                last_salaries.ForEach(t => t.Status = MonthStatus.Last);
                current_salaries.ForEach(t => t.Status = MonthStatus.Current);

                var result = new Tuple<IList<Salary>, IList<Salary>>(last_salaries, current_salaries);
                return result;
            }
        }
        /// <summary>
        /// 出现在上月，未出现在本月的工资
        /// 可能的理由，转停薪、退休、死亡
        /// </summary>
        public IList<Salary> NotInCurrent
        {
            get
            {
                var last = NotEqualsWithLastMonth.Item1;
                var current = NotEqualsWithLastMonth.Item2;
                var result = last.Except(current).ToList();
                return result;
            }
        }

        /// <summary>
        /// 人员ID相同，应发金额不相等的记录，分别为上月工资、本月工资
        /// </summary>
        public Tuple<IList<Salary>, IList<Salary>> ChangedByExistId
        {
            get
            {
                //取应发不相等的工资
                var not_equals = NotEqualsWithLastMonth;
                //本月应发不相等的工资
                var current_salaries = not_equals.Item2.Intersect(not_equals.Item1).ToList();
                //上月应发不相等的工资
                var last_salaries = not_equals.Item1.Intersect(not_equals.Item2).ToList();
                var result = new Tuple<IList<Salary>, IList<Salary>>(last_salaries, current_salaries);
                return result;
            }
        }
        /// <summary>
        /// 本月新增工资
        /// </summary>
        public IList<Salary> NewSalaries
        {
            get
            {
                var not_equals = NotEqualsWithLastMonth;
                //取新增工资
                var new_ones = not_equals.Item2.Except(not_equals.Item1).ToList();
                new_ones.ForEach(t => t.ChangedStatus = ChangedStatus.New);
                return new_ones;
            }
        }
        /// <summary>
        /// 计算本月各项的变动金额
        /// </summary>
        public IList<Salary> MashupById
        {
            get
            {
                var not_equals = ChangedByExistId;
                //人员ID相同，应发金额不同的记录混合
                /// 先按ID排序，再按月度排序
                //var result = not_equals.Item1.Concat(not_equals.Item2).OrderBy(t=> t.UserId).ThenBy(t=>t.Status);
                var result = not_equals.Item2.Join(not_equals.Item1,
                    current => current.UserId,
                    last => last.UserId,
                    (current, last) => new Salary
                    {
                        UserId = current.UserId,
                        UserName = current.UserName,
                        DepartmentName = current.DepartmentName,
                        Position = current.Position - last.Position,
                        Scale = current.Scale - last.Scale,
                        Performance = current.Performance - last.Performance,
                        MonthlyReward = current.MonthlyReward - last.MonthlyReward,
                        Talent = current.Talent - last.Talent,
                        Title = current.Title - last.Title,
                        HealthOfFemale = current.HealthOfFemale - last.HealthOfFemale,
                        HousingSubsidy = current.HousingSubsidy - last.HousingSubsidy,
                        TenPercent = current.TenPercent - last.TenPercent,
                        ProtectingEducation = current.ProtectingEducation - last.ProtectingEducation,
                        SpecialSubsidy = current.SpecialSubsidy - last.SpecialSubsidy,
                        DefenseSubsidy = current.DefenseSubsidy - last.DefenseSubsidy,
                        WageOfTemporaryStaff = current.WageOfTemporaryStaff - last.WageOfTemporaryStaff,
                        PerformanceOfTemporaryStaff = current.PerformanceOfTemporaryStaff - last.PerformanceOfTemporaryStaff,
                        Payable = current.Payable - last.Payable,
                        //扣款
                        Rent = current.Rent - last.Rent,
                        TotalTax = current.TotalTax - last.TotalTax,
                        Fund = current.Fund - last.Fund,
                        MedicalInsurance = current.MedicalInsurance - last.MedicalInsurance,
                        EndowmentInsurance = current.EndowmentInsurance - last.EndowmentInsurance,
                        OccupationalPension = current.OccupationalPension - last.OccupationalPension,
                        Others = current.Others - last.Others,
                        Water = current.Water - last.Water,
                        Actual = current.Actual - last.Actual,
                        PerformanceOfLastMonth = current.PerformanceOfLastMonth - last.PerformanceOfLastMonth,
                        WithholdingTax = current.WithholdingTax - last.WithholdingTax,
                        Status = current.Status,
                        ChangedStatus = current.ChangedStatus
                    });
                return result.ToList();
            }
        }

        /// <summary>
        /// 本月应发金额异动记录
        /// 新增在前，异动在后
        /// 异动包括上月工资和本月工资
        /// </summary>
        public IList<Salary> MashupAll
        {
            get
            {
                var result = MashupById.Concat(NewSalaries).ToList();
                
                return result;
            }
        }
    }
}
