using InventorySystem.Mobile.ViewModels.Category;
using Rg.Plugins.Popup.Pages;
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
    public partial class CategoryEntryPage : PopupPage
    {
        private readonly int _id;
        private CategoryEntryPageViewModel _categoryEntryPageViewModel;
        public CategoryEntryPage(int id = 0)
        {
            InitializeComponent();
            _id = id;
        }
        protected override void OnBindingContextChanged()
        {
            base.OnBindingContextChanged();
            if (BindingContext is CategoryEntryPageViewModel categoryEntryPageViewModel)
            {
                _categoryEntryPageViewModel = categoryEntryPageViewModel;
            }
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
            if(_id is not 0)
            {
                CategoryEntryTitle.Text = "Update Category";
                _categoryEntryPageViewModel?.GetCategoryCommand.Execute(_id);
            }
        }
    }
}