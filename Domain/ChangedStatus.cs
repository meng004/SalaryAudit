using System.ComponentModel;

namespace JournalVoucherAudit.Domain
{
    /// <summary>
    /// 工资变动理由，入职、调整、停薪、退休、死亡等
    /// </summary>
    public enum ChangedStatus
    {
        /// <summary>
        /// 未知
        /// </summary>
        [Description("未知")]
        Unknown,
        /// <summary>
        /// 无变动
        /// </summary>
        [Description("无变动")]
        UnChanged,
        /// <summary>
        /// 入职起薪
        /// </summary>
        [Description("入职")]
        New,
        /// <summary>
        /// 调整
        /// </summary>
        [Description("调整")]
        Regulated,
        /// <summary>
        /// 停薪
        /// </summary>
        [Description("停薪")]
        Unpaid,
        /// <summary>
        /// 退休
        /// </summary>
        [Description("退休")]
        Retired,
        /// <summary>
        /// 死亡
        /// </summary>
        [Description("死亡")]
        Departed,
    }
}