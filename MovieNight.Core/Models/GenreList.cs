using System;
using System.Collections.Generic;
using System.Text;

namespace MovieNight.Core.Models
{
    public class GenreResponse
    {
        /// <summary>
        /// Class containing response data for Genres.
        /// </summary>
        //public Genres[] genres { get; set; }
        public List<Genres> genres { get; set; }
        public string status_message { get; set; }
        public int status_code { get; set; }
    }

    /// <summary>
    /// Class containing Genres results.
    /// </summary>
    public class Genres
    {
        public int id { get; set; }
        public string name { get; set; }
    }
}
