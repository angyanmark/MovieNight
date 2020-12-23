using MovieNight.ViewModels;
using Windows.UI.Xaml.Controls;

namespace MovieNight.Views
{
    public sealed partial class Upcoming_MoviesPage : Page
    {
        private Upcoming_MoviesViewModel ViewModel
        {
            get { return ViewModelLocator.Current.Upcoming_MoviesViewModel; }
        }

        public Upcoming_MoviesPage()
        {
            InitializeComponent();
        }
    }
}
