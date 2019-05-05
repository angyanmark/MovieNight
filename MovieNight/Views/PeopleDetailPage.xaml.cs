using System;
using System.Collections.ObjectModel;
using System.Linq;
using Microsoft.Toolkit.Uwp.UI.Animations;
using MovieNight.Core.Models;
using MovieNight.Services;
using MovieNight.ViewModels;

using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

namespace MovieNight.Views
{
    public sealed partial class Popular_PeopleDetailPage : Page
    {
        private PeopleDetailViewModel ViewModel
        {
            get { return ViewModelLocator.Current.Popular_PeopleDetailViewModel; }
        }

        public NavigationServiceEx NavigationService => ViewModelLocator.Current.NavigationService;

        public Popular_PeopleDetailPage()
        {
            InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            if (e.Parameter is int id)
            {
                ViewModel.Initialize(id);
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

        private void castSortByCombo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var comboBoxItem = e.AddedItems[0] as ComboBoxItem;
            if (comboBoxItem == null) return;
            var content = comboBoxItem.Content as string;
            if (content != null && content.Equals("Popularity"))
            {
                ViewModel.CastSource.Clear();
                foreach(var v in ViewModel.PermanentCast)
                {
                    ViewModel.CastSource.Add(v);
                }
            }
            if (content != null && content.Equals("Release Date - Newest First"))
            {
                ObservableCollection<Cast> temp = new ObservableCollection<Cast>(ViewModel.CastSource.OrderByDescending(o => o.combined_date));
                ViewModel.CastSource.Clear();
                foreach (var v in temp)
                {
                    ViewModel.CastSource.Add(v);
                }

                int size = ViewModel.CastSource.Count;
                int cnt = size - 1;
                for (int i = ViewModel.CastSource.Count - 1; i >= 0; i--)
                {
                    if (ViewModel.CastSource[cnt].combined_date == "" || ViewModel.CastSource[cnt].combined_date == null)
                    {
                        var item = ViewModel.CastSource[cnt];
                        ViewModel.CastSource.RemoveAt(cnt);
                        ViewModel.CastSource.Insert(0, item);
                    }
                    else
                    {
                        cnt--;
                    }
                }
            }
        }

        private void crewSortByCombo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var comboBoxItem = e.AddedItems[0] as ComboBoxItem;
            if (comboBoxItem == null) return;
            var content = comboBoxItem.Content as string;
            if (content != null && content.Equals("Popularity"))
            {
                ViewModel.CrewSource.Clear();
                foreach (var v in ViewModel.PermanentCrew)
                {
                    ViewModel.CrewSource.Add(v);
                }
            }
            if (content != null && content.Equals("Release Date - Newest First"))
            {
                ObservableCollection<Crew> temp = new ObservableCollection<Crew>(ViewModel.CrewSource.OrderByDescending(o => o.combined_date));
                ViewModel.CrewSource.Clear();
                foreach (var v in temp)
                {
                    ViewModel.CrewSource.Add(v);
                }

                int size = ViewModel.CrewSource.Count;
                int cnt = size - 1;
                for (int i = ViewModel.CrewSource.Count - 1; i >= 0; i--)
                {
                    if (ViewModel.CrewSource[cnt].combined_date == "" || ViewModel.CrewSource[cnt].combined_date == null)
                    {
                        var item = ViewModel.CrewSource[cnt];
                        ViewModel.CrewSource.RemoveAt(cnt);
                        ViewModel.CrewSource.Insert(0, item);
                    }
                    else
                    {
                        cnt--;
                    }
                }
            }
        }
    }
}
