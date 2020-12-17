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
    public class DiscoverTVViewModel : ViewModelBase
    {
        public int loadedPages = 0;
        public bool noMore = false;

        public delegate void loadCompleted();
        public event loadCompleted LoadCompleted;

        public NavigationServiceEx NavigationService => ViewModelLocator.Current.NavigationService;

        private ICommand _itemClickCommand;

        public ICommand ItemClickCommand => _itemClickCommand ?? (_itemClickCommand = new RelayCommand<DiscoverItem>(OnItemClick));

        public ObservableCollection<DiscoverItem> Source { get; set; } = new ObservableCollection<DiscoverItem>();

        public async Task LoadTVShows(string keyword, int decade, int year, int genre, int count, string sortby)
        {
            if (!noMore)
            {
                for (int i = 0; i < TMDbService.pages; i++)
                {
                    var tvshows = await TMDbService.GetDiscoverTVPageAsync(++loadedPages, keyword, decade, year, genre, count, sortby);
                    if (!tvshows.IsAny())
                    {
                        noMore = true;
                        break;
                    }
                    foreach (var v in tvshows)
                    {
                        Source.Add(v);
                    }
                }
            }
            LoadCompleted();
        }

        public DiscoverTVViewModel()
        {
            _ = LoadTVShows("", 0, 0, 0, 0, "popularity.desc");
        }

        private void OnItemClick(DiscoverItem clickedItem)
        {
            if (clickedItem != null)
            {
                NavigationService.Frame.SetListDataItemForNextConnectedAnimation(clickedItem);
                NavigationService.Navigate(typeof(TV_ShowsDetailViewModel).FullName, clickedItem.id);
            }
        }
    }
}
