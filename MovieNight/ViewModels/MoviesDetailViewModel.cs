using System;
using System.Linq;

using GalaSoft.MvvmLight;

using MovieNight.Core.Models;
using MovieNight.Core.Services;

namespace MovieNight.ViewModels
{
    public class MoviesDetailViewModel : ViewModelBase
    {
        private Film _item;

        public Film Item
        {
            get { return _item; }
            set { Set(ref _item, value); }
        }

        public MoviesDetailViewModel()
        {
        }

        public void Initialize(int id)
        {
             Item = APICalls.CallDetailedFilm(id);
        }
    }
}
