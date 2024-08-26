using AppShoppingCenter.Libraries.Storages;
using AppShoppingCenter.Models;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Maui.Animations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppShoppingCenter.ViewModels.TicketsVM
{
    [QueryProperty(nameof(Ticket), "ticket")]
    public partial class PayPageViewModel : ObservableObject
    {

        private Ticket ticket;

        public Ticket Ticket
        {
            get { return ticket; }
            set {
                GenrateDateOut(value);
                GeneratePrice(value);
                SetProperty(ref ticket, value); }
        }

        [ObservableProperty]
        private string pixcode = "00020126360014BR.GOV.BCB.PIX0114+5561999...";

        [RelayCommand]
        private async Task CopyAndPaste()
        {
           await Clipboard.Default.SetTextAsync(Pixcode);

            await Task.Delay(30000);
            var storage = App.Current.Handler.MauiContext.Services.GetService<TicketPreferenceStorage>();

            storage.Save(Ticket);

            var param = new Dictionary<string, object> {
                {"ticket", Ticket }
            };

            await Shell.Current.GoToAsync("../result", param);

           
        }
        private void GenrateDateOut(Ticket ticket) 
        {
            var rd = new Random();
            var hour = rd.Next(0,12);
            var minute = rd.Next(0, 60);


            ticket.DateOut = ticket.DateIn.AddHours(hour).AddMinutes(minute);
            ticket.DateTolerance = ticket.DateOut.AddMinutes(30);
            
        }
        private double HourValue = 0.10;
        private void GeneratePrice(Ticket ticket)
        {
            var dif = (ticket.DateOut.Ticks - ticket.DateIn.Ticks);
            var difTS = new TimeSpan(dif);


            ticket.Price = difTS.TotalMinutes* HourValue;
        }

    }
}
