using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
namespace InventorySystem.Mobile.Helpers
{
    public static class ApiHelper
    {
        public const string Root = "api";
        public const string Version = "v1";
        public const string Base = "/" + Root + "/" + Version;
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
                var response = await client.PostAsJsonAsync<T>(Base+uri, value);
                return response.IsSuccessStatusCode;
            }
        }
        public static async Task<T> GetAsync<T>(string uri)
        {
            using (var client = NetClient)
            {
                var response = await client.GetAsync(Base+uri);
                return await response.Content.ReadAsAsync<T>();
            }
        }
        public static async Task<bool> DeleteAsync(string uri)
        {
            using (var client = NetClient)
            {
                var response = await client.DeleteAsync(Base+uri);
                return response.IsSuccessStatusCode;
            }
        }
        public static async Task<bool> PutAsync<T>(string uri, T value)
        {
            using (var client = NetClient)
            {
                var response = await client.PutAsJsonAsync<T>(Base+uri, value);
                return response.IsSuccessStatusCode;
            }
        }
        public static async Task<T> ReadAs<T>(HttpContent value)
        {
            return await value.ReadAsAsync<T>();
        }
    }
}
