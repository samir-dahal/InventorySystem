using InventorySystem.Mobile.ViewModels.Customer;
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
    public partial class CustomerPage : ContentPage
    {
        private CustomerPageViewModel _customerPageViewModel;
        public CustomerPage()
        {
            InitializeComponent();
        }
        protected override void OnBindingContextChanged()
        {
            base.OnBindingContextChanged();
            if(BindingContext is CustomerPageViewModel customerPageViewModel)
            {
                _customerPageViewModel = customerPageViewModel;
            }
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
            _customerPageViewModel?.GetAllCustomersCommand.Execute(null);
        }
    }
}