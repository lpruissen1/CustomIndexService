using System.Collections.Generic;

namespace UserCustomIndices.Model.Response
{
    public class CustomIndexResponse
    {
        public string Id { get; set; }
        public List<string> Markets { get; set; }
        public List<string> Sectors { get; set; }
        public List<string> Industries { get; set; }
        public List<Rule> Rules { get; init; } = new List<Rule>();
        public string Test { get; set; }
    }
}
