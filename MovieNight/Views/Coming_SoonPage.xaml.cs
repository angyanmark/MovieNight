using System;

using MovieNight.ViewModels;

using Windows.UI.Xaml.Controls;

namespace MovieNight.Views
{
    public sealed partial class Coming_SoonPage : Page
    {
        private Coming_SoonViewModel ViewModel
        {
            get { return ViewModelLocator.Current.Coming_SoonViewModel; }
        }

        public Coming_SoonPage()
        {
            InitializeComponent();
        }
    }
}
