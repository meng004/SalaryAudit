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
        /// 退休、停薪、辞职或死亡
        /// </summary>
        [Description("退休等")]
        Retired,

    }
}