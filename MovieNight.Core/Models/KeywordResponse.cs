using System.Collections.Generic;

namespace MovieNight.Core.Models
{
    public class KeywordResponse
    {
        public int page { get; set; }
        public List<Result7> results { get; set; } = new List<Result7>();
        public int total_pages { get; set; }
        public int total_results { get; set; }
    }

    public class Result7
    {
        public int id { get; set; }
        public string name { get; set; }
    }
}
