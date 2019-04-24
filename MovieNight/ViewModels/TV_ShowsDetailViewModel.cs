using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using Microsoft.Toolkit.Uwp.UI.Animations;
using MovieNight.Core.Models;
using MovieNight.Core.Services;
using MovieNight.Services;

namespace MovieNight.ViewModels
{
    public class TV_ShowsDetailViewModel : ViewModelBase
    {
        public NavigationServiceEx NavigationService => ViewModelLocator.Current.NavigationService;

        private ICommand _itemClickCommandRecommendations;

        public ICommand ItemClickCommandRecommendations => _itemClickCommandRecommendations ?? (_itemClickCommandRecommendations = new RelayCommand<Result1>(OnItemClick));

        private ICommand _itemClickCommandCast;

        public ICommand ItemClickCommandCast => _itemClickCommandCast ?? (_itemClickCommandCast = new RelayCommand<Cast>(OnItemClick));

        private ICommand _itemClickCommandCrew;

        public ICommand ItemClickCommandCrew => _itemClickCommandCrew ?? (_itemClickCommandCrew = new RelayCommand<Crew>(OnItemClick));

        private ICommand _itemClickCommandSeason;

        public ICommand ItemClickCommandSeason => _itemClickCommandSeason ?? (_itemClickCommandSeason = new RelayCommand<TVShowSeason>(OnItemClick));

        public ObservableCollection<Result1> RecommendationsSource { get; set; }

        public ObservableCollection<Cast> CastSource { get; set; }

        public ObservableCollection<Crew> CrewSource { get; set; }

        public ObservableCollection<Season> SeasonSource { get; set; }

        private TVShow _item;

        public TVShow Item
        {
            get { return _item; }
            set { Set(ref _item, value); }
        }

        public TV_ShowsDetailViewModel()
        {
            SeasonSource = new ObservableCollection<Season>();
            RecommendationsSource = new ObservableCollection<Result1>();
            CastSource = new ObservableCollection<Cast>();
            CrewSource = new ObservableCollection<Crew>();
        }

        public void Initialize(int id)
        {
            Item = APICalls.CallDetailedTVShow(id);

            RecommendationsSource.Clear();
            foreach (var recommendationItem in Item.recommendations.results)
                RecommendationsSource.Add(recommendationItem);

            CastSource.Clear();
            foreach (var castItem in Item.credits.cast)
                CastSource.Add(castItem);

            CrewSource.Clear();
            foreach (var crewItem in Item.credits.crew)
                CrewSource.Add(crewItem);

            SeasonSource.Clear();
            foreach (var seasonItem in Item.seasons)
                SeasonSource.Add(seasonItem);
        }

        private void OnItemClick(TVShowSeason clickedItem)
        {
            if (clickedItem != null)
            {
                NavigationService.Frame.SetListDataItemForNextConnectedAnimation(clickedItem);
                NavigationService.Navigate(typeof(TV_ShowsSeasonDetailViewModel).FullName, clickedItem.id);
            }
        }

        private void OnItemClick(Result1 clickedItem)
        {
            if (clickedItem != null)
            {
                NavigationService.Frame.SetListDataItemForNextConnectedAnimation(clickedItem);
                NavigationService.Navigate(typeof(TV_ShowsDetailViewModel).FullName, clickedItem.id);
            }
        }

        private void OnItemClick(Cast clickedItem)
        {
            if (clickedItem != null)
            {
                NavigationService.Frame.SetListDataItemForNextConnectedAnimation(clickedItem);
                NavigationService.Navigate(typeof(PeopleDetailViewModel).FullName, clickedItem.id);
            }
        }

        private void OnItemClick(Crew clickedItem)
        {
            if (clickedItem != null)
            {
                NavigationService.Frame.SetListDataItemForNextConnectedAnimation(clickedItem);
                NavigationService.Navigate(typeof(PeopleDetailViewModel).FullName, clickedItem.id);
            }
        }
    }
}
