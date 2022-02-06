using Newtonsoft.Json;

namespace RetryPolly.Contracts
{
    public class User : UserRequest
    {
        [JsonProperty("id")] public int Id { get; set; }
    }

    public class UserRequest
    {
        [JsonProperty("name")] public string name { get; set; }
        [JsonProperty("email")] public string email { get; set; }
        [JsonProperty("gender")] public string gender { get; set; }
        [JsonProperty("status")] public string status { get; set; }
    }
}