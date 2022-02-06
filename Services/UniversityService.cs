using System.Collections.Generic;
using System.Globalization;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Polly;
using Polly.Retry;
using RetryPolly.Contracts;

namespace RetryPolly.Services
{
    public class UniversityService : IUniversityService
    {
        private const int MaxRetries = 3;
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly AsyncPolicy<HttpResponseMessage> _retryPolicy;
        public UniversityService(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
            _retryPolicy = Policy<HttpResponseMessage>.Handle<HttpRequestException>().RetryAsync(MaxRetries);
        }
        public async Task<List<University>> GetUniversitiesByCountryNameAsync(string countryName)
        {
            var client = _httpClientFactory.CreateClient("hipolabs");
            
            var httpResponseMessage = await _retryPolicy.ExecuteAsync(async () =>
            {
                var response =  await client.SendAsync(new HttpRequestMessage(HttpMethod.Get, "http://universities.hipolabs.com/search?country=" + countryName));

                return response.IsSuccessStatusCode ? response : null;
            });

            var content = await httpResponseMessage.Content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<List<University>>(content);
        }
    }
}