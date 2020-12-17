using MovieNight.Core.Services;
using System.Collections.Generic;

namespace MovieNight.Core.Models
{
    public class TVShowSeason
    {
        public string _id { get; set; }
        public string showName { get; set; }
        private string Air_date;
        public string air_date
        {
            get
            {
                if (Air_date == "" || Air_date == null)
                {
                    return "-";
                }
                else
                {
                    /*string str = First_air_date.Replace("-", "");
                    DateTime dt = DateTime.ParseExact(str, "yyyyMMdd", CultureInfo.InvariantCulture);
                    return dt.ToString("yyyy. MM. dd.");*/
                    return Air_date;
                }
            }
            set
            {
                Air_date = value;
            }
        }
        //public Episode[] episodes { get; set; }
        public List<Episode> episodes { get; set; }
        public int getEpisodesCount
        {
            get
            {
                return episodes.Count;
            }
        }
        public string name { get; set; }
        private string Overview;
        public string overview
        {
            get
            {
                if (Overview == "" || Overview == null)
                {
                    return "-";
                }
                else
                {
                    return Overview;
                }
            }
            set
            {
                Overview = value;
            }
        }
        public int id { get; set; }
        public FilmImages images { get; set; }
        public string isPosters
        {
            get
            {
                if (images.posters.Count > 0)
                {
                    return "Visible";
                }
                else
                {
                    return "Collapsed";
                }
            }
        }
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
                    return "https://image.tmdb.org/t/p/" + TMDbService.POSTER_SIZE + "/" + Poster_path;
                }
            }
            set
            {
                Poster_path = value;
            }
        }
        public string getOriginalPoster
        {
            get
            {
                if (Poster_path == "" || Poster_path == null)
                {
                    return "https://www.themoviedb.org/";
                }
                else
                {
                    return "https://image.tmdb.org/t/p/original/" + Poster_path;
                }
            }
        }
        public int season_number { get; set; }
        public string getSeasonNumber
        {
            get
            {
                string sNum = "Season " + season_number;

                if(sNum != name)
                {
                    return sNum;
                }
                else
                {
                    return "";
                }
            }
        }
        public Credits credits { get; set; }
    }

    public class Episode
    {
        public string air_date { get; set; }
        public string getAirDate
        {
            get
            {
                if (air_date != "" && air_date != null)
                {
                    return air_date;
                }
                else
                {
                    return "-";
                }
            }
        }
        public int episode_number { get; set; }
        public string getEpisode_number
        {
            get
            {
                return "Episode " + episode_number;
            }
        }
        public int id { get; set; }
        public string name { get; set; }
        public string getName
        {
            get
            {
                if(name != "" && name != null)
                {
                    return name;
                }
                else
                {
                    return "";
                }
            }
        }
        public string overview { get; set; }
        public string getOverview
        {
            get
            {
                if (overview != "" && overview != null)
                {
                    return overview;
                }
                else
                {
                    return "-";
                }
            }
        }
        public string production_code { get; set; }
        public int season_number { get; set; }
        public int show_id { get; set; }
        private string Still_path;
        public string still_path
        {
            get
            {
                if (Still_path == "" || Still_path == null)
                {
                    return "/Assets/placeholder_poster.png";
                }
                else
                {
                    return "https://image.tmdb.org/t/p/" + TMDbService.STILL_SIZE + "/" + Still_path;
                }
            }
            set
            {
                Still_path = value;
            }
        }
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
