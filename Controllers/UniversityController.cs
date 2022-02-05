using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using RetryPolly.Services;

namespace RetryPolly.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UniversityController : ControllerBase
    {
        private readonly IUniversityService _universityService;

        public UniversityController(
            IUniversityService universityService)
        {
            _universityService = universityService;
        }

        [HttpGet("universities")]
        public async Task<IActionResult> GetUniversitiesByCountry(string countryName)
        {
            var universities = await _universityService.GetUniversitiesByCountryNameAsync(countryName);

            return universities == null ? NotFound() : (IActionResult) Ok(universities);
        }
    }
}