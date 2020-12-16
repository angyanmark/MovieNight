using MovieNight.Core.Services;
using System;
using System.Collections.Generic;

namespace MovieNight.Core.Models
{
    public class PeopleResponse
    {
        public int page { get; set; }
        public int total_results { get; set; }
        public int total_pages { get; set; }
        public List<Person> results { get; set; }
        public string status_message { get; set; }
        public int status_code { get; set; }
    }

    public class Person
    {
        public Person()
        {

        }
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
                    /*string str = Birthday.Replace("-", "");
                    DateTime dt = DateTime.ParseExact(str, "yyyyMMdd", CultureInfo.InvariantCulture);
                    return dt.ToString("yyyy. MM. dd.");*/
                    return Birthday;
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
                    /*string str = Deathday.Replace("-", "");
                    DateTime dt = DateTime.ParseExact(str, "yyyyMMdd", CultureInfo.InvariantCulture);
                    return dt.ToString("yyyy. MM. dd.");*/
                    return Deathday;
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
                if (deathday == "")
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
        public string Also_known_as
        {
            get
            {
                string builder = "";
                int cnt = 0;
                foreach (string s in also_known_as)
                {
                    if (cnt++ > 7)
                    {
                        break;
                    }
                    else
                    {
                        builder += s + ",\n";
                    }
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
        }
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
        public string getOriginalProfile
        {
            get
            {
                if (Profile_path == "" || Profile_path == null)
                {
                    return getTmdb_link;
                }
                else
                {
                    return "https://image.tmdb.org/t/p/original/" + Profile_path;
                }
            }
        }
        public bool adult { get; set; }
        public string isAdult
        {
            get
            {
                if (adult)
                {
                    return "Visible";
                }
                else
                {
                    return "Collapsed";
                }
            }
        }
        public string imdb_id { get; set; }
        public string getHomepage
        {
            get
            {
                if (homepage == "")
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
                if (Homepage != "" && Homepage != null)
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
                if (homepage == "")
                {
                    return getTmdb_link;
                }
                else
                {
                    Uri uriResult;
                    bool result = Uri.TryCreate(homepage, UriKind.Absolute, out uriResult) && (uriResult.Scheme == Uri.UriSchemeHttp || uriResult.Scheme == Uri.UriSchemeHttps);
                    if (result)
                    {
                        return homepage;
                    }
                    else
                    {
                        return getTmdb_link;
                    }
                }
            }
        }
        public Tagged_Images tagged_images { get; set; }
        public string isImages
        {
            get
            {
                if (tagged_images.results.Count > 0)
                {
                    return "Visible";
                }
                else
                {
                    return "Collapsed";
                }
            }
        }
        //public Profile[] profiles { get; set; }
        public Images images { get; set; }
        public string isProfiles
        {
            get
            {
                if(images.profiles.Count > 0)
                {
                    return "Visible";
                }
                else
                {
                    return "Collapsed";
                }
            }
        }
        public Dictionary<string, string> getTagged
        {
            get
            {
                Random r = new Random();
                int rand;
                if (tagged_images.results.Count > 0)
                {
                    for (int i = 0; i < 5; i++)
                    {
                        rand = r.Next(tagged_images.results.Count);
                        if (tagged_images.results[rand].File_path != "" && tagged_images.results[rand].File_path != null)
                        {
                            string path = tagged_images.results[rand].file_path;
                            Dictionary<string, string> dic = new Dictionary<string, string>();
                            if (tagged_images.results[rand].media_type == "movie")
                            {
                                if (tagged_images.results[rand].media.release_date != "" && tagged_images.results[rand].media.release_date != null)
                                {
                                    dic.Add(path, tagged_images.results[rand].media.title + " (" + tagged_images.results[rand].media.release_date.Substring(0, 4) + ")");
                                }
                                else
                                {
                                    dic.Add(path, tagged_images.results[rand].media.title);
                                }
                            }
                            else
                            {
                                if (tagged_images.results[rand].media.first_air_date != "" && tagged_images.results[rand].media.first_air_date != null)
                                {
                                    dic.Add(path, tagged_images.results[rand].media.name + " (" + tagged_images.results[rand].media.first_air_date.Substring(0, 4) + ")");
                                }
                                else
                                {
                                    dic.Add(path, tagged_images.results[rand].media.name);
                                }
                            }
                            return dic;
                        }
                    }
                }
                if (combined_credits.cast.Count > 0 || combined_credits.crew.Count > 0)
                {
                    for (int i = 0; i < 5; i++)
                    {
                        if (combined_credits.cast.Count >= combined_credits.crew.Count)
                        {
                            rand = r.Next(combined_credits.cast.Count);
                            if (combined_credits.cast[rand].Backdrop_path != "" && combined_credits.cast[rand].Backdrop_path != null)
                            {
                                string path = combined_credits.cast[rand].backdrop_path;
                                Dictionary<string, string> dic = new Dictionary<string, string>();
                                if (combined_credits.cast[rand].media_type == "movie")
                                {
                                    if (combined_credits.cast[rand].release_date != "" && combined_credits.cast[rand].release_date != null)
                                    {
                                        dic.Add(path, combined_credits.cast[rand].title + " (" + combined_credits.cast[rand].release_date.Substring(0, 4) + ")");
                                    }
                                    else
                                    {
                                        dic.Add(path, combined_credits.cast[rand].title);
                                    }
                                }
                                else
                                {
                                    if (combined_credits.cast[rand].first_air_date != "" && combined_credits.cast[rand].first_air_date != null)
                                    {
                                        dic.Add(path, combined_credits.cast[rand].name + " (" + combined_credits.cast[rand].first_air_date.Substring(0, 4) + ")");
                                    }
                                    else
                                    {
                                        dic.Add(path, combined_credits.cast[rand].name);
                                    }
                                }
                                return dic;
                            }
                        }
                        else
                        {
                            rand = r.Next(combined_credits.crew.Count);
                            if (combined_credits.crew[rand].Backdrop_path != "" && combined_credits.crew[rand].Backdrop_path != null)
                            {
                                string path = combined_credits.crew[rand].backdrop_path;
                                Dictionary<string, string> dic = new Dictionary<string, string>();
                                if (combined_credits.crew[rand].media_type == "movie")
                                {
                                    if (combined_credits.crew[rand].release_date != "" && combined_credits.crew[rand].release_date != null)
                                    {
                                        dic.Add(path, combined_credits.crew[rand].title + " (" + combined_credits.crew[rand].release_date.Substring(0, 4) + ")");
                                    }
                                    else
                                    {
                                        dic.Add(path, combined_credits.crew[rand].title);
                                    }
                                }
                                else
                                {
                                    if (combined_credits.crew[rand].first_air_date != "" && combined_credits.crew[rand].first_air_date != null)
                                    {
                                        dic.Add(path, combined_credits.crew[rand].name + " (" + combined_credits.crew[rand].first_air_date.Substring(0, 4) + ")");
                                    }
                                    else
                                    {
                                        dic.Add(path, combined_credits.crew[rand].name);
                                    }
                                }
                                return dic;
                            }
                        }
                    }
                }
                if (tagged_images.results.Count > 0)
                {
                    for (int i = 0; i < tagged_images.results.Count; i++)
                    {
                        if (tagged_images.results[i].File_path != "" && tagged_images.results[i].File_path != null)
                        {
                            string path = tagged_images.results[i].file_path;
                            Dictionary<string, string> dic = new Dictionary<string, string>();
                            if (tagged_images.results[i].media_type == "movie")
                            {
                                if (tagged_images.results[i].media.release_date != "" && tagged_images.results[i].media.release_date != null)
                                {
                                    dic.Add(path, tagged_images.results[i].media.title + " (" + tagged_images.results[i].media.release_date.Substring(0, 4) + ")");
                                }
                                else
                                {
                                    dic.Add(path, tagged_images.results[i].media.title);
                                }
                            }
                            else
                            {
                                if (tagged_images.results[i].media.first_air_date != "" && tagged_images.results[i].media.first_air_date != null)
                                {
                                    dic.Add(path, tagged_images.results[i].media.name + " (" + tagged_images.results[i].media.first_air_date.Substring(0, 4) + ")");
                                }
                                else
                                {
                                    dic.Add(path, tagged_images.results[i].media.name);
                                }
                            }
                            return dic;
                        }
                    }
                }
                if (combined_credits.cast.Count > 0)
                {
                    for (int i = 0; i < combined_credits.cast.Count; i++)
                    {
                        if (combined_credits.cast[i].Backdrop_path != "" && combined_credits.cast[i].Backdrop_path != null)
                        {
                            string path = combined_credits.cast[i].backdrop_path;
                            Dictionary<string, string> dic = new Dictionary<string, string>();
                            if (combined_credits.cast[i].media_type == "movie")
                            {
                                if (combined_credits.cast[i].release_date != "" && combined_credits.cast[i].release_date != null)
                                {
                                    dic.Add(path, combined_credits.cast[i].title + " (" + combined_credits.cast[i].release_date.Substring(0, 4) + ")");
                                }
                                else
                                {
                                    dic.Add(path, combined_credits.cast[i].title);
                                }
                            }
                            else
                            {
                                if (combined_credits.cast[i].first_air_date != "" && combined_credits.cast[i].first_air_date != null)
                                {
                                    dic.Add(path, combined_credits.cast[i].name + " (" + combined_credits.cast[i].first_air_date.Substring(0, 4) + ")");
                                }
                                else
                                {
                                    dic.Add(path, combined_credits.cast[i].name);
                                }
                            }
                            return dic;
                        }
                    }
                }
                if (combined_credits.crew.Count > 0)
                {
                    for (int i = 0; i < combined_credits.crew.Count; i++)
                    {
                        if (combined_credits.crew[i].Backdrop_path != "" && combined_credits.crew[i].Backdrop_path != null)
                        {
                            string path = combined_credits.crew[i].backdrop_path;
                            Dictionary<string, string> dic = new Dictionary<string, string>();
                            if (combined_credits.crew[i].media_type == "movie")
                            {
                                if (combined_credits.crew[i].release_date != "" && combined_credits.crew[i].release_date != null)
                                {
                                    dic.Add(path, combined_credits.crew[i].title + " (" + combined_credits.crew[i].release_date.Substring(0, 4) + ")");
                                }
                                else
                                {
                                    dic.Add(path, combined_credits.crew[i].title);
                                }
                            }
                            else
                            {
                                if (combined_credits.crew[i].first_air_date != "" && combined_credits.crew[i].first_air_date != null)
                                {
                                    dic.Add(path, combined_credits.crew[i].name + " (" + combined_credits.crew[i].first_air_date.Substring(0, 4) + ")");
                                }
                                else
                                {
                                    dic.Add(path, combined_credits.crew[i].name);
                                }
                            }
                            return dic;
                        }
                    }
                }
                Dictionary<string, string> dic2 = new Dictionary<string, string>();
                dic2.Add("https://image.tmdb.org/t/p/original/", "-");
                return dic2;
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
        public string getLetterboxd_link
        {
            get
            {
                return "https://letterboxd.com/search/actors/" + name + "/";
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
        public string combined_date
        {
            get
            {
                if(release_date != "" && release_date != null)
                {
                    return release_date;
                }
                else
                {
                    return first_air_date;
                }
            }
        }
        public string release_date { get; set; }
        public float vote_average { get; set; }
        public float popularity { get; set; }
        public string title { get; set; }
        public string character { get; set; }
        //public int?[] genre_ids { get; set; }
        public List<int> genre_ids { get; set; }
        public bool adult { get; set; }
        public string Backdrop_path;
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
        public int episode_count { get; set; }
        //public string[] origin_country { get; set; }
        public List<string> origin_country { get; set; }
        public string original_name { get; set; }
        public string name { get; set; }
        public string getName
        {
            get
            {
                if (name != "" && name != null)
                {
                    return name;
                }
                else
                {
                    return "";
                }
            }
        }
        public string first_air_date { get; set; }

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
        public int order { get; set; }
        public string getTitleDate
        {
            get
            {
                if (media_type == "movie")
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
                else
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
        }
        public string getCharacter
        {
            get
            {
                if (character != "" && character != null)
                {
                    return character;
                }
                else
                {
                    return "";
                }
            }
        }
        public string getNameCharacter
        {
            get
            {
                string builder = "";
                if (name != "" && name != null)
                {
                    builder += name;
                }

                if (character != "" && character != null)
                {
                    builder += " - " + character;
                }
                return builder;
            }
        }
        public string getTitleDateCharacter
        {
            get
            {
                string builder = getTitleDate;
                if (character != "" && character != null)
                {
                    builder += " - " + character;
                }
                return builder;
            }
        }
        public string cast_id { get; set; }

    }

    public class Crew
    {
        public int id { get; set; }
        public string department { get; set; }
        public string original_language { get; set; }
        public string original_title { get; set; }
        public string job { get; set; }
        public string getJob
        {
            get
            {
                if (job != "" && job != null)
                {
                    return job;
                }
                else
                {
                    return "";
                }
            }
        }
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
        public float popularity { get; set; }
        public string Backdrop_path;
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
        public float vote_average { get; set; }
        public int vote_count { get; set; }
        public bool adult { get; set; }
        public string title { get; set; }
        public string getTitleDate
        {
            get
            {
                if (media_type == "movie")
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
                else
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
        }
        public string release_date { get; set; }
        public string combined_date
        {
            get
            {
                if (release_date != "" && release_date != null)
                {
                    return release_date;
                }
                else
                {
                    return first_air_date;
                }
            }
        }
        public int episode_count { get; set; }
        //public string[] origin_country { get; set; }
        public List<string> origin_country { get; set; }
        public string original_name { get; set; }
        public string name { get; set; }
        public string getName
        {
            get
            {
                if (name != "" && name != null)
                {
                    return name;
                }
                else
                {
                    return "";
                }
            }
        }
        public string first_air_date { get; set; }
        
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
        public string getNameJob
        {
            get
            {
                string builder = "";
                if (name != "" && name != null)
                {
                    builder += name;
                }
                if (job != "" && job != null)
                {
                    builder += " - " + job;
                }
                return builder;
            }
        }
        public string titleDateJob
        {
            get
            {
                if (media_type == "movie")
                {
                    string returntitle = "";
                    if (release_date != "" && release_date != null)
                    {
                        returntitle = title + " (" + release_date.Substring(0, 4) + ")";
                    }
                    else
                    {
                        returntitle = title;
                    }

                    if (job != "" && job != null)
                    {
                        returntitle = returntitle + " - " + job;
                    }

                    return returntitle;
                }
                else
                {
                    string returntitle = "";
                    if (first_air_date != "" && first_air_date != null)
                    {
                        returntitle = name + " (" + first_air_date.Substring(0, 4) + ")";
                    }
                    else
                    {
                        returntitle = name;
                    }

                    if (job != "" && job != null)
                    {
                        returntitle = returntitle + " - " + job;
                    }

                    return returntitle;
                }
            }
        }
        public string cast_id { get; set; }
    }

    public class Tagged_Images
    {
        //public Result3[] results { get; set; }
        public List<TaggedImagesResult> results { get; set; }
        public int page { get; set; }
        public int total_results { get; set; }
        public int id { get; set; }
        public int total_pages { get; set; }
    }

    public class TaggedImagesResult
    {
        public object iso_639_1 { get; set; }
        public int vote_count { get; set; }
        public string media_type { get; set; }
        public string File_path;
        public string file_path
        {
            get
            {
                return "https://image.tmdb.org/t/p/" + APICalls.FILE_SIZE + "/" + File_path;
            }
            set
            {
                File_path = value;
            }
        }
        public string getTitleShortDate
        {
            get
            {
                if(media_type == "movie")
                {
                    if (media.release_date != "" && media.release_date != null)
                    {
                        return media.title + " (" + media.release_date.Substring(0, 4) + ")";
                    }
                    else
                    {
                        return media.title;
                    }
                }
                else
                {
                    if (media.first_air_date != "" && media.first_air_date != null)
                    {
                        return media.name + " (" + media.first_air_date.Substring(0, 4) + ")";
                    }
                    else
                    {
                        return media.name;
                    }
                }
            }
        }
        public string getOriginalFilePath
        {
            get
            {
                if (File_path == "" || File_path == null)
                {
                    return "https://www.themoviedb.org/";
                }
                else
                {
                    return "https://image.tmdb.org/t/p/original/" + File_path;
                }
            }
        }
        public float aspect_ratio { get; set; }
        public Media media { get; set; }
        public int height { get; set; }
        public float vote_average { get; set; }
        public int width { get; set; }
    }

    public class Media
    {
        public string Poster_path;
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
        public int id { get; set; }
        public bool video { get; set; }
        public int vote_count { get; set; }
        public bool adult { get; set; }
        public string Backdrop_path;
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
        //public int[] genre_ids { get; set; }
        public List<int> genre_ids { get; set; }
        public string original_language { get; set; }
        public string original_title { get; set; }
        public float popularity { get; set; }
        public string title { get; set; }
        public string name { get; set; }
        public float vote_average { get; set; }
        public string overview { get; set; }
        public string release_date { get; set; }
        public string first_air_date { get; set; }
    }

    public class Images
    {
        public List<Profile> profiles { get; set; }
    }

    public class Profile
    {
        public float aspect_ratio { get; set; }
        public string File_path;
        public string file_path
        {
            get
            {
                return "https://image.tmdb.org/t/p/" + APICalls.FILE_SIZE + "/" + File_path;
            }
            set
            {
                File_path = value;
            }
        }
        public string getOriginalFilePath
        {
            get
            {
                if (File_path == "" || File_path == null)
                {
                    return "https://www.themoviedb.org/";
                }
                else
                {
                    return "https://image.tmdb.org/t/p/original/" + File_path;
                }
            }
        }
        public int height { get; set; }
        public object iso_639_1 { get; set; }
        public float vote_average { get; set; }
        public int vote_count { get; set; }
        public int width { get; set; }
    }
}
