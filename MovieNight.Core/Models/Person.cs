using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace MovieNight.Core.Models
{

    public class Person
    {
        private string Birthday;
        public string birthday
        {
            get
            {
                if (Birthday == "" || Birthday == null)
                {
                    return "-";
                }
                else
                {
                    string str = Birthday.Replace("-", "");
                    DateTime dt = DateTime.ParseExact(str, "yyyyMMdd", CultureInfo.InvariantCulture);
                    return dt.ToString("yyyy. MM. dd.");
                }
            }
            set
            {
                Birthday = value;
            }
        }
        public string known_for_department { get; set; }
        private string Deathday;
        public string deathday
        {
            get
            {
                if (Deathday == "" || Deathday == null)
                {
                    return "";
                }
                else
                {
                    string str = Deathday.Replace("-", "");
                    DateTime dt = DateTime.ParseExact(str, "yyyyMMdd", CultureInfo.InvariantCulture);
                    return dt.ToString("yyyy. MM. dd.");
                }
            }
            set
            {
                Deathday = value;
            }
        }
        public string getDeathdayTitle
        {
            get
            {
                if(deathday == "")
                {
                    return "";
                }
                else
                {
                    return "Deathday";
                }
            }
        }
        public int id { get; set; }
        public External_Ids external_ids { get; set; }
        public string name { get; set; }
        public Combined_Credits combined_credits { get; set; }
        //public string[] also_known_as { get; set; }
        public List<string> also_known_as { get; set; }
        public int gender { private get; set; }
        public string Gender
        {
            get
            {
                switch (gender)
                {
                    case 0: return "Not specified";
                    case 1: return "Female";
                    case 2: return "Male";
                    default: return "-";
                }
            }
        }
        private string Biography;
        public string biography
        {
            get
            {
                if (Biography == "" || Biography == null)
                {
                    return "-";
                }
                else
                {
                    return Biography;
                }
            }
            set
            {
                Biography = value;
            }
        }
        public float popularity { get; set; }
        private string Place_of_birth;
        public string place_of_birth
        {
            get
            {
                if (Place_of_birth == "" || Place_of_birth == null)
                {
                    return "-";
                }
                else
                {
                    return Place_of_birth;
                }
            }
            set
            {
                Place_of_birth = value;
            }
        }
        private string Profile_path;
        public string profile_path
        {
            get
            {
                return "http://image.tmdb.org/t/p/original/" + Profile_path;
            }
            set
            {
                Profile_path = value;
            }
        }
        public bool adult { get; set; }
        public string imdb_id { get; set; }
        public string getHomepage
        {
            get
            {
                if(homepage == "")
                {
                    return "";
                }
                else
                {
                    return "Homepage";
                }
            }
        }
        private string Homepage;
        public string homepage
        {
            get
            {
                if(Homepage != "" && Homepage != null)
                {
                    return Homepage;
                }
                else
                {
                    return "";
                }
            }
            set
            {
                Homepage = value;
            }
        }
        public string getHomepageUri
        {
            get
            {
                if(homepage == "")
                {
                    return "https://www.themoviedb.org/";
                }
                else
                {
                    return homepage;
                }
            }
        }
        public Tagged_Images tagged_images { private get; set; }
        public string getTagged
        {
            get
            {
                Random r = new Random();
                foreach (Result3 res in tagged_images.results)
                {
                    if (res.media.backdrop_path != "" && res.media.backdrop_path != null)
                    {
                        if (r.Next(5) == 0)
                            return res.media.backdrop_path;
                    }
                }
                foreach (Result3 res in tagged_images.results)
                {
                    if (res.media.backdrop_path != "" && res.media.backdrop_path != null)
                    {
                        if(r.Next(3) == 0)
                            return res.media.backdrop_path;
                    }
                }
                foreach (Result3 res in tagged_images.results)
                {
                    if (res.media.backdrop_path != "" && res.media.backdrop_path != null)
                    {
                        if (r.Next(2) == 0)
                            return res.media.backdrop_path;
                    }
                }
                if(combined_credits.cast.Count > 0)
                {
                    if (combined_credits.cast[0].backdrop_path != "" && combined_credits.cast[0].backdrop_path != null)
                    {
                        return combined_credits.cast[0].backdrop_path;
                    }
                }
                if (combined_credits.crew.Count > 0)
                {
                    if (combined_credits.crew[0].backdrop_path != "" && combined_credits.crew[0].backdrop_path != null)
                    {
                        return combined_credits.crew[0].backdrop_path;
                    }
                }
                if(tagged_images.results.Count > 0)
                {
                    if(tagged_images.results[0].media.backdrop_path != "" && tagged_images.results[0].media.backdrop_path != null)
                    {
                        return tagged_images.results[0].media.backdrop_path;
                    }
                    if (tagged_images.results[0].media.poster_path != "" && tagged_images.results[0].media.poster_path != null)
                    {
                        return tagged_images.results[0].media.poster_path;
                    }
                }

                return Profile_path;
            }
            private set { }
        }
        public string getImdb_link
        {
            get
            {
                return "https://www.imdb.com/name/" + external_ids.imdb_id + "/";
            }
        }
        public string getTmdb_link
        {
            get
            {
                return "https://www.themoviedb.org/person/" + id;
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
    }

    public class Combined_Credits
    {
        //public Cast[] cast { get; set; }
        public List<Cast> cast { get; set; }
        //public Crew[] crew { get; set; }
        public List<Crew> crew { get; set; }
    }

    public class Cast
    {
        public int id { get; set; }
        public string original_language { get; set; }
        public string original_title { get; set; }
        public string overview { get; set; }
        public int vote_count { get; set; }
        public bool video { get; set; }
        public string media_type { get; set; }
        public string credit_id { get; set; }
        public string release_date { get; set; }
        public float vote_average { get; set; }
        public float popularity { get; set; }
        public string title { get; set; }
        public string character { get; set; }
        //public int?[] genre_ids { get; set; }
        public List<int> genre_ids { get; set; }
        public bool adult { get; set; }
        private string Backdrop_path;
        public string backdrop_path
        {
            get
            {
                return "http://image.tmdb.org/t/p/original/" + Backdrop_path;
            }
            set
            {
                Backdrop_path = value;
            }
        }
        private string Poster_path;
        public string poster_path
        {
            get
            {
                return "http://image.tmdb.org/t/p/original/" + Poster_path;
            }
            set
            {
                Poster_path = value;
            }
        }
        public int episode_count { get; set; }
        //public string[] origin_country { get; set; }
        public List<string> origin_country { get; set; }
        public string original_name { get; set; }
        public string name { get; set; }
        public string first_air_date { get; set; }
        //--------
        public int gender { get; set; }
        private string Profile_path;
        public string profile_path
        {
            get
            {
                return "http://image.tmdb.org/t/p/original/" + Profile_path;
            }
            set
            {
                Profile_path = value;
            }
        }
        public int order { get; set; }
    }

    public class Crew
    {
        public int id { get; set; }
        public string department { get; set; }
        public string original_language { get; set; }
        public string original_title { get; set; }
        public string job { get; set; }
        public string overview { get; set; }
        //public int?[] genre_ids { get; set; }
        public List<int> genre_ids { get; set; }
        public bool video { get; set; }
        public string media_type { get; set; }
        public string credit_id { get; set; }
        private string Poster_path;
        public string poster_path
        {
            get
            {
                return "http://image.tmdb.org/t/p/original/" + Poster_path;
            }
            set
            {
                Poster_path = value;
            }
        }
        public float popularity { get; set; }
        private string Backdrop_path;
        public string backdrop_path
        {
            get
            {
                return "http://image.tmdb.org/t/p/original/" + Backdrop_path;
            }
            set
            {
                Backdrop_path = value;
            }
        }
        public float vote_average { get; set; }
        public int vote_count { get; set; }
        public bool adult { get; set; }
        public string title { get; set; }
        public string release_date { get; set; }
        public int episode_count { get; set; }
        //public string[] origin_country { get; set; }
        public List<string> origin_country { get; set; }
        public string original_name { get; set; }
        public string name { get; set; }
        public string first_air_date { get; set; }
        //--------
        public int gender { get; set; }
        private string Profile_path;
        public string profile_path
        {
            get
            {
                return "http://image.tmdb.org/t/p/original/" + Profile_path;
            }
            set
            {
                Profile_path = value;
            }
        }
    }

    public class Tagged_Images
    {
        //public Result3[] results { get; set; }
        public List<Result3> results { get; set; }
        public int page { get; set; }
        public int total_results { get; set; }
        public int id { get; set; }
        public int total_pages { get; set; }
    }

    public class Result3
    {
        public object iso_639_1 { get; set; }
        public int vote_count { get; set; }
        public string media_type { get; set; }
        public string file_path { get; set; }
        public float aspect_ratio { get; set; }
        public Media media { get; set; }
        public int height { get; set; }
        public float vote_average { get; set; }
        public int width { get; set; }
    }

    public class Media
    {
        private string Poster_path;
        public string poster_path
        {
            get
            {
                return "http://image.tmdb.org/t/p/original/" + Poster_path;
            }
            set
            {
                Poster_path = value;
            }
        }
        public int id { get; set; }
        public bool video { get; set; }
        public int vote_count { get; set; }
        public bool adult { get; set; }
        private string Backdrop_path;
        public string backdrop_path
        {
            get
            {
                return "http://image.tmdb.org/t/p/original/" + Backdrop_path;
            }
            set
            {
                Backdrop_path = value;
            }
        }
        //public int[] genre_ids { get; set; }
        public List<int> genre_ids { get; set; }
        public string original_language { get; set; }
        public string original_title { get; set; }
        public float popularity { get; set; }
        public string title { get; set; }
        public float vote_average { get; set; }
        public string overview { get; set; }
        public string release_date { get; set; }
    }

}
