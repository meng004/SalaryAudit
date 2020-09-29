using JournalVoucherAudit.Domain;
using System.Collections.Generic;
using System.Linq;

namespace JournalVoucherAudit.Service
{
    public class AuditOfRetirement : Audit<Retirement, BalanceOfRetirement>
    {
        public AuditOfRetirement(IList<Retirement> last, IList<Retirement> current) : base(last, current)
        {
        }

        public override IList<BalanceOfRetirement> Balances
        {
            get
            {
                //取工资变动记录
                var not_equals = ChangedWithSameUserId;
                //构造差额
                var result = not_equals.Item2.Join(not_equals.Item1,
                    current => current.UserId,
                    last => last.UserId,
                    (current, last) => new BalanceOfRetirement(last, current)
                    );
                //返回结果
                return result.ToList();
            }
        }


        public override IList<BalanceOfRetirement> Mashup
        {
            get
            {
                //按应发降序，按部门、用户编号升序
                var balanced = Balances.OrderByDescending(t => t.PayableOfCurrent)
                                       .ThenBy(t => t.DepartmentName)
                                       .ThenBy(t => t.UserId);
                //取新入职，按部门、用户编号升序
                var news = NewSalaries.OrderBy(t => t.DepartmentName)
                                      .ThenBy(t => t.UserId)
                                      .Select(t => new BalanceOfRetirement(new Retirement(), t));
                //取退休，按部门、用户编号升序
                var retired = Retired.OrderBy(t => t.DepartmentName)
                                     .ThenBy(t => t.UserId)
                                     .Select(t => new BalanceOfRetirement(t, new Retirement()));

                //返回
                var balancedWithNew = balanced.Concat(news);
                var withRetired = balancedWithNew.Concat(retired);
                return withRetired.ToList();
            }
        }


        //public override bool IsBalanced
        //{
        //    get
        //    {
        //        //上月应发调节后余额，上月应发 - 上月应发差额小计
        //        var last = _last_month_salaries.TotalPayable() - Mashup.BalancePayableOfLast<BalanceOfRetirement, Retirement>();
        //        //本月应发调节后余额，本月应发 - 本月应发差额小计
        //        var current = _current_month_salaries.TotalPayable() - Mashup.BalancePayableOfCurrent<BalanceOfRetirement, Retirement>();

        //        return decimal.Equals(last, current);
        //    }
        //}
    }
}
