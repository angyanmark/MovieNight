using System;

using MovieNight.ViewModels;

using Windows.UI.Xaml.Controls;

namespace MovieNight.Views
{
    public sealed partial class TV_on_the_AirPage : Page
    {
        private TV_on_the_AirViewModel ViewModel
        {
            get { return ViewModelLocator.Current.TV_on_the_AirViewModel; }
        }

        public TV_on_the_AirPage()
        {
            InitializeComponent();
        }
    }
}
