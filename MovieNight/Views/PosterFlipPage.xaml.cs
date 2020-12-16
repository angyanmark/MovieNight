using MovieNight.ViewModels;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

namespace MovieNight.Views
{
    public sealed partial class ImageFlipPage : Page
    {
        private PosterFlipViewModel ViewModel
        {
            get { return ViewModelLocator.Current.ImageFlipViewModel; }
        }

        public ImageFlipPage()
        {
            InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            if (e.Parameter is ImageHolder ih)
            {
                ViewModel.Initialize(ih);
            }
        }
    }
}
