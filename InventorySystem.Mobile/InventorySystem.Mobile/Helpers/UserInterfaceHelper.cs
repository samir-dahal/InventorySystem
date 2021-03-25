using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
namespace InventorySystem.Mobile.Helpers
{
    public static class UserInterfaceHelper
    {
        public static async Task DisplayAlertAsync(string title, string message)
        {
            await App.Current.MainPage.DisplayAlert(title, message, "OK");
        }
        public static async Task<bool> ConfirmationAlertAsync(string title, string message)
        {
            return await App.Current.MainPage.DisplayAlert(title, message, "Yes", "No");
        }
    }
}
