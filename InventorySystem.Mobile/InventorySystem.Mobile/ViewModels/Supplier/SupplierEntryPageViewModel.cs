using InventorySystem.Contracts.v1._0.Requests.Supplier;
using InventorySystem.Contracts.v1._0.Responses;
using InventorySystem.Mobile.Helpers;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.CommunityToolkit.ObjectModel;

namespace InventorySystem.Mobile.ViewModels.Supplier
{
    public class SupplierEntryPageViewModel : ObservableBase
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
        private string _email;

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

        public IAsyncCommand SaveTappedCommand { get; }
        public IAsyncCommand<int> GetSupplierCommand { get; }
        public SupplierEntryPageViewModel()
        {
            SaveTappedCommand = new AsyncCommand(OnSaveAsync, allowsMultipleExecutions: false);
            GetSupplierCommand = new AsyncCommand<int>(async (id) => await GetSupplierAsync(id), allowsMultipleExecutions: false);
        }
        private async Task GetSupplierAsync(int id)
        {
            try
            {
                var supplier = await ApiHelper.GetAsync<SupplierResponse>($"/suppliers/{id}");
                Id = supplier.Id;
                Name = supplier.Name;
                Email = supplier.Email;
                Phone = supplier.Phone;
            }
            catch { }
        }
        private async Task UpdateSupplierAsync()
        {
            success = await ApiHelper.PutAsync($"/suppliers/{Id}", new UpdateSupplierRequest
            {
                Name = Name,
                Email = Email,
                Phone = Phone,
            });
            if (success)
            {
                await UserInterfaceHelper.DisplayAlertAsync("Success", "Supplier updated successfully");
                return;
            }
            await UserInterfaceHelper.DisplayAlertAsync("Error", "Something went wrong");
        }
        private async Task CreateSupplierAsync()
        {
            success = await ApiHelper.PostAsync("/suppliers", new CreateSupplierRequest
            {
                Name = Name,
                Email = Email,
                Phone = Phone,
            });
            if (success)
            {
                await UserInterfaceHelper.DisplayAlertAsync("Success", "Supplier added successfully");
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
                    await UpdateSupplierAsync();
                    return;
                }
                await CreateSupplierAsync();
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
