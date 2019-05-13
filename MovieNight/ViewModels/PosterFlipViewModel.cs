using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using GalaSoft.MvvmLight;
using MovieNight.Core.Models;

namespace MovieNight.ViewModels
{
    public class PosterFlipViewModel : ViewModelBase
    {
        public ObservableCollection<Poster> PosterSource { get; set; }
        public int selectedIndex = 0;

        public PosterFlipViewModel()
        {
            PosterSource = new ObservableCollection<Poster>();
        }

        async Task LoadImage(ImageHolder ih)
        {
            PosterSource.Clear();
            foreach (Poster p in ih.Posters)
                PosterSource.Add(p);

            selectedIndex = ih.selectedIndex;
        }

        public void Initialize(ImageHolder ih)
        {
            LoadImage(ih);
        }
    }
}
