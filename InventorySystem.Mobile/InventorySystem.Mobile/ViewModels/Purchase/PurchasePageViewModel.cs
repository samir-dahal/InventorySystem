using InventorySystem.Contracts.v1._0.Responses;
using InventorySystem.Mobile.Helpers;
using InventorySystem.Mobile.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.CommunityToolkit.ObjectModel;
using System.Linq;
using InventorySystem.Mobile.Views.Purchase;

namespace InventorySystem.Mobile.ViewModels.Purchase
{
    public class PurchasePageViewModel : ObservableBase
    {
        public static PurchasePageViewModel Instance;
        public ObservableRangeCollection<PurchaseModel> Purchases { get; set; } = new();
        public IAsyncCommand GetAllPurchasesCommand { get; }
        public IAsyncCommand MakePurchaseTappedCommand { get; }
        public IAsyncCommand ReloadTappedCommand { get; }
        public IAsyncCommand<int> DeleteTappedCommand { get; }
        public IAsyncCommand<int> EditTappedCommand { get; }
        public PurchasePageViewModel()
        {
            Instance = this;
            GetAllPurchasesCommand = new AsyncCommand(GetAllPurchasesAsync, allowsMultipleExecutions: false);
            ReloadTappedCommand = new AsyncCommand(GetAllPurchasesAsync, allowsMultipleExecutions: false);
            MakePurchaseTappedCommand = new AsyncCommand(OnMakePurchaseAsync, allowsMultipleExecutions: false);
            EditTappedCommand = new AsyncCommand<int>(async (id) => await OnEditAsync(id), allowsMultipleExecutions: false);
            DeleteTappedCommand = new AsyncCommand<int>(async (id) => await OnDeleteAsync(id), allowsMultipleExecutions: false);
        }
        private async Task OnMakePurchaseAsync()
        {
            await NavigationHelper.PushRgPageAsync(new PurchaseEntryPage());
        }
        private async Task OnEditAsync(int id)
        {
            await NavigationHelper.PushRgPageAsync(new PurchaseEntryPage(id));
        }
        private async Task OnDeleteAsync(int id)
        {
            if (await UserInterfaceHelper.ConfirmationAlertAsync("Warning", "Are you sure you want to delete this purchase?") is false) return;
            try
            {
                bool success = await ApiHelper.DeleteAsync($"/purchases/{id}");
                if (success)
                {
                    Purchases.Remove(Purchases.FirstOrDefault(purchase => purchase.Id == id));
                    await UserInterfaceHelper.DisplayAlertAsync("Success", "Purchase successfully deleted");
                    return;
                }
                await UserInterfaceHelper.DisplayAlertAsync("Error", "Something went wrong");
            }
            catch { }
        }
        private async Task GetAllPurchasesAsync()
        {
            try
            {
                IsBusy = true;
                Purchases.Clear();
                var purchases = await ApiHelper.GetAsync<Response<PurchaseResponse>>("/purchases");
                Purchases.AddRange(purchases.Data.Select(purchase => new PurchaseModel
                {
                    Id = purchase.Id,
                    ProductId = purchase.ProductId,
                    Quantity = purchase.Quantity,
                    Product = purchase.Product,
                    UnitPrice = purchase.UnitPrice,
                    TotalPrice = purchase.TotalPrice,
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
