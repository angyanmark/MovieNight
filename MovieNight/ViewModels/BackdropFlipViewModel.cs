using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using GalaSoft.MvvmLight;
using MovieNight.Core.Models;

namespace MovieNight.ViewModels
{
    public class BackdropFlipViewModel : ViewModelBase
    {
        public ObservableCollection<Backdrop> BackdropSource { get; set; }
        public int selectedIndex = 0;

        public BackdropFlipViewModel()
        {
            BackdropSource = new ObservableCollection<Backdrop>();
        }

        async Task LoadImage(ImageHolder ih)
        {
            BackdropSource.Clear();
            foreach (Backdrop b in ih.Backdrops)
                BackdropSource.Add(b);

            selectedIndex = ih.selectedIndex;
        }

        public void Initialize(ImageHolder ih)
        {
            LoadImage(ih);
        }
    }
}
