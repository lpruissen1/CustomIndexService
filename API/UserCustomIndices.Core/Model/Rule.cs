using System.Collections.Generic;

namespace UserCustomIndices.Core.Model
{
    public class Rule
    {
        RuleType RuleType;
    }

    public class Sector : Rule
    {
        List<string> Sectors { get; set; }
        List<string> Industries { get; set; }
    }
}
