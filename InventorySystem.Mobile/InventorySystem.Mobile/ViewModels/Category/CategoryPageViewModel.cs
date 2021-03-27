using InventorySystem.Contracts.v1._0.Responses;
using InventorySystem.Mobile.Helpers;
using InventorySystem.Mobile.Models;
using InventorySystem.Mobile.Views.Category;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.CommunityToolkit.ObjectModel;

namespace InventorySystem.Mobile.ViewModels.Category
{
    public class CategoryPageViewModel : ObservableBase
    {
        public static CategoryPageViewModel Instance;
        public ObservableRangeCollection<CategoryModel> Categories { get; set; } = new();
        public IAsyncCommand GetAllCategoriesCommand { get; }
        public IAsyncCommand AddTappedCommand { get; }
        public IAsyncCommand ReloadTappedCommand { get; }
        public IAsyncCommand<int> DeleteTappedCommand { get; }
        public IAsyncCommand<int> EditTappedCommand { get; }
        public IAsyncCommand<string> SearchCommand { get; }
        public CategoryPageViewModel()
        {
            Instance = this;
            GetAllCategoriesCommand = new AsyncCommand(GetAllCategoriesAsync, allowsMultipleExecutions: false);
            AddTappedCommand = new AsyncCommand(OnAddTappedAsync, allowsMultipleExecutions: false);
            ReloadTappedCommand = new AsyncCommand(GetAllCategoriesAsync, allowsMultipleExecutions: false);
            DeleteTappedCommand = new AsyncCommand<int>(async (id) => await OnDeleteAsync(id), allowsMultipleExecutions: false);
            EditTappedCommand = new AsyncCommand<int>(async (id) => await OnEditAsync(id), allowsMultipleExecutions: false);
            SearchCommand = new AsyncCommand<string>(async (query) => await GetAllCategoriesBySearchQueryAsync(query), allowsMultipleExecutions: false);
        }
        private async Task OnAddTappedAsync()
        {
            await NavigationHelper.PushRgPageAsync(new CategoryEntryPage());
        }
        private async Task OnDeleteAsync(int id)
        {
            if (await UserInterfaceHelper.ConfirmationAlertAsync("Warning", "Are you sure you want to delete this category?") is false) return;
            try
            {
                bool success = await ApiHelper.DeleteAsync($"/categories/{id}");
                if (success)
                {
                    Categories.Remove(Categories.FirstOrDefault(category => category.Id == id));
                    await UserInterfaceHelper.DisplayAlertAsync("Success", "Category successfully deleted");
                }
                else
                {
                    await UserInterfaceHelper.DisplayAlertAsync("Error", "Could not delete this item");
                }
            }
            catch { }
        }

        private async Task OnEditAsync(int id)
        {
            await NavigationHelper.PushRgPageAsync(new CategoryEntryPage(id));
        }
        private async Task GetAllCategoriesAsync()
        {
            try
            {
                IsBusy = true;
                Categories.Clear();
                Categories.AddRange(await GetAllCategoriesAsync(null));
            }
            catch { }
            finally
            {
                IsBusy = false;
            }
        }
        private async Task GetAllCategoriesBySearchQueryAsync(string query)
        {
            try
            {
                IsBusy = true;
                Categories.Clear();
                Categories.AddRange(await GetAllCategoriesAsync(query));
            }
            catch { }
            finally
            {
                IsBusy = false;
            }
        }
    }
}
