using InventorySystem.Mobile.ViewModels.Supplier;
using Rg.Plugins.Popup.Pages;
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
    public partial class SupplierEntryPage : PopupPage
    {
        private readonly int _id;
        private SupplierEntryPageViewModel _supplierEntryPageViewModel;
        public SupplierEntryPage(int id = 0)
        {
            InitializeComponent();
            _id = id;
        }
        protected override void OnBindingContextChanged()
        {
            base.OnBindingContextChanged();
            if(BindingContext is SupplierEntryPageViewModel supplierEntryPageViewModel)
            {
                _supplierEntryPageViewModel = supplierEntryPageViewModel;
            }
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
            if(_id is not 0)
            {
                SupplierEntryTitle.Text = "Update Supplier";
                _supplierEntryPageViewModel?.GetSupplierCommand.Execute(_id);
            }
        }
    }
}