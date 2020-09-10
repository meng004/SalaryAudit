using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JournalVoucherAudit.Domain
{
    /// <summary>
    /// 依据用户ID和应发，相等比较
    /// </summary>
    public class SalaryEqualityComparerWithPayable : IEqualityComparer<Salary>
    {
        bool IEqualityComparer<Salary>.Equals(Salary x, Salary y)
        {
            //人员代码相等
            var keyIsEqual = string.Compare(x.UserId, y.UserId);
            //应发金额相等
            var countIsEqual = decimal.Compare(x.Payable, y.Payable);
            return (keyIsEqual == 0) && (countIsEqual == 0);
        }

        int IEqualityComparer<Salary>.GetHashCode(Salary obj)
        {
            return obj.UserId.GetHashCode();
        }
    }

    /// <summary>
    /// 依据用户ID和实发，相等比较
    /// </summary>
    public class SalaryEqualityComparerWithActual : IEqualityComparer<Salary>
    {
        bool IEqualityComparer<Salary>.Equals(Salary x, Salary y)
        {
            //人员代码相等
            var keyIsEqual = string.Compare(x.UserId, y.UserId);
            //实发金额相等
            var countIsEqual = decimal.Compare(x.Actual, y.Actual);
            return (keyIsEqual == 0) && (countIsEqual == 0);
        }

        int IEqualityComparer<Salary>.GetHashCode(Salary obj)
        {
            return obj.UserId.GetHashCode();
        }
    }
}
