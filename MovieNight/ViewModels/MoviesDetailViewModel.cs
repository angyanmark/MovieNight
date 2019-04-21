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
    public class MoviesDetailViewModel : ViewModelBase
    {
        public NavigationServiceEx NavigationService => ViewModelLocator.Current.NavigationService;

        private ICommand _itemClickCommandCast;

        public ICommand ItemClickCommandCast => _itemClickCommandCast ?? (_itemClickCommandCast = new RelayCommand<Cast>(OnItemClick));

        private ICommand _itemClickCommandCrew;

        public ICommand ItemClickCommandCrew => _itemClickCommandCrew ?? (_itemClickCommandCrew = new RelayCommand<Crew>(OnItemClick));

        private ICommand _itemClickCommandRecommendations;

        public ICommand ItemClickCommandRecommendations => _itemClickCommandRecommendations ?? (_itemClickCommandRecommendations = new RelayCommand<Result1>(OnItemClick));

        public ObservableCollection<Cast> CastSource { get; set; }

        public ObservableCollection<Crew> CrewSource { get; set; }

        public ObservableCollection<Result1> RecommendationsSource { get; set; }

        private Film _item;

        public Film Item
        {
            get { return _item; }
            set { Set(ref _item, value); }
        }

        public MoviesDetailViewModel()
        {
            CastSource = new ObservableCollection<Cast>();
            CrewSource = new ObservableCollection<Crew>();
            RecommendationsSource = new ObservableCollection<Result1>();
        }

        public void Initialize(int id)
        {
            Item = APICalls.CallDetailedFilm(id);

            RecommendationsSource.Clear();
            foreach (var recommendationItem in Item.recommendations.results)
                RecommendationsSource.Add(recommendationItem);

            CastSource.Clear();
            foreach (var castItem in Item.credits.cast)
                CastSource.Add(castItem);

            CrewSource.Clear();
            foreach (var crewItem in Item.credits.crew)
                CrewSource.Add(crewItem);            
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

        private void OnItemClick(Result1 clickedItem)
        {
            if (clickedItem != null)
            {
                NavigationService.Frame.SetListDataItemForNextConnectedAnimation(clickedItem);
                NavigationService.Navigate(typeof(MoviesDetailViewModel).FullName, clickedItem.id);
            }
        }
    }
}
