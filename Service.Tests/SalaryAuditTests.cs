using System;
using System.Collections.Generic;
using JournalVoucherAudit.Domain;
using JournalVoucherAudit.Service;
using Microsoft.VisualStudio.TestTools.UnitTesting;


namespace Service.Tests
{
    [TestClass]
    public class SalaryAuditTests
    {
        private string _last_month_filepath = @"D:\项目\谭林艳\工资\7月工资\6月在职人员工资.xls";

        private string _current_month_filepath = @"D:\项目\谭林艳\工资\7月工资\7月在职人员工资.xls";

        private IList<Salary> _last_month_salaries;

        private IList<Salary> _current_month_salaries;

        private SalaryAudit _audit;

        [TestInitialize]
        public void Initial()
        {
            var _last_month_importer = new SalaryImport(_last_month_filepath);
            var _current_month_importer = new SalaryImport(_current_month_filepath);

            _last_month_salaries = _last_month_importer.Salaries;
            _current_month_salaries = _current_month_importer.Salaries;

            _audit = new SalaryAudit(_last_month_salaries, _current_month_salaries);
        }

        [TestMethod]
        public void EqualsWithLastMonth_NotZero()
        {
            var equals = _audit.EqualsWithLastMonth;
            Assert.AreNotEqual(0, equals.Count);
        }

        [TestMethod]
        public void NotEqualsWithLastMonth_NotZero()
        {
            var not_equals = _audit.NotEquals;
            Assert.AreNotEqual(0, not_equals.Item1.Count);
            Assert.AreNotEqual(0, not_equals.Item2.Count);
        }

        [TestMethod]
        public void NotEqualsByExistId_NotZero()
        {
            var not_equals = _audit.ChangedWithSameUserId;
            Assert.AreNotEqual(0, not_equals.Item1.Count);
            Assert.AreNotEqual(0, not_equals.Item2.Count);
        }

        [TestMethod]
        public void NewSalaries_NotZero()
        {
            var new_ones = _audit.NewSalaries;
            Assert.AreNotEqual(0, new_ones.Count);
        }

        [TestMethod]
        public void MashupById_NotZero()
        {
            var new_ones = _audit.MashupById;
            Assert.AreNotEqual(0, new_ones.Count);
        }

        [TestMethod]
        public void MashupAll_NotZero()
        {
            var all = _audit.MashupDetailed;
            Assert.AreNotEqual(0, all.Count);
        }

        [TestCleanup]
        public void MyTestMethod()
        {

        }
    }
}
