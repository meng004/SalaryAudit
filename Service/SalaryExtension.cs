using JournalVoucherAudit.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JournalVoucherAudit.Service
{
    public static class SalaryExtension
    {
        /// <summary>
        /// 应发累计
        /// </summary>
        /// <param name="salaries"></param>
        /// <returns></returns>
        public static decimal TotalPayable<T>(this IList<T> salaries)
            where T : User, new()
        {
            return salaries.Sum(t => t.Payable);
        }
        /// <summary>
        /// 实发累计
        /// </summary>
        /// <param name="salaries"></param>
        /// <returns></returns>
        public static decimal TotalActual<T>(this IList<T> salaries)
            where T : User, new()
        {
            return salaries.Sum(t => t.Actual);
        }
    }
}
