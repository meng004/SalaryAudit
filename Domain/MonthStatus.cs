using System.ComponentModel;

namespace JournalVoucherAudit.Domain
{
    public enum MonthStatus
    {
        /// <summary>
        /// 未知
        /// </summary>
        [Description("未知")]
        Unknown,
        /// <summary>
        /// 上月
        /// </summary>
        [Description("上月")]
        Last,
        /// <summary>
        /// 本月
        /// </summary>
        [Description("本月")]
        Current,
    }
}
