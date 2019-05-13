using System;

using Microsoft.Toolkit.Uwp.UI.Animations;

using MovieNight.Core.Models;
using MovieNight.Helpers;
using MovieNight.Services;
using MovieNight.ViewModels;

using Windows.System;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Navigation;

namespace MovieNight.Views
{
    public sealed partial class ImageGalleryDetailPage : Page
    {
        private ImageGalleryDetailViewModel ViewModel
        {
            get { return ViewModelLocator.Current.ImageGalleryDetailViewModel; }
        }

        public NavigationServiceEx NavigationService => ViewModelLocator.Current.NavigationService;

        public ImageGalleryDetailPage()
        {
            InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            ViewModel.Initialize(e.Parameter as string, e.NavigationMode);
        }

        protected override void OnNavigatingFrom(NavigatingCancelEventArgs e)
        {
            base.OnNavigatingFrom(e);
            if (e.NavigationMode == NavigationMode.Back)
            {
                NavigationService.Frame.SetListDataItemForNextConnectedAnimation(ViewModel.SelectedImage);
                //ImagesNavigationHelper.RemoveImageId(ImageGalleryViewModel.ImageGallerySelectedIdKey);
            }
        }

        private void OnPageKeyDown(object sender, KeyRoutedEventArgs e)
        {
            if (e.Key == VirtualKey.Escape && NavigationService.CanGoBack)
            {
                NavigationService.GoBack();
                e.Handled = true;
            }
        }
    }
}
