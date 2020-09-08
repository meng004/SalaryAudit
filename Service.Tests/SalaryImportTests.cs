using System;
using JournalVoucherAudit.Service;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Service.Tests
{
    [TestClass]
    public class SalaryImportTests
    {
        private string _filepath = @"D:\项目\谭林艳\工资\7月工资\7月在职人员工资.xls";

        [TestMethod]
        public void Salaries_NotZero()
        {
            var importer = new SalaryImport(_filepath);
            Assert.AreNotEqual(0, importer.Salaries.Count);
        }
    }
}
