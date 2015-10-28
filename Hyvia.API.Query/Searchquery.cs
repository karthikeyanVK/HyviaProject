using System.Collections.Generic;

namespace Hyvia.API.Query
{
    public class SearchQuery
    {
        public string SearchField { get; set; }
        public IList<string> SearchValue { get; set; }
    }
}
