using System;
using System.Collections.Generic;
using System.Text;

namespace MovieNight.Core.Models
{

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
                        return "https://i.imgur.com/5qGcAV4.png";
                    }
                    else
                    {
                        return "http://image.tmdb.org/t/p/original/" + profile_path;
                    }
                }
                else
                {
                    if (poster_path == "" || poster_path == null)
                    {
                        return "https://i.imgur.com/5qGcAV4.png";
                    }
                    else
                    {
                        return "http://image.tmdb.org/t/p/original/" + poster_path;
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
                    return title;
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
                            builder += "\tVote average: " + k.vote_average + "\t\tVote count: " + k.vote_count + "\n\n";
                        }
                        else
                        {
                            builder += "\t" + k.title + "\n";
                            builder += "\tVote average: " + k.vote_average + "\t\tVote count: " + k.vote_count + "\n\n";
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
                return media_type;
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
                    return vote_average.ToString();
                }
            }
        }
        public string getVoteCount
        {
            get
            {
                if (media_type.Equals("person"))
                {
                    return "";
                }
                else
                {
                    return vote_count.ToString();
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
                    return "Vote average";
                }
            }
        }
        public string getVoteCountTitle
        {
            get
            {
                if (media_type.Equals("person"))
                {
                    return "";
                }
                else
                {
                    return "Vote count";
                }
            }
        }
    }

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
