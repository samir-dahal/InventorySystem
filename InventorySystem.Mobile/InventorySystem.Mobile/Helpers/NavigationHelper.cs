using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;
using System.Threading.Tasks;
using System.Linq;
using Rg.Plugins.Popup.Pages;
using Rg.Plugins.Popup.Services;

namespace InventorySystem.Mobile.Helpers
{
    public static class NavigationHelper
    {
        public static async Task PushPageAsync(Page page)
        {
            var thePage = App.Current.MainPage.Navigation.NavigationStack.FirstOrDefault(p => p.GetType() == page.GetType());
            if (thePage is null)
            {
                await App.Current.MainPage.Navigation.PushAsync(page);
            }
        }
        public static async Task PushRgPageAsync(PopupPage page)
        {
            var thePage = PopupNavigation.Instance.PopupStack.FirstOrDefault(p => p.GetType() == page.GetType());
            if (thePage is null)
            {
                await PopupNavigation.Instance.PushAsync(page);
            }
        }
        public static async Task PopAllRgPagesAsync()
        {
            if (PopupNavigation.Instance.PopupStack.Count > 0)
            {
                await PopupNavigation.Instance.PopAllAsync();
            }
        }
        public static async Task PopRgPageAsync()
        {
            if (PopupNavigation.Instance.PopupStack.Count > 0)
            {
                await PopupNavigation.Instance.PopAsync();
            }
        }
    }
}
