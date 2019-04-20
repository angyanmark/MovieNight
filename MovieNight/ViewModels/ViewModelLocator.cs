using System;

using GalaSoft.MvvmLight.Ioc;

using MovieNight.Services;
using MovieNight.Views;

namespace MovieNight.ViewModels
{
    [Windows.UI.Xaml.Data.Bindable]
    public class ViewModelLocator
    {
        private static ViewModelLocator _current;

        public static ViewModelLocator Current => _current ?? (_current = new ViewModelLocator());

        private ViewModelLocator()
        {
            SimpleIoc.Default.Register(() => new NavigationServiceEx());
            SimpleIoc.Default.Register<ShellViewModel>();
            Register<Popular_MoviesViewModel, Popular_MoviesPage>();
            Register<MoviesDetailViewModel, Popular_MoviesDetailPage>();
            Register<Now_Playing_MoviesViewModel, Now_Playing_MoviesPage>();
            Register<Upcoming_MoviesViewModel, Upcoming_MoviesPage>();
            Register<Popular_TV_ShowsViewModel, Popular_TV_ShowsPage>();
            Register<TV_ShowsDetailViewModel, Popular_TV_ShowsDetailPage>();
            Register<TV_on_the_AirViewModel, TV_on_the_AirPage>();
            Register<Popular_PeopleViewModel, Popular_PeoplePage>();
            Register<PeopleDetailViewModel, Popular_PeopleDetailPage>();
            Register<SearchViewModel, SearchPage>();
            Register<SettingsViewModel, SettingsPage>();
            Register<TV_Airing_TodayViewModel, TV_Airing_TodayPage>();
            Register<Coming_SoonViewModel, Coming_SoonPage>();
            Register<DiscoverViewModel, DiscoverPage>();
        }

        public DiscoverViewModel DiscoverViewModel => SimpleIoc.Default.GetInstance<DiscoverViewModel>();

        public Coming_SoonViewModel Coming_SoonViewModel => SimpleIoc.Default.GetInstance<Coming_SoonViewModel>();

        public TV_Airing_TodayViewModel TV_Airing_TodayViewModel => SimpleIoc.Default.GetInstance<TV_Airing_TodayViewModel>();

        public SettingsViewModel SettingsViewModel => SimpleIoc.Default.GetInstance<SettingsViewModel>();

        public SearchViewModel SearchViewModel => SimpleIoc.Default.GetInstance<SearchViewModel>();

        public PeopleDetailViewModel Popular_PeopleDetailViewModel => SimpleIoc.Default.GetInstance<PeopleDetailViewModel>();

        public Popular_PeopleViewModel Popular_PeopleViewModel => SimpleIoc.Default.GetInstance<Popular_PeopleViewModel>();

        public TV_on_the_AirViewModel TV_on_the_AirViewModel => SimpleIoc.Default.GetInstance<TV_on_the_AirViewModel>();

        public TV_ShowsDetailViewModel Popular_TV_ShowsDetailViewModel => SimpleIoc.Default.GetInstance<TV_ShowsDetailViewModel>();

        public Popular_TV_ShowsViewModel Popular_TV_ShowsViewModel => SimpleIoc.Default.GetInstance<Popular_TV_ShowsViewModel>();

        public Upcoming_MoviesViewModel Upcoming_MoviesViewModel => SimpleIoc.Default.GetInstance<Upcoming_MoviesViewModel>();

        public Now_Playing_MoviesViewModel Now_Playing_MoviesViewModel => SimpleIoc.Default.GetInstance<Now_Playing_MoviesViewModel>();

        public MoviesDetailViewModel Popular_MoviesDetailViewModel => SimpleIoc.Default.GetInstance<MoviesDetailViewModel>();

        public Popular_MoviesViewModel Popular_MoviesViewModel => SimpleIoc.Default.GetInstance<Popular_MoviesViewModel>();

        public ShellViewModel ShellViewModel => SimpleIoc.Default.GetInstance<ShellViewModel>();

        public NavigationServiceEx NavigationService => SimpleIoc.Default.GetInstance<NavigationServiceEx>();

        public void Register<VM, V>()
            where VM : class
        {
            SimpleIoc.Default.Register<VM>();

            NavigationService.Configure(typeof(VM).FullName, typeof(V));
        }
    }
}
