using InventorySystem.Contracts.v1._0.Requests.Product;
using InventorySystem.Contracts.v1._0.Responses;
using InventorySystem.Mobile.Helpers;
using InventorySystem.Mobile.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.CommunityToolkit.ObjectModel;

namespace InventorySystem.Mobile.ViewModels.Product
{
    public class ProductEntryPageViewModel : ObservableBase
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
        private string _code;

        public string Code
        {
            get { return _code; }
            set { _code = value; OnPropertyChanged(nameof(Code)); }
        }
        private CategoryModel _selectedCategory;

        public CategoryModel SelectedCategory
        {
            get { return _selectedCategory; }
            set { _selectedCategory = value; OnPropertyChanged(nameof(SelectedCategory)); }
        }
        public IAsyncCommand GetAllCategoriesCommand { get; }
        public IAsyncCommand SaveTappedCommand { get; }
        public IAsyncCommand<int> GetProductCommand { get; }
        public ObservableRangeCollection<CategoryModel> Categories { get; set; } = new();
        public ProductEntryPageViewModel()
        {
            GetAllCategoriesCommand = new AsyncCommand(GetAllCategoriesAsync, allowsMultipleExecutions: false);
            SaveTappedCommand = new AsyncCommand(OnSaveAsync, allowsMultipleExecutions: false);
            GetProductCommand = new AsyncCommand<int>(async (id) => await GetProductAsync(id), allowsMultipleExecutions: false);
        }
        private async Task GetAllCategoriesAsync()
        {
            try
            {
                Categories.Clear();
                var categories = await ApiHelper.GetAsync<Response<CategoryResponse>>("/categories");
                Categories.AddRange(categories.Data.Select(category => new CategoryModel
                {
                    Id = category.Id,
                    Name = category.Name,
                }));
            }
            catch { }
        }
        private async Task GetProductAsync(int id)
        {
            try
            {
                var product = await ApiHelper.GetAsync<ProductResponse>($"/products/{id}");
                Id = product.Id;
                Name = product.Name;
                Code = product.Code;
                SelectedCategory = Categories.FirstOrDefault(category => category.Id == product.CategoryId);
            }
            catch { }
        }
        private async Task UpdateProductAsync()
        {
            success = await ApiHelper.PutAsync($"/products/{Id}", new UpdateProductRequest
            {
                CategoryId = SelectedCategory.Id,
                Code = Code,
                Name = Name,
            });
            if (success)
            {
                await UserInterfaceHelper.DisplayAlertAsync("Success", "Product updated successfully");
                return;
            }
            await UserInterfaceHelper.DisplayAlertAsync("Error", "Something went wrong");
        }
        private async Task CreateProductAsync()
        {
            success = await ApiHelper.PostAsync("/products", new CreateProductRequest
            {
                Name = Name,
                CategoryId = SelectedCategory.Id,
                Code = Code,
            });
            if (success)
            {
                await UserInterfaceHelper.DisplayAlertAsync("Success", "Product added successfully");
                return;
            }
            await UserInterfaceHelper.DisplayAlertAsync("Error", "Something went wrong");
        }
        private async Task OnSaveAsync()
        {
            if (IsFormValid() == false) return;
            try
            {
                if (Id != 0)
                {
                    await UpdateProductAsync();
                    return;
                }
                await CreateProductAsync();
            }
            catch { }
            finally
            {
                Id = 0;
                await NavigationHelper.PopAllRgPagesAsync();
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
            if (string.IsNullOrWhiteSpace(Code))
            {
                sb.Append("Code is required\n");
                IsValidationError = true;
                ValidationErrors = sb.ToString();
                return false;
            }
            if (SelectedCategory is null)
            {
                sb.Append("Please select a Category\n");
                IsValidationError = true;
                ValidationErrors = sb.ToString();
                return false;
            }
            return true;
        }
    }
}
