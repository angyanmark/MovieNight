using System;
using System.Linq;

using GalaSoft.MvvmLight;

using MovieNight.Core.Models;
using MovieNight.Core.Services;

namespace MovieNight.ViewModels
{
    public class PeopleDetailViewModel : ViewModelBase
    {
        private Person _item;

        public Person Item
        {
            get { return _item; }
            set { Set(ref _item, value); }
        }

        public PeopleDetailViewModel()
        {
        }

        public void Initialize(int id)
        {
            Item = APICalls.CallDetailedPerson(id);
        }
    }
}
