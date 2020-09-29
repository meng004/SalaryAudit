namespace JournalVoucherAudit.Domain
{
    /// <summary>
    /// 工资差额
    /// 抽象类
    /// </summary>
    public abstract class Balance<U> 
        where U : User
    {
        #region 字段

        /// <summary>
        /// 上月工资
        /// </summary>
        protected U _last;
        /// <summary>
        /// 本月工资
        /// </summary>
        protected U _current;

        protected Balance(U last, U current)
        {
            _last = last;
            _current = current;
        }


        #endregion

        #region 个人基本信息
        /// <summary>
        /// 部门名称
        /// </summary>
        public string DepartmentName => _current.MonthStatus == MonthStatus.Unknown
                                                             ? _last.DepartmentName
                                                             : _current.DepartmentName;
        /// <summary>
        /// 人员代码
        /// </summary>
        public string UserId => _current.MonthStatus == MonthStatus.Unknown
                                                     ? _last.UserId
                                                     : _current.UserId;
        /// <summary>
        /// 姓名
        /// </summary>
        public string UserName => _current.MonthStatus == MonthStatus.Unknown
                                                       ? _last.UserName
                                                       : _current.UserName;
        /// <summary>
        /// 工资变动事由
        /// </summary>
        public ChangedStatus ChangedStatus
        {
            get
            {
                //如果退休
                if (_current.ChangedStatus == ChangedStatus.Unknown)
                    return _last.ChangedStatus;
                else
                    return _current.ChangedStatus;
            }
        }
        /// <summary>
        /// 月度状态，本月或上月
        /// </summary>
        public MonthStatus MonthStatus
        {
            get
            {
                //如果退休
                if (_current.MonthStatus == MonthStatus.Unknown)
                    return _last.MonthStatus;
                else
                    return _current.MonthStatus;
            }
        }
        #endregion

        #region 工资
        /// <summary>
        /// 上月工资
        /// </summary>
        public U Last { get { return _last; } }
        /// <summary>
        /// 本月工资
        /// </summary>
        public U Current { get { return _current; } }
        /// <summary>
        /// 上月应发
        /// </summary>
        public decimal PayableOfLast { get { return _last.Payable; } }
        /// <summary>
        /// 本月应发
        /// </summary>
        public decimal PayableOfCurrent { get { return _current.Payable; } }
        /// <summary>
        /// 上月实发
        /// </summary>
        public decimal ActualOfLast { get { return _last.Actual; } }
        /// <summary>
        /// 本月实发
        /// </summary>
        public decimal ActualOfCurrent { get { return _current.Actual; } }
        /// <summary>
        /// 工资差额
        /// 由子类实现
        /// </summary>
        public abstract U Detail { get; }
 
        #endregion
    }
}
