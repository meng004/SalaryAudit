using ExcelMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JournalVoucherAudit.Domain
{
    /// <summary>
    /// 离退休人员工资
    /// </summary>
    public class Retirement : User
    {

        #region 工资科目
        /// <summary>
        /// 基本离退休费
        /// </summary>
        [ExcelColumnName("基本离退休费")]
        public decimal Basic { get; set; }
        /// <summary>
        /// 省批生活补贴
        /// </summary>
        [ExcelColumnName("省批生活补贴")]
        public decimal ProvincialSubsidy { get; set; }
        /// <summary>
        /// 校内保留
        /// </summary>
        [ExcelColumnName("校内保留")]
        public decimal Reservation { get; set; }
        /// <summary>
        /// 生贴
        /// </summary>
        [ExcelColumnName("生贴")]
        public decimal LivingSubsidy { get; set; }
        /// <summary>
        /// 国防
        /// </summary>
        [ExcelColumnName("国防")]
        public decimal DefenseSubsidy { get; set; }
        /// <summary>
        /// 核补
        /// </summary>
        [ExcelColumnName("核补")]
        public decimal NuclearSubsidy { get; set; }
        /// <summary>
        /// 独生子女费
        /// </summary>
        [ExcelColumnName("独生子女费")]
        public decimal OnlyChild { get; set; }
        /// <summary>
        /// 中人基本养老金
        /// </summary>
        [ExcelColumnName("中人基本养老金")]
        public decimal MiddleMan { get; set; }
        /// <summary>
        /// 津特贴
        /// </summary>
        [ExcelColumnName("津特贴")]
        public decimal Allowance { get; set; }
        /// <summary>
        /// 护理
        /// </summary>
        [ExcelColumnName("护理")]
        public decimal Nursing { get; set; }
        /// <summary>
        /// 调整的基本养老金
        /// </summary>
        [ExcelColumnName("调整的基本养老金")]
        public decimal AdjustedPension { get; set; }
        /// <summary>
        /// 职业年金
        /// </summary>
        [ExcelColumnName("职业年金")]
        public decimal OccupationalPension { get; set; }
        /// <summary>
        /// 水电
        /// </summary>
        [ExcelColumnName("水电")]
        public decimal Utilities { get; set; }

        #endregion




    }
}
