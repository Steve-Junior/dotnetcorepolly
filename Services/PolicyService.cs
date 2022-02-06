using System.Net.Http;
using System.Threading.Tasks;
using Polly;
using Polly.Retry;

namespace RetryPolly.Services
{
    public class PolicyService : IPolicy
    {
        private readonly IHttpClientFactory _httpClient;
        
        public PolicyService(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory;
        }

        public HttpResponseMessage RetryByMaxAttempts(HttpRequestMessage httpRequestMessage, int maxRetries)
        {
            var policy = Policy<HttpResponseMessage>.Handle<HttpRequestException>().Retry(maxRetries);
            
            return policy.Execute(() => _httpClient.CreateClient().Send(httpRequestMessage));
        }
        public async Task<HttpResponseMessage> RetryByMaxAttemptsAsync(HttpRequestMessage httpRequestMessage, int maxRetries = 1)
        {
            var policy = Policy<HttpResponseMessage>.Handle<HttpRequestException>().RetryAsync(maxRetries);
            
            return await policy.ExecuteAsync(async () => await _httpClient.CreateClient().SendAsync(httpRequestMessage));
        }
    }
}