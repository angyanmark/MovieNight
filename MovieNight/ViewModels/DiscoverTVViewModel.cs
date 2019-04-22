using System;
using System.Collections.ObjectModel;
using System.Windows.Input;

using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;

using Microsoft.Toolkit.Uwp.UI.Animations;

using MovieNight.Core.Models;
using MovieNight.Core.Services;
using MovieNight.Services;

namespace MovieNight.ViewModels
{
    public class DiscoverTVViewModel : ViewModelBase
    {
        public NavigationServiceEx NavigationService => ViewModelLocator.Current.NavigationService;

        private ICommand _itemClickCommand;

        public ICommand ItemClickCommand => _itemClickCommand ?? (_itemClickCommand = new RelayCommand<DiscoverItem>(OnItemClick));

        public ObservableCollection<DiscoverItem> Source { get; set; }

        public DiscoverTVViewModel()
        {
            Source = APICalls.CallDiscoverTVPage(0, 0, 0, "popularity.desc");
        }

        private void OnItemClick(DiscoverItem clickedItem)
        {
            if (clickedItem != null)
            {
                NavigationService.Frame.SetListDataItemForNextConnectedAnimation(clickedItem);
                NavigationService.Navigate(typeof(TV_ShowsDetailViewModel).FullName, clickedItem.id);
            }
        }
    }
}
