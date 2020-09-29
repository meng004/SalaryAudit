using ExcelMapper;

namespace JournalVoucherAudit.Domain
{
    /// <summary>
    /// 在职人员工资
    /// 默认使用用户ID进行相等比较
    /// </summary>
    public class Salary : User
    {
        //#region 个人基本信息
        ///// <summary>
        ///// 部门名称
        ///// </summary>
        //[ExcelColumnName("部门名称")]
        //public string DepartmentName { get; set; }
        ///// <summary>
        ///// 人员代码
        ///// </summary>
        //[ExcelColumnName("人员代码")]
        //public string UserId { get; set; }
        ///// <summary>
        ///// 姓名
        ///// </summary>
        //[ExcelColumnName("姓名")]
        //public string UserName { get; set; }

        //#endregion

        #region 工资科目
        /// <summary>
        /// 岗位工资
        /// </summary>
        [ExcelColumnName("岗位工资")]
        public decimal Position { get; set; }
        /// <summary>
        /// 薪级工资
        /// </summary>
        [ExcelColumnName("薪级工资")]
        public decimal Scale { get; set; }
        /// <summary>
        /// 绩效工资
        /// </summary>
        [ExcelColumnName("基础绩效工资")]
        public decimal Performance { get; set; }
        /// <summary>
        /// 月度奖励绩效
        /// </summary>
        [ExcelColumnName("月度奖励绩效")]
        public decimal MonthlyReward { get; set; }
        /// <summary>
        /// 人才绩效
        /// </summary>
        [ExcelColumnName("人才绩效")]
        public decimal Talent { get; set; }
        /// <summary>
        /// 职称绩效
        /// </summary>
        [ExcelColumnName("职称绩效")]
        public decimal Title { get; set; }
        /// <summary>
        /// 女职工卫生费
        /// </summary>
        [ExcelColumnName("女职工卫生费")]
        public decimal HealthOfFemale { get; set; }
        /// <summary>
        /// 住房补贴
        /// </summary>
        [ExcelColumnName("住房补贴")]
        public decimal HousingSubsidy { get; set; }
        /// <summary>
        /// 百分之十
        /// </summary>
        [ExcelColumnName("百分之十")]
        public decimal TenPercent { get; set; }
        ///// <summary>
        ///// 护教
        ///// </summary>
        //[ExcelColumnName("护教")]
        //public decimal ProtectingEducation { get; set; }
        ///// <summary>
        ///// 特贴
        ///// </summary>
        //[ExcelColumnName("特贴")]
        //public decimal SpecialSubsidy { get; set; }
        /// <summary>
        /// 国防津贴
        /// </summary>
        [ExcelColumnName("国防津贴")]
        public decimal DefenseSubsidy { get; set; }
        /// <summary>
        /// 临聘人员工资
        /// </summary>
        [ExcelColumnName("临聘专业技术人员工资")]
        public decimal WageOfTemporaryStaff { get; set; }
        /// <summary>
        /// 临聘人员绩效
        /// </summary>
        [ExcelColumnName("临聘专业技术人员绩效")]
        public decimal PerformanceOfTemporaryStaff { get; set; }
        ///// <summary>
        ///// 应发工资
        ///// </summary>
        //[ExcelColumnName("应发工资")]
        //public decimal Payable { get; set; }
        ///// <summary>
        ///// 房租
        ///// </summary>
        //[ExcelColumnName("房租")]
        //public decimal Rent { get; set; }
        /// <summary>
        /// 合计扣税
        /// </summary>
        [ExcelColumnName("合计扣税")]
        public decimal TotalTax { get; set; }
        /// <summary>
        /// 公积金
        /// </summary>
        [ExcelColumnName("公积金")]
        public decimal Fund { get; set; }
        /// <summary>
        /// 医保
        /// </summary>
        [ExcelColumnName("医保")]
        public decimal MedicalInsurance { get; set; }
        /// <summary>
        /// 养老保险
        /// </summary>
        [ExcelColumnName("扣养老保险")]
        public decimal EndowmentInsurance { get; set; }
        /// <summary>
        /// 职业年金
        /// </summary>
        [ExcelColumnName("扣职业年金")]
        public decimal OccupationalPension { get; set; }
        ///// <summary>
        ///// 扣其它
        ///// </summary>
        //[ExcelColumnName("扣其它")]
        //public decimal Others { get; set; }
        /// <summary>
        /// 水费
        /// </summary>
        [ExcelColumnName("水费")]
        public decimal Water { get; set; }
        ///// <summary>
        ///// 实发工资
        ///// </summary>
        //[ExcelColumnName("实发工资")] 
        //public decimal Actual { get; set; }
        /// <summary>
        /// 上月其它绩效
        /// </summary>
        [ExcelColumnName("上月其他绩效")]
        public decimal PerformanceOfLastMonth { get; set; }
        /// <summary>
        /// 预扣税
        /// </summary>
        [ExcelColumnName("上月预扣税")]
        public decimal WithholdingTax { get; set; }

        #endregion

        #region 账号

        ///// <summary>
        ///// 账号
        ///// </summary>
        //[ExcelColumnName("帐号")]
        //public string BankAccount { get; set; }
        ///// <summary>
        ///// 身份证号
        ///// </summary>
        //[ExcelColumnName("身份证号")]
        //public string IdentityNumber { get; set; }
        /// <summary>
        /// 公积金帐号
        /// </summary>
        [ExcelColumnName("公积金帐号")]
        public string FundAccount { get; set; }
        #endregion

        //#region 标志

        ///// <summary>
        ///// 月度，上月或本月
        ///// </summary>
        //public MonthStatus MonthStatus { get; set; }

        ///// <summary>
        ///// 工资变动理由，新增、调整、退休或离职、死亡等
        ///// </summary>
        //public ChangedStatus ChangedStatus { get; set; }

        //#endregion

        //#region 实现IEquatable

        //public override bool Equals(object obj)
        //{
        //    return Equals(obj as Salary);
        //}
        ///// <summary>
        ///// 使用用户ID进行相等比较
        ///// </summary>
        ///// <param name="other"></param>
        ///// <returns></returns>
        //public bool Equals(Salary other)
        //{
        //    return other != null &&
        //           UserId == other.UserId;
        //}
        //public override int GetHashCode()
        //{
        //    int hashCode = 356858736;
        //    hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(UserId);
        //    return hashCode;
        //}

        //#endregion
    }
}
