using System;

using MovieNight.ViewModels;

using Windows.UI.Xaml.Controls;

namespace MovieNight.Views
{
    public sealed partial class Popular_TV_ShowsPage : Page
    {
        private Popular_TV_ShowsViewModel ViewModel
        {
            get { return ViewModelLocator.Current.Popular_TV_ShowsViewModel; }
        }

        public Popular_TV_ShowsPage()
        {
            InitializeComponent();
        }
    }
}
