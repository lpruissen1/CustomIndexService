using System.Collections.Generic;

namespace UserCustomIndices.Core.Model.Requests
{
    public class CustomIndexRequest
    {
        public string UserId { get; set; }
        public List<string> Markets { get; set; }
        public List<Rule> Rules { get; init; } = new List<Rule>();
    }
}
