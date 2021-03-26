using InventorySystem.Contracts.v1._0.Requests.Category;
using InventorySystem.Contracts.v1._0.Responses;
using InventorySystem.Mobile.Helpers;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.CommunityToolkit.ObjectModel;

namespace InventorySystem.Mobile.ViewModels.Category
{
    public class CategoryEntryPageViewModel : ObservableBase
    {
        StringBuilder sb = new();
        bool success = false;
        private int _id;

        public int Id
        {
            get { return _id; }
            set { _id = value; OnPropertyChanged(nameof(Id)); }
        }

        private string _name;

        public string Name
        {
            get { return _name; }
            set { _name = value; OnPropertyChanged(nameof(Name)); }
        }
        public IAsyncCommand SaveTappedCommand { get; }
        public IAsyncCommand<int> GetCategoryCommand { get; }
        public CategoryEntryPageViewModel()
        {
            GetCategoryCommand = new AsyncCommand<int>(async (id) => await GetCategoryAsync(id), allowsMultipleExecutions: false);
            SaveTappedCommand = new AsyncCommand(OnSaveAsync, allowsMultipleExecutions: false);
        }
        private async Task GetCategoryAsync(int id)
        {
            try
            {
                var category = await ApiHelper.GetAsync<CategoryResponse>($"/categories/{id}");
                Id = category.Id;
                Name = category.Name;
            }
            catch { }
        }
        private async Task UpdateCategoryAsync()
        {
            success = await ApiHelper.PutAsync($"/categories/{Id}", new UpdateCategoryRequest
            {
                Name = Name,
            });
            if (success)
            {
                await UserInterfaceHelper.DisplayAlertAsync("Success", "Category updated successfully");
                return;
            }
            await UserInterfaceHelper.DisplayAlertAsync("Error", "Something went wrong");
        }
        private async Task CreateCategoryAsync()
        {
            success = await ApiHelper.PostAsync("/categories", new CreateCategoryRequest
            {
                Name = Name,
            });
            if (success)
            {
                await UserInterfaceHelper.DisplayAlertAsync("Success", "Category added successfully");
                return;
            }
            await UserInterfaceHelper.DisplayAlertAsync("Error", "Something went wrong");
        }
        private async Task OnSaveAsync()
        {
            if (IsFormValid() == false) return;
            try
            {
                if (Id is not 0)
                {
                    await UpdateCategoryAsync();
                    return;
                }
                await CreateCategoryAsync();
            }
            catch { }
            finally
            {
                Id = 0;
                await NavigationHelper.PopAllRgPagesAsync();
                CategoryPageViewModel.Instance.GetAllCategoriesCommand.Execute(null);
            }
        }
        private bool IsFormValid()
        {
            sb.Clear();
            if (string.IsNullOrWhiteSpace(Name))
            {
                sb.Append("Name is required\n");
                IsValidationError = true;
                ValidationErrors = sb.ToString();
                return false;
            }
            return true;
        }
    }
}
