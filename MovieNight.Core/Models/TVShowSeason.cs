using System;
using System.Collections.Generic;
using System.Text;

namespace MovieNight.Core.Models
{

    public class TVShowSeason
    {
        public string _id { get; set; }
        public string air_date { get; set; }
        //public Episode[] episodes { get; set; }
        public List<Episode> episodes { get; set; }
        public string name { get; set; }
        public string overview { get; set; }
        public int id { get; set; }
        public string poster_path { get; set; }
        public int season_number { get; set; }
        public Credits credits { get; set; }
    }

    public class Episode
    {
        public string air_date { get; set; }
        public int episode_number { get; set; }
        public int id { get; set; }
        public string name { get; set; }
        public string overview { get; set; }
        public string production_code { get; set; }
        public int season_number { get; set; }
        public int show_id { get; set; }
        public string still_path { get; set; }
        public float vote_average { get; set; }
        public int vote_count { get; set; }
        //public Crew1[] crew { get; set; }
        public List<Crew1> crew { get; set; }
        //public Guest_Stars[] guest_stars { get; set; }
        public List<Guest_Stars> guest_stars { get; set; }
    }

    public class Crew1
    {
        public int id { get; set; }
        public string credit_id { get; set; }
        public string name { get; set; }
        public string department { get; set; }
        public string job { get; set; }
        public int gender { get; set; }
        public string profile_path { get; set; }
    }

    public class Guest_Stars
    {
        public int id { get; set; }
        public string name { get; set; }
        public string credit_id { get; set; }
        public string character { get; set; }
        public int order { get; set; }
        public int gender { get; set; }
        public string profile_path { get; set; }
    }

}
