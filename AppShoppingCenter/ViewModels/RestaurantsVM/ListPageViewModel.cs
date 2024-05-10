using AppShoppingCenter.Models;
using AppShoppingCenter.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppShoppingCenter.ViewModels.RestaurantsVM
{
    public partial class ListPageViewModel : ObservableObject
    {

        [ObservableProperty]
        private string textSearch;


        private List<Establishment> establishmentfull;

        [ObservableProperty]
        private List<Establishment> establishmentsfilted;

        public ListPageViewModel()
        {
            var service = App.Current.Handler.MauiContext.Services.GetService<RestaurantService>();
            establishmentfull = service.GetRestaurants();
            establishmentsfilted = establishmentfull.ToList();
        }


        [RelayCommand]
        private void TextSearchChanged()
        {
            Establishmentsfilted = establishmentfull
                .Where(
                a=>a.Name.ToLower().Contains(TextSearch.ToLower())
                )
                .ToList();
        }

        [RelayCommand]
        private async void OnTapToDetailPage(Establishment establishment)
        {
            var navParameter = new Dictionary<string, object>()
            {
                {"establishment", establishment }
            };


            await Shell.Current.GoToAsync("detail", navParameter);
        }
    }
}
