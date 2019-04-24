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
        public string status_message { get; set; }
        public int status_code { get; set; }
    }
    public class DiscoverItem
    {
        public int vote_count { get; set; }
        public int id { get; set; }
        public bool video { get; set; }
        public float vote_average { get; set; }
        public string title { get; set; }
        public string titleDate
        {
            get
            {
                if(release_date != "" && release_date != null)
                {
                    return title + " (" + release_date + ")";
                }
                else
                {
                    return title;
                }
            }
        }
        public string titleShortDateMovie
        {
            get
            {
                if (release_date != "" && release_date != null)
                {
                    return title + " (" + release_date.Substring(0, 4) + ")";
                }
                else
                {
                    return title;
                }
            }
        }
        public string titleShortDateTV
        {
            get
            {
                if (first_air_date != "" && first_air_date != null)
                {
                    return name + " (" + first_air_date.Substring(0, 4) + ")";
                }
                else
                {
                    return name;
                }
            }
        }
        public float popularity { get; set; }
        private string Poster_path;
        public string poster_path
        {
            get
            {
                if (Poster_path == "" || Poster_path == null)
                {
                    return "/Assets/placeholder_poster.png";
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
        public string first_air_date { get; set; }
        public List<string> origin_country { get; set; }
        public string name { get; set; }
        public string original_name { get; set; }
    }

}
