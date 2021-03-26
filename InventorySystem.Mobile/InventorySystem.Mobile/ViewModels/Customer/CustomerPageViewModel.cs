using InventorySystem.Contracts.v1._0.Responses;
using InventorySystem.Mobile.Helpers;
using InventorySystem.Mobile.Models;
using InventorySystem.Mobile.Views.Customer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.CommunityToolkit.ObjectModel;

namespace InventorySystem.Mobile.ViewModels.Customer
{
    public class CustomerPageViewModel : ObservableBase
    {
        public static CustomerPageViewModel Instance;
        public ObservableRangeCollection<CustomerModel> Customers { get; set; } = new();
        public IAsyncCommand GetAllCustomersCommand { get; }
        public IAsyncCommand AddTappedCommand { get; }
        public IAsyncCommand ReloadTappedCommand { get; }
        public IAsyncCommand<int> DeleteTappedCommand { get; }
        public IAsyncCommand<int> EditTappedCommand { get; }
        public IAsyncCommand<string> SearchCommand { get; }
        public CustomerPageViewModel()
        {
            Instance = this;
            GetAllCustomersCommand = new AsyncCommand(GetAllCustomersAsync, allowsMultipleExecutions: false);
            AddTappedCommand = new AsyncCommand(OnAddTappedAsync, allowsMultipleExecutions: false);
            ReloadTappedCommand = new AsyncCommand(GetAllCustomersAsync, allowsMultipleExecutions: false);
            DeleteTappedCommand = new AsyncCommand<int>(async (id) => await OnDeleteAsync(id), allowsMultipleExecutions: false);
            EditTappedCommand = new AsyncCommand<int>(async (id) => await OnEditAsync(id), allowsMultipleExecutions: false);
            SearchCommand = new AsyncCommand<string>(async (query) => await GetAllCustomersBySearchQueryAsync(query), allowsMultipleExecutions: false);
        }
        private async Task OnAddTappedAsync()
        {
            await NavigationHelper.PushRgPageAsync(new CustomerEntryPage());
        }
        private async Task OnDeleteAsync(int id)
        {
            if (await UserInterfaceHelper.ConfirmationAlertAsync("Warning", "Are you sure you want to delete this customer?") is false) return;
            try
            {
                bool success = await ApiHelper.DeleteAsync($"/customers/{id}");
                if (success)
                {
                    Customers.Remove(Customers.FirstOrDefault(customer => customer.Id == id));
                    await UserInterfaceHelper.DisplayAlertAsync("Success", "Customer successfully deleted");
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
            await NavigationHelper.PushRgPageAsync(new CustomerEntryPage(id));
        }
        private async Task GetAllCustomersAsync()
        {
            try
            {
                IsBusy = true;
                Customers.Clear();
                Customers.AddRange(await GetAllCustomersAsync(null));
            }
            catch { }
            finally
            {
                IsBusy = false;
            }
        }
        private async Task GetAllCustomersBySearchQueryAsync(string query)
        {
            try
            {
                IsBusy = true;
                Customers.Clear();
                Customers.AddRange(await GetAllCustomersAsync(query));
            }
            catch { }
            finally
            {
                IsBusy = false;
            }
        }
    }
}
