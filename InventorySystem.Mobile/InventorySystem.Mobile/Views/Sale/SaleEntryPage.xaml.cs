using InventorySystem.Mobile.ViewModels.Sale;
using Rg.Plugins.Popup.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace InventorySystem.Mobile.Views.Sale
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SaleEntryPage : PopupPage
    {
        private SaleEntryPageViewModel _saleEntryPageViewModel;
        public SaleEntryPage()
        {
            InitializeComponent();
        }
        protected override void OnBindingContextChanged()
        {
            base.OnBindingContextChanged();
            if(BindingContext is SaleEntryPageViewModel saleEntryPageViewModel)
            {
                _saleEntryPageViewModel = saleEntryPageViewModel;
            }
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
            _saleEntryPageViewModel?.GetAllCustomersCommand.Execute(null);
            _saleEntryPageViewModel?.GetAllPurchasesCommand.Execute(null);
        }
    }
}