using InventorySystem.Contracts.v1._0;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
namespace InventorySystem.Mobile.Helpers
{
    public static class ApiHelper
    {
        public static HttpClient NetClient
        {
            get
            {
                var netClient = new HttpClient();
                netClient.BaseAddress = new Uri(App.Url);
                return netClient;
            }
        }
        public static async Task<bool> PostAsync<T>(string uri, T value)
        {
            using (var client = NetClient)
            {
                var response = await client.PostAsJsonAsync<T>(ApiRoutes.Base+uri, value);
                return response.IsSuccessStatusCode;
            }
        }
        public static async Task<T> GetAsync<T>(string uri)
        {
            using (var client = NetClient)
            {
                var response = await client.GetAsync(ApiRoutes.Base+uri);
                return await response.Content.ReadAsAsync<T>();
            }
        }
        public static async Task<bool> DeleteAsync(string uri)
        {
            using (var client = NetClient)
            {
                var response = await client.DeleteAsync(ApiRoutes.Base+uri);
                return response.IsSuccessStatusCode;
            }
        }
        public static async Task<bool> PutAsync<T>(string uri, T value)
        {
            using (var client = NetClient)
            {
                var response = await client.PutAsJsonAsync<T>(ApiRoutes.Base+uri, value);
                return response.IsSuccessStatusCode;
            }
        }
    }
}
