using ExcelMapper;
using System;
using System.Collections.Generic;

namespace JournalVoucherAudit.Domain
{
    public abstract class User : IEquatable<User>
    {
        #region 个人基本信息
        /// <summary>
        /// 部门名称
        /// </summary>
        [ExcelColumnName("部门名称")]
        public string DepartmentName { get; set; }
        /// <summary>
        /// 人员代码
        /// </summary>
        [ExcelColumnName("人员代码")]
        public string UserId { get; set; }
        /// <summary>
        /// 姓名
        /// </summary>
        [ExcelColumnName("姓名")]
        public string UserName { get; set; }

        #endregion

        #region 工资科目

        /// <summary>
        /// 护教
        /// </summary>
        [ExcelColumnName("护教")]
        public decimal ProtectingEducation { get; set; }
        /// <summary>
        /// 特贴
        /// </summary>
        [ExcelColumnName("特贴")]
        public decimal SpecialSubsidy { get; set; }
        /// <summary>
        /// 应发工资
        /// </summary>
        [ExcelColumnName("应发工资")]
        public decimal Payable { get; set; }
        /// <summary>
        /// 房租
        /// </summary>
        [ExcelColumnName("房租")]
        public decimal Rent { get; set; }
        /// <summary>
        /// 扣其它
        /// </summary>
        [ExcelColumnName("扣其它")]
        public decimal Others { get; set; }
        /// <summary>
        /// 实发工资
        /// </summary>
        [ExcelColumnName("实发工资")]
        public decimal Actual { get; set; }

        #endregion

        #region 账号

        /// <summary>
        /// 账号
        /// </summary>
        [ExcelColumnName("帐号")]
        public string BankAccount { get; set; }
        /// <summary>
        /// 身份证号
        /// </summary>
        [ExcelColumnName("身份证号")]
        public string IdentityNumber { get; set; }

        #endregion

        #region 标志

        /// <summary>
        /// 月度，上月或本月
        /// </summary>
        public MonthStatus MonthStatus { get; set; }

        /// <summary>
        /// 工资变动理由，新增、调整、退休或离职、死亡等
        /// </summary>
        public ChangedStatus ChangedStatus { get; set; }

        #endregion

        #region 实现IEquatable

        public override bool Equals(object obj)
        {
            return Equals(obj as User);
        }
        /// <summary>
        /// 使用用户ID进行相等比较
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public bool Equals(User other)
        {
            return other != null &&
                   UserId == other.UserId;
        }
        public override int GetHashCode()
        {
            int hashCode = 356858736;
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(UserId);
            return hashCode;
        }

        #endregion
    }
}