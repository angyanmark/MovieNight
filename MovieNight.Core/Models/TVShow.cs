using MovieNight.Core.Services;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace MovieNight.Core.Models
{
    public class TVShowsResponse
    {
        public int page { get; set; }
        public int total_results { get; set; }
        public int total_pages { get; set; }
        public List<TVShow> results { get; set; }
    }
    public class TVShow
    {
        private string Backdrop_path;
        public string backdrop_path
        {
            get
            {
                return "https://image.tmdb.org/t/p/" + APICalls.BACKDROP_SIZE + "/" + Backdrop_path;
            }
            set
            {
                Backdrop_path = value;
            }
        }
        public string getOriginalBackdrop
        {
            get
            {
                return "https://image.tmdb.org/t/p/original/" + Backdrop_path;
            }
        }
        public string isBackDrop
        {
            get
            {
                if (Backdrop_path == "" || Backdrop_path == null)
                {
                    return "Collapsed";
                }
                else
                {
                    return "Visible";
                }
            }
        }
        //public Created_By[] created_by { get; set; }
        public List<Created_By> created_by { private get; set; }
        public string Created_by
        {
            get
            {
                string builder = "";

                foreach (Created_By cb in created_by)
                {
                    builder += cb.name + ", ";
                }

                if (builder.Length > 0)
                {
                    builder = builder.Substring(0, builder.Length - 2);
                }
                else
                {
                    builder = "-";
                }

                return builder;
            }
            private set { }
        }
        //public int[] episode_run_time { get; set; }
        public List<int> episode_run_time { private get; set; }
        public string Episode_run_time
        {
            get
            {
                string builder = "";

                if (episode_run_time.Count > 0)
                {
                    if(episode_run_time[0] != 0)
                    {
                        builder = episode_run_time[0].ToString() + " mins";
                    }
                    else
                    {
                        builder = "-";
                    }
                }
                else
                {
                    builder = "-";
                }

                return builder;
            }
            private set { }
        }
        private string First_air_date;
        public string first_air_date
        {
            get
            {
                if (First_air_date == "" || First_air_date == null)
                {
                    return "-";
                }
                else
                {
                    /*string str = First_air_date.Replace("-", "");
                    DateTime dt = DateTime.ParseExact(str, "yyyyMMdd", CultureInfo.InvariantCulture);
                    return dt.ToString("yyyy. MM. dd.");*/
                    return First_air_date;
                }
            }
            set
            {
                First_air_date = value;
            }
        }
        //public Genre[] genres { get; set; }
        public List<Genre> genres { private get; set; }
        public string Genres
        {
            get
            {
                string builder = "";

                foreach (Genre g in genres)
                {
                    builder += g.name + ", ";
                }

                if (builder.Length > 0)
                {
                    builder = builder.Substring(0, builder.Length - 2);
                }
                else
                {
                    builder = "-";
                }

                return builder;
            }
            private set { }
        }
        public List<int> genre_ids { get; set; }
        public string homepage { get; set; }
        public int id { get; set; }
        public bool in_production { get; set; }
        //public string[] languages { get; set; }
        public List<string> languages { get; set; }
        public string last_air_date { get; set; }
        public Last_Episode_To_Air last_episode_to_air { get; set; }
        public string name { get; set; }
        public Next_Episode_To_Air next_episode_to_air { get; set; }
        //public Network1[] networks { get; set; }
        public List<Network1> networks { private get; set; }
        public string Networks
        {
            get
            {
                string builder = "";

                foreach (Network1 n in networks)
                {
                    builder += n.name + ",\n";
                }

                if (builder.Length > 0)
                {
                    builder = builder.Substring(0, builder.Length - 2);
                }
                else
                {
                    builder = "-";
                }

                return builder;
            }
            private set { }
        }
        public int number_of_episodes { get; set; }
        public int number_of_seasons { get; set; }
        //public string[] origin_country { get; set; }
        public List<string> origin_country { get; set; }
        public string original_language { get; set; }
        public string original_name { get; set; }
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
        public string getOriginalPoster
        {
            get
            {
                if (Poster_path == "" || Poster_path == null)
                {
                    return "https://i.imgur.com/5qGcAV4.png";
                }
                else
                {
                    return "https://image.tmdb.org/t/p/original/" + Poster_path;
                }
            }
        }
        //public Production_Companies[] production_companies { get; set; }
        public List<Production_Companies> production_companies { get; set; }
        //public Season[] seasons { get; set; }
        public List<Season> seasons { get; set; }
        private string Status;
        public string status
        {
            get
            {
                if (Status == "" || Status == null)
                {
                    return "-";
                }
                else
                {
                    return Status;
                }
            }
            set
            {
                Status = value;
            }
        }
        private string Type;
        public string type
        {
            get
            {
                if (Type == "" || Type == null)
                {
                    return "-";
                }
                else
                {
                    return Type;
                }
            }
            set
            {
                Type = value;
            }
        }
        public float vote_average { get; set; }
        public int vote_count { get; set; }
        public External_Ids external_ids { get; set; }
        public Credits credits { get; set; }
        public Recommendations recommendations { get; set; }
        public Videos videos { get; set; }
        public string getImdb_link
        {
            get
            {
                return "https://www.imdb.com/title/" + external_ids.imdb_id + "/";
            }
        }
        public string getTmdb_link
        {
            get
            {
                return "https://www.themoviedb.org/tv/" + id;
            }
        }
        public string getYoutube_link
        {
            get
            {
                if (videos.results.Count > 0)
                {
                    return "https://www.youtube.com/watch?v=" + videos.results[0].key;
                }
                else
                {
                    return "https://www.youtube.com/results?search_query=" + name + " trailer";
                }
            }
        }
        public string getFacebook_link
        {
            get
            {
                return "https://www.facebook.com/" + external_ids.facebook_id + "/";
            }
        }
        public string getInstagram_link
        {
            get
            {
                return "https://www.instagram.com/" + external_ids.instagram_id + "/";
            }
        }
        public string getTwitter_link
        {
            get
            {
                return "https://twitter.com/" + external_ids.twitter_id;
            }
        }
        public string isImdb
        {
            get
            {
                if (external_ids.imdb_id == "" || external_ids.imdb_id == null)
                {
                    return "Collapsed";
                }
                else
                {
                    return "Visible";
                }
            }
        }
        public string isYoutube
        {
            get
            {
                if (videos.results.Count == 0)
                {
                    return "Collapsed";
                }
                else
                {
                    return "Visible";
                }
            }
        }
        public string isFacebook
        {
            get
            {
                if (external_ids.facebook_id == "" || external_ids.facebook_id == null)
                {
                    return "Collapsed";
                }
                else
                {
                    return "Visible";
                }
            }
        }
        public string isInstagram
        {
            get
            {
                if (external_ids.instagram_id == "" || external_ids.instagram_id == null)
                {
                    return "Collapsed";
                }
                else
                {
                    return "Visible";
                }
            }
        }
        public string isTwitter
        {
            get
            {
                if (external_ids.twitter_id == "" || external_ids.twitter_id == null)
                {
                    return "Collapsed";
                }
                else
                {
                    return "Visible";
                }
            }
        }
        public Content_Ratings content_ratings { get; set; }
        public string certification
        {
            get
            {
                foreach(Result6 csr in content_ratings.results)
                {
                    if(csr.iso_3166_1 == "US")
                    {
                        return csr.rating;
                    }
                }
                return "-";
            }
        }
    }

    public class Last_Episode_To_Air
    {
        public string air_date { get; set; }
        public int episode_number { get; set; }
        public int id { get; set; }
        public string name { get; set; }
        public string overview { get; set; }
        public string production_code { get; set; }
        public int season_number { get; set; }
        public int show_id { get; set; }
        private string Still_path;
        public string still_path
        {
            get
            {
                return "https://image.tmdb.org/t/p/" + APICalls.STILL_SIZE + "/" + Still_path;
            }
            set
            {
                Still_path = value;
            }
        }
        public float vote_average { get; set; }
        public int vote_count { get; set; }
    }

    public class Next_Episode_To_Air
    {
        public string air_date { get; set; }
        public int episode_number { get; set; }
        public int id { get; set; }
        public string name { get; set; }
        public string overview { get; set; }
        public string production_code { get; set; }
        public int season_number { get; set; }
        public int show_id { get; set; }
        private string Still_path;
        public string still_path
        {
            get
            {
                return "https://image.tmdb.org/t/p/" + APICalls.STILL_SIZE + "/" + Still_path;
            }
            set
            {
                Still_path = value;
            }
        }
        public int vote_average { get; set; }
        public int vote_count { get; set; }
    }

    public class External_Ids
    {
        public string imdb_id { get; set; }
        public string freebase_mid { get; set; }
        public string freebase_id { get; set; }
        public int tvdb_id { get; set; }
        public int tvrage_id { get; set; }
        public string facebook_id { get; set; }
        public string instagram_id { get; set; }
        public string twitter_id { get; set; }
    }

    public class Credits
    {
        //public Cast[] cast { get; set; }
        public List<Cast> cast { get; set; }
        //public Crew[] crew { get; set; }
        public List<Crew> crew { get; set; }
    }

    public class Network
    {
        public int id { get; set; }
        public Logo logo { get; set; }
        public string name { get; set; }
        public string origin_country { get; set; }
    }

    public class Logo
    {
        private string Path;
        public string path
        {
            get
            {
                return "https://image.tmdb.org/t/p/" + APICalls.LOGO_SIZE + "/" + Path;
            }
            set
            {
                Path = value;
            }
        }
        public float aspect_ratio { get; set; }
    }

    public class Created_By
    {
        public int id { get; set; }
        public string credit_id { get; set; }
        public string name { get; set; }
        public int gender { get; set; }
        private string Profile_path;
        public string profile_path
        {
            get
            {
                if (Profile_path == "" || Profile_path == null)
                {
                    return "/Assets/placeholder_poster.png";
                }
                else
                {
                    return "https://image.tmdb.org/t/p/" + APICalls.PROFILE_SIZE + "/" + Profile_path;
                }
            }
            set
            {
                Profile_path = value;
            }
        }
    }

    public class Network1
    {
        public string name { get; set; }
        public int id { get; set; }
        private string Logo_path;
        public string logo_path
        {
            get
            {
                return "https://image.tmdb.org/t/p/" + APICalls.LOGO_SIZE + "/" + Logo_path;
            }
            set
            {
                Logo_path = value;
            }
        }
        public string origin_country { get; set; }
    }

    public class Season
    {
        public string air_date { get; set; }
        public int episode_count { get; set; }
        public int id { get; set; }
        public string name { get; set; }
        public string overview { get; set; }
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
        public int season_number { get; set; }
    }

    public class Content_Ratings
    {
        //public Result6[] results { get; set; }
        public List<Result6> results { get; set; }
    }

    public class Result6
    {
        public string iso_3166_1 { get; set; }
        public string rating { get; set; }
    }

}
