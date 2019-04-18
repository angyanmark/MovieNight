using MovieNight.Core.Models;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
            //request.AddParameter("region", "US");
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
            //request.AddParameter("region", "US");
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
    }
}
