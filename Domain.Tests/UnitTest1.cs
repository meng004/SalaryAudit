using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using JournalVoucherAudit.Domain;
using ExcelMapper;
using System.IO;
using System.Linq;
using ExcelDataReader;

namespace JournalVoucherAudit.Domain.Tests
{
    [TestClass]
    public class UnitTest1
    {
        //map已移动到service，该测试不在有效
        //[TestMethod]
        //public void TestMethod1()
        //{
        //    using (var stream = File.OpenRead(@"D:\项目\谭林艳\工资\7月工资\7月在职人员工资.xls"))
        //    {
        //        using (var importer = new ExcelImporter(stream))
        //        {
        //            importer.Configuration.RegisterClassMap<SalaryMap>();
        //            var sheet = importer.ReadSheet();
        //            sheet.HeadingIndex = 2;

        //            var rows = sheet.ReadRows<Salary>().ToList();
        //            var count = rows.Count;
        //            var last1 = rows.LastOrDefault();
        //            var last2 = rows.ElementAt(rows.Count - 2);
        //            rows.RemoveRange(rows.Count - 2, 2);
        //            Assert.AreNotEqual(0.0m, rows.Count);
        //        }
        //    }
        //}
    }
}
