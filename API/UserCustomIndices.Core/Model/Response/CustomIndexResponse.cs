using System.Collections.Generic;

namespace UserCustomIndices.Model.Response
{
    public class CustomIndexResponse
    {
        public string Id { get; set; }
        public List<string> Markets { get; set; }
        public List<string> Sectors { get; set; }
        public List<string> Industries { get; set; }
        public List<Fule> Fules { get; init; } = new List<Fule>();
        public string Test { get; set; }
    }
}
