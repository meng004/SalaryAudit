using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JournalVoucherAudit.Domain
{
    public class SalaryEqualityComparer : IEqualityComparer<Salary>
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
}
