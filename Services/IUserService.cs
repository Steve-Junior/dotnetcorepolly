using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using RetryPolly.Contracts;

namespace RetryPolly.Services
{
    public interface IUserService
    {
        public Task<User> CreateUserAsync(UserRequest user);
        public Task<List<User>> GetUsersAsync();
    }
}