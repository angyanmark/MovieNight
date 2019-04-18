using System;

using MovieNight.ViewModels;

using Windows.UI.Xaml.Controls;

namespace MovieNight.Views
{
    public sealed partial class TV_Airing_TodayPage : Page
    {
        private TV_Airing_TodayViewModel ViewModel
        {
            get { return ViewModelLocator.Current.TV_Airing_TodayViewModel; }
        }

        public TV_Airing_TodayPage()
        {
            InitializeComponent();
        }
    }
}
