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
    public class PeopleDetailViewModel : ViewModelBase
    {
        private Person person = new Person();

        public Person Person
        {
            get { return person; }
            set { Set(ref person, value); }
        }

        public delegate void loadCompleted();
        public event loadCompleted LoadCompleted;

        public NavigationServiceEx NavigationService => ViewModelLocator.Current.NavigationService;

        private ICommand _itemClickCommandCast;

        public ICommand ItemClickCommandCast => _itemClickCommandCast ?? (_itemClickCommandCast = new RelayCommand<Cast>(OnItemClick));

        private ICommand _itemClickCommandCrew;

        public ICommand ItemClickCommandCrew => _itemClickCommandCrew ?? (_itemClickCommandCrew = new RelayCommand<Crew>(OnItemClick));

        private ICommand _itemClickCommandProfile;

        public ICommand ItemClickCommandProfile => _itemClickCommandProfile ?? (_itemClickCommandProfile = new RelayCommand<Profile>(OnItemClick));

        private ICommand _itemClickCommandTaggedImage;

        public ICommand ItemClickCommandTaggedImage => _itemClickCommandTaggedImage ?? (_itemClickCommandTaggedImage = new RelayCommand<TaggedImagesResult>(OnItemClick));

        public ObservableCollection<Profile> ProfilesSource { get; set; }

        public ObservableCollection<TaggedImagesResult> TaggedImagesSource { get; set; }

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

        public PeopleDetailViewModel()
        {
            ProfilesSource = new ObservableCollection<Profile>();
            TaggedImagesSource = new ObservableCollection<TaggedImagesResult>();
            CastSource = new ObservableCollection<Cast>();
            PermanentCast = new ObservableCollection<Cast>();
            CrewSource = new ObservableCollection<Crew>();
            PermanentCrew = new ObservableCollection<Crew>();
        }

        async Task LoadPerson(int id)
        {
            Item = await Task.Run(() => TMDbService.GetDetailedPerson(id));
            Item.profile_path = "";

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

            LoadCompleted();
        }

        public void Initialize(int id)
        {
            _ = LoadPerson(id);
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

        private void OnItemClick(Profile clickedItem)
        {
            if (clickedItem != null)
            {
                int idx = 0;
                foreach (Profile p in ProfilesSource)
                {
                    if (clickedItem.file_path == p.file_path)
                    {
                        break;
                    }
                    idx++;
                }
                ImageHolder ih = new ImageHolder
                {
                    Profiles = ProfilesSource,
                    selectedIndex = idx
                };

                NavigationService.Frame.SetListDataItemForNextConnectedAnimation(clickedItem);
                NavigationService.Navigate(typeof(ProfileFlipViewModel).FullName, ih);
            }
        }

        private void OnItemClick(TaggedImagesResult clickedItem)
        {
            if (clickedItem != null)
            {
                int idx = 0;
                foreach (TaggedImagesResult b in TaggedImagesSource)
                {
                    if (clickedItem.file_path == b.file_path)
                    {
                        break;
                    }
                    idx++;
                }
                ImageHolder ih = new ImageHolder
                {
                    TaggedImages = TaggedImagesSource,
                    selectedIndex = idx
                };

                NavigationService.Frame.SetListDataItemForNextConnectedAnimation(clickedItem);
                NavigationService.Navigate(typeof(TaggedImageFlipViewModel).FullName, ih);
            }
        }
    }
}
