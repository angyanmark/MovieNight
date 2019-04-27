using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using MovieNight.Core.Models;
using MovieNight.Core.Services;
using MovieNight.ViewModels;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;

namespace MovieNight.Views
{
    public class dataContainerTV
    {
        public int YearIdx = 0;
        public int GenreIdx = 0;
        public int CountIdx = 0;
        public int SortByIdx = 0;
        public string keyword = "";
    }
    public sealed partial class DiscoverTVPage : Page
    {
        static dataContainerTV dc = new dataContainerTV();
        int decade;
        int year;
        int genre;
        int count;
        string sortby;

        private DiscoverTVViewModel ViewModel
        {
            get { return ViewModelLocator.Current.DiscoverTVViewModel; }
        }

        public DiscoverTVPage()
        {
            InitializeComponent();
            fillYears();
            fillGenres();

            yearCombo.SelectedIndex = dc.YearIdx;
            genreCombo.SelectedIndex = dc.GenreIdx;
            minimumVotesCombo.SelectedIndex = dc.CountIdx;
            sortByCombo.SelectedIndex = dc.SortByIdx;
            keywordText.Text = dc.keyword;
        }

        public List<ComboBoxItem> years = new List<ComboBoxItem>();

        public void fillYears()
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
            ComboBoxItem cbi3 = new ComboBoxItem();
            cbi3.Content = "Year";
            years.Add(cbi3);
            years.Reverse();
        }

        public List<string> genres = new List<string>();
        public Dictionary<string, int> genresDictionary = new Dictionary<string, int>();

        public void fillGenres()
        {
            genres.Add("Genre");

            ObservableCollection<Genres> genreList = APICalls.CallGenres("tv");

            foreach (Genres g in genreList)
            {
                genres.Add(g.name);
                genresDictionary.Add(g.name, g.id);
            }
        }

        private void setYear()
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
                decade = Int32.Parse(yearValue.Substring(0, 4));
            }
            else
            {
                decade = 0;
                year = Int32.Parse(yearValue);
            }

            dc.YearIdx = yearCombo.SelectedIndex;
        }

        private void setGenre()
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
                case "500":
                    count = 500;
                    break;
                case "1,000":
                    count = 1000;
                    break;
                case "2,500":
                    count = 2500;
                    break;
                case "4,000":
                    count = 4000;
                    break;
                case "6,000":
                    count = 6000;
                    break;
                case "8,000":
                    count = 8000;
                    break;
                case "10,000":
                    count = 10000;
                    break;
                case "15,000":
                    count = 15000;
                    break;
            }

            dc.CountIdx = minimumVotesCombo.SelectedIndex;
        }

        private void setSortBy()
        {
            var comboBoxItem = sortByCombo.SelectedItem as ComboBoxItem;
            if (comboBoxItem == null) return;
            string sortByValue = comboBoxItem.Content as string;

            switch (sortByValue)
            {
                case "TV Show Popularity":
                    sortby = "popularity.desc";
                    break;
                case "Newest First":
                    sortby = "first_air_date.desc";
                    break;
                case "Earliest First":
                    sortby = "first_air_date.asc";
                    break;
                case "Highest Vote Average First":
                    sortby = "vote_average.desc";
                    break;
                case "Lowest Vote Average First":
                    sortby = "vote_average.asc";
                    break;
            }
            dc.SortByIdx = sortByCombo.SelectedIndex;
        }

        private void FindButton_Click(object sender, RoutedEventArgs e)
        {
            dc.keyword = keywordText.Text;
            setYear();
            setGenre();
            setCount();
            setSortBy();

            ViewModel.Source.Clear();

            string yearValue = (string)(yearCombo.SelectedItem as ComboBoxItem).Content;

            if (yearValue == "Year")
            {
                decade = 0;
                year = 0;
            }
            else if (yearValue.Length == 5)
            {
                year = 0;
                decade = Int32.Parse(yearValue.Substring(0, 4));
            }
            else
            {
                decade = 0;
                year = Int32.Parse(yearValue);
            }

            var s = APICalls.CallDiscoverTVPage(keywordText.Text, decade, year, genre, count, sortby);
            foreach (var v in s)
            {
                ViewModel.Source.Add(v);
            }
        }

        private void ClearButton_Click(object sender, RoutedEventArgs e)
        {
            keywordText.Text = "";
            yearCombo.SelectedIndex = 0;
            genreCombo.SelectedIndex = 0;
            minimumVotesCombo.SelectedIndex = 0;
            sortByCombo.SelectedIndex = 0;
        }
    }
}
