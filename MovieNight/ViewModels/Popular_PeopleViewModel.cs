using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using Microsoft.Toolkit.Uwp.UI.Animations;
using MovieNight.Core.Models;
using MovieNight.Core.Services;
using MovieNight.Helpers;
using MovieNight.Services;

namespace MovieNight.ViewModels
{
    public class Popular_PeopleViewModel : ViewModelBase
    {
        private int loadedPages = 0;
        private bool noMore = false;

        public NavigationServiceEx NavigationService => ViewModelLocator.Current.NavigationService;

        private ICommand _itemClickCommand;

        public ICommand ItemClickCommand => _itemClickCommand ?? (_itemClickCommand = new RelayCommand<Person>(OnItemClick));

        public ObservableCollection<Person> Source { get; set; } = new ObservableCollection<Person>();

        async Task LoadPeople()
        {
            if (!noMore)
            {
                for (int i = 0; i < TMDbService.pages; i++)
                {
                    var people = await TMDbService.GetPopularPeopleAsync(++loadedPages);
                    if (!people.IsAny())
                    {
                        noMore = true;
                        break;
                    }
                    foreach (var v in people)
                    {
                        Source.Add(v);
                    }
                }
            }
        }

        public Popular_PeopleViewModel()
        {
            _ = LoadPeople();
        }

        private void OnItemClick(Person clickedItem)
        {
            if (clickedItem != null)
            {
                NavigationService.Frame.SetListDataItemForNextConnectedAnimation(clickedItem);
                NavigationService.Navigate(typeof(PeopleDetailViewModel).FullName, clickedItem.id);
            }
        }
    }
}
