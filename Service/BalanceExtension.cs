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
        public static decimal BalancePayableOfLast(this IList<Balance> balances)
        {
            return balances.Sum(t => t.PayableOfLast);
        }
        /// <summary>
        /// 本月应发差额小计
        /// </summary>
        /// <param name="balances"></param>
        /// <returns></returns>
        public static decimal BalancePayableOfCurrent(this IList<Balance> balances)
        {
            return balances.Sum(t => t.PayableOfCurrent);
        }
        /// <summary>
        /// 上月实发差额小计
        /// </summary>
        /// <param name="balances"></param>
        /// <returns></returns>
        public static decimal BalanceActualOfLast(this IList<Balance> balances)
        {
            return balances.Sum(t => t.ActualOfLast);
        }
        /// <summary>
        /// 本月实发差额小计
        /// </summary>
        /// <param name="balances"></param>
        /// <returns></returns>
        public static decimal BalanceActualOfCurrent(this IList<Balance> balances)
        {
            return balances.Sum(t => t.ActualOfCurrent);
        }
    }
}
