using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading;
using System.Threading.Tasks;
using MovieNight.Core.Models;
using MovieNight.Core.Services;
using MovieNight.ViewModels;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace MovieNight.Views
{
    public class DataContainer
    {
        public int YearIdx = 0;
        public int GenreIdx = 0;
        public int CountIdx = 0;
        public int SortByIdx = 0;
        public string keyword = "";
        public bool adult = false;
    }

    public sealed partial class DiscoverPage : Page
    {
        private static readonly DataContainer dc = new DataContainer();
        private int decade;
        private int year;
        private int genre;
        private int count;
        private string sortby;
        private bool adult;

        private DiscoverViewModel ViewModel
        {
            get { return ViewModelLocator.Current.DiscoverViewModel; }
        }

        public DiscoverPage()
        {
            InitializeComponent();
            FillYears();
            FillGenres();

            yearCombo.SelectedIndex = dc.YearIdx;
            genreCombo.SelectedIndex = dc.GenreIdx;
            minimumVotesCombo.SelectedIndex = dc.CountIdx;
            sortByCombo.SelectedIndex = dc.SortByIdx;
            keywordText.Text = dc.keyword;
            includeAdultCheck.IsChecked = dc.adult;

            ViewModel.LoadCompleted += ViewModel_LoadCompleted;
        }

        private void ViewModel_LoadCompleted()
        {
            findButton.IsEnabled = true;
        }

        public List<ComboBoxItem> years = new List<ComboBoxItem>();

        public void FillYears()
        {
            int until = DateTime.Now.Year + 5;

            while (until % 10 != 0)
            {
                until++;
            }

            for (int i = 1870; i < until; i++)
            {
                ComboBoxItem cbi1 = new ComboBoxItem();
                cbi1.Content = i.ToString();
                cbi1.Margin = new Thickness(24, 0, 0, 0);
                years.Add(cbi1);
                if ((i + 1) % 10 == 0)
                {
                    ComboBoxItem cbi2 = new ComboBoxItem();
                    cbi2.Content = (i - 9).ToString() + "s";
                    years.Add(cbi2);
                }
            }
            ComboBoxItem cbi3 = new ComboBoxItem
            {
                Content = "Year"
            };
            years.Add(cbi3);
            years.Reverse();
        }

        public List<string> genres = new List<string>();
        public Dictionary<string, int> genresDictionary = new Dictionary<string, int>();

        public void FillGenres()
        {
            genres.Add("Genre");

            ObservableCollection<Genres> genreList = APICalls.CallGenres("movie");

            foreach (Genres g in genreList)
            {
                genres.Add(g.name);
                genresDictionary.Add(g.name, g.id);
            }
        }

        private void SetYear()
        {
            string yearValue = (string)(yearCombo.SelectedItem as ComboBoxItem).Content;

            if (yearValue == "Year")
            {
                decade = 0;
                year = 0;
            }
            else if (yearValue.Length == 5)
            {
                year = 0;
                decade = int.Parse(yearValue.Substring(0, 4));
            }
            else
            {
                decade = 0;
                year = int.Parse(yearValue);
            }

            dc.YearIdx = yearCombo.SelectedIndex;
        }

        private void SetGenre()
        {
            string genreValue = genreCombo.SelectedItem.ToString();

            genre = genresDictionary.GetValueOrDefault(genreValue);

            dc.GenreIdx = genreCombo.SelectedIndex;
        }

        private void setCount()
        {
            var comboBoxItem = minimumVotesCombo.SelectedItem as ComboBoxItem;
            if (comboBoxItem == null) return;
            string countValue = comboBoxItem.Content as string;

            switch (countValue)
            {
                case "Votes":
                    count = -1;
                    break;
                case "100":
                    count = 100;
                    break;
                case "500":
                    count = 500;
                    break;
                case "1,000":
                    count = 1000;
                    break;
                case "3,000":
                    count = 3000;
                    break;
                case "5,000":
                    count = 5000;
                    break;
                case "10,000":
                    count = 10000;
                    break;
                case "15,000":
                    count = 15000;
                    break;
                case "20,000":
                    count = 20000;
                    break;
                case "35,000":
                    count = 35000;
                    break;
                case "50,000":
                    count = 50000;
                    break;
            }

            dc.CountIdx = minimumVotesCombo.SelectedIndex;
        }

        private void setSortBy()
        {
            var comboBoxItem = sortByCombo.SelectedItem as ComboBoxItem;
            if (comboBoxItem == null)
            {
                return;
            }
            
            string sortByValue = comboBoxItem.Content as string;

            switch (sortByValue)
            {
                case "Film Popularity":
                    sortby = "popularity.desc";
                    break;
                case "Film Name":
                    sortby = "original_title.asc";
                    break;
                case "Newest First":
                    sortby = "primary_release_date.desc";
                    break;
                case "Earliest First":
                    sortby = "primary_release_date.asc";
                    break;
                case "Highest Vote Average First":
                    sortby = "vote_average.desc";
                    break;
                case "Lowest Vote Average First":
                    sortby = "vote_average.asc";
                    break;
                case "Highest Vote Count First":
                    sortby = "vote_count.desc";
                    break;
                case "Lowest Vote Count First":
                    sortby = "vote_count.asc";
                    break;
                case "Highest Revenue First":
                    sortby = "revenue.desc";
                    break;
                case "Lowest Revenue First":
                    sortby = "revenue.asc";
                    break;
            }
            dc.SortByIdx = sortByCombo.SelectedIndex;
        }

        private void FindButton_Click(object sender, RoutedEventArgs e)
        {
            findButton.IsEnabled = false;
            dc.keyword = keywordText.Text;
            SetYear();
            SetGenre();
            setCount();
            setSortBy();
            adult = (bool)includeAdultCheck.IsChecked;
            dc.adult = adult;

            ViewModel.Source.Clear();
            ViewModel.loadedPages = 0;
            ViewModel.noMore = false;

            string yearValue = (string)(yearCombo.SelectedItem as ComboBoxItem).Content;

            if (yearValue == "Year")
            {
                decade = 0;
                year = 0;
            }
            else if (yearValue.Length == 5)
            {
                year = 0;
                decade = int.Parse(yearValue.Substring(0, 4));
            }
            else
            {
                decade = 0;
                year = int.Parse(yearValue);
            }

            _ = ViewModel.LoadMovies(keywordText.Text, decade, year, genre, count, sortby, adult);
        }

        private void ClearButton_Click(object sender, RoutedEventArgs e)
        {
            keywordText.Text = "";
            yearCombo.SelectedIndex = 0;
            genreCombo.SelectedIndex = 0;
            minimumVotesCombo.SelectedIndex = 0;
            sortByCombo.SelectedIndex = 0;
            includeAdultCheck.IsChecked = false;
        }
    }
}
