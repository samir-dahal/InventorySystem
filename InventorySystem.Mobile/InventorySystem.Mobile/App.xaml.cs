using InventorySystem.Mobile.Views;
using InventorySystem.Mobile.Views.Category;
using InventorySystem.Mobile.Views.Product;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
[assembly: ExportFont("materialdesignicons.ttf", Alias = "Material")]
[assembly: ExportFont("Poppins-Regular.ttf", Alias = "PoppinsRegular")]
[assembly: ExportFont("Poppins-SemiBold.ttf", Alias = "PoppinsSemibold")]
namespace InventorySystem.Mobile
{
    public partial class App : Application
    {
        public static string Url => "http://localhost:5000";
        public App()
        {
            InitializeComponent();

            MainPage = new NavigationPage(new MainPage());
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
