using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace UserCustomIndices.Core.Model
{
    public class RangedRule
    {
        [JsonConverter(typeof(StringEnumConverter))]  // JSON.Net
        public RuleType RuleType;
        public double Upper;
        public double Lower;
    }
}
