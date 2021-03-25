using InventorySystem.Mobile.ViewModels.Customer;
using Rg.Plugins.Popup.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace InventorySystem.Mobile.Views.Customer
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CustomerEntryPage : PopupPage
    {
        private readonly int _id;
        private CustomerEntryPageViewModel _customerEntryPageViewModel;
        public CustomerEntryPage(int id = 0)
        {
            InitializeComponent();
            _id = id;
        }
        protected override void OnBindingContextChanged()
        {
            base.OnBindingContextChanged();
            if(BindingContext is CustomerEntryPageViewModel customerEntryPageViewModel)
            {
                _customerEntryPageViewModel = customerEntryPageViewModel;
            }
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
            if(_id != 0)
            {
                CustomerEntryTitle.Text = "Update Customer";
                _customerEntryPageViewModel?.GetCustomerCommand.Execute(_id);
            }
        }
    }
}