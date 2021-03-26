using InventorySystem.Contracts.v1._0.Requests.Purchase;
using InventorySystem.Contracts.v1._0.Responses;
using InventorySystem.Mobile.Helpers;
using InventorySystem.Mobile.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.CommunityToolkit.ObjectModel;
using Xamarin.Forms;

namespace InventorySystem.Mobile.ViewModels.Purchase
{
    public class PurchaseEntryPageViewModel : ObservableBase
    {
        StringBuilder sb = new();
        bool success = false;
        private int _id;

        public int Id
        {
            get { return _id; }
            set { _id = value; OnPropertyChanged(nameof(Id)); }
        }

        private int _quantity;

        public int Quantity
        {
            get { return _quantity; }
            set { _quantity = value; OnPropertyChanged(nameof(Quantity)); }
        }
        private decimal _unitPrice;

        public decimal UnitPrice
        {
            get { return _unitPrice; }
            set { _unitPrice = value; OnPropertyChanged(nameof(UnitPrice)); }
        }
        private decimal _totalPrice;

        public decimal TotalPrice
        {
            get { return _totalPrice; }
            set { _totalPrice = value; OnPropertyChanged(nameof(TotalPrice)); }
        }
        private ProductModel _selectedProduct;

        public ProductModel SelectedProduct
        {
            get { return _selectedProduct; }
            set { _selectedProduct = value; OnPropertyChanged(nameof(SelectedProduct)); }
        }
        private SupplierModel _selectedSupplier;

        public SupplierModel SelectedSupplier
        {
            get { return _selectedSupplier; }
            set { _selectedSupplier = value; OnPropertyChanged(nameof(SelectedSupplier)); }
        }
        public ObservableRangeCollection<ProductModel> Products { get; set; } = new();
        public ObservableRangeCollection<SupplierModel> Suppliers { get; set; } = new();
        public IAsyncCommand GetAllProductsCommand { get; }
        public IAsyncCommand GetAllSuppliersCommand { get; }
        public IAsyncCommand<int> GetPurchaseCommand { get; }
        public IAsyncCommand SaveTappedCommand { get; }
        public ICommand QuantityTextChangedCommand { get; }
        public ICommand UnitPriceTextChangedCommand { get; }
        public PurchaseEntryPageViewModel()
        {
            GetAllProductsCommand = new AsyncCommand(GetAllProductsAsync, allowsMultipleExecutions: false);
            GetAllSuppliersCommand = new AsyncCommand(GetAllSuppliersAsync, allowsMultipleExecutions: false);
            SaveTappedCommand = new AsyncCommand(OnSaveAsync, allowsMultipleExecutions: false);
            QuantityTextChangedCommand = new Command<string>((value) => OnQuantityTextChange(value));
            UnitPriceTextChangedCommand = new Command<string>((value) => OnUnitPriceTextChange(value));
            GetPurchaseCommand = new AsyncCommand<int>(async (id) => await GetPurchaseAsync(id), allowsMultipleExecutions: false);
        }
        private void OnQuantityTextChange(string value)
        {
            sb.Clear();
            IsValidationError = false;
            int quantity = 0;
            bool isValidQuantity = Int32.TryParse(value, out quantity);
            if (isValidQuantity)
            {
                Quantity = quantity;
                TotalPrice = UnitPrice * Quantity;
                return;
            }
            IsValidationError = true;
            sb.Append("Please enter valid quantity\n");
            ValidationErrors = sb.ToString();
        }
        private void OnUnitPriceTextChange(string value)
        {
            sb.Clear();
            IsValidationError = false;
            decimal unitPrice = 0;
            bool isValidUnitPrice = decimal.TryParse(value, out unitPrice);
            if (isValidUnitPrice)
            {
                UnitPrice = unitPrice;
                TotalPrice = UnitPrice * Quantity;
                return;
            }
            IsValidationError = true;
            sb.Append("Please enter valid unit price\n");
            ValidationErrors = sb.ToString();
        }
        private async Task GetAllProductsAsync()
        {
            try
            {
                IsBusy = true;
                Products.Clear();
                Products.AddRange(await GetAllProductsAsync(null));
            }
            catch { }
            finally
            {
                IsBusy = false;
            }
        }
        private async Task GetAllSuppliersAsync()
        {
            try
            {
                IsBusy = true;
                Suppliers.Clear();
                Suppliers.AddRange(await GetAllSuppliersAsync(null));
            }
            catch { }
            finally
            {
                IsBusy = false;
            }
        }
        private async Task CreatePurchaseAsync()
        {
            try
            {
                success = await ApiHelper.PostAsync("/purchases", new CreatePurchaseRequest
                {
                    ProductId = SelectedProduct.Id,
                    SupplierId = SelectedSupplier.Id,
                    UnitPrice = UnitPrice,
                    Quantity = Quantity,
                    TotalPrice = TotalPrice,
                });
                if (success)
                {
                    await UserInterfaceHelper.DisplayAlertAsync("Success", "Purchase added successfully");
                    PurchasePageViewModel.Instance.GetAllPurchasesCommand.Execute(null);
                    return;
                }
                await UserInterfaceHelper.DisplayAlertAsync("Error", "Something went wrong");
            }
            catch { }
        }
        private async Task UpdatePurchaseAsync()
        {
            try
            {
                success = await ApiHelper.PutAsync($"/purchases/{Id}", new UpdatePurchaseRequest
                {
                    ProductId = SelectedProduct.Id,
                    SupplierId = SelectedSupplier.Id,
                    UnitPrice = UnitPrice,
                    Quantity = Quantity,
                    TotalPrice = TotalPrice,
                });
                if (success)
                {
                    await UserInterfaceHelper.DisplayAlertAsync("Success", "Purchase updated successfully");
                    return;
                }
                await UserInterfaceHelper.DisplayAlertAsync("Error", "Something went wrong");
            }
            catch { }
        }
        private async Task GetPurchaseAsync(int id)
        {
            try
            {
                var purchase = await ApiHelper.GetAsync<PurchaseResponse>($"/purchases/{id}");
                Id = purchase.Id;
                SelectedProduct = Products.FirstOrDefault(product => product.Id == purchase.ProductId);
                SelectedSupplier = Suppliers.FirstOrDefault(supplier => supplier.Id == purchase.SupplierId);
                Quantity = purchase.Quantity;
                UnitPrice = purchase.UnitPrice;
                TotalPrice = purchase.TotalPrice;
            }
            catch { }
        }
        private async Task OnSaveAsync()
        {
            if (IsFormValid() == false) return;
            try
            {
                if(_id != 0)
                {
                    await UpdatePurchaseAsync();
                }
                else
                {
                    await CreatePurchaseAsync();
                }
            }
            catch { }
            finally
            {
                Id = 0;
                await NavigationHelper.PopAllRgPagesAsync();
                PurchasePageViewModel.Instance.GetAllPurchasesCommand.Execute(null);
            }
        }
        private bool IsFormValid()
        {
            sb.Clear();
            if (SelectedSupplier is null)
            {
                sb.Append("Please select a Supplier\n");
                IsValidationError = true;
                ValidationErrors = sb.ToString();
                return false;
            }
            if (SelectedProduct is null)
            {
                sb.Append("Please select a Product\n");
                IsValidationError = true;
                ValidationErrors = sb.ToString();
                return false;
            }
            return true;
        }
    }
}
