using InventorySystem.Mobile.ViewModels.Product;
using Rg.Plugins.Popup.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace InventorySystem.Mobile.Views.Product
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ProductEntryPage : PopupPage
    {
        private readonly int _id;
        private ProductEntryPageViewModel _productEntryPageViewModel;
        public ProductEntryPage(int id = 0)
        {
            InitializeComponent();
            _id = id;
        }
        protected override void OnBindingContextChanged()
        {
            base.OnBindingContextChanged();
            if(BindingContext is ProductEntryPageViewModel productEntryPageViewModel)
            {
                _productEntryPageViewModel = productEntryPageViewModel;
            }
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
            _productEntryPageViewModel?.GetAllCategoriesCommand.Execute(null);
            if(_id is not 0)
            {
                ProductEntryTitle.Text = "Update Product";
                _productEntryPageViewModel?.GetProductCommand.Execute(_id);
            }
        }
    }
}