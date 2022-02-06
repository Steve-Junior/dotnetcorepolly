using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace RetryPolly.Contracts
{
    public class User : UserRequest
    {
        [JsonProperty("id")] public int Id { get; set; }
    }

    public class UserRequest
    {
        [Required]
        [JsonProperty("name")]
        public string Name { get; set; }
        
        [Required]
        [JsonProperty("email")] 
        public string Email { get; set; }
        
        [Required]
        [JsonProperty("gender")] 
        public string Gender { get; set; }
        
        [Required]
        [JsonProperty("status")] 
        public string Status { get; set; }
    }
}