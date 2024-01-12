using System.Collections.Generic;

namespace Microsoft.Teams
{
    public class AdaptiveCardContent
    {
        public string Title;

        public string Description;

        public List<string> Fields;

        public AdaptiveCardContent()
        {
            Fields = new List<string>();
        }
    }
}