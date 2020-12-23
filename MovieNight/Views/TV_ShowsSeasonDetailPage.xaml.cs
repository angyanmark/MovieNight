using Microsoft.Toolkit.Uwp.UI.Animations;
using MovieNight.Services;
using MovieNight.ViewModels;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

namespace MovieNight.Views
{
    public sealed partial class TV_ShowsSeasonDetailPage : Page
    {
        private TV_ShowsSeasonDetailViewModel ViewModel
        {
            get { return ViewModelLocator.Current.TV_ShowsSeasonDetailViewModel; }
        }

        public NavigationServiceEx NavigationService => ViewModelLocator.Current.NavigationService;

        public TV_ShowsSeasonDetailPage()
        {
            InitializeComponent();
            ViewModel.LoadCompleted += ViewModel_LoadCompleted;
        }

        private void ViewModel_LoadCompleted()
        {
            if (ViewModel.EpisodeSource.Count < 1)
            {
                pivot.Items.Remove(episodesPivot);
                pivot.Items.Remove(sep1);
            }
            if (ViewModel.CastSource.Count < 1)
            {
                pivot.Items.Remove(castPivot);
                pivot.Items.Remove(sep2);
            }
            if (ViewModel.CrewSource.Count < 1)
            {
                pivot.Items.Remove(crewPivot);
                pivot.Items.Remove(sep3);
            }
            if (ViewModel.PosterSource.Count < 1)
            {
                pivot.Items.Remove(postersPivot);
                pivot.Items.Remove(sep4);
            }
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            if (e.Parameter is Holder h)
            {
                ViewModel.Initialize(h.tv_id, h.season_number, h.showName);
            }
        }

        protected override void OnNavigatingFrom(NavigatingCancelEventArgs e)
        {
            base.OnNavigatingFrom(e);
            if (e.NavigationMode == NavigationMode.Back)
            {
                NavigationService.Frame.SetListDataItemForNextConnectedAnimation(ViewModel.Item);
            }
        }
    }
}
