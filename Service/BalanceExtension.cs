using JournalVoucherAudit.Domain;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;

namespace JournalVoucherAudit.Service
{
    public static class BalanceExtension
    {
        /// <summary>
        /// 上月应发差额小计
        /// </summary>
        /// <param name="balances"></param>
        /// <returns></returns>
        public static decimal BalancePayableOfLast<T, U>(this IList<T> balances)
            where T : Balance<U>
            where U : User, new()
        {
            return balances.Sum(t => t.PayableOfLast);
        }
        /// <summary>
        /// 本月应发差额小计
        /// </summary>
        /// <param name="balances"></param>
        /// <returns></returns>
        public static decimal BalancePayableOfCurrent<T, U>(this IList<T> balances)
            where T : Balance<U>
            where U : User, new()
        {
            return balances.Sum(t => t.PayableOfCurrent);
        }
        /// <summary>
        /// 上月实发差额小计
        /// </summary>
        /// <param name="balances"></param>
        /// <returns></returns>
        public static decimal BalanceActualOfLast<T, U>(this IList<T> balances)
            where T : Balance<U>
            where U : User, new()
        {
            return balances.Sum(t => t.ActualOfLast);
        }
        /// <summary>
        /// 本月实发差额小计
        /// </summary>
        /// <param name="balances"></param>
        /// <returns></returns>
        public static decimal BalanceActualOfCurrent<T, U>(this IList<T> balances)
            where T : Balance<U>
            where U : User, new()
        {
            return balances.Sum(t => t.ActualOfCurrent);
        }
    }
}
