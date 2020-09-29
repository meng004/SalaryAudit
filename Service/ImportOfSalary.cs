using JournalVoucherAudit.Domain;

namespace JournalVoucherAudit.Service
{
    public class ImportOfSalary : Import<Salary, MapOfSalary>
    {
        public ImportOfSalary(string filepath, int header_row_index, int last_row_index)
            : base(filepath, header_row_index, last_row_index)
        {

        }
    }
}
