using System.Collections.Generic;
using System.Threading.Tasks;
using RetryPolly.Contracts;

namespace RetryPolly.Services
{
    public interface IUniversityService
    {
        public Task<List<University>> GetUniversitiesByCountryNameAsync(string countryName);
    }
}