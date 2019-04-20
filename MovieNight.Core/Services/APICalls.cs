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

        public static ObservableCollection<Film> CallPopularFilms()
        {
            ObservableCollection<Film> data1 = new ObservableCollection<Film>();

            RestRequest request = new RestRequest("/movie/popular");

            request.AddParameter("api_key", API_KEY);
            FilmsResponse result = client.Execute<FilmsResponse>(request).Data;

            data1 = new ObservableCollection<Film>(result.results);
            int totalPages = result.total_pages;

            for (int i = 2; i <= pages; i++)
            {
                if (i > totalPages)
                    break;
                request.AddParameter("page", i);
                result = client.Execute<FilmsResponse>(request).Data;

                ObservableCollection<Film> data2 = new ObservableCollection<Film>(result.results);

                data1 = new ObservableCollection<Film>(data1.Union(data2).ToList());
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
            data1 = new ObservableCollection<Film>(result.results);
            int totalPages = result.total_pages;

            for (int i = 2; i <= pages; i++)
            {
                if (i > totalPages)
                    break;
                request.AddParameter("page", i);
                result = client.Execute<FilmsResponse>(request).Data;

                ObservableCollection<Film> data2 = new ObservableCollection<Film>(result.results);

                data1 = new ObservableCollection<Film>(data1.Union(data2).ToList());
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
            data1 = new ObservableCollection<Film>(result.results);
            int totalPages = result.total_pages;

            for (int i = 2; i <= pages; i++)
            {
                if (i > totalPages)
                    break;
                request.AddParameter("page", i);
                result = client.Execute<FilmsResponse>(request).Data;

                ObservableCollection<Film> data2 = new ObservableCollection<Film>(result.results);

                data1 = new ObservableCollection<Film>(data1.Union(data2).ToList());
            }

            return data1;
        }

        public static ObservableCollection<DiscoverItem> CallComingSoon()
        {
            ObservableCollection<DiscoverItem> data1 = new ObservableCollection<DiscoverItem>();

            RestRequest request = new RestRequest("/discover/movie");

            DateTime d = DateTime.Now;
            string from = d.AddMonths(1).ToString("u", DateTimeFormatInfo.InvariantInfo).Substring(0, 10);
            //string to = d.AddMonths(6).ToString("u", DateTimeFormatInfo.InvariantInfo).Substring(0, 10);
            string to = d.AddYears(1).ToString("u", DateTimeFormatInfo.InvariantInfo).Substring(0, 10);
            request.AddParameter("api_key", API_KEY);
            request.AddParameter("region", "US");
            request.AddParameter("primary_release_date.gte", from);
            request.AddParameter("primary_release_date.lte", to);
            request.AddParameter("release_date.gte", from);
            request.AddParameter("release_date.lte", to);
            request.AddParameter("sort_by", "popularity.desc");
            DiscoverResponse result = client.Execute<DiscoverResponse>(request).Data;
            data1 = new ObservableCollection<DiscoverItem>(result.results);
            int totalPages = result.total_pages;

            for (int i = 2; i <= pages; i++)
            {
                if (i > totalPages)
                    break;
                request.AddParameter("page", i);
                result = client.Execute<DiscoverResponse>(request).Data;

                ObservableCollection<DiscoverItem> data2 = new ObservableCollection<DiscoverItem>(result.results);

                data1 = new ObservableCollection<DiscoverItem>(data1.Union(data2).ToList());
            }

            return data1;
        }

        public static ObservableCollection<DiscoverItem> CallDiscoverPage(int decade, int year, int genre, string sortby)
        {
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
                request.AddParameter("primary_release_year", year);
            if(genre != 0)
                request.AddParameter("with_genres", genre);

            request.AddParameter("sort_by", sortby);
            DiscoverResponse result = client.Execute<DiscoverResponse>(request).Data;
            
            data1 = new ObservableCollection<DiscoverItem>(result.results);
            int totalPages = result.total_pages;

            for (int i = 2; i <= pages; i++)
            {
                if (i > totalPages)
                    break;
                request.AddParameter("page", i);
                result = client.Execute<DiscoverResponse>(request).Data;

                ObservableCollection<DiscoverItem> data2 = new ObservableCollection<DiscoverItem>(result.results);

                data1 = new ObservableCollection<DiscoverItem>(data1.Union(data2).ToList());
            }

            return data1;
        }

        public static ObservableCollection<TVShow> CallPopularTVShows()
        {
            ObservableCollection<TVShow> data1 = new ObservableCollection<TVShow>();

            RestRequest request = new RestRequest("/tv/popular");

            request.AddParameter("api_key", API_KEY);
            TVShowsResponse result = client.Execute<TVShowsResponse>(request).Data;

            data1 = new ObservableCollection<TVShow>(result.results);
            int totalPages = result.total_pages;

            for (int i = 2; i <= pages; i++)
            {
                if (i > totalPages)
                    break;
                request.AddParameter("page", i);
                result = client.Execute<TVShowsResponse>(request).Data;

                ObservableCollection<TVShow> data2 = new ObservableCollection<TVShow>(result.results);

                data1 = new ObservableCollection<TVShow>(data1.Union(data2).ToList());
            }

            return data1;
        }

        public static ObservableCollection<TVShow> CallTvOnTheAir()
        {
            ObservableCollection<TVShow> data1 = new ObservableCollection<TVShow>();

            RestRequest request = new RestRequest("/tv/on_the_air");

            request.AddParameter("api_key", API_KEY);
            TVShowsResponse result = client.Execute<TVShowsResponse>(request).Data;

            data1 = new ObservableCollection<TVShow>(result.results);
            int totalPages = result.total_pages;

            for (int i = 2; i <= pages; i++)
            {
                if (i > totalPages)
                    break;
                request.AddParameter("page", i);
                result = client.Execute<TVShowsResponse>(request).Data;

                ObservableCollection<TVShow> data2 = new ObservableCollection<TVShow>(result.results);

                data1 = new ObservableCollection<TVShow>(data1.Union(data2).ToList());
            }

            return data1;
        }

        public static ObservableCollection<TVShow> CallTvAiringToday()
        {
            ObservableCollection<TVShow> data1 = new ObservableCollection<TVShow>();

            RestRequest request = new RestRequest("/tv/airing_today");

            request.AddParameter("api_key", API_KEY);
            TVShowsResponse result = client.Execute<TVShowsResponse>(request).Data;

            data1 = new ObservableCollection<TVShow>(result.results);
            int totalPages = result.total_pages;

            for (int i = 2; i <= pages; i++)
            {
                if (i > totalPages)
                    break;
                request.AddParameter("page", i);
                result = client.Execute<TVShowsResponse>(request).Data;

                ObservableCollection<TVShow> data2 = new ObservableCollection<TVShow>(result.results);

                data1 = new ObservableCollection<TVShow>(data1.Union(data2).ToList());
            }

            return data1;
        }

        public static ObservableCollection<Person> CallPopularPeople()
        {
            ObservableCollection<Person> data1 = new ObservableCollection<Person>();

            RestRequest request = new RestRequest("/person/popular");

            request.AddParameter("api_key", API_KEY);
            PeopleResponse result = client.Execute<PeopleResponse>(request).Data;

            data1 = new ObservableCollection<Person>(result.results);
            int totalPages = result.total_pages;

            for (int i = 2; i <= pages; i++)
            {
                if (i > totalPages)
                    break;
                request.AddParameter("page", i);
                result = client.Execute<PeopleResponse>(request).Data;

                ObservableCollection<Person> data2 = new ObservableCollection<Person>(result.results);

                data1 = new ObservableCollection<Person>(data1.Union(data2).ToList());
            }

            return data1;
        }

        public static Film CallDetailedFilm(int id)
        {
            RestRequest request = new RestRequest("/movie/{id}");

            request.AddParameter("api_key", API_KEY);
            request.AddParameter("append_to_response", "external_ids,videos,recommendations,release_dates");
            request.AddUrlSegment("id", id);
            Film data = client.Execute<Film>(request).Data;

            return data;
        }

        public static TVShow CallDetailedTVShow(int id)
        {
            RestRequest request = new RestRequest("/tv/{id}");

            request.AddParameter("api_key", API_KEY);
            request.AddParameter("append_to_response", "external_ids,videos,recommendations,credits,content_ratings");
            request.AddUrlSegment("id", id);
            TVShow data = client.Execute<TVShow>(request).Data;

            return data;
        }

        public static Person CallDetailedPerson(int id)
        {
            RestRequest request = new RestRequest("/person/{id}");

            request.AddParameter("api_key", API_KEY);
            request.AddParameter("include_adult", "true");
            request.AddParameter("append_to_response", "external_ids,combined_credits,tagged_images");
            request.AddUrlSegment("id", id);
            Person data = client.Execute<Person>(request).Data;

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

            data1 = new ObservableCollection<MultiSearchItem>(result.results);
            int totalPages = result.total_pages;

            for (int i = 2; i <= pages; i++)
            {
                if (i > totalPages)
                    break;
                request.AddParameter("page", i);
                result = client.Execute<MultiSearchResponse>(request).Data;

                ObservableCollection<MultiSearchItem> data2 = new ObservableCollection<MultiSearchItem>(result.results);

                data1 = new ObservableCollection<MultiSearchItem>(data1.Union(data2).ToList());
            }

            return data1;
        }

        public static ObservableCollection<Genres> CallGenres()
        {
            ObservableCollection<Genres> data1 = new ObservableCollection<Genres>();

            RestRequest request = new RestRequest("/genre/movie/list");

            request.AddParameter("api_key", API_KEY);
            //request.AddParameter("include_adult", "true");
            //request.AddParameter("include_video", "true");
            GenreResponse result = client.Execute<GenreResponse>(request).Data;

            data1 = new ObservableCollection<Genres>(result.genres);

            return data1;

        }
}
}
