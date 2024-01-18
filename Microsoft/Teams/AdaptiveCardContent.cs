using System.Collections.Generic;
using Newtonsoft.Json;

namespace Microsoft.Teams
{

    public class AdaptiveCardData
    {
        public string Title;

        public string Description;

        public List<Field> Fields;

        public AdaptiveCardData()
        {
            Fields = new List<Field>();
        }

        public string ToJson()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}