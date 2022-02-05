using System.Collections.Generic;
using Newtonsoft.Json;

namespace RetryPolly.Contracts
{
    public class University
    {
        [JsonProperty("name")] public string Name { get; set; }
        [JsonProperty("country")] public string Country { get; set; }
        [JsonProperty("web_pages")] public List<string> WebPages { get; set; }
        [JsonProperty("alpha_two_code")] public string AlphaTwoCode { get; set; }

        [JsonProperty("state-province")] public string StateProvince { get; set; }
        [JsonProperty("domains")] public List<string> Domains { get; set; }
    }
}