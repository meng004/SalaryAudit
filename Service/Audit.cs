using JournalVoucherAudit.Domain;
using System;
using System.Collections.Generic;
using System.Linq;

namespace JournalVoucherAudit.Service
{
    public abstract class Audit<B, U>
        where B : Balance<U>
        where U : User, new()
    {
        /// <summary>
        /// 上月工资
        /// </summary>
        protected IList<U> _last_month_salaries;
        /// <summary>
        /// 本月工资
        /// </summary>
        protected IList<U> _current_month_salaries;

        protected Audit(IList<U> last, IList<U> current)
        {
            _last_month_salaries = last;
            _current_month_salaries = current;
        }
        /// <summary>
        /// 上月工资
        /// </summary>
        public IList<U> Last { get { return _last_month_salaries; } }
        /// <summary>
        /// 本月工资
        /// </summary>
        public IList<U> Current { get { return _current_month_salaries; } }

        /// <summary>
        /// 取本月工资与上月相同的记录
        /// 比较规则封装在比较器类，两种，分别为应发金额、实发金额
        /// </summary>
        public IList<U> EqualsWithLastMonth
        {
            get
            {
                //指定比较器，取交集

                //按用户ID和应发比较
                var salaries = _current_month_salaries.Intersect(_last_month_salaries, new SalaryEqualityComparerWithPayable()).ToList();

                //按用户ID和实发比较
                //var salaries = _current_month_salaries.Intersect(_last_month_salaries, new SalaryEqualityComparerWithActual()).ToList();
                //修改工资变动状态
                var result = new List<U>();
                foreach (var item in salaries)
                {
                    item.ChangedStatus = ChangedStatus.UnChanged;
                    result.Add(item as U);                    
                }
                //salaries.ForEach(t => t.ChangedStatus = ChangedStatus.UnChanged);                

                return result;
            }
        }

        /// <summary>
        /// 不相等的记录
        /// 包括三种情况：
        /// userid上月有，本月无；本月有，上月无；两月都有
        /// Item1为上月记录，Item2为本月记录
        /// </summary>
        public Tuple<IList<U>, IList<U>> NotEquals
        {
            get
            {
                //取工资相等的集合
                var equals = EqualsWithLastMonth;
                //取差集
                //工资发生变动的上月记录集合，包括（退休、停薪、离职）和工资调整
                var last_salaries = _last_month_salaries.Except(equals).ToList();
                //last_salaries.ForEach(t => t.ChangedStatus = ChangedStatus.Regulated); 

                //工资发生变动的本月记录集合，包括新入职、工资调整
                var current_salaries = _current_month_salaries.Except(equals).ToList();
                //current_salaries.ForEach(t => t.ChangedStatus = ChangedStatus.Regulated);

                //设置月度状态，上月或本月
                last_salaries.ForEach(t => t.MonthStatus = MonthStatus.Last);
                current_salaries.ForEach(t => t.MonthStatus = MonthStatus.Current);

                var result = new Tuple<IList<U>, IList<U>>(last_salaries, current_salaries);
                return result;
            }
        }

        /// <summary>
        /// 出现在上月，未出现在本月的工资
        /// 可能的理由，离职、停薪、退休、死亡
        /// </summary>
        public IList<U> Retired
        {
            get
            {
                //取上月变动与本月变动的差集
                //userid在上月，但未出现在本月
                var result = NotEquals.Item1.Except(NotEquals.Item2).ToList();
                //修改状态
                result.ForEach(t => t.ChangedStatus = ChangedStatus.Retired);
                return result;
            }
        }

        /// <summary>
        /// 工资变动
        /// userID两月都有，工资不同的记录
        /// Item1为上月记录，Item2为本月记录
        /// </summary>
        public Tuple<IList<U>, IList<U>> ChangedWithSameUserId
        {
            get
            {
                //取交集
                //ID相同，工资变动的本月记录
                var current_salaries = NotEquals.Item2.Intersect(NotEquals.Item1).ToList();
                //ID相同，工资变动的上月记录
                var last_salaries = NotEquals.Item1.Intersect(NotEquals.Item2).ToList();
                //修改状态
                current_salaries.ForEach(t => t.ChangedStatus = ChangedStatus.Regulated);
                last_salaries.ForEach(t => t.ChangedStatus = ChangedStatus.Regulated);

                var result = new Tuple<IList<U>, IList<U>>(last_salaries, current_salaries);
                return result;
            }
        }

        /// <summary>
        /// 新入职
        /// 出现在本月，未出现在上月
        /// </summary>
        public IList<U> NewSalaries
        {
            get
            {
                //上月无记录，本月有记录
                //取新增工资
                var new_ones = NotEquals.Item2.Except(NotEquals.Item1).ToList();
                //修改状态
                new_ones.ForEach(t => t.ChangedStatus = ChangedStatus.New);

                return new_ones;
            }
        }

        /// <summary>
        /// 差额
        /// </summary>
        public abstract IList<B> Balances { get; }
        //{
        //    get
        //    {
        //        //取工资变动记录
        //        var not_equals = ChangedWithSameUserId;
        //        //构造差额
        //        var result = not_equals.Item2.Join(not_equals.Item1,
        //            current => current.UserId,
        //            last => last.UserId,
        //            (current, last) => new U(last, current)
        //            );
        //        //返回结果
        //        return result.ToList();
        //    }
        //}
        /// <summary>
        /// 差额详情
        /// 本月-上月的变动金额
        /// 正为增加，负为减少
        /// </summary>
        public IList<U> BalancesDetailed
        {
            get
            {
                //取工资变动记录
                var result = Balances.Select(t => t.Detail);
                //返回详情
                return result.ToList();
            }
        }
        /// <summary>
        /// 将调整了工资的记录混合
        /// 上月工资在上
        /// 本月工资在下
        /// </summary>
        public IList<U> MashupById
        {
            get
            {
                //取工资变动记录
                var not_equals = ChangedWithSameUserId;
                //人员ID相同，工资不同
                //按ID和月度状态排序
                var result = not_equals.Item1.Concat(not_equals.Item2)
                                             .OrderBy(t => t.UserId)
                                             .ThenBy(t => t.MonthStatus)
                                             .ToList();

                return result;
            }
        }
        /// <summary>
        /// 差额混合
        /// 异动在前，入职、退休在后
        /// </summary>
        public abstract IList<B> Mashup { get; }
        //{
        //    get
        //    {
        //        //按应发降序，按部门、用户编号升序
        //        var balanced = Balances.OrderByDescending(t => t.PayableOfCurrent)
        //                               .ThenBy(t => t.DepartmentName)
        //                               .ThenBy(t => t.UserId);
        //        //取新入职，按部门、用户编号升序
        //        var news = NewSalaries.OrderBy(t => t.DepartmentName)
        //                              .ThenBy(t => t.UserId)
        //                              .Select(t => new U(new T(), t));
        //        //取退休，按部门、用户编号升序
        //        var retired = Retired.OrderBy(t => t.DepartmentName)
        //                             .ThenBy(t => t.UserId)
        //                             .Select(t => new U(t, new T()));

        //        //返回
        //        var balancedWithNew = balanced.Concat(news);
        //        var withRetired = balancedWithNew.Concat(retired);
        //        return withRetired.ToList();
        //    }
        //}
        /// <summary>
        /// 差额混合细节
        /// 异动在前，入职、退休在后
        /// 异动包括上月工资和本月工资
        /// </summary>
        public IList<U> MashupDetailed
        {
            get
            {
                ////按实发降序，按部门、用户ID升序
                //var balanced = BalancesDetailed.OrderByDescending(t => t.Payable)
                //                               .ThenBy(t => t.DepartmentName)
                //                               .ThenBy(t => t.UserId);
                ////因为用户关注在职人员，所以对应记录放在最上面，包括变动 + 新入职
                ////新入职按编号排序
                //var balancedWithNew = balanced.Concat(NewSalaries.OrderBy(t => t.DepartmentName)
                //                                                 .ThenBy(t => t.UserId));
                //// + 退休，按部门排序
                //var withRetired = balancedWithNew.Concat(Retired.OrderBy(t => t.DepartmentName)
                //                                                .ThenBy(t => t.UserId));
                var result = Mashup.Select(t => t.Detail);
                return result.ToList();
            }
        }
        /// <summary>
        /// 应发余额是否平衡
        /// </summary>
        public bool IsBalanced //{ get; }
        {
            get
            {
                //上月应发调节后余额，上月应发 - 上月应发差额小计
                var last = _last_month_salaries.TotalPayable() - Mashup.BalancePayableOfLast<B, U>();
                //本月应发调节后余额，本月应发 - 本月应发差额小计
                var current = _current_month_salaries.TotalPayable() - Mashup.BalancePayableOfCurrent<B, U>();

                return decimal.Equals(last, current);
            }
        }
    }
}
