using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.CommunityToolkit.ObjectModel;
using InventorySystem.Mobile.Helpers;
using System.Threading.Tasks;
using InventorySystem.Mobile.Views.Product;
using InventorySystem.Mobile.Views.Category;
using InventorySystem.Mobile.Models;
using InventorySystem.Mobile.Views.Customer;
using InventorySystem.Mobile.Views.Supplier;
using InventorySystem.Mobile.Views.Purchase;

namespace InventorySystem.Mobile.ViewModels
{
    public enum MenuType
    {
        Products,
        Suppliers,
        Categories,
        Purchases,
        Sales,
        Customers,
    }
    public class MainPageViewModel : ObservableBase
    {
        public IAsyncCommand<MenuType> NavigateToPageCommand { get; }
        public ObservableRangeCollection<MenuModel> Menus { get; set; } = new();
        public MainPageViewModel()
        {
            InitializeMenus();
            NavigateToPageCommand = new AsyncCommand<MenuType>(async (type) => await OnNavigateAsync(type));
        }
        private async Task OnNavigateAsync(MenuType type)
        {
            switch (type)
            {
                case MenuType.Products:
                    await NavigationHelper.PushPageAsync(new ProductPage());
                    break;
                case MenuType.Categories:
                    await NavigationHelper.PushPageAsync(new CategoryPage());
                    break;
                case MenuType.Customers:
                    await NavigationHelper.PushPageAsync(new CustomerPage());
                    break;
                case MenuType.Suppliers:
                    await NavigationHelper.PushPageAsync(new SupplierPage());
                    break;
                case MenuType.Purchases:
                    await NavigationHelper.PushPageAsync(new PurchasePage());
                    break;
            }
        }
        private void InitializeMenus()
        {
            List<MenuModel> menus = new();
            menus.Add(new MenuModel
            {
                Name = nameof(MenuType.Products),
                Icon = MaterialIconHelper.Gift,
                MenuType = MenuType.Products,
            });
            menus.Add(new MenuModel
            {
                Name = nameof(MenuType.Categories),
                Icon = MaterialIconHelper.Shape,
                MenuType = MenuType.Categories,
            });
            menus.Add(new MenuModel
            {
                Name = nameof(MenuType.Suppliers),
                Icon = MaterialIconHelper.TruckDelivery,
                MenuType = MenuType.Suppliers,
            });
            menus.Add(new MenuModel
            {
                Name = nameof(MenuType.Purchases),
                Icon = MaterialIconHelper.CashMultiple,
                MenuType = MenuType.Purchases,
            });
            menus.Add(new MenuModel
            {
                Name = nameof(MenuType.Sales),
                Icon = MaterialIconHelper.Sale,
                MenuType = MenuType.Sales,
            });
            menus.Add(new MenuModel
            {
                Name = nameof(MenuType.Customers),
                Icon = MaterialIconHelper.AccountGroup,
                MenuType = MenuType.Customers,
            });
            Menus.AddRange(menus);
        }
    }
}
