using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
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
        
        public User CreateUser(UserRequest userRequest)
        {
            var request = new HttpRequestMessage();
            request.Method = HttpMethod.Post;
            request.RequestUri = new Uri(GoRestUrl);
            request.Content = new StringContent(
                JsonConvert.SerializeObject(userRequest), 
                Encoding.UTF8, "application/json"
                );
            request.Headers.Add("Accept", "application/json");

            var accessToken = _configuration.GetValue<string>("Settings:GoRestAccessToken");
            request.Headers.Add("Authorization", "Bearer " + accessToken);
            
            var httpResponseMessage =  _policy.RetryByMaxAttempts(request, 2);

            if (httpResponseMessage.StatusCode != HttpStatusCode.Created) return null;
           
            var content =  httpResponseMessage.Content.ReadAsStringAsync().Result;

            return JsonConvert.DeserializeObject<User>(content);
        }

        public async Task<List<User>> GetUsersAsync()
        {
            var request = new HttpRequestMessage();
            request.Method = HttpMethod.Get;
            request.RequestUri = new Uri(GoRestUrl);
            request.Headers.Add("Accept", "application/json");
            
            var httpResponseMessage = await _policy.RetryByMaxAttemptsAsync(request, 3);

            if (httpResponseMessage.StatusCode != HttpStatusCode.OK) return null;
            
            var content = await httpResponseMessage.Content.ReadAsStringAsync();
    
            return JsonConvert.DeserializeObject<List<User>>(content);
        }
    }
}