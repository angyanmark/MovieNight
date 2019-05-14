using System;
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
    public class Popular_TV_ShowsViewModel : ViewModelBase
    {
        private int loadedPages = 0;
        private bool noMore = false;

        public NavigationServiceEx NavigationService => ViewModelLocator.Current.NavigationService;

        private ICommand _itemClickCommand;

        public ICommand ItemClickCommand => _itemClickCommand ?? (_itemClickCommand = new RelayCommand<TVShow>(OnItemClick));

        public ObservableCollection<TVShow> Source { get; set; } = new ObservableCollection<TVShow>();

        async Task LoadTVShows()
        {
            if (!noMore)
            {
                ObservableCollection<TVShow> tvshows = new ObservableCollection<TVShow>();

                for (int i = 0; i < APICalls.pages; i++)
                {
                    tvshows = await Task.Run(() => APICalls.CallPopularTVShows(++loadedPages));
                    if (tvshows.Count == 0)
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
        }

        public Popular_TV_ShowsViewModel()
        {
            LoadTVShows();
        }

        private void OnItemClick(TVShow clickedItem)
        {
            if (clickedItem != null)
            {
                NavigationService.Frame.SetListDataItemForNextConnectedAnimation(clickedItem);
                NavigationService.Navigate(typeof(TV_ShowsDetailViewModel).FullName, clickedItem.id);
            }
        }
    }
}
