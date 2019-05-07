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
using Windows.UI.Xaml.Controls;

namespace MovieNight.ViewModels
{
    public class PeopleDetailViewModel : ViewModelBase
    {
        private Person person = new Person();

        public Person Person
        {
            get { return person; }
            set { Set(ref person, value); }
        }

        public NavigationServiceEx NavigationService => ViewModelLocator.Current.NavigationService;

        private ICommand _itemClickCommandCast;

        public ICommand ItemClickCommandCast => _itemClickCommandCast ?? (_itemClickCommandCast = new RelayCommand<Cast>(OnItemClick));

        private ICommand _itemClickCommandCrew;

        public ICommand ItemClickCommandCrew => _itemClickCommandCrew ?? (_itemClickCommandCrew = new RelayCommand<Crew>(OnItemClick));

        public ObservableCollection<Profile> ProfilesSource { get; set; }

        public ObservableCollection<Result3> TaggedImagesSource { get; set; }

        public ObservableCollection<Cast> CastSource { get; set; }

        public ObservableCollection<Cast> PermanentCast { get; set; }

        public ObservableCollection<Crew> CrewSource { get; set; }

        public ObservableCollection<Crew> PermanentCrew { get; set; }

        private Person _item;

        public Person Item
        {
            get { return _item; }
            set { Set(ref _item, value); }
        }

        public Dictionary<string, string> tag;

        public string tagPath { get; set; }

        public string tagTitle { get; set; }

        public string isTagged { get; set; }

        public PeopleDetailViewModel()
        {
            ProfilesSource = new ObservableCollection<Profile>();
            TaggedImagesSource = new ObservableCollection<Result3>();
            CastSource = new ObservableCollection<Cast>();
            PermanentCast = new ObservableCollection<Cast>();
            CrewSource = new ObservableCollection<Crew>();
            PermanentCrew = new ObservableCollection<Crew>();
        }

        async Task LoadPerson(int id)
        {
            Item = await Task.Run(() => APICalls.CallDetailedPerson(id));

            /*tag = Item.getTagged;
            tagPath = tag.First().Key;
            tagTitle = tag.First().Value;
            if (tagPath == "https://image.tmdb.org/t/p/original/")
            {
                isTagged = "Collapsed";
            }
            else
            {
                isTagged = "Visible";
            }*/

            ProfilesSource.Clear();
            foreach (var imageItem in Item.images.profiles)
                ProfilesSource.Add(imageItem);

            TaggedImagesSource.Clear();
            foreach (var taggedImageItem in Item.tagged_images.results)
                TaggedImagesSource.Add(taggedImageItem);

            CastSource.Clear();
            foreach (var castItem in Item.combined_credits.cast)
                CastSource.Add(castItem);

            CrewSource.Clear();
            foreach (var crewItem in Item.combined_credits.crew)
                CrewSource.Add(crewItem);

            PermanentCast.Clear();
            foreach (var permCastItem in CastSource)
                PermanentCast.Add(permCastItem);

            PermanentCrew.Clear();
            foreach (var permCrewItem in CrewSource)
                PermanentCrew.Add(permCrewItem);
        }

        public void Initialize(int id)
        {
            LoadPerson(id);
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
