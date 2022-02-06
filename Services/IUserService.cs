using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using RetryPolly.Contracts;

namespace RetryPolly.Services
{
    public interface IUserService
    {
        public User CreateUser(UserRequest user);
        public Task<List<User>> GetUsersAsync();
    }
}