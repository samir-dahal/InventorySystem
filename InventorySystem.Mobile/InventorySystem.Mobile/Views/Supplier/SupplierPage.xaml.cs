using InventorySystem.Mobile.ViewModels.Supplier;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace InventorySystem.Mobile.Views.Supplier
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SupplierPage : ContentPage
    {
        private SupplierPageViewModel _supplierPageViewModel;
        public SupplierPage()
        {
            InitializeComponent();
        }
        protected override void OnBindingContextChanged()
        {
            base.OnBindingContextChanged();
            if(BindingContext is SupplierPageViewModel supplierPageViewModel)
            {
                _supplierPageViewModel = supplierPageViewModel;
            }
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
            _supplierPageViewModel?.GetAllSuppliersCommand.Execute(null);
        }
    }
}