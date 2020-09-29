using ExcelMapper;
using JournalVoucherAudit.Domain;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace JournalVoucherAudit.Service
{
    public abstract class Import<U, M>
        where U : User, new()
        where M : ExcelClassMap, new()
    {
        #region 属性
        /// <summary>
        /// 工资文件路径
        /// </summary>
        protected string _filepath { get; set; }
        /// <summary>
        /// 标题行行号
        /// </summary>
        protected int _header_row_index { get; set; }
        /// <summary>
        /// 数据与末尾的间隔行数
        /// </summary>
        protected int _last_row_index { get; set; }
        /// <summary>
        /// 工资文件导入
        /// </summary>
        /// <param name="filepath">excel文件，包含路径</param>
        /// <param name="header_row_index">标题行行号</param>
        /// <param name="last_row_index">数据与末尾的间隔行数</param>
        public Import(string filepath, int header_row_index = 2, int last_row_index = 2)
        {
            _filepath = filepath;
            _header_row_index = header_row_index;
            _last_row_index = last_row_index;
        }

        #endregion

        /// <summary>
        /// 工资列表
        /// </summary>
        public IList<U> Salaries
        {
            get
            {
                using (var stream = File.OpenRead(_filepath))
                {
                    using (var importer = new ExcelImporter(stream))
                    {
                        importer.Configuration.RegisterClassMap<M>();
                        var sheet = importer.ReadSheet();
                        //设置标题行的行号
                        sheet.HeadingIndex = _header_row_index;

                        var rows = sheet.ReadRows<U>().ToList();
                        //删除非数据行
                        rows.RemoveRange(rows.Count - _last_row_index, _last_row_index);
                        return rows;
                    }
                }
            }
        }
    }
}
