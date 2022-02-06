using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using RetryPolly.Contracts;

namespace RetryPolly.Services
{
    public class UserService : IUserService
    {
        private const string GoRestUrl = "https://gorest.co.in/public/v2/users";
        private readonly IPolicy _policy;
        private readonly IConfiguration _configuration;
        
        public UserService(IPolicy policy, IConfiguration iConfig)
        {
            _policy = policy;
            _configuration =  iConfig;
        }
        
        public async Task<User> CreateUserAsync(UserRequest userRequest)
        {
            var request = new HttpRequestMessage()
            {
                Method = HttpMethod.Post,
                RequestUri = new Uri(GoRestUrl),
                Content = new StringContent(
                    JsonConvert.SerializeObject(userRequest), 
                    Encoding.UTF8, "application/json"
                )
            };
            
            var accessToken = _configuration.GetValue<string>("Settings:GoRestAccessToken");
            request.Headers.Add("Authorization", "Bearer " + accessToken);
            request.Headers.Add("Accept", "application/json");

            var httpResponseMessage = await  _policy.RetryByMaxAttemptsAsync(request, 2);

            return httpResponseMessage.StatusCode != HttpStatusCode.Created 
                ? null 
                : JsonConvert.DeserializeObject<User>(
                    await  httpResponseMessage.Content.ReadAsStringAsync()
                    );
        }

        public async Task<List<User>> GetUsersAsync()
        {
            var request = new HttpRequestMessage()
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri(GoRestUrl)
            };
            
            request.Headers.Add("Accept", "application/json");
            
            var httpResponseMessage = await _policy.RetryByMaxAttemptsAsync(request, 3);

            return httpResponseMessage.StatusCode != HttpStatusCode.OK 
                ? null 
                : JsonConvert.DeserializeObject<List<User>>(
                    await httpResponseMessage.Content.ReadAsStringAsync()
                    );
        }
    }
}