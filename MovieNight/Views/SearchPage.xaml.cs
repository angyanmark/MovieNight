using MovieNight.ViewModels;
using Windows.UI.Xaml.Controls;

namespace MovieNight.Views
{
    public sealed partial class SearchPage : Page
    {
        private SearchViewModel ViewModel
        {
            get { return ViewModelLocator.Current.SearchViewModel; }
        }

        public SearchPage()
        {
            InitializeComponent();
        }

        private async void AutoSuggestBox_TextChangedAsync(AutoSuggestBox sender, AutoSuggestBoxTextChangedEventArgs args)
        {
            await ViewModel.LoadSearch(sender.Text);
        }
    }
}
