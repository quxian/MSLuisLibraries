using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace MSLuisLibraries.Extension {
    static class HttpClientExtension {
        private const string _mediaTypeHeaderValue = "application/json";
        public static Task<HttpResponseMessage> PutAsJsonAsync<T>(this HttpClient client, Uri uri, T value) {
            var byteData = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(value));

            using (var content = new ByteArrayContent(byteData)) {
                content.Headers.ContentType = new MediaTypeHeaderValue(_mediaTypeHeaderValue);
                return client.PutAsync(uri, content);
            }
        }

        public static Task<HttpResponseMessage> PostAsJsonAsync<T>(this HttpClient client, Uri uri, T value) {
            var byteData = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(value));

            using (var content = new ByteArrayContent(byteData)) {
                content.Headers.ContentType = new MediaTypeHeaderValue(_mediaTypeHeaderValue);
                return client.PostAsync(uri, content);
            }
        }
    }
}
