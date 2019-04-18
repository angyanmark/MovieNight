using System;
using System.Linq;

using GalaSoft.MvvmLight;

using MovieNight.Core.Models;
using MovieNight.Core.Services;

namespace MovieNight.ViewModels
{
    public class TV_ShowsDetailViewModel : ViewModelBase
    {
        private TVShow _item;

        public TVShow Item
        {
            get { return _item; }
            set { Set(ref _item, value); }
        }

        public TV_ShowsDetailViewModel()
        {
        }

        public void Initialize(int id)
        {
            Item = APICalls.CallDetailedTVShow(id);
        }
    }
}
