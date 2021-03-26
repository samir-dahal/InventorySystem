using InventorySystem.Contracts.v1._0.Requests.Sale;
using InventorySystem.Mobile.Helpers;
using InventorySystem.Mobile.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.CommunityToolkit.ObjectModel;
using Xamarin.Forms;

namespace InventorySystem.Mobile.ViewModels.Sale
{
    public class SaleEntryPageViewModel : ObservableBase
    {
        StringBuilder sb = new();
        bool success = false;
        private int _id;

        public int Id
        {
            get { return _id; }
            set { _id = value; OnPropertyChanged(nameof(Id)); }
        }

        private CustomerModel _selectedCustomer;

        public CustomerModel SelectedCustomer
        {
            get { return _selectedCustomer; }
            set { _selectedCustomer = value; OnPropertyChanged(nameof(SelectedCustomer)); }
        }
        private PurchaseModel _selectedPurchase;

        public PurchaseModel SelectedPurchase
        {
            get { return _selectedPurchase; }
            set { _selectedPurchase = value; OnPropertyChanged(nameof(SelectedPurchase)); }
        }
        private int _quantity;

        public int Quantity
        {
            get { return _quantity; }
            set { _quantity = value; OnPropertyChanged(nameof(Quantity)); }
        }
        public ObservableRangeCollection<CustomerModel> Customers { get; set; } = new();
        public ObservableRangeCollection<PurchaseModel> Purchases { get; set; } = new();
        public IAsyncCommand GetAllCustomersCommand { get; }
        public IAsyncCommand GetAllPurchasesCommand { get; }
        public IAsyncCommand SaveTappedCommand { get; }
        public ICommand QuantityTextChangedCommand { get; }
        public SaleEntryPageViewModel()
        {
            GetAllCustomersCommand = new AsyncCommand(GetAllCustomersAsync, allowsMultipleExecutions: false);
            GetAllPurchasesCommand = new AsyncCommand(GetAllPurchasesAsync, allowsMultipleExecutions: false);
            SaveTappedCommand = new AsyncCommand(OnSaveAsync, allowsMultipleExecutions: false);
            QuantityTextChangedCommand = new Command<string>((value) => OnQuantityTextChange(value));
        }
        private void OnQuantityTextChange(string value)
        {
            sb.Clear();
            IsValidationError = false;
            int quantity = 0;
            bool isValidQuantity = Int32.TryParse(value, out quantity);
            if (isValidQuantity is false)
            {
                IsValidationError = true;
                sb.Append("Please enter valid quantity");
                ValidationErrors = sb.ToString();
                return;
            }
            if (quantity > SelectedPurchase?.Quantity || quantity < 0)
            {
                IsValidationError = true;
                sb.Append("Quantity must be less than or equal to purchased product quantity");
                ValidationErrors = sb.ToString();
                return;
            }
            Quantity = quantity;
        }
        private async Task CreateSaleAsync()
        {
            try
            {
                success = await ApiHelper.PostAsync("/sales", new CreateSaleRequest
                {
                    CustomerId = SelectedCustomer.Id,
                    PurchaseId = SelectedPurchase.Id,
                    Quantity = Quantity,
                    UnitPrice = SelectedPurchase.UnitPrice,
                    TotalPrice = SelectedPurchase.UnitPrice * Quantity,
                });
                if (success)
                {
                    await UserInterfaceHelper.DisplayAlertAsync("Success", "Sale successfully created");
                    return;
                }
                await UserInterfaceHelper.DisplayAlertAsync("Error", "Something went wrong");
            }
            catch { }
        }
        private async Task OnSaveAsync()
        {
            if (IsFormValid() == false) return;
            try
            {
                await CreateSaleAsync();
            }
            catch { }
            finally
            {
                Id = 0;
                await NavigationHelper.PopAllRgPagesAsync();
            }
        }
        private async Task GetAllCustomersAsync()
        {
            try
            {
                Customers.AddRange(await GetAllCustomersAsync(null));
            }
            catch { }
        }
        private async Task GetAllPurchasesAsync()
        {
            try
            {
                Purchases.AddRange(await GetAllPurchasesAsync(null));
            }
            catch { }
        }
        private bool IsFormValid()
        {
            sb.Clear();
            if(SelectedCustomer is null)
            {
                sb.Append("Please select a Customer\n");
                IsValidationError = true;
                ValidationErrors = sb.ToString();
                return false;
            }
            if (SelectedPurchase is null)
            {
                sb.Append("Please select a Purchase\n");
                IsValidationError = true;
                ValidationErrors = sb.ToString();
                return false;
            }
            return true;
        }
    }
}
