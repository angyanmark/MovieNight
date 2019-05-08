using MovieNight.Core.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace MovieNight.Core.Models
{
    /// <summary>
    /// Class containing response data for MultiSearchItem.
    /// </summary>
    public class MultiSearchResponse
    {
        public int page { get; set; }
        public int total_results { get; set; }
        public int total_pages { get; set; }
        public List<MultiSearchItem> results { get; set; }
        public string status_message { get; set; }
        public int status_code { get; set; }
    }

    /// <summary>
    /// Class containing MultiSearchItem data.
    /// </summary>
    public class MultiSearchItem
    {
        public float vote_average { get; set; }
        public int vote_count { get; set; }
        public int id { get; set; }
        public bool video { get; set; }
        public string media_type { get; set; }
        public string title { get; set; }
        public float popularity { get; set; }
        public string poster_path { get; set; }
        public string original_language { get; set; }
        public string original_title { get; set; }
        //public int?[] genre_ids { get; set; }
        public List<int> genre_ids { get; set; }
        public string backdrop_path { get; set; }
        public bool adult { get; set; }
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
        public string release_date { get; set; }
        public string original_name { get; set; }
        public string name { get; set; }
        public string first_air_date { get; set; }
        //public string[] origin_country { get; set; }
        public List<string> origin_country { get; set; }
        public string profile_path { get; set; }
        //public Known_For[] known_for { get; set; }
        public List<Known_For> known_for { get; set; }
        public string getPoster
        {
            get
            {
                if (media_type.Equals("person"))
                {
                    if (profile_path == "" || profile_path == null)
                    {
                        return "/Assets/placeholder_poster.png";
                    }
                    else
                    {
                        return "https://image.tmdb.org/t/p/" + APICalls.PROFILE_SIZE + "/" + profile_path;
                    }
                }
                else
                {
                    if (poster_path == "" || poster_path == null)
                    {
                        return "/Assets/placeholder_poster.png";
                    }
                    else
                    {
                        return "https://image.tmdb.org/t/p/" + APICalls.POSTER_SIZE + "/" + poster_path;
                    }
                }
            }
        }
        public string getTitle
        {
            get
            {
                if (media_type.Equals("movie"))
                {
                    if(release_date != "" && release_date != null)
                    {
                        return title + " (" + release_date.Substring(0, 4) + ")";
                    }
                    else
                    {
                        return title;
                    }
                }
                else if (media_type.Equals("tv"))
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
                else
                {
                    return name;
                }
            }
        }
        public string getOverviewTitle
        {
            get
            {
                if (media_type.Equals("person"))
                {
                    return "Known for";
                }
                else
                {
                    return "Overview";
                }
            }
        }
        public string getOverview
        {
            get
            {
                if (media_type.Equals("person"))
                {

                    string builder = "";

                    foreach (Known_For k in known_for)
                    {
                        if(k.release_date != "" && k.release_date != null)
                        {
                            builder += "\t" + k.title + " (" + k.release_date.Substring(0, 4) + ")\n";
                            if(k.vote_count > 1)
                            {
                                builder += "\tRating:   ★ " + k.vote_average + " (" + k.vote_count + " votes)\n\n";
                            }
                            else
                            {
                                builder += "\tRating:   ★ " + k.vote_average + " (" + k.vote_count + " vote)\n\n";
                            }
                        }
                        else
                        {
                            builder += "\t" + k.title + "\n";
                            if (k.vote_count > 1)
                            {
                                builder += "\tRating:   ★ " + k.vote_average + " (" + k.vote_count + " votes)\n\n";
                            }
                            else
                            {
                                builder += "\tRating:   ★ " + k.vote_average + " (" + k.vote_count + " vote)\n\n";
                            }
                        }
                    }

                    if (builder.Length == 0)
                    {
                        builder = "-";
                    }  

                    return builder;
                }
                else
                {
                    return overview;
                }
            }
        }
        public string getMediaType
        {
            get
            {
                if(media_type == "movie")
                {
                    return "film";
                }
                else
                {
                    return media_type;
                }
            }
        }
        public string getAdult
        {
            get
            {
                if (adult)
                {
                    return "adult";
                }
                else
                {
                    return "";
                }
            }
        }
        public string getVoteAverage
        {
            get
            {
                if (media_type.Equals("person"))
                {
                    return "";
                }
                else
                {
                    if(vote_count > 1)
                    {
                        return "★ " + vote_average + " (" + vote_count + " votes)";
                    }
                    else
                    {
                        return "★ " + vote_average + " (" + vote_count + " vote)";
                    }
                }
            }
        }
        public string getVoteAverageTitle
        {
            get
            {
                if (media_type.Equals("person"))
                {
                    return "";
                }
                else
                {
                    return "Rating";
                }
            }
        }
        public int getMaxLines
        {
            get
            {
                if (media_type.Equals("person"))
                {
                    return 8;
                }
                else
                {
                    return 5;
                }
            }

        }
    }

    /// <summary>
    /// Class containing Know For items for a MultiSearch Person.
    /// </summary>
    public class Known_For
    {
        public float vote_average { get; set; }
        public int vote_count { get; set; }
        public int id { get; set; }
        public bool video { get; set; }
        public string media_type { get; set; }
        public string title { get; set; }
        public float popularity { get; set; }
        public string poster_path { get; set; }
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
