using InventorySystem.Contracts.v1._0.Responses;
using InventorySystem.Mobile.Helpers;
using InventorySystem.Mobile.Models;
using InventorySystem.Mobile.Views.Sale;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.CommunityToolkit.ObjectModel;
using System.Linq;
namespace InventorySystem.Mobile.ViewModels.Sale
{
    public class SalePageViewModel : ObservableBase
    {
        public static SalePageViewModel Instance;
        public IAsyncCommand MakeSaleTappedCommand { get; }
        public IAsyncCommand GetAllSalesCommand { get; }
        public IAsyncCommand ReloadTappedCommand { get; }
        public ObservableRangeCollection<SaleModel> Sales { get; set; } = new();
        public SalePageViewModel()
        {
            Instance = this;
            MakeSaleTappedCommand = new AsyncCommand(OnMakeSaleAsync, allowsMultipleExecutions: false);
            GetAllSalesCommand = new AsyncCommand(GetAllSalesAsync, allowsMultipleExecutions: false);
            ReloadTappedCommand = new AsyncCommand(GetAllSalesAsync, allowsMultipleExecutions: false);
        }
        private async Task OnMakeSaleAsync()
        {
            await NavigationHelper.PushRgPageAsync(new SaleEntryPage());
        }
        private async Task GetAllSalesAsync()
        {
            try
            {
                IsBusy = true;
                Sales.Clear();
                var sales = await ApiHelper.GetAsync<Response<SaleResponse>>("/sales");
                Sales.AddRange(sales.Data.Select(sale => new SaleModel
                {
                    Id = sale.Id,
                    CustomerId = sale.CustomerId,
                    Product = sale.Product,
                    PurchaseId = sale.PurchaseId,
                    Quantity = sale.Quantity,
                    TotalPrice = sale.TotalPrice,
                    UnitPrice = sale.UnitPrice,
                }));
            }
            catch { }
            finally
            {
                IsBusy = false;
            }
        }
    }
}
