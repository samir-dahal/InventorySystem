using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InventorySystem.Mobile.ViewModels.Purchase;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace InventorySystem.Mobile.Views.Purchase
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PurchasePage : ContentPage
    {
        private PurchasePageViewModel _purchasePageViewModel;
        public PurchasePage()
        {
            InitializeComponent();
        }
        protected override void OnBindingContextChanged()
        {
            base.OnBindingContextChanged();
            if (BindingContext is PurchasePageViewModel purchasePageViewModel)
            {
                _purchasePageViewModel = purchasePageViewModel;
            }
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
            _purchasePageViewModel?.GetAllPurchasesCommand.Execute(null);
        }
    }
}