using System;
using System.Collections.Generic;
using System.Text;

namespace CodeLines.Lib.Types
{
    public class ResultSet
    {
        public List<SummaryResult> SummaryResults { get; set; }

        public ResultSet()
        {
            SummaryResults = new List<SummaryResult>();
        }
    }
}
