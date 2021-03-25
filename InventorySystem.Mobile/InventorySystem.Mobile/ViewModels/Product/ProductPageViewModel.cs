using System;
using System.Collections.Generic;
using System.Text;
using InventorySystem.Mobile.Helpers;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.CommunityToolkit.ObjectModel;
using System.Threading.Tasks;
using InventorySystem.Mobile.Views.Product;
using InventorySystem.Contracts.v1._0.Responses;
using System.Linq;
using InventorySystem.Mobile.Models;

namespace InventorySystem.Mobile.ViewModels.Product
{
    public class ProductPageViewModel : ObservableBase
    {
        public static ProductPageViewModel Instance;
        public ObservableRangeCollection<ProductModel> Products { get; set; } = new();
        public IAsyncCommand GetAllProductsCommand { get; }
        public IAsyncCommand AddTappedCommand { get; }
        public IAsyncCommand ReloadTappedCommand { get; }
        public IAsyncCommand<int> DeleteTappedCommand { get; }
        public IAsyncCommand<int> EditTappedCommand { get; }
        public IAsyncCommand<string> SearchCommand { get; }
        public ProductPageViewModel()
        {
            Instance = this;
            GetAllProductsCommand = new AsyncCommand(GetAllProductsAsync, allowsMultipleExecutions: false);
            AddTappedCommand = new AsyncCommand(OnAddTappedAsync, allowsMultipleExecutions: false);
            ReloadTappedCommand = new AsyncCommand(GetAllProductsAsync, allowsMultipleExecutions: false);
            DeleteTappedCommand = new AsyncCommand<int>(async (id) => await OnDeleteAsync(id), allowsMultipleExecutions: false);
            EditTappedCommand = new AsyncCommand<int>(async (id) => await OnEditAsync(id), allowsMultipleExecutions: false);
            SearchCommand = new AsyncCommand<string>(async (query) => await GetAllProductsBySearchQueryAsync(query), allowsMultipleExecutions: false);
        }
        private async Task OnAddTappedAsync()
        {
            await NavigationHelper.PushRgPageAsync(new ProductEntryPage());
        }
        private async Task OnDeleteAsync(int id)
        {
            if (await UserInterfaceHelper.ConfirmationAlertAsync("Warning", "Are you sure you want to delete this product?") is false) return;
            try
            {
                bool success = await ApiHelper.DeleteAsync($"/products/{id}");
                if (success)
                {
                    Products.Remove(Products.FirstOrDefault(product => product.Id == id));
                    await UserInterfaceHelper.DisplayAlertAsync("Success", "Product successfully deleted");
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
            await NavigationHelper.PushRgPageAsync(new ProductEntryPage(id));
        }
        private async Task GetAllProductsAsync()
        {
            try
            {
                IsBusy = true;
                Products.Clear();
                var products = await ApiHelper.GetAsync<Response<ProductResponse>>("/products");
                Products.AddRange(products.Data.Select(product => new ProductModel
                {
                    Id = product.Id,
                    Name = product.Name,
                    Code = product.Code,
                }));
            }
            catch { }
            finally
            {
                IsBusy = false;
            }
        }
        private async Task GetAllProductsBySearchQueryAsync(string query)
        {
            try
            {
                IsBusy = true;
                Products.Clear();
                var products = await ApiHelper.GetAsync<Response<ProductResponse>>($"/products/search?query={query}");
                Products.AddRange(products.Data.Select(product => new ProductModel
                {
                    Id = product.Id,
                    Name = product.Name,
                    Code = product.Code,
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
