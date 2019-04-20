using System;
using System.Collections.Generic;
using System.Text;

namespace MovieNight.Core.Models
{

    public class GenreResponse
    {
        //public Genres[] genres { get; set; }
        public List<Genres> genres { get; set; }
    }

    public class Genres
    {
        public int id { get; set; }
        public string name { get; set; }
    }

}
