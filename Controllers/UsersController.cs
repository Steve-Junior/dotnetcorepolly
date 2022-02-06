using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using RetryPolly.Contracts;
using RetryPolly.Services;

namespace RetryPolly.Controllers
{ 
    [ApiController]
    [Route("[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;
        
        public UsersController(IUserService userService)
        {
            _userService = userService;
        }
        
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] UserRequest request)
        {
            var user = await _userService.CreateUserAsync(request);
            
            return user == null ? BadRequest() : (IActionResult) Ok(user);
        }
        
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var users = await _userService.GetUsersAsync();
            
            return users == null ? NotFound() : (IActionResult) Ok(users);
        }
    }
}