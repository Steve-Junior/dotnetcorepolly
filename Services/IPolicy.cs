using System.Net.Http;
using System.Threading.Tasks;
using Polly.Retry;

namespace RetryPolly.Services
{
    public interface IPolicy
    {
        public Task<HttpResponseMessage>  RetryByMaxAttemptsAsync(HttpRequestMessage httpRequestMessage,  int maxRetries);
        public HttpResponseMessage  RetryByMaxAttempts(HttpRequestMessage httpRequestMessage,  int maxRetries);
    }
}