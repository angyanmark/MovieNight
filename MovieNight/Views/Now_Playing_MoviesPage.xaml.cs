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

        /*private void OnScrollViewerViewChanged(object sender, ScrollViewerViewChangedEventArgs e)
        {
            var verticalOffset = sv.VerticalOffset;
            var maxVerticalOffset = sv.ScrollableHeight; //sv.ExtentHeight - sv.ViewportHeight;

            if (maxVerticalOffset < 0 ||
                verticalOffset == maxVerticalOffset)
            {
                // Scrolled to bottom
                ViewModel.LoadMovies();
            }
            else
            {
                // Not scrolled to bottom
            }
        }*/
    }
}
