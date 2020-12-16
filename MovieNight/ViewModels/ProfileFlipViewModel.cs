using System.Collections.ObjectModel;
using GalaSoft.MvvmLight;
using MovieNight.Core.Models;

namespace MovieNight.ViewModels
{
    public class ProfileFlipViewModel : ViewModelBase
    {
        public ObservableCollection<Profile> ProfileSource { get; set; }
        public int selectedIndex = 0;

        public ProfileFlipViewModel()
        {
            ProfileSource = new ObservableCollection<Profile>();
        }

        public void Initialize(ImageHolder ih)
        {
            LoadImage(ih);
        }

        private void LoadImage(ImageHolder ih)
        {
            ProfileSource.Clear();
            foreach (Profile p in ih.Profiles)
                ProfileSource.Add(p);

            selectedIndex = ih.selectedIndex;
        }
    }
}
