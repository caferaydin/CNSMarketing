using CNSMarketing.Application.Abstraction.ExternalService.Common;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Net;
using System.Net.Http;
using System.Text;

namespace CNSMarketing.Infrastructure.Services.Common
{
    public class ServiceManager<T> : IServiceManager<T>
    {
        private readonly IHttpClientFactory _clientFactory;

        public ServiceManager(IHttpClientFactory clientFactory)
        {
            _clientFactory = clientFactory;
        }

        public async Task<T> GetAsync(string url, Dictionary<string, string> headers = null)
        {
            try
            {

                var client = _clientFactory.CreateClient("LinkedInClient");
                client.DefaultRequestHeaders.Clear();
                AddHeaders(client, headers);

                var response = await client.GetAsync(url);

                var json = await response.Content.ReadAsStringAsync();
                T result = JsonConvert.DeserializeObject<T>(json);
                return result;
            }

            catch (Exception)
            {
                return default;
            }

        }

        public async Task<T> PostAsync(string url, object data, Dictionary<string, string> headers = null)
        {
            try
            {
                var client = _clientFactory.CreateClient("LinkedInClient");
                client.DefaultRequestHeaders.Clear();

                AddHeaders(client, headers);

                var content = new StringContent(JsonConvert.SerializeObject(data), Encoding.UTF8, "application/json");
                var response = await client.PostAsync(url, content);


                var json = await response.Content.ReadAsStringAsync();
                T result = JsonConvert.DeserializeObject<T>(json);
                return result;
            }
            catch (Exception)
            {
                return default;
            }

        }

        public async Task<T> PostBinaryAsync(string url, byte[] binaryData, Dictionary<string, string> headers = null)
        {
            try
            {
                var content = new ByteArrayContent(binaryData);

                var client = _clientFactory.CreateClient("LinkedInClient");
                client.DefaultRequestHeaders.Clear();

                AddHeaders(client, headers);

                client.DefaultRequestHeaders.TryAddWithoutValidation("Content-Type", "application/json");

                var response = await client.PostAsync(url, content);

                var json = await response.Content.ReadAsStringAsync();
                T result = JsonConvert.DeserializeObject<T>(json);
                return result;
            }
            catch (Exception)
            {
                return default;
            }

        }

        public async Task<T> PostFormEncodedResult(IEnumerable<KeyValuePair<string, string>> data, string url, Dictionary<string, string> headers)
        {
            try
            {
                // HttpClientFactory'den HttpClient oluşturuluyor
                var client = _clientFactory.CreateClient("LinkedInClient");
                client.DefaultRequestHeaders.Clear();

                // Form URL encoded content oluşturuluyor
                var formData = new FormUrlEncodedContent(data);

                // Default olarak başlıkları temizleyin

                // Başlıkları ekleyin
                client.DefaultRequestHeaders.TryAddWithoutValidation("Content-Type", "application/x-www-form-urlencoded");
                foreach (var item in headers)
                {
                    client.DefaultRequestHeaders.TryAddWithoutValidation(item.Key, item.Value);
                }

                // POST isteği gönderiliyor
                var response = await client.PostAsync(url, formData);

                // Yanıt alınıyor
                var json = await response.Content.ReadAsStringAsync();

                // JSON deserialization işlemi
                T result = JsonConvert.DeserializeObject<T>(json);
                return result;
            }
            catch (Exception ex)
            {
                // Hata durumunda default(T) döndürülüyor
                Console.WriteLine($"Error: {ex.Message}");
                return default;
            }
        }

        public async Task<T> PutAsync(string url, object data, Dictionary<string, string> headers = null)
        {

            try
            {
                var client = _clientFactory.CreateClient("LinkedInClient");
                client.DefaultRequestHeaders.Clear();

                AddHeaders(client, headers);

                var content = new StringContent(JsonConvert.SerializeObject(data), Encoding.UTF8, "application/json");
                var response = await client.PutAsync(url, content);

                var json = await response.Content.ReadAsStringAsync();
                T result = JsonConvert.DeserializeObject<T>(json);
                return result;

            }
            catch (Exception)
            {
                return default;
            }

        }

        public async Task<bool> DeleteAsync(string url, Dictionary<string, string> headers = null)
        {

            try
            {
                var client = _clientFactory.CreateClient("LinkedInClient");
                client.DefaultRequestHeaders.Clear();

                AddHeaders(client, headers);

                var response = await client.DeleteAsync(url);
                if (!response.IsSuccessStatusCode)
                {
                    return false;
                }

                return true;
            }
            catch (Exception)
            {
                throw;
            }

        }

        private void AddHeaders(HttpClient client, Dictionary<string, string> headers)
        {
            if (headers != null)
            {
                foreach (var header in headers)
                {
                    client.DefaultRequestHeaders.TryAddWithoutValidation(header.Key, header.Value);
                }
            }
        }
    }
}
