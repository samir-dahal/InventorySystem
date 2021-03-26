using InventorySystem.Mobile.ViewModels.Purchase;
using Rg.Plugins.Popup.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace InventorySystem.Mobile.Views.Purchase
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PurchaseEntryPage : PopupPage
    {
        private readonly int _id;
        private PurchaseEntryPageViewModel _purchaseEntryPageViewModel;
        public PurchaseEntryPage(int id = 0)
        {
            InitializeComponent();
            _id = id;
        }
        protected override void OnBindingContextChanged()
        {
            base.OnBindingContextChanged();
            if(BindingContext is PurchaseEntryPageViewModel purchaseEntryPageViewModel)
            {
                _purchaseEntryPageViewModel = purchaseEntryPageViewModel;
            }
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
            _purchaseEntryPageViewModel?.GetAllProductsCommand.Execute(null);
            _purchaseEntryPageViewModel?.GetAllSuppliersCommand.Execute(null);
            if(_id is not 0)
            {
                PurchaseEntryTitle.Text = "Update Purchase";
                _purchaseEntryPageViewModel?.GetPurchaseCommand.Execute(_id);
            }
        }
    }
}