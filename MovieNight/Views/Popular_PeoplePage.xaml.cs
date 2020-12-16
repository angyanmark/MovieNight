using MovieNight.ViewModels;
using Windows.UI.Xaml.Controls;

namespace MovieNight.Views
{
    public sealed partial class Popular_PeoplePage : Page
    {
        private Popular_PeopleViewModel ViewModel
        {
            get { return ViewModelLocator.Current.Popular_PeopleViewModel; }
        }

        public Popular_PeoplePage()
        {
            InitializeComponent();
        }
    }
}
