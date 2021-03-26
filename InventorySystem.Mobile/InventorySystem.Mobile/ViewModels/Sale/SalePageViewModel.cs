using InventorySystem.Mobile.Helpers;
using InventorySystem.Mobile.Models;
using InventorySystem.Mobile.Views.Sale;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.CommunityToolkit.ObjectModel;

namespace InventorySystem.Mobile.ViewModels.Sale
{
    public class SalePageViewModel : ObservableBase
    {
        public IAsyncCommand MakeSaleTappedCommand { get; }
        public SalePageViewModel()
        {
            MakeSaleTappedCommand = new AsyncCommand(OnMakeSaleAsync, allowsMultipleExecutions: false);
        }
        private async Task OnMakeSaleAsync()
        {
            await NavigationHelper.PushRgPageAsync(new SaleEntryPage());
        }
    }
}
