using InventorySystem.Contracts.v1._0.Responses;
using InventorySystem.Mobile.Helpers;
using InventorySystem.Mobile.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.CommunityToolkit.ObjectModel;
using Xamarin.Forms;

namespace InventorySystem.Mobile
{
    public class ObservableBase : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged(string prop = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
        private string _title;

        public string Title
        {
            get { return _title; }
            set { _title = value; OnPropertyChanged(nameof(Title)); }
        }

        private bool _isBusy;

        public bool IsBusy
        {
            get { return _isBusy; }
            set { _isBusy = value; OnPropertyChanged(nameof(IsBusy)); }
        }
        private bool _isValidationError;

        public bool IsValidationError
        {
            get { return _isValidationError; }
            set { _isValidationError = value; OnPropertyChanged(nameof(IsValidationError)); }
        }
        private string _validationErrors;

        public string ValidationErrors
        {
            get { return _validationErrors; }
            set { _validationErrors = value; OnPropertyChanged(nameof(ValidationErrors)); }
        }
        public ICommand InputChangedCommand => new Command(() => IsValidationError = false);

        //these methods can be moved to service classes if needed
        protected async Task<ObservableRangeCollection<ProductModel>> GetAllProductsAsync(string query = null)
        {
            ObservableRangeCollection<ProductModel> result = new();
            Response<ProductResponse> products;
            try
            {
                if (string.IsNullOrWhiteSpace(query))
                {
                    products = await ApiHelper.GetAsync<Response<ProductResponse>>("/products");
                    result.AddRange(products.Data.Select(product => new ProductModel
                    {
                        Id = product.Id,
                        Name = product.Name,
                        Code = product.Code,
                    }));
                }
                else
                {
                    products = await ApiHelper.GetAsync<Response<ProductResponse>>($"/products/search?query={query}");
                    result.AddRange(products.Data.Select(product => new ProductModel
                    {
                        Id = product.Id,
                        Name = product.Name,
                        Code = product.Code,
                    }));
                }
            }
            catch { }
            return result;
        }
        protected async Task<ObservableRangeCollection<SupplierModel>> GetAllSuppliersAsync(string query = null)
        {
            ObservableRangeCollection<SupplierModel> result = new();
            Response<SupplierResponse> suppliers;
            try
            {
                if (string.IsNullOrWhiteSpace(query))
                {
                    suppliers = await ApiHelper.GetAsync<Response<SupplierResponse>>("/suppliers");
                    result.AddRange(suppliers.Data.Select(supplier => new SupplierModel
                    {
                        Id = supplier.Id,
                        Name = supplier.Name,
                        Email = supplier.Email,
                        Phone = supplier.Phone,
                    }));
                }
                else
                {
                    suppliers = await ApiHelper.GetAsync<Response<SupplierResponse>>($"/suppliers/search?query={query}");
                    result.AddRange(suppliers.Data.Select(supplier => new SupplierModel
                    {
                        Id = supplier.Id,
                        Name = supplier.Name,
                        Email = supplier.Email,
                        Phone = supplier.Phone,
                    }));
                }
            }
            catch { }
            return result;
        }
        protected async Task<ObservableRangeCollection<CategoryModel>> GetAllCategoriesAsync(string query = null)
        {
            ObservableRangeCollection<CategoryModel> result = new();
            Response<CategoryResponse> categories;
            try
            {
                if (string.IsNullOrEmpty(query))
                {
                    categories = await ApiHelper.GetAsync<Response<CategoryResponse>>("/categories");
                    result.AddRange(categories.Data.Select(category => new CategoryModel
                    {
                        Id = category.Id,
                        Name = category.Name,
                    }));
                }
                else
                {
                    categories = await ApiHelper.GetAsync<Response<CategoryResponse>>($"/categories/search?query={query}");
                    result.AddRange(categories.Data.Select(category => new CategoryModel
                    {
                        Id = category.Id,
                        Name = category.Name,
                    }));
                }
            }
            catch { }
            return result;
        }
        protected async Task<ObservableRangeCollection<CustomerModel>> GetAllCustomersAsync(string query = null)
        {
            ObservableRangeCollection<CustomerModel> result = new();
            Response<CustomerResponse> customers;
            try
            {
                if (string.IsNullOrWhiteSpace(query))
                {
                    customers = await ApiHelper.GetAsync<Response<CustomerResponse>>("/customers");
                    result.AddRange(customers.Data.Select(customer => new CustomerModel
                    {
                        Id = customer.Id,
                        Name = customer.Name,
                        Email = customer.Email,
                        Phone = customer.Phone,
                    }));
                }
                else
                {
                    customers = await ApiHelper.GetAsync<Response<CustomerResponse>>($"/customers/search?query={query}");
                    result.AddRange(customers.Data.Select(customer => new CustomerModel
                    {
                        Id = customer.Id,
                        Name = customer.Name,
                        Email = customer.Email,
                        Phone = customer.Phone,
                    }));
                }
            }
            catch { }
            return result;
        }
        protected async Task<ObservableRangeCollection<PurchaseModel>> GetAllPurchasesAsync(string query = null)
        {
            Response<PurchaseResponse> purchases;
            ObservableRangeCollection<PurchaseModel> result = new();
            try
            {
                purchases = await ApiHelper.GetAsync<Response<PurchaseResponse>>("/purchases");
                result.AddRange(purchases.Data.Select(purchase => new PurchaseModel
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
            return result;
        }
    }
}
