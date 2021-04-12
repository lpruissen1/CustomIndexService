using System.Collections.Generic;
using UserCustomIndices.Database.Model.User.CustomIndices;

namespace Database.Model.User.CustomIndices
{
    public class DebtToEquityRatios : Rule 
    {
        public List<Range> TimedRanges;
    }
}