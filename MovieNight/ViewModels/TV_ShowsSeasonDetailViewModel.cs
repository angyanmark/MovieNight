using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using MovieNight.Core.Models;
using MovieNight.Core.Services;
using MovieNight.Services;

namespace MovieNight.ViewModels
{
    public class TV_ShowsSeasonDetailViewModel : ViewModelBase
    {
        public NavigationServiceEx NavigationService => ViewModelLocator.Current.NavigationService;

        private ICommand _itemClickCommandCast;

        public ICommand ItemClickCommandCast => _itemClickCommandCast ?? (_itemClickCommandCast = new RelayCommand<Cast>(OnItemClick));

        private ICommand _itemClickCommandCrew;

        public ICommand ItemClickCommandCrew => _itemClickCommandCrew ?? (_itemClickCommandCrew = new RelayCommand<Crew>(OnItemClick));

        public ObservableCollection<Cast> CastSource { get; set; }

        public ObservableCollection<Crew> CrewSource { get; set; }

        public ObservableCollection<Episode> EpisodeSource { get; set; }

        private TVShowSeason _item;

        public TVShowSeason Item
        {
            get { return _item; }
            set { Set(ref _item, value); }
        }

        public TV_ShowsSeasonDetailViewModel()
        {
            EpisodeSource = new ObservableCollection<Episode>();
            CastSource = new ObservableCollection<Cast>();
            CrewSource = new ObservableCollection<Crew>();
        }

        public void Initialize(int tv_id, int season_number, string showName)
        {
            //var first = d.First();
            Item = APICalls.CallDetailedTVShowSeason(tv_id, season_number, showName);

            CastSource.Clear();
            foreach (var castItem in Item.credits.cast)
                CastSource.Add(castItem);

            CrewSource.Clear();
            foreach (var crewItem in Item.credits.crew)
                CrewSource.Add(crewItem);

            EpisodeSource.Clear();
            foreach (var episodeItem in Item.episodes)
                EpisodeSource.Add(episodeItem);
        }

        private void OnItemClick(Cast clickedItem)
        {
            if (clickedItem != null)
            {
                //NavigationService.Frame.SetListDataItemForNextConnectedAnimation(clickedItem);
                NavigationService.Navigate(typeof(PeopleDetailViewModel).FullName, clickedItem.id);
            }
        }

        private void OnItemClick(Crew clickedItem)
        {
            if (clickedItem != null)
            {
                //NavigationService.Frame.SetListDataItemForNextConnectedAnimation(clickedItem);
                NavigationService.Navigate(typeof(PeopleDetailViewModel).FullName, clickedItem.id);
            }
        }
    }
}
