using ExcelMapper;
using JournalVoucherAudit.Domain;

namespace JournalVoucherAudit.Domain
{
    public class MapOfRetirement : ExcelClassMap<Retirement>
    {
        public MapOfRetirement()
        {
            // 人才绩效 职称绩效  女职工卫生费 住房补贴    百分之十 护教  特贴 国防津贴    
            // 临聘专业技术人员工资 临聘专业技术人员绩效
            // 房租	合计扣税	公积金	医保	扣养老保险	扣职业年金	扣其它	水费	
            // 上月其他绩效	上月预扣税	帐号	身份证号	公积金帐号
            Map(salary => salary.DepartmentName);
            Map(salary => salary.UserId);
            Map(salary => salary.UserName);
            //数值类型
            Map(salary => salary.Basic)
                .WithEmptyFallback(0.0m)
                .WithInvalidFallback(0.0m);
            Map(salary => salary.ProvincialSubsidy)
                .WithEmptyFallback(0.0m)
                .WithInvalidFallback(0.0m);
            Map(salary => salary.Reservation)
                .WithEmptyFallback(0.0m)
                .WithInvalidFallback(0.0m);
            Map(salary => salary.LivingSubsidy)
                .WithEmptyFallback(0.0m)
                .WithInvalidFallback(0.0m);
            Map(salary => salary.DefenseSubsidy)
                .WithEmptyFallback(0.0m)
                .WithInvalidFallback(0.0m);
            Map(salary => salary.ProtectingEducation)
                .WithEmptyFallback(0.0m)
                .WithInvalidFallback(0.0m);
            Map(salary => salary.NuclearSubsidy)
                .WithEmptyFallback(0.0m)
                .WithInvalidFallback(0.0m);
            Map(salary => salary.OnlyChild)
                .WithEmptyFallback(0.0m)
                .WithInvalidFallback(0.0m);
            Map(salary => salary.MiddleMan)
                .WithEmptyFallback(0.0m)
                .WithInvalidFallback(0.0m);
            Map(salary => salary.Allowance)
                .WithEmptyFallback(0.0m)
                .WithInvalidFallback(0.0m);
            Map(salary => salary.Nursing)
                .WithEmptyFallback(0.0m)
                .WithInvalidFallback(0.0m);
            Map(salary => salary.SpecialSubsidy)
                .WithEmptyFallback(0.0m)
                .WithInvalidFallback(0.0m);
            Map(salary => salary.AdjustedPension)
                .WithEmptyFallback(0.0m)
                .WithInvalidFallback(0.0m);
            Map(salary => salary.OccupationalPension)
                .WithEmptyFallback(0.0m)
                .WithInvalidFallback(0.0m);
            Map(salary => salary.Payable)
                .WithEmptyFallback(0.0m)
                .WithInvalidFallback(0.0m);
            Map(salary => salary.Rent)
                .WithEmptyFallback(0.0m)
                .WithInvalidFallback(0.0m);
            Map(salary => salary.Others)
                .WithEmptyFallback(0.0m)
                .WithInvalidFallback(0.0m);
            Map(salary => salary.Utilities)
                .WithEmptyFallback(0.0m)
                .WithInvalidFallback(0.0m);
            Map(salary => salary.Actual)
                .WithEmptyFallback(0.0m)
                .WithInvalidFallback(0.0m);

            //可选项
            //字符串类型
            Map(salary => salary.IdentityNumber)
                .MakeOptional()
                .WithEmptyFallback(string.Empty)
                .WithInvalidFallback(string.Empty);
            Map(salary => salary.BankAccount)
                .MakeOptional()
                .WithEmptyFallback(string.Empty)
                .WithInvalidFallback(string.Empty);
            //不映射
            Map(salary => salary.MonthStatus)
                .MakeOptional()
                .WithEmptyFallback(MonthStatus.Unknown)
                .WithInvalidFallback(MonthStatus.Unknown);
            Map(salary => salary.ChangedStatus)
                .MakeOptional()
                .WithEmptyFallback(ChangedStatus.Unknown)
                .WithInvalidFallback(ChangedStatus.Unknown);
        }
    }
}
