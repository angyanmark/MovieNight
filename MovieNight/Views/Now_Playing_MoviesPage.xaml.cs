using System;

using MovieNight.ViewModels;

using Windows.UI.Xaml.Controls;

namespace MovieNight.Views
{
    public sealed partial class Now_Playing_MoviesPage : Page
    {
        private Now_Playing_MoviesViewModel ViewModel
        {
            get { return ViewModelLocator.Current.Now_Playing_MoviesViewModel; }
        }

        public Now_Playing_MoviesPage()
        {
            InitializeComponent();
        }
    }
}
