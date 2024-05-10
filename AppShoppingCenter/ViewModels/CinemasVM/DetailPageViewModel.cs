using AppShoppingCenter.Models;
using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppShoppingCenter.ViewModels.CinemasVM
{
    [QueryProperty(nameof(Movie),"movie")]
    public partial class  DetailPageViewModel : ObservableObject
    {
        [ObservableProperty]
        private Movie movie;

    }
}
