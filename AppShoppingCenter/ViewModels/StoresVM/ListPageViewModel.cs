using AppShoppingCenter.Models;
using AppShoppingCenter.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppShoppingCenter.ViewModels.StoresVM
{
    public partial class ListPageViewModel : ObservableObject
    {
        [ObservableProperty]
        private string textSearch;

        private List<Establishment> establishmentsFull;

        [ObservableProperty]
        private List<Establishment> establishmentsFilted;

        public ListPageViewModel()
        {
            var service = App.Current.Handler.MauiContext.Services.GetService<StoreService>();
            establishmentsFull = service.GetStores();
            establishmentsFilted = establishmentsFull.ToList();
        }

        [RelayCommand]
        private void TextSearchInList()
        {
            

            EstablishmentsFilted = establishmentsFull
                .Where(
                    a=>a.Name.ToLower().Contains(TextSearch.ToLower())
                )
                .ToList();
        }

        [RelayCommand]
        private async void OnTapEstablishmentGoToDetailPage(Establishment establishment)
        {
            var navParameter = new Dictionary<string, object>()
            {
                {"establishment", establishment }
            };


             await Shell.Current.GoToAsync("detail",navParameter);
        }
    }
}
