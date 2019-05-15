using System;

using MovieNight.ViewModels;

using Windows.UI.Xaml.Controls;

namespace MovieNight.Views
{
    public sealed partial class Popular_MoviesPage : Page
    {
        private Popular_MoviesViewModel ViewModel
        {
            get { return ViewModelLocator.Current.Popular_MoviesViewModel; }
        }

        public Popular_MoviesPage()
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
