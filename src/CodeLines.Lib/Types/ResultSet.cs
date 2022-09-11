using System.Collections.Generic;

namespace CodeLines.Lib.Types
{
    public class ResultSet
    {
        public List<SummaryResult> SummaryResults { get; set; }
        public List<FileResult> FileResults { get; set; }

        public ResultSet()
        {
            SummaryResults = new List<SummaryResult>();
            FileResults = new List<FileResult>();
        }
    }
}
