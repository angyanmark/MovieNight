using System;
using System.Collections.Generic;
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

        public Dictionary<string, string> tag;

        public string tagPath
        {
            get
            {
                var s = tag.First();
                return s.Key;
            }
        }

        public string tagTitle
        {
            get
            {
                var s = tag.First();
                return s.Value;
            }
        }

        public string isTagged
        {
            get
            {
                if(tagPath == "https://image.tmdb.org/t/p/original/")
                {
                    return "Collapsed";
                }
                else
                {
                    return "Visible";
                }
            }
        }

        public PeopleDetailViewModel()
        {
        }

        public void Initialize(int id)
        {
            Item = APICalls.CallDetailedPerson(id);
            tag = Item.getTagged;
        }
    }
}
