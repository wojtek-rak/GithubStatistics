using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GithubStatistics.Models
{
    public class SearchResultRoot
    {
        public int total_count { get; set; }
        public bool incomplete_results { get; set; }
        public List<SearchResult> items { get; set; }

    }
}
