using InventorySystem.Mobile.ViewModels.Product;
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
    public partial class ProductPage : ContentPage
    {
        private ProductPageViewModel _productPageViewModel;
        public ProductPage()
        {
            InitializeComponent();
        }
        protected override void OnBindingContextChanged()
        {
            base.OnBindingContextChanged();
            if (BindingContext is ProductPageViewModel productPageViewModel)
            {
                _productPageViewModel = productPageViewModel;
            }
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
            _productPageViewModel?.GetAllProductsCommand.Execute(null);
        }
    }
}