using Microsoft.Extensions.Configuration;
using NSI.Common.Serialization;
using System;
using System.Diagnostics.CodeAnalysis;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace NSI.Proxy.Azure
{
    [ExcludeFromCodeCoverage]
    public class AzureProxy : IAzureProxy
    {
        private string _personalAccessToken { get; set; }

        public AzureProxy(IConfiguration configuration)
        {
            _personalAccessToken = configuration["PersonalAccessToken"];
        }

        public async Task<T> Get<T>(string url)
        {
            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Accept.Add(
                    new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic",
                    Convert.ToBase64String(
                        System.Text.ASCIIEncoding.ASCII.GetBytes(
                            string.Format("{0}:{1}", "", _personalAccessToken))));

                using (HttpResponseMessage response = await client.GetAsync(
                            url))
                {
                    response.EnsureSuccessStatusCode();
                    string responseBody = await response.Content.ReadAsStringAsync();
                    return JsonHelper.Deserialize<T>(responseBody);
                }
            }
        }

        public async Task<T> Post<T>(string url, object body)
        {
            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Add("Accept", "application/json; api-version=5.0");

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic",
                    Convert.ToBase64String(
                        System.Text.Encoding.ASCII.GetBytes(
                            string.Format("{0}:{1}", "", _personalAccessToken))));

                var json = JsonHelper.Serialize(body);
                var content = new StringContent(json, System.Text.Encoding.UTF8, "application/json");
                using (HttpResponseMessage response = await client.PostAsync(
                            url,
                            content
                            ))
                {
                    response.EnsureSuccessStatusCode();
                    string responseBody = await response.Content.ReadAsStringAsync();
                    return JsonHelper.Deserialize<T>(responseBody);
                }
            }
        }
    }
}
