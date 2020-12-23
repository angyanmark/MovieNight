using MovieNight.Core.Models;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace MovieNight.Core.Services
{
    public static class TMDbService
    {
        public static readonly string BACKDROP_SIZE = "w1280"; // w300 w780 w1280 original

        public static readonly string LOGO_SIZE = "w500"; // w45 w92 w154 w185 w300 w500 original

        public static readonly string POSTER_SIZE = "w500"; // w92 w154 w185 w342 w500 w780 original

        public static readonly string PROFILE_SIZE = "h632"; // w45 w185 h632 original

        public static readonly string STILL_SIZE = "original"; // w92 w185 w300 original

        public static readonly string FILE_SIZE = "w500"; // same as poster size

        private static readonly string API_KEY = "5e9bcb638329a15acf75c1b2d85ae67e";

        private static readonly RestClient client = new RestClient("https://api.themoviedb.org/3");

        public static readonly int pages = 6;

        public static async Task<IEnumerable<Film>> GetPopularFilmsAsync(int page)
        {
            RestRequest request = new RestRequest("/movie/popular");
            request.AddParameter("api_key", API_KEY);
            request.AddParameter("page", page);

            var response = await client.GetAsync<FilmsResponse>(request);
            return response.results;
        }

        public static async Task<IEnumerable<Film>> GetNowPlayingFilmsAsync(int page)
        {
            RestRequest request = new RestRequest("/movie/now_playing");
            request.AddParameter("api_key", API_KEY);
            request.AddParameter("region", "US");
            request.AddParameter("page", page);

            var response = await client.GetAsync<FilmsResponse>(request);
            return response.results;
        }

        public static async Task<IEnumerable<Film>> GetUpcomingFilmsAsync(int page)
        {
            RestRequest request = new RestRequest("/movie/upcoming");
            request.AddParameter("api_key", API_KEY);
            request.AddParameter("region", "US");
            request.AddParameter("page", page);

            var response = await client.GetAsync<FilmsResponse>(request);
            return response.results;
        }

        public static async Task<IEnumerable<DiscoverItem>> GetComingSoonAsync(int page, int time)
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
            request.AddParameter("page", page);
            request.AddParameter("primary_release_date.gte", from);
            request.AddParameter("release_date.gte", from);
            if(time >= 0)
            {
                request.AddParameter("primary_release_date.lte", to);
                request.AddParameter("release_date.lte", to);
            }
            request.AddParameter("sort_by", "popularity.desc");

            var response = await client.GetAsync<DiscoverResponse>(request);
            return response.results;
        }

        private static async Task<string> GetKeywordIdsAsync(string keyword)
        {
            RestRequest request = new RestRequest("/search/keyword");
            request.AddParameter("api_key", API_KEY);
            request.AddParameter("query", keyword);

            KeywordResponse response = await client.GetAsync<KeywordResponse>(request);

            var ids = response.results.Select(result => result.id);

            //return string.Join(",", ids.Select(x => x.ToString()).ToArray());

            return ids.FirstOrDefault().ToString();
        }

        public static async Task<IEnumerable<DiscoverItem>> GetDiscoverPageAsync(int page, string keyword, int decade, int year, int genre, int count, string sortby, bool adult)
        {
            string keywordIds = string.Empty;

            if(!string.IsNullOrWhiteSpace(keyword))
            {
                keywordIds = await GetKeywordIdsAsync(keyword);
            }

            RestRequest request = new RestRequest("/discover/movie");
            request.AddParameter("api_key", API_KEY);
            request.AddParameter("region", "US");
            request.AddParameter("page", page);

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

            var response = await client.GetAsync<DiscoverResponse>(request);
            return response.results;
        }

        public static async Task<IEnumerable<DiscoverItem>> GetDiscoverTVPageAsync(int page, string keyword, int decade, int year, int genre, int count, string sortby)
        {
            string keywordIds = string.Empty;

            if (!string.IsNullOrWhiteSpace(keyword))
            {
                keywordIds = await GetKeywordIdsAsync(keyword);
            }

            RestRequest request = new RestRequest("/discover/tv");
            request.AddParameter("api_key", API_KEY);
            request.AddParameter("region", "US");
            request.AddParameter("page", page);

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

            var response = await client.GetAsync<DiscoverResponse>(request);
            return response.results;
        }

        public static async Task<IEnumerable<TVShow>> GetPopularTVShowsAsync(int page)
        {
            RestRequest request = new RestRequest("/tv/popular");
            request.AddParameter("api_key", API_KEY);
            request.AddParameter("page", page);

            var response = await client.GetAsync<TVShowsResponse>(request);
            return response.results;
        }

        public static async Task<IEnumerable<TVShow>> GetTvOnTheAirAsync(int page)
        {
            RestRequest request = new RestRequest("/tv/on_the_air");
            request.AddParameter("api_key", API_KEY);
            request.AddParameter("page", page);

            var response = await client.GetAsync<TVShowsResponse>(request);
            return response.results;
        }

        public static async Task<IEnumerable<TVShow>> GetTvAiringTodayAsync(int page)
        {
            RestRequest request = new RestRequest("/tv/airing_today");
            request.AddParameter("api_key", API_KEY);
            request.AddParameter("page", page);

            var response = await client.GetAsync<TVShowsResponse>(request);
            return response.results;
        }

        public static async Task<IEnumerable<Person>> GetPopularPeopleAsync(int page)
        {
            RestRequest request = new RestRequest("/person/popular");
            request.AddParameter("api_key", API_KEY);
            request.AddParameter("page", page);

            var response = await client.GetAsync<PeopleResponse>(request);
            return response.results;
        }

        public static async Task<Film> GetDetailedFilmAsync(int id)
        {
            RestRequest request = new RestRequest("/movie/{id}");
            request.AddParameter("api_key", API_KEY);
            request.AddParameter("append_to_response", "external_ids,videos,recommendations,release_dates,credits,images,reviews,keywords");
            request.AddUrlSegment("id", id);

            Film film = await client.GetAsync<Film>(request);

            if (film.belongs_to_collection != null)
            {
                var parts = await GetFilmCollectionAsync(film.belongs_to_collection.id);
                film.collection_films = parts.ToList();
            }

            return film;
        }

        private static async Task<IEnumerable<Part>> GetFilmCollectionAsync(int collection_id)
        {
            RestRequest request = new RestRequest("/collection/{collection_id}");
            request.AddParameter("api_key", API_KEY);
            request.AddUrlSegment("collection_id", collection_id);

            CollectionResponse response = await client.GetAsync<CollectionResponse>(request);

            return response.parts?
                .OrderByDescending(p => !string.IsNullOrEmpty(p.release_date))
                .ThenBy(p => p.release_date);
        }

        public static async Task<TVShow> GetDetailedTVShowAsync(int id)
        {
            RestRequest request = new RestRequest("/tv/{id}");
            request.AddParameter("api_key", API_KEY);
            request.AddParameter("append_to_response", "external_ids,videos,recommendations,credits,content_ratings,images,reviews,keywords");
            request.AddUrlSegment("id", id);

            return await client.GetAsync<TVShow>(request);
        }

        public static async Task<TVShowSeason> GetDetailedTVShowSeasonAsync(int tv_id, int season_number, string showName)
        {
            RestRequest request = new RestRequest("/tv/{tv_id}/season/{season_number}");
            request.AddParameter("api_key", API_KEY);
            request.AddParameter("append_to_response", "credits,images");
            request.AddUrlSegment("tv_id", tv_id);
            request.AddUrlSegment("season_number", season_number);

            TVShowSeason season = await client.GetAsync<TVShowSeason>(request);
            season.showName = showName;

            return season;
        }

        public static async Task<Person> GetDetailedPersonAsync(int id)
        {
            RestRequest request = new RestRequest("/person/{id}");
            request.AddParameter("api_key", API_KEY);
            request.AddParameter("append_to_response", "external_ids,combined_credits,tagged_images,images");
            request.AddUrlSegment("id", id);

            Person person = await client.GetAsync<Person>(request);

            person.combined_credits.cast = person.combined_credits.cast.OrderByDescending(o => o.popularity).ToList();
            person.combined_credits.crew = person.combined_credits.crew.OrderByDescending(o => o.popularity).ToList();

            return person;
        }

        public static async Task<IEnumerable<MultiSearchItem>> GetMultiSearchAsync(string searchValue)
        {
            RestRequest request = new RestRequest("/search/multi");
            request.AddParameter("api_key", API_KEY);
            request.AddParameter("include_adult", "true");
            request.AddParameter("query", searchValue);

            var response = await client.GetAsync<MultiSearchResponse>(request);
            return response.results;
        }

        public static async Task<IEnumerable<Genres>> GetGenresAsync(string media)
        {
            RestRequest request = new RestRequest($"/genre/{media}/list");
            request.AddParameter("api_key", API_KEY);

            var response = await client.GetAsync<GenreResponse>(request);
            return response.genres;
        }
    }
}
