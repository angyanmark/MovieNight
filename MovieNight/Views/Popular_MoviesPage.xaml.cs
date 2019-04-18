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
    }
}
