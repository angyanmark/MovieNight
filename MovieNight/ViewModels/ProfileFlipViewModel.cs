using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
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

        async Task LoadImage(ImageHolder ih)
        {
            ProfileSource.Clear();
            foreach (Profile p in ih.Profiles)
                ProfileSource.Add(p);

            selectedIndex = ih.selectedIndex;
        }

        public void Initialize(ImageHolder ih)
        {
            LoadImage(ih);
        }
    }
}
