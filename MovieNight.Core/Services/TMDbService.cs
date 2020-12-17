using MovieNight.Core.Models;
using RestSharp;
using System;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;

namespace MovieNight.Core.Services
{
    public static class TMDbService
    {
        private static readonly string API_KEY = "5e9bcb638329a15acf75c1b2d85ae67e";

        private static readonly RestClient client = new RestClient("https://api.themoviedb.org/3");

        public static readonly int pages = 6;

        public static readonly string BACKDROP_SIZE = "w1280"; // w300 w780 w1280 original

        public static readonly string LOGO_SIZE = "w500"; // w45 w92 w154 w185 w300 w500 original

        public static readonly string POSTER_SIZE = "w500"; // w92 w154 w185 w342 w500 w780 original

        public static readonly string PROFILE_SIZE = "h632"; // w45 w185 h632 original

        public static readonly string STILL_SIZE = "original"; // w92 w185 w300 original

        public static readonly string FILE_SIZE = "w500"; // same as poster size

        public static ObservableCollection<Film> GetPopularFilms(int loadPage)
        {
            RestRequest request = new RestRequest("/movie/popular");
            request.AddParameter("api_key", API_KEY);
            request.AddParameter("page", loadPage);

            FilmsResponse result = client.Execute<FilmsResponse>(request).Data;

            if(result.results != null)
            {
                return new ObservableCollection<Film>(result.results);
            }
            else
            {
                return new ObservableCollection<Film>();
            }
        }

        public static ObservableCollection<Film> GetNowPlayingFilms(int loadPage)
        {
            RestRequest request = new RestRequest("/movie/now_playing");
            request.AddParameter("api_key", API_KEY);
            request.AddParameter("region", "US");
            request.AddParameter("page", loadPage);

            FilmsResponse result = client.Execute<FilmsResponse>(request).Data;

            if(result.results != null)
            {
                return new ObservableCollection<Film>(result.results);
            }
            else
            {
                return new ObservableCollection<Film>();
            }
        }

        public static ObservableCollection<Film> GetUpcomingFilms(int loadPage)
        {
            RestRequest request = new RestRequest("/movie/upcoming");
            request.AddParameter("api_key", API_KEY);
            request.AddParameter("region", "US");
            request.AddParameter("page", loadPage);

            FilmsResponse result = client.Execute<FilmsResponse>(request).Data;

            if(result.results != null)
            {
                return new ObservableCollection<Film>(result.results);
            }
            else
            {
                return new ObservableCollection<Film>();
            }
        }

        public static ObservableCollection<DiscoverItem> GetComingSoon(int loadPage, int time)
        {
            RestRequest request = new RestRequest("/discover/movie");

            DateTime now = DateTime.Now;
            string from = now.AddMonths(1).ToString("u", DateTimeFormatInfo.InvariantInfo).Substring(0, 10);
            string to = "";

            if (time == 0)
            {
                to = now.AddMonths(6).ToString("u", DateTimeFormatInfo.InvariantInfo).Substring(0, 10);
            }
            else if(time > 0)
            {
                to = now.AddYears(time).ToString("u", DateTimeFormatInfo.InvariantInfo).Substring(0, 10);
            }

            request.AddParameter("api_key", API_KEY);
            request.AddParameter("region", "US");
            request.AddParameter("page", loadPage);
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
                return new ObservableCollection<DiscoverItem>(result.results);
            }
            else
            {
                return new ObservableCollection<DiscoverItem>();
            }
        }

        private static string GetKeywordIds(string keyword)
        {
            RestRequest request = new RestRequest("/search/keyword");
            request.AddParameter("api_key", API_KEY);
            request.AddParameter("query", keyword);

            KeywordResponse data = client.Execute<KeywordResponse>(request).Data;

            var ids = data.results.Select(result => result.id);

            //return string.Join(",", ids.Select(x => x.ToString()).ToArray());

            return ids.FirstOrDefault().ToString();
        }

        public static ObservableCollection<DiscoverItem> GetDiscoverPage(int loadPage, string keyword, int decade, int year, int genre, int count, string sortby, bool adult)
        {
            string keywordIds = string.Empty;

            if(!string.IsNullOrWhiteSpace(keyword))
            {
                keywordIds = GetKeywordIds(keyword);
            }

            RestRequest request = new RestRequest("/discover/movie");
            request.AddParameter("api_key", API_KEY);
            request.AddParameter("region", "US");
            request.AddParameter("page", loadPage);

            if (decade != 0)
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

            if(!string.IsNullOrWhiteSpace(keywordIds))
            {
                request.AddParameter("with_keywords", keywordIds);
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
                return new ObservableCollection<DiscoverItem>(result.results);
            }
            else
            {
                return new ObservableCollection<DiscoverItem>();
            }
        }

        public static ObservableCollection<DiscoverItem> GetDiscoverTVPage(int loadPage, string keyword, int decade, int year, int genre, int count, string sortby)
        {
            string keywordIds = string.Empty;

            if (!string.IsNullOrWhiteSpace(keyword))
            {
                keywordIds = GetKeywordIds(keyword);
            }

            RestRequest request = new RestRequest("/discover/tv");
            request.AddParameter("api_key", API_KEY);
            request.AddParameter("region", "US");
            request.AddParameter("page", loadPage);

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

            if (!string.IsNullOrWhiteSpace(keywordIds))
            {
                request.AddParameter("with_keywords", keywordIds);
            }

            if (count > 0)
            {
                request.AddParameter("vote_count.gte", count);
            }

            request.AddParameter("sort_by", sortby);

            DiscoverResponse result = client.Execute<DiscoverResponse>(request).Data;

            if(result.results != null)
            {
                return new ObservableCollection<DiscoverItem>(result.results);
            }
            else
            {
                return new ObservableCollection<DiscoverItem>();
            }
        }

        public static ObservableCollection<TVShow> GetPopularTVShows(int loadPage)
        {
            RestRequest request = new RestRequest("/tv/popular");
            request.AddParameter("api_key", API_KEY);
            request.AddParameter("page", loadPage);

            TVShowsResponse result = client.Execute<TVShowsResponse>(request).Data;

            if(result.results != null)
            {
                return new ObservableCollection<TVShow>(result.results);
            }
            else
            {
                return new ObservableCollection<TVShow>();
            }
        }

        public static ObservableCollection<TVShow> GetTvOnTheAir(int loadPage)
        {
            RestRequest request = new RestRequest("/tv/on_the_air");
            request.AddParameter("api_key", API_KEY);
            request.AddParameter("page", loadPage);

            TVShowsResponse result = client.Execute<TVShowsResponse>(request).Data;

            if(result.results != null)
            {
                return new ObservableCollection<TVShow>(result.results);
            }
            else
            {
                return new ObservableCollection<TVShow>();
            }
        }

        public static ObservableCollection<TVShow> GetTvAiringToday(int loadPage)
        {
            RestRequest request = new RestRequest("/tv/airing_today");
            request.AddParameter("api_key", API_KEY);
            request.AddParameter("page", loadPage);

            TVShowsResponse result = client.Execute<TVShowsResponse>(request).Data;

            if(result.results != null)
            {
                return new ObservableCollection<TVShow>(result.results);
            }
            else
            {
                return new ObservableCollection<TVShow>();
            }
        }

        public static ObservableCollection<Person> GetPopularPeople(int loadPage)
        {
            RestRequest request = new RestRequest("/person/popular");
            request.AddParameter("api_key", API_KEY);
            request.AddParameter("page", loadPage);

            PeopleResponse result = client.Execute<PeopleResponse>(request).Data;

            if(result.results != null)
            {
                return new ObservableCollection<Person>(result.results);
            }
            else
            {
                return new ObservableCollection<Person>();
            }
        }

        public static Film GetDetailedFilm(int id)
        {
            RestRequest request = new RestRequest("/movie/{id}");
            request.AddParameter("api_key", API_KEY);
            request.AddParameter("append_to_response", "external_ids,videos,recommendations,release_dates,credits,images,reviews,keywords");
            request.AddUrlSegment("id", id);

            Film film = client.Execute<Film>(request).Data;

            if(film.belongs_to_collection != null)
            {
                RestRequest request2 = new RestRequest("/collection/{collection_id}");
                request2.AddParameter("api_key", API_KEY);
                request2.AddUrlSegment("collection_id", film.belongs_to_collection.id);
                film.collection_films = client.Execute<CollectionResponse>(request2).Data;

                if(film.collection_films.parts != null)
                {
                    film.collection_films.parts = film.collection_films.parts.OrderBy(o => o.release_date).ToList();

                    int cnt = 0;
                    for (int i = 0; i < film.collection_films.parts.Count; i++)
                    {
                        if (film.collection_films.parts[cnt].release_date == "" || film.collection_films.parts[cnt].release_date == null)
                        {
                            var item = film.collection_films.parts[cnt];
                            film.collection_films.parts.RemoveAt(cnt);
                            film.collection_films.parts.Add(item);
                        }
                        else
                        {
                            cnt++;
                        }
                    }
                }
                else
                {
                    film.belongs_to_collection = null;
                }
            }

            return film;
        }

        public static TVShow GetDetailedTVShow(int id)
        {
            RestRequest request = new RestRequest("/tv/{id}");
            request.AddParameter("api_key", API_KEY);
            request.AddParameter("append_to_response", "external_ids,videos,recommendations,credits,content_ratings,images,reviews,keywords");
            request.AddUrlSegment("id", id);

            return client.Execute<TVShow>(request).Data;
        }

        public static TVShowSeason GetDetailedTVShowSeason(int tv_id, int season_number, string showName)
        {
            RestRequest request = new RestRequest("/tv/{tv_id}/season/{season_number}");
            request.AddParameter("api_key", API_KEY);
            request.AddParameter("append_to_response", "credits,images");
            request.AddUrlSegment("tv_id", tv_id);
            request.AddUrlSegment("season_number", season_number);

            TVShowSeason season = client.Execute<TVShowSeason>(request).Data;
            season.showName = showName;

            return season;
        }

        public static Person GetDetailedPerson(int id)
        {
            RestRequest request = new RestRequest("/person/{id}");
            request.AddParameter("api_key", API_KEY);
            request.AddParameter("append_to_response", "external_ids,combined_credits,tagged_images,images");
            request.AddUrlSegment("id", id);

            Person person = client.Execute<Person>(request).Data;

            person.combined_credits.cast = person.combined_credits.cast.OrderByDescending(o => o.popularity).ToList();
            person.combined_credits.crew = person.combined_credits.crew.OrderByDescending(o => o.popularity).ToList();

            return person;
        }

        public static ObservableCollection<MultiSearchItem> GetMultiSearch(string searchString)
        {
            RestRequest request = new RestRequest("/search/multi");
            request.AddParameter("api_key", API_KEY);
            request.AddParameter("include_adult", "true");
            request.AddParameter("query", searchString);

            MultiSearchResponse result = client.Execute<MultiSearchResponse>(request).Data;

            if(result.results != null)
            {
                return new ObservableCollection<MultiSearchItem>(result.results);
            }
            else
            {
                return new ObservableCollection<MultiSearchItem>();
            }
        }

        public static ObservableCollection<Genres> GetGenres(string media)
        {
            RestRequest request = new RestRequest("/genre/" + media + "/list");
            request.AddParameter("api_key", API_KEY);

            GenreResponse result = client.Execute<GenreResponse>(request).Data;

            if (result.genres != null)
            {
                return new ObservableCollection<Genres>(result.genres);
            }
            else
            {
                return new ObservableCollection<Genres>();
            }
        }
    }
}
