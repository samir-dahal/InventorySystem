using InventorySystem.Contracts.v1._0.Responses;
using InventorySystem.Mobile.Helpers;
using InventorySystem.Mobile.Models;
using InventorySystem.Mobile.Views.Supplier;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.CommunityToolkit.ObjectModel;

namespace InventorySystem.Mobile.ViewModels.Supplier
{
    public class SupplierPageViewModel : ObservableBase
    {
        public static SupplierPageViewModel Instance;
        public ObservableRangeCollection<SupplierModel> Suppliers { get; set; } = new();
        public IAsyncCommand GetAllSuppliersCommand { get; }
        public IAsyncCommand AddTappedCommand { get; }
        public IAsyncCommand ReloadTappedCommand { get; }
        public IAsyncCommand<int> DeleteTappedCommand { get; }
        public IAsyncCommand<int> EditTappedCommand { get; }
        public IAsyncCommand<string> SearchCommand { get; }
        public SupplierPageViewModel()
        {
            Instance = this;
            GetAllSuppliersCommand = new AsyncCommand(GetAllSuppliersAsync, allowsMultipleExecutions: false);
            AddTappedCommand = new AsyncCommand(OnAddTappedAsync, allowsMultipleExecutions: false);
            ReloadTappedCommand = new AsyncCommand(GetAllSuppliersAsync, allowsMultipleExecutions: false);
            DeleteTappedCommand = new AsyncCommand<int>(async (id) => await OnDeleteAsync(id), allowsMultipleExecutions: false);
            EditTappedCommand = new AsyncCommand<int>(async (id) => await OnEditAsync(id), allowsMultipleExecutions: false);
            SearchCommand = new AsyncCommand<string>(async (query) => await GetAllSuppliersBySearchQueryAsync(query), allowsMultipleExecutions: false);
        }
        private async Task OnAddTappedAsync()
        {
            await NavigationHelper.PushRgPageAsync(new SupplierEntryPage());
        }
        private async Task OnDeleteAsync(int id)
        {
            if (await UserInterfaceHelper.ConfirmationAlertAsync("Warning", "Are you sure you want to delete this supplier?") is false) return;
            try
            {
                bool success = await ApiHelper.DeleteAsync($"/suppliers/{id}");
                if (success)
                {
                    Suppliers.Remove(Suppliers.FirstOrDefault(supplier => supplier.Id == id));
                    await UserInterfaceHelper.DisplayAlertAsync("Success", "Supplier successfully deleted");
                }
                else
                {
                    await UserInterfaceHelper.DisplayAlertAsync("Error", "Something went wrong");
                }
            }
            catch { }
        }

        private async Task OnEditAsync(int id)
        {
            await NavigationHelper.PushRgPageAsync(new SupplierEntryPage(id));
        }
        private async Task GetAllSuppliersAsync()
        {
            try
            {
                IsBusy = true;
                Suppliers.Clear();
                var suppliers = await ApiHelper.GetAsync<Response<SupplierResponse>>("/suppliers");
                Suppliers.AddRange(suppliers.Data.Select(supplier => new SupplierModel
                {
                    Id = supplier.Id,
                    Name = supplier.Name,
                    Email = supplier.Email,
                    Phone = supplier.Phone,
                }));
            }
            catch { }
            finally
            {
                IsBusy = false;
            }
        }
        private async Task GetAllSuppliersBySearchQueryAsync(string query)
        {
            try
            {
                IsBusy = true;
                Suppliers.Clear();
                var suppliers = await ApiHelper.GetAsync<Response<SupplierResponse>>($"/suppliers/search?query={query}");
                Suppliers.AddRange(suppliers.Data.Select(supplier => new SupplierModel
                {
                    Id = supplier.Id,
                    Name = supplier.Name,
                    Email = supplier.Email,
                    Phone = supplier.Phone,
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
