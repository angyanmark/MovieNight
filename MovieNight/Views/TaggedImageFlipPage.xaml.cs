using MovieNight.ViewModels;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

namespace MovieNight.Views
{
    public sealed partial class TaggedImageFlipPage : Page
    {
        private TaggedImageFlipViewModel ViewModel
        {
            get { return ViewModelLocator.Current.TaggedImageFlipViewModel; }
        }

        public TaggedImageFlipPage()
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
