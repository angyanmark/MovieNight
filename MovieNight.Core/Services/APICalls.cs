using MovieNight.Core.Models;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Text;

namespace MovieNight.Core.Services
{
    public static class APICalls
    {
        private static readonly string API_KEY = "5e9bcb638329a15acf75c1b2d85ae67e";

        private static RestClient client = new RestClient("https://api.themoviedb.org/3");

        private static readonly int pages = 2;

        public static readonly string BACKDROP_SIZE = "w1280"; // w300 w780 w1280 original

        public static readonly string LOGO_SIZE = "w500";      // w45 w92 w154 w185 w300 w500 original

        public static readonly string POSTER_SIZE = "w500";    // w92 w154 w185 w342 w500 w780 original

        public static readonly string PROFILE_SIZE = "h632";   // w45 w185 h632 original

        public static readonly string STILL_SIZE = "original";     // w92 w185 w300 original

        public static readonly string FILE_SIZE = "original";  // ???

        public static ObservableCollection<Film> CallPopularFilms()
        {
            ObservableCollection<Film> data1 = new ObservableCollection<Film>();

            RestRequest request = new RestRequest("/movie/popular");

            request.AddParameter("api_key", API_KEY);
            RestSharp.IRestResponse<FilmsResponse> v = client.Execute<FilmsResponse>(request);
            FilmsResponse result = v.Data;

            /*object limit;
            object remaining;
            object resetIn;
            foreach(var value in v.Headers)
            {
                if (value.Name.Equals("X-RateLimit-Limit"))
                {
                    limit = value.Value;
                }
                if (value.Name.Equals("X-RateLimit-Remaining"))
                {
                    remaining = value.Value;
                }
                if (value.Name.Equals("X-RateLimit-Reset"))
                {
                    resetIn = value.Value;
                }
            }*/

            if(result.results != null)
            {
                data1 = new ObservableCollection<Film>(result.results);
                int totalPages = result.total_pages;

                for (int i = 2; i <= pages; i++)
                {
                    if (i > totalPages)
                        break;
                    request.AddParameter("page", i);
                    result = client.Execute<FilmsResponse>(request).Data;

                    if(result.results != null)
                    {
                        ObservableCollection<Film> data2 = new ObservableCollection<Film>(result.results);
                        data1 = new ObservableCollection<Film>(data1.Union(data2).ToList());
                    }
                    else
                    {
                        break;
                    }
                }
            }
            

            return data1;
        }

        public static ObservableCollection<Film> CallNowPlayingFilms()
        {
            ObservableCollection<Film> data1 = new ObservableCollection<Film>();

            RestRequest request = new RestRequest("/movie/now_playing");

            request.AddParameter("api_key", API_KEY);
            request.AddParameter("region", "US");
            FilmsResponse result = client.Execute<FilmsResponse>(request).Data;

            if(result.results != null)
            {
                data1 = new ObservableCollection<Film>(result.results);
                int totalPages = result.total_pages;

                for (int i = 2; i <= pages; i++)
                {
                    if (i > totalPages)
                        break;
                    request.AddParameter("page", i);
                    result = client.Execute<FilmsResponse>(request).Data;

                    if(result.results != null)
                    {
                        ObservableCollection<Film> data2 = new ObservableCollection<Film>(result.results);
                        data1 = new ObservableCollection<Film>(data1.Union(data2).ToList());
                    }
                    else
                    {
                        break;
                    }
                }
            }

            return data1;
        }

        public static ObservableCollection<Film> CallUpcomingFilms()
        {
            ObservableCollection<Film> data1 = new ObservableCollection<Film>();

            RestRequest request = new RestRequest("/movie/upcoming");

            request.AddParameter("api_key", API_KEY);
            request.AddParameter("region", "US");
            FilmsResponse result = client.Execute<FilmsResponse>(request).Data;

            if(result.results != null)
            {
                data1 = new ObservableCollection<Film>(result.results);
                int totalPages = result.total_pages;

                for (int i = 2; i <= pages; i++)
                {
                    if (i > totalPages)
                        break;
                    request.AddParameter("page", i);
                    result = client.Execute<FilmsResponse>(request).Data;

                    if(result.results != null)
                    {
                        ObservableCollection<Film> data2 = new ObservableCollection<Film>(result.results);
                        data1 = new ObservableCollection<Film>(data1.Union(data2).ToList());
                    }
                    else
                    {
                        break;
                    }
                }
            }
            
            return data1;
        }

        public static ObservableCollection<DiscoverItem> CallComingSoon(int time)
        {
            ObservableCollection<DiscoverItem> data1 = new ObservableCollection<DiscoverItem>();

            RestRequest request = new RestRequest("/discover/movie");

            DateTime d = DateTime.Now;
            string from = d.AddMonths(1).ToString("u", DateTimeFormatInfo.InvariantInfo).Substring(0, 10);
            string to = "";
            if (time == 0)
            {
                to = d.AddMonths(6).ToString("u", DateTimeFormatInfo.InvariantInfo).Substring(0, 10);
            }
            else if(time > 0)
            {
                to = d.AddYears(time).ToString("u", DateTimeFormatInfo.InvariantInfo).Substring(0, 10);
            }

            request.AddParameter("api_key", API_KEY);
            request.AddParameter("region", "US");
            request.AddParameter("primary_release_date.gte", from);
            request.AddParameter("release_date.gte", from);
            if(time >= 0)
            {
                request.AddParameter("primary_release_date.lte", to);
                request.AddParameter("release_date.lte", to);
            }
            request.AddParameter("sort_by", "popularity.desc");
            DiscoverResponse result = client.Execute<DiscoverResponse>(request).Data;

            if(result.results != null)
            {
                data1 = new ObservableCollection<DiscoverItem>(result.results);
                int totalPages = result.total_pages;

                for (int i = 2; i <= pages; i++)
                {
                    if (i > totalPages)
                        break;
                    request.AddParameter("page", i);
                    result = client.Execute<DiscoverResponse>(request).Data;

                    if(result.results != null)
                    {
                        ObservableCollection<DiscoverItem> data2 = new ObservableCollection<DiscoverItem>(result.results);
                        data1 = new ObservableCollection<DiscoverItem>(data1.Union(data2).ToList());
                    }
                    else
                    {
                        break;
                    }
                }
            }

            return data1;
        }

        public static string CallKeywordIDs(string keyword)
        {
            RestRequest request = new RestRequest("/search/keyword");

            request.AddParameter("api_key", API_KEY);
            request.AddParameter("query", keyword);
            KeywordResponse data = client.Execute<KeywordResponse>(request).Data;

            string builder = "";

            /*foreach(Result7 r in data.results)
            {
                builder += r.id + ",";
            }
            if(builder.Length > 0)
            {
                builder = builder.Substring(0, builder.Length - 1);
            }*/

            if(data.results != null)
            {
                if(data.results.Count > 0)
                {
                    builder += data.results[0].id.ToString();
                }
            }

            return builder;
        }

        public static ObservableCollection<DiscoverItem> CallDiscoverPage(string keyword, int decade, int year, int genre, int count, string sortby, bool adult)
        {
            string keywordIDs = "";

            if(keyword != "")
            {
                keywordIDs = CallKeywordIDs(keyword);
            }

            ObservableCollection<DiscoverItem> data1 = new ObservableCollection<DiscoverItem>();

            RestRequest request = new RestRequest("/discover/movie");

            request.AddParameter("api_key", API_KEY);
            request.AddParameter("region", "US");

            if(decade != 0)
            {
                request.AddParameter("primary_release_date.gte", decade.ToString() + "-01-01");
                request.AddParameter("primary_release_date.lte", (decade + 9).ToString() + "-12-31");
            }

            if(year != 0)
            {
                request.AddParameter("primary_release_year", year);
            }
                
            if(genre != 0)
            {
                request.AddParameter("with_genres", genre);
            }   

            if(keywordIDs != "")
            {
                request.AddParameter("with_keywords", keywordIDs);
            }
            
            if(count > 0)
            {
                request.AddParameter("vote_count.gte", count);
            }

            request.AddParameter("sort_by", sortby);

            if (adult)
            {
                request.AddParameter("include_adult", "true");
            }

            DiscoverResponse result = client.Execute<DiscoverResponse>(request).Data;
            
            if(result.results != null)
            {
                data1 = new ObservableCollection<DiscoverItem>(result.results);
                int totalPages = result.total_pages;

                for (int i = 2; i <= pages; i++)
                {
                    if (i > totalPages)
                        break;
                    request.AddParameter("page", i);
                    result = client.Execute<DiscoverResponse>(request).Data;

                    if(result.results != null)
                    {
                        ObservableCollection<DiscoverItem> data2 = new ObservableCollection<DiscoverItem>(result.results);
                        data1 = new ObservableCollection<DiscoverItem>(data1.Union(data2).ToList());
                    }
                    else
                    {
                        break;
                    }
                }
            }

            return data1;
        }

        public static ObservableCollection<DiscoverItem> CallDiscoverTVPage(string keyword, int decade, int year, int genre, int count, string sortby)
        {
            string keywordIDs = "";

            if (keyword != "")
            {
                keywordIDs = CallKeywordIDs(keyword);
            }

            ObservableCollection<DiscoverItem> data1 = new ObservableCollection<DiscoverItem>();

            RestRequest request = new RestRequest("/discover/tv");

            request.AddParameter("api_key", API_KEY);
            request.AddParameter("region", "US");

            if (decade != 0)
            {
                request.AddParameter("first_air_date.gte", decade.ToString() + "-01-01");
                request.AddParameter("first_air_date.lte", (decade + 9).ToString() + "-12-31");
            }

            if (year != 0)
            {
                request.AddParameter("first_air_date_year", year);
            }
                
            if (genre != 0)
            {
                request.AddParameter("with_genres", genre);
            }   

            if (keywordIDs != "")
            {
                request.AddParameter("with_keywords", keywordIDs);
            }

            if (count > 0)
            {
                request.AddParameter("vote_count.gte", count);
            }

            request.AddParameter("sort_by", sortby);

            DiscoverResponse result = client.Execute<DiscoverResponse>(request).Data;

            if(result.results != null)
            {
                data1 = new ObservableCollection<DiscoverItem>(result.results);
                int totalPages = result.total_pages;

                for (int i = 2; i <= pages; i++)
                {
                    if (i > totalPages)
                        break;
                    request.AddParameter("page", i);
                    result = client.Execute<DiscoverResponse>(request).Data;

                    if(result.results != null)
                    {
                        ObservableCollection<DiscoverItem> data2 = new ObservableCollection<DiscoverItem>(result.results);
                        data1 = new ObservableCollection<DiscoverItem>(data1.Union(data2).ToList());
                    }
                    else
                    {
                        break;
                    }
                }
            }

            return data1;
        }

        public static ObservableCollection<TVShow> CallPopularTVShows()
        {
            ObservableCollection<TVShow> data1 = new ObservableCollection<TVShow>();

            RestRequest request = new RestRequest("/tv/popular");

            request.AddParameter("api_key", API_KEY);
            TVShowsResponse result = client.Execute<TVShowsResponse>(request).Data;

            if(result.results != null)
            {
                data1 = new ObservableCollection<TVShow>(result.results);
                int totalPages = result.total_pages;

                for (int i = 2; i <= pages; i++)
                {
                    if (i > totalPages)
                        break;
                    request.AddParameter("page", i);
                    result = client.Execute<TVShowsResponse>(request).Data;

                    if(result.results != null)
                    {
                        ObservableCollection<TVShow> data2 = new ObservableCollection<TVShow>(result.results);
                        data1 = new ObservableCollection<TVShow>(data1.Union(data2).ToList());
                    }
                    else
                    {
                        break;
                    }
                }
            }

            return data1;
        }

        public static ObservableCollection<TVShow> CallTvOnTheAir()
        {
            ObservableCollection<TVShow> data1 = new ObservableCollection<TVShow>();

            RestRequest request = new RestRequest("/tv/on_the_air");

            request.AddParameter("api_key", API_KEY);
            TVShowsResponse result = client.Execute<TVShowsResponse>(request).Data;

            if(result.results != null)
            {
                data1 = new ObservableCollection<TVShow>(result.results);
                int totalPages = result.total_pages;

                for (int i = 2; i <= pages; i++)
                {
                    if (i > totalPages)
                        break;
                    request.AddParameter("page", i);
                    result = client.Execute<TVShowsResponse>(request).Data;

                    if(result.results != null)
                    {
                        ObservableCollection<TVShow> data2 = new ObservableCollection<TVShow>(result.results);
                        data1 = new ObservableCollection<TVShow>(data1.Union(data2).ToList());
                    }
                    else
                    {
                        break;
                    }
                }
            }

            return data1;
        }

        public static ObservableCollection<TVShow> CallTvAiringToday()
        {
            ObservableCollection<TVShow> data1 = new ObservableCollection<TVShow>();

            RestRequest request = new RestRequest("/tv/airing_today");

            request.AddParameter("api_key", API_KEY);
            TVShowsResponse result = client.Execute<TVShowsResponse>(request).Data;

            if(result.results != null)
            {
                data1 = new ObservableCollection<TVShow>(result.results);
                int totalPages = result.total_pages;

                for (int i = 2; i <= pages; i++)
                {
                    if (i > totalPages)
                        break;
                    request.AddParameter("page", i);
                    result = client.Execute<TVShowsResponse>(request).Data;

                    if(result.results != null)
                    {
                        ObservableCollection<TVShow> data2 = new ObservableCollection<TVShow>(result.results);
                        data1 = new ObservableCollection<TVShow>(data1.Union(data2).ToList());
                    }
                    else
                    {
                        break;
                    }
                }
            }
            

            return data1;
        }

        public static ObservableCollection<Person> CallPopularPeople()
        {
            ObservableCollection<Person> data1 = new ObservableCollection<Person>();

            RestRequest request = new RestRequest("/person/popular");

            request.AddParameter("api_key", API_KEY);
            PeopleResponse result = client.Execute<PeopleResponse>(request).Data;

            if(result.results != null)
            {
                data1 = new ObservableCollection<Person>(result.results);
                int totalPages = result.total_pages;

                for (int i = 2; i <= pages; i++)
                {
                    if (i > totalPages)
                        break;
                    request.AddParameter("page", i);
                    result = client.Execute<PeopleResponse>(request).Data;

                    if(result.results != null)
                    {
                        ObservableCollection<Person> data2 = new ObservableCollection<Person>(result.results);
                        data1 = new ObservableCollection<Person>(data1.Union(data2).ToList());
                    }
                    else
                    {
                        break;
                    }
                }
            }

            return data1;
        }

        public static Film CallDetailedFilm(int id)
        {
            RestRequest request = new RestRequest("/movie/{id}");

            request.AddParameter("api_key", API_KEY);
            request.AddParameter("append_to_response", "external_ids,videos,recommendations,release_dates,credits,images,reviews,keywords");
            request.AddUrlSegment("id", id);
            Film data = client.Execute<Film>(request).Data;

            if(data.belongs_to_collection != null)
            {
                RestRequest request2 = new RestRequest("/collection/{collection_id}");
                request2.AddParameter("api_key", API_KEY);
                request2.AddUrlSegment("collection_id", data.belongs_to_collection.id);
                data.collection_films = client.Execute<CollectionResponse>(request2).Data;

                data.collection_films.parts = data.collection_films.parts.OrderBy(o => o.release_date).ToList();

                int size = data.collection_films.parts.Count;
                int cnt = 0;
                for (int i = 0; i < data.collection_films.parts.Count; i++)
                {
                    if (data.collection_films.parts[cnt].release_date == "" || data.collection_films.parts[cnt].release_date == null)
                    {
                        var item = data.collection_films.parts[cnt];
                        data.collection_films.parts.RemoveAt(cnt);
                        data.collection_films.parts.Add(item);
                    }
                    else
                    {
                        cnt++;
                    }
                }
            }

            return data;
        }

        public static TVShow CallDetailedTVShow(int id)
        {
            RestRequest request = new RestRequest("/tv/{id}");

            request.AddParameter("api_key", API_KEY);
            request.AddParameter("append_to_response", "external_ids,videos,recommendations,credits,content_ratings,images,reviews,keywords");
            request.AddUrlSegment("id", id);
            TVShow data = client.Execute<TVShow>(request).Data;

            return data;
        }

        public static TVShowSeason CallDetailedTVShowSeason(int tv_id, int season_number, string showName)
        {
            RestRequest request = new RestRequest("/tv/{tv_id}/season/{season_number}");

            request.AddParameter("api_key", API_KEY);
            request.AddParameter("append_to_response", "credits,images");
            request.AddUrlSegment("tv_id", tv_id);
            request.AddUrlSegment("season_number", season_number);
            TVShowSeason data = client.Execute<TVShowSeason>(request).Data;

            data.showName = showName;

            return data;
        }

        public static Person CallDetailedPerson(int id)
        {
            RestRequest request = new RestRequest("/person/{id}");

            request.AddParameter("api_key", API_KEY);
            request.AddParameter("append_to_response", "external_ids,combined_credits,tagged_images,images");
            request.AddUrlSegment("id", id);
            Person data = client.Execute<Person>(request).Data;
            string cont = client.Execute<Person>(request).Content;

            //data.combined_credits.cast.OrderBy(o => ((o.vote_count + 400) * (o.popularity + 10) * (o.vote_average + 3))).ToList();
            //data.combined_credits.crew.OrderBy(o => ((o.vote_count + 400) * (o.popularity + 10) * (o.vote_average + 3))).ToList();

            bubbleSortCast(data.combined_credits.cast, data.combined_credits.cast.Count);
            bubbleSortCrew(data.combined_credits.crew, data.combined_credits.crew.Count);

            return data;
        }

        public static ObservableCollection<MultiSearchItem> CallMultiSearch(string searchString)
        {
            ObservableCollection<MultiSearchItem> data1 = new ObservableCollection<MultiSearchItem>();

            RestRequest request = new RestRequest("/search/multi");

            request.AddParameter("api_key", API_KEY);
            request.AddParameter("include_adult", "true");
            request.AddParameter("query", searchString);
            MultiSearchResponse result = client.Execute<MultiSearchResponse>(request).Data;

            if(result.results != null)
            {
                data1 = new ObservableCollection<MultiSearchItem>(result.results);
                int totalPages = result.total_pages;

                for (int i = 2; i <= pages; i++)
                {
                    if (i > totalPages)
                        break;
                    request.AddParameter("page", i);
                    result = client.Execute<MultiSearchResponse>(request).Data;

                    if(result.results != null)
                    {
                        ObservableCollection<MultiSearchItem> data2 = new ObservableCollection<MultiSearchItem>(result.results);
                        data1 = new ObservableCollection<MultiSearchItem>(data1.Union(data2).ToList());
                    }
                    else
                    {
                        break;
                    }
                }
            }

            return data1;
        }

        public static ObservableCollection<Genres> CallGenres(string media)
        {
            ObservableCollection<Genres> data1 = new ObservableCollection<Genres>();

            RestRequest request = new RestRequest("/genre/" + media + "/list");

            request.AddParameter("api_key", API_KEY);
            //request.AddParameter("include_adult", "true");
            //request.AddParameter("include_video", "true");
            GenreResponse result = client.Execute<GenreResponse>(request).Data;

            if(result.genres != null)
            {
                data1 = new ObservableCollection<Genres>(result.genres);
            }

            return data1;

        }

        private static void bubbleSortCast(List<Cast> arr, int n)
        {
            Cast tempc = new Cast();
            for (int i = 0; i < n - 1; i++)
                for (int j = 0; j < n - i - 1; j++)
                    if (((arr[  j  ].vote_count + 400) * (arr[  j  ].popularity + 10) * (arr[  j  ].vote_average + 3)) <
                        ((arr[j + 1].vote_count + 400) * (arr[j + 1].popularity + 10) * (arr[j + 1].vote_average + 3)))
                    {
                        tempc = arr[j];
                        arr[j] = arr[j + 1];
                        arr[j + 1] = tempc;
                    }
        }

        private static void bubbleSortCrew(List<Crew> arr, int n)
        {
            Crew tempc = new Crew();
            for (int i = 0; i < n - 1; i++)
                for (int j = 0; j < n - i - 1; j++)
                    if (((arr[  j  ].vote_count + 400) * (arr[  j  ].popularity + 10) * (arr[  j  ].vote_average + 3)) <
                        ((arr[j + 1].vote_count + 400) * (arr[j + 1].popularity + 10) * (arr[j + 1].vote_average + 3)))
                    {
                        tempc = arr[j];
                        arr[j] = arr[j + 1];
                        arr[j + 1] = tempc;
                    }
        }
    }
}
