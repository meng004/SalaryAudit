using ExcelReport;
using ExcelReport.Driver.NPOI;
using ExcelReport.Renderers;
using JournalVoucherAudit.Domain;
using JournalVoucherAudit.Utility;
using NPOI.Extend;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace JournalVoucherAudit.Service
{
    public class Export
    {
        /// <summary>
        /// 修改第一个sheet的名称
        /// </summary>
        /// <param name="filename">文件名，包括路径</param>
        /// <param name="sheetName">新的sheet名</param>
        private void SetSheetName(string filename, string sheetName)
        {
            //更改sheetName
            var workbook = NPOIHelper.LoadWorkbook(filename);
            workbook.SetSheetName(0, sheetName);
            //保存文件            
            using (FileStream stream = File.OpenWrite(filename))
            {
                var buffer = workbook.SaveToBuffer();
                stream.Write(buffer, 0, buffer.Length);
                stream.Flush();
            }
            //文件更名
            //var path = Path.GetDirectoryName(filename);
            //var extension = Path.GetExtension(filename);
            //var newFilename = Path.Combine(path, sheetName + extension);
            //File.Move(filename, newFilename);
        }


        /// <summary>
        /// 导出保存
        /// </summary>
        /// <param name="filename">文件名</param>
        /// <param name="salaries">异动工资记录，包含上月与本月</param>
        /// <param name="lastTotal">上月应发合计</param>
        /// <param name="currentTotal">本月应发合计</param>
        public void Save(string filename, IEnumerable<Salary> salaries, Dictionary<string, decimal> salaryStatistics)
        {
            // 项目启动时，添加
            Configurator.Put(".xlsx", new WorkbookLoader());
            // 获取当前运行目录
            var path = System.AppDomain.CurrentDomain.BaseDirectory;

            var tiaoJieItems = salaries as IList<Salary> ?? salaries.ToList();
            //计算本月异动记录的各项小计
            var current = tiaoJieItems.Where(t => t.Status == MonthStatus.Current).ToList();
            //岗位工资
            var positionTotal = current.Sum(t => t.Position);
            //薪级工资
            var scaleTotal = current.Sum(t => t.Scale);
            //基础绩效工资
            var performanceTotal = current.Sum(t => t.Performance);
            //月度奖励绩效
            var monthlyRewardTotal = current.Sum(t => t.MonthlyReward);

            //对账日期
            //月份的最后一天
            var lastDayOfMonth = DateTime.Today.LastDayOfMonth();

            //输出excel
            ExportHelper.ExportToLocal(path + @"Template\Template_new.xlsx", filename,
                new SheetRenderer("工资调节表",
                    new ParameterRenderer("CurrentDate", lastDayOfMonth.ToLongDateString()),//格式为2019年12月31日
                    new ParameterRenderer("LastTotal", salaryStatistics["last_total"]),
                    new ParameterRenderer("CurrentTotal", salaryStatistics["current_total"]),
                    new ParameterRenderer("PositionTotal", positionTotal),
                    new ParameterRenderer("ScaleTotal", scaleTotal),
                    new ParameterRenderer("PerformanceTotal", performanceTotal),
                    new ParameterRenderer("MonthlyRewardTotal", monthlyRewardTotal),
                    new ParameterRenderer("PayableTotal", salaryStatistics["current_subtotal"]),
                    new ParameterRenderer("LastPayableBalance", salaryStatistics["last_balance"]),
                    new ParameterRenderer("CurrentPayableBalance", salaryStatistics["current_balance"]),
                    new RepeaterRenderer<Salary>("Reconciliation", tiaoJieItems,
                        // 部门 人员代码 姓名 月度 岗位工资 薪级工资 基础绩效工资 月度奖励工资 应发工资  工资变更状态   
                        new ParameterRenderer<Salary>("DepartmentName", t => t.DepartmentName),
                        new ParameterRenderer<Salary>("UserId", t => t.UserId),
                        new ParameterRenderer<Salary>("UserName", t => t.UserName),
                        new ParameterRenderer<Salary>("Status", t => t.Status.GetDescription()),//== MonthStatus.Current ? "本月" : (t.Status == MonthStatus.Last ? "上月" : "未知")),
                        new ParameterRenderer<Salary>("Position", t => t.Position),
                        new ParameterRenderer<Salary>("Scale", t => t.Scale),
                        new ParameterRenderer<Salary>("Performance", t => t.Performance),
                        new ParameterRenderer<Salary>("MonthlyReward", t => t.MonthlyReward),
                        new ParameterRenderer<Salary>("Payable", t => t.Payable),
                        new ParameterRenderer<Salary>("ChangedStatus", t => t.ChangedStatus.GetDescription())
                        )
                    )
                );
        }
    }
}
