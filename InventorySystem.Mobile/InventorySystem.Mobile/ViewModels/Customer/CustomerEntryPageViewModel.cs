using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.CommunityToolkit.ObjectModel;
using System.Threading.Tasks;
using InventorySystem.Contracts.v1._0.Responses;
using InventorySystem.Mobile.Helpers;
using InventorySystem.Contracts.v1._0.Requests.Customer;

namespace InventorySystem.Mobile.ViewModels.Customer
{
    public class CustomerEntryPageViewModel : ObservableBase
    {
        StringBuilder sb = new();
        bool success = false;
        private int _id;

        public int Id
        {
            get { return _id; }
            set { _id = value; OnPropertyChanged(nameof(Id)); }
        }
        private string _email;

        private string _name;

        public string Name
        {
            get { return _name; }
            set { _name = value; OnPropertyChanged(nameof(Name)); }
        }
        public string Email
        {
            get { return _email; }
            set { _email = value; OnPropertyChanged(nameof(Email)); }
        }
        private string _phone;

        public string Phone
        {
            get { return _phone; }
            set { _phone = value; OnPropertyChanged(nameof(Phone)); }
        }
        public IAsyncCommand<int> GetCustomerCommand { get; }
        public IAsyncCommand SaveTappedCommand { get; }
        public CustomerEntryPageViewModel()
        {
            SaveTappedCommand = new AsyncCommand(OnSaveAsync, allowsMultipleExecutions: false);
            GetCustomerCommand = new AsyncCommand<int>(async (id) => await GetCustomerAsync(id), allowsMultipleExecutions: false);
        }
        private async Task GetCustomerAsync(int id)
        {
            try
            {
                var customer = await ApiHelper.GetAsync<CustomerResponse>($"/customers/{id}");
                Id = customer.Id;
                Name = customer.Name;
                Email = customer.Email;
                Phone = customer.Phone;
            }
            catch { }
        }
        private async Task UpdateCustomerAsync()
        {
            success = await ApiHelper.PutAsync($"/customers/{Id}", new UpdateCustomerRequest
            {
                Name = Name,
                Email = Email,
                Phone = Phone,
            });
            if (success)
            {
                await UserInterfaceHelper.DisplayAlertAsync("Success", "Customer updated successfully");
                return;
            }
            await UserInterfaceHelper.DisplayAlertAsync("Error", "Something went wrong");
        }
        private async Task CreateCustomerAsync()
        {
            success = await ApiHelper.PostAsync("/customers", new CreateCustomerRequest
            {
                Name = Name,
                Email = Email,
                Phone = Phone,
            });
            if (success)
            {
                await UserInterfaceHelper.DisplayAlertAsync("Success", "Customer added successfully");
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
                    await UpdateCustomerAsync();
                    return;
                }
                await CreateCustomerAsync();
            }
            catch { }
            finally
            {
                Id = 0;
                await NavigationHelper.PopAllRgPagesAsync();
                CustomerPageViewModel.Instance.GetAllCustomersCommand.Execute(null);
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
            if (string.IsNullOrWhiteSpace(Email))
            {
                sb.Append("Email is required\n");
                IsValidationError = true;
                ValidationErrors = sb.ToString();
                return false;
            }
            if (string.IsNullOrWhiteSpace(Phone))
            {
                sb.Append("Phone is required\n");
                IsValidationError = true;
                ValidationErrors = sb.ToString();
                return false;
            }
            return true;
        }
    }
}
