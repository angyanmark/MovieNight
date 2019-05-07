using System;
using System.Collections.ObjectModel;
using System.Windows.Input;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;

using Microsoft.Toolkit.Uwp.UI.Animations;
using MovieNight.Core.Models;
using MovieNight.Services;

namespace MovieNight.ViewModels
{
    public class SearchViewModel : ViewModelBase
    {
        public NavigationServiceEx NavigationService => ViewModelLocator.Current.NavigationService;

        private ICommand _itemClickCommand;

        public ICommand ItemClickCommand => _itemClickCommand ?? (_itemClickCommand = new RelayCommand<MultiSearchItem>(OnItemClick));

        public ObservableCollection<MultiSearchItem> Source { get; set; } = new ObservableCollection<MultiSearchItem>();

        public SearchViewModel()
        {
        }

        private void OnItemClick(MultiSearchItem clickedItem)
        {
            if (clickedItem != null)
            {
                switch (clickedItem.media_type)
                {
                    case "movie":
                        NavigationService.Frame.SetListDataItemForNextConnectedAnimation(clickedItem);
                        NavigationService.Navigate(typeof(MoviesDetailViewModel).FullName, clickedItem.id);
                        break;
                    case "tv":
                        NavigationService.Frame.SetListDataItemForNextConnectedAnimation(clickedItem);
                        NavigationService.Navigate(typeof(TV_ShowsDetailViewModel).FullName, clickedItem.id);
                        break;
                    default: //person
                        NavigationService.Frame.SetListDataItemForNextConnectedAnimation(clickedItem);
                        NavigationService.Navigate(typeof(PeopleDetailViewModel).FullName, clickedItem.id);
                        break;
                }
            }
        }
    }
}
