using InventorySystem.Mobile.ViewModels.Category;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace InventorySystem.Mobile.Views.Category
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CategoryPage : ContentPage
    {
        private CategoryPageViewModel _categoryPageViewModel;
        public CategoryPage()
        {
            InitializeComponent();
        }
        protected override void OnBindingContextChanged()
        {
            base.OnBindingContextChanged();
            if(BindingContext is CategoryPageViewModel categoryPageViewModel)
            {
                _categoryPageViewModel = categoryPageViewModel;
            }
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
            _categoryPageViewModel?.GetAllCategoriesCommand.Execute(null);
        }
    }
}