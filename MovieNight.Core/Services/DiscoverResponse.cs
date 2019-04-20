using MovieNight.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace MovieNight.Core.Services
{

    public class DiscoverResponse
    {
        public int page { get; set; }
        public int total_results { get; set; }
        public int total_pages { get; set; }
        //public Result[] results { get; set; }
        public List<DiscoverItem> results { get; set; }
    }
}
