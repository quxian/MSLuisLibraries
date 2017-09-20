using MSLuisLibraries.Entities;
using MSLuisLibraries.Extension;
using MSLuisLibraries.Interface;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace MSLuisLibraries {
    class Models : IModels {
        private HttpClient _client;
        public HttpClient Client { get => _client; set => _client = value; }
        private Uri _uri;
        public Uri Uri { get => _uri; set => _uri = value; }

        public Models(HttpClient client, Uri uri) {
            Client = client;
            Uri = uri;
        }

        public async Task<ClosedListEntity[]> GetClosedListEntityAsync() {
            var httpResult = await _client.GetStringAsync(new Uri(Uri, "closedlists"));
            return JsonConvert.DeserializeObject<ClosedListEntity[]>(httpResult);
        }

        public async Task<ClosedListEntity> GetClosedListEntityByIdAsync(string id) {
            var httpResult = await _client.GetStringAsync(new Uri(Uri, $"closedlists/{id}"));
            return JsonConvert.DeserializeObject<ClosedListEntity>(httpResult);
        }

        public async Task<bool> UpdateClosedListEntityAsync(string id, ClosedListEntity entity) {
            var httpResult = await _client.PutAsJsonAsync(new Uri(Uri, $"closedlists/{id}"), entity);
            return httpResult.IsSuccessStatusCode;
        }

        public async Task<string> CreateClosedListEntityAsync(ClosedListEntity entity) {
            var httpResult = await _client.PostAsJsonAsync(new Uri(Uri, "closedlists"), entity);
            if (httpResult.IsSuccessStatusCode) {

                return await httpResult.Content.ReadAsStringAsync();
            }

            return null;
        }

        public async Task<bool> DeleteClosedListEntityByIdAsync(string id) {
            var httpResult = await _client.DeleteAsync(new Uri(Uri, $"closedlists/{id}"));
            return httpResult.IsSuccessStatusCode;
        }

        public async Task<bool> UpdateClosedListSublistAsync(string id, int subId, Sublist subList) {
            var httpResult = await _client.PutAsJsonAsync(new Uri(Uri, $"closedlists/{id}/sublists/{subId}"), subList);
            return httpResult.IsSuccessStatusCode;
        }

        public async Task<int?> AddClosedListSublistAsync(string id, Sublist subList) {
            var httpResult = await _client.PostAsJsonAsync(new Uri(Uri, $"closedlists/{id}/sublists"), subList);
            if (httpResult.IsSuccessStatusCode) {
                return int.Parse(await httpResult.Content.ReadAsStringAsync());
            }

            return null;
        }
    }
}
