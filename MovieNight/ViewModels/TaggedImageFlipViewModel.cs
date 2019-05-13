using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using GalaSoft.MvvmLight;
using MovieNight.Core.Models;

namespace MovieNight.ViewModels
{
    public class TaggedImageFlipViewModel : ViewModelBase
    {
        public ObservableCollection<TaggedImagesResult> TaggedImageSource { get; set; }
        public int selectedIndex = 0;

        public TaggedImageFlipViewModel()
        {
            TaggedImageSource = new ObservableCollection<TaggedImagesResult>();
        }

        async Task LoadImage(ImageHolder ih)
        {
            TaggedImageSource.Clear();
            foreach (TaggedImagesResult t in ih.TaggedImages)
                TaggedImageSource.Add(t);

            selectedIndex = ih.selectedIndex;
        }

        public void Initialize(ImageHolder ih)
        {
            LoadImage(ih);
        }
    }
}
