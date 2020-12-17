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
    public class DiscoverViewModel : ViewModelBase
    {
        public int loadedPages = 0;
        public bool noMore = false;

        public delegate void loadCompleted();
        public event loadCompleted LoadCompleted;

        public NavigationServiceEx NavigationService => ViewModelLocator.Current.NavigationService;

        private ICommand _itemClickCommand;

        public ICommand ItemClickCommand => _itemClickCommand ?? (_itemClickCommand = new RelayCommand<DiscoverItem>(OnItemClick));

        public ObservableCollection<DiscoverItem> Source { get; set; } = new ObservableCollection<DiscoverItem>();

        public async Task LoadMovies(string keyword, int decade, int year, int genre, int count, string sortby, bool adult)
        {
            if (!noMore)
            {
                ObservableCollection<DiscoverItem> films = new ObservableCollection<DiscoverItem>();

                for (int i = 0; i < TMDbService.pages; i++)
                {
                    films = await Task.Run(() => TMDbService.GetDiscoverPage(++loadedPages, keyword, decade, year, genre, count, sortby, adult));
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
            LoadCompleted();
        }

        public DiscoverViewModel()
        {
            _ = LoadMovies("", 0, 0, 0, 0, "popularity.desc", false);
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
