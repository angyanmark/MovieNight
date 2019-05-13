using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
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
    public class Holder
    {
        public int tv_id;
        public int season_number;
        public string showName;
    }
    public class TV_ShowsDetailViewModel : ViewModelBase
    {
        private TVShow tvshow = new TVShow();

        public TVShow TVShow
        {
            get { return tvshow; }
            set { Set(ref tvshow, value); }
        }

        public delegate void loadCompleted();
        public event loadCompleted LoadCompleted;

        public NavigationServiceEx NavigationService => ViewModelLocator.Current.NavigationService;

        private ICommand _itemClickCommandRecommendations;

        public ICommand ItemClickCommandRecommendations => _itemClickCommandRecommendations ?? (_itemClickCommandRecommendations = new RelayCommand<Result1>(OnItemClick));

        private ICommand _itemClickCommandCast;

        public ICommand ItemClickCommandCast => _itemClickCommandCast ?? (_itemClickCommandCast = new RelayCommand<Cast>(OnItemClick));

        private ICommand _itemClickCommandCrew;

        public ICommand ItemClickCommandCrew => _itemClickCommandCrew ?? (_itemClickCommandCrew = new RelayCommand<Crew>(OnItemClick));

        private ICommand _itemClickCommandSeason;

        public ICommand ItemClickCommandSeason => _itemClickCommandSeason ?? (_itemClickCommandSeason = new RelayCommand<Season>(OnItemClick));

        private ICommand _itemClickCommandPoster;

        public ICommand ItemClickCommandPoster => _itemClickCommandPoster ?? (_itemClickCommandPoster = new RelayCommand<Poster>(OnItemClick));

        private ICommand _itemClickCommandBackdrop;

        public ICommand ItemClickCommandBackdrop => _itemClickCommandBackdrop ?? (_itemClickCommandBackdrop = new RelayCommand<Backdrop>(OnItemClick));

        public ObservableCollection<Poster> PosterSource { get; set; }
        public ObservableCollection<Backdrop> BackdropSource { get; set; }
        public ObservableCollection<ReviewResult> ReviewSource { get; set; }
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
            PosterSource = new ObservableCollection<Poster>();
            BackdropSource = new ObservableCollection<Backdrop>();
            ReviewSource = new ObservableCollection<ReviewResult>();
            SeasonSource = new ObservableCollection<Season>();
            RecommendationsSource = new ObservableCollection<Result1>();
            CastSource = new ObservableCollection<Cast>();
            CrewSource = new ObservableCollection<Crew>();
        }

        async Task LoadTVShow(int id)
        {
            Item = await Task.Run(() => APICalls.CallDetailedTVShow(id));
            Item.poster_path = "";
            Item.backdrop_path = "";

            PosterSource.Clear();
            foreach (var posterItem in Item.images.posters)
                PosterSource.Add(posterItem);

            BackdropSource.Clear();
            foreach (var backdropItem in Item.images.backdrops)
                BackdropSource.Add(backdropItem);

            ReviewSource.Clear();
            foreach (var reviewItem in Item.reviews.results)
                ReviewSource.Add(reviewItem);

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

            LoadCompleted();
        }

        public void Initialize(int id)
        {
            LoadTVShow(id);
        }

        private void OnItemClick(Season clickedItem)
        {
            if (clickedItem != null)
            {
                NavigationService.Frame.SetListDataItemForNextConnectedAnimation(clickedItem);

                Holder h = new Holder();
                h.tv_id = Item.id;
                h.season_number = clickedItem.season_number;
                h.showName = Item.name;

                NavigationService.Navigate(typeof(TV_ShowsSeasonDetailViewModel).FullName, h);
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

        private void OnItemClick(Poster clickedItem)
        {
            if (clickedItem != null)
            {
                int idx = 0;
                foreach (Poster p in PosterSource)
                {
                    if (clickedItem.file_path == p.file_path)
                    {
                        break;
                    }
                    idx++;
                }
                ImageHolder ih = new ImageHolder();
                ih.Posters = PosterSource;
                ih.selectedIndex = idx;

                NavigationService.Frame.SetListDataItemForNextConnectedAnimation(clickedItem);
                NavigationService.Navigate(typeof(PosterFlipViewModel).FullName, ih);
            }
        }

        private void OnItemClick(Backdrop clickedItem)
        {
            if (clickedItem != null)
            {
                int idx = 0;
                foreach (Backdrop b in BackdropSource)
                {
                    if (clickedItem.file_path == b.file_path)
                    {
                        break;
                    }
                    idx++;
                }
                ImageHolder ih = new ImageHolder();
                ih.Backdrops = BackdropSource;
                ih.selectedIndex = idx;

                NavigationService.Frame.SetListDataItemForNextConnectedAnimation(clickedItem);
                NavigationService.Navigate(typeof(BackdropFlipViewModel).FullName, ih);
            }
        }
    }
}
