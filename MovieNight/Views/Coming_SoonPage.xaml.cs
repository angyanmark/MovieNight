using MovieNight.ViewModels;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace MovieNight.Views
{
    public class TimeContainer
    {
        public int TimeIdx = 1;
    }

    public sealed partial class Coming_SoonPage : Page
    {
        private static readonly TimeContainer tc = new TimeContainer();
        private int time = 1;

        private Coming_SoonViewModel ViewModel
        {
            get { return ViewModelLocator.Current.Coming_SoonViewModel; }
        }

        public Coming_SoonPage()
        {
            InitializeComponent();
            timeCombo.SelectedIndex = tc.TimeIdx;
            ViewModel.LoadCompleted += ViewModel_LoadCompleted;
        }

        private void ViewModel_LoadCompleted()
        {
            findButton.IsEnabled = true;
        }

        public void SetTime()
        {
            switch (timeCombo.SelectedIndex)
            {
                case 0:
                    time = 0;
                    break;
                case 1:
                    time = 1;
                    break;
                case 2:
                    time = 2;
                    break;
                case 3:
                    time = 3;
                    break;
                case 4:
                    time = 5;
                    break;
                case 5:
                    time = 10;
                    break;
                case 6:
                    time = -1;
                    break;
            }
        }

        private void FindButton_Click(object sender, RoutedEventArgs e)
        {
            findButton.IsEnabled = false;
            tc.TimeIdx = timeCombo.SelectedIndex;
            SetTime();
            ViewModel.Source.Clear();
            ViewModel.loadedPages = 0;
            ViewModel.noMore = false;
            _ = ViewModel.LoadMovies(time);
        }
    }
}
