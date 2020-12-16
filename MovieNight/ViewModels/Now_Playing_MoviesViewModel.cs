using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using Microsoft.Toolkit.Uwp.UI.Animations;
using MovieNight.Core.Models;
using MovieNight.Core.Services;
using MovieNight.Services;

namespace MovieNight.ViewModels
{
    public class Now_Playing_MoviesViewModel : ViewModelBase
    {
        private int loadedPages = 0;
        private bool noMore = false;

        public NavigationServiceEx NavigationService => ViewModelLocator.Current.NavigationService;

        private ICommand _itemClickCommand;

        public ICommand ItemClickCommand => _itemClickCommand ?? (_itemClickCommand = new RelayCommand<Film>(OnItemClick));

        public ObservableCollection<Film> Source { get; set; } = new ObservableCollection<Film>();

        public async Task LoadMovies()
        {
            if (!noMore)
            {
                ObservableCollection<Film> films = new ObservableCollection<Film>();

                for (int i = 0; i < APICalls.pages; i++)
                {
                    films = await Task.Run(() => APICalls.CallNowPlayingFilms(++loadedPages));
                    if (films.Count == 0)
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

        public Now_Playing_MoviesViewModel()
        {
            _ = LoadMovies();
        }

        private void OnItemClick(Film clickedItem)
        {
            if (clickedItem != null)
            {
                NavigationService.Frame.SetListDataItemForNextConnectedAnimation(clickedItem);
                NavigationService.Navigate(typeof(MoviesDetailViewModel).FullName, clickedItem.id);
            }
        }
    }
}
