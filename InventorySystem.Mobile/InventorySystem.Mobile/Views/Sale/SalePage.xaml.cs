using InventorySystem.Mobile.ViewModels.Sale;
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
    public partial class SalePage : ContentPage
    {
        private SalePageViewModel _salePageViewModel;
        public SalePage()
        {
            InitializeComponent();
        }
        protected override void OnBindingContextChanged()
        {
            base.OnBindingContextChanged();
            if(BindingContext is SalePageViewModel salePageViewModel)
            {
                _salePageViewModel = salePageViewModel;
            }
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
            _salePageViewModel?.GetAllSalesCommand.Execute(null);
        }
    }
}