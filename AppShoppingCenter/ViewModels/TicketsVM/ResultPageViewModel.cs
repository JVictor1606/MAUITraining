﻿using AppShoppingCenter.Models;
using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppShoppingCenter.ViewModels.TicketsVM
{
    [QueryProperty(nameof(Ticket),"ticket")]
    public  partial class ResultPageViewModel : ObservableObject
    {
        [ObservableProperty]
        private Ticket ticket;

        [ObservableProperty]
        private int tolerance = 30;
    }
}
