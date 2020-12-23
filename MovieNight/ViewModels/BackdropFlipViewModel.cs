using System.Collections.ObjectModel;
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

        public void Initialize(ImageHolder ih)
        {
            LoadImage(ih);
        }

        private void LoadImage(ImageHolder ih)
        {
            BackdropSource.Clear();
            foreach (Backdrop b in ih.Backdrops)
                BackdropSource.Add(b);

            selectedIndex = ih.selectedIndex;
        }
    }
}
