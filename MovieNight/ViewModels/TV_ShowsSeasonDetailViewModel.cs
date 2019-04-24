using System;
using System.Linq;

using GalaSoft.MvvmLight;

using MovieNight.Core.Models;
using MovieNight.Core.Services;

namespace MovieNight.ViewModels
{
    public class TV_ShowsSeasonDetailViewModel : ViewModelBase
    {
        private SampleOrder _item;

        public SampleOrder Item
        {
            get { return _item; }
            set { Set(ref _item, value); }
        }

        public TV_ShowsSeasonDetailViewModel()
        {
        }

        public void Initialize(long orderId)
        {
            var data = SampleDataService.GetContentGridData();
            Item = data.First(i => i.OrderId == orderId);
        }
    }
}
