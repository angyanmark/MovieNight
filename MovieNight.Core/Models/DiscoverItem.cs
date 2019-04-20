using MovieNight.Core.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace MovieNight.Core.Models
{
    public class DiscoverResponse
    {
        public int page { get; set; }
        public int total_results { get; set; }
        public int total_pages { get; set; }
        //public Result[] results { get; set; }
        public List<DiscoverItem> results { get; set; }
    }
    public class DiscoverItem
    {
        public int vote_count { get; set; }
        public int id { get; set; }
        public bool video { get; set; }
        public float vote_average { get; set; }
        public string title { get; set; }
        public float popularity { get; set; }
        private string Poster_path;
        public string poster_path
        {
            get
            {
                if (Poster_path == "" || Poster_path == null)
                {
                    return "https://i.imgur.com/5qGcAV4.png";
                }
                else
                {
                    return "https://image.tmdb.org/t/p/" + APICalls.POSTER_SIZE + "/" + Poster_path;
                }
            }
            set
            {
                Poster_path = value;
            }
        }
        public string original_language { get; set; }
        public string original_title { get; set; }
        //public int[] genre_ids { get; set; }
        public List<int> genre_ids { get; set; }
        public string backdrop_path { get; set; }
        public bool adult { get; set; }
        public string overview { get; set; }
        public string release_date { get; set; }
    }

}
