using JournalVoucherAudit.Domain;

namespace JournalVoucherAudit.Service
{
    public class ImportOfRetirement:Import<Retirement,MapOfRetirement>
    {
        public ImportOfRetirement(string filepath, int header_row_index, int last_row_index)
            : base(filepath, header_row_index, last_row_index)
        {

        }
    }
}
