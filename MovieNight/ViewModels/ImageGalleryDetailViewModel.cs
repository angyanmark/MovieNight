using System;
using System.Collections.ObjectModel;
using System.Linq;

using GalaSoft.MvvmLight;

using MovieNight.Core.Models;
using MovieNight.Core.Services;
using MovieNight.Helpers;

using Windows.UI.Xaml.Navigation;

namespace MovieNight.ViewModels
{
    public class ImageGalleryDetailViewModel : ViewModelBase
    {
        private object _selectedImage;
        private ObservableCollection<Poster> _source;

        public object SelectedImage
        {
            get => _selectedImage;
            set
            {
                Set(ref _selectedImage, value);
                //ImagesNavigationHelper.UpdateImageId(ImageGalleryViewModel.ImageGallerySelectedIdKey, ((Poster)SelectedImage).file_path);
            }
        }

        public ObservableCollection<Poster> Source
        {
            get => _source;
            set => Set(ref _source, value);
        }

        public ImageGalleryDetailViewModel(ObservableCollection<Poster> posters)
        {
            foreach (Poster p in posters)
                Source.Add(p);
        }

        public void Initialize(string selectedImagePath, NavigationMode navigationMode)
        {
            selectedImagePath = _source[0].file_path;
            if (!string.IsNullOrEmpty(selectedImagePath) && navigationMode == NavigationMode.New)
            {
                SelectedImage = Source.FirstOrDefault(i => i.file_path == selectedImagePath);
            }
            else
            {
                //selectedImagePath = ImagesNavigationHelper.GetImageId(ImageGalleryViewModel.ImageGallerySelectedIdKey);
                if (!string.IsNullOrEmpty(selectedImagePath))
                {
                    SelectedImage = Source.FirstOrDefault(i => i.file_path == selectedImagePath);
                }
            }
        }
    }
}
