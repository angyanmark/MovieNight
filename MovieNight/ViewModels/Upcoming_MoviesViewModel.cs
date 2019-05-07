using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;

using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;

using Microsoft.Toolkit.Uwp.UI.Animations;

using MovieNight.Core.Models;
using MovieNight.Core.Services;
using MovieNight.Services;

namespace MovieNight.ViewModels
{
    public class Upcoming_MoviesViewModel : ViewModelBase
    {
        public NavigationServiceEx NavigationService => ViewModelLocator.Current.NavigationService;

        private ICommand _itemClickCommand;

        public ICommand ItemClickCommand => _itemClickCommand ?? (_itemClickCommand = new RelayCommand<Film>(OnItemClick));

        public ObservableCollection<Film> Source { get; set; } = new ObservableCollection<Film>();

        async Task LoadMovies()
        {
            ObservableCollection<Film> films = await Task.Run(() => APICalls.CallUpcomingFilms());
            foreach (var v in films)
            {
                Source.Add(v);
            }
        }

        public Upcoming_MoviesViewModel()
        {
            LoadMovies();
        }

        private void OnItemClick(Film clickedItem)
        {
            if (clickedItem != null)
            {
                NavigationService.Frame.SetListDataItemForNextConnectedAnimation(clickedItem);
                NavigationService.Navigate(typeof(MoviesDetailViewModel).FullName, clickedItem.id);
            }
        }
    }
}
