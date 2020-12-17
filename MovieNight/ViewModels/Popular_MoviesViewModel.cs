using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using Microsoft.Toolkit.Uwp.UI.Animations;
using MovieNight.Core.Models;
using MovieNight.Core.Services;
using MovieNight.Helpers;
using MovieNight.Services;

namespace MovieNight.ViewModels
{
    public class Popular_MoviesViewModel : ViewModelBase
    {
        private int loadedPages = 0;
        private bool noMore = false;

        public NavigationServiceEx NavigationService => ViewModelLocator.Current.NavigationService;

        private ICommand _itemClickCommand;

        public ICommand ItemClickCommand => _itemClickCommand ?? (_itemClickCommand = new RelayCommand<DiscoverItem>(OnItemClick));

        public ObservableCollection<DiscoverItem> Source { get; set; } = new ObservableCollection<DiscoverItem>();

        public async Task LoadMovies()
        {
            if (!noMore)
            {
                for (int i = 0; i < TMDbService.pages; i++)
                {
                    var films = await TMDbService.GetDiscoverPageAsync(++loadedPages, "", 0, 0, 0, 0, "popularity.desc", false);
                    if (!films.IsAny())
                    {
                        noMore = true;
                        break;
                    }
                    foreach (var v in films)
                    {
                        Source.Add(v);
                    }
                }
            }
        }

        public Popular_MoviesViewModel()
        {
            _ = LoadMovies();
        }

        private void OnItemClick(DiscoverItem clickedItem)
        {
            if (clickedItem != null)
            {
                NavigationService.Frame.SetListDataItemForNextConnectedAnimation(clickedItem);
                NavigationService.Navigate(typeof(MoviesDetailViewModel).FullName, clickedItem.id);
            }
        }
    }
}
