using System.Collections.ObjectModel;
using System.Threading.Tasks;
using MovieNight.Core.Models;
using MovieNight.Core.Services;
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

        private void AutoSuggestBox_QuerySubmitted(AutoSuggestBox sender, AutoSuggestBoxQuerySubmittedEventArgs args)
        {
            /*if (args.ChosenSuggestion != null)
            {
                // User selected an item from the suggestion list, take an action on it here.
            }
            else
            {
                if (args.QueryText != "")
                {
                    // Use args.QueryText to determine what to do.
                    LoadSearch(args.QueryText);
                }
            }*/
        }

        async Task LoadSearch(string text)
        {
            ViewModel.Source.Clear();
            ObservableCollection<MultiSearchItem> items = await Task.Run(() => APICalls.CallMultiSearch(text));
            foreach (var v in items)
            {
                ViewModel.Source.Add(v);
            }
        }

        private void AutoSuggestBox_TextChanged(AutoSuggestBox sender, AutoSuggestBoxTextChangedEventArgs args)
        {
            // Only get results when it was a user typing, 
            // otherwise assume the value got filled in by TextMemberPath 
            // or the handler for SuggestionChosen.
            if (args.Reason == AutoSuggestionBoxTextChangeReason.UserInput)
            {
                //Set the ItemsSource to be your filtered dataset
                //sender.ItemsSource = dataset;
                /*if(sender.Text == "")
                {
                    ViewModel.Source.Clear();
                }*/
                _ = LoadSearch(sender.Text);
            }
        }
    }
}
