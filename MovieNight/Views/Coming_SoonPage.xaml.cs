using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using MovieNight.Core.Models;
using MovieNight.Core.Services;
using MovieNight.ViewModels;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace MovieNight.Views
{
    public class timeContainer
    {
        public int TimeIdx = 1;
    }
    public sealed partial class Coming_SoonPage : Page
    {
        static timeContainer tc = new timeContainer();
        int time = 1;
        private Coming_SoonViewModel ViewModel
        {
            get { return ViewModelLocator.Current.Coming_SoonViewModel; }
        }

        public Coming_SoonPage()
        {
            InitializeComponent();
            timeCombo.SelectedIndex = tc.TimeIdx;
            ViewModel.LoadCompleted += ViewModel_LoadCompleted;
        }

        private void ViewModel_LoadCompleted()
        {
            findButton.IsEnabled = true;
        }

        public void setTime()
        {
            switch (timeCombo.SelectedIndex)
            {
                case 0:
                    time = 0;
                    break;
                case 1:
                    time = 1;
                    break;
                case 2:
                    time = 2;
                    break;
                case 3:
                    time = 3;
                    break;
                case 4:
                    time = 5;
                    break;
                case 5:
                    time = 10;
                    break;
                case 6:
                    time = -1;
                    break;
            }
        }

        private void FindButton_Click(object sender, RoutedEventArgs e)
        {
            findButton.IsEnabled = false;
            tc.TimeIdx = timeCombo.SelectedIndex;
            setTime();
            ViewModel.Source.Clear();
            ViewModel.loadedPages = 0;
            ViewModel.noMore = false;
            ViewModel.LoadMovies(time);
        }
    }
}
