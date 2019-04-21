using System;
using System.Collections.Generic;
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
    public class PeopleDetailViewModel : ViewModelBase
    {
        public NavigationServiceEx NavigationService => ViewModelLocator.Current.NavigationService;

        private ICommand _itemClickCommandCast;

        public ICommand ItemClickCommandCast => _itemClickCommandCast ?? (_itemClickCommandCast = new RelayCommand<Cast>(OnItemClick));

        private ICommand _itemClickCommandCrew;

        public ICommand ItemClickCommandCrew => _itemClickCommandCrew ?? (_itemClickCommandCrew = new RelayCommand<Crew>(OnItemClick));

        public ObservableCollection<Cast> CastSource { get; set; }

        public ObservableCollection<Crew> CrewSource { get; set; }

        private Person _item;

        public Person Item
        {
            get { return _item; }
            set { Set(ref _item, value); }
        }

        public Dictionary<string, string> tag;

        public string tagPath
        {
            get
            {
                var s = tag.First();
                return s.Key;
            }
        }

        public string tagTitle
        {
            get
            {
                var s = tag.First();
                return s.Value;
            }
        }

        public string isTagged
        {
            get
            {
                if(tagPath == "https://image.tmdb.org/t/p/original/")
                {
                    return "Collapsed";
                }
                else
                {
                    return "Visible";
                }
            }
        }

        public PeopleDetailViewModel()
        {
            CastSource = new ObservableCollection<Cast>();
            CrewSource = new ObservableCollection<Crew>();
        }

        public void Initialize(int id)
        {
            Item = APICalls.CallDetailedPerson(id);
            tag = Item.getTagged;

            CastSource.Clear();
            foreach (var castItem in Item.combined_credits.cast)
                CastSource.Add(castItem);

            CrewSource.Clear();
            foreach (var crewItem in Item.combined_credits.crew)
                CrewSource.Add(crewItem);
        }

        private void OnItemClick(Cast clickedItem)
        {
            if (clickedItem != null)
            {
                if(clickedItem.media_type == "movie")
                {
                    NavigationService.Frame.SetListDataItemForNextConnectedAnimation(clickedItem);
                    NavigationService.Navigate(typeof(MoviesDetailViewModel).FullName, clickedItem.id);
                }
                else
                {
                    NavigationService.Frame.SetListDataItemForNextConnectedAnimation(clickedItem);
                    NavigationService.Navigate(typeof(TV_ShowsDetailViewModel).FullName, clickedItem.id);
                }
            }
        }

        private void OnItemClick(Crew clickedItem)
        {
            if (clickedItem != null)
            {
                if (clickedItem.media_type == "movie")
                {
                    NavigationService.Frame.SetListDataItemForNextConnectedAnimation(clickedItem);
                    NavigationService.Navigate(typeof(MoviesDetailViewModel).FullName, clickedItem.id);
                }
                else
                {
                    NavigationService.Frame.SetListDataItemForNextConnectedAnimation(clickedItem);
                    NavigationService.Navigate(typeof(TV_ShowsDetailViewModel).FullName, clickedItem.id);
                }
            }
        }
    }
}
